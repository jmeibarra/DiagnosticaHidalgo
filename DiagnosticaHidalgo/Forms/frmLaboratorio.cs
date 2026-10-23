using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Negocio;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DiagnosticaHidalgo.Forms
{
    public partial class frmLaboratorio : Form
    {
        Laboratorio laboratorio;
        /// <summary>
        /// Usuario que esta manejando el sistema
        /// </summary>
        public Usuario us = null;
        public frmLaboratorio(Laboratorio lab)
        {
            InitializeComponent();
            gbDatos.ForeColor = C.COLOR_SISTEMA;
            foreach (Control ctr in gbDatos.Controls)
                ctr.ForeColor = C.COLOR_SISTEMA;

            btnAceptar.BackColor = C.COLOR_SISTEMA;
            btnAceptar.ForeColor = C.COLOR_SISTEMA_CLARO;
            btnCancelar.BackColor = C.COLOR_SISTEMA;
            btnCancelar.ForeColor = C.COLOR_SISTEMA_CLARO;

            this.laboratorio = lab;
            if (lab != null) //edición
            {
                this.btnAceptar.Text = "EDITAR";
                this.CargarLaboratorio();
            }
            else this.btnAceptar.Text = C.AGREGAR;
        }
        /// <summary>
        /// Evento de carga para el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLaboratorio_Load(object sender, EventArgs e)
        {

        }
        public void CargarLaboratorio()
        {
            txtClave.Text = laboratorio.Clave;
            txtDescripcion.Text = laboratorio.Descripcion;
            txtFamilia.Text = laboratorio.Familia;
            txtLinea.Text = laboratorio.Linea;
            txtFormula.Text = laboratorio.Formula;
            txtVenta.Text = laboratorio.PrecioVenta.HasValue ? laboratorio.PrecioVenta.Value.ToString() : "";
            txtCosto.Text = laboratorio.PrecioCosto.HasValue ? laboratorio.PrecioCosto.Value.ToString() : "";
            txtPrecioUrgencia.Text = laboratorio.PrecioVentaUrgencia.HasValue ? laboratorio.PrecioVentaUrgencia.Value.ToString() : "";
            txtExistencias.Text = laboratorio.Existencias.HasValue ? laboratorio.Existencias.Value.ToString() : "";
            txtIndicaciones.Text = laboratorio.Indicaciones;
        }

        public string ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtClave.Text.Trim()))
                return "Clave es requerido.";
            else if (!Regex.IsMatch(txtClave.Text.Trim(), "^[^|]{1,50}$"))
                return "Clave inválido.";
            if (string.IsNullOrEmpty(txtDescripcion.Text.Trim()))
                return "Descripción es requerido.";
            else if (!Regex.IsMatch(txtDescripcion.Text.Trim(), "^[^|]{1,100}$"))
                return "Descripción inválido.";
            else if (!string.IsNullOrEmpty(txtFamilia.Text.Trim()) && !Regex.IsMatch(txtFamilia.Text.Trim(), "^[^|]{1,100}$"))
                return "Familia inválido.";
            else if (!string.IsNullOrEmpty(txtLinea.Text.Trim()) && !Regex.IsMatch(txtLinea.Text.Trim(), "^[^|]{1,100}$"))
                return "Linea inválido.";
            else if (!string.IsNullOrEmpty(txtFormula.Text.Trim()) && !Regex.IsMatch(txtFormula.Text.Trim(), "^[^|]{1,100}$"))
                return "Formula inválido.";
            else if (string.IsNullOrEmpty(txtVenta.Text.Trim()))
                return "Precio de venta es requerido.";
            else if (!Regex.IsMatch(txtVenta.Text.Trim(), C.EREG_DECIMAL))
                return "Precio de venta inválido.";
            else if (!string.IsNullOrEmpty(txtCosto.Text.Trim()) && !Regex.IsMatch(txtCosto.Text.Trim(), C.EREG_DECIMAL))
                return "Precio de costo inválido.";
            else if (!string.IsNullOrEmpty(txtPrecioUrgencia.Text.Trim()) && !Regex.IsMatch(txtPrecioUrgencia.Text.Trim(), C.EREG_DECIMAL))
                return "Precio venta urgencia inválido.";
            else if (!string.IsNullOrEmpty(txtExistencias.Text.Trim()) && !Regex.IsMatch(txtExistencias.Text.Trim(), C.EREG_DECIMAL))
                return "Existencias inválido.";
            else if (!string.IsNullOrEmpty(txtIndicaciones.Text.Trim()) && !Regex.IsMatch(txtIndicaciones.Text.Trim(), "^[^|]{1,100}$"))
                return "Indicaciones inválido.";

            return "";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string msjError = ValidarCampos();
                if (string.IsNullOrEmpty(msjError))
                {
                    if (btnAceptar.Text == C.AGREGAR) this.laboratorio = new Laboratorio();

                    this.laboratorio.Clave = txtClave.Text.Trim();
                    this.laboratorio.Descripcion = txtDescripcion.Text.Trim();
                    this.laboratorio.Familia = txtFamilia.Text.Trim();
                    this.laboratorio.Linea = txtLinea.Text.Trim();
                    this.laboratorio.Formula = txtFormula.Text.Trim();
                    this.laboratorio.PrecioVenta = Convert.ToDecimal(txtVenta.Text.Trim());
                    if (!string.IsNullOrEmpty(txtCosto.Text.Trim()))
                        this.laboratorio.PrecioCosto = Convert.ToDecimal(txtCosto.Text.Trim());
                    if (!string.IsNullOrEmpty(txtExistencias.Text.Trim()))
                        this.laboratorio.Existencias = Convert.ToInt32(txtExistencias.Text.Trim());
                    if (!string.IsNullOrEmpty(txtPrecioUrgencia.Text.Trim()))
                        this.laboratorio.PrecioVentaUrgencia = Convert.ToDecimal(txtPrecioUrgencia.Text.Trim());
                    this.laboratorio.Indicaciones = txtIndicaciones.Text.Trim();
                    
                    LaboratorioNeg laboratorioNeg = new LaboratorioNeg(us.Cadenaconn);

                    if (C.AGREGAR == btnAceptar.Text)
                    {
                        laboratorioNeg.Agregar(this.laboratorio);
                        Utilerias.MostrarMensaje("El laboratorio ha sido agregado.");
                    }
                    else
                    { laboratorioNeg.Actualizar(this.laboratorio);
                        Utilerias.MostrarMensaje("El laboratorio ha sido editado.");
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else Utilerias.MostrarMensaje(msjError);
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
