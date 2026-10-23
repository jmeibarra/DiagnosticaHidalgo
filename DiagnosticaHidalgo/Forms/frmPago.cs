using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Negocio;
using System;
using System.CodeDom;
using System.Drawing;
using System.Windows.Forms;

namespace DiagnosticaHidalgo.Forms
{
    public partial class frmPago : Form
    {
        #region Variables
        public Pago pago;
        private bool bDevolucion;
        private decimal montoTotalVenta;
        private decimal montoPendiente;
        private int numPago;
        private int IdCorteTurno;
        private string C_CLASE = "frmPago";
        #endregion
        public frmPago(string cadconn,bool esDevolucion, decimal montoTotalVenta, decimal montoPendiente, int numPago, int IdCorteTurno)
        {
            InitializeComponent();
            /*Cambiar estilos*/
            gbInfPago.ForeColor = C.COLOR_SISTEMA;
            foreach (Control ctr in gbInfPago.Controls)
                ctr.ForeColor = C.COLOR_SISTEMA;
            gbMonto.ForeColor = C.COLOR_SISTEMA;
            foreach (Control ctr in gbMonto.Controls)
                ctr.ForeColor = C.COLOR_SISTEMA;

            btnAgregar.BackColor = C.COLOR_SISTEMA;
            btnAgregar.ForeColor = C.COLOR_SISTEMA_CLARO;
            btnCancelar.BackColor = C.COLOR_SISTEMA;
            btnCancelar.ForeColor = C.COLOR_SISTEMA_CLARO;

            gvPagos.ColumnHeadersDefaultCellStyle.ForeColor = C.COLOR_SISTEMA;
            gvPagos.ForeColor = C.COLOR_SISTEMA;
            gvPagos.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            this.IdCorteTurno = IdCorteTurno;

            try
            {
                foreach (DataGridViewColumn col in gvPagos.Columns) {
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;
                }


                if (numPago == 1) //Es el primer pago de la nota de venta
                {
                    gvPagos.Visible = false;
                    gbPago.Visible = true;
                    this.bDevolucion = esDevolucion;
                    this.montoTotalVenta = montoTotalVenta;
                    this.montoPendiente = montoPendiente;
                    this.numPago = numPago;
                    this.cmbTipoPago.SelectedIndex = 0;
                    this.txtCantidad.Text = montoTotalVenta.ToString();
                    this.Size = new Size(729, 331);
                    this.Text = "AGREGAR PAGO";
                }
                else if (IdCorteTurno > 0)
                {
                    PagoNeg pagoNeg = new PagoNeg(cadconn);
                    gvPagos.Visible = true;
                    gbPago.Visible = false;
                    //gvPagos.DataSource = pagoNeg.ObtenerPorIdCorteTurnoSinVenta(IdCorteTurno);
                    gvPagos.DataSource = null;
                    gvPagos.DataSource = pagoNeg.ObtenerPorIdCorteTurno(IdCorteTurno);
                    gvPagos.Size = new Size(695, 272);
                    gvPagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    this.Size = new Size(729, 331);
                    this.Text = "PAGOS Y DEVOLUCIONES";
                    gvPagos.Columns["Cantidad"].DefaultCellStyle.Format = "C2";
                    gvPagos.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

                }

                gvPagos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                foreach (DataGridViewColumn item in gvPagos.Columns)
                    item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ex) {
                Utilerias.RegistrarLogError(ex, C_CLASE, "frmPago", "");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPago_Load(object sender, EventArgs e)
        {
            if (IdCorteTurno < 1)
            {
                this.gvPagos.Visible = false;
                int y_gbPago = 0;

                if (this.bDevolucion)
                {
                    lblPagoCon.Text = "Devolución con:";
                    lblPagoCon.Location = new Point(23, 38); //Si fuera pago (23,38)
                }

                lblMontoTotal.Text = montoTotalVenta.ToString();
                lblMontoTotal.Size = new Size(77, 21);
                lblMontoTotal.Location = new Point(215, 60);

                lblMontoPagado.Text = "0.00";
                lblMontoPagado.Size = new Size(77, 21);
                lblMontoPagado.Location = new Point(215, 100);


                if (this.numPago == 1)
                {
                    lblMontoPendiente.Text = montoTotalVenta.ToString();
                    lblMontoPendiente.Size = new Size(77, 21);
                    lblMontoPendiente.Location = new Point(215, 140);
                    y_gbPago = 12;
                }
                else
                {
                    this.gvPagos.Visible = true;
                    //Obtener todos los pago de la venta
                    y_gbPago = gvPagos.Location.Y + gvPagos.Height + 50;
                }

                this.gbPago.Location = new Point(12, y_gbPago);
                this.Height = 12 + this.gbPago.Height + 50;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Crear el pago
            pago = new Pago()
            {
                //Por ahora omitir cta bancaria
                NumPago= numPago,
                TipoPago = Utilerias.ObtenerTipoPagoParaBD(cmbTipoPago.Text),
                Cantidad = Convert.ToDecimal(txtCantidad.Text.Trim())
            };

            //Imprimir Pago

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
