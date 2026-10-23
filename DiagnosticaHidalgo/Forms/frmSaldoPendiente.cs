using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Negocio;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DiagnosticaHidalgo.Forms
{
    public partial class frmSaldoPendiente : Form
    {
        private bool bCargandoDatos = false;
        private ClienteNeg clienteNeg;
        private const string C_CLASE = "frmSaldoPorCliente";
        /// <summary>
        /// Usuario que esta manejando el sistema
        /// </summary>
        public Usuario us = null;

        public frmSaldoPendiente(string cadenaconn)
        {
            InitializeComponent();
            clienteNeg = new ClienteNeg(cadenaconn);
        }

        private void frmSaldoPorCliente_Load(object sender, EventArgs e)
        {
            //Obtener los clientes con notas pendientes
            label1.ForeColor = C.COLOR_SISTEMA;
            lblSaldo.ForeColor = C.COLOR_SISTEMA;
            cmbClientes.ForeColor = C.COLOR_SISTEMA;

            btnBuscar.BackColor = C.COLOR_SISTEMA;
            btnBuscar.ForeColor = C.COLOR_SISTEMA_CLARO;

            dgvVentas.ColumnHeadersDefaultCellStyle.ForeColor = C.COLOR_SISTEMA;
            dgvVentas.ForeColor = C.COLOR_SISTEMA;
            dgvVentas.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        /// <summary>
        /// Evento al cambiar la cadena de búsqueda en el control cmbClientes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbClientes_TextChanged(object sender, EventArgs e)
        {
            if (!bCargandoDatos)
            {
                bCargandoDatos = true;
                try
                {
                    string textoBusqueda = cmbClientes.Text.Trim();
                    object lClientes = clienteNeg.ObtenerPorNombrePendiente(cmbClientes.Text.Trim());
                    cmbClientes.DataSource = lClientes;
                    cmbClientes.DisplayMember = "Nombre";
                    cmbClientes.ValueMember = "IdCliente";
                    cmbClientes.SelectedItem = null;
                    cmbClientes.Text = textoBusqueda;
                    cmbClientes.SelectionStart = cmbClientes.Text.Length;
                }
                catch (Exception ex)
                {
                    Utilerias.MostrarMensaje(ex.Message);
                    Utilerias.RegistrarLogError(ex, C_CLASE, "cmbClientes_TextChanged", "");
                }
                finally { bCargandoDatos = false; }
            }
        }

        /// <summary>
        /// Evento al presionar una tecla sobre el comboBox de clientes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbClientes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.ObtenerSaldoPendiente();   
            }
            else
            {
                //Convertir en mayúsculas
                if (Char.IsLower(e.KeyChar)) {
                    e.KeyChar = Char.ToUpper(e.KeyChar);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.ObtenerSaldoPendiente();
        }

        /// <summary>
        /// Método para obtener el saldo pendiente de un cliente.
        /// </summary>
        private void ObtenerSaldoPendiente() {
            if (cmbClientes.SelectedValue != null)
            {
                double totalDeuda = 0;
                //Obtener las notas de ventas pendientes
                VentaNeg ventaNeg = new VentaNeg(us.Cadenaconn);
                dgvVentas.DataSource = null;
                dgvVentas.DataSource = ventaNeg.ObtenerPendientePorIdCliente((int)cmbClientes.SelectedValue, ref totalDeuda);
                dgvVentas.Columns["Id"].Visible = false;
                dgvVentas.Columns["Maquina"].Visible = false;
                dgvVentas.Columns["Usuario"].Visible = false;
                dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                dgvVentas.Columns["TotalVenta"].DefaultCellStyle.Format = "C2";
                dgvVentas.Columns["TotalVenta"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dgvVentas.Columns["Descuento"].DefaultCellStyle.Format = "C2";
                dgvVentas.Columns["Descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                dgvVentas.Columns["Resto"].DefaultCellStyle.Format = "C2";
                dgvVentas.Columns["Resto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

                lblSaldo.Text = "Saldo  $  " + Utilerias.ObtenerConDosDecimales(totalDeuda);

                foreach (DataGridViewColumn item in dgvVentas.Columns)
                    item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

    }
}
