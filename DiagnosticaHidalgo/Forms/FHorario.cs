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
    public partial class FHorario : Form
    {
        public Horarios _horarios;
        private string _familia;
        public DateTime reservacion;
        public string _horarioLaboratorio;
        public FHorario(string cadenaconn, string familia, string clave, string nombre, Horarios horarios)
        {
            InitializeComponent();

            _horarios = horarios;
            _familia = familia;
            dtFechaReservacion.MinDate = DateTime.Now.Date;
            lblLaboratorio.Text = nombre;
            List<string> horas = horarios.ObtenerHorariosDisponibles(familia, clave, dtFechaReservacion.Value,cadenaconn);
            cmbHora.DataSource = horas;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbHora.Text) || cmbHora.Text == Horarios.SIN_HORARIOS)
            {
                Utilerias.MostrarMensaje("Hora es requerido");
                return;
            }

            reservacion = new DateTime(dtFechaReservacion.Value.Year, dtFechaReservacion.Value.Month, dtFechaReservacion.Value.Day);
            string[] arr = cmbHora.Text.Split(':');
            reservacion = reservacion.AddHours(Convert.ToInt32(arr[0]));
            reservacion = reservacion.AddMinutes(Convert.ToDouble(arr[1]));
            _horarios.AgregarReservacion(cmbHora.Text, reservacion, _familia);
            _horarioLaboratorio = cmbHora.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
