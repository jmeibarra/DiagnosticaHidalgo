using DiagnosticaHidalgo.Clases;
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
    public partial class fCitas : Form
    {
        LabPorVentaNeg labPorVentaNeg;
        public fCitas(string cadenaconn)
        {
            InitializeComponent();
            labPorVentaNeg = new LabPorVentaNeg(cadenaconn);
            gv.ColumnHeadersDefaultCellStyle.ForeColor = C.COLOR_SISTEMA;
            gv.ForeColor = C.COLOR_SISTEMA;
            gv.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            CargarCitas();
        }

        /// <summary>
        /// Método para obtener y mostrar las citas de la semana
        /// </summary>
        private void CargarCitas()
        {            
            DataTable tabla = labPorVentaNeg.ObtenerReservaciones();
            if (tabla != null && tabla.Rows != null)
            {
                gv.DataSource = tabla;
                gv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                Utilerias.MostrarMensaje("No existen citas");
                this.Close();
            }
        }
    }
}
