using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Clases.Modelos;
using DiagnosticaHidalgo.Negocio;
using DiagnosticaHidalgo.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DiagnosticaHidalgo.Forms
{
    public partial class frmCorteTurno : Form
    {
        public List<Venta> lVentas = new List<Venta>();
        public CorteTurno corteTurno;
        public int IdVenta = -1;
        CorteTurnoNeg corteTurnoNeg;
        public bool bverInformacionCorte;
        public bool bOpcionesVenta;
        public bool cotizaciones;
        private static string C_CLASE = "frmCorteTurno";
        private Configuraciones config = null;
        private List<ItemAutocompletar> _listaClientes;
        private List<ItemAutocompletar> _listaDoctores;
        private List<ItemAutocompletar> _listaEstudios;
        private string cadconn;
        VentaNeg ventaNeg;
        PagoNeg pagoNeg;

        /// <summary>
        /// Método de creación de objeto.
        /// </summary>
        /// <param name="IdCorteTurno">Identificador del turno</param>
        /// <param name="verInformacionCorte">Bandera para mostrar el reporte del corte de turno (Totales).</param>
        /// <param name="cotizaciones">Bandera que indica si se van a mostrar notas de venta o cotizaciones</param>
        public frmCorteTurno(string cadenaconn, int IdCorteTurno, bool verInformacionCorte, bool opcionesVenta, bool cotizaciones, Configuraciones config)
        {
            InitializeComponent();
            this.cadconn = cadenaconn;

            /*Cambiar estilos*/
            foreach (Control gb in this.Controls)
            {

                if (gb is GroupBox || gb is Panel)
                {
                    gb.ForeColor = C.COLOR_SISTEMA;
                    foreach (Control ctrl in gb.Controls)
                    {
                        if (ctrl is Button)
                        {
                            ctrl.BackColor = C.COLOR_SISTEMA;
                            ctrl.ForeColor = C.COLOR_SISTEMA_CLARO;
                        }
                        else ctrl.ForeColor = C.COLOR_SISTEMA;
                    }
                }
                else if (gb is DataGridView)
                {
                    DataGridView gv = (DataGridView)gb;
                    gv.ColumnHeadersDefaultCellStyle.ForeColor = C.COLOR_SISTEMA;
                    gv.ForeColor = C.COLOR_SISTEMA;
                    gv.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
                else if (gb is Button)
                {
                    Button btn = (Button)gb;
                    btn.BackColor = C.COLOR_SISTEMA;
                    btn.ForeColor = C.COLOR_SISTEMA_CLARO;
                }
            }

            tt.SetToolTip(this.btnVerPagos, "Ver los pagos que fueron hechos durante este corte de turno. \r\n También se podrán ver los pagos de notas que fueron hechas en otro turno.");

            corteTurnoNeg = new CorteTurnoNeg(cadenaconn);
            ventaNeg = new VentaNeg(cadenaconn);
            pagoNeg = new PagoNeg(cadenaconn);
            this.IdVenta = -1;
            this.bOpcionesVenta = opcionesVenta;
            this.bverInformacionCorte = verInformacionCorte;
            this.btnVerOtrosPagos.Visible = false;
            this.config = config;
            this.Text = "CORTE DE TURNO";
            this.cotizaciones = cotizaciones;
            if (verInformacionCorte)
            {
                gb.Visible = true;
                btnVerPagos.Visible = true;
                this.Size = new Size(872, 589);
            }
            else
            {
                gb.Visible = false;
                pnlFiltros.Visible = true;
                btnVerPagos.Visible = false;
                btnImprimir.Visible = false;
                gv.Location = new Point(10, 150);
                this.Size = new Size(872, 490);
                this.Text = "NOTAS DE VENTAS";

                //Cargar los clientes, los doctores y los estudios para autocompletar
                txtNombreBusqueda.AutoCompleteCustomSource = LoadAutocompleteClientes();
                txtNombreBusqueda.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtNombreBusqueda.AutoCompleteSource = AutoCompleteSource.CustomSource;

                txtEstudioBusqueda.AutoCompleteCustomSource = LoadAutocompleteEstudios();
                txtEstudioBusqueda.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtEstudioBusqueda.AutoCompleteSource = AutoCompleteSource.CustomSource;

                if (cotizaciones)
                {
                    txtDoctorBusqueda.Visible = false;
                    lblDoctor.Visible = false;
                }
                else
                {
                    txtDoctorBusqueda.AutoCompleteCustomSource = LoadAutocompleteDoctores();
                    txtDoctorBusqueda.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtDoctorBusqueda.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
            }
            CargarCorteTurno(IdCorteTurno);
        }

        #region Funciones Auxiliares
        /// <summary>
        /// Método para cargar los datos del objeto CorteTurno en el formulario.
        /// </summary>
        public void CargarCorteTurno(int IdCorteTurno)
        {
            try
            {
                if (bOpcionesVenta)
                {
                    if (!gv.Columns.Contains(C.COLUMNA_IMPRIMIR))
                    {/*Imprimir notas*/
                        DataGridViewButtonColumn btnImprimir = new DataGridViewButtonColumn();
                        btnImprimir.Text = C.COLUMNA_IMPRIMIR;
                        btnImprimir.Name = C.COLUMNA_IMPRIMIR;
                        btnImprimir.UseColumnTextForButtonValue = true;
                        gv.Columns.Add(btnImprimir);
                    }

                    if (cotizaciones)
                    {
                        if (!gv.Columns.Contains(C.COLUMNA_CREAR_NOTA))
                        {
                            //Agregar columna para cancelar nota
                            DataGridViewImageColumn btnCN = new DataGridViewImageColumn();
                            btnCN.MinimumWidth = 25;
                            btnCN.Image = Resources.cargarNota;
                            btnCN.Name = C.COLUMNA_CREAR_NOTA;
                            btnCN.ToolTipText = "Crear nota a partir de la cotización";
                            btnCN.HeaderText = "";
                            btnCN.ImageLayout = DataGridViewImageCellLayout.Stretch;
                            btnCN.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            gv.Columns.Add(btnCN);
                        }
                        if (!gv.Columns.Contains(C.COLUMNA_ELIMINAR))
                        {
                            //Agregar columna para eliminar cotización
                            DataGridViewImageColumn btnE = new DataGridViewImageColumn();
                            btnE.MinimumWidth = 25;
                            btnE.Image = Resources.eliminar25;
                            btnE.Name = C.COLUMNA_ELIMINAR;
                            btnE.ToolTipText = "Eliminar cotización";
                            btnE.HeaderText = "";
                            btnE.ImageLayout = DataGridViewImageCellLayout.Stretch;
                            btnE.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            gv.Columns.Add(btnE);
                        }
                    }
                    else
                    {
                        if (!gv.Columns.Contains(C.COLUMNA_CANCELAR))
                        {
                            //Agregar columna para cancelar nota
                            DataGridViewImageColumn btnC = new DataGridViewImageColumn();
                            btnC.MinimumWidth = 25;
                            btnC.Image = Resources.cancelar;
                            btnC.Name = C.COLUMNA_CANCELAR;
                            btnC.ToolTipText = "Cancelar nota de venta";
                            btnC.HeaderText = "";
                            btnC.ImageLayout = DataGridViewImageCellLayout.Stretch;
                            btnC.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            gv.Columns.Add(btnC);
                        }
                        if (!gv.Columns.Contains(C.COLUMNA_VIGENTE))
                        {
                            //Agregar columna para cancelar nota
                            DataGridViewImageColumn btnV = new DataGridViewImageColumn();
                            btnV.MinimumWidth = 25;
                            btnV.Image = Image.FromFile(C.PathImages + "vigente.png");
                            btnV.Name = C.COLUMNA_VIGENTE;
                            btnV.ToolTipText = "Cambiar nota de venta a vigente";
                            btnV.HeaderText = "";
                            btnV.ImageLayout = DataGridViewImageCellLayout.Stretch;
                            btnV.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            gv.Columns.Add(btnV);
                        }
                    }

                    if (!gv.Columns.Contains(C.COLUMNA_ESTUDIOS))
                    {
                        //Botón para ver los laboratorios de cada venta
                        DataGridViewImageColumn btnEst = new DataGridViewImageColumn();
                        btnEst.MinimumWidth = 25;
                        btnEst.Image = Resources.icoEstudios;
                        btnEst.Name = C.COLUMNA_ESTUDIOS;
                        btnEst.ToolTipText = "Estudios de nota de venta";
                        btnEst.HeaderText = "";
                        btnEst.ImageLayout = DataGridViewImageCellLayout.Stretch;
                        btnEst.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        gv.Columns.Add(btnEst);
                    }
                }

                this.corteTurno = corteTurnoNeg.ObtenerPorId(IdCorteTurno);
                if (bverInformacionCorte)
                {
                    lblFecha.Text = corteTurno.FechaCreacion.ToString();
                    lblTCtas.Text = Utilerias.ObtenerConDosDecimales(Convert.ToDouble(corteTurno.TotalCtas.Value));
                    lblTEfectivo.Text = Utilerias.ObtenerConDosDecimales(Convert.ToDouble(corteTurno.TotalEfectivo));
                    lblTotal.Text = Utilerias.ObtenerConDosDecimales(Convert.ToDouble(corteTurno.MontoTotal));

                    //Monto por cuenta
                    gvCtasMontos.DataSource = pagoNeg.ObtenerTotalPorCuenta(corteTurno.IdCorteTurno);
                    gvCtasMontos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    gvCtasMontos.Columns["Cantidad"].DefaultCellStyle.Format = "C2";
                    gvCtasMontos.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

                    foreach (DataGridViewColumn item in gvCtasMontos.Columns)
                        item.SortMode = DataGridViewColumnSortMode.NotSortable;

                    gv.DataSource = ventaNeg.ObtenerPorIdCorteTurno(corteTurno.IdCorteTurno);
                }
                else
                {
                    int idCliente = -1, idEstudio = -1, idDoctor = -1;

                    if (!string.IsNullOrEmpty(txtNombreBusqueda.Text.Trim()))
                    {
                        ItemAutocompletar oCliente = _listaClientes.FirstOrDefault(x => x.Nombre == txtNombreBusqueda.Text.Trim());
                        idCliente = oCliente != null ? oCliente.Id : -1;
                    }
                    if (!string.IsNullOrEmpty(txtDoctorBusqueda.Text.Trim()))
                    {
                        ItemAutocompletar oDoctor = _listaDoctores.FirstOrDefault(x => x.Nombre == txtDoctorBusqueda.Text.Trim());
                        idDoctor = oDoctor != null ? oDoctor.Id : -1;
                    }
                    if (!string.IsNullOrEmpty(txtEstudioBusqueda.Text.Trim()))
                    {
                        ItemAutocompletar oEstudio = _listaEstudios.FirstOrDefault(x => x.Nombre == txtEstudioBusqueda.Text.Trim());
                        idEstudio = oEstudio != null ? oEstudio.Id : -1;
                    }
                    gv.DataSource = null;

                    gv.DataSource = cotizaciones ? ventaNeg.ObtenerCotizacionesPorFiltros(idCliente, idEstudio) : ventaNeg.ObtenerPorFiltros(idCliente, idEstudio, idDoctor);
                }
                //gv.DataSource = bverInformacionCorte == true ? ventaNeg.ObtenerPorIdCorteTurno(corteTurno.IdCorteTurno) : ventaNeg.ObtenerTodas();

                //Ventas
                gv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                gv.Columns["Id"].Visible = false;
                gv.Columns["TotalVenta"].DefaultCellStyle.Format = "C2";
                gv.Columns["TotalVenta"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                if (!cotizaciones)
                {
                    gv.Columns["Pagado"].DefaultCellStyle.Format = "C2";
                    gv.Columns["Pagado"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                }
                //gv.Columns["Maquina"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                foreach (DataGridViewColumn item in gv.Columns)
                    item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "CargarCorteTurno", "");
                Utilerias.MostrarMensaje(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// Evento al dar clic en el gridView de las ventas.
        /// Para cancelar una nota de venta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                try
                {
                    int Id = 0;
                    bool cancelaadministrador = false;
                    string nombreColumna = gv.Columns[e.ColumnIndex].Name;
                    Id = (int)gv["Id", e.RowIndex].Value;
                    string msjError = "";

                    if (nombreColumna == C.COLUMNA_CANCELAR || nombreColumna == C.COLUMNA_VIGENTE) //Cancelar una nota
                    {
                        //Validar si la nota de venta esta cancelada
                        string estatus = gv["Estatus", e.RowIndex].Value.ToString();
                        if ((nombreColumna == C.COLUMNA_CANCELAR && estatus == "CANCELADA")
                            || (nombreColumna == C.COLUMNA_VIGENTE && estatus == "VIGENTE"))
                            return;

                        msjError = (estatus == "CANCELADA") ? "¿Esta seguro de cambiar a VIGENTE la nota de venta?" : "¿Esta seguro de cancelar la nota de venta?";
                        if (Utilerias.MostrarMensajeConfirmacion(msjError))
                        {
                            msjError = "";
                            estatus = (estatus == "CANCELADA") ? "V" : "C";

                            if (ventaNeg.Cancelar(Id, false, estatus, ref cancelaadministrador, ref msjError))
                            {
                                msjError = estatus == "V" ? "cambiada como vigente" : "cancelada";
                                Utilerias.MostrarMensaje("La nota de venta ha sido " + msjError + ".");
                                CargarCorteTurno(this.corteTurno.IdCorteTurno);
                            }
                            else
                            {
                                if (cancelaadministrador)
                                {
                                    using (frmLogin f = new frmLogin(true, msjError))
                                    {
                                        if (f.ShowDialog() == DialogResult.OK)
                                        {
                                            msjError = "";
                                            if (ventaNeg.Cancelar(Id, true, estatus, ref cancelaadministrador, ref msjError))
                                            {
                                                msjError = estatus == "V" ? "cambiada como vigente" : "cancelada";
                                                Utilerias.MostrarMensaje("La nota de venta ha sido " + msjError + ".");
                                                CargarCorteTurno(this.corteTurno.IdCorteTurno);
                                            }
                                            else
                                            {
                                                Utilerias.MostrarMensaje(!string.IsNullOrEmpty(msjError) ? msjError : "¡Ocurrió un error al cambiar de estatus la nota de venta!");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Utilerias.MostrarMensaje(!string.IsNullOrEmpty(msjError) ? msjError : "¡Ocurrió un error al cambiar de estatus la nota de venta!");
                                }
                            }
                        }
                    }
                    else if (nombreColumna == C.COLUMNA_IMPRIMIR) //Imprimir una nota
                    {
                        byte[] bytes = ventaNeg.CreatePDF(Id, config.Empresa, cotizaciones ? -1 : config.DigitosFolio);
                        string fileName = "NOTA_" + DateTime.Now.ToString("ddMMMMyyyy_HHmmss") + ".pdf";
                        string filePath = C.PathTEMP + fileName;
                        Utilerias.Imprimir(bytes, filePath);
                        Utilerias.MostrarMensaje("La nota se imprimio con éxito.");
                        this.Close();
                    }
                    else if (nombreColumna == C.COLUMNA_ESTUDIOS) //Ver laboratorios de una venta
                    {
                        using (frmConsultarLaboratorios frm = new frmConsultarLaboratorios(this.cadconn, false, Id))
                        {
                            frm.ShowDialog();
                        }
                    }
                    else if (nombreColumna == C.COLUMNA_CREAR_NOTA) //Cargar nota de venta a partir de una cotización
                    {
                        this.IdVenta = Id;
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else if (nombreColumna == C.COLUMNA_ELIMINAR) //Eliminar cotización
                    {
                        if (Utilerias.MostrarMensajeConfirmacion("¿Esta seguro de eliminar la cotización?"))
                        {
                            ventaNeg.EliminarCotizacion(Id);
                            Utilerias.MostrarMensaje("La cotización ha sido eliminada.");
                            CargarCorteTurno(this.corteTurno.IdCorteTurno);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utilerias.MostrarMensaje(ex.Message);
                    Utilerias.RegistrarLogError(ex, C_CLASE, "gv_CellContentClick", "");
                }
            }
        }

        /// <summary>
        /// Evento para mostrar los pagos y devoluciones del corte del día
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVerPagos_Click_(object sender, EventArgs e)
        {
            using (frmPago f = new frmPago(this.cadconn, false, 0, 0, 0, corteTurno.IdCorteTurno))
            {
                f.ShowDialog();
            }
        }

        #region Codigo que no se usa
        /// <summary>
        /// Botón para ver los pagos que se hicieron en el corte de turno pero 
        /// no pertenecen a alguna venta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVerOtrosPagos_Click(object sender, EventArgs e)
        {
            using (frmPago f = new frmPago(this.cadconn, false, 0, 0, 0, corteTurno.IdCorteTurno))
            {
                f.ShowDialog();
            }
        }
        #endregion

        /// <summary>
        /// Evento que se dispará al llenar un renglón.
        /// El evento se agrego para pintar cada renglon, dependiendo del estatus de la venta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!bverInformacionCorte && !cotizaciones)
            {
                DataGridViewRow fila = ((DataGridView)sender).Rows[e.RowIndex];
                switch (fila.Cells["Estatus"].Value.ToString())
                {
                    case "PENDIENTE":
                        fila.DefaultCellStyle.BackColor = Color.Yellow;
                        break;
                    case "CANCELADA":
                        fila.DefaultCellStyle.BackColor = Color.Red;
                        break;
                    default:
                        fila.DefaultCellStyle.BackColor = Color.Empty;
                        break;
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(fbd.SelectedPath))
            {
                try
                {
                    string directorio = fbd.SelectedPath;
                    string nombrearchexcel = "ReporteDetallado" + DateTime.Now.ToString("ddMMMMyyyy_HHmmss") + ".xlsx";
                    string filePathexcel = Path.Combine(directorio, nombrearchexcel);
                    corteTurnoNeg.CrearReporteDetalladoPorCorteTurno(this.corteTurno.IdCorteTurno, filePathexcel);
                    byte[] arch = corteTurnoNeg.CrearPDF(config.Empresa, lblFecha.Text, lblTCtas.Text, lblTEfectivo.Text, lblTotal.Text, this.corteTurno.IdCorteTurno);
                    string fileName = "CORTE_TURNO_" + DateTime.Now.ToString("ddMMMMyyyy_HHmmss") + ".pdf";
                    string filePath = Path.Combine(directorio, fileName);

                    Utilerias.Imprimir(arch, filePath);
                }
                catch (Exception ex)
                {
                    Utilerias.MostrarMensaje("Ocurrió un error al intentar guardar el corte de turno.");
                    Utilerias.RegistrarLogError(ex, C_CLASE, "btnImprimir_Click", "");
                }

            }


        }

        public AutoCompleteStringCollection LoadAutocompleteClientes()
        {
            //Instanciamos el objeto que se usara para el AutoComplete
            AutoCompleteStringCollection stringdata = new AutoCompleteStringCollection();
            ClienteNeg clienteNeg = new ClienteNeg(this.cadconn);
            _listaClientes = clienteNeg.ObtenerTodosAutocompletar();
            foreach (ItemAutocompletar item in _listaClientes) stringdata.Add(item.Nombre);
            //Devolvemos el AutoCompleteStringCollection con los datos cargados
            return stringdata;
        }

        public AutoCompleteStringCollection LoadAutocompleteDoctores()
        {
            //Instanciamos el objeto que se usara para el AutoComplete
            AutoCompleteStringCollection stringdata = new AutoCompleteStringCollection();
            DoctorNeg doctorNeg = new DoctorNeg(this.cadconn);
            _listaDoctores = doctorNeg.ObtenerTodosAutocompletar();
            foreach (ItemAutocompletar item in _listaDoctores) stringdata.Add(item.Nombre);
            //Devolvemos el AutoCompleteStringCollection con los datos cargados
            return stringdata;
        }

        public AutoCompleteStringCollection LoadAutocompleteEstudios()
        {
            //Instanciamos el objeto que se usara para el AutoComplete
            AutoCompleteStringCollection stringdata = new AutoCompleteStringCollection();
            LaboratorioNeg laboratorioNeg = new LaboratorioNeg(this.cadconn);
            _listaEstudios = laboratorioNeg.ObtenerTodosAutocompletar();
            foreach (ItemAutocompletar item in _listaEstudios) stringdata.Add(item.Nombre);
            //Devolvemos el AutoCompleteStringCollection con los datos cargados
            return stringdata;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarCorteTurno(-1);

        }
    }
}
