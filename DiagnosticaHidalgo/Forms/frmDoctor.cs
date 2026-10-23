using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Clases.Modelos;
using DiagnosticaHidalgo.Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DiagnosticaHidalgo.Forms
{
    public partial class frmDoctor : Form
    {
        #region Variables
        public int IdDoctor;
        private DoctorNeg oDoctorNeg;
        public string nombre;
        public string C_CLASE = "frmDoctor";
        private List<ItemAutocompletar> _listaDoctores;
        private bool cargandoNombres = false;
        /// <summary>
        /// Usuario que esta manejando el sistema
        /// </summary>
        public Usuario us = null;
        #endregion

        /// <summary>
        /// Constructor del formulario Doctor.
        /// </summary>
        /// <param name="IdDoctor">Si es -1, es para buscar un doctor para una nota de venta. Mayor a cero, es para edición.</param>
        /// <param name="usuario">Usuario que esta ejecutando el sistema</param>
        public frmDoctor(Usuario usuario, int IdDoctor)
        {
            InitializeComponent();
            this.us = usuario;
            /*Cambiar fuentes, colores, estilos*/
            btnAceptar.BackColor = C.COLOR_SISTEMA;
            btnAceptar.ForeColor = C.COLOR_SISTEMA_CLARO;
            btnCancelar.BackColor = C.COLOR_SISTEMA;
            btnCancelar.ForeColor = C.COLOR_SISTEMA_CLARO;
            btnAgregar.BackColor = C.COLOR_SISTEMA_CLARO;
            btnAgregar.ForeColor = C.COLOR_SISTEMA;

            gbBusqueda.ForeColor = C.COLOR_SISTEMA;
            foreach (Control ctr in gbBusqueda.Controls)
                ctr.ForeColor = C.COLOR_SISTEMA;

            gbDatos.ForeColor = C.COLOR_SISTEMA;
            foreach (Control ctr in gbDatos.Controls)
                ctr.ForeColor = C.COLOR_SISTEMA;

            oDoctorNeg = new DoctorNeg(us.Cadenaconn);
            this.us = usuario;
            this.IdDoctor = IdDoctor;
        }

        #region Eventos
        /// <summary>
        /// Al cargar el formulario, limpiar la información de los controles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDoctor_Load(object sender, EventArgs e)
        {
            //Utilerias.LimpiarControles(this.gbDatos);
            if (this.IdDoctor > 0) //Edición de un doctor
            {
                gbDatos.Enabled = true;

                Doctor oDoctor = oDoctorNeg.ObtenerPorIdDoctor(this.IdDoctor);
                Actualizar_gbDatos(oDoctor);

            }
            else //Seleccionar un doctor para nota de venta
            {
                this.nombre = "";
            }

            gbDatos.Enabled = true;
            //Posición de controles
            label7.Visible = label6.Visible = gbBusqueda.Visible = false;
            gbDatos.Location = new Point(16, 12);
            btnCancelar.Location = new Point(389, 341);
            btnAceptar.Location = new Point(519, 341);
            btnAceptar.Text = "GUARDAR";
            this.Size = new Size(658, 432);
        }

        /// <summary>
        /// Evento al dar click en el botón "AGREGAR", si el doctor ya existe se actualiza su información, 
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
                    //Crear el objeto Cliente
                    Doctor oDoctor = new Doctor();
                    oDoctor.Nombre = txtNombre.Text.Trim();
                    oDoctor.Especialidad = txtEspecialidad.Text.Trim();
                    oDoctor.Estado = cmbEstado.Text.Trim();
                    oDoctor.Telefono = txtCelular1.Text.Trim() + txtCelular2.Text.Trim(); //$"{txtCelular1.Text.Trim()}{txtCelular2.Text.Trim()}";
                    oDoctor.Direccion = txtDireccion.Text.Trim();
                    oDoctor.CP = txtCP.Text.Trim();

                    this.IdDoctor = oDoctorNeg.Upsert(oDoctor);
                    //Es el identificador que se va a envíar al formulario de venta para agregarlo a la notade venta
                    nombre = oDoctor.Nombre;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    Utilerias.MostrarMensaje(ex.Message);
                    Utilerias.RegistrarLogError(ex, C_CLASE, "btnAceptar_Click", us.IdUsuario > 0 ? us.IdUsuario.ToString() + us.Nombre : us.Clave);
                    this.IdDoctor = -1;
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
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Utilerias.LimpiarControles(this.gbDatos);
            gbDatos.Enabled = true;
            txtNombre.Enabled = true;
            this.IdDoctor = -1;
        }

        private void txtNombreBusqueda_TextChanged(object sender, EventArgs e)
        {
            ItemAutocompletar item = _listaDoctores.FirstOrDefault(x => x.Nombre == txtNombreBusqueda.Text);
            if (item == null)
            {
                Utilerias.LimpiarControles(this.gbDatos);
                gbDatos.Enabled = false;
                return;
            }
            else
            {
                this.IdDoctor = item.Id;
                gbDatos.Enabled = true;
                if (this.IdDoctor > 0)
                {
                    //Obtener los datos del doctor seleccionado
                    Doctor oDoctor = oDoctorNeg.ObtenerPorIdDoctor(this.IdDoctor);
                    Actualizar_gbDatos(oDoctor);
                    txtNombre.Enabled = false;
                }
            }
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
            string msjError = "";
            if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                msjError = "Nombre completo es requerido.";
            else if (!Regex.IsMatch(txtNombre.Text.Trim(), C.EREG_NOMBRE))
                msjError = "Nombre completo inválido.";
            else if (!string.IsNullOrEmpty(txtEspecialidad.Text.Trim()) && !Regex.IsMatch(txtEspecialidad.Text.Trim(), "^[^|]{1,100}$"))
                msjError = "Especialidad inválido.";
            else if (!string.IsNullOrEmpty(txtCelular1.Text.Trim() + txtCelular2.Text.Trim()) && !Regex.IsMatch(txtCelular1.Text.Trim() + txtCelular2.Text.Trim(), "^([\\d]{10})$"))
                msjError = "Teléfono celular inválido.";
            else if (!string.IsNullOrEmpty(txtDireccion.Text.Trim()) && !Regex.IsMatch(txtDireccion.Text.Trim(), "^([^|]{1,300})$"))
                msjError = "Dirección inválido.";
            else if (!string.IsNullOrEmpty(txtCP.Text.Trim()) && !Regex.IsMatch(txtCP.Text.Trim(), "^([\\d]{5})$"))
                msjError = "Código Postal inválido.";

            return msjError;
        }

        /// <summary>
        /// Función para mostrar la información del doctor pasado por parámetro.
        /// </summary>
        public void Actualizar_gbDatos(Doctor doctor)
        {
            txtNombre.Text = doctor.Nombre;
            txtEspecialidad.Text = doctor.Especialidad;
            cmbEstado.Text = doctor.Estado;
            if (!string.IsNullOrEmpty(doctor.Telefono))
            {
                txtCelular1.Text = doctor.Telefono.Substring(0, 3);
                txtCelular2.Text = doctor.Telefono.Substring(3, 7);
            }
            else
            {
                txtCelular1.Text = "";
                txtCelular2.Text = "";
            }
            txtDireccion.Text = string.IsNullOrEmpty(doctor.Direccion) ? "" : doctor.Direccion;
            txtCP.Text = string.IsNullOrEmpty(doctor.CP) ? "" : doctor.CP;
        }
        //public AutoCompleteStringCollection LoadAutocomplete(bool bApPaterno)
        //{
        //    //Instanciamos el objeto que se usara para el AutoComplete
        //    AutoCompleteStringCollection stringdata = new AutoCompleteStringCollection();
        //    _listaDoctores = oDoctorNeg.ObtenerTodosAutocompletar(bApPaterno);
        //    foreach (ItemAutocompletar item in _listaDoctores) stringdata.Add(item.Nombre);
        //    //Devolvemos el AutoCompleteStringCollection con los datos cargados
        //    return stringdata;
        //}

        #endregion

        private void cmbNombreDoctor_TextChanged(object sender, EventArgs e)
        {
            if (cmbNombreDoctor.Text.Length > 0)
            {
                cargandoNombres = true;
                string cadena = cmbNombreDoctor.Text;
                _listaDoctores = oDoctorNeg.ObtenerTodosAutocompletar(cmbNombreDoctor.Text);
                cmbNombreDoctor.DataSource = _listaDoctores;
                cmbNombreDoctor.DisplayMember = "Nombre";
                cmbNombreDoctor.ValueMember = "Id";
                cmbNombreDoctor.Text = cadena;
                cargandoNombres = false;
            }
        }
    }
}
