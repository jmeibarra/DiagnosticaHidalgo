
namespace DiagnosticaHidalgo.Forms
{
    partial class frmReportes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportes));
            this.gbFiltros = new System.Windows.Forms.GroupBox();
            this.dtFechaF = new System.Windows.Forms.DateTimePicker();
            this.dtFechaI = new System.Windows.Forms.DateTimePicker();
            this.cmbAnios = new System.Windows.Forms.ComboBox();
            this.rbEstudios = new System.Windows.Forms.RadioButton();
            this.rbFamiliaEstudios = new System.Windows.Forms.RadioButton();
            this.rbDoctores = new System.Windows.Forms.RadioButton();
            this.cmbCorteFin = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCorteIni = new System.Windows.Forms.ComboBox();
            this.gbFiltros.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFiltros
            // 
            this.gbFiltros.Controls.Add(this.dtFechaF);
            this.gbFiltros.Controls.Add(this.dtFechaI);
            this.gbFiltros.Controls.Add(this.cmbAnios);
            this.gbFiltros.Controls.Add(this.rbEstudios);
            this.gbFiltros.Controls.Add(this.rbFamiliaEstudios);
            this.gbFiltros.Controls.Add(this.rbDoctores);
            this.gbFiltros.Controls.Add(this.cmbCorteFin);
            this.gbFiltros.Controls.Add(this.label5);
            this.gbFiltros.Controls.Add(this.btnGenerar);
            this.gbFiltros.Controls.Add(this.label4);
            this.gbFiltros.Controls.Add(this.label3);
            this.gbFiltros.Controls.Add(this.label2);
            this.gbFiltros.Controls.Add(this.label1);
            this.gbFiltros.Controls.Add(this.cmbCorteIni);
            this.gbFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFiltros.Location = new System.Drawing.Point(0, 0);
            this.gbFiltros.Name = "gbFiltros";
            this.gbFiltros.Size = new System.Drawing.Size(977, 287);
            this.gbFiltros.TabIndex = 0;
            this.gbFiltros.TabStop = false;
            this.gbFiltros.Text = "FILTROS";
            // 
            // dtFechaF
            // 
            this.dtFechaF.CustomFormat = "dd/MM/yyyy";
            this.dtFechaF.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaF.Location = new System.Drawing.Point(694, 87);
            this.dtFechaF.Name = "dtFechaF";
            this.dtFechaF.Size = new System.Drawing.Size(164, 29);
            this.dtFechaF.TabIndex = 12;
            this.dtFechaF.ValueChanged += new System.EventHandler(this.dtFechaF_ValueChanged);
            // 
            // dtFechaI
            // 
            this.dtFechaI.CustomFormat = "dd/MM/yyyy";
            this.dtFechaI.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaI.Location = new System.Drawing.Point(379, 87);
            this.dtFechaI.Name = "dtFechaI";
            this.dtFechaI.Size = new System.Drawing.Size(175, 29);
            this.dtFechaI.TabIndex = 12;
            this.dtFechaI.ValueChanged += new System.EventHandler(this.dtFechaI_ValueChanged);
            // 
            // cmbAnios
            // 
            this.cmbAnios.FormattingEnabled = true;
            this.cmbAnios.Location = new System.Drawing.Point(63, 90);
            this.cmbAnios.Name = "cmbAnios";
            this.cmbAnios.Size = new System.Drawing.Size(121, 29);
            this.cmbAnios.TabIndex = 11;
            this.cmbAnios.SelectedIndexChanged += new System.EventHandler(this.cmbAnios_SelectedIndexChanged);
            // 
            // rbEstudios
            // 
            this.rbEstudios.AutoSize = true;
            this.rbEstudios.Location = new System.Drawing.Point(464, 44);
            this.rbEstudios.Name = "rbEstudios";
            this.rbEstudios.Size = new System.Drawing.Size(126, 25);
            this.rbEstudios.TabIndex = 10;
            this.rbEstudios.Text = "Por estudios";
            this.rbEstudios.UseVisualStyleBackColor = true;
            // 
            // rbFamiliaEstudios
            // 
            this.rbFamiliaEstudios.AutoSize = true;
            this.rbFamiliaEstudios.Location = new System.Drawing.Point(212, 44);
            this.rbFamiliaEstudios.Name = "rbFamiliaEstudios";
            this.rbFamiliaEstudios.Size = new System.Drawing.Size(208, 25);
            this.rbFamiliaEstudios.TabIndex = 10;
            this.rbFamiliaEstudios.Text = "Por familia de estudios";
            this.rbFamiliaEstudios.UseVisualStyleBackColor = true;
            // 
            // rbDoctores
            // 
            this.rbDoctores.AutoSize = true;
            this.rbDoctores.Checked = true;
            this.rbDoctores.Location = new System.Drawing.Point(16, 44);
            this.rbDoctores.Name = "rbDoctores";
            this.rbDoctores.Size = new System.Drawing.Size(128, 25);
            this.rbDoctores.TabIndex = 10;
            this.rbDoctores.TabStop = true;
            this.rbDoctores.Text = "Por doctores";
            this.rbDoctores.UseVisualStyleBackColor = true;
            // 
            // cmbCorteFin
            // 
            this.cmbCorteFin.FormattingEnabled = true;
            this.cmbCorteFin.Location = new System.Drawing.Point(654, 148);
            this.cmbCorteFin.Name = "cmbCorteFin";
            this.cmbCorteFin.Size = new System.Drawing.Size(311, 29);
            this.cmbCorteFin.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(603, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 21);
            this.label5.TabIndex = 1;
            this.label5.Text = "Fecha Fin:";
            // 
            // btnGenerar
            // 
            this.btnGenerar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerar.Location = new System.Drawing.Point(765, 238);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(200, 37);
            this.btnGenerar.TabIndex = 8;
            this.btnGenerar.Text = "IMPRIMIR REPORTE";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(268, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "Fecha Inicio:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "Año:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(485, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha de corte final:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fecha de corte de inicio:";
            // 
            // cmbCorteIni
            // 
            this.cmbCorteIni.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCorteIni.FormattingEnabled = true;
            this.cmbCorteIni.Location = new System.Drawing.Point(212, 148);
            this.cmbCorteIni.Name = "cmbCorteIni";
            this.cmbCorteIni.Size = new System.Drawing.Size(247, 29);
            this.cmbCorteIni.TabIndex = 0;
            // 
            // frmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(977, 287);
            this.Controls.Add(this.gbFiltros);
            this.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmReportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REPORTES";
            this.Load += new System.EventHandler(this.frmReportes_Load);
            this.gbFiltros.ResumeLayout(false);
            this.gbFiltros.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFiltros;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCorteIni;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.ComboBox cmbCorteFin;
        private System.Windows.Forms.RadioButton rbFamiliaEstudios;
        private System.Windows.Forms.RadioButton rbDoctores;
        private System.Windows.Forms.RadioButton rbEstudios;
        private System.Windows.Forms.ComboBox cmbAnios;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtFechaI;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtFechaF;
        private System.Windows.Forms.Label label5;
    }
}