using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DiagnosticaHidalgo.Forms
{
    public partial class frmPagoCliente : Form
    {
        private static string COLUMNA_RESTO = "Resto";
        private static string COLUMNA_FOLIO = "Folio";

        private ClienteNeg oClienteNeg;
        private object ventas;
        private bool bCargandoClientes;
        public int IdCliente;
        public double ctdPago;
        public double ctdPagoDisponible;
        public double ctdSumadaPagoPorLab;
        public double ctdPagoParcial;
        public int indVtaPagoParcial;
        /// <summary>
        /// Usuario que esta manejando el sistema
        /// </summary>
        public Usuario us = null;

        /// <summary>
        /// Constructor del formulario.
        /// </summary>
        public frmPagoCliente(Usuario usuario)
        {
            InitializeComponent();
            this.us=usuario;
            oClienteNeg = new ClienteNeg(this.us.Cadenaconn);
        }

        /// <summary>
        /// Método de carga del formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPagoCliente_Load(object sender, EventArgs e)
        {
            /*Cambiar estilos*/
            btnPagar.BackColor = C.COLOR_SISTEMA;
            btnPagar.ForeColor = C.COLOR_SISTEMA_CLARO;

            gbBusqueda.ForeColor = C.COLOR_SISTEMA;
            foreach (Control ctr in gbBusqueda.Controls)
                ctr.ForeColor = C.COLOR_SISTEMA;
            gbInfPago.ForeColor = C.COLOR_SISTEMA;
            foreach (Control ctr in gbInfPago.Controls)
                ctr.ForeColor = C.COLOR_SISTEMA;

            gv.ColumnHeadersDefaultCellStyle.ForeColor = C.COLOR_SISTEMA;
            gv.ForeColor = C.COLOR_SISTEMA;
            gv.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            /*Termina cambiar estilos*/

            this.ctdSumadaPagoPorLab = this.ctdPago = this.ctdPagoDisponible = 0;
            this.bCargandoClientes = true;
            object lClientes = oClienteNeg.ObtenerTodos();
            cmbBusqueda.DataSource = lClientes;
            cmbBusqueda.DisplayMember = "Nombre";
            cmbBusqueda.ValueMember = "IdCliente";
            cmbBusqueda.SelectedItem = null;
            cmbBusqueda.Text = "";
            cmbBusqueda.SelectionStart = cmbBusqueda.Text.Length;
            this.bCargandoClientes = false;
        }

        /// <summary>
        ///  Evento al seleccionar un elemento del combo de búsqueda.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bCargandoClientes)
            {
                this.IdCliente = (int)cmbBusqueda.SelectedValue;
                if (this.IdCliente > 0)
                {
                    double totalDeuda = 0;
                    //Cargar las ventas del cliente
                    VentaNeg ventaNeg = new VentaNeg(us.Cadenaconn);
                    this.ventas = ventaNeg.ObtenerPendientePorIdCliente(this.IdCliente, ref totalDeuda);
                    gv.DataSource = null;
                    gv.DataSource = this.ventas;
                    gv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    gv.Columns["Id"].Visible = false;

                    //Eliminar las columnas de seleccionar
                    if (gv.Columns[0].Name != "Id")
                    {
                        gv.Columns.RemoveAt(0);
                    }

                    gv.Columns["Resto"].DefaultCellStyle.Format = "C2";
                    gv.Columns["Resto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                    gv.Columns["TotalVenta"].DefaultCellStyle.Format = "C2";
                    gv.Columns["TotalVenta"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                    gv.Columns["Descuento"].DefaultCellStyle.Format = "C2";
                    gv.Columns["Descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

                    DataGridViewCheckBoxColumn btnE = new DataGridViewCheckBoxColumn();
                    btnE.Name = C.COLUMNA_SELECCION;
                    gv.Columns.Add(btnE);

                    foreach (DataGridViewColumn item in gv.Columns)
                        item.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                //else Utilerias.LimpiarControles(this.gbDatos);
            }
        }

        private void gv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string nombreColumna = gv.Columns[e.ColumnIndex].Name;
                if (nombreColumna == C.COLUMNA_SELECCION) //Selección de venta para pagar
                {
                    bool selecionado = Convert.ToBoolean(gv[e.ColumnIndex, e.RowIndex].Value);
                    double restoLab = Convert.ToDouble(gv.Rows[e.RowIndex].Cells[COLUMNA_RESTO].Value);

                    if (selecionado == false) //Pagar
                    {
                        if (this.ctdPagoDisponible >= restoLab)
                        {
                            this.ctdSumadaPagoPorLab = this.ctdSumadaPagoPorLab + restoLab;
                            this.ctdPagoDisponible = this.ctdPago - this.ctdSumadaPagoPorLab;
                            gv[e.ColumnIndex, e.RowIndex].Value = true;
                        }
                        else
                        {
                            string folio = gv.Rows[e.RowIndex].Cells[COLUMNA_FOLIO].Value.ToString();
                            if (Utilerias.MostrarMensajeConfirmacion("¿Desea agregar un pago por $" + this.ctdPagoDisponible.ToString().PadRight(2, '0') + " a la venta del folio número " + folio + " ?"))
                            {
                                lblPagoParcial.Text = "Se agregará un pago por &" + Utilerias.ObtenerConDosDecimales(this.ctdPagoDisponible) + "\r\na la venta del folio " + folio;
                                this.lblPagoParcial.Visible = true;
                                this.ctdPagoParcial = this.ctdPagoDisponible;
                                this.ctdSumadaPagoPorLab = ctdSumadaPagoPorLab + this.ctdPagoDisponible;
                                this.ctdPagoDisponible = this.ctdPago - this.ctdSumadaPagoPorLab;
                                this.indVtaPagoParcial = e.RowIndex;
                                gv[e.ColumnIndex, e.RowIndex].Value = true;
                            }
                        }
                        //}
                    }
                    else //No pagar
                    {
                        if (e.RowIndex == this.indVtaPagoParcial)
                        {
                            this.lblPagoParcial.Visible = false;
                            this.indVtaPagoParcial = -1;
                            ctdSumadaPagoPorLab = ctdSumadaPagoPorLab - this.ctdPagoParcial;
                            this.ctdPagoParcial = 0;
                        }
                        else
                        {
                            ctdSumadaPagoPorLab = ctdSumadaPagoPorLab - restoLab;
                        }
                        this.ctdPagoDisponible = this.ctdPago - this.ctdSumadaPagoPorLab;
                        gv[e.ColumnIndex, e.RowIndex].Value = false;
                    }

                    this.lblMontoGastado.Text = this.ctdSumadaPagoPorLab.ToString();
                    this.lblRestoPago.Text = this.ctdPagoDisponible.ToString();
                }
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (txtCantidad.TextLength > 0)
            {
                //Recalcular el resto del pago
                this.ctdPago = Convert.ToDouble(txtCantidad.Text);
                if (this.ctdSumadaPagoPorLab > 0)
                {
                    //Quitar selección de todos los pagos
                    foreach (DataGridViewRow item in gv.Rows)
                        item.Cells[C.COLUMNA_SELECCION].Value = false;
                    this.lblPagoParcial.Visible = false;
                }
                this.ctdPagoDisponible = this.ctdPago;
                this.ctdPagoParcial = this.ctdSumadaPagoPorLab = 0;
                this.indVtaPagoParcial = -1;

                this.lblMontoGastado.Text = "0.00";
                this.lblRestoPago.Text = "0.00";
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)13)
            //{
            //    //Recalcular el importe
            //    CalcularImporte();
            //}
            //else 
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            string msjError = "";
            try
            {
                int IdVtaPagoParcial = -1;
                List<Pago> lPago = new List<Pago>();
                PagoNeg oPagoNeg = new PagoNeg(us.Cadenaconn);
                VentaNeg oVentaNeg = new VentaNeg(us.Cadenaconn);

                string tipoPago = Utilerias.ObtenerTipoPagoParaBD(cmbTipoPago.Text);
                foreach (DataGridViewRow item in gv.Rows)
                {
                    bool selecionado = Convert.ToBoolean(item.Cells[C.COLUMNA_SELECCION].Value);
                    double restoLab = Convert.ToDouble(item.Cells[COLUMNA_RESTO].Value);
                    if (selecionado) //Si esta seleccionado
                    {
                        int IdVta = (int)item.Cells[0].Value;
                        Pago oPago = new Pago();
                        //oPago.IdCorteTurno = Se agrega en el negocio 
                        oPago.IdVenta = IdVta;
                        oPago.TipoPago = tipoPago;
                        oPago.NumPago = oPagoNeg.ObtenerSigNumPago(IdVta);
                        oPago.Devolucion = false;

                        if (this.indVtaPagoParcial == item.Index)
                        {
                            IdVtaPagoParcial = IdVta;
                            oPago.Cantidad = Convert.ToDecimal(this.ctdPagoParcial);
                        }
                        else
                        {
                            oPago.Cantidad = Convert.ToDecimal(restoLab);
                        }

                        lPago.Add(oPago);
                    }
                }

                if (lPago.Count > 0)
                {
                    oVentaNeg.Pagar(lPago, IdVtaPagoParcial, ref msjError);
                }

                if (!string.IsNullOrEmpty(msjError)) Utilerias.MostrarMensaje(msjError);
                else
                {
                    Utilerias.MostrarMensaje("Pago creado correctamente");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje(ex.Message);
            }
        }
    }
}
