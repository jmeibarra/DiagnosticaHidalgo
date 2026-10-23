using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Clases.Modelos;
using DiagnosticaHidalgo.Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DiagnosticaHidalgo.Forms
{
    public partial class frmReportes : Form
    {
        #region Variables
        CorteTurnoNeg corteTurnoNeg;
        private static string C_CLASE = "frmReportes";
        private Configuraciones config = null;
        /// <summary>
        /// Usuario que esta manejando el sistema
        /// </summary>
        public Usuario us = null;
        #endregion
        public frmReportes(Usuario usuario, Configuraciones configuracion)
        {
            InitializeComponent();
            this.us=usuario;
            corteTurnoNeg = new CorteTurnoNeg(us.Cadenaconn);
            /*Cambiar estilos*/
            foreach (Control gb in this.Controls)
            {
                if (gb is GroupBox)
                {
                    gb.ForeColor = C.COLOR_SISTEMA;
                    foreach (Control ctrl in gb.Controls)
                    {
                        if (ctrl is Button)
                        {
                            ctrl.BackColor = C.COLOR_SISTEMA;
                            ctrl.ForeColor = C.COLOR_SISTEMA_CLARO;
                        }
                        else ctrl.ForeColor = C.COLOR_SISTEMA;
                    }
                }
                else if (gb is DataGridView)
                {
                    DataGridView gv = (DataGridView)gb;
                    gv.ColumnHeadersDefaultCellStyle.ForeColor = C.COLOR_SISTEMA;
                    gv.ForeColor = C.COLOR_SISTEMA;
                    gv.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
            }
            /*Termina cambiar estilos*/

            this.config = configuracion;
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            try
            {
                List<int> anios = new List<int>();
                for (int i = 2021; i <= DateTime.Now.Year; i++)
                {
                    anios.Add(i);
                }

                cmbAnios.DataSource = anios;               
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "frmReportes_Load", "");
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "reporte_" + DateTime.Now.ToString("ddMMMMyyyy_HHmmss") + ".pdf";
                string filePath = C.PathTEMP + fileName;
                DateTime fechaIni = Convert.ToDateTime(cmbCorteIni.Text);
                DateTime fechaFin = Convert.ToDateTime(cmbCorteFin.Text);
                byte[] bytesPDF = null;
                if (rbDoctores.Checked)
                    bytesPDF = corteTurnoNeg.CrearReporteDoctorPDF(fechaIni, fechaFin, this.config.Empresa);
                else if (rbFamiliaEstudios.Checked)
                {
                    bytesPDF = corteTurnoNeg.CrearReporteFamiliaEstudiosPDF(fechaIni, fechaFin, this.config.Empresa);
                }
                else if (rbEstudios.Checked)
                {
                    bytesPDF = corteTurnoNeg.CrearReporteEstudiosPDF(fechaIni, fechaFin, this.config.Empresa);
                }

                if (bytesPDF != null)
                {
                    Utilerias.Imprimir(bytesPDF, filePath);
                    Utilerias.MostrarMensaje("Reporte generado con éxito");
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "btnGenerar_Click", "");
                Utilerias.MostrarMensaje("¡Ocurrió un problema intenetelo mas trade!");
            }
        }

        private void dtFechaI_ValueChanged(object sender, EventArgs e)
        {
            var cortes = corteTurnoNeg.ObtenerCortesPorRango(dtFechaI.Value, dtFechaF.Value);

            // Combo de inicio con su propia copia de la lista
            cmbCorteIni.DataSource = new List<CorteTurnoMod>(cortes);
            cmbCorteIni.DisplayMember = "FechaCreacion";
            cmbCorteIni.ValueMember = "Id";
            cmbCorteIni.SelectedItem = null;

            // Combo de fin con otra copia independiente
            cmbCorteFin.DataSource = new List<CorteTurnoMod>(cortes);
            cmbCorteFin.DisplayMember = "FechaFinal";
            cmbCorteFin.ValueMember = "Id";
            cmbCorteFin.SelectedItem = null;
        }

        private void dtFechaF_ValueChanged(object sender, EventArgs e)
        {
            var cortes = corteTurnoNeg.ObtenerCortesPorRango(dtFechaI.Value, dtFechaF.Value);

            // Solo refrescar el combo de fin con una lista independiente
            cmbCorteFin.DataSource = new List<CorteTurnoMod>(cortes);
            cmbCorteFin.DisplayMember = "FechaFinal";
            cmbCorteFin.ValueMember = "Id";
            cmbCorteFin.SelectedItem = null;
        }

        private void cmbAnios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAnios.SelectedItem != null)
            {
                int anioSeleccionado = Convert.ToInt32(cmbAnios.SelectedItem);

                DateTime min = new DateTime(anioSeleccionado, 1, 1);
                DateTime max = new DateTime(anioSeleccionado, 12, 31);

                // 1. Resetear rango antes de asignar
                dtFechaI.MinDate = DateTimePicker.MinimumDateTime;
                dtFechaI.MaxDate = DateTimePicker.MaximumDateTime;
                dtFechaF.MinDate = DateTimePicker.MinimumDateTime;
                dtFechaF.MaxDate = DateTimePicker.MaximumDateTime;

                // 2. Asignar nuevo rango
                dtFechaI.MinDate = min;
                dtFechaI.MaxDate = max;
                dtFechaF.MinDate = min;
                dtFechaF.MaxDate = max;

                // 3. Ajustar Value dentro del rango
                if (anioSeleccionado == DateTime.Now.Year)
                {
                    dtFechaI.Value = DateTime.Now;
                    dtFechaF.Value = DateTime.Now;
                }
                else
                {
                    dtFechaI.Value = min;
                    dtFechaF.Value = max;
                }

                // Obtener cortes filtrados por rango
                var cortes = corteTurnoNeg.ObtenerCortesPorRango(dtFechaI.Value, dtFechaF.Value);

                cmbCorteIni.DataSource = cortes;
                cmbCorteFin.DataSource = cortes;

                cmbCorteIni.DisplayMember = "FechaCreacion";
                cmbCorteFin.DisplayMember = "FechaFinal";
                cmbCorteIni.ValueMember = "Id";
                cmbCorteFin.ValueMember = "Id";

                cmbCorteFin.SelectedItem = cmbCorteIni.SelectedItem = null;
            }
        }

    }
}
