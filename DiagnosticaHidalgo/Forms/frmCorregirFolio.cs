using System;
using System.Windows.Forms;
using DiagnosticaHidalgo.Negocio;
using DiagnosticaHidalgo.Clases;
using System.Drawing;

namespace DiagnosticaHidalgo.Forms
{
    public partial class frmCorregirFolio : Form
    {
        #region Variables
        public string C_CLASE = "frmCorregirFolio";
        #endregion

        #region Eventos
        public frmCorregirFolio()
        {
            InitializeComponent();

            btnAceptar.BackColor = C.COLOR_SISTEMA;
            btnAceptar.ForeColor = C.COLOR_SISTEMA_CLARO;
            btnCancelar.BackColor = C.COLOR_SISTEMA;
            btnCancelar.ForeColor = C.COLOR_SISTEMA_CLARO;
            picAyuda.Image = Image.FromFile(Application.StartupPath + @"\Imagenes\icoayuda.png");
        }
        #endregion

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtFolio.Text))
            {
                Utilerias.MostrarMensaje("Folio es requerido.");
                return;
            }

            try
            {
                if (Utilerias.MostrarMensajeConfirmacion("¿Esta seguro que el último folio correcto es " + txtFolio.Text + "?")) 
                { 
                    VentaNeg.CorregirFolios(Convert.ToInt32(txtFolio.Text));                
                }
                Utilerias.MostrarMensaje("Los folios fueron corregidos con éxito.");
                this.Close();
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje(ex.Message);
                Utilerias.RegistrarLogError(ex, C_CLASE, "btnAceptar_Click", "");
            }

        }

        private void picAyuda_Click(object sender, EventArgs e)
        {
            Form ayudaForm = new Form();
            ayudaForm.Text = "Ayuda - Folios";
            ayudaForm.Size = new Size(800, 600);

            PictureBox pb = new PictureBox();
            pb.Dock = DockStyle.Fill;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Image = Image.FromFile(Application.StartupPath + @"\Imagenes\picayudafolio.png");

            ayudaForm.Controls.Add(pb);
            ayudaForm.ShowDialog();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
