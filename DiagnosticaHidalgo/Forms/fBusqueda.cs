using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Clases.Modelos;
using DiagnosticaHidalgo.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DiagnosticaHidalgo.Forms
{
    public partial class fBusqueda : Form
    {
        #region Variables
        public int Id;
        private bool busqueda_doctor;
        public string nombre;
        public string C_CLASE = "fBusquedaDoctor";
        public bool nuevo_registro = false;
        /// <summary>
        /// Usuario que esta manejando el sistema
        /// </summary>
        public Usuario us = null;
        #endregion
        public fBusqueda(Usuario usuario, bool doctores)
        {
            InitializeComponent();

            /*Cambiar fuentes, colores, estilos*/
            btnAgregar.BackColor = C.COLOR_SISTEMA_CLARO;
            btnAgregar.ForeColor = C.COLOR_SISTEMA;

            gbBusqueda.ForeColor = C.COLOR_SISTEMA;
            foreach (Control ctr in gbBusqueda.Controls)
                ctr.ForeColor = C.COLOR_SISTEMA;

            gv.ColumnHeadersDefaultCellStyle.ForeColor = C.COLOR_SISTEMA;
            gv.ForeColor = C.COLOR_SISTEMA;


            this.us = usuario;
            this.Id = -1;
            this.busqueda_doctor = doctores;
            this.nuevo_registro = false;

            if (busqueda_doctor)
            {
                this.Text = "DOCTORES";
                btnAgregar.Text = "NUEVO DOCTOR";
            }
            else
            {
                this.Text = "CLIENTES";
                btnAgregar.Text = "NUEVO CLIENTE";
            }
        }

        private void txtNombreBusqueda_TextChanged(object sender, EventArgs e)
        {
            if (busqueda_doctor)
            {
                DoctorNeg oDoctorNeg = new DoctorNeg(us.Cadenaconn);
                gv.DataSource = oDoctorNeg.ObtenerTodosAutocompletar(txtNombreBusqueda.Text.Trim());
            }
            else
            {
                ClienteNeg oClienteNeg = new ClienteNeg(us.Cadenaconn);
                gv.DataSource = oClienteNeg.ObtenerTodosAutocompletar(txtNombreBusqueda.Text.Trim());
            }
            gv.Columns[0].Visible = false;
            gv.Columns[1].Width = 650;
            gv.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            nuevo_registro = true;
            this.Close();
        }

        private void gv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                try
                {
                    Id = (int)gv["Id", e.RowIndex].Value;
                    nombre = gv["Nombre", e.RowIndex].Value.ToString();
                }
                catch (Exception ex)
                {
                    Utilerias.MostrarMensaje(ex.Message);
                    Utilerias.RegistrarLogError(ex, C_CLASE, "gv_CellContentClick", "");
                }

                this.Close();
            }
        }
    }
}
