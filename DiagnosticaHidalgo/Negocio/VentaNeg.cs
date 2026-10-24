using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Clases.Modelos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using DiagnosticaHidalgo.Clases.Seguridad;

namespace DiagnosticaHidalgo.Negocio
{
    public class VentaNeg
    {
        #region CONST
        #region PDF_ANALYZER
        public const int MARGENTOP = 80;
        public const int MARGENTOPTOTALES = 210;
        #endregion
        #region PDF_DIAGNOSTICA_HIDALGO
        public const int MARGENTOP_DH = 95;
        public const int MARGENTOPTOTALES_DH = 230;
        public const int PADDINGTOP_CELL = -2;
        #endregion
        #region Estatus
        public const string EST_COTIZACION = "Z";
        public const string EST_CANCELADA = "C";
        public const string EST_VIGENTE = "V";
        public const string EST_PENDIENTE = "P";
        #endregion
        #endregion

        string _cadenaconn;
        DataClassDataContext context;
        #region Constructor
        public VentaNeg(string cadenaconn)
        {
            this._cadenaconn = cadenaconn;
            this.context = new DataClassDataContext(_cadenaconn);
        }
        #endregion
        #region Agregar
        /// <summary>
        /// Agregar una venta por procedimiento almacenado
        /// </summary>
        public bool Agregar(Venta venta, List<LabPorVenta> laboratorios, Pago pago, ref string msjError)
        {
            bool res = true;
            try
            {
                //Si se pago el total de la cuenta queda como Vigente, de lo contrario está como Pendiente
                venta.Estatus = venta.Total == pago.Cantidad ? EST_VIGENTE : EST_PENDIENTE;

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Venta));
                StringWriter ventaxml = new StringWriter();
                xmlSerializer.Serialize(ventaxml, venta);

                XmlSerializer xmlSerLab = new XmlSerializer(typeof(List<LabPorVenta>));
                StringWriter labxml = new StringWriter();
                xmlSerLab.Serialize(labxml, laboratorios);

                XmlSerializer xmlSerPago = new XmlSerializer(typeof(Pago));
                StringWriter pagoxml = new StringWriter();
                xmlSerPago.Serialize(pagoxml, pago);

                using (SqlConnection conn = new SqlConnection(_cadenaconn))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("venta_insert", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@VENTA", SqlDbType.Xml);
                    command.Parameters["@VENTA"].Value = ventaxml.ToString();
                    command.Parameters.Add("@LABORATORIOS", SqlDbType.Xml);
                    command.Parameters["@LABORATORIOS"].Value = labxml.ToString();
                    command.Parameters.Add("@PAGO", SqlDbType.Xml);
                    command.Parameters["@PAGO"].Value = pagoxml.ToString();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            msjError = reader["mensaje"].ToString();
                            venta.IdVenta = Convert.ToInt32(reader["idventa"].ToString());
                            res = venta.IdVenta > 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, "VentaNeg", "Agregar", "EMPLEADO");
                msjError = "Ocurrió un error al intentar realizar la venta.";
                res = false;
            }

            return res;
        }

        /// <summary>
        /// Inserta un nuevo registro en la tabla Venta como cotización, junto con los laboratorios
        /// </summary>
        public bool AgregarCotizacion(Venta venta, List<LabPorVenta> laboratorios, ref string msjError)
        {
            bool res = true;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Venta));
                StringWriter ventaxml = new StringWriter();
                xmlSerializer.Serialize(ventaxml, venta);

                XmlSerializer xmlSerLab = new XmlSerializer(typeof(List<LabPorVenta>));
                StringWriter labxml = new StringWriter();
                xmlSerLab.Serialize(labxml, laboratorios);

                using (SqlConnection conn = new SqlConnection(_cadenaconn))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("cotizacion_insert", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@VENTA", SqlDbType.Xml);
                    command.Parameters["@VENTA"].Value = ventaxml.ToString();
                    command.Parameters.Add("@LABORATORIOS", SqlDbType.Xml);
                    command.Parameters["@LABORATORIOS"].Value = labxml.ToString();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            msjError = reader["mensaje"].ToString();
                            venta.IdVenta = Convert.ToInt32(reader["idventa"].ToString());
                            res = venta.IdVenta > 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, "VentaNeg", "AgregarCotizacion", "EMPLEADO");
                msjError = "Ocurrió un error al intentar realizar la cotización.";
                res = false;
            }

            return res;
        }
        #endregion
        #region Eliminar
        public void EliminarCotizacion(int idVenta)
        {
            using (SqlConnection conn = new SqlConnection(_cadenaconn))
            {
                conn.Open();

                SqlCommand command = new SqlCommand("deletecotizacion", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("IdVenta", idVenta);
                command.ExecuteNonQuery();
            }
        }
        #endregion
        #region Consultar Ventas
        /// <summary>
        /// Obtener cotizacion por IdVenta
        /// </summary>
        /// <returns></returns>
        public Cotizacion ObtenerCotizacionPorId(int Id)
        {
            Cotizacion cotizacion = (from v in context.Venta
                                     join u in context.Usuario on v.IdUsuario equals u.IdUsuario
                                     join c in context.Cliente on v.IdCliente equals c.IdCliente
                                     join d in context.Doctor on v.IdDoctor equals d.IdDoctor
                                     where v.Estatus == EST_COTIZACION && v.IdVenta == Id
                                     orderby v.Folio descending
                                     select new Cotizacion()
                                     {
                                         IdVenta = Id,
                                         IdUsuario = u.IdUsuario,
                                         ClaveUsuario = u.Clave,
                                         IdCliente = v.IdCliente,
                                         NombreCliente = c.Nombre,
                                         IdDoctor = v.IdDoctor.Value,
                                         NombreDoctor = d.Nombre,
                                         laboratorios = context.LabPorVenta.Where(lxv => lxv.IdVenta == Id).ToList(),
                                         Total = v.Total.Value,
                                         SubTotal = v.SubTotal.Value,
                                         Descuento = v.Descuento.HasValue ? v.Descuento.Value : -1,
                                         Impuestos = v.Impuestos.Value
                                     }).FirstOrDefault();


            if (cotizacion != null)
            {
                cotizacion.labResumidos = new List<LabResumido>();
                List<LabPorVenta> nuevosLab = new List<LabPorVenta>();
                
                foreach (LabPorVenta item in cotizacion.laboratorios)
                {
                    Laboratorio lab = context.Laboratorio.Where(laboratorio => laboratorio.IdLaboratorio == item.IdLaboratorio).FirstOrDefault();
                    cotizacion.labResumidos.Add(new LabResumido()
                    {
                        Clave = lab.Clave,
                        Nombre = lab.Descripcion,
                        Costo = item.Costo.Value,
                        Cantidad = item.NumLaboratorios,
                        Familia = lab.Familia
                    });
                    nuevosLab.Add(new LabPorVenta()
                    {
                        IdLaboratorio = lab.IdLaboratorio,
                        NumLaboratorios = item.NumLaboratorios,
                        SubTotal = item.SubTotal,
                        Total=item.Total,
                        Costo = item.Costo,
                        Reservacion = item.Reservacion
                    });
                }

                cotizacion.laboratorios = nuevosLab;
            }
            return cotizacion;
        }
        /// <summary>
        /// Obtener el folio para una nueva venta.
        /// </summary>
        /// <returns></returns>
        public int ObtenerUltimoFolio()
        {
            int? ultFolio = (from v in context.Venta
                             where v.Estatus != EST_COTIZACION
                             orderby v.Folio descending
                             select v.Folio).FirstOrDefault();

            context.Connection.Close();
            return ultFolio.Value;
        }

        /// <summary>
        /// Obtener el folio para una nueva venta.
        /// </summary>
        /// <returns></returns>
        public int ObtenerSigFolio()
        {
            int? sigFoilio = ((from v in context.Venta
                               where v.Estatus != EST_COTIZACION
                               orderby v.Folio descending
                               select v.Folio).FirstOrDefault() + 1);
            context.Connection.Close();

            return sigFoilio.Value;
        }

        /// <summary>
        /// Metodo para obtener todas las ventas por corte de turno.
        /// </summary>
        /// <returns></returns>
        public object ObtenerPorIdCorteTurno(int IdCorteTurno)
        {
            var registros = (from v in context.Venta
                             join c in context.Cliente on v.IdCliente equals c.IdCliente
                             join u in context.Usuario on v.IdUsuario equals u.IdUsuario
                             join d in context.Doctor on v.IdDoctor equals d.IdDoctor into DoctorSet
                             from DS in DoctorSet.DefaultIfEmpty()
                             where v.IdCorteTurno == IdCorteTurno && v.Estatus != EST_COTIZACION
                             orderby v.Folio descending
                             select new
                             {
                                 Id = v.IdVenta,
                                 Folio = v.Folio,
                                 Fecha = v.FechaCreacion,
                                 Maquina = v.NumMaquina,
                                 Usuario = u.Nombre,
                                 Cliente = c.Nombre,
                                 Doctor = v.IdDoctor != null ? DS.Nombre : "",
                                 TotalVenta = v.Total,
                                 PreciosAlterados = context.LabPorVenta.Count(lv => lv.IdVenta == v.IdVenta && lv.Costo.Value != lv.CostoOriginal.Value) > 0 ? "SI" : "NO",
                                 Descuento = v.Descuento,
                                 Pagado = context.Pago.Where(p => p.IdVenta == v.IdVenta).Sum(p => p.Cantidad),
                                 Estatus = v.Estatus == "P" ? "PENDIENTE" : v.Estatus == "C" ? "CANCELADA" : "VIGENTE",
                                 Observaciones = v.Observaciones
                             });

            return registros;
        }

        /// <summary>
        /// Metodo para obtener todas las ventas por corte de turno.
        /// </summary>
        /// <returns></returns>
        public object ObtenerTodas()
        {
            var registros = (from v in context.Venta
                             join c in context.Cliente on v.IdCliente equals c.IdCliente
                             join u in context.Usuario on v.IdUsuario equals u.IdUsuario
                             join d in context.Doctor on v.IdDoctor equals d.IdDoctor into DoctorSet
                             from DS in DoctorSet.DefaultIfEmpty()
                             where v.Estatus != EST_COTIZACION
                             orderby v.Folio descending
                             select new
                             {
                                 Id = v.IdVenta,
                                 Folio = v.Folio,
                                 Fecha = v.FechaCreacion,
                                 Maquina = v.NumMaquina,
                                 Usuario = u.Nombre,
                                 Cliente = c.Nombre,
                                 Doctor = v.IdDoctor != null ? DS.Nombre : "",
                                 TotalVenta = v.Total,
                                 Descuento = v.Descuento,
                                 Pagado = context.Pago.Where(p => p.IdVenta == v.IdVenta).Sum(p => p.Cantidad),
                                 Estatus = v.Estatus == "P" ? "PENDIENTE" : v.Estatus == "C" ? "CANCELADA" : "VIGENTE",
                                 Observaciones = v.Observaciones
                             });

            return registros;
        }

        /// <summary>
        /// Metodo para obtener todas las ventas por corte de turno.
        /// </summary>
        /// <param name="IdCliente">Identificador del cliente</param>
        /// <param name="IdLaboratorio">Identificador del laboratorio</param>
        /// <param name="IdDoctor">Identificador del doctor</param>
        /// <returns></returns>
        public DataTable ObtenerPorFiltros(int IdCliente, int IdLaboratorio, int IdDoctor)
        {
            DataTable res = new DataTable();
            try
            {
                string query = "SELECT v.IdVenta Id," +
                            "v.Folio Folio," +
                            "v.FechaCreacion Fecha," +
                            "u.Nombre Usuario," +
                            "c.Nombre Cliente," +
                            "ISNULL(d.Nombre, '') Doctor," +
                            "v.Total TotalVenta," +
                            "v.Descuento Descuento," +
                            "SUM(distinct p.Cantidad) Pagado," +
                            "CASE v.Estatus" +
                                " WHEN 'P' THEN 'PENDIENTE' " +
                                " WHEN 'C' THEN 'CANCELADA' " +
                                " WHEN 'V' THEN 'VIGENTE' " +
                                " ELSE '' " +
                                " END Estatus, " +
                            "v.Observaciones Observaciones" +
                            " FROM Venta v" +
                            " INNER JOIN Usuario u ON v.IdUsuario = u.IdUsuario" +
                            " INNER JOIN LabPorVenta lv ON v.IdVenta = lv.IdVenta" +
                            " INNER JOIN Pago p ON v.IdVenta = p.IdVenta" +
                            " LEFT JOIN Cliente c ON v.IdCliente = c.IdCliente" +
                            " LEFT JOIN Doctor d ON v.IdDoctor = d.IdDoctor ";


                string queryWhere = " WHERE v.Estatus <> '" + EST_COTIZACION + "' ";
                if (IdCliente > 0 || IdLaboratorio > 0 || IdDoctor > 0)
                {
                    if (IdCliente > 0)
                        queryWhere = queryWhere + " AND v.IdCliente = @IdCliente ";
                    if (IdDoctor > 0)
                        queryWhere = queryWhere + " AND v.IdDoctor = @IdDoctor ";
                    if (IdLaboratorio > 0)
                        queryWhere = queryWhere + " AND lv.IdLaboratorio = @IdLaboratorio";
                }
                query = query + queryWhere;

                //group by
                query = query + " GROUP BY v.IdVenta, v.Folio, v.FechaCreacion, u.Nombre," +
                        "c.Nombre, d.Nombre, v.Total, v.Descuento, v.Observaciones, v.Estatus" +
                        " ORDER BY v.Folio DESC";

                using (SqlConnection conn = new SqlConnection(_cadenaconn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Clear();
                    if (IdCliente > 0)
                        cmd.Parameters.AddWithValue("IdCliente", IdCliente);
                    if (IdLaboratorio > 0)
                        cmd.Parameters.AddWithValue("IdLaboratorio", IdLaboratorio);
                    if (IdDoctor > 0)
                        cmd.Parameters.AddWithValue("IdDoctor", IdDoctor);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(res);

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje(ex.Message);
            }
            return res;
        }

        /// <summary>
        /// Metodo para obtener todas las cotizaciones.
        /// </summary>
        /// <param name="IdCliente">Identificador del cliente</param>
        /// <param name="IdLaboratorio">Identificador del laboratorio</param>
        /// <param name="IdDoctor">Identificador del doctor</param>
        /// <returns></returns>
        public DataTable ObtenerCotizacionesPorFiltros(int IdCliente, int IdLaboratorio)
        {
            DataTable res = new DataTable();
            try
            {
                string query = "SELECT v.IdVenta Id," +
                            "v.FechaCreacion Fecha," +
                            "u.Nombre Usuario," +
                            "c.Nombre Cliente," +
                            "ISNULL(d.Nombre, '') Doctor," +
                            "v.Total TotalVenta," +
                            "v.Descuento Descuento," +
                            "v.Observaciones Observaciones" +
                            " FROM Venta v" +
                            " INNER JOIN Usuario u ON v.IdUsuario = u.IdUsuario" +
                            " INNER JOIN LabPorVenta lv ON v.IdVenta = lv.IdVenta" +
                            " LEFT JOIN Cliente c ON v.IdCliente = c.IdCliente" +
                            " LEFT JOIN Doctor d ON v.IdDoctor = d.IdDoctor ";

                string queryWhere = " WHERE v.Estatus = '" + EST_COTIZACION + "' ";
                if (IdCliente > 0 || IdLaboratorio > 0)
                {
                    if (IdCliente > 0)
                        queryWhere = queryWhere + " AND v.IdCliente = @IdCliente ";
                    if (IdLaboratorio > 0)
                        queryWhere = queryWhere + " AND lv.IdLaboratorio = @IdLaboratorio";
                }
                query = query + queryWhere;

                query = query + " GROUP BY v.IdVenta, v.FechaCreacion, u.Nombre," +
                        "c.Nombre, d.Nombre, v.Total, v.Descuento, v.Observaciones" +
                        " ORDER BY v.FechaCreacion DESC";

                using (SqlConnection conn = new SqlConnection(_cadenaconn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Clear();
                    if (IdCliente > 0)
                        cmd.Parameters.AddWithValue("IdCliente", IdCliente);
                    if (IdLaboratorio > 0)
                        cmd.Parameters.AddWithValue("IdLaboratorio", IdLaboratorio);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(res);

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje(ex.Message);
            }
            return res;
        }

        /// <summary>
        /// Metodo para obtener todas las ventas de un cliente
        /// </summary>
        /// <returns></returns>
        public object ObtenerPendientePorIdCliente(int IdCliente, ref double total)
        {
            var registros = (from v in context.Venta
                             join c in context.Cliente on v.IdCliente equals c.IdCliente
                             join u in context.Usuario on v.IdUsuario equals u.IdUsuario
                             join d in context.Doctor on v.IdDoctor equals d.IdDoctor into DoctorSet
                             from DS in DoctorSet.DefaultIfEmpty()
                             where v.IdCliente == IdCliente && v.Estatus == "P" && v.Estatus != EST_COTIZACION
                             select new
                             {
                                 Id = v.IdVenta,
                                 Fecha = v.FechaCreacion,
                                 Maquina = v.NumMaquina,
                                 Usuario = u.Nombre,
                                 Cliente = c.Nombre,
                                 Doctor = v.IdDoctor != null ? DS.Nombre : "",
                                 TotalVenta = v.Total,
                                 Descuento = v.Descuento,
                                 Estatus = "PENDIENTE",
                                 Resto = v.Total - context.Pago.Where(p => p.IdVenta == v.IdVenta).Sum(p => p.Cantidad),
                                 Folio = v.Folio,
                                 Observaciones = v.Observaciones
                                 //Seleccionar = false
                             });

            total = registros.Count() > 0 ? Convert.ToDouble(registros.Sum(r => r.Resto).Value) : 0;
            return registros;
        }

        /// <summary>
        /// Metodo para obtener todas las ventas de un cliente
        /// </summary>
        /// <returns></returns>
        public object ObtenerPorIdCliente(int IdCliente)
        {
            var registros = (from v in context.Venta
                             join c in context.Cliente on v.IdCliente equals c.IdCliente
                             join u in context.Usuario on v.IdUsuario equals u.IdUsuario
                             join d in context.Doctor on v.IdDoctor equals d.IdDoctor into DoctorSet
                             from DS in DoctorSet.DefaultIfEmpty()
                             where v.IdCliente == IdCliente && v.Estatus != EST_COTIZACION
                             select new
                             {
                                 Id = v.IdVenta,
                                 Fecha = v.FechaCreacion,
                                 Maquina = v.NumMaquina,
                                 Usuario = u.Nombre,
                                 Cliente = c.Nombre,
                                 Doctor = v.IdDoctor != null ? DS.Nombre : "",
                                 TotalVenta = v.Total,
                                 Descuento = v.Descuento,
                                 Estatus = "P",
                                 Resto = v.Total - context.Pago.Where(p => p.IdVenta == v.IdVenta).Sum(p => p.Cantidad),
                                 Folio = v.Folio,
                                 Observaciones = v.Observaciones
                                 //Seleccionar = false
                             });

            return registros;
        }

        /// <summary>
        /// Metodo para obtener todas las ventas por corte de turno.
        /// </summary>
        /// <returns></returns>
        public DataTable ObtenerParaDevolucion(int IdCliente, int IdDoctor, DateTime fechaInicio, DateTime fechaFinal)
        {
            DataTable tabla = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_cadenaconn))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("ventaxidclientexiddoctorxfechas", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    if (IdCliente > 0) { command.Parameters.AddWithValue("IDCLIENTE", IdCliente); }
                    if (IdDoctor > 0) { command.Parameters.AddWithValue("IDDOCTOR", IdDoctor); }
                    if (fechaInicio > DateTime.MinValue && fechaFinal > DateTime.MinValue)
                    {
                        command.Parameters.AddWithValue("FECHAINICIO", fechaInicio.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("FECHAFIN", fechaFinal.ToString("yyyy-MM-dd"));
                    }


                    using (SqlDataAdapter adaptador = new SqlDataAdapter(command))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            adaptador.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                tabla = ds.Tables[0];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, "VentaNeg", "ObtenerParaDevolucion", "EMPLEADO");
            }

            return tabla;
        }

        public DataTable ObtenerPorFolio(string folio)
        {
            DataTable res = new DataTable();
            try
            {
                string query = "SELECT Estatus = CASE WHEN V.Estatus = 'C' THEN 'CANCELADA' " +
                                                 "WHEN V.Estatus = 'V' THEN 'VIGENTE'" +
                                                 "ELSE 'COTIZACION' END" +
                                       ",Format(V.FechaCreacion,'dd/MMMM/yy') AS FechaCreacion," +
                                       "V.IdVenta, V.Folio,C.Nombre" +
                                       ",Estudios = REPLACE(REPLACE(STUFF((SELECT ',', L.Descripcion " +
                                                                            "FROM LabPorVenta LXV " +
                                                                            "INNER JOIN Laboratorio L ON LXV.IdLaboratorio = L.IdLaboratorio " +
                                                                            "WHERE LXV.IdVenta = V.IdVenta " +
                                                                            "FOR XML PATH('')" +
                                                                            "), 1, 1, ''), '<Descripcion>', ''),'</Descripcion>','')" +
                                        ",V.Total " +
                                "FROM Venta V " +
                                "INNER JOIN Cliente C ON V.IdCliente = C.IdCliente " +
                                "WHERE V.Folio = @FOLIO";

                using (SqlConnection conn = new SqlConnection(_cadenaconn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("FOLIO", folio);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(res);

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje(ex.Message);
            }
            return res;
        }
        #endregion
        #region CANCELAR VENTA

        public bool Cancelar(int IdVenta, bool roladministrador, string nuevoestatus, ref bool cancelaadministrador, ref string msjError)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_cadenaconn))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("cancelarnota", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDVENTA", IdVenta);
                    command.Parameters.AddWithValue("@NUEVOESTATUS", nuevoestatus);
                    command.Parameters.AddWithValue("@ADMINISTRADOR", roladministrador);

                    string msj = command.ExecuteScalar().ToString();
                    if (msj != "0")
                    {
                        if (msj == "ADMINISTRADOR")
                        {
                            msjError = nuevoestatus == "C" ? "cancelada\n" : "marcada \ncomo vigente";
                            msjError = "La nota de venta solo puede ser  " + msjError + " por el administrador.";
                            cancelaadministrador = true;
                        }
                        else if (msj == "PAGO")
                            msjError = "La nota de venta no puede cambiar de estatus porque existe mas de un pago.";
                        else msjError = msj;
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, "VentaNeg", "Cancelar", roladministrador ? "ADMINISTRADOR" : "EMPLEADO");
                return false;
            }
            return true;
        }

        //public bool Cancelar(int IdVenta,bool roladministrador, ref bool cancelaadministrador, ref string msjError)
        //{
        //    SqlTransaction txn = null;
        //    try
        //    {
        //        Venta venta = context.Venta.Where(v => v.IdVenta == IdVenta && v.Estatus != EST_COTIZACION).FirstOrDefault();
        //        if (venta == null) return false;

        //        /* Validar que la nota de venta pertenezca al actual corte de turno */
        //        CorteTurno corteTurno = (from c in context.CorteTurno
        //                                 where c.FechaFin == null
        //                                 orderby c.IdCorteTurno descending
        //                                 select c).FirstOrDefault();

        //        if (venta.IdCorteTurno != corteTurno.IdCorteTurno)
        //        {
        //            cancelaadministrador = true;
        //            return false;
        //        }

        //        //Si la venta tiene mas de un pago no se podrá cancelar
        //        List<Pago> lPagos = context.Pago.Where(p => p.IdVenta == IdVenta).ToList();
        //        if (lPagos.Count > 1)
        //        {
        //            msjError = "La nota de venta no se puede cancelar porque tiene mas de un pago.";
        //            return false;
        //        }

        //        venta.Estatus = "C";

        //        using (SqlConnection conn = new SqlConnection(_cadenaconn))
        //        {
        //            conn.Open();
        //            txn = conn.BeginTransaction();

        //            SqlCommand cmdVenta = new SqlCommand("UPDATE Venta SET Estatus = @Estatus, FechaAct =@FechaAct WHERE IdVenta = @IdVenta", conn, txn);
        //            cmdVenta.Parameters.Clear();
        //            cmdVenta.Parameters.AddWithValue("Estatus", venta.Estatus);
        //            cmdVenta.Parameters.AddWithValue("FechaAct", DateTime.Now);
        //            cmdVenta.Parameters.AddWithValue("IdVenta", venta.IdVenta);
        //            cmdVenta.ExecuteNonQuery();

        //            //Cancelar pago
        //            if (lPagos.Count > 0)
        //            {
        //                SqlCommand cmdPago = new SqlCommand("UPDATE Pago SET Cancelado = @Cancelado, FechaAct =@FechaAct WHERE IdPago = @IdPago", conn, txn);
        //                cmdPago.Parameters.Clear();
        //                cmdPago.Parameters.AddWithValue("Cancelado", true);
        //                cmdPago.Parameters.AddWithValue("FechaAct", DateTime.Now);
        //                cmdPago.Parameters.AddWithValue("IdPago", lPagos[0].IdPago);
        //                cmdPago.ExecuteNonQuery();
        //            }

        //            txn.Commit();
        //            txn.Dispose();

        //            conn.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msjError = ex.Message;
        //        return false;
        //    }

        //    return true;
        //}
        #endregion
        public double ObtenerSigFolioTarjeta()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_cadenaconn))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("getsigfoliotarjeta", conn);
                    command.CommandType = CommandType.StoredProcedure;

                    string folio = command.ExecuteScalar().ToString();

                    return Convert.ToDouble(folio);
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, "VentaNeg", "ObtenerSigFolioTarjeta", "EMPLEADO");
                return -1;
            }
        }
        #region Actualizar
        public void ActualizarFolioTarjeta(string notasxml)
        {
            using (SqlConnection conn = new SqlConnection(_cadenaconn))
            {
                conn.Open();

                SqlCommand command = new SqlCommand("VentaActualizarFolioTarjeta", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("NotasXML", notasxml);
                command.ExecuteNonQuery();
            }
        }
        #endregion
        #region Pagar ventas
        public bool Pagar(List<Pago> lPagos, int IdVtaPagoParcial, ref string msjError)
        {
            SqlTransaction txn = null;
            CorteTurnoNeg corteTurnoNeg = new CorteTurnoNeg(_cadenaconn);
            try
            {
                using (SqlConnection conn = new SqlConnection(_cadenaconn))
                {
                    conn.Open();
                    txn = conn.BeginTransaction();

                    foreach (Pago p in lPagos)
                    {
                        CorteTurno corteTurno = corteTurnoNeg.ObtenerSinFechaFin();
                        if (corteTurno != null)
                            p.IdCorteTurno = corteTurno.IdCorteTurno;
                        else
                        {
                            p.IdCorteTurno = corteTurnoNeg.Agregar();
                            Utilerias.RegistrarLogError(new Exception("No es una excepción. *SE AGREGO CORTE DE TURNO AL AGREGAR PAGO DE VENTAS. Se agrego el corte de turno porque no existía otro sin fecha de fin. IdCorteTurno = " + p.IdCorteTurno.ToString()), "VentaNeg", "Pagar", "");
                        }
                        SqlCommand cmdPago = new SqlCommand("INSERT INTO Pago(IdVenta, IdCtaBancaria, NumPago, TipoPago, Cantidad, Devolucion, FechaCreacion, IdCorteTurno, FechaAct) " +
                                                                        "VALUES(@IdVenta, @IdCtaBancaria, @NumPago, @TipoPago, @Cantidad, @Devolucion, @FechaCreacion, @IdCorteTurno, @FechaAct)", conn, txn);
                        cmdPago.Parameters.Clear();
                        cmdPago.Parameters.AddWithValue("IdVenta", p.IdVenta);
                        if (p.IdCtaBancaria == null) cmdPago.Parameters.AddWithValue("IdCtaBancaria", DBNull.Value);
                        else cmdPago.Parameters.AddWithValue("IdCtaBancaria", p.IdCtaBancaria);
                        cmdPago.Parameters.AddWithValue("NumPago", p.NumPago);
                        cmdPago.Parameters.AddWithValue("TipoPago", p.TipoPago);
                        cmdPago.Parameters.AddWithValue("Cantidad", p.Cantidad);
                        cmdPago.Parameters.AddWithValue("Devolucion", false);
                        cmdPago.Parameters.AddWithValue("FechaCreacion", DateTime.Now);
                        cmdPago.Parameters.AddWithValue("IdCorteTurno", p.IdCorteTurno);
                        cmdPago.Parameters.AddWithValue("FechaAct", DBNull.Value);
                        cmdPago.ExecuteNonQuery();

                        if (p.IdVenta != IdVtaPagoParcial)
                        {
                            SqlCommand cmdVenta = new SqlCommand("UPDATE Venta SET Estatus = 'V', FechaAct = @FechaAct  WHERE IdVenta=@IdVenta", conn, txn);
                            cmdVenta.Parameters.Clear();
                            cmdVenta.Parameters.AddWithValue("IdVenta", p.IdVenta);
                            cmdVenta.Parameters.AddWithValue("FechaAct", DateTime.Now);
                            cmdVenta.ExecuteNonQuery();
                        }
                    }

                    txn.Commit();
                    txn.Dispose();

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                msjError = ex.Message;
                return false;
            }

            return true;
        }
        #endregion
        #region Corregir folio
        public static bool CorregirFolios(int ultimofoliocorrecto)
        {
            bool res = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.ConexionDB()))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("restaurarfolio", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ULTIMOFOLIOCORRECTO", ultimofoliocorrecto);

                    command.ExecuteNonQuery();
                }
                res = true;
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, "VentaNeg", "ObtenerSigFolioTarjeta", "EMPLEADO");
            }
            return res;
        }
        #endregion
        #region PDF
        public byte[] CreatePDF(int IdVenta, string empresa, int numDigFolio)
        {
            try
            {
                if (empresa.ToLower().StartsWith(C.EMPRESA_ANALIZER))
                    return CrearNotaAnalyzer(IdVenta, empresa, numDigFolio);
                else
                {
                    return numDigFolio == -1 ? CrearCotizacion(IdVenta, empresa) : CrearNotaDH(IdVenta, empresa, numDigFolio);
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, "VentaNeg", "CreatePDF", "");
                return null;
            }
        }

        public byte[] CrearNotaAnalyzer(int IdVenta, string empresa, int numDigFolio)
        {
            var vta = (from v in context.Venta
                       join c in context.Cliente on v.IdCliente equals c.IdCliente
                       join d in context.Doctor on v.IdDoctor equals d.IdDoctor into DoctorSet
                       from DS in DoctorSet.DefaultIfEmpty()
                       where v.IdVenta == IdVenta
                       select new
                       {
                           NomCliente = c.Nombre,
                           Folio = v.Folio,
                           FolioTarjeta = v.FolioTarjeta,
                           Fecha = v.FechaCreacion,
                           NomDoctor = v.IdDoctor != null ? DS.Nombre : "",
                           Resto = v.Total - context.Pago.Where(p => p.IdVenta == v.IdVenta).Sum(p => p.Cantidad),
                           MontoPagado = context.Pago.Where(p => p.IdVenta == v.IdVenta).Sum(p => p.Cantidad),
                           Total = v.Total
                       }).FirstOrDefault();

            Document doc = new Document(PageSize.LETTER, -31, 0, MARGENTOP, 0);
            using (MemoryStream msPdf = new MemoryStream())
            {
                //Inicializar el objeto PdfWriter para crear el documento PDF.
                PdfWriter writer = PdfWriter.GetInstance(doc, msPdf);
                //La plantilla de fondo la seleccionamos dependiendo de la marca
                string TemplatePDF = C.PathPlantillasPDF + "notaVenta.pdf";
                //Importar el tipo de letra "Segoe". -- verificar la fuente
                BaseFont sans_serif = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);

                /*************************
                NUEVO MANEJO DE EVENTOS.
                *************************/
                MyEventHandlerVenta funcionEventosPDF = new MyEventHandlerVenta();
                // Establecer en la variable pública el nombre del archivo PDF template.
                funcionEventosPDF.templateFile = TemplatePDF;
                // Indicar que el objeto PdfWriter sea sensible a los eventos de la clase MyEventHandler.
                writer.PageEvent = funcionEventosPDF;
                /******************************
                ' FIN: NUEVO MANEJO DE EVENTOS.*/

                //Fuentes para datos de la nota
                Font fontEtiquetas = new Font(sans_serif, 6, Font.BOLD, BaseColor.BLACK);
                Font fontVariable = new Font(sans_serif, 6, Font.NORMAL, BaseColor.BLACK);

                //Agregar autor del documento pdf.
                doc.AddAuthor(empresa);
                //Agregar creador del documento pdf.
                doc.AddCreator(empresa);
                //Agregar título del documento pdf.
                doc.AddTitle("Nota de venta");
                //Agregar asunto del documento pdf.
                doc.AddSubject("Nota de venta");

                //Abrir el documento PDF para su creación.
                doc.Open();

                //Crear una tabla que contiene los datos de la nota.
                PdfPTable tablaDatosVta = new PdfPTable(5);
                //Crear los porcentajes de las columnas.
                int[] columnastablaDatosVta = { 10, 50, 10, 10, 20 };
                //Agregar los porcentajes a la tabla.
                tablaDatosVta.SetWidths(columnastablaDatosVta);
                //Quitar el borde de las celdas.
                tablaDatosVta.DefaultCell.BorderWidth = 0;

                tablaDatosVta.AddCell(new Phrase("Cliente: ", fontEtiquetas));
                tablaDatosVta.AddCell(new Phrase(vta.NomCliente, fontVariable));
                tablaDatosVta.AddCell(new Phrase("Sexo: ", fontEtiquetas));
                tablaDatosVta.AddCell(new Phrase("FOLIO", fontVariable));//Sexo
                tablaDatosVta.AddCell(new Phrase("FECHA", fontVariable));

                tablaDatosVta.AddCell(new Phrase("Médico: ", fontEtiquetas));
                tablaDatosVta.AddCell(new Phrase(vta.NomDoctor, fontVariable));
                if (vta.FolioTarjeta != null)
                { tablaDatosVta.AddCell(new Phrase("T" + vta.FolioTarjeta.ToString().PadLeft(numDigFolio, '0'), fontVariable)); }
                else { tablaDatosVta.AddCell(new Phrase(vta.Folio.ToString().PadLeft(numDigFolio, '0'), fontVariable)); }
                tablaDatosVta.AddCell(new Phrase(vta.Fecha.ToString(), fontVariable));
                doc.Add(tablaDatosVta);

                /******************************* LABORATORIOS *******************************************/
                //Obtener los laboratorios
                var lLaboratorios = from lv in context.LabPorVenta
                                    join l in context.Laboratorio on lv.IdLaboratorio equals l.IdLaboratorio
                                    where lv.IdVenta == IdVenta
                                    select new
                                    {
                                        Cantidad = lv.NumLaboratorios,
                                        NomLab = l.Descripcion,
                                        Precio = lv.Costo,
                                        Importe = lv.Total
                                    };

                //Crear una tabla que contiene los datos de la nota.
                PdfPTable tablaEncabezado = new PdfPTable(4);
                //Crear los porcentajes de las columnas.
                int[] columnastablaLaboratorios = { 10, 50, 20, 20 };
                //Agregar los porcentajes a la tabla.
                tablaEncabezado.SetWidths(columnastablaLaboratorios);
                //Quitar el borde de las celdas.
                tablaEncabezado.DefaultCell.BorderWidth = 0;
                tablaEncabezado.DefaultCell.BackgroundColor = new BaseColor(System.Drawing.Color.Gray);

                //Encabezados de la tabla
                tablaEncabezado.AddCell(new Phrase("Cantidad ", fontEtiquetas));
                tablaEncabezado.AddCell(new Phrase("Laboratorio", fontEtiquetas));
                tablaEncabezado.AddCell(new Phrase("Precio", fontEtiquetas));//Sexo
                tablaEncabezado.AddCell(new Phrase("Importe", fontEtiquetas));

                doc.Add(tablaEncabezado);

                //Crear una tabla que contiene los datos de la nota.
                PdfPTable tablaLaboratorios = new PdfPTable(4);
                //Crear los porcentajes de las columnas.
                //int[] columnastablaLaboratorios = { 10, 50, 20, 20 };
                //Agregar los porcentajes a la tabla.
                tablaLaboratorios.SetWidths(columnastablaLaboratorios);
                //Quitar el borde de las celdas.
                tablaLaboratorios.DefaultCell.BorderWidth = 0;

                foreach (var l in lLaboratorios)
                {
                    tablaLaboratorios.AddCell(new Phrase(l.Cantidad.ToString(), fontVariable));
                    tablaLaboratorios.AddCell(new Phrase(l.NomLab, fontVariable));
                    tablaLaboratorios.AddCell(new Phrase(l.Precio.Value.ToString("#.##"), fontVariable));//Sexo
                    tablaLaboratorios.AddCell(new Phrase(l.Importe.Value.ToString("#.##"), fontVariable));
                }

                doc.Add(tablaLaboratorios);

                /******************************* TOTALES *******************************************/
                //Crear una tabla que contiene los datos de la nota.
                PdfPTable tablaTotales = new PdfPTable(3);
                //Crear los porcentajes de las columnas.
                int[] columnastablaTotales = { 60, 20, 20 };
                //Agregar los porcentajes a la tabla.
                tablaTotales.SetWidths(columnastablaTotales);
                //Quitar el borde de las celdas.
                tablaTotales.DefaultCell.BorderWidth = 0;

                string[] sTotales = new string[] { "TOTAL" };

                foreach (string item in sTotales)
                {
                    //Total
                    tablaTotales.AddCell(new Phrase("", fontEtiquetas));
                    Paragraph parLeyenada = new Paragraph(item, fontEtiquetas);
                    parLeyenada.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cellsLeyenda = new PdfPCell();
                    cellsLeyenda.AddElement(parLeyenada);
                    cellsLeyenda.Border = Rectangle.NO_BORDER;
                    tablaTotales.AddCell(cellsLeyenda);
                    //tablaTotales.AddCell(new Phrase("TOTAL", fontEtiquetas));
                    string valor = "";
                    switch (item)
                    {
                        case "TOTAL":
                            valor = vta.Total.Value.ToString("C2");
                            break;
                        case "MONTO PAGADO":
                            valor = vta.MontoPagado.Value.ToString("C2");
                            break;
                        case "SALDO":
                            valor = vta.Resto.Value.ToString("C2");
                            break;
                    }

                    Paragraph parTotal = new Paragraph(valor, fontVariable);
                    parTotal.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cellTotal = new PdfPCell();
                    cellTotal.AddElement(parTotal);
                    cellTotal.Border = Rectangle.NO_BORDER;
                    cellTotal.PaddingRight = 30;
                    tablaTotales.AddCell(cellTotal);
                }

                /************************************************************************************
                    FINALIZAR EL DOCUMENTO.
                ***********************************************************************************/
                //Cerrar y crear el documento final PDF.
                doc.Close();
                return msPdf.ToArray();
            }
        }
        public byte[] CrearNotaDH(int IdVenta, string empresa, int numDigFolio)
        {
            var vta = (from v in context.Venta
                       join c in context.Cliente on v.IdCliente equals c.IdCliente
                       join u in context.Usuario on v.IdUsuario equals u.IdUsuario
                       join d in context.Doctor on v.IdDoctor equals d.IdDoctor into DoctorSet
                       from DS in DoctorSet.DefaultIfEmpty()
                       where v.IdVenta == IdVenta
                       select new
                       {
                           NomCliente = c.Nombre,
                           NomDoctor = v.IdDoctor != null ? DS.Nombre : "",
                           NomUsuario = u.Nombre,
                           Folio = v.Folio,
                           FolioTarjeta = v.FolioTarjeta,
                           Fecha = v.FechaCreacion,
                           Resto = v.Total - context.Pago.Where(p => p.IdVenta == v.IdVenta).Sum(p => p.Cantidad),
                           MontoPagado = context.Pago.Where(p => p.IdVenta == v.IdVenta).Sum(p => p.Cantidad),
                           Descuento = v.Descuento,
                           Sexo = c.Sexo != null ? c.Sexo == C.HOMBRE ? "HOMBRE" : "MUJER" : "",
                           Anios = c.Anios,
                           Meses = c.Meses,
                           Dias = c.Dias,
                           Telefono = c.Telefono,
                           Observaciones = v.Observaciones
                       }).FirstOrDefault();
            // "Están son las observaciones" 
            Document doc = new Document(PageSize.LETTER, -17, -13, MARGENTOP_DH, 0);
            using (MemoryStream msPdf = new MemoryStream())
            {
                //Inicializar el objeto PdfWriter para crear el documento PDF.
                PdfWriter writer = PdfWriter.GetInstance(doc, msPdf);
                //La plantilla de fondo la seleccionamos dependiendo de la marca
                string TemplatePDF = C.PathPlantillasPDF + "dh_nota.pdf";
                //Importar el tipo de letra "Segoe". -- verificar la fuente
                BaseFont sans_serif = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);

                /*************************
                NUEVO MANEJO DE EVENTOS.
                *************************/
                MyEventHandlerVenta funcionEventosPDF = new MyEventHandlerVenta();
                // Establecer en la variable pública el nombre del archivo PDF template.
                funcionEventosPDF.templateFile = TemplatePDF;
                // Indicar que el objeto PdfWriter sea sensible a los eventos de la clase MyEventHandler.
                writer.PageEvent = funcionEventosPDF;
                /******************************
                ' FIN: NUEVO MANEJO DE EVENTOS.*/

                //Fuentes para datos de la nota
                Font fontEtiquetas = new Font(sans_serif, 9, Font.BOLD, BaseColor.BLACK);
                Font fontVariable = new Font(sans_serif, 9, Font.NORMAL, BaseColor.BLACK);
                Font fontObservaciones = new Font(sans_serif, 9, Font.NORMAL, BaseColor.BLACK);

                //Agregar autor del documento pdf.
                doc.AddAuthor(empresa);
                //Agregar creador del documento pdf.
                doc.AddCreator(empresa);
                //Agregar título del documento pdf.
                doc.AddTitle("Nota de venta");
                //Agregar asunto del documento pdf.
                doc.AddSubject("Nota de venta");

                //Abrir el documento PDF para su creación.
                doc.Open();

                //Crear una tabla que contiene los datos de la nota.
                PdfPTable tablaDatosVta = new PdfPTable(6);
                //Crear los porcentajes de las columnas.
                int[] columnastablaDatosVta = { 10, 37, 8, 15, 8, 22 };
                //Agregar los porcentajes a la tabla.
                tablaDatosVta.SetWidths(columnastablaDatosVta);
                //Quitar el borde de las celdas.
                tablaDatosVta.DefaultCell.BorderWidth = 0;

                tablaDatosVta.AddCell(new Phrase("Paciente: ", fontEtiquetas));
                tablaDatosVta.AddCell(new Phrase(vta.NomCliente, fontVariable));
                /* Sexo ALINEADO AL CENTRO */
                tablaDatosVta.AddCell(new Phrase("Sexo: ", fontEtiquetas));
                tablaDatosVta.AddCell(new Phrase(vta.Sexo, fontVariable));
                /* FOLIO ALINEADO AL CENTRO */
                Paragraph parFolio = new Paragraph("FOLIO", fontEtiquetas);
                parFolio.Alignment = Element.ALIGN_CENTER;
                PdfPCell cellFolio = new PdfPCell();
                cellFolio.AddElement(parFolio);
                cellFolio.Border = Rectangle.NO_BORDER;
                cellFolio.PaddingTop = PADDINGTOP_CELL;
                tablaDatosVta.AddCell(cellFolio);
                /* FECHA ALINEADA AL CENTRO */
                Paragraph parFecha = new Paragraph("FECHA", fontEtiquetas);
                parFecha.Alignment = Element.ALIGN_CENTER;
                PdfPCell cellFecha = new PdfPCell();
                cellFecha.Border = Rectangle.NO_BORDER;
                cellFecha.PaddingTop = PADDINGTOP_CELL;
                cellFecha.AddElement(parFecha);
                tablaDatosVta.AddCell(cellFecha);

                if (string.IsNullOrEmpty(vta.NomDoctor))
                {
                    tablaDatosVta.AddCell("");
                    tablaDatosVta.AddCell("");
                }
                else
                {
                    tablaDatosVta.AddCell(new Phrase("Médico: ", fontEtiquetas));
                    tablaDatosVta.AddCell(new Phrase(vta.NomDoctor, fontVariable));
                }

                tablaDatosVta.AddCell(new Phrase("Edad:", fontEtiquetas));
                if (vta.Anios.HasValue && vta.Anios.Value > 0)
                {
                    tablaDatosVta.AddCell(new Phrase(vta.Anios.ToString() + " años", fontVariable));
                }
                else if ((vta.Meses.HasValue && vta.Meses.Value > 0) || (vta.Dias.HasValue && vta.Dias.Value > 0))
                {
                    string mesesDias = "";
                    if (vta.Meses.HasValue && vta.Meses.Value > 0)
                    {
                        mesesDias = vta.Meses.ToString() + " meses";
                        if (vta.Dias.HasValue && vta.Dias.Value > 0) mesesDias = mesesDias + " y " + vta.Meses.ToString() + " días.";
                        else mesesDias = mesesDias + ".";
                    }
                    else mesesDias = vta.Dias.ToString() + " días.";

                    tablaDatosVta.AddCell(new Phrase(mesesDias, fontVariable));
                }
                else tablaDatosVta.AddCell(new Phrase("", fontEtiquetas));

                Paragraph parFolioV = new Paragraph(vta.FolioTarjeta != null ? vta.FolioTarjeta.ToString().PadLeft(numDigFolio, '0') : vta.Folio.ToString().PadLeft(numDigFolio, '0'), fontVariable);
                parFolioV.Alignment = Element.ALIGN_CENTER;
                PdfPCell cellFolioV = new PdfPCell();
                cellFolioV.Border = Rectangle.NO_BORDER;
                cellFolioV.PaddingTop = PADDINGTOP_CELL;
                cellFolioV.AddElement(parFolioV);
                tablaDatosVta.AddCell(cellFolioV);
                Paragraph parFechaV = new Paragraph(vta.Fecha.ToString(), fontVariable);
                parFechaV.Alignment = Element.ALIGN_CENTER;
                PdfPCell cellFechaV = new PdfPCell();
                cellFechaV.Border = Rectangle.NO_BORDER;
                cellFechaV.PaddingTop = PADDINGTOP_CELL;
                cellFechaV.AddElement(parFechaV);
                tablaDatosVta.AddCell(cellFechaV);

                tablaDatosVta.AddCell(new Phrase("Atendió:", fontEtiquetas));
                tablaDatosVta.AddCell(new Phrase(vta.NomUsuario, fontVariable));

                if (string.IsNullOrEmpty(vta.Telefono))
                {
                    tablaDatosVta.AddCell(new Phrase("", fontEtiquetas));
                    tablaDatosVta.AddCell(new Phrase("", fontVariable));
                }
                else
                {
                    tablaDatosVta.AddCell(new Phrase("Teléfono:", fontEtiquetas));
                    tablaDatosVta.AddCell(new Phrase(vta.Telefono, fontVariable));
                }
                tablaDatosVta.AddCell(new Phrase("", fontEtiquetas));
                tablaDatosVta.AddCell(new Phrase("", fontEtiquetas));

                /******************************* LABORATORIOS *******************************************/
                //Obtener los laboratorios
                var lLaboratorios = from lv in context.LabPorVenta
                                    join l in context.Laboratorio on lv.IdLaboratorio equals l.IdLaboratorio
                                    where lv.IdVenta == IdVenta
                                    select new
                                    {
                                        Cantidad = lv.NumLaboratorios,
                                        NomLab = l.Descripcion,
                                        Precio = lv.Costo,
                                        Importe = lv.Total
                                    };

                /*Crear una tabla que contiene los datos de la nota.*/
                //Crear los porcentajes de las columnas.
                int[] columnastablaLaboratorios = { 13, 61, 13, 13 };

                //Crear una tabla que contiene los datos de la nota.
                PdfPTable tablaLaboratorios = new PdfPTable(4);
                //Agregar los porcentajes a la tabla.
                tablaLaboratorios.SetWidths(columnastablaLaboratorios);
                //Quitar el borde de las celdas.
                tablaLaboratorios.DefaultCell.BorderWidth = 0;
                //tablaLaboratorios.TotalWidth = 800f;

                tablaLaboratorios.AddCell(new Phrase("Cantidad ", fontEtiquetas));
                tablaLaboratorios.AddCell(new Phrase("Laboratorio", fontEtiquetas));
                tablaLaboratorios.AddCell(new Phrase("Precio", fontEtiquetas));
                tablaLaboratorios.AddCell(new Phrase("Importe", fontEtiquetas));

                foreach (var l in lLaboratorios)
                {
                    Paragraph parCantLab = new Paragraph(l.Cantidad.ToString(), fontVariable);
                    parCantLab.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cellCantLab = new PdfPCell();
                    cellCantLab.AddElement(parCantLab);
                    cellCantLab.Border = Rectangle.NO_BORDER;
                    cellCantLab.PaddingTop = PADDINGTOP_CELL;
                    tablaLaboratorios.AddCell(cellCantLab);
                    tablaLaboratorios.AddCell(new Phrase(l.NomLab, fontVariable));
                    Paragraph parPrecioLab = new Paragraph(l.Precio.Value.ToString("C2"), fontVariable);
                    parPrecioLab.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cellPrecioLab = new PdfPCell();
                    cellPrecioLab.AddElement(parPrecioLab);
                    cellPrecioLab.Border = Rectangle.NO_BORDER;
                    cellPrecioLab.PaddingTop = PADDINGTOP_CELL;
                    tablaLaboratorios.AddCell(cellPrecioLab);
                    Paragraph parImporteLab = new Paragraph(l.Importe.Value.ToString("C2"), fontVariable);
                    parImporteLab.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cellImporteLab = new PdfPCell();
                    cellImporteLab.AddElement(parImporteLab);
                    cellImporteLab.Border = Rectangle.NO_BORDER;
                    cellImporteLab.PaddingTop = PADDINGTOP_CELL;
                    tablaLaboratorios.AddCell(cellImporteLab);
                }

                /******************************* TOTALES *******************************************/
                PdfPTable tablaTotales = CrearTablaTotales(vta.Observaciones, vta.MontoPagado.HasValue ? vta.MontoPagado.Value : 0, vta.Descuento.HasValue ? vta.Descuento.Value : 0, fontEtiquetas, fontVariable, fontObservaciones);

                /************** Crear la tabla principal, donde se van a acomodar las demás **************/
                PdfPTable tableDatos = new PdfPTable(1);
                //Quitar el padding.
                tableDatos.DefaultCell.Padding = 0;
                //Quitar el borde.
                tableDatos.DefaultCell.BorderWidth = 0;
                tableDatos.AddCell(tablaDatosVta);
                PdfPCell cellLab = new PdfPCell();
                cellLab.FixedHeight = vta.Descuento.HasValue && vta.Descuento > 0 ? 180f : vta.Observaciones != null ? 190f : 200f;
                cellLab.BorderWidth = -100;
                cellLab.PaddingLeft = -40;
                cellLab.AddElement(tablaLaboratorios);
                tableDatos.AddCell(cellLab);
                tableDatos.AddCell(tablaTotales);

                doc.Add(tableDatos);

                /************************************************************************************
                    FINALIZAR EL DOCUMENTO.
                ***********************************************************************************/
                //Cerrar y crear el documento final PDF.
                doc.Close();
                return msPdf.ToArray();
            }
        }
        public byte[] CrearCotizacion(int IdVenta, string empresa)
        {
            var vta = (from v in context.Venta
                       join c in context.Cliente on v.IdCliente equals c.IdCliente
                       join u in context.Usuario on v.IdUsuario equals u.IdUsuario
                       join d in context.Doctor on v.IdDoctor equals d.IdDoctor into DoctorSet
                       from DS in DoctorSet.DefaultIfEmpty()
                       where v.IdVenta == IdVenta
                       select new
                       {
                           NomCliente = c.Nombre,
                           NomDoctor = v.IdDoctor != null ? DS.Nombre : "",
                           NomUsuario = u.Nombre,
                           Fecha = v.FechaCreacion,
                           Total = v.Total,
                           Sexo = c.Sexo != null ? c.Sexo == C.HOMBRE ? "HOMBRE" : "MUJER" : "",
                           Anios = c.Anios,
                           Meses = c.Meses,
                           Dias = c.Dias,
                           Telefono = c.Telefono,
                           Observaciones = v.Observaciones
                       }).FirstOrDefault();

            Document doc = new Document(PageSize.LETTER, -17, -13, MARGENTOP_DH, 0);
            using (MemoryStream msPdf = new MemoryStream())
            {
                //Inicializar el objeto PdfWriter para crear el documento PDF.
                PdfWriter writer = PdfWriter.GetInstance(doc, msPdf);
                //La plantilla de fondo la seleccionamos dependiendo de la marca
                string TemplatePDF = C.PathPlantillasPDF + "dh_cotizacion.pdf";
                //Importar el tipo de letra "Segoe". -- verificar la fuente
                BaseFont sans_serif = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);

                /*************************
                NUEVO MANEJO DE EVENTOS.
                *************************/
                MyEventHandlerVenta funcionEventosPDF = new MyEventHandlerVenta();
                // Establecer en la variable pública el nombre del archivo PDF template.
                funcionEventosPDF.templateFile = TemplatePDF;
                // Indicar que el objeto PdfWriter sea sensible a los eventos de la clase MyEventHandler.
                writer.PageEvent = funcionEventosPDF;
                /******************************
                ' FIN: NUEVO MANEJO DE EVENTOS.*/

                //Fuentes para datos de la nota
                Font fontEtiquetas = new Font(sans_serif, 9, Font.BOLD, BaseColor.BLACK);
                Font fontVariable = new Font(sans_serif, 9, Font.NORMAL, BaseColor.BLACK);
                Font fontObservaciones = new Font(sans_serif, 9, Font.NORMAL, BaseColor.BLACK);

                //Agregar autor del documento pdf.
                doc.AddAuthor(empresa);
                //Agregar creador del documento pdf.
                doc.AddCreator(empresa);
                //Agregar título del documento pdf.
                doc.AddTitle("Cotización");
                //Agregar asunto del documento pdf.
                doc.AddSubject("Cotización");

                //Abrir el documento PDF para su creación.
                doc.Open();

                //Crear una tabla que contiene los datos de la nota.
                PdfPTable tablaDatosVta = new PdfPTable(6);
                //Crear los porcentajes de las columnas.
                int[] columnastablaDatosVta = { 10, 37, 8, 15, 8, 22 };
                //Agregar los porcentajes a la tabla.
                tablaDatosVta.SetWidths(columnastablaDatosVta);
                //Quitar el borde de las celdas.
                tablaDatosVta.DefaultCell.BorderWidth = 0;

                tablaDatosVta.AddCell(new Phrase("Paciente: ", fontEtiquetas));
                tablaDatosVta.AddCell(new Phrase(vta.NomCliente, fontVariable));
                /* Sexo ALINEADO AL CENTRO */
                tablaDatosVta.AddCell(new Phrase("Sexo: ", fontEtiquetas));
                tablaDatosVta.AddCell(new Phrase(vta.Sexo, fontVariable));
                Paragraph parFolio = new Paragraph("FOLIO", fontEtiquetas);
                parFolio.Alignment = Element.ALIGN_CENTER;
                PdfPCell cellFolio = new PdfPCell();
                cellFolio.AddElement(parFolio);
                cellFolio.Border = Rectangle.NO_BORDER;
                cellFolio.PaddingTop = PADDINGTOP_CELL;
                tablaDatosVta.AddCell(cellFolio);
                /* FECHA ALINEADA AL CENTRO */
                Paragraph parFecha = new Paragraph("FECHA", fontEtiquetas);
                parFecha.Alignment = Element.ALIGN_CENTER;
                PdfPCell cellFecha = new PdfPCell();
                cellFecha.Border = Rectangle.NO_BORDER;
                cellFecha.PaddingTop = PADDINGTOP_CELL;
                cellFecha.AddElement(parFecha);
                tablaDatosVta.AddCell(cellFecha);

                if (string.IsNullOrEmpty(vta.NomDoctor))
                {
                    tablaDatosVta.AddCell("");
                    tablaDatosVta.AddCell("");
                }
                else
                {
                    tablaDatosVta.AddCell(new Phrase("Médico: ", fontEtiquetas));
                    tablaDatosVta.AddCell(new Phrase(vta.NomDoctor, fontVariable));
                }


                tablaDatosVta.AddCell(new Phrase("Edad:", fontEtiquetas));
                if (vta.Anios.HasValue && vta.Anios.Value > 0)
                {
                    tablaDatosVta.AddCell(new Phrase(vta.Anios.ToString() + " años", fontVariable));
                }
                else if ((vta.Meses.HasValue && vta.Meses.Value > 0) || (vta.Dias.HasValue && vta.Dias.Value > 0))
                {
                    string mesesDias = "";
                    if (vta.Meses.HasValue && vta.Meses.Value > 0)
                    {
                        mesesDias = vta.Meses.ToString() + " meses";
                        if (vta.Dias.HasValue && vta.Dias.Value > 0) mesesDias = mesesDias + " y " + vta.Meses.ToString() + " días.";
                        else mesesDias = mesesDias + ".";
                    }
                    else mesesDias = vta.Dias.ToString() + " días.";

                    tablaDatosVta.AddCell(new Phrase(mesesDias, fontVariable));
                }
                else tablaDatosVta.AddCell(new Phrase("", fontEtiquetas));


                Paragraph parFolioV = new Paragraph("", fontVariable);
                parFolioV.Alignment = Element.ALIGN_CENTER;
                PdfPCell cellFolioV = new PdfPCell();
                cellFolioV.Border = Rectangle.NO_BORDER;
                cellFolioV.PaddingTop = PADDINGTOP_CELL;
                cellFolioV.AddElement(parFolioV);
                tablaDatosVta.AddCell(cellFolioV);
                Paragraph parFechaV = new Paragraph(vta.Fecha.ToString(), fontVariable);
                parFechaV.Alignment = Element.ALIGN_CENTER;
                PdfPCell cellFechaV = new PdfPCell();
                cellFechaV.Border = Rectangle.NO_BORDER;
                cellFechaV.PaddingTop = PADDINGTOP_CELL;
                cellFechaV.AddElement(parFechaV);
                tablaDatosVta.AddCell(cellFechaV);

                tablaDatosVta.AddCell(new Phrase("Atendió:", fontEtiquetas));
                tablaDatosVta.AddCell(new Phrase(vta.NomUsuario, fontVariable));

                if (string.IsNullOrEmpty(vta.Telefono))
                {
                    tablaDatosVta.AddCell(new Phrase("", fontEtiquetas));
                    tablaDatosVta.AddCell(new Phrase("", fontVariable));
                }
                else
                {
                    tablaDatosVta.AddCell(new Phrase("Teléfono:", fontEtiquetas));
                    tablaDatosVta.AddCell(new Phrase(vta.Telefono, fontVariable));
                }
                tablaDatosVta.AddCell(new Phrase("", fontEtiquetas));
                tablaDatosVta.AddCell(new Phrase("", fontEtiquetas));

                /******************************* LABORATORIOS *******************************************/
                //Obtener los laboratorios
                var lLaboratorios = from lv in context.LabPorVenta
                                    join l in context.Laboratorio on lv.IdLaboratorio equals l.IdLaboratorio
                                    where lv.IdVenta == IdVenta
                                    select new
                                    {
                                        Cantidad = lv.NumLaboratorios,
                                        NomLab = l.Descripcion,
                                        Precio = lv.Costo,
                                        Importe = lv.Total
                                    };

                //Crear una tabla que contiene los datos de la nota.                
                //Crear los porcentajes de las columnas.
                int[] columnastablaLaboratorios = { 13, 61, 13, 13 };

                //Crear una tabla que contiene los datos de la nota.
                PdfPTable tablaLaboratorios = new PdfPTable(4);
                //Agregar los porcentajes a la tabla.
                tablaLaboratorios.SetWidths(columnastablaLaboratorios);
                //Quitar el borde de las celdas.
                tablaLaboratorios.DefaultCell.BorderWidth = 0;
                //tablaLaboratorios.TotalWidth = 800f;

                tablaLaboratorios.AddCell(new Phrase("Cantidad ", fontEtiquetas));
                tablaLaboratorios.AddCell(new Phrase("Laboratorio", fontEtiquetas));
                tablaLaboratorios.AddCell(new Phrase("Precio", fontEtiquetas));
                tablaLaboratorios.AddCell(new Phrase("Importe", fontEtiquetas));

                foreach (var l in lLaboratorios)
                {
                    Paragraph parCantLab = new Paragraph(l.Cantidad.ToString(), fontVariable);
                    parCantLab.Alignment = Element.ALIGN_CENTER;
                    PdfPCell cellCantLab = new PdfPCell();
                    cellCantLab.AddElement(parCantLab);
                    cellCantLab.Border = Rectangle.NO_BORDER;
                    cellCantLab.PaddingTop = PADDINGTOP_CELL;
                    tablaLaboratorios.AddCell(cellCantLab);
                    tablaLaboratorios.AddCell(new Phrase(l.NomLab, fontVariable));
                    Paragraph parPrecioLab = new Paragraph(l.Precio.Value.ToString("C2"), fontVariable);
                    parPrecioLab.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cellPrecioLab = new PdfPCell();
                    cellPrecioLab.AddElement(parPrecioLab);
                    cellPrecioLab.Border = Rectangle.NO_BORDER;
                    cellPrecioLab.PaddingTop = PADDINGTOP_CELL;
                    tablaLaboratorios.AddCell(cellPrecioLab);
                    Paragraph parImporteLab = new Paragraph(l.Importe.Value.ToString("C2"), fontVariable);
                    parImporteLab.Alignment = Element.ALIGN_RIGHT;
                    PdfPCell cellImporteLab = new PdfPCell();
                    cellImporteLab.AddElement(parImporteLab);
                    cellImporteLab.Border = Rectangle.NO_BORDER;
                    cellImporteLab.PaddingTop = PADDINGTOP_CELL;
                    tablaLaboratorios.AddCell(cellImporteLab);
                }

                /******************************* TOTALES *******************************************/
                ////Crear una tabla que contiene los datos de la nota.
                //PdfPTable tablaTotales = new PdfPTable(5);
                ////Crear los porcentajes de las columnas.
                //int[] columnastablaTotales = { 14, 56, 10, 10, 10 };
                ////Agregar los porcentajes a la tabla.
                //tablaTotales.SetWidths(columnastablaTotales);
                ////Quitar el borde de las celdas.
                //tablaTotales.DefaultCell.BorderWidth = 0;

                //if (string.IsNullOrEmpty(vta.Observaciones))
                //{
                //    tablaTotales.AddCell(new Phrase("", fontEtiquetas));
                //    tablaTotales.AddCell(new Phrase("", fontVariable));
                //}
                //else
                //{
                //    tablaTotales.AddCell(new Phrase("Observaciones:", fontEtiquetas));
                //    tablaTotales.AddCell(new Phrase(vta.Observaciones, fontVariable));
                //}
                //tablaTotales.AddCell(new Phrase("", fontVariable));
                ///*Total*/
                //Paragraph parLeyenada = new Paragraph("TOTAL", fontEtiquetas);
                //parLeyenada.Alignment = Element.ALIGN_RIGHT;
                //PdfPCell cellsLeyenda = new PdfPCell();
                //cellsLeyenda.AddElement(parLeyenada);
                //cellsLeyenda.Border = Rectangle.NO_BORDER;
                //cellsLeyenda.PaddingTop = 40;
                //tablaTotales.AddCell(cellsLeyenda);
                //string valor = vta.Total.Value.ToString("C2");

                //Paragraph parTotal = new Paragraph(valor, fontVariable);
                //parTotal.Alignment = Element.ALIGN_RIGHT;
                //PdfPCell cellTotal = new PdfPCell();
                //cellTotal.AddElement(parTotal);
                //cellTotal.Border = Rectangle.NO_BORDER;
                //cellTotal.PaddingTop = 40;
                //tablaTotales.AddCell(cellTotal);
                PdfPTable tablaTotales = CrearTablaTotales(vta.Observaciones, vta.Total.HasValue ? vta.Total.Value : 0, 0, fontEtiquetas, fontVariable, fontObservaciones);

                /************** Crear la tabla principal, donde se van a acomodar las demás **************/
                PdfPTable tableDatos = new PdfPTable(1);
                //Quitar el padding.
                tableDatos.DefaultCell.Padding = 0;
                //Quitar el borde.
                tableDatos.DefaultCell.BorderWidth = 0;
                tableDatos.AddCell(tablaDatosVta);
                PdfPCell cellLab = new PdfPCell();
                cellLab.FixedHeight = 200f;
                cellLab.BorderWidth = -100;
                cellLab.PaddingLeft = -40;
                cellLab.AddElement(tablaLaboratorios);
                tableDatos.AddCell(cellLab);
                tableDatos.AddCell(tablaTotales);

                //doc.Add(tablePrincipal);
                doc.Add(tableDatos);
                /************************************************************************************
                    FINALIZAR EL DOCUMENTO.
                ***********************************************************************************/
                //Cerrar y crear el documento final PDF.
                doc.Close();
                return msPdf.ToArray();
            }
        }

        public PdfPTable CrearTablaTotales(string observaciones, decimal MontoPagado, decimal descuento, Font fontEtiquetas, Font fontVariable, Font fontObservaciones)
        {
            //PdfPTable tablaTotales = new PdfPTable(5);
            PdfPTable tablaTotales = new PdfPTable(4);
            string valor;
            if (descuento > 0)
            {
                /* Crear los porcentajes de las columnas. */
                int[] columnastablaTotales = { 14, 56, 10, 20 };
                /* Agregar los porcentajes a la tabla. */
                tablaTotales.SetWidths(columnastablaTotales);
                /* Quitar el borde de las celdas. */
                tablaTotales.DefaultCell.BorderWidth = 0;

                /* Tabla de observaciones */
                if (string.IsNullOrEmpty(observaciones))
                {
                    tablaTotales.AddCell(new Phrase("", fontEtiquetas));
                    tablaTotales.AddCell(new Phrase("", fontVariable));
                }
                else
                {
                    tablaTotales.AddCell(new Phrase("Observaciones:", fontEtiquetas));
                    tablaTotales.AddCell(new Phrase(observaciones, fontVariable));
                }
                tablaTotales.AddCell(new Phrase("", fontVariable));

                /* Tabla de cantidades */
                PdfPTable tablaCantidades = new PdfPTable(2);
                int[] columnastablaCantidades = { 50, 50 };
                /* Agregar los porcentajes a la tabla. */
                tablaCantidades.SetWidths(columnastablaCantidades);
                /* Quitar el borde de las celdas. */
                tablaCantidades.DefaultCell.BorderWidth = 0;

                /*Total*/
                Paragraph parLeyenada = new Paragraph("Total", fontEtiquetas);
                parLeyenada.Alignment = Element.ALIGN_RIGHT;
                PdfPCell cellsLeyenda = new PdfPCell();
                cellsLeyenda.AddElement(parLeyenada);
                cellsLeyenda.Border = Rectangle.NO_BORDER;
                cellsLeyenda.PaddingTop = -5;
                tablaCantidades.AddCell(cellsLeyenda);

                valor = (MontoPagado + descuento).ToString("C2");
                Paragraph parTotal = new Paragraph(valor, fontVariable);
                parTotal.Alignment = Element.ALIGN_RIGHT;
                PdfPCell cellTotal = new PdfPCell();
                cellTotal.AddElement(parTotal);
                cellTotal.Border = Rectangle.NO_BORDER;
                cellTotal.PaddingTop = -5;
                tablaCantidades.AddCell(cellTotal);

                /*Descuento*/
                Paragraph parLeyenadaD = new Paragraph("Descuento", fontEtiquetas);
                parLeyenadaD.Alignment = Element.ALIGN_RIGHT;
                PdfPCell cellsLeyendaD = new PdfPCell();
                cellsLeyendaD.AddElement(parLeyenadaD);
                cellsLeyendaD.Border = Rectangle.NO_BORDER;
                cellsLeyendaD.PaddingTop = -2;
                tablaCantidades.AddCell(cellsLeyendaD);

                valor = descuento.ToString("C2");
                Paragraph parDesc = new Paragraph(valor, fontVariable);
                parDesc.Alignment = Element.ALIGN_RIGHT;
                PdfPCell cellDesc = new PdfPCell();
                cellDesc.AddElement(parDesc);
                cellDesc.Border = Rectangle.NO_BORDER;
                cellDesc.PaddingTop = -2;
                tablaCantidades.AddCell(cellDesc);

                /*A pagar*/
                Paragraph parLeyenadaP = new Paragraph("A pagar", fontEtiquetas);
                parLeyenadaP.Alignment = Element.ALIGN_RIGHT;
                PdfPCell cellsLeyendaP = new PdfPCell();
                cellsLeyendaP.AddElement(parLeyenadaP);
                cellsLeyendaP.Border = Rectangle.NO_BORDER;
                cellsLeyendaP.PaddingTop = -2;
                tablaCantidades.AddCell(cellsLeyendaP);


                valor = MontoPagado.ToString("C2");
                Paragraph parPagar = new Paragraph(valor, fontVariable);
                parPagar.Alignment = Element.ALIGN_RIGHT;
                PdfPCell cellPagar = new PdfPCell();
                cellPagar.AddElement(parPagar);
                cellPagar.Border = Rectangle.NO_BORDER;
                cellPagar.PaddingTop = -2;
                tablaCantidades.AddCell(cellPagar);

                tablaTotales.AddCell(tablaCantidades);
            }
            else
            {
                /* Crear los porcentajes de las columnas. */
                int[] columnastablaTotales = { 14, 56, 10, 20 };
                /* Agregar los porcentajes a la tabla. */
                tablaTotales.SetWidths(columnastablaTotales);
                /* Quitar el borde de las celdas. */
                tablaTotales.DefaultCell.BorderWidth = 0;

                /* Tabla de observaciones */
                if (string.IsNullOrEmpty(observaciones))
                {
                    tablaTotales.AddCell(new Phrase("", fontEtiquetas));
                    tablaTotales.AddCell(new Phrase("", fontVariable));
                }
                else
                {
                    tablaTotales.AddCell(new Phrase("Observaciones:", fontEtiquetas));
                    tablaTotales.AddCell(new Phrase(observaciones, fontVariable));
                }
                tablaTotales.AddCell(new Phrase("", fontVariable));

                /* Tabla de cantidades */
                PdfPTable tablaCantidades = new PdfPTable(2);
                int[] columnastablaCantidades = { 50, 50 };
                /* Agregar los porcentajes a la tabla. */
                tablaCantidades.SetWidths(columnastablaCantidades);
                /* Quitar el borde de las celdas. */
                tablaCantidades.DefaultCell.BorderWidth = 0;

                /*A pagar*/
                Paragraph parLeyenadaTotal = new Paragraph("Total", fontEtiquetas);
                parLeyenadaTotal.Alignment = Element.ALIGN_RIGHT;
                PdfPCell cellsLeyendaP = new PdfPCell();
                cellsLeyendaP.AddElement(parLeyenadaTotal);
                cellsLeyendaP.Border = Rectangle.NO_BORDER;
                cellsLeyendaP.PaddingTop = 0;
                tablaCantidades.AddCell(parLeyenadaTotal);

                valor = MontoPagado.ToString("C2");
                Paragraph parPagar = new Paragraph(valor, fontVariable);
                parPagar.Alignment = Element.ALIGN_RIGHT;
                PdfPCell cellPagar = new PdfPCell();
                cellPagar.AddElement(parPagar);
                cellPagar.Border = Rectangle.NO_BORDER;
                cellPagar.PaddingTop = -2;
                tablaCantidades.AddCell(cellPagar);

                tablaTotales.AddCell(tablaCantidades);
            }

            return tablaTotales;
        }
        #endregion
    }

    public class MyEventHandlerVenta : PdfPageEventHelper
    {
        #region Clases, Constantes y Variables
        //Nombre del archivo PDF template (plantilla).
        public string templateFile { get; set; }
        //Importar el tipo de letra "Microsoft Sans Serif".
        private BaseFont TipoLetra { get; set; }
        //Declarar un objeto PdfContentByte.
        private PdfContentByte cb;
        //Declarar objeto como PDFTemplate.
        public PdfTemplate templatePDF { get; set; }
        //El tamaño de la letra del footer, para el número de página.
        public const float FOOTER_FONT_SIZE = (float)7.5;

        #endregion

        #region Públicas: Manejo de Eventos de Página para Creación de Venta

        //Evento OnOpenDocument.
        public override void OnOpenDocument(PdfWriter writer, Document doc)
        {

            //Nombre de la letra
            //Dim FontName As String = ConfigurationManager.AppSettings("FontMarca")
            //Dim NombreCliente As Font = FontFactory.GetFont(BaseFont.HELVETICA, 9, Font.BOLD, BaseColor.BLACK)
            TipoLetra = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);
            //Establecer un objeto PdfContentByte.
            cb = writer.DirectContent;
            //Establecer objeto como PDFTemplate.
            this.templatePDF = cb.CreateTemplate(30, 10);
        }

        //Evento OnStartPage.
        public override void OnStartPage(PdfWriter writer, Document doc)
        {
            // Obtener el nombre del template PDF.
            string template = templateFile;
            //Crear un objeto PDFReader y obtener la plantilla.
            PdfReader reader = new PdfReader(template);
            // Crear objeto PdfImportedPage.
            PdfImportedPage page;
            //Cargar en el objeto la plantilla.
            page = writer.GetImportedPage(reader, 1);
            // Agregar la plantilla en la nueva página iniciada.
            cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0, 0);
        }

        public override void OnCloseDocument(PdfWriter writer, Document doc)
        {
            // Iniciar texto.
            templatePDF.BeginText();
            // Establecer el tipo de letra y el tamaño.
            templatePDF.SetFontAndSize(TipoLetra, FOOTER_FONT_SIZE);
            // Mostrar el texto deseado.
            templatePDF.ShowText((writer.PageNumber - 1).ToString());
            // Finalizar inserción de texto.
            templatePDF.EndText();
        }

        //Evento OnEndPage.
        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            try
            {
                // El número de la página actual.
                int pageNumber = writer.PageNumber;
                // Espacio en blanco necesario entre la leyenda de CFDI impreso y el número de página (80 espacios en blanco).
                string espacioEnBlanco = "                                                                                         ";
                // Con el objeto PdfContentByte iniciar sección de texto.
                cb.BeginText();
                //' Establecer el tamaño y letra con que se escribirá.
                cb.SetFontAndSize(TipoLetra, FOOTER_FONT_SIZE);
                //' Establecer el recuadro de texto.
                cb.SetTextMatrix(240, (float)8.5);
                // Finalizar escritura de texto.
                cb.EndText();

                //Finalizar evento.
                base.OnEndPage(writer, document);
            }
            catch (Exception ex)
            {
                string prueba = ex.Message;
            }
        }

        #endregion

    }
}
