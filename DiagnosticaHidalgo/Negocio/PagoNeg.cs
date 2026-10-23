using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Clases.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace DiagnosticaHidalgo.Negocio
{
    public class PagoNeg
    {
        string _cadenaconn;
        DataClassDataContext context;

        #region Consructor
        public PagoNeg(string cadenaconn)
        {
            this._cadenaconn = cadenaconn;
            this.context = new DataClassDataContext(_cadenaconn);
        }
        #endregion
        #region Agregar
        /// <summary>
        /// Agregar un pago tipo devolución.
        /// Por el momento no cambia el estatus de la venta (Pendiente | Vigente).
        /// Si se hace la devolución se verá reflejado en el total del corte de turno.
        /// </summary>
        /// <param name="lPagos"></param>
        /// <param name="IdVtaPagoParcial"></param>
        /// <param name="msjError"></param>
        /// <returns></returns>
        public bool ArgegarDevolucion(Pago pago, ref string msjError)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Pago));
                StringWriter pagoxml = new StringWriter();
                xmlSerializer.Serialize(pagoxml, pago);

                using (SqlConnection conn = new SqlConnection(_cadenaconn))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("PagoDevolucion", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("PAGO", pagoxml.ToString());
                    command.ExecuteNonQuery();
                    return true;             
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, "VentaPagoNeg", "ArgegarDevolucion", "ADMIN");
                msjError = "No se pudo realizar la devolución";
                return false;
            }
        }
        #endregion
        /// <summary>
        /// Obtener los totales de los pagos por turno.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public CorteTurno ActualizarCorteTurnoPorPagos(CorteTurno corteTurno)
        {
            int IdCorteTurno = corteTurno.IdCorteTurno;
            var totales = (from p in context.Pago.Where(p => p.IdCorteTurno == IdCorteTurno && (p.Cancelado == null || p.Cancelado == false))
                           join cta in context.CtaBancaria on p.IdCtaBancaria equals cta.IdCtaBancaria into ctaSet
                           from CS in ctaSet.DefaultIfEmpty()
                           group p by new { p.TipoPago, CS.NumCuenta } into grupo
                           select new
                           {
                               TipoPago = grupo.First().TipoPago,
                               NumPagos = grupo.Count(),
                               Total = grupo.Sum(p => p.Cantidad)
                           }).ToList();

            var efectivo = totales.Where(t => t.TipoPago == C.PAGO_EFECTIVO).FirstOrDefault();
            corteTurno.TotalEfectivo = efectivo == null ? 0 : efectivo.Total;
            corteTurno.TotalEfectivo = decimal.Round(corteTurno.TotalEfectivo.Value, 2);
            var totalesSum = totales.Where(t => t.TipoPago != C.PAGO_EFECTIVO).ToList();
            if (totalesSum.Count() > 0)
            {
                corteTurno.TotalCtas = totalesSum.Sum(t => t.Total);
                corteTurno.TotalCtas = decimal.Round(corteTurno.TotalCtas.Value, 2);
            }
            else corteTurno.TotalCtas = 0;

            corteTurno.MontoTotal = corteTurno.TotalEfectivo + corteTurno.TotalCtas;
            corteTurno.MontoTotal = decimal.Round(corteTurno.MontoTotal.Value, 2);

            return corteTurno;
        }

        /// <summary>
        /// Obtener el total que existe en cada cuenta.
        /// </summary>
        public List<TotalCuenta> ObtenerTotalPorCuenta(int IdCorteTurno)
        {
            List<TotalCuenta> lTotal = (from p in context.Pago.Where(p => p.IdCorteTurno == IdCorteTurno && p.IdCtaBancaria.HasValue)
                                        join cta in context.CtaBancaria on p.IdCtaBancaria equals cta.IdCtaBancaria
                                        group p by new { cta.NumCuenta, cta.Alias } into grupo
                                        select new TotalCuenta
                                        {
                                            Alias = grupo.Key.Alias,
                                            NumCuenta = grupo.Key.NumCuenta,
                                            Cantidad = (double)grupo.Sum(p => p.Cantidad)
                                        }).ToList();

            return lTotal;
        }

        /// <summary>
        /// Método para obtener los pagos que fueron hechos en el corte de turno 
        /// pero que no pertenecen a alguna venta de ese mismo corte.
        /// </summary>
        public object ObtenerPorIdCorteTurno(int IdCorteTurno)
        {
            var pagos = (from p in context.Pago
                         join v in context.Venta on p.IdVenta equals v.IdVenta
                         where p.IdCorteTurno == IdCorteTurno
                         orderby p.IdPago ascending
                         select new
                         {
                             FechaPago = p.FechaCreacion,
                             FechaVenta = v.FechaCreacion,
                             NumeroPago = p.NumPago,
                             PagoDevolucion = p.Devolucion.Value == true ? "DEVOLUCION" : "PAGO",
                             Cantidad = Math.Abs(p.Cantidad.Value),
                             TipoPago = p.TipoPago,
                         }).ToList();


            return pagos.Count > 0 ? pagos : null;
        }

        /// <summary>
        /// Método para obtener los pagos que fueron hechos en el corte de turno 
        /// pero que no pertenecen a alguna venta de ese mismo corte.
        /// </summary>
        public object ObtenerPorIdCorteTurnoSinVenta(int IdCorteTurno)
        {
            var pagos = (from p in context.Pago
                         join v in context.Venta on p.IdVenta equals v.IdVenta
                         where p.IdCorteTurno == IdCorteTurno && v.IdCorteTurno != IdCorteTurno
                         orderby p.IdPago ascending
                         select new
                         {
                             FechaPago = p.FechaCreacion,
                             FechaVenta = v.FechaCreacion,
                             NumeroPago = p.NumPago,
                             PagoDevolucion = p.Devolucion.Value == true ? "Devolución" : "Pago",
                             Cantidad = p.Cantidad,
                             TipoPago = p.TipoPago,
                         }).ToList();


            return pagos.Count > 0 ? pagos : null;
        }

        /// <summary>
        /// Obtener el siguiente número de pago
        /// </summary>
        public int ObtenerSigNumPago(int IdVenta)
        {
            int numPago = context.Pago.Where(p => p.IdVenta == IdVenta).ToList().Count + 1;
            return numPago;
        }
    }
}
