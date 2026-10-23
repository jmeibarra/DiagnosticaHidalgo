
namespace DiagnosticaHidalgo.Forms
{
    partial class fNotasTarjeta
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fNotasTarjeta));
            this.btnCargarArch = new System.Windows.Forms.Button();
            this.btnDescargarPlantilla = new System.Windows.Forms.Button();
            this.lblNombreArch = new System.Windows.Forms.Label();
            this.openFileDialog_Cargar = new System.Windows.Forms.OpenFileDialog();
            this.gvNotas = new System.Windows.Forms.DataGridView();
            this.pnlIndicaciones = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlFinal = new System.Windows.Forms.Panel();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.Estatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FolioReal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NuevoFolio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Paciente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estudios = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvNotas)).BeginInit();
            this.pnlIndicaciones.SuspendLayout();
            this.pnlFinal.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCargarArch
            // 
            this.btnCargarArch.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargarArch.Location = new System.Drawing.Point(3, 9);
            this.btnCargarArch.Name = "btnCargarArch";
            this.btnCargarArch.Size = new System.Drawing.Size(142, 37);
            this.btnCargarArch.TabIndex = 5;
            this.btnCargarArch.Text = "CARGAR NOTAS";
            this.btnCargarArch.UseVisualStyleBackColor = true;
            this.btnCargarArch.Click += new System.EventHandler(this.btnCargarArch_Click);
            // 
            // btnDescargarPlantilla
            // 
            this.btnDescargarPlantilla.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDescargarPlantilla.Location = new System.Drawing.Point(677, 80);
            this.btnDescargarPlantilla.Name = "btnDescargarPlantilla";
            this.btnDescargarPlantilla.Size = new System.Drawing.Size(212, 37);
            this.btnDescargarPlantilla.TabIndex = 6;
            this.btnDescargarPlantilla.Text = "DESCARGAR PLANTILLA";
            this.btnDescargarPlantilla.UseVisualStyleBackColor = true;
            this.btnDescargarPlantilla.Click += new System.EventHandler(this.btnDescargarPlantilla_Click);
            // 
            // lblNombreArch
            // 
            this.lblNombreArch.AutoSize = true;
            this.lblNombreArch.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreArch.Location = new System.Drawing.Point(151, 17);
            this.lblNombreArch.Name = "lblNombreArch";
            this.lblNombreArch.Size = new System.Drawing.Size(230, 21);
            this.lblNombreArch.TabIndex = 8;
            this.lblNombreArch.Text = "No se ha seleccionado archivo";
            // 
            // openFileDialog_Cargar
            // 
            this.openFileDialog_Cargar.FileName = "openFileDialog1";
            // 
            // gvNotas
            // 
            this.gvNotas.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvNotas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvNotas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvNotas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Estatus,
            this.Fecha,
            this.FolioReal,
            this.NuevoFolio,
            this.Paciente,
            this.Estudios,
            this.Total});
            this.gvNotas.Dock = System.Windows.Forms.DockStyle.Top;
            this.gvNotas.Location = new System.Drawing.Point(0, 0);
            this.gvNotas.MultiSelect = false;
            this.gvNotas.Name = "gvNotas";
            this.gvNotas.ReadOnly = true;
            this.gvNotas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gvNotas.Size = new System.Drawing.Size(901, 234);
            this.gvNotas.TabIndex = 9;
            this.gvNotas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvNotas_CellContentClick);
            // 
            // pnlIndicaciones
            // 
            this.pnlIndicaciones.Controls.Add(this.label3);
            this.pnlIndicaciones.Controls.Add(this.label2);
            this.pnlIndicaciones.Controls.Add(this.label1);
            this.pnlIndicaciones.Controls.Add(this.btnDescargarPlantilla);
            this.pnlIndicaciones.Controls.Add(this.lblNombreArch);
            this.pnlIndicaciones.Controls.Add(this.btnCargarArch);
            this.pnlIndicaciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlIndicaciones.Location = new System.Drawing.Point(0, 0);
            this.pnlIndicaciones.Name = "pnlIndicaciones";
            this.pnlIndicaciones.Size = new System.Drawing.Size(901, 133);
            this.pnlIndicaciones.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(437, 21);
            this.label3.TabIndex = 9;
            this.label3.Text = "2. NuevoFolio - Es el folio que se mostrará al imprimir la nota.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(308, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "1. FolioReal - Es el folio de la nota de venta.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(616, 21);
            this.label1.TabIndex = 9;
            this.label1.Text = "La plantilla debe ser un archivo tipo excel, donde la primer hoja contiene dos co" +
    "lumnas: ";
            // 
            // pnlFinal
            // 
            this.pnlFinal.Controls.Add(this.btnImprimir);
            this.pnlFinal.Controls.Add(this.gvNotas);
            this.pnlFinal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFinal.Location = new System.Drawing.Point(0, 139);
            this.pnlFinal.Name = "pnlFinal";
            this.pnlFinal.Size = new System.Drawing.Size(901, 303);
            this.pnlFinal.TabIndex = 11;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Location = new System.Drawing.Point(764, 254);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(125, 37);
            this.btnImprimir.TabIndex = 10;
            this.btnImprimir.Text = "IMPRIMIR";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // Estatus
            // 
            this.Estatus.HeaderText = "Estatus";
            this.Estatus.Name = "Estatus";
            this.Estatus.ReadOnly = true;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 130;
            // 
            // FolioReal
            // 
            this.FolioReal.HeaderText = "No. Nota real";
            this.FolioReal.Name = "FolioReal";
            this.FolioReal.ReadOnly = true;
            // 
            // NuevoFolio
            // 
            this.NuevoFolio.HeaderText = "No. Nota fiscal";
            this.NuevoFolio.Name = "NuevoFolio";
            this.NuevoFolio.ReadOnly = true;
            // 
            // Paciente
            // 
            this.Paciente.FillWeight = 150F;
            this.Paciente.HeaderText = "Paciente";
            this.Paciente.Name = "Paciente";
            this.Paciente.ReadOnly = true;
            this.Paciente.Width = 230;
            // 
            // Estudios
            // 
            this.Estudios.FillWeight = 200F;
            this.Estudios.HeaderText = "Estudios";
            this.Estudios.Name = "Estudios";
            this.Estudios.ReadOnly = true;
            this.Estudios.Width = 200;
            // 
            // Total
            // 
            this.Total.FillWeight = 80F;
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Width = 80;
            // 
            // fNotasTarjeta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(901, 442);
            this.Controls.Add(this.pnlFinal);
            this.Controls.Add(this.pnlIndicaciones);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "fNotasTarjeta";
            this.Text = "Notas pagadas con tarjeta";
            ((System.ComponentModel.ISupportInitialize)(this.gvNotas)).EndInit();
            this.pnlIndicaciones.ResumeLayout(false);
            this.pnlIndicaciones.PerformLayout();
            this.pnlFinal.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCargarArch;
        private System.Windows.Forms.Button btnDescargarPlantilla;
        private System.Windows.Forms.Label lblNombreArch;
        private System.Windows.Forms.OpenFileDialog openFileDialog_Cargar;
        private System.Windows.Forms.DataGridView gvNotas;
        private System.Windows.Forms.Panel pnlIndicaciones;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlFinal;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn FolioReal;
        private System.Windows.Forms.DataGridViewTextBoxColumn NuevoFolio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Paciente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estudios;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
    }
}