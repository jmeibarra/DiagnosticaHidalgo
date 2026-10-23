using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Clases.Modelos;
using DiagnosticaHidalgo.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DiagnosticaHidalgo.Forms
{
    public partial class frmDevolucion : Form
    {
        #region Variables
        private ClienteNeg clienteNeg;
        private DoctorNeg doctorNeg;
        private VentaNeg ventaNeg;
        private bool bCargandoDatos;
        private int IdVenta;
        public int IdCliente;
        public int IdDoctor;
        private int renglonSel;
        private List<ItemAutocompletar> _listaClientes;
        private List<ItemAutocompletar> _listaDoctores;
        public static string C_CLASE = "frmDevolucion";
        /// <summary>
        /// Usuario que esta manejando el sistema
        /// </summary>
        public Usuario us = null;
        #endregion
        public frmDevolucion(Usuario usuario)
        {
            InitializeComponent();
            this.us = usuario;
            /*Cambiar los colores, tipografía, estilos*/
            gbInfPago.ForeColor = C.COLOR_SISTEMA;
            gbBusqueda.ForeColor = C.COLOR_SISTEMA;
            foreach (Control ctr in gbInfPago.Controls)
                ctr.ForeColor = C.COLOR_SISTEMA;
            foreach (Control ctr in gbBusqueda.Controls)
                ctr.ForeColor = C.COLOR_SISTEMA;

            btnBuscar.BackColor = C.COLOR_SISTEMA;
            btnBuscar.ForeColor = C.COLOR_SISTEMA_CLARO;
            btnPagar.BackColor = C.COLOR_SISTEMA;
            btnPagar.ForeColor = C.COLOR_SISTEMA_CLARO;

            gv.ColumnHeadersDefaultCellStyle.ForeColor = C.COLOR_SISTEMA;
            gv.ForeColor = C.COLOR_SISTEMA;
            gv.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
        }

        private void frmDevolucion_Load(object sender, EventArgs e)
        {
            ventaNeg = new VentaNeg(us.Cadenaconn);
            //Obtener todos los clientes
            clienteNeg = new ClienteNeg(us.Cadenaconn);
            txtNombreBusqueda.AutoCompleteCustomSource = LoadAutocomplete();
            txtNombreBusqueda.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtNombreBusqueda.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //Obtener todos los doctores
            doctorNeg = new DoctorNeg(us.Cadenaconn);
            txtDoctorBusqueda.AutoCompleteCustomSource = LoadAutocompleteDoctor();
            txtDoctorBusqueda.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtDoctorBusqueda.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }

        /// <summary>
        /// Evento al dar clic en el botón "BUSCAR".
        /// Busca todas las notas de venta por los filtros capturados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int IdCliente = checkCliente.Checked && this.IdCliente > 0 ? this.IdCliente : -1;
                int IdDoctor = checkDoctor.Checked && this.IdDoctor > 0 ? this.IdDoctor : -1;
                DateTime fechaI, fechaF;
                if (checkFechas.Checked)
                {
                    //fechaI = dtpFechaDesde.Value;
                    fechaI = new DateTime(dtpFechaDesde.Value.Year, dtpFechaDesde.Value.Month, dtpFechaDesde.Value.Day, 0, 0, 0);
                    fechaF = new DateTime(dtpFechaHasta.Value.Year, dtpFechaHasta.Value.Month, dtpFechaHasta.Value.Day, 23, 59, 59);
                }
                else
                {
                    fechaI = DateTime.MinValue;
                    fechaF = DateTime.MinValue;
                }

                gv.DataSource = null;
                gv.DataSource = ventaNeg.ObtenerParaDevolucion(IdCliente, IdDoctor, fechaI, fechaF);

                if (gv.Rows.Count > 0)
                {
                    gv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    gv.Columns["Id"].Visible = false;
                    gv.Columns["IdCliente"].Visible = false;
                    gv.Columns["IdDoctor"].Visible = false;
                    gv.Columns["TotalVenta"].DefaultCellStyle.Format = "C2";
                    gv.Columns["Descuento"].DefaultCellStyle.Format = "C2";
                }

                //Eliminar las columnas de seleccionar
                if (gv.Columns[0].Name != "Id")
                {
                    gv.Columns.RemoveAt(0);
                }

                DataGridViewCheckBoxColumn cCheck = new DataGridViewCheckBoxColumn();
                cCheck.Name = C.COLUMNA_SELECCION;
                gv.Columns.Add(cCheck);

                foreach (DataGridViewColumn item in gv.Columns)
                    item.SortMode = DataGridViewColumnSortMode.NotSortable;

                this.IdVenta = -1;
                this.renglonSel = -1;
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje("No se pudieron cargar las ventas");
                Utilerias.RegistrarLogError(ex, C_CLASE, "btnBuscar_Click", "");
            }

        }

        /// <summary>
        /// Evento al elegir buscar por cliente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkCliente_CheckedChanged(object sender, EventArgs e)
        {
            //cmbClientes_TextChanged(cmbClientes, null);
            txtNombreBusqueda.Text = "";
            txtNombreBusqueda.Enabled = checkCliente.Checked;
            this.IdCliente = -1;

        }

        /// <summary>
        /// Evento al elegir buscar por doctor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkDoctor_CheckedChanged(object sender, EventArgs e)
        {
            txtDoctorBusqueda.Text = "";
            txtDoctorBusqueda.Enabled = checkDoctor.Checked;
            this.IdDoctor = -1;
        }

        /// <summary>
        /// Evento al elegir buscar entre dos fechas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkFechas_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaDesde.Enabled = dtpFechaHasta.Enabled = checkFechas.Enabled;
        }

        /// <summary>
        /// Evento al dar clic en alguna celda del gridview que contiene las ventas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string nombreColumna = gv.Columns[e.ColumnIndex].Name;
                if (nombreColumna == C.COLUMNA_SELECCION) //Selección de venta para devolución
                {
                    //Quitar selección si hay uno
                    if (renglonSel > -1) gv[e.ColumnIndex, renglonSel].Value = false;

                    bool selecionado = Convert.ToBoolean(gv[e.ColumnIndex, e.RowIndex].Value);

                    if (selecionado == false) //Devolución en esa venta - Apenas se va a seleccionar
                    {
                        renglonSel = e.RowIndex;
                        this.IdVenta = Convert.ToInt32(gv.Rows[e.RowIndex].Cells[0].Value);
                        gv[e.ColumnIndex, e.RowIndex].Value = true;
                    }
                    else //Quitar selección del renglón
                    {
                        this.IdVenta = -1;
                        renglonSel = -1;
                    }
                }
            }
        }

        /// <summary>
        /// Evento para agregar la devolución
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPagar_Click(object sender, EventArgs e)
        {
            string msjError = ValidarCampos();
            if (string.IsNullOrEmpty(msjError))
            {
                try
                {
                    //Preguntar si se agrega un descuento por el monto de la devolución
                    //string mensaje = "¿Desea agregar un descuento a la venta por el mismo monto de la devolución? " +
                    //    "Esta sugerencia es para que la venta quede como Vigente(liquidada) y no como Pendiente.";
                    //bool bAgregarDesc = Utilerias.MostrarMensajeConfirmacion(mensaje);

                    PagoNeg oPagoNeg = new PagoNeg(us.Cadenaconn);
                    string tipoPago = Utilerias.ObtenerTipoPagoParaBD(cmbTipoPago.Text);

                    Pago pago = new Pago();
                    pago.IdVenta = this.IdVenta;
                    pago.TipoPago = tipoPago;
                    //pago.NumPago = oPagoNeg.ObtenerSigNumPago(this.IdVenta);
                    pago.NumPago = -1;
                    double cantidad = Convert.ToDouble(txtCantidad.Text.Trim()) * -1;
                    pago.Cantidad = Convert.ToDecimal(cantidad);
                    pago.Devolucion = true;

                    if (oPagoNeg.ArgegarDevolucion(pago, ref msjError))
                        Utilerias.MostrarMensaje("Devolución creada correctamente");
                    else Utilerias.MostrarMensaje("No se pudo hacer la devolución");

                    this.Close();
                }
                catch (Exception ex)
                {
                    Utilerias.MostrarMensaje(ex.Message);
                    Utilerias.RegistrarLogError(ex, C_CLASE, "btnPagar_Click", "");
                }
            }
            else Utilerias.MostrarMensaje(msjError);
        }


        /// <summary>
        /// Función para válidar que todos los campos del pago sean correctos.
        /// </summary>
        /// <returns></returns>
        public string ValidarCampos()
        {
            if (cmbTipoPago.SelectedItem == null)
                return "Devolución con es requerido.";
            if (string.IsNullOrEmpty(txtCantidad.Text.Trim()))
                return "Cantidad es requerido.";
            decimal cdd = Convert.ToDecimal(txtCantidad.Text.Trim());
            if (!(cdd > 0))
                return "Cantidad debe ser mayor a 0.";
            if (this.IdVenta == -1)
                return "Debe seleccionar una venta.";

            return "";
        }
        public AutoCompleteStringCollection LoadAutocompleteDoctor()
        {
            //Instanciamos el objeto que se usara para el AutoComplete
            AutoCompleteStringCollection stringdata = new AutoCompleteStringCollection();
            _listaDoctores = doctorNeg.ObtenerTodosAutocompletar();
            foreach (ItemAutocompletar item in _listaDoctores) stringdata.Add(item.Nombre);
            //Devolvemos el AutoCompleteStringCollection con los datos cargados
            return stringdata;
        }

        public AutoCompleteStringCollection LoadAutocomplete()
        {
            //Instanciamos el objeto que se usara para el AutoComplete
            AutoCompleteStringCollection stringdata = new AutoCompleteStringCollection();
            _listaClientes = clienteNeg.ObtenerTodosAutocompletar();
            foreach (ItemAutocompletar item in _listaClientes) stringdata.Add(item.Nombre);
            //Devolvemos el AutoCompleteStringCollection con los datos cargados
            return stringdata;
        }

        private void txtNombreBusqueda_TextChanged(object sender, EventArgs e)
        {
            ItemAutocompletar item = _listaClientes.FirstOrDefault(x => x.Nombre == txtNombreBusqueda.Text);
            if (item == null)
            {
                this.IdCliente = -1;
            }
            else
            {
                this.IdCliente = item.Id;
            }
        }

        private void txtDoctorBusqueda_TextChanged(object sender, EventArgs e)
        {
            ItemAutocompletar item = _listaDoctores.FirstOrDefault(x => x.Nombre == txtDoctorBusqueda.Text);
            if (item == null)
            {
                this.IdDoctor = -1;
            }
            else
            {
                this.IdDoctor = item.Id;
            }

        }
    }
}
