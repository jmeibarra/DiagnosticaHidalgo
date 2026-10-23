
namespace DiagnosticaHidalgo.Forms
{
    partial class frmDevolucion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDevolucion));
            this.gv = new System.Windows.Forms.DataGridView();
            this.gbBusqueda = new System.Windows.Forms.GroupBox();
            this.txtDoctorBusqueda = new System.Windows.Forms.TextBox();
            this.txtNombreBusqueda = new System.Windows.Forms.TextBox();
            this.checkFechas = new System.Windows.Forms.CheckBox();
            this.checkDoctor = new System.Windows.Forms.CheckBox();
            this.checkCliente = new System.Windows.Forms.CheckBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.lblDesde = new System.Windows.Forms.Label();
            this.dtpFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.gbInfPago = new System.Windows.Forms.GroupBox();
            this.btnPagar = new System.Windows.Forms.Button();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTipoPago = new System.Windows.Forms.ComboBox();
            this.lblPagoCon = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            this.gbBusqueda.SuspendLayout();
            this.gbInfPago.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gv
            // 
            this.gv.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gv.Location = new System.Drawing.Point(0, 302);
            this.gv.MultiSelect = false;
            this.gv.Name = "gv";
            this.gv.ReadOnly = true;
            this.gv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv.Size = new System.Drawing.Size(834, 277);
            this.gv.TabIndex = 3;
            this.gv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_CellContentClick);
            // 
            // gbBusqueda
            // 
            this.gbBusqueda.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gbBusqueda.Controls.Add(this.txtDoctorBusqueda);
            this.gbBusqueda.Controls.Add(this.txtNombreBusqueda);
            this.gbBusqueda.Controls.Add(this.checkFechas);
            this.gbBusqueda.Controls.Add(this.checkDoctor);
            this.gbBusqueda.Controls.Add(this.checkCliente);
            this.gbBusqueda.Controls.Add(this.btnBuscar);
            this.gbBusqueda.Controls.Add(this.lblHasta);
            this.gbBusqueda.Controls.Add(this.dtpFechaDesde);
            this.gbBusqueda.Controls.Add(this.lblDesde);
            this.gbBusqueda.Controls.Add(this.dtpFechaHasta);
            this.gbBusqueda.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbBusqueda.Location = new System.Drawing.Point(3, 3);
            this.gbBusqueda.Name = "gbBusqueda";
            this.gbBusqueda.Size = new System.Drawing.Size(443, 268);
            this.gbBusqueda.TabIndex = 0;
            this.gbBusqueda.TabStop = false;
            this.gbBusqueda.Text = "Filtros de búsqueda de ventas";
            // 
            // txtDoctorBusqueda
            // 
            this.txtDoctorBusqueda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDoctorBusqueda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDoctorBusqueda.Location = new System.Drawing.Point(92, 70);
            this.txtDoctorBusqueda.Name = "txtDoctorBusqueda";
            this.txtDoctorBusqueda.Size = new System.Drawing.Size(332, 29);
            this.txtDoctorBusqueda.TabIndex = 22;
            this.txtDoctorBusqueda.TextChanged += new System.EventHandler(this.txtDoctorBusqueda_TextChanged);
            // 
            // txtNombreBusqueda
            // 
            this.txtNombreBusqueda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombreBusqueda.Enabled = false;
            this.txtNombreBusqueda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreBusqueda.Location = new System.Drawing.Point(92, 30);
            this.txtNombreBusqueda.Name = "txtNombreBusqueda";
            this.txtNombreBusqueda.Size = new System.Drawing.Size(332, 29);
            this.txtNombreBusqueda.TabIndex = 21;
            this.txtNombreBusqueda.TextChanged += new System.EventHandler(this.txtNombreBusqueda_TextChanged);
            // 
            // checkFechas
            // 
            this.checkFechas.AutoSize = true;
            this.checkFechas.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkFechas.Location = new System.Drawing.Point(9, 112);
            this.checkFechas.Name = "checkFechas";
            this.checkFechas.Size = new System.Drawing.Size(78, 25);
            this.checkFechas.TabIndex = 2;
            this.checkFechas.Text = "Fechas";
            this.checkFechas.UseVisualStyleBackColor = true;
            this.checkFechas.CheckedChanged += new System.EventHandler(this.checkFechas_CheckedChanged);
            // 
            // checkDoctor
            // 
            this.checkDoctor.AutoSize = true;
            this.checkDoctor.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkDoctor.Location = new System.Drawing.Point(9, 72);
            this.checkDoctor.Name = "checkDoctor";
            this.checkDoctor.Size = new System.Drawing.Size(80, 25);
            this.checkDoctor.TabIndex = 20;
            this.checkDoctor.Text = "Doctor";
            this.checkDoctor.UseVisualStyleBackColor = true;
            this.checkDoctor.CheckedChanged += new System.EventHandler(this.checkDoctor_CheckedChanged);
            // 
            // checkCliente
            // 
            this.checkCliente.AutoSize = true;
            this.checkCliente.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkCliente.Location = new System.Drawing.Point(9, 32);
            this.checkCliente.Name = "checkCliente";
            this.checkCliente.Size = new System.Drawing.Size(80, 25);
            this.checkCliente.TabIndex = 20;
            this.checkCliente.Text = "Cliente";
            this.checkCliente.UseVisualStyleBackColor = true;
            this.checkCliente.CheckedChanged += new System.EventHandler(this.checkCliente_CheckedChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(312, 225);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(125, 37);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHasta.Location = new System.Drawing.Point(69, 186);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(55, 21);
            this.lblHasta.TabIndex = 13;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpFechaDesde
            // 
            this.dtpFechaDesde.Enabled = false;
            this.dtpFechaDesde.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaDesde.Location = new System.Drawing.Point(127, 140);
            this.dtpFechaDesde.Name = "dtpFechaDesde";
            this.dtpFechaDesde.Size = new System.Drawing.Size(298, 29);
            this.dtpFechaDesde.TabIndex = 3;
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesde.Location = new System.Drawing.Point(65, 146);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(60, 21);
            this.lblDesde.TabIndex = 12;
            this.lblDesde.Text = "Desde:";
            // 
            // dtpFechaHasta
            // 
            this.dtpFechaHasta.Enabled = false;
            this.dtpFechaHasta.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaHasta.Location = new System.Drawing.Point(127, 180);
            this.dtpFechaHasta.Name = "dtpFechaHasta";
            this.dtpFechaHasta.Size = new System.Drawing.Size(298, 29);
            this.dtpFechaHasta.TabIndex = 4;
            // 
            // gbInfPago
            // 
            this.gbInfPago.Controls.Add(this.btnPagar);
            this.gbInfPago.Controls.Add(this.txtCantidad);
            this.gbInfPago.Controls.Add(this.label1);
            this.gbInfPago.Controls.Add(this.label3);
            this.gbInfPago.Controls.Add(this.cmbTipoPago);
            this.gbInfPago.Controls.Add(this.lblPagoCon);
            this.gbInfPago.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbInfPago.Location = new System.Drawing.Point(452, 3);
            this.gbInfPago.Name = "gbInfPago";
            this.gbInfPago.Size = new System.Drawing.Size(374, 268);
            this.gbInfPago.TabIndex = 1;
            this.gbInfPago.TabStop = false;
            this.gbInfPago.Text = "Información de la devolución";
            // 
            // btnPagar
            // 
            this.btnPagar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagar.Location = new System.Drawing.Point(243, 225);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(125, 37);
            this.btnPagar.TabIndex = 3;
            this.btnPagar.Text = "ACEPTAR";
            this.btnPagar.UseVisualStyleBackColor = true;
            this.btnPagar.Click += new System.EventHandler(this.btnPagar_Click);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidad.Location = new System.Drawing.Point(192, 138);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCantidad.Size = new System.Drawing.Size(158, 29);
            this.txtCantidad.TabIndex = 2;
            this.txtCantidad.Text = "0.00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(167, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "$";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(82, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cantidad:";
            // 
            // cmbTipoPago
            // 
            this.cmbTipoPago.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoPago.FormattingEnabled = true;
            this.cmbTipoPago.Items.AddRange(new object[] {
            "Efectivo",
            "Tarjeta"});
            this.cmbTipoPago.Location = new System.Drawing.Point(171, 57);
            this.cmbTipoPago.Name = "cmbTipoPago";
            this.cmbTipoPago.Size = new System.Drawing.Size(179, 29);
            this.cmbTipoPago.TabIndex = 0;
            // 
            // lblPagoCon
            // 
            this.lblPagoCon.AutoSize = true;
            this.lblPagoCon.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPagoCon.Location = new System.Drawing.Point(31, 60);
            this.lblPagoCon.Name = "lblPagoCon";
            this.lblPagoCon.Size = new System.Drawing.Size(127, 21);
            this.lblPagoCon.TabIndex = 0;
            this.lblPagoCon.Text = "Devolución con:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbBusqueda);
            this.panel1.Controls.Add(this.gbInfPago);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(834, 280);
            this.panel1.TabIndex = 0;
            // 
            // frmDevolucion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(834, 579);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gv);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmDevolucion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Devolución";
            this.Load += new System.EventHandler(this.frmDevolucion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            this.gbBusqueda.ResumeLayout(false);
            this.gbBusqueda.PerformLayout();
            this.gbInfPago.ResumeLayout(false);
            this.gbInfPago.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gv;
        private System.Windows.Forms.GroupBox gbBusqueda;
        private System.Windows.Forms.GroupBox gbInfPago;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTipoPago;
        private System.Windows.Forms.Label lblPagoCon;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpFechaDesde;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpFechaHasta;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPagar;
        private System.Windows.Forms.CheckBox checkFechas;
        private System.Windows.Forms.CheckBox checkDoctor;
        private System.Windows.Forms.CheckBox checkCliente;
        private System.Windows.Forms.TextBox txtNombreBusqueda;
        private System.Windows.Forms.TextBox txtDoctorBusqueda;
    }
}