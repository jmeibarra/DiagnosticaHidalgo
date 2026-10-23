using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Clases.Modelos;
using DiagnosticaHidalgo.Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DiagnosticaHidalgo
{
    public partial class frmCliente : Form
    {
        #region Variables
        private const string C_CLASE = "frmCliente";
        public int IdCliente;
        private ClienteNeg oClienteNeg;
        private Cliente oCliente;
        private List<ItemAutocompletar> _listaClientes;
        public string nombre;
        private bool bCambioChecked;
        /// <summary>
        /// Usuario que esta manejando el sistema
        /// </summary>
        public Usuario us = null;
        #endregion
        public frmCliente(Usuario usuario, int IdCliente)
        {
            try
            {
                InitializeComponent();
                this.us = usuario;
                //Verificar si los controles tienen información
                oClienteNeg = new ClienteNeg(us.Cadenaconn);
                this.IdCliente = IdCliente > 0 ? IdCliente : -1;

                //Cambiar el color de fuente de groupbox
                gbBusqueda.ForeColor = C.COLOR_SISTEMA;
                foreach (Control ctr in gbBusqueda.Controls)
                    ctr.ForeColor = C.COLOR_SISTEMA;
                gbDatos.ForeColor = C.COLOR_SISTEMA;
                foreach (Control ctr in gbDatos.Controls)
                    ctr.ForeColor = C.COLOR_SISTEMA;

                gbEdad.ForeColor = C.COLOR_SISTEMA;
                foreach (Control ctr in gbBusqueda.Controls)
                    ctr.ForeColor = C.COLOR_SISTEMA;

                btnAceptar.BackColor = C.COLOR_SISTEMA_CLARO;
                btnAceptar.ForeColor = C.COLOR_SISTEMA;
                btnAgregar.BackColor = C.COLOR_SISTEMA_CLARO;
                btnAgregar.ForeColor = C.COLOR_SISTEMA;
                btnCancelar.BackColor = C.COLOR_SISTEMA_CLARO;
                btnCancelar.ForeColor = C.COLOR_SISTEMA;
            }
            catch (Exception ex) {
                Utilerias.RegistrarLogError(ex, C_CLASE, "btnAgregarCliente_Click", "");
            }

        }

        #region Eventos
        /// <summary>
        /// Al cargar el formulario, limpiar la información de los controles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCliente_Load(object sender, EventArgs e)
        {
            if (this.IdCliente > 0)
            {
                btnAceptar.Text = "ACTUALIZAR";
                this.oCliente = oClienteNeg.ObtenerPorIdCliente(this.IdCliente);
                Actualizar_gbDatos(this.oCliente);

                lblIns1.Visible = lblIns2.Visible = gbBusqueda.Visible = false;
                gbDatos.Location = new Point(12, 12);
                btnAceptar.Location = new Point(587, 344);
                btnCancelar.Location = new Point(456, 344);
                this.Size = new Size(738, 420);
                gbDatos.Enabled = true;
            }
            else
            {
                this.nombre = "";
                txtNombreBusqueda.AutoCompleteCustomSource = LoadAutocomplete();
                txtNombreBusqueda.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtNombreBusqueda.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            this.bCambioChecked = false;
        }

        /// <summary>
        /// Evento al dar click en el botón "AGREGAR", si el cliente ya existe se actualiza su información, 
        /// de lo contrario se agrega.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Validación de los campos
            string msjError = ValidarCampos();
            if (string.IsNullOrEmpty(msjError))
            {
                try
                {
                    if (this.oCliente == null && this.IdCliente < 1)
                    {
                        ItemAutocompletar item = _listaClientes.FirstOrDefault(x => x.Nombre == txtNombre.Text.Trim());
                        if (item != null)
                        {
                            Utilerias.MostrarMensaje("El usuario no se puede agregar porque ya existe, es necesario seleccionarlo.");

                            txtNombre.Clear();
                            txtAnios.Clear();
                            cmbMeses.Text = "";
                            cmbDias.Text = "";
                            txtEmail.Clear();
                            txtCelular1.Clear();
                            txtCelular2.Clear();
                            chkMesesDias.Checked = chkAnios.Checked = false;
                            gbDatos.Enabled = false;

                            return;
                        }
                    }

                    if (this.oCliente == null)
                    {
                        //Crear el objeto Cliente
                        Cliente oCliente = new Cliente();
                        oCliente.Nombre = txtNombre.Text.Trim();
                        oCliente.Sexo = rbMujer.Checked ? C.MUJER : C.HOMBRE;
                        if (!string.IsNullOrEmpty(txtAnios.Text.Trim())) oCliente.Anios = Convert.ToInt32(txtAnios.Text.Trim());
                        else oCliente.Anios = -1;
                        if (!string.IsNullOrEmpty(cmbMeses.Text.Trim())) oCliente.Meses = Convert.ToInt32(cmbMeses.Text.Trim());
                        else oCliente.Meses = -1;
                        if (!string.IsNullOrEmpty(cmbDias.Text.Trim())) oCliente.Dias = Convert.ToInt32(cmbDias.Text.Trim());
                        else oCliente.Dias = -1;
                        if (!string.IsNullOrEmpty(txtEmail.Text.Trim())) oCliente.Email = txtEmail.Text.Trim();
                        if (!string.IsNullOrEmpty(txtCelular1.Text.Trim()) && !string.IsNullOrEmpty(txtCelular2.Text.Trim())) oCliente.Telefono = txtCelular1.Text.Trim() + txtCelular2.Text.Trim();

                        //Agregar o actualizar al cliente en la base de datos.
                        this.IdCliente = oClienteNeg.Upsert(oCliente);

                        nombre = oCliente.Nombre;
                    }
                    else
                    {
                        oCliente.Nombre = txtNombre.Text.Trim();
                        oCliente.Sexo = rbMujer.Checked ? C.MUJER : C.HOMBRE;
                        if (!string.IsNullOrEmpty(txtAnios.Text.Trim())) oCliente.Anios = Convert.ToInt32(txtAnios.Text.Trim());
                        else oCliente.Anios = -1;
                        if (!string.IsNullOrEmpty(cmbMeses.Text.Trim())) oCliente.Meses = Convert.ToInt32(cmbMeses.Text.Trim());
                        else oCliente.Meses = -1;
                        if (!string.IsNullOrEmpty(cmbDias.Text.Trim())) oCliente.Dias = Convert.ToInt32(cmbDias.Text.Trim());
                        else oCliente.Dias = -1;
                        if (!string.IsNullOrEmpty(txtEmail.Text.Trim())) oCliente.Email = txtEmail.Text.Trim();
                        if (!string.IsNullOrEmpty(txtCelular1.Text.Trim()) && !string.IsNullOrEmpty(txtCelular2.Text.Trim())) oCliente.Telefono = txtCelular1.Text.Trim() + txtCelular2.Text.Trim();
                        //Agregar o actualizar al cliente en la base de datos.
                        else oClienteNeg.Upsert(oCliente);

                        this.IdCliente = oCliente.IdCliente;
                        nombre = oCliente.Nombre;
                    }

                    oCliente = null;
                    this.Close();
                }
                catch (Exception ex)
                {
                    Utilerias.MostrarMensaje(ex.Message);
                }
            }
            else Utilerias.MostrarMensaje(msjError);
        }

        /// <summary>
        /// Evento click del botón "CANCELAR"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.nombre = "";
            this.Close();
        }

        private void txtCelular1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
                e.Handled = true;
            if (txtCelular1.TextLength == 2)
                txtCelular2.Focus();
        }

        private void txtCelular2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
                e.Handled = true;
        }
        #endregion

        #region Funciones Auxiliares
        /// <summary>
        /// Función para válidar que todos los campos sean correctos.
        /// Se válidan campos requeridos y expresiones regulares.
        /// </summary>
        /// <returns></returns>
        public string ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                return "Nombre completo es requerido.";
            else if (!Regex.IsMatch(txtNombre.Text.Trim(), C.EREG_NOMBRE))
                return "Nombre completo inválido.";
            else if (!rbHombre.Checked && !rbMujer.Checked)
                return "Sexo es requerido.";
            else if (!string.IsNullOrEmpty(txtEmail.Text.Trim()) && !Regex.IsMatch(txtEmail.Text.Trim(), "^([\\w\\._-]{1,150}@[\\w_-]{1,100}(\\.[\\w_-]{1,100}){1,10})$"))
                return "Correo electrónico inválido.";
            else if (!string.IsNullOrEmpty(txtCelular1.Text.Trim() + txtCelular2.Text.Trim()) && !Regex.IsMatch(txtCelular1.Text.Trim() + txtCelular2.Text.Trim(), "^([\\d]{10})$"))
                return "Teléfono celular inválido.";

            //Edad
            if (chkAnios.Checked)
            {
                if (!string.IsNullOrEmpty(txtAnios.Text.Trim()) && !Regex.IsMatch(txtAnios.Text.Trim(), C.EREG_NUMERO_ENTERO))
                    return "Años inválido.";
            }

            if (chkMesesDias.Checked)
            {
                if (!string.IsNullOrEmpty(cmbMeses.Text.Trim()))
                {
                    if (!Regex.IsMatch(cmbMeses.Text.Trim(), C.EREG_NUMERO_ENTERO))
                        return "Meses inválido.";
                }
                if (!string.IsNullOrEmpty(cmbDias.Text.Trim()))
                {
                    if (!Regex.IsMatch(cmbDias.Text.Trim(), C.EREG_NUMERO_ENTERO))
                        return "Días inválido.";
                }
            }

            return "";
        }

        /// <summary>
        /// Función para mostrar la información del cliente pasado por parámetro.
        /// </summary>
        public void Actualizar_gbDatos(Cliente cliente)
        {
            txtNombre.Text = cliente.Nombre;
            txtNombre.Enabled = false;
            rbHombre.Checked = rbMujer.Checked = false;
            if (cliente.Sexo == C.HOMBRE) rbHombre.Checked = true;
            else if (cliente.Sexo == C.MUJER) rbMujer.Checked = true;
            bCambioChecked = true;
            if (cliente.Anios.HasValue && cliente.Anios > -1)
            {
                chkAnios.Checked = true;
                txtAnios.Text = cliente.Anios.ToString();
                txtAnios.Enabled = true;
            }
            else
            {
                chkAnios.Checked = false;
                txtAnios.Text = "";
                txtAnios.Enabled = false;
            }
            if ((cliente.Meses.HasValue && cliente.Meses.Value > -1) || (cliente.Dias.HasValue && cliente.Dias.Value > -1))
            {
                chkMesesDias.Checked = true;
                if (cliente.Meses.HasValue) cmbMeses.Text = cliente.Meses.ToString();
                if (cliente.Dias.HasValue) cmbDias.Text = cliente.Dias.ToString();
                cmbMeses.Enabled = true;
                cmbDias.Enabled = true;
            }
            else
            {
                chkMesesDias.Checked = false;
                cmbMeses.Text = "";
                cmbDias.Text = "";
                cmbMeses.Enabled = false;
                cmbDias.Enabled = false;
            }
            bCambioChecked = false;

            txtEmail.Text = string.IsNullOrEmpty(cliente.Email) ? "" : cliente.Email;
            if (!string.IsNullOrEmpty(cliente.Telefono))
            {
                txtCelular1.Text = cliente.Telefono.Substring(0, 3);
                txtCelular2.Text = cliente.Telefono.Substring(3, 7);
            }
            else
            {
                txtCelular1.Text = "";
                txtCelular2.Text = "";
            }
        }
        #endregion

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Utilerias.LimpiarControles(this.gbDatos);
            txtNombreBusqueda.Text = "";
            gbDatos.Enabled = true;
            txtNombre.Enabled = true;
            this.IdCliente = -1;
            chkAnios.Checked = true;
            bCambioChecked = true;
            chkAnios.Checked = false;
            txtAnios.Enabled = false;
            txtAnios.Text = "";
            bCambioChecked = false;
        }

        private void chkAnios_CheckedChanged(object sender, EventArgs e)
        {
            if (!bCambioChecked)
            {
                txtAnios.Enabled = chkAnios.Checked;
                txtAnios.Text = "";
                bCambioChecked = true;
                if (chkAnios.Checked)
                {
                    chkMesesDias.Checked = false;
                    cmbMeses.Enabled = cmbDias.Enabled = chkMesesDias.Checked;
                    cmbMeses.Text = cmbDias.Text = "";
                }
                bCambioChecked = false;
            }
        }

        private void chkMesesDias_CheckedChanged(object sender, EventArgs e)
        {
            if (!bCambioChecked)
            {
                cmbMeses.Enabled = cmbDias.Enabled = chkMesesDias.Checked;
                cmbMeses.Text = cmbDias.Text = "";
                bCambioChecked = true;
                if (chkMesesDias.Checked)
                {
                    chkAnios.Checked = false;
                    txtAnios.Enabled = chkAnios.Checked;
                    txtAnios.Text = "";
                }
                bCambioChecked = false;
            }
        }

        private void txtAnios_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void cmbMeses_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        public AutoCompleteStringCollection LoadAutocomplete()
        {
            //Instanciamos el objeto que se usara para el AutoComplete
            AutoCompleteStringCollection stringdata = new AutoCompleteStringCollection();
            _listaClientes = oClienteNeg.ObtenerTodosAutocompletar();
            foreach (ItemAutocompletar item in _listaClientes) stringdata.Add(item.Nombre);
            //Devolvemos el AutoCompleteStringCollection con los datos cargados
            return stringdata;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNombreBusqueda_TextChanged(object sender, EventArgs e)
        {
            ItemAutocompletar item = _listaClientes.FirstOrDefault(x => x.Nombre == txtNombreBusqueda.Text);
            if (item == null)
            {
                Utilerias.LimpiarControles(this.gbDatos);
                gbDatos.Enabled = false;
                return;
            }
            else
            {
                this.IdCliente = item.Id;
                gbDatos.Enabled = true;
                if (this.IdCliente > 0)
                {
                    //Obtener los datos del cliente seleccionado
                    Cliente oClienteEncontrado = oClienteNeg.ObtenerPorIdCliente(this.IdCliente);
                    Actualizar_gbDatos(oClienteEncontrado);
                }
            }
        }
    }
}
