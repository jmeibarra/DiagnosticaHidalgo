
namespace DiagnosticaHidalgo.Forms
{
    partial class frmPago
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPago));
            this.gbInfPago = new System.Windows.Forms.GroupBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTipoPago = new System.Windows.Forms.ComboBox();
            this.lblPagoCon = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblMontoTotal = new System.Windows.Forms.Label();
            this.lblMontoPagado = new System.Windows.Forms.Label();
            this.lblMontoPendiente = new System.Windows.Forms.Label();
            this.gbMonto = new System.Windows.Forms.GroupBox();
            this.gvPagos = new System.Windows.Forms.DataGridView();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.gbPago = new System.Windows.Forms.GroupBox();
            this.gbInfPago.SuspendLayout();
            this.gbMonto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPagos)).BeginInit();
            this.gbPago.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbInfPago
            // 
            this.gbInfPago.Controls.Add(this.txtCantidad);
            this.gbInfPago.Controls.Add(this.label1);
            this.gbInfPago.Controls.Add(this.label3);
            this.gbInfPago.Controls.Add(this.cmbTipoPago);
            this.gbInfPago.Controls.Add(this.lblPagoCon);
            this.gbInfPago.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbInfPago.Location = new System.Drawing.Point(6, 28);
            this.gbInfPago.Name = "gbInfPago";
            this.gbInfPago.Size = new System.Drawing.Size(355, 184);
            this.gbInfPago.TabIndex = 0;
            this.gbInfPago.TabStop = false;
            this.gbInfPago.Text = "Información del pago";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Enabled = false;
            this.txtCantidad.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidad.Location = new System.Drawing.Point(178, 111);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCantidad.Size = new System.Drawing.Size(100, 29);
            this.txtCantidad.TabIndex = 2;
            this.txtCantidad.Text = "0.00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(153, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "$";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(68, 114);
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
            this.cmbTipoPago.Location = new System.Drawing.Point(157, 52);
            this.cmbTipoPago.Name = "cmbTipoPago";
            this.cmbTipoPago.Size = new System.Drawing.Size(179, 29);
            this.cmbTipoPago.TabIndex = 0;
            // 
            // lblPagoCon
            // 
            this.lblPagoCon.AutoSize = true;
            this.lblPagoCon.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPagoCon.Location = new System.Drawing.Point(66, 55);
            this.lblPagoCon.Name = "lblPagoCon";
            this.lblPagoCon.Size = new System.Drawing.Size(82, 21);
            this.lblPagoCon.TabIndex = 0;
            this.lblPagoCon.Text = "Pago con:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(31, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "Monto total a pagar:  $";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(66, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "Monto Pagado:   $";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(49, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "Monto pendiente:  $";
            // 
            // lblMontoTotal
            // 
            this.lblMontoTotal.AutoSize = true;
            this.lblMontoTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoTotal.Location = new System.Drawing.Point(215, 60);
            this.lblMontoTotal.Name = "lblMontoTotal";
            this.lblMontoTotal.Size = new System.Drawing.Size(77, 21);
            this.lblMontoTotal.TabIndex = 0;
            this.lblMontoTotal.Text = "         0.00";
            // 
            // lblMontoPagado
            // 
            this.lblMontoPagado.AutoSize = true;
            this.lblMontoPagado.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoPagado.Location = new System.Drawing.Point(216, 100);
            this.lblMontoPagado.Name = "lblMontoPagado";
            this.lblMontoPagado.Size = new System.Drawing.Size(77, 21);
            this.lblMontoPagado.TabIndex = 1;
            this.lblMontoPagado.Text = "         0.00";
            // 
            // lblMontoPendiente
            // 
            this.lblMontoPendiente.AutoSize = true;
            this.lblMontoPendiente.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoPendiente.Location = new System.Drawing.Point(215, 140);
            this.lblMontoPendiente.Name = "lblMontoPendiente";
            this.lblMontoPendiente.Size = new System.Drawing.Size(77, 21);
            this.lblMontoPendiente.TabIndex = 2;
            this.lblMontoPendiente.Text = "         0.00";
            // 
            // gbMonto
            // 
            this.gbMonto.Controls.Add(this.lblMontoPendiente);
            this.gbMonto.Controls.Add(this.lblMontoPagado);
            this.gbMonto.Controls.Add(this.lblMontoTotal);
            this.gbMonto.Controls.Add(this.label7);
            this.gbMonto.Controls.Add(this.label6);
            this.gbMonto.Controls.Add(this.label5);
            this.gbMonto.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbMonto.Location = new System.Drawing.Point(374, 28);
            this.gbMonto.Name = "gbMonto";
            this.gbMonto.Size = new System.Drawing.Size(314, 184);
            this.gbMonto.TabIndex = 1;
            this.gbMonto.TabStop = false;
            this.gbMonto.Text = "Montos de la venta con respecto al pago";
            // 
            // gvPagos
            // 
            this.gvPagos.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPagos.Dock = System.Windows.Forms.DockStyle.Top;
            this.gvPagos.Location = new System.Drawing.Point(0, 0);
            this.gvPagos.MultiSelect = false;
            this.gvPagos.Name = "gvPagos";
            this.gvPagos.ReadOnly = true;
            this.gvPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPagos.Size = new System.Drawing.Size(713, 150);
            this.gvPagos.TabIndex = 2;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(563, 229);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(125, 37);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "AGREGAR";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(432, 229);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(125, 37);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // gbPago
            // 
            this.gbPago.Controls.Add(this.gbInfPago);
            this.gbPago.Controls.Add(this.btnCancelar);
            this.gbPago.Controls.Add(this.gbMonto);
            this.gbPago.Controls.Add(this.btnAgregar);
            this.gbPago.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPago.Location = new System.Drawing.Point(12, 187);
            this.gbPago.Name = "gbPago";
            this.gbPago.Size = new System.Drawing.Size(695, 272);
            this.gbPago.TabIndex = 0;
            this.gbPago.TabStop = false;
            this.gbPago.Text = "PAGO";
            // 
            // frmPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(713, 479);
            this.Controls.Add(this.gbPago);
            this.Controls.Add(this.gvPagos);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AGREGAR PAGO";
            this.Load += new System.EventHandler(this.frmPago_Load);
            this.gbInfPago.ResumeLayout(false);
            this.gbInfPago.PerformLayout();
            this.gbMonto.ResumeLayout(false);
            this.gbMonto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPagos)).EndInit();
            this.gbPago.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbInfPago;
        private System.Windows.Forms.ComboBox cmbTipoPago;
        private System.Windows.Forms.Label lblPagoCon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label lblMontoTotal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMontoPagado;
        private System.Windows.Forms.Label lblMontoPendiente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbMonto;
        private System.Windows.Forms.DataGridView gvPagos;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox gbPago;
    }
}