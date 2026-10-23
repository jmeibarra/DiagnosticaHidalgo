
namespace DiagnosticaHidalgo.Forms
{
    partial class frmConsultarLaboratorios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConsultarLaboratorios));
            this.gv = new System.Windows.Forms.DataGridView();
            this.gbFiltros = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtFormula = new System.Windows.Forms.TextBox();
            this.txtLinea = new System.Windows.Forms.TextBox();
            this.txtFamilia = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.chkFormula = new System.Windows.Forms.CheckBox();
            this.chkLinea = new System.Windows.Forms.CheckBox();
            this.chkFamilia = new System.Windows.Forms.CheckBox();
            this.chkClave = new System.Windows.Forms.CheckBox();
            this.chkDescripcion = new System.Windows.Forms.CheckBox();
            this.gbOpciones = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnPagar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            this.gbFiltros.SuspendLayout();
            this.gbOpciones.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gv
            // 
            this.gv.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gv.Location = new System.Drawing.Point(0, 0);
            this.gv.MultiSelect = false;
            this.gv.Name = "gv";
            this.gv.ReadOnly = true;
            this.gv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv.Size = new System.Drawing.Size(1106, 422);
            this.gv.TabIndex = 0;
            this.gv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_CellContentClick);
            this.gv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_CellDoubleClick);
            // 
            // gbFiltros
            // 
            this.gbFiltros.BackColor = System.Drawing.SystemColors.Window;
            this.gbFiltros.Controls.Add(this.btnBuscar);
            this.gbFiltros.Controls.Add(this.txtFormula);
            this.gbFiltros.Controls.Add(this.txtLinea);
            this.gbFiltros.Controls.Add(this.txtFamilia);
            this.gbFiltros.Controls.Add(this.txtDescripcion);
            this.gbFiltros.Controls.Add(this.txtClave);
            this.gbFiltros.Controls.Add(this.chkFormula);
            this.gbFiltros.Controls.Add(this.chkLinea);
            this.gbFiltros.Controls.Add(this.chkFamilia);
            this.gbFiltros.Controls.Add(this.chkClave);
            this.gbFiltros.Controls.Add(this.chkDescripcion);
            this.gbFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbFiltros.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltros.Location = new System.Drawing.Point(0, 0);
            this.gbFiltros.Name = "gbFiltros";
            this.gbFiltros.Size = new System.Drawing.Size(1106, 161);
            this.gbFiltros.TabIndex = 1;
            this.gbFiltros.TabStop = false;
            this.gbFiltros.Text = "Filtros de búsqueda";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(828, 120);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(266, 35);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtFormula
            // 
            this.txtFormula.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFormula.Enabled = false;
            this.txtFormula.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFormula.Location = new System.Drawing.Point(839, 59);
            this.txtFormula.Name = "txtFormula";
            this.txtFormula.Size = new System.Drawing.Size(100, 29);
            this.txtFormula.TabIndex = 1;
            this.txtFormula.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            // 
            // txtLinea
            // 
            this.txtLinea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLinea.Enabled = false;
            this.txtLinea.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLinea.Location = new System.Drawing.Point(673, 59);
            this.txtLinea.Name = "txtLinea";
            this.txtLinea.Size = new System.Drawing.Size(100, 29);
            this.txtLinea.TabIndex = 1;
            this.txtLinea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            // 
            // txtFamilia
            // 
            this.txtFamilia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFamilia.Enabled = false;
            this.txtFamilia.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFamilia.Location = new System.Drawing.Point(515, 59);
            this.txtFamilia.Name = "txtFamilia";
            this.txtFamilia.Size = new System.Drawing.Size(100, 29);
            this.txtFamilia.TabIndex = 1;
            this.txtFamilia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Enabled = false;
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(207, 59);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(250, 29);
            this.txtDescripcion.TabIndex = 1;
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            // 
            // txtClave
            // 
            this.txtClave.Enabled = false;
            this.txtClave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClave.Location = new System.Drawing.Point(35, 59);
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(100, 29);
            this.txtClave.TabIndex = 1;
            this.txtClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            // 
            // chkFormula
            // 
            this.chkFormula.AutoSize = true;
            this.chkFormula.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFormula.Location = new System.Drawing.Point(816, 28);
            this.chkFormula.Name = "chkFormula";
            this.chkFormula.Size = new System.Drawing.Size(87, 25);
            this.chkFormula.TabIndex = 0;
            this.chkFormula.Text = "Fórmula";
            this.chkFormula.UseVisualStyleBackColor = true;
            this.chkFormula.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkLinea
            // 
            this.chkLinea.AutoSize = true;
            this.chkLinea.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLinea.Location = new System.Drawing.Point(654, 28);
            this.chkLinea.Name = "chkLinea";
            this.chkLinea.Size = new System.Drawing.Size(66, 25);
            this.chkLinea.TabIndex = 0;
            this.chkLinea.Text = "Linea";
            this.chkLinea.UseVisualStyleBackColor = true;
            this.chkLinea.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkFamilia
            // 
            this.chkFamilia.AutoSize = true;
            this.chkFamilia.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFamilia.Location = new System.Drawing.Point(497, 28);
            this.chkFamilia.Name = "chkFamilia";
            this.chkFamilia.Size = new System.Drawing.Size(79, 25);
            this.chkFamilia.TabIndex = 0;
            this.chkFamilia.Text = "Familia";
            this.chkFamilia.UseVisualStyleBackColor = true;
            this.chkFamilia.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkClave
            // 
            this.chkClave.AutoSize = true;
            this.chkClave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkClave.Location = new System.Drawing.Point(17, 28);
            this.chkClave.Name = "chkClave";
            this.chkClave.Size = new System.Drawing.Size(67, 25);
            this.chkClave.TabIndex = 0;
            this.chkClave.Text = "Clave";
            this.chkClave.UseVisualStyleBackColor = true;
            this.chkClave.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkDescripcion
            // 
            this.chkDescripcion.AutoSize = true;
            this.chkDescripcion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDescripcion.Location = new System.Drawing.Point(188, 28);
            this.chkDescripcion.Name = "chkDescripcion";
            this.chkDescripcion.Size = new System.Drawing.Size(110, 25);
            this.chkDescripcion.TabIndex = 0;
            this.chkDescripcion.Text = "Descripción";
            this.chkDescripcion.UseVisualStyleBackColor = true;
            this.chkDescripcion.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // gbOpciones
            // 
            this.gbOpciones.Controls.Add(this.btnCancelar);
            this.gbOpciones.Controls.Add(this.btnPagar);
            this.gbOpciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbOpciones.Location = new System.Drawing.Point(0, 583);
            this.gbOpciones.Name = "gbOpciones";
            this.gbOpciones.Size = new System.Drawing.Size(1106, 65);
            this.gbOpciones.TabIndex = 2;
            this.gbOpciones.TabStop = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(508, 21);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(125, 37);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnPagar
            // 
            this.btnPagar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagar.Location = new System.Drawing.Point(639, 21);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(125, 37);
            this.btnPagar.TabIndex = 4;
            this.btnPagar.Text = "GUARDAR";
            this.btnPagar.UseVisualStyleBackColor = true;
            this.btnPagar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gv);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 161);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1106, 422);
            this.panel1.TabIndex = 3;
            // 
            // frmConsultarLaboratorios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1106, 648);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbOpciones);
            this.Controls.Add(this.gbFiltros);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmConsultarLaboratorios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LABORATORIOS";
            this.Load += new System.EventHandler(this.frmConsulta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            this.gbFiltros.ResumeLayout(false);
            this.gbFiltros.PerformLayout();
            this.gbOpciones.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gv;
        private System.Windows.Forms.GroupBox gbFiltros;
        private System.Windows.Forms.CheckBox chkFormula;
        private System.Windows.Forms.CheckBox chkLinea;
        private System.Windows.Forms.CheckBox chkFamilia;
        private System.Windows.Forms.CheckBox chkClave;
        private System.Windows.Forms.CheckBox chkDescripcion;
        private System.Windows.Forms.TextBox txtFormula;
        private System.Windows.Forms.TextBox txtLinea;
        private System.Windows.Forms.TextBox txtFamilia;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox gbOpciones;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnPagar;
    }
}