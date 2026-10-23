
namespace DiagnosticaHidalgo.Forms
{
    partial class frmCorteTurno
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCorteTurno));
            this.gv = new System.Windows.Forms.DataGridView();
            this.gb = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTEfectivo = new System.Windows.Forms.Label();
            this.lblTCtas = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl08 = new System.Windows.Forms.Label();
            this.gvCtasMontos = new System.Windows.Forms.DataGridView();
            this.lblTotalCta = new System.Windows.Forms.Label();
            this.lblTotalEfectivo = new System.Windows.Forms.Label();
            this.lbl01 = new System.Windows.Forms.Label();
            this.btnVerOtrosPagos = new System.Windows.Forms.Button();
            this.btnVerPagos = new System.Windows.Forms.Button();
            this.tt = new System.Windows.Forms.ToolTip(this.components);
            this.btnImprimir = new System.Windows.Forms.Button();
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.txtDoctorBusqueda = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtEstudioBusqueda = new System.Windows.Forms.TextBox();
            this.lblDoctor = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombreBusqueda = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            this.gb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCtasMontos)).BeginInit();
            this.pnlFiltros.SuspendLayout();
            this.SuspendLayout();
            // 
            // gv
            // 
            this.gv.AllowUserToAddRows = false;
            this.gv.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv.Location = new System.Drawing.Point(0, 260);
            this.gv.MultiSelect = false;
            this.gv.Name = "gv";
            this.gv.ReadOnly = true;
            this.gv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv.Size = new System.Drawing.Size(856, 290);
            this.gv.TabIndex = 1;
            this.gv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_CellContentClick);
            this.gv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gv_CellFormatting);
            // 
            // gb
            // 
            this.gb.Controls.Add(this.lblTotal);
            this.gb.Controls.Add(this.lblTEfectivo);
            this.gb.Controls.Add(this.lblTCtas);
            this.gb.Controls.Add(this.lblFecha);
            this.gb.Controls.Add(this.label10);
            this.gb.Controls.Add(this.lbl08);
            this.gb.Controls.Add(this.gvCtasMontos);
            this.gb.Controls.Add(this.lblTotalCta);
            this.gb.Controls.Add(this.lblTotalEfectivo);
            this.gb.Controls.Add(this.lbl01);
            this.gb.Dock = System.Windows.Forms.DockStyle.Top;
            this.gb.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb.Location = new System.Drawing.Point(0, 0);
            this.gb.Name = "gb";
            this.gb.Size = new System.Drawing.Size(856, 211);
            this.gb.TabIndex = 2;
            this.gb.TabStop = false;
            this.gb.Text = "Datos del corte de turno";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(204, 164);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(51, 21);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "label1";
            // 
            // lblTEfectivo
            // 
            this.lblTEfectivo.AutoSize = true;
            this.lblTEfectivo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTEfectivo.Location = new System.Drawing.Point(204, 124);
            this.lblTEfectivo.Name = "lblTEfectivo";
            this.lblTEfectivo.Size = new System.Drawing.Size(51, 21);
            this.lblTEfectivo.TabIndex = 7;
            this.lblTEfectivo.Text = "label1";
            // 
            // lblTCtas
            // 
            this.lblTCtas.AutoSize = true;
            this.lblTCtas.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTCtas.Location = new System.Drawing.Point(204, 84);
            this.lblTCtas.Name = "lblTCtas";
            this.lblTCtas.Size = new System.Drawing.Size(51, 21);
            this.lblTCtas.TabIndex = 7;
            this.lblTCtas.Text = "label1";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(204, 44);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(54, 21);
            this.lblFecha.TabIndex = 6;
            this.lblFecha.Text = "label2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(13, 164);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(185, 21);
            this.label10.TabIndex = 5;
            this.label10.Text = "Monto total del corte  $";
            // 
            // lbl08
            // 
            this.lbl08.AutoSize = true;
            this.lbl08.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl08.Location = new System.Drawing.Point(413, 25);
            this.lbl08.Name = "lbl08";
            this.lbl08.Size = new System.Drawing.Size(185, 21);
            this.lbl08.TabIndex = 4;
            this.lbl08.Text = "Monto total por cuenta.";
            // 
            // gvCtasMontos
            // 
            this.gvCtasMontos.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gvCtasMontos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCtasMontos.Location = new System.Drawing.Point(417, 56);
            this.gvCtasMontos.Name = "gvCtasMontos";
            this.gvCtasMontos.ReadOnly = true;
            this.gvCtasMontos.Size = new System.Drawing.Size(399, 105);
            this.gvCtasMontos.TabIndex = 3;
            // 
            // lblTotalCta
            // 
            this.lblTotalCta.AutoSize = true;
            this.lblTotalCta.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCta.Location = new System.Drawing.Point(39, 84);
            this.lblTotalCta.Name = "lblTotalCta";
            this.lblTotalCta.Size = new System.Drawing.Size(159, 21);
            this.lblTotalCta.TabIndex = 1;
            this.lblTotalCta.Text = "Monto en cuentas  $";
            // 
            // lblTotalEfectivo
            // 
            this.lblTotalEfectivo.AutoSize = true;
            this.lblTotalEfectivo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalEfectivo.Location = new System.Drawing.Point(35, 124);
            this.lblTotalEfectivo.Name = "lblTotalEfectivo";
            this.lblTotalEfectivo.Size = new System.Drawing.Size(163, 21);
            this.lblTotalEfectivo.TabIndex = 0;
            this.lblTotalEfectivo.Text = "Monto en efectivo  $";
            // 
            // lbl01
            // 
            this.lbl01.AutoSize = true;
            this.lbl01.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl01.Location = new System.Drawing.Point(27, 44);
            this.lbl01.Name = "lbl01";
            this.lbl01.Size = new System.Drawing.Size(171, 21);
            this.lbl01.TabIndex = 0;
            this.lbl01.Text = "Fecha y hora de corte:";
            // 
            // btnVerOtrosPagos
            // 
            this.btnVerOtrosPagos.Location = new System.Drawing.Point(545, 495);
            this.btnVerOtrosPagos.Name = "btnVerOtrosPagos";
            this.btnVerOtrosPagos.Size = new System.Drawing.Size(142, 43);
            this.btnVerOtrosPagos.TabIndex = 2;
            this.btnVerOtrosPagos.Text = "Ver otros pagos";
            this.btnVerOtrosPagos.UseVisualStyleBackColor = true;
            this.btnVerOtrosPagos.Visible = false;
            this.btnVerOtrosPagos.Click += new System.EventHandler(this.btnVerOtrosPagos_Click);
            // 
            // btnVerPagos
            // 
            this.btnVerPagos.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerPagos.Location = new System.Drawing.Point(708, 217);
            this.btnVerPagos.Name = "btnVerPagos";
            this.btnVerPagos.Size = new System.Drawing.Size(136, 37);
            this.btnVerPagos.TabIndex = 3;
            this.btnVerPagos.Text = "Ver pagos";
            this.btnVerPagos.UseVisualStyleBackColor = true;
            this.btnVerPagos.Click += new System.EventHandler(this.btnVerPagos_Click_);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Location = new System.Drawing.Point(566, 217);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(136, 37);
            this.btnImprimir.TabIndex = 3;
            this.btnImprimir.Text = "IMPRIMIR";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.Controls.Add(this.txtDoctorBusqueda);
            this.pnlFiltros.Controls.Add(this.btnBuscar);
            this.pnlFiltros.Controls.Add(this.txtEstudioBusqueda);
            this.pnlFiltros.Controls.Add(this.lblDoctor);
            this.pnlFiltros.Controls.Add(this.label1);
            this.pnlFiltros.Controls.Add(this.txtNombreBusqueda);
            this.pnlFiltros.Controls.Add(this.label9);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Location = new System.Drawing.Point(0, 211);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Size = new System.Drawing.Size(856, 150);
            this.pnlFiltros.TabIndex = 4;
            this.pnlFiltros.Visible = false;
            // 
            // txtDoctorBusqueda
            // 
            this.txtDoctorBusqueda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDoctorBusqueda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDoctorBusqueda.Location = new System.Drawing.Point(195, 112);
            this.txtDoctorBusqueda.Name = "txtDoctorBusqueda";
            this.txtDoctorBusqueda.Size = new System.Drawing.Size(470, 29);
            this.txtDoctorBusqueda.TabIndex = 23;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(695, 107);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(161, 37);
            this.btnBuscar.TabIndex = 22;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtEstudioBusqueda
            // 
            this.txtEstudioBusqueda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEstudioBusqueda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstudioBusqueda.Location = new System.Drawing.Point(195, 66);
            this.txtEstudioBusqueda.Name = "txtEstudioBusqueda";
            this.txtEstudioBusqueda.Size = new System.Drawing.Size(470, 29);
            this.txtEstudioBusqueda.TabIndex = 21;
            // 
            // lblDoctor
            // 
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoctor.Location = new System.Drawing.Point(118, 115);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(67, 21);
            this.lblDoctor.TabIndex = 20;
            this.lblDoctor.Text = "Doctor:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(114, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 21);
            this.label1.TabIndex = 20;
            this.label1.Text = "Estudio:";
            // 
            // txtNombreBusqueda
            // 
            this.txtNombreBusqueda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombreBusqueda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreBusqueda.Location = new System.Drawing.Point(195, 22);
            this.txtNombreBusqueda.Name = "txtNombreBusqueda";
            this.txtNombreBusqueda.Size = new System.Drawing.Size(470, 29);
            this.txtNombreBusqueda.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(15, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(170, 21);
            this.label9.TabIndex = 18;
            this.label9.Text = "Nombre de paciente:";
            // 
            // frmCorteTurno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(856, 550);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnVerPagos);
            this.Controls.Add(this.gb);
            this.Controls.Add(this.gv);
            this.Controls.Add(this.btnVerOtrosPagos);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmCorteTurno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CORTE DE TURNO";
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            this.gb.ResumeLayout(false);
            this.gb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCtasMontos)).EndInit();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gv;
        private System.Windows.Forms.GroupBox gb;
        private System.Windows.Forms.Label lblTotalEfectivo;
        private System.Windows.Forms.Label lbl01;
        private System.Windows.Forms.Label lblTotalCta;
        private System.Windows.Forms.Button btnVerOtrosPagos;
        private System.Windows.Forms.Label lbl08;
        private System.Windows.Forms.DataGridView gvCtasMontos;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnVerPagos;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTEfectivo;
        private System.Windows.Forms.Label lblTCtas;
        private System.Windows.Forms.ToolTip tt;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.TextBox txtNombreBusqueda;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEstudioBusqueda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtDoctorBusqueda;
        private System.Windows.Forms.Label lblDoctor;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}