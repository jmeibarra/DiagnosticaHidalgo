
namespace DiagnosticaHidalgo.Forms
{
    partial class frmPagoCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPagoCliente));
            this.gbBusqueda = new System.Windows.Forms.GroupBox();
            this.gbInfPago = new System.Windows.Forms.GroupBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTipoPago = new System.Windows.Forms.ComboBox();
            this.lblPagoCon = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbBusqueda = new System.Windows.Forms.ComboBox();
            this.gv = new System.Windows.Forms.DataGridView();
            this.lblCtdGastada = new System.Windows.Forms.Label();
            this.lblMontoGastado = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblRestoPago = new System.Windows.Forms.Label();
            this.lblPagoParcial = new System.Windows.Forms.Label();
            this.btnPagar = new System.Windows.Forms.Button();
            this.gbBusqueda.SuspendLayout();
            this.gbInfPago.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            this.SuspendLayout();
            // 
            // gbBusqueda
            // 
            this.gbBusqueda.Controls.Add(this.gbInfPago);
            this.gbBusqueda.Controls.Add(this.label9);
            this.gbBusqueda.Controls.Add(this.cmbBusqueda);
            this.gbBusqueda.Location = new System.Drawing.Point(1, 0);
            this.gbBusqueda.Name = "gbBusqueda";
            this.gbBusqueda.Size = new System.Drawing.Size(453, 285);
            this.gbBusqueda.TabIndex = 0;
            this.gbBusqueda.TabStop = false;
            // 
            // gbInfPago
            // 
            this.gbInfPago.Controls.Add(this.txtCantidad);
            this.gbInfPago.Controls.Add(this.label1);
            this.gbInfPago.Controls.Add(this.label3);
            this.gbInfPago.Controls.Add(this.label2);
            this.gbInfPago.Controls.Add(this.cmbTipoPago);
            this.gbInfPago.Controls.Add(this.lblPagoCon);
            this.gbInfPago.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbInfPago.Location = new System.Drawing.Point(17, 72);
            this.gbInfPago.Name = "gbInfPago";
            this.gbInfPago.Size = new System.Drawing.Size(409, 184);
            this.gbInfPago.TabIndex = 17;
            this.gbInfPago.TabStop = false;
            this.gbInfPago.Text = "Información del pago";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidad.Location = new System.Drawing.Point(191, 122);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCantidad.Size = new System.Drawing.Size(100, 29);
            this.txtCantidad.TabIndex = 2;
            this.txtCantidad.Text = "0.00";
            this.txtCantidad.TextChanged += new System.EventHandler(this.txtCantidad_TextChanged);
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(166, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "$";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(81, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cantidad:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = " ";
            // 
            // cmbTipoPago
            // 
            this.cmbTipoPago.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoPago.FormattingEnabled = true;
            this.cmbTipoPago.Items.AddRange(new object[] {
            "Efectivo",
            "Tarjeta"});
            this.cmbTipoPago.Location = new System.Drawing.Point(171, 56);
            this.cmbTipoPago.Name = "cmbTipoPago";
            this.cmbTipoPago.Size = new System.Drawing.Size(179, 29);
            this.cmbTipoPago.TabIndex = 0;
            // 
            // lblPagoCon
            // 
            this.lblPagoCon.AutoSize = true;
            this.lblPagoCon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPagoCon.Location = new System.Drawing.Point(80, 59);
            this.lblPagoCon.Name = "lblPagoCon";
            this.lblPagoCon.Size = new System.Drawing.Size(85, 21);
            this.lblPagoCon.TabIndex = 0;
            this.lblPagoCon.Text = "Pago con:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(13, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 21);
            this.label9.TabIndex = 16;
            this.label9.Text = "Cliente:";
            // 
            // cmbBusqueda
            // 
            this.cmbBusqueda.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBusqueda.FormattingEnabled = true;
            this.cmbBusqueda.Location = new System.Drawing.Point(80, 19);
            this.cmbBusqueda.Name = "cmbBusqueda";
            this.cmbBusqueda.Size = new System.Drawing.Size(346, 29);
            this.cmbBusqueda.TabIndex = 0;
            this.cmbBusqueda.SelectedIndexChanged += new System.EventHandler(this.cmbBusqueda_SelectedIndexChanged);
            // 
            // gv
            // 
            this.gv.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv.Location = new System.Drawing.Point(471, 107);
            this.gv.MultiSelect = false;
            this.gv.Name = "gv";
            this.gv.ReadOnly = true;
            this.gv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv.Size = new System.Drawing.Size(615, 138);
            this.gv.TabIndex = 2;
            this.gv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_CellContentClick);
            // 
            // lblCtdGastada
            // 
            this.lblCtdGastada.AutoSize = true;
            this.lblCtdGastada.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCtdGastada.Location = new System.Drawing.Point(473, 61);
            this.lblCtdGastada.Name = "lblCtdGastada";
            this.lblCtdGastada.Size = new System.Drawing.Size(134, 21);
            this.lblCtdGastada.TabIndex = 3;
            this.lblCtdGastada.Text = "Resto de pago  $";
            // 
            // lblMontoGastado
            // 
            this.lblMontoGastado.AutoSize = true;
            this.lblMontoGastado.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoGastado.Location = new System.Drawing.Point(605, 22);
            this.lblMontoGastado.Name = "lblMontoGastado";
            this.lblMontoGastado.Size = new System.Drawing.Size(32, 21);
            this.lblMontoGastado.TabIndex = 4;
            this.lblMontoGastado.Text = "0.0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(467, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "Monto gastado  $";
            // 
            // lblRestoPago
            // 
            this.lblRestoPago.AutoSize = true;
            this.lblRestoPago.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRestoPago.Location = new System.Drawing.Point(605, 61);
            this.lblRestoPago.Name = "lblRestoPago";
            this.lblRestoPago.Size = new System.Drawing.Size(32, 21);
            this.lblRestoPago.TabIndex = 5;
            this.lblRestoPago.Text = "0.0";
            // 
            // lblPagoParcial
            // 
            this.lblPagoParcial.AutoSize = true;
            this.lblPagoParcial.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPagoParcial.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblPagoParcial.Location = new System.Drawing.Point(778, 22);
            this.lblPagoParcial.Name = "lblPagoParcial";
            this.lblPagoParcial.Size = new System.Drawing.Size(161, 21);
            this.lblPagoParcial.TabIndex = 6;
            this.lblPagoParcial.Text = "Se agregará un pago";
            this.lblPagoParcial.Visible = false;
            // 
            // btnPagar
            // 
            this.btnPagar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagar.Location = new System.Drawing.Point(961, 251);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(125, 37);
            this.btnPagar.TabIndex = 1;
            this.btnPagar.Text = "PAGAR";
            this.btnPagar.UseVisualStyleBackColor = true;
            this.btnPagar.Click += new System.EventHandler(this.btnPagar_Click);
            // 
            // frmPagoCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1098, 293);
            this.Controls.Add(this.btnPagar);
            this.Controls.Add(this.lblPagoParcial);
            this.Controls.Add(this.lblRestoPago);
            this.Controls.Add(this.lblMontoGastado);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblCtdGastada);
            this.Controls.Add(this.gv);
            this.Controls.Add(this.gbBusqueda);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmPagoCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AGREGAR PAGO DE CLIENTE";
            this.Load += new System.EventHandler(this.frmPagoCliente_Load);
            this.gbBusqueda.ResumeLayout(false);
            this.gbBusqueda.PerformLayout();
            this.gbInfPago.ResumeLayout(false);
            this.gbInfPago.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbBusqueda;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbBusqueda;
        private System.Windows.Forms.DataGridView gv;
        private System.Windows.Forms.GroupBox gbInfPago;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTipoPago;
        private System.Windows.Forms.Label lblPagoCon;
        private System.Windows.Forms.Label lblCtdGastada;
        private System.Windows.Forms.Label lblMontoGastado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblRestoPago;
        private System.Windows.Forms.Label lblPagoParcial;
        private System.Windows.Forms.Button btnPagar;
    }
}