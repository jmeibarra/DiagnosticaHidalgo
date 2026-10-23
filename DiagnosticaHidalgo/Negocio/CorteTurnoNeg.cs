using DiagnosticaHidalgo.Clases;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Excel;
using GemBox.Spreadsheet;

namespace DiagnosticaHidalgo.Negocio
{
    class CorteTurnoNeg
    {
        #region PDF_ANALYZER
        public const int MARGENTOP = 80;
        #endregion

        string _cadenaconn;
        DataClassDataContext context;

        public CorteTurnoNeg(string cadenaconn)
        {
            this._cadenaconn = cadenaconn;
            context = new DataClassDataContext(_cadenaconn);
        }
        #region Agregar
        /// <summary>
        /// Inserta un nuevo registro en la tabla CorteTurno
        /// </summary>
        public int Agregar()
        {
            int IdCorteTurno = 0;
            using (SqlConnection conn = new SqlConnection(_cadenaconn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO CorteTurno(FechaInicio,FechaCreacion) VALUES(@FechaInicio, @FechaCreacion);  SELECT CAST(SCOPE_IDENTITY() AS INT)", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("FechaInicio", DateTime.Now);
                cmd.Parameters.AddWithValue("FechaCreacion", DateTime.Now);

                IdCorteTurno = (int)cmd.ExecuteScalar();

                Utilerias.RegistrarLogError(new Exception("No es una excepción. *SE AGREGO CORTE DE TURNO. IdCorteTurno = " + IdCorteTurno.ToString()), "CorteTurnoNeg", "Agregar", "");
                conn.Close();
            }

            return IdCorteTurno;
        }
        /// <summary>
        /// Inserta un nuevo registro en la tabla CorteTurno
        /// </summary>
        public int AgregarSinVentas()
        {
            CorteTurno corteTurno = new CorteTurno()
            {
                FechaCreacion = DateTime.Now,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now,
                TotalEfectivo = 0,
                TotalCtas = 0,
                MontoTotal = 0
            };
            context.CorteTurno.InsertOnSubmit(corteTurno);
            context.SubmitChanges();

            return corteTurno.IdCorteTurno;
        }
        #endregion
        #region Actualizar
        /// <summary>
        /// Actualiza la información de un corte de turno por pagos y devoluciones.
        /// </summary>
        public int ActualizarPorPagos()
        {
            CorteTurno corteTurno = context.CorteTurno.Where(c => c.FechaFin == null).FirstOrDefault();

            /* Obtener los totales con los pagos del corte de turno */
            PagoNeg pagoNeg = new PagoNeg(this._cadenaconn);
            corteTurno = pagoNeg.ActualizarCorteTurnoPorPagos(corteTurno);
            corteTurno.FechaFin = DateTime.Now;
            if (corteTurno.FechaCreacion == null) corteTurno.FechaCreacion = DateTime.Now;

            List<Venta> ventas = context.Venta.Where(v => v.IdCorteTurno == corteTurno.IdCorteTurno).ToList();
            if (ventas.Count > 0)
                corteTurno.FechaInicio = context.Venta.Where(v => v.IdCorteTurno == corteTurno.IdCorteTurno).OrderBy(v => v.FechaCreacion).FirstOrDefault().FechaCreacion;


            using (SqlConnection conn = new SqlConnection(_cadenaconn))
            {
                conn.Open();
                SqlCommand cmdVenta = new SqlCommand("UPDATE CorteTurno SET TotalEfectivo=@TotalEfectivo, TotalCtas=@TotalCtas, MontoTotal=@MontoTotal, " +
                "FechaInicio=@FechaInicio, FechaFin=@FechaFin, FechaAct=@FechaAct, FechaCreacion=@FechaCreacion WHERE IdCorteTurno=@IdCorteTurno", conn);
                cmdVenta.Parameters.Clear();
                cmdVenta.Parameters.AddWithValue("TotalEfectivo", corteTurno.TotalEfectivo);
                cmdVenta.Parameters.AddWithValue("TotalCtas", corteTurno.TotalCtas);
                cmdVenta.Parameters.AddWithValue("MontoTotal", corteTurno.MontoTotal);
                cmdVenta.Parameters.AddWithValue("FechaInicio", corteTurno.FechaInicio);
                cmdVenta.Parameters.AddWithValue("FechaFin", corteTurno.FechaFin);
                cmdVenta.Parameters.AddWithValue("FechaAct", DateTime.Now);
                cmdVenta.Parameters.AddWithValue("FechaCreacion", corteTurno.FechaCreacion);
                cmdVenta.Parameters.AddWithValue("IdCorteTurno", corteTurno.IdCorteTurno);

                cmdVenta.ExecuteNonQuery();
                conn.Close();
            }

            return corteTurno.IdCorteTurno;
        }

        /// <summary>
        /// Actualiza la información de un corte de turno por pagos y devoluciones.
        /// </summary>
        public int Finalizar(ref string msjError)
        {
            int idcorteturno = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(_cadenaconn))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("corteturno_finalizar", conn);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            msjError = reader["MENSAJEERR"].ToString();
                            idcorteturno = Convert.ToInt32(reader["IDCORTETURNO"].ToString());
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, "CorteTurnoNeg", "Finalizar", "EMPLEADO");
                msjError = "Ocurrió un error al intentar finalizar el corte de turno.";
                idcorteturno = -1;
            }

            return idcorteturno;
        }
        #endregion
        #region Consultar registros
        /// <summary>
        /// Metodo para obtener un corte de turno por IdCorteTurno.
        /// </summary>
        /// <returns></returns>
        public CorteTurno ObtenerPorId(int IdCorteTurno)
        {
            return context.CorteTurno.Where(c => c.IdCorteTurno == IdCorteTurno).FirstOrDefault();
        }

        /// <summary>
        /// Metodo para obtener el último corte de turno.
        /// </summary>
        /// <returns></returns>
        public CorteTurno ObtenerSinFechaFin()
        {
            CorteTurno ct = (from c in context.CorteTurno
                             where c.FechaFin == null
                             orderby c.IdCorteTurno descending
                             select c).FirstOrDefault();
            context.Connection.Close();
            return ct;
        }

        /// <summary>
        /// Obtiene los cortes de turno por una fecha inicial y otra fecha final.
        /// </summary>
        /// <returns></returns>
        public object ObtenerPorFiltros(DateTime fechaInicio, DateTime fechaFin)
        {
            object var = (from ct in context.CorteTurno
                          where ct.FechaFin != null && fechaInicio <= ct.FechaCreacion &&
                                ct.FechaCreacion <= fechaFin
                          orderby ct.FechaCreacion ascending
                          select new
                          {
                              Id = ct.IdCorteTurno,
                              FechaCreacion = ct.FechaCreacion,
                              TotalEfectivo = ct.TotalEfectivo,
                              TotalCuentas = ct.TotalCtas,
                              Total = ct.MontoTotal,
                              FechaAct = ct.FechaAct
                          }).ToList();
            context.Connection.Close();

            return var;
        }

        public List<CorteTurnoMod> ObtenerCortesPorRango(DateTime fechaInicio, DateTime fechaFin)
        {
            var lista = new List<CorteTurnoMod>();

            using (SqlConnection conn = new SqlConnection(_cadenaconn))
            {
                conn.Open();

                string query = @"
            SELECT IdCorteTurno, FechaInicio, FechaFin
            FROM CorteTurno
            WHERE (CAST(FechaInicio AS DATE) BETWEEN @FechaInicio AND @FechaFin)
               OR (CAST(FechaFin AS DATE) BETWEEN @FechaInicio AND @FechaFin)
            ORDER BY IdCorteTurno DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = fechaInicio.Date;
                    cmd.Parameters.Add("@FechaFin", SqlDbType.Date).Value = fechaFin.Date;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new CorteTurnoMod
                            {
                                Id = reader.GetInt32(0),
                                FechaCreacion = reader.GetDateTime(1),
                                FechaFinal = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2)
                            });
                        }
                    }
                }
            }

            return lista;
        }


        #endregion

        #region Imprimir
        public byte[] CrearPDF(string empresa, string fecha, string montoCtas, string montoEfectivo,
            string montoTotal, int IdCorteTurno)
        {

            Document doc = new Document(PageSize.LETTER, -31, 0, MARGENTOP, 0);
            using (MemoryStream msPdf = new MemoryStream())
            {
                //Inicializar el objeto PdfWriter para crear el documento PDF.
                PdfWriter writer = PdfWriter.GetInstance(doc, msPdf);
                //La plantilla de fondo la seleccionamos dependiendo de la marca
                string TemplatePDF = C.PathPlantillasPDF + "dh_corte.pdf";
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
                doc.AddTitle("CorteTurno_" + DateTime.Now.ToString());
                //Agregar asunto del documento pdf.
                doc.AddSubject("CorteTurno");

                //Abrir el documento PDF para su creación.
                doc.Open();

                //Crear una tabla que contiene los datos de la nota.
                PdfPTable tablaFecha = new PdfPTable(2);
                //Crear los porcentajes de las columnas.
                int[] columnastablaFecha = { 80, 20 };
                //Agregar los porcentajes a la tabla.
                tablaFecha.SetWidths(columnastablaFecha);
                //Quitar el borde de las celdas.
                tablaFecha.DefaultCell.BorderWidth = 0;
                tablaFecha.SpacingAfter = 10;

                tablaFecha.AddCell(new Phrase("", fontEtiquetas));
                tablaFecha.AddCell(new Phrase("Fecha: " + fecha, fontEtiquetas));

                doc.Add(tablaFecha);

                /******************************* MONTOS *******************************************/

                //Crear una tabla que contiene los montos del corte de turno.
                PdfPTable tablaMontos = new PdfPTable(3);
                //Crear los porcentajes de las columnas.
                int[] columnastablaMontos = { 33, 33, 33 };
                //Agregar los porcentajes a la tabla.
                tablaMontos.SetWidths(columnastablaMontos);
                //Quitar el borde de las celdas.
                tablaMontos.SpacingAfter = 20;

                //Encabezados de la tabla
                tablaMontos.AddCell(new Phrase("Monto en cuentas ", fontEtiquetas));
                tablaMontos.AddCell(new Phrase("Monto en efectivo", fontEtiquetas));
                tablaMontos.AddCell(new Phrase("Monto total de corte", fontEtiquetas));
                tablaMontos.AddCell(new Phrase("$ " + montoCtas, fontEtiquetas));
                tablaMontos.AddCell(new Phrase("$ " + montoEfectivo, fontEtiquetas));
                tablaMontos.AddCell(new Phrase("$ " + montoTotal, fontEtiquetas));

                doc.Add(tablaMontos);

                //Crear una tabla que contiene las ventas.
                PdfPTable tablaVentas = new PdfPTable(8);
                //Crear los porcentajes de las columnas.
                int[] columnastablaLaboratorios = { 10, 10, 10, 30, 10, 10, 10, 10 };
                //Agregar los porcentajes a la tabla.
                tablaVentas.SetWidths(columnastablaLaboratorios);
                tablaVentas.SpacingAfter = 20;

                /************************** ENCABEZADOS *****************************/
                string[] encabezados = new string[] { "FECHA", "Folio", "Maquina", "Doctor", "TotalVenta", "Descuento", "Pagado", "Estatus" };
                foreach (string enc in encabezados)
                    tablaVentas.AddCell(new Phrase(enc.ToUpper(), fontEtiquetas));


                var ventas = (from v in context.Venta
                              join c in context.Cliente on v.IdCliente equals c.IdCliente
                              join u in context.Usuario on v.IdUsuario equals u.IdUsuario
                              join d in context.Doctor on v.IdDoctor equals d.IdDoctor into DoctorSet
                              from DS in DoctorSet.DefaultIfEmpty()
                              where v.IdCorteTurno == IdCorteTurno && v.Estatus != VentaNeg.EST_COTIZACION
                              orderby v.Folio ascending
                              select new
                              {
                                  Folio = v.Folio,
                                  Fecha = v.FechaCreacion,
                                  Maquina = v.NumMaquina,
                                  Doctor = v.IdDoctor != null ? DS.Nombre : "",
                                  TotalVenta = v.Total,
                                  Descuento = v.Descuento != null ? v.Descuento.Value : 0,
                                  Pagado = context.Pago.Where(p => p.IdVenta == v.IdVenta).Sum(p => p.Cantidad),
                                  Estatus = v.Estatus == "P" ? "PENDIENTE" : v.Estatus == "C" ? "CANCELADA" : "VIGENTE"
                              });

                foreach (var v in ventas)
                {
                    tablaVentas.AddCell(new Phrase(v.Fecha.ToString(), fontVariable));
                    tablaVentas.AddCell(new Phrase(v.Folio.ToString(), fontVariable));
                    tablaVentas.AddCell(new Phrase(v.Maquina, fontVariable));
                    tablaVentas.AddCell(new Phrase(v.Doctor, fontVariable));
                    tablaVentas.AddCell(new Phrase(v.TotalVenta.Value.ToString("C2"), fontVariable));
                    tablaVentas.AddCell(new Phrase(v.Descuento.ToString("C2"), fontVariable));
                    if (v.Pagado != null) { tablaVentas.AddCell(new Phrase(v.Pagado.Value.ToString("C2"), fontVariable)); }
                    else { tablaVentas.AddCell(new Phrase("", fontVariable)); }
                    tablaVentas.AddCell(new Phrase(v.Estatus, fontVariable));
                }

                doc.Add(tablaVentas);


                /****************** TABLA DE PAGOS ***********************/
                //Crear una tabla que contiene las ventas.
                PdfPTable tablaPagos = new PdfPTable(5);
                //Crear los porcentajes de las columnas.
                int[] columnastablaPagos = { 25, 25, 25, 15, 10 };
                //Agregar los porcentajes a la tabla.
                tablaPagos.SetWidths(columnastablaPagos);
                //Quitar el borde de las celdas.
                //tablaPagos.DefaultCell.BorderWidth = 0;

                /************************** ENCABEZADOS *****************************/
                string[] encabezadosP = new string[] { "FECHA PAGO", "FECHA VENTA", "CANTIDAD", "TIPO PAGO", "" };
                foreach (string enc in encabezadosP)
                    tablaPagos.AddCell(new Phrase(enc.ToUpper(), fontEtiquetas));

                var pagos = (from p in context.Pago
                             join v in context.Venta on p.IdVenta equals v.IdVenta
                             where p.IdCorteTurno == IdCorteTurno
                             orderby v.Folio ascending
                             select new
                             {
                                 FechaPago = p.FechaCreacion,
                                 FechaVenta = v.FechaCreacion,
                                 Cantidad = Math.Abs(p.Cantidad.Value),
                                 TipoPago = p.TipoPago,
                                 Devolucion = p.Devolucion.HasValue && p.Devolucion == true ? "DEVOLUCION" : "PAGO"
                             }).ToList();


                foreach (var p in pagos)
                {
                    tablaPagos.AddCell(new Phrase(p.FechaPago.ToString(), fontVariable));
                    tablaPagos.AddCell(new Phrase(p.FechaVenta.ToString(), fontVariable));
                    tablaPagos.AddCell(new Phrase(p.Cantidad.ToString("C2"), fontVariable));
                    tablaPagos.AddCell(new Phrase(p.TipoPago, fontVariable));
                    tablaPagos.AddCell(new Phrase(p.Devolucion, fontVariable));
                }

                doc.Add(tablaPagos);

                /************************************************************************************
                    FINALIZAR EL DOCUMENTO.
                ***********************************************************************************/
                //Cerrar y crear el documento final PDF.
                doc.Close();
                return msPdf.ToArray();
            }
        }

        public byte[] CrearReporteEstudiosPDF(DateTime fechaIni, DateTime fechaFin, string empresa)
        {
            Document doc = new Document(PageSize.LETTER, -31, 0, MARGENTOP, 0);
            using (MemoryStream msPdf = new MemoryStream())
            {
                //Inicializar el objeto PdfWriter para crear el documento PDF.
                PdfWriter writer = PdfWriter.GetInstance(doc, msPdf);
                //La plantilla de fondo la seleccionamos dependiendo de la marca
                string TemplatePDF = C.PathPlantillasPDF + "dh_corte.pdf";
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

                //Fuentes para datos del reporte
                Font fontEtiquetas = new Font(sans_serif, 9, Font.BOLD, BaseColor.BLACK);
                Font fontVariable = new Font(sans_serif, 9, Font.NORMAL, BaseColor.BLACK);

                //Agregar autor del documento pdf.
                doc.AddAuthor(empresa);
                //Agregar creador del documento pdf.
                doc.AddCreator(empresa);
                //Agregar título del documento pdf.
                doc.AddTitle("Reporte_estudios_" + DateTime.Now.ToString());
                //Agregar asunto del documento pdf.
                doc.AddSubject("Reporte de estudios");

                //Abrir el documento PDF para su creación.
                doc.Open();

                //Crear una tabla que contiene los datos de la nota.
                PdfPTable tablaFecha = new PdfPTable(2);
                //Crear los porcentajes de las columnas.
                int[] columnastablaFecha = { 50, 50 };
                //Agregar los porcentajes a la tabla.
                tablaFecha.SetWidths(columnastablaFecha);
                //Quitar el borde de las celdas.
                tablaFecha.DefaultCell.BorderWidth = 0;
                tablaFecha.SpacingAfter = 10;

                tablaFecha.AddCell(new Phrase("Fecha de inicio: " + fechaIni, fontEtiquetas));
                tablaFecha.AddCell(new Phrase("Fecha final: " + fechaFin, fontEtiquetas));

                doc.Add(tablaFecha);

                /******************************* MONTOS *******************************************/
                /*Obtener los montos de los estudios*/
                var totEst = (from lv in context.LabPorVenta
                              join v in context.Venta.Where(v => fechaIni <= v.FechaCreacion && v.FechaCreacion <= fechaFin && v.Estatus == "V")
                              on lv.IdVenta equals v.IdVenta
                              join l in context.Laboratorio on lv.IdLaboratorio equals l.IdLaboratorio
                              orderby l.Descripcion ascending
                              group lv by new { l.Descripcion } into grupo
                              select new
                              {
                                  Estudio = grupo.Key.Descripcion,
                                  Numestudios = grupo.Count(),
                                  Cantidad = (double)grupo.Sum(lv => lv.Total)
                              }).ToList().OrderByDescending(td => td.Cantidad).ToList();

                double montoTotal = 0;
                montoTotal = totEst.Sum(te => te.Cantidad);
                double descuentos = (double)context.Venta.Where(v => fechaIni <= v.FechaCreacion && v.FechaCreacion <= fechaFin && v.Estatus == "V").Sum(v => v.Descuento ?? 0);

                /*Totales del reporte*/
                //Crear una tabla que contiene los datos de la nota.
                PdfPTable tablamontosTot = new PdfPTable(3);
                //Crear los porcentajes de las columnas.
                int[] columnastablamontosTot = { 33, 33, 33 };
                //Agregar los porcentajes a la tabla.
                tablamontosTot.SetWidths(columnastablamontosTot);
                //Quitar el borde de las celdas.
                tablamontosTot.DefaultCell.BorderWidth = 0;
                tablamontosTot.SpacingAfter = 10;

                tablamontosTot.AddCell(new Phrase("Monto total sin aplicar descuentos", fontEtiquetas));
                tablamontosTot.AddCell(new Phrase("Monto total de descuentos en ventas", fontEtiquetas));
                tablamontosTot.AddCell(new Phrase("Monto total aplicando descuentos", fontEtiquetas));

                tablamontosTot.AddCell(new Phrase(montoTotal.ToString("C2"), fontVariable));
                tablamontosTot.AddCell(new Phrase(descuentos.ToString("C2"), fontVariable));
                tablamontosTot.AddCell(new Phrase((montoTotal - descuentos).ToString("C2"), fontVariable));

                doc.Add(tablamontosTot);

                /****************************************** TABLA DE TOTALES POR ESTUDIO ****************************************/
                //Crear una tabla que contiene los datos del estudio.
                PdfPTable tablaTot = new PdfPTable(3);
                //Crear los porcentajes de las columnas.
                int[] columnasDoc = { 70, 15, 15 };
                //Agregar los porcentajes a la tabla.
                tablaTot.SetWidths(columnasDoc);
                //Quitar el borde de las celdas.
                tablaTot.DefaultCell.BorderWidth = 1;
                tablaTot.SpacingAfter = 10;

                tablaTot.AddCell(new Phrase("ESTUDIO", fontEtiquetas));
                tablaTot.AddCell(new Phrase("NÚMERO DE ESTUDIOS", fontEtiquetas));
                tablaTot.AddCell(new Phrase("Total", fontEtiquetas));

                foreach (var e in totEst)
                {
                    tablaTot.AddCell(new Phrase(e.Estudio, fontVariable));
                    tablaTot.AddCell(new Phrase(e.Numestudios.ToString(), fontVariable));
                    tablaTot.AddCell(new Phrase(e.Cantidad.ToString("C2"), fontVariable));
                }
                doc.Add(tablaTot);

                /************************************************************************************
                    FINALIZAR EL DOCUMENTO.
                ***********************************************************************************/
                //Cerrar y crear el documento final PDF.
                doc.Close();
                return msPdf.ToArray();
            }
        }

        public byte[] CrearReporteFamiliaEstudiosPDF(DateTime fechaIni, DateTime fechaFin, string empresa)
        {
            Document doc = new Document(PageSize.LETTER, -31, 0, MARGENTOP, 0);
            using (MemoryStream msPdf = new MemoryStream())
            {
                //Inicializar el objeto PdfWriter para crear el documento PDF.
                PdfWriter writer = PdfWriter.GetInstance(doc, msPdf);
                //La plantilla de fondo la seleccionamos dependiendo de la marca
                string TemplatePDF = C.PathPlantillasPDF + "dh_corte.pdf";
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

                //Fuentes para datos del reporte
                Font fontEtiquetas = new Font(sans_serif, 9, Font.BOLD, BaseColor.BLACK);
                Font fontVariable = new Font(sans_serif, 9, Font.NORMAL, BaseColor.BLACK);

                //Agregar autor del documento pdf.
                doc.AddAuthor(empresa);
                //Agregar creador del documento pdf.
                doc.AddCreator(empresa);
                //Agregar título del documento pdf.
                doc.AddTitle("Reporte_familia_estudios_" + DateTime.Now.ToString());
                //Agregar asunto del documento pdf.
                doc.AddSubject("Reporte por familia de estudios");

                //Abrir el documento PDF para su creación.
                doc.Open();

                //Crear una tabla que contiene los datos de la nota.
                PdfPTable tablaFecha = new PdfPTable(2);
                //Crear los porcentajes de las columnas.
                int[] columnastablaFecha = { 50, 50 };
                //Agregar los porcentajes a la tabla.
                tablaFecha.SetWidths(columnastablaFecha);
                //Quitar el borde de las celdas.
                tablaFecha.DefaultCell.BorderWidth = 0;
                tablaFecha.SpacingAfter = 10;

                tablaFecha.AddCell(new Phrase("Fecha de inicio: " + fechaIni, fontEtiquetas));
                tablaFecha.AddCell(new Phrase("Fecha final: " + fechaFin, fontEtiquetas));

                doc.Add(tablaFecha);

                /******************************* MONTOS *******************************************/
                /*Obtener los montos de los estudios*/
                var totFamEst = (from lv in context.LabPorVenta
                                 join v in context.Venta.Where(v => fechaIni <= v.FechaCreacion && v.FechaCreacion <= fechaFin && v.Estatus == "V")
                                 on lv.IdVenta equals v.IdVenta
                                 join l in context.Laboratorio on lv.IdLaboratorio equals l.IdLaboratorio
                                 orderby l.Familia ascending
                                 group lv by new { l.Familia } into grupo
                                 select new
                                 {
                                     Familia = grupo.Key.Familia == "" ? "SIN FAMILIA" : grupo.Key.Familia,
                                     Cantidad = (double)grupo.Sum(lv => lv.Total)
                                 }).ToList().OrderByDescending(td => td.Cantidad).ToList();

                double montoTotal = 0;
                montoTotal = totFamEst.Sum(te => te.Cantidad);
                double descuentos = (double)context.Venta.Where(v => fechaIni <= v.FechaCreacion && v.FechaCreacion <= fechaFin && v.Estatus == "V").Sum(v => v.Descuento ?? 0);

                /*Totales del reporte*/
                //Crear una tabla que contiene los datos de la nota.
                PdfPTable tablamontosTot = new PdfPTable(3);
                //Crear los porcentajes de las columnas.
                int[] columnastablamontosTot = { 33, 33, 33 };
                //Agregar los porcentajes a la tabla.
                tablamontosTot.SetWidths(columnastablamontosTot);
                //Quitar el borde de las celdas.
                tablamontosTot.DefaultCell.BorderWidth = 0;
                tablamontosTot.SpacingAfter = 10;

                tablamontosTot.AddCell(new Phrase("Monto total sin aplicar descuentos", fontEtiquetas));
                tablamontosTot.AddCell(new Phrase("Monto total de descuentos en ventas", fontEtiquetas));
                tablamontosTot.AddCell(new Phrase("Monto total aplicando descuentos", fontEtiquetas));

                tablamontosTot.AddCell(new Phrase(montoTotal.ToString("C2"), fontVariable));
                tablamontosTot.AddCell(new Phrase(descuentos.ToString("C2"), fontVariable));
                tablamontosTot.AddCell(new Phrase((montoTotal - descuentos).ToString("C2"), fontVariable));

                doc.Add(tablamontosTot);

                /****************************************** TABLA DE TOTALES POR ESTUDIO ****************************************/
                foreach (var e in totFamEst)
                {
                    //Crear una tabla que contiene los datos de la nota.
                    PdfPTable tablaTot = new PdfPTable(2);
                    //Crear los porcentajes de las columnas.
                    int[] columnasDoc = { 50, 50 };
                    //Agregar los porcentajes a la tabla.
                    tablaTot.SetWidths(columnasDoc);
                    //Quitar el borde de las celdas.
                    tablaTot.DefaultCell.BorderWidth = 0;
                    tablaTot.SpacingAfter = 10;

                    tablaTot.AddCell(new Phrase("Familia de estudio: " + e.Familia, fontEtiquetas));
                    tablaTot.AddCell(new Phrase("Cantidad: " + e.Cantidad.ToString("C2"), fontEtiquetas));
                    doc.Add(tablaTot);
                }

                /************************************************************************************
                    FINALIZAR EL DOCUMENTO.
                ***********************************************************************************/
                //Cerrar y crear el documento final PDF.
                doc.Close();
                return msPdf.ToArray();
            }
        }
        public byte[] CrearReporteDoctorPDF(DateTime fechaIni, DateTime fechaFin, string empresa)
        {
            try
            {
                Document doc = new Document(PageSize.LETTER, -31, 0, MARGENTOP, 0);
                using (MemoryStream msPdf = new MemoryStream())
                {
                    //Inicializar el objeto PdfWriter para crear el documento PDF.
                    PdfWriter writer = PdfWriter.GetInstance(doc, msPdf);
                    //La plantilla de fondo la seleccionamos dependiendo de la marca
                    string TemplatePDF = C.PathPlantillasPDF + "dh_corte.pdf";
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

                    //Fuentes para datos del reporte
                    Font fontEtiquetas = new Font(sans_serif, 9, Font.BOLD, BaseColor.BLACK);
                    Font fontVariable = new Font(sans_serif, 9, Font.NORMAL, BaseColor.BLACK);

                    //Agregar autor del documento pdf.
                    doc.AddAuthor(empresa);
                    //Agregar creador del documento pdf.
                    doc.AddCreator(empresa);
                    //Agregar título del documento pdf.
                    doc.AddTitle("Reporte_doctores_" + DateTime.Now.ToString());
                    //Agregar asunto del documento pdf.
                    doc.AddSubject("Reporte de doctores");

                    //Abrir el documento PDF para su creación.
                    doc.Open();

                    //Crear una tabla que contiene los datos de la nota.
                    PdfPTable tablaFecha = new PdfPTable(2);
                    //Crear los porcentajes de las columnas.
                    int[] columnastablaFecha = { 50, 50 };
                    //Agregar los porcentajes a la tabla.
                    tablaFecha.SetWidths(columnastablaFecha);
                    //Quitar el borde de las celdas.
                    tablaFecha.DefaultCell.BorderWidth = 0;
                    tablaFecha.SpacingAfter = 10;

                    tablaFecha.AddCell(new Phrase("Fecha de inicio: " + fechaIni, fontEtiquetas));
                    tablaFecha.AddCell(new Phrase("Fecha final: " + fechaFin, fontEtiquetas));
                    doc.Add(tablaFecha);
                    //No se agrega la tabla hasta tener el monto total del reporte


                    /**************************************************************** MONTOS ******************************************************/

                    /*Obtener los montos de los doctores*/
                    var totDoc = (from v in context.Venta.Where(v => fechaIni <= v.FechaCreacion && v.FechaCreacion <= fechaFin && v.Estatus == "V")
                                  join d in context.Doctor on v.IdDoctor equals d.IdDoctor
                                  group v by new { d.Nombre } into grupo
                                  select new
                                  {
                                      Doctor = grupo.Key.Nombre,
                                      TotalDoctor = (double)grupo.Sum(v => v.Total) + (double)grupo.Sum(v => v.Descuento ?? 0),
                                      Descuento = (double)grupo.Sum(v => v.Descuento ?? 0)
                                  }).ToList().OrderByDescending(td => td.TotalDoctor).ToList();

                    /* Ventas sin doctor */
                    List<Venta> ventasSD = context.Venta.Where(v => fechaIni <= v.FechaCreacion && v.FechaCreacion <= fechaFin &&
                                            v.Estatus == "V" && v.IdDoctor == null).ToList();

                    if (ventasSD.Count > 0)
                    {
                        double total = Convert.ToDouble(ventasSD.Sum(v => v.Total).Value) + Convert.ToDouble(ventasSD.Sum(v => v.Descuento ?? 0));
                        double descuento = Convert.ToDouble(ventasSD.Sum(v => v.Descuento ?? 0));
                        totDoc.Add(new { Doctor = "SIN DOCTOR", TotalDoctor = total, Descuento = descuento });
                    }

                    /******************************* MONTOS TOTALES DEL REPORTE **************************/
                    double montoTotal = 0, descuentos = 0;
                    montoTotal = totDoc.Sum(td => td.TotalDoctor);
                    descuentos = totDoc.Sum(td => td.Descuento);

                    /*Totales del reporte*/
                    //Crear una tabla que contiene los datos de la nota.
                    PdfPTable tablamontosTot = new PdfPTable(3);
                    //Crear los porcentajes de las columnas.
                    int[] columnastablamontosTot = { 33, 33, 33 };
                    //Agregar los porcentajes a la tabla.
                    tablamontosTot.SetWidths(columnastablamontosTot);
                    //Quitar el borde de las celdas.
                    tablamontosTot.DefaultCell.BorderWidth = 0;
                    tablamontosTot.SpacingAfter = 10;

                    tablamontosTot.AddCell(new Phrase("Monto total sin aplicar descuentos", fontEtiquetas));
                    tablamontosTot.AddCell(new Phrase("Monto total de descuentos en ventas", fontEtiquetas));
                    tablamontosTot.AddCell(new Phrase("Monto total aplicando descuentos", fontEtiquetas));

                    tablamontosTot.AddCell(new Phrase(montoTotal.ToString("C2"), fontVariable));
                    tablamontosTot.AddCell(new Phrase(descuentos.ToString("C2"), fontVariable));
                    tablamontosTot.AddCell(new Phrase((montoTotal - descuentos).ToString("C2"), fontVariable));

                    doc.Add(tablamontosTot);


                    foreach (var d in totDoc)
                    {

                        //Crear una tabla que contiene los datos de la nota.
                        PdfPTable tablaDoc = new PdfPTable(2);
                        //Crear los porcentajes de las columnas.
                        int[] columnasDoc = { 50, 50 };
                        //Agregar los porcentajes a la tabla.
                        tablaDoc.SetWidths(columnasDoc);
                        //Quitar el borde de las celdas.
                        tablaDoc.DefaultCell.BorderWidth = 0;
                        tablaDoc.SpacingAfter = 10;

                        tablaDoc.AddCell(new Phrase("Doctor: " + d.Doctor, fontEtiquetas));
                        //double totalDoc = d.TotalDoctor + d.Descuento;
                        tablaDoc.AddCell(new Phrase("Cantidad: " + d.TotalDoctor.ToString("C2"), fontEtiquetas));
                        doc.Add(tablaDoc);
                    }


                    /************************************************************************************
                        FINALIZAR EL DOCUMENTO.
                    ***********************************************************************************/
                    //Cerrar y crear el documento final PDF.
                    doc.Close();
                    return msPdf.ToArray();
                }

            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, "CorteTurnoNeg", "CrearReporteDoctorPDF", "");
                return null;
            }
        }

        public void CrearReporteDetalladoPorCorteTurno(int idcorteturno, string rutaguardar)
        {
            try
            {
                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

                using (SqlConnection conn = new SqlConnection(_cadenaconn))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("reportenotasporcorteturno", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IDCORTETURNO", idcorteturno);

                    // Usar DataSet para múltiples tablas
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);

                    var workbook = new ExcelFile();

                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        var worksheet = workbook.Worksheets.Add("Tabla" + (i + 1));
                        worksheet.InsertDataTable(ds.Tables[i], new InsertDataTableOptions()
                        {
                            ColumnHeaders = true,
                            StartRow = 0
                        });

                        // Buscar columnas por nombre
                        int colCosto = ds.Tables[i].Columns.IndexOf("COSTO_TOTAL_FAMILIA");
                        int colEfectivo = ds.Tables[i].Columns.IndexOf("PAGO_EN_EFECTIVO");
                        int colTarjeta = ds.Tables[i].Columns.IndexOf("PAGO_CON_TARJETA");

                        // Aplicar formato solo a las filas con datos
                        int startRow = 1; // porque la fila 0 son los encabezados
                        int endRow = ds.Tables[i].Rows.Count;

                        if (colCosto >= 0)
                            worksheet.Cells.GetSubrangeAbsolute(startRow, colCosto, endRow, colCosto).Style.NumberFormat = "\"$\"#,##0.00";

                        if (colEfectivo >= 0)
                            worksheet.Cells.GetSubrangeAbsolute(startRow, colEfectivo, endRow, colEfectivo).Style.NumberFormat = "\"$\"#,##0.00";

                        if (colTarjeta >= 0)
                            worksheet.Cells.GetSubrangeAbsolute(startRow, colTarjeta, endRow, colTarjeta).Style.NumberFormat = "\"$\"#,##0.00";


                    }


                    workbook.Save(rutaguardar);
                    da.Dispose();
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, "CorteTurnoNeg", "CrearReporteDetalladoPorCorteTurno", "EMPLEADO");
            }
        }

        #endregion
    }
}
