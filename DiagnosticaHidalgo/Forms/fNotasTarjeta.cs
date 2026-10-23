using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Clases.Modelos;
using DiagnosticaHidalgo.Negocio;
using DiagnosticaHidalgo.Properties;
using Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace DiagnosticaHidalgo.Forms
{
    public partial class fNotasTarjeta : Form
    {
        private const string C_CLASE = "fNotasTarjeta";
        List<RenglonExcel> foliosnotas = new List<RenglonExcel>();
        Configuraciones _conf;
        /// <summary>
        /// Usuario que esta manejando el sistema
        /// </summary>
        public Usuario us = null;

        /// <summary>
        /// Evento incial de formulario
        /// </summary>
        /// <param name="conf"></param>
        public fNotasTarjeta(Usuario usuario, Configuraciones conf)
        {
            InitializeComponent();
            this._conf = conf;
            this.us = usuario;
            openFileDialog_Cargar.Filter = "Excel Files| *.xls; *.xlsx; *.xlsm";
            //btnCargarArch.BackColor = C.COLOR_SISTEMA;
            //btnDescargarPlantilla.BackColor = C.COLOR_SISTEMA;
            //btnImprimir.BackColor = C.COLOR_SISTEMA;

            foreach (Control ctrl in pnlIndicaciones.Controls)
            {
                if (ctrl is Button)
                {
                    ctrl.BackColor = C.COLOR_SISTEMA;
                    ctrl.ForeColor = C.COLOR_SISTEMA_CLARO;
                }
                else ctrl.ForeColor = C.COLOR_SISTEMA;
            }
            foreach (Control ctrl in pnlFinal.Controls)
            {
                if (ctrl is Button)
                {
                    ctrl.BackColor = C.COLOR_SISTEMA;
                    ctrl.ForeColor = C.COLOR_SISTEMA_CLARO;
                }
                else ctrl.ForeColor = C.COLOR_SISTEMA;
            }
        }

        /// <summary>
        /// Evento para cargar archivo que contiene las notas que fueron pagadas con tarjeta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCargarArch_Click(object sender, EventArgs e)
        {
            //Seccion para lanzar el open file dialog
            if (openFileDialog_Cargar.ShowDialog() == DialogResult.OK)
            {
                //Muestro la ruta y el nombre del archivo que contiene las notas
                lblNombreArch.Text = openFileDialog_Cargar.FileName;
                //Cargar los datos del archivo en un gridview
                byte[] buffer = File.ReadAllBytes(openFileDialog_Cargar.FileName);
                ProcesarContenidoArchivo(buffer);
                //AgregarEliminarGVNotas();
            }
        }

        /// <summary>
        /// Método para analizar el contenido del archivo y mostrarlo en el gridview
        /// </summary>
        /// <param name="contenidoArchivo"></param>
        private void ProcesarContenidoArchivo(byte[] contenidoArchivo)
        {
            try
            {
                IExcelDataReader reader;
                foliosnotas = new List<RenglonExcel>();
                gvNotas.DataSource = null;
                int i = 0;
                string mensajeError = "";

                using (MemoryStream ms = new MemoryStream(contenidoArchivo))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(ms);
                    DataSet DS = reader.AsDataSet();

                    if (DS.Tables.Count == 0)
                        mensajeError = "El archivo no contiene folios de nota válidos para procesar.";
                    else
                    {
                        foreach (DataRow renglon in DS.Tables[0].Rows)
                        {
                            if (renglon[0].ToString().Trim() != "FolioReal")
                            {
                                //Validar que el folio no este repetido
                                string FolioReal = renglon[0].ToString().Trim();
                                RenglonExcel repetido = foliosnotas.Find(f => f.FolioReal == FolioReal);
                                if (repetido == null)
                                {
                                    RenglonExcel nuevorenglon = new RenglonExcel()
                                    {
                                        FolioReal = FolioReal,
                                        NuevoFolio = renglon[1].ToString().Trim()
                                    };

                                    VentaNeg ventaNeg = new VentaNeg(us.Cadenaconn);
                                    DataTable dtVenta = ventaNeg.ObtenerPorFolio(FolioReal);
                                    if (dtVenta != null && dtVenta.Rows != null && dtVenta.Rows.Count > 0)
                                    {
                                        if (FolioReal == dtVenta.Rows[0]["Folio"].ToString())
                                        {
                                            nuevorenglon.IdVenta = dtVenta.Rows[0]["IdVenta"].ToString();
                                            gvNotas.Rows.Insert(i++, dtVenta.Rows[0][0].ToString(), dtVenta.Rows[0][1].ToString(), nuevorenglon.FolioReal, nuevorenglon.NuevoFolio, dtVenta.Rows[0][4].ToString(), dtVenta.Rows[0][5].ToString(), dtVenta.Rows[0][6]);
                                            foliosnotas.Add(nuevorenglon);
                                        }
                                    }
                                }
                            }
                        }

                        if (foliosnotas.Count > 0)
                        {
                            btnImprimir.Enabled = true;
                            gvNotas.Columns["Total"].DefaultCellStyle.Format = "C2";
                            gvNotas.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                        }
                        else
                        {
                            mensajeError = "El archivo no contiene folios de nota válidos para procesar.";
                        }
                    }
                }

                if (foliosnotas.Count == 0 || !string.IsNullOrEmpty(mensajeError))
                {
                    Utilerias.MostrarMensaje(mensajeError);
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "ProcesarContenidoArchivo", "");
            }
        }

        /// <summary>
        /// Evento para descargar la plantilla donde se capturan los folios de las notas que se les dará un folio ya que fue pagada con Tarjeta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDescargarPlantilla_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog _FBD = new FolderBrowserDialog())
            {
                if (_FBD.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(C.PathPlantillaCargarNotasTarjeta, _FBD.SelectedPath + "\\PlantillaCargarNotas.xlsx", true);
                }
                _FBD.Dispose();
            }
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                VentaNeg ventaNeg = new VentaNeg(us.Cadenaconn);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<RenglonExcel>));
                using (StringWriter textWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(textWriter, foliosnotas);
                    ventaNeg.ActualizarFolioTarjeta(textWriter.ToString());
                }

                foreach (RenglonExcel r in foliosnotas)
                {
                    byte[] bytesPDF = ventaNeg.CreatePDF(Int32.Parse(r.IdVenta), _conf.Empresa, _conf.DigitosFolio);
                    string fileName = "NOTA_" + r.FolioReal + ".pdf";
                    string filePath = C.PathTEMP + fileName;
                    Utilerias.Imprimir(bytesPDF, filePath);
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "btnImprimir_Click", "");
            }
        }

        private void gvNotas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                //try
                //{
                //    string folio = "";
                //    bool cancelaadministrador = false;
                //    VentaNeg ventaNeg = new VentaNeg();
                //    string nombreColumna = gvNotas.Columns[e.ColumnIndex].Name;
                //    folio = gvNotas["FolioReal", e.RowIndex].Value.ToString();
                //    string msjError = "";

                //    //if (nombreColumna == C.COLUMNA_ELIMINAR) //Eliminar cotización
                //    //{
                //    //    if (Utilerias.MostrarMensajeConfirmacion("¿Esta seguro de eliminar la cotización?"))
                //    //    {
                //    //        ventaNeg.EliminarCotizacion(Id);
                //    //        Utilerias.MostrarMensaje("La cotización ha sido eliminada.");
                //    //        CargarCorteTurno(this.corteTurno.IdCorteTurno);
                //    //    }
                //    //}
                //}
                //catch (Exception ex)
                //{
                //    Utilerias.MostrarMensaje(ex.Message);
                //    Utilerias.RegistrarLogError(ex, C_CLASE, "gv_CellContentClick", "");
                //}
            }
        }


        ///// <summary>
        ///// Método para actualizar la información del control donde se visualizan las notas cargadas desde excel
        ///// </summary>
        //public void AgregarEliminarGVNotas()
        //{
        //    try
        //    {
        //        if (gvNotas.Columns.Contains(C.COLUMNA_ELIMINAR)) gvNotas.Columns.RemoveAt(gvNotas.Columns.Count - 1);
        //        //if (gvNotas.Columns.Contains(C.COLUMNA_ELIMINAR)) gvNotas.Columns.RemoveAt(gvNotas.Columns.Count - 1);
        //        //Eliminar la columna eliminar
        //        if (foliosnotas.Count > 0)
        //        {
        //            gvNotas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        //            gvNotas.Columns["FolioReal"].Visible = true;

        //            DataGridViewImageColumn btnE = new DataGridViewImageColumn();
        //            btnE.MinimumWidth = 25;
        //            btnE.Image = Resources.eliminar25;
        //            btnE.Name = C.COLUMNA_ELIMINAR;
        //            btnE.ToolTipText = "Eliminar nota de venta";
        //            btnE.HeaderText = "";
        //            btnE.ImageLayout = DataGridViewImageCellLayout.Stretch;
        //            gvNotas.Columns.Add(btnE);

        //            //foreach (DataGridViewColumn item in gvLaboratorios.Columns)
        //            //    item.SortMode = DataGridViewColumnSortMode.NotSortable;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utilerias.RegistrarLogError(ex, C_CLASE, "ActualizarGVLaboratorios", "");
        //    }
        //}
    }
}
