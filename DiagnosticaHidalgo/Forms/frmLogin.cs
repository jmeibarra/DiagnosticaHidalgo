using System;
using System.Windows.Forms;
using DiagnosticaHidalgo.Negocio;
using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Clases.Seguridad;

namespace DiagnosticaHidalgo.Forms
{
    public partial class frmLogin : Form
    {
        #region Variables
        public Usuario oUsuario = null;
        public bool bDescuento = false;
        public bool bDescAprobado = false;
        #endregion

        #region Eventos
        public frmLogin(bool activarDescuento, string mensaje = "")
        {
            InitializeComponent();
            /*Cambiar colores, estilos*/
            lblClave.ForeColor = label2.ForeColor = txtUsuario.ForeColor = txtCont.ForeColor = C.COLOR_SISTEMA;

            btnIniciarSesion.BackColor = C.COLOR_SISTEMA;
            btnIniciarSesion.ForeColor = C.COLOR_SISTEMA_CLARO;
            btnLimpiarCampos.BackColor = C.COLOR_SISTEMA;
            btnLimpiarCampos.ForeColor = C.COLOR_SISTEMA_CLARO;

            lblClave.Text = !string.IsNullOrEmpty(mensaje) ? mensaje : "Clave de Usuario:";
            bDescuento = activarDescuento;
        }

        /// <summary>
        /// Evento de carga de pagina.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (bDescuento)
            {
                lblSucursal.Visible = cmbSucursal.Visible = false;
                txtUsuario.Visible = false;
                lblClave.Visible = lblClave.Text == "Clave de Usuario:" ? false : true;
                btnIniciarSesion.Text = "ACEPTAR";
                btnLimpiarCampos.Text = "CANCELAR";
                this.Text = "Usuario Administrador";
                checkMostrarCont.Visible = false;
            }
            else { cmbSucursal.SelectedIndex = 0; }
        }

        /// <summary>
        /// Evento del check para mostrar u ocultar la contraseña
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkMostrarCont_CheckedChanged(object sender, EventArgs e)
        {
            txtCont.UseSystemPasswordChar = !checkMostrarCont.Checked;
        }

        /// <summary>
        /// Evento click de iniciar sesión.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                if (!bDescuento && txtUsuario.Text.Length == 0)
                {
                    txtUsuario.Focus();
                    throw new Exception("Usuario es requerido.");
                }
                if (txtCont.Text.Length == 0)
                {
                    txtCont.Focus();
                    throw new Exception("Contraseña es requerido.");
                }

                //string mensajeError = "";
                //Creamos contexto e instancia de UsuarioNeg.
                string cadenaconnsucursal = Conexion.ConexionDB();
                if (cmbSucursal.SelectedIndex == 1)
                    cadenaconnsucursal = ParametroGen.Cadena_conexion_sucursa_da();
                UsuarioNeg oUsuarioNeg = new UsuarioNeg(cadenaconnsucursal);
                if (bDescuento)
                {
                    if (!oUsuarioNeg.ExistePassAdministrador(txtCont.Text))
                        throw new Exception("Contraseña incorrecta.");

                    bDescAprobado = true;
                }
                else
                {
                    oUsuario = oUsuarioNeg.AutenticarUsuario(txtUsuario.Text, txtCont.Text);

                    if (oUsuario == null)
                        throw new Exception("Credenciales incorrectas.");

                    if (cmbSucursal.SelectedIndex == 1)
                        oUsuario.Cadenaconn = ParametroGen.Cadena_conexion_sucursa_da();
                    else oUsuario.Cadenaconn = Conexion.ConexionDB();
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje(ex.Message);
            }
        }

        /// <summary>
        /// Evento click del botón limpiar campos, borra la información que existe en los botones
        /// txtCont y txtUsuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            if (bDescuento)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else
            {
                txtUsuario.Text = "";
                txtCont.Text = "";
                checkMostrarCont.Checked = false;
                txtCont.UseSystemPasswordChar = true;
            }

        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnIniciarSesion_Click(btnIniciarSesion, null);
            }
        }
        #endregion

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!bDescuento || !bDescAprobado) { this.DialogResult = DialogResult.Cancel; }
        }

        private void CerrarSesion(object sender, FormClosedEventArgs e)
        {
            btnLimpiarCampos_Click(btnLimpiarCampos, null);
            //limpiar mensaje de error 
            this.Show();
            this.oUsuario = null;
            txtUsuario.Focus();
        }
    }
}
