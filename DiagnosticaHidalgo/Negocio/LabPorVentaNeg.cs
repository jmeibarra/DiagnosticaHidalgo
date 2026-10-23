using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Clases.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DiagnosticaHidalgo.Negocio
{
    public class LabPorVentaNeg
    {
        string _cadenaconn;
        DataClassDataContext context;

        #region Constrcutor
        public LabPorVentaNeg(string cadenaconn)
        {
            this._cadenaconn = cadenaconn;
            this.context = new DataClassDataContext(_cadenaconn);
        }
        #endregion
        /// <summary>
        /// Metodo para obtener los laboratorios que fueron agregados a una venta.
        /// </summary>
        /// <returns></returns>
        public object ObtenerPorIdVenta(int IdVenta)
        {
            var lista = (from labXVta in context.LabPorVenta
                         join lab in context.Laboratorio on labXVta.IdLaboratorio equals lab.IdLaboratorio
                         where labXVta.IdVenta == IdVenta
                         select new
                         {
                             Id = labXVta.IdLabPorVenta,
                             Laboratorio = lab.Descripcion,
                             NumLaboratorios = labXVta.NumLaboratorios,
                             CostoOriginal = labXVta.CostoOriginal,
                             CostoCobrado = labXVta.Costo,
                             CostoAlterado = labXVta.Costo.Value != labXVta.CostoOriginal ? "SI" : "NO",
                             Total = labXVta.Total,
                             Entregado = labXVta.Entregado ? "SI" : "NO"
                         });

            return lista;
        }

        public DataTable ObtenerReservacionesPorFamilia(string familia, DateTime dia)
        {
            DataTable tabla = null;
            using (SqlConnection conn = new SqlConnection(_cadenaconn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("getreservacion", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("familia", familia);
                command.Parameters.AddWithValue("dia", dia.ToString("yyyy-MM-dd"));

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

            return tabla;
        }

        /// <summary>
        /// Método para obtener las citas de hoy y otros días.
        /// </summary>
        /// <returns></returns>
        public DataTable ObtenerReservaciones()
        {
            DataTable tabla = null;
            using (SqlConnection conn = new SqlConnection(_cadenaconn))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("getreservacionbymonth", conn);
                command.CommandType = CommandType.StoredProcedure;                

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

            return tabla;
        }
        /// <summary>
        /// Método para cambiar la propiedad Entregado de la tabla LabPorVenta.
        /// </summary>
        public bool CambiarEntregado(List<LabPorVenta> labPorVentas, ref string msjError)
        {
            SqlTransaction txn = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_cadenaconn))
                {
                    conn.Open();
                    txn = conn.BeginTransaction();

                    /******************************* LABORATORIOS ******************************/
                    foreach (LabPorVenta lv in labPorVentas)
                    {
                        SqlCommand cmdLab = new SqlCommand("UPDATE LabPorVenta SET Entregado=@Entregado,FechaAct=@FechaAct WHERE IdLabPorVenta=@IdLabPorVenta", conn, txn);
                        cmdLab.Parameters.Clear();
                        cmdLab.Parameters.AddWithValue("Entregado", labPorVentas[0].Entregado);
                        cmdLab.Parameters.AddWithValue("FechaAct", DateTime.Now);
                        cmdLab.Parameters.AddWithValue("IdLabPorVenta", labPorVentas[0].IdLabPorVenta);
                        cmdLab.ExecuteNonQuery();
                    }

                    txn.Commit();
                    txn.Dispose();

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                msjError = "Ocurrió un problema al guardar la información, intentelo mas tarde.";
                Utilerias.RegistrarLogError(ex, "LabPorVentaNeg", "CambiarEntregado", "");
                return false;
            }

            return true;
        }

    }
}
