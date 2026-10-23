using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Negocio;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DiagnosticaHidalgo.Forms
{
    public partial class frmUsuario : Form
    {
        #region Variables
        private bool bRegistro = false;
        private UsuarioNeg usuarioNeg;
        private Usuario us;
        /// <summary>
        /// Usuario que esta manejando el sistema
        /// </summary>
        public Usuario ussesion = null;
        #endregion
        public frmUsuario(string cadconn,bool registro, int IdUsuario)
        {
            InitializeComponent();
            usuarioNeg = new UsuarioNeg(cadconn);
            /*Cambiar estilos*/
            btnAceptar.BackColor = C.COLOR_SISTEMA;
            btnAceptar.ForeColor = C.COLOR_SISTEMA_CLARO;
            btnCancelar.BackColor = C.COLOR_SISTEMA;
            btnCancelar.ForeColor = C.COLOR_SISTEMA_CLARO;

            gbDatos.ForeColor = C.COLOR_SISTEMA;
            foreach (Control ctr in gbDatos.Controls)
                ctr.ForeColor = C.COLOR_SISTEMA;
            gbSesion.ForeColor = C.COLOR_SISTEMA;
            foreach (Control ctr in gbSesion.Controls)
                ctr.ForeColor = C.COLOR_SISTEMA;

            this.bRegistro = registro;
            gbSesion.Visible = false;
            if (registro)
            {
                btnAceptar.Text = "REGISTRARME";
                lblIndicaciones1.Visible = true;
                lblIndicaciones2.Visible = true;
                gbDatos.Location = new Point(12, 58);
            }
            else
            {
                btnAceptar.Text = "GUARDAR";
                lblIndicaciones1.Visible = false;
                lblIndicaciones2.Visible = false;
                //Obtener el usuario que se va a modificar
                this.us = usuarioNeg.ObtenerPorIdUsuario(IdUsuario);
                if (us.Administrador.Value)
                {
                    gbDatos.Size = new Size(593, 180);
                    gbDatos.Location = new Point(12, 12);
                    gbSesion.Location = new Point(gbDatos.Location.X, 218);
                    gbSesion.Visible = true;
                    btnCancelar.Location = new Point(316, 463);
                    btnAceptar.Location = new Point(470, 463);
                    this.Size = new Size(637, 575);
                }
                else
                {
                    btnCancelar.Location = new Point(316, 250);
                    btnAceptar.Location = new Point(470, 250);
                    gbDatos.Size = new Size(593, 220);
                    gbDatos.Location = new Point(12, 12);
                    this.Size = new Size(637, 330);
                }

                CargarUsuario(this.us);
            }
            
        }

        /// <summary>
        /// Botón para agregar o actualizar la información de un usuario.
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
                    if (bRegistro)
                    {
                        //Crear el objeto Usuario
                        Usuario oUsuario = new Usuario()
                        {
                            Nombre = txtNombre.Text.Trim(),
                            Email = txtEmail.Text.Trim(),
                            Telefono = txtCelular.Text.Trim(),
                            Clave = txtClave.Text.Trim(),
                            //Password = txtPassword.Text.Trim(),
                        };

                        //Agregar al usuario a la base de datos si el formulario es para registrar
                        usuarioNeg.Agregar(oUsuario);
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else {
                        us.Nombre = txtNombre.Text.Trim();
                        us.Email = txtEmail.Text.Trim();
                        us.Telefono = txtCelular.Text.Trim();
                        us.Clave = us.Administrador.Value ? txtClaveAdm.Text.Trim() : txtClave.Text.Trim();
                        if (us.Administrador.Value && chkCambiarCont.Checked)
                            us.Pass = txtPassword.Text.Trim();
                        else
                            us.Pass = null;

                        //Agregar al usuario a la base de datos si el formulario es para registrar
                        usuarioNeg.Actualizar(us);
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    Utilerias.MostrarMensaje(ex.Message);
                }
            }
            else Utilerias.MostrarMensaje(msjError);
        }

        /// <summary>
        /// El contro del teléfono celular solo acepte números
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #region Métodos auxiliares
        /// <summary>
        /// Función para válidar que todos los campos sean correctos.
        /// Se válidan campos requeridos y expresiones regulares.
        /// </summary>
        /// <returns></returns>
        public string ValidarCampos()
        {
            string msjError = "";
            if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                msjError = "Nombre es requerido.";
            else if (!Regex.IsMatch(txtNombre.Text.Trim(), "^[^|]{1,50}$"))
                msjError = "Nombre inválido.";
            else if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
                msjError = "Correo electrónico es requerido.";
            else if (!Regex.IsMatch(txtEmail.Text.Trim(), "^([\\w\\._-]{1,150}@[\\w_-]{1,100}(\\.[\\w_-]{1,100}){1,10})$"))
                msjError = "Correo electrónico inválido.";
            else if (string.IsNullOrEmpty(txtCelular.Text.Trim()))
                msjError = "Teléfono celular es requerido.";
            else if (!Regex.IsMatch(txtCelular.Text.Trim(), "^([\\d]{10})$"))
                msjError = "Teléfono celular inválido.";

            if (string.IsNullOrEmpty(msjError))
            {
                if (bRegistro)
                {
                    if (string.IsNullOrEmpty(txtClave.Text.Trim()))
                        msjError = "Clave es requerida.";
                    else if (!Regex.IsMatch(txtClave.Text.Trim(), "^([\\d]{3})$"))
                        msjError = "Clave inválida.";

                    //Validar si ya existe el usuario
                    if (usuarioNeg.ObtenerUsuarioPorClaveONombre(txtClave.Text.Trim(), txtNombre.Text.Trim()) != null)
                        msjError = "Ya existe un usuario con el mismo nombre o clave.";
                }
                else 
                {
                    if (us == null)
                        return "No existe el usuario";

                    if (us.Administrador.Value)
                    {
                        if (string.IsNullOrEmpty(txtClaveAdm.Text.Trim()))
                            return "Clave es requerida.";
                        else if (!Regex.IsMatch(txtClaveAdm.Text.Trim(), "^([\\d]{3})$"))
                            return  "Clave inválida.";
                        
                        if (chkCambiarCont.Checked) {
                            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                                return "Contraseña es requerida.";
                            else if (!Regex.IsMatch(txtPassword.Text.Trim(), "^([@$%+=\\dA-ZÑa-zñ]{0,}((([a-zñ]+[@$%+=\\dA-ZÑa-zñ]{0,}[A-ZÑ]+)|([A-ZÑ]+[@$%+=\\dA-ZÑa-zñ]{0,}[a-zñ]+))[@$%+=\\dA-ZÑa-zñ]{0,}\\d+[@$%+=\\dA-ZÑa-zñ]{0,}[@$%+=]+[@$%+=\\dA-ZÑa-zñ]{0,})|((([a-zñ]+[@$%+=\\dA-ZÑa-zñ]{0,}[A-ZÑ]+)|([A-ZÑ]+[@$%+=\\dA-ZÑa-zñ]{0,}[a-zñ]+))[@$%+=\\dA-ZÑa-zñ]{0,}[@$%+=]+[@$%+=\\dA-ZÑa-zñ]{0,}\\d+[@$%+=\\dA-ZÑa-zñ]{0,})|((([a-zñ]+[@$%+=\\dA-ZÑa-zñ]{0,}[@$%+=]+)|([@$%+=]+[@$%+=\\dA-ZÑa-zñ]{0,}[a-zñ]+))[@$%+=\\dA-ZÑa-zñ]{0,}[A-ZÑ]+[@$%+=\\dA-ZÑa-zñ]{0,}\\d+[@$%+=\\dA-ZÑa-zñ]{0,})|((([a-zñ]+[@$%+=\\dA-ZÑa-zñ]{0,}\\d+)|(\\d+[@$%+=\\dA-ZÑa-zñ]{0,}[a-zñ]+))[@$%+=\\dA-ZÑa-zñ]{0,}[A-ZÑ]+[@$%+=\\dA-ZÑa-zñ]{0,}[@$%+=]+[@$%+=\\dA-ZÑa-zñ]{0,})|((([@$%+=]+[@$%+=\\dA-ZÑa-zñ]{0,}[a-zñ]+)|([a-zñ]+[@$%+=\\dA-ZÑa-zñ]{0,}[@$%+=]+))[@$%+=\\dA-ZÑa-zñ]{0,}\\d+[@$%+=\\dA-ZÑa-zñ]{0,}[A-ZÑ]+[@$%+=\\dA-ZÑa-zñ]{0,})|((([A-ZÑ]+[@$%+=\\dA-ZÑa-zñ]{0,}[@$%+=]+)|([@$%+=]+[@$%+=\\dA-ZÑa-zñ]{0,}[A-ZÑ]+))[@$%+=\\dA-ZÑa-zñ]{0,}\\d+[@$%+=\\dA-ZÑa-zñ]{0,}[a-zñ]+[@$%+=\\dA-ZÑa-zñ]{0,})|(((\\ d+[a-zñ]+[@$%+=\\dA-ZÑa-zñ]{0,}[@$%+=])|([a-zñ]+[@$%+=\\dA-ZÑa-zñ]{0,}\\d+))[@$%+=\\dA-ZÑa-zñ]{0,}[@$%+=]+[@$%+=\\dA-ZÑa-zñ]{0,}[A-ZÑ]+[@$%+=\\dA-ZÑa-zñ]{0,})|((([A-ZÑ]+[@$%+=\\dA-ZÑa-zñ]{0,}\\d+)|(\\d+[@$%+=\\dA-ZÑa-zñ]{0,}[A-ZÑ]+))[@$%+=\\dA-ZÑa-zñ]{0,}[@$%+=]+[@$%+=\\dA-ZÑa-zñ]{0,}[a-zñ]+[@$%+=\\dA-ZÑa-zñ]{0,})|((([A-ZÑ]+[@$%+=\\dA-ZÑa-zñ]{0,}\\d+)|(\\d+[@$%+=\\dA-ZÑa-zñ]{0,}[A-ZÑ]+))[@$%+=\\dA-ZÑa-zñ]{0,}[a-zñ]+[@$%+=\\dA-ZÑa-zñ]{0,}[@$%+=]+[@$%+=\\dA-ZÑa-zñ]{0,})|((([A-ZÑ]+[@$%+=\\dA-ZÑa-zñ]{0,}[@$%+=]+)|([@$%+=]+[@$%+=\\dA-ZÑa-zñ]{0,}[A-ZÑ]+))[@$%+=\\dA-ZÑa-zñ]{0,}[a-zñ]+[@$%+=\\dA-ZÑa-zñ]{0,}\\d+[@$%+=\\dA-ZÑa-zñ]{0,})|((([@$%+=]+[@$%+=\\dA-ZÑa-zñ]{0,}\\d+)|(\\d+[@$%+=\\dA-ZÑa-zñ]{0,}[@$%+=]+))[@$%+=\\dA-ZÑa-zñ]{0,}[a-zñ]+[@$%+=\\dA-ZÑa-zñ]{0,}[A-ZÑ]+[@$%+=\\dA-ZÑa-zñ]{0,})|((([@$%+=]+[@$%+=\\dA-ZÑa-zñ]{0,}\\d+)|(\\d+[@$%+=\\dA-ZÑa-zñ]{0,}[@$%+=]+))[@$%+=\\dA-ZÑa-zñ]{0,}[A-ZÑ]+[@$%+=\\dA-ZÑa-zñ]{0,}[a-zñ]+)[@$%+=\\dA-ZÑa-zñ]{0,})$"))
                                return "Contraseña inválida.";
                            else if (string.IsNullOrEmpty(txtRepPass.Text.Trim()))
                                return "Confirmar contraseña es requerido.";
                            else if (txtRepPass.Text.Trim() != txtPassword.Text.Trim())
                                return "Las contraseñas no coinciden.";
                        }
                    }
                    else {
                        if (string.IsNullOrEmpty(txtClave.Text.Trim()))
                            msjError = "Clave es requerida.";
                        else if (!Regex.IsMatch(txtClave.Text.Trim(), "^([\\d]{3})$"))
                            msjError = "Clave inválida.";
                    }

                    //Validar si ya existe el usuario
                    if (usuarioNeg.ObtenerUsuarioPorClaveONombre(txtClave.Text.Trim(), txtNombre.Text.Trim(), us.IdUsuario) != null)
                        msjError = "Ya existe un usuario con el mismo nombre o clave.";
                }

            }
            return msjError;
        }

        /// <summary>
        /// Método para cargar los campos del usuario a partir de un objeto
        /// </summary>
        /// <param name="us"></param>
        public void CargarUsuario(Usuario us)
        {
            txtNombre.Text = us.Nombre;
            txtEmail.Text = us.Email;
            txtCelular.Text = us.Telefono;            

            if (us.Administrador.Value)
            {
                txtClaveAdm.Text = us.Clave;
            }
            else txtClave.Text = us.Clave;
        }
        #endregion

        private void chkCambiarCont_CheckedChanged(object sender, EventArgs e)
        {
            chkMostrarCont.Enabled = txtPassword.Enabled = txtRepPass.Enabled = chkCambiarCont.Checked;
            
            if (!chkCambiarCont.Checked) {
                txtPassword.Text = txtRepPass.Text = "";
                chkMostrarCont.Checked = false;
            }
        }

        private void chkMostrarCont_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkMostrarCont.Checked;
        }

        /// <summary>
        /// Evento clic del botón Cancelar el registro de un usuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
