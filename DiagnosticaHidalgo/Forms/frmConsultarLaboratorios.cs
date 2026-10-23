using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Negocio;
using DiagnosticaHidalgo.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DiagnosticaHidalgo.Forms
{
    public partial class frmConsultarLaboratorios : Form
    {
        #region Variables
        LabPorVentaNeg labPorVentaNeg;
        private LaboratorioNeg oLaboratorioNeg = null;
        List<Laboratorio> lLaboratorio = new List<Laboratorio>();

        public Laboratorio laboratorio = null;
        public List<string> lColsOcultas = new List<string>();
        public bool bEdicion;
        public int IdVenta;
        public string cadenaconn;
        #endregion
        public frmConsultarLaboratorios(string cadconn, bool edicion, int IdVenta)
        {
            InitializeComponent();
            oLaboratorioNeg = new LaboratorioNeg(cadconn);
            labPorVentaNeg = new LabPorVentaNeg(cadconn);
            cadenaconn = cadconn;

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

            this.IdVenta = IdVenta;
            this.bEdicion = edicion;
            gbOpciones.Visible = false;
        }

        private void frmConsulta_Load(object sender, EventArgs e)
        {
            if (this.IdVenta < 1)
            {
                this.Text = "LABORATORIOS";
                lLaboratorio = oLaboratorioNeg.ObtenerTodos();
                ActualizarGV();
            }
            else
            {
                gbFiltros.Visible = false;
                this.Size = new Size(770, 300);
                this.Text = "LABORATORIOS POR VENTA";
                gbOpciones.Visible = true;
                ActualizarGVLabPorVta();
            }
        }

        #region Funciones Auxiliares
        /// <summary>
        /// Función para actualizar DataGridView que contiene los registros de consulta
        /// </summary>
        public void ActualizarGV()
        {
            gv.DataSource = lLaboratorio;
            foreach (string col in lColsOcultas)
            {
                gv.Columns[col].Visible = false;
            }
            //Eliminar las columnas de editar y eliminar
            if (gv.Columns[0].Name != "IdLaboratorio")
            {
                gv.Columns.RemoveAt(0);
            }
            gv.Columns["IdLaboratorio"].Visible = false;
            gv.Columns["PrecioVenta"].DefaultCellStyle.Format = "C2";
            gv.Columns["PrecioVenta"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            gv.Columns["PrecioVentaUrgencia"].DefaultCellStyle.Format = "C2";
            gv.Columns["PrecioVentaUrgencia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;

            if (this.bEdicion)
            {
                //Agregar columna para editar laboratorio
                DataGridViewImageColumn btnE = new DataGridViewImageColumn();
                btnE.Width = 100;
                btnE.MinimumWidth = 25;
                btnE.Image = Resources.edicion;
                btnE.Name = C.COLUMNA_MODIFICAR;
                btnE.ToolTipText = "Editar laboratorio";
                //btnE.HeaderText = "E";
                btnE.ImageLayout = DataGridViewImageCellLayout.Stretch;
                btnE.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                gv.Columns.Add(btnE);
                gv.Columns["PrecioCosto"].DefaultCellStyle.Format = "C2";
                gv.Columns["PrecioCosto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            }
            else
            {
                gv.Columns["PrecioCosto"].Visible = false;
                gv.Columns["ClaveSAT"].Visible = false;
                gv.Columns["UnidadSAT"].Visible = false;
                gv.Columns["FechaCreacion"].Visible = false;
                gv.Columns["FechaAct"].Visible = false;
            }
            gv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            foreach (DataGridViewColumn item in gv.Columns)
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        /// <summary>
        /// Función para actualizar DataGridView que contiene los registros de laboratorios por venta
        /// </summary>
        public void ActualizarGVLabPorVta()
        {
            gv.DataSource = labPorVentaNeg.ObtenerPorIdVenta(this.IdVenta);

            gv.Columns["CostoOriginal"].DefaultCellStyle.Format = "C2";
            gv.Columns["CostoOriginal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            gv.Columns["Total"].DefaultCellStyle.Format = "C2";
            gv.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            gv.Columns["CostoCobrado"].DefaultCellStyle.Format = "C2";
            gv.Columns["CostoCobrado"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            gv.Columns["Entregado"].Visible = false;
            gv.Columns["Id"].Visible = false;

            DataGridViewCheckBoxColumn btnS = new DataGridViewCheckBoxColumn();
            btnS.Name = C.COLUMNA_SELECCION;
            btnS.HeaderText = "Entregado";
            gv.Columns.Add(btnS);

            for (int i = 0; i < gv.RowCount; i++)
            {
                gv[C.COLUMNA_SELECCION, i].Value = gv["Entregado", i].Value.ToString() == "SI" ? true : false;
            }

            gv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
        #endregion

        private void gv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            laboratorio = lLaboratorio[gv.CurrentRow.Index];
            if (!this.bEdicion)
                this.Close();
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            TextBox txtBox = null;
            string campo = chkBox.Name.Substring(3, chkBox.Name.Length - 3);
            switch (campo)
            {
                case "Clave":
                    txtBox = txtClave;
                    break;
                case "Descripcion":
                    txtBox = txtDescripcion;
                    break;
                case "Familia":
                    txtBox = txtFamilia;
                    break;
                case "Linea":
                    txtBox = txtLinea;
                    break;
                case "Formula":
                    txtBox = txtFormula;
                    break;
            }

            txtBox.ResetText();
            txtBox.Enabled = chkBox.Checked;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lLaboratorio = oLaboratorioNeg.ObtenerPorFiltros(txtClave.Text.Trim(), txtDescripcion.Text.Trim(), txtFamilia.Text.Trim(), txtLinea.Text.Trim(), txtFormula.Text.Trim());
            ActualizarGV();
        }
        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                lLaboratorio = oLaboratorioNeg.ObtenerPorFiltros(txtClave.Text.Trim(), txtDescripcion.Text.Trim(), txtFamilia.Text.Trim(), txtLinea.Text.Trim(), txtFormula.Text.Trim());
                ActualizarGV();
            }
        }

        #region Metodos Auxiliares
        #endregion

        private void gv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                try
                {
                    string nombreColumna = gv.Columns[e.ColumnIndex].Name;
                    if (nombreColumna == C.COLUMNA_MODIFICAR) //Editar
                    {
                        using (frmLaboratorio frm = new frmLaboratorio(lLaboratorio[e.RowIndex]))
                        {
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                btnBuscar_Click(btnBuscar, null);
                            }
                        }
                    }
                    else if (nombreColumna == C.COLUMNA_SELECCION) //El estudio ha sido entregado
                    {
                        bool selecionado = Convert.ToBoolean(gv[e.ColumnIndex, e.RowIndex].Value);
                        if (selecionado == false) //Pagar
                        {
                            gv[e.ColumnIndex, e.RowIndex].Value = true;
                        }
                        else
                        {
                            gv[e.ColumnIndex, e.RowIndex].Value = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utilerias.MostrarMensaje(ex.Message);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (gv.Rows.Count > 0)
            {
                //Alterar los laboratorios de la venta, si fueron o no entregados
                List<LabPorVenta> lLabPorVentas = new List<LabPorVenta>();
                foreach (DataGridViewRow renglon in gv.Rows)
                {
                    bool seleccionado = Convert.ToBoolean(renglon.Cells[C.COLUMNA_SELECCION].Value);
                    bool entregado = renglon.Cells["Entregado"].Value.ToString() == "SI" ? true : false;
                    if (entregado != seleccionado)
                    {
                        LabPorVenta labPorVenta = new LabPorVenta();
                        labPorVenta.IdLabPorVenta = Convert.ToInt32(renglon.Cells["Id"].Value);
                        labPorVenta.Entregado = seleccionado;
                        lLabPorVentas.Add(labPorVenta);
                    }
                }

                if (lLabPorVentas.Count > 0)
                {
                    string msjError = "";
                    if (!labPorVentaNeg.CambiarEntregado(lLabPorVentas, ref msjError))
                        Utilerias.MostrarMensaje(msjError);
                }
            }
            this.Close();
        }
    }
}
