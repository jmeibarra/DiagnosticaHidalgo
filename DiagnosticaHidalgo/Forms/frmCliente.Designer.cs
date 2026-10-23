
namespace DiagnosticaHidalgo
{
    partial class frmCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCliente));
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.gbEdad = new System.Windows.Forms.GroupBox();
            this.cmbDias = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbMeses = new System.Windows.Forms.ComboBox();
            this.chkAnios = new System.Windows.Forms.CheckBox();
            this.txtAnios = new System.Windows.Forms.TextBox();
            this.chkMesesDias = new System.Windows.Forms.CheckBox();
            this.rbHombre = new System.Windows.Forms.RadioButton();
            this.rbMujer = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCelular2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCelular1 = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.gbBusqueda = new System.Windows.Forms.GroupBox();
            this.txtNombreBusqueda = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.lblIns1 = new System.Windows.Forms.Label();
            this.lblIns2 = new System.Windows.Forms.Label();
            this.gbDatos.SuspendLayout();
            this.gbEdad.SuspendLayout();
            this.gbBusqueda.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(456, 513);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(125, 37);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.AccessibleName = "btnBuscar";
            this.btnAceptar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(587, 513);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(125, 37);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "AGREGAR";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // gbDatos
            // 
            this.gbDatos.Controls.Add(this.gbEdad);
            this.gbDatos.Controls.Add(this.rbHombre);
            this.gbDatos.Controls.Add(this.rbMujer);
            this.gbDatos.Controls.Add(this.label10);
            this.gbDatos.Controls.Add(this.txtCelular2);
            this.gbDatos.Controls.Add(this.label8);
            this.gbDatos.Controls.Add(this.label1);
            this.gbDatos.Controls.Add(this.txtCelular1);
            this.gbDatos.Controls.Add(this.txtEmail);
            this.gbDatos.Controls.Add(this.label2);
            this.gbDatos.Controls.Add(this.label4);
            this.gbDatos.Controls.Add(this.label5);
            this.gbDatos.Controls.Add(this.txtNombre);
            this.gbDatos.Enabled = false;
            this.gbDatos.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDatos.Location = new System.Drawing.Point(16, 207);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(696, 300);
            this.gbDatos.TabIndex = 1;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos Generales";
            // 
            // gbEdad
            // 
            this.gbEdad.Controls.Add(this.cmbDias);
            this.gbEdad.Controls.Add(this.label6);
            this.gbEdad.Controls.Add(this.cmbMeses);
            this.gbEdad.Controls.Add(this.chkAnios);
            this.gbEdad.Controls.Add(this.txtAnios);
            this.gbEdad.Controls.Add(this.chkMesesDias);
            this.gbEdad.Location = new System.Drawing.Point(19, 110);
            this.gbEdad.Name = "gbEdad";
            this.gbEdad.Size = new System.Drawing.Size(659, 69);
            this.gbEdad.TabIndex = 3;
            this.gbEdad.TabStop = false;
            this.gbEdad.Text = "Edad";
            // 
            // cmbDias
            // 
            this.cmbDias.FormattingEnabled = true;
            this.cmbDias.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.cmbDias.Location = new System.Drawing.Point(500, 25);
            this.cmbDias.Name = "cmbDias";
            this.cmbDias.Size = new System.Drawing.Size(46, 29);
            this.cmbDias.TabIndex = 2;
            this.cmbDias.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbMeses_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(454, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 21);
            this.label6.TabIndex = 20;
            this.label6.Text = "Días";
            // 
            // cmbMeses
            // 
            this.cmbMeses.FormattingEnabled = true;
            this.cmbMeses.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11"});
            this.cmbMeses.Location = new System.Drawing.Point(391, 25);
            this.cmbMeses.Name = "cmbMeses";
            this.cmbMeses.Size = new System.Drawing.Size(49, 29);
            this.cmbMeses.TabIndex = 1;
            this.cmbMeses.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbMeses_KeyPress);
            // 
            // chkAnios
            // 
            this.chkAnios.AutoSize = true;
            this.chkAnios.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAnios.Location = new System.Drawing.Point(94, 27);
            this.chkAnios.Name = "chkAnios";
            this.chkAnios.Size = new System.Drawing.Size(64, 25);
            this.chkAnios.TabIndex = 0;
            this.chkAnios.Text = "Años";
            this.chkAnios.UseVisualStyleBackColor = true;
            this.chkAnios.CheckedChanged += new System.EventHandler(this.chkAnios_CheckedChanged);
            // 
            // txtAnios
            // 
            this.txtAnios.Location = new System.Drawing.Point(164, 25);
            this.txtAnios.MaxLength = 2;
            this.txtAnios.Name = "txtAnios";
            this.txtAnios.Size = new System.Drawing.Size(45, 29);
            this.txtAnios.TabIndex = 0;
            this.txtAnios.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAnios_KeyPress);
            // 
            // chkMesesDias
            // 
            this.chkMesesDias.AutoSize = true;
            this.chkMesesDias.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMesesDias.Location = new System.Drawing.Point(312, 27);
            this.chkMesesDias.Name = "chkMesesDias";
            this.chkMesesDias.Size = new System.Drawing.Size(73, 25);
            this.chkMesesDias.TabIndex = 2;
            this.chkMesesDias.Text = "Meses";
            this.chkMesesDias.UseVisualStyleBackColor = true;
            this.chkMesesDias.CheckedChanged += new System.EventHandler(this.chkMesesDias_CheckedChanged);
            // 
            // rbHombre
            // 
            this.rbHombre.AutoSize = true;
            this.rbHombre.Location = new System.Drawing.Point(308, 76);
            this.rbHombre.Name = "rbHombre";
            this.rbHombre.Size = new System.Drawing.Size(92, 25);
            this.rbHombre.TabIndex = 2;
            this.rbHombre.TabStop = true;
            this.rbHombre.Text = "Hombre";
            this.rbHombre.UseVisualStyleBackColor = true;
            // 
            // rbMujer
            // 
            this.rbMujer.AutoSize = true;
            this.rbMujer.Location = new System.Drawing.Point(197, 76);
            this.rbMujer.Name = "rbMujer";
            this.rbMujer.Size = new System.Drawing.Size(75, 25);
            this.rbMujer.TabIndex = 1;
            this.rbMujer.TabStop = true;
            this.rbMujer.Text = "Mujer";
            this.rbMujer.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(224, 253);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 21);
            this.label10.TabIndex = 14;
            this.label10.Text = "-";
            // 
            // txtCelular2
            // 
            this.txtCelular2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCelular2.Location = new System.Drawing.Point(246, 250);
            this.txtCelular2.MaxLength = 7;
            this.txtCelular2.Name = "txtCelular2";
            this.txtCelular2.Size = new System.Drawing.Size(81, 29);
            this.txtCelular2.TabIndex = 6;
            this.txtCelular2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCelular2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCelular2_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(333, 256);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 17);
            this.label8.TabIndex = 4;
            this.label8.Text = "10 Dígitos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(23, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre completo:";
            // 
            // txtCelular1
            // 
            this.txtCelular1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCelular1.Location = new System.Drawing.Point(183, 250);
            this.txtCelular1.MaxLength = 3;
            this.txtCelular1.Name = "txtCelular1";
            this.txtCelular1.Size = new System.Drawing.Size(37, 29);
            this.txtCelular1.TabIndex = 5;
            this.txtCelular1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCelular1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCelular1_KeyPress);
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(183, 200);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(495, 29);
            this.txtEmail.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(126, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Sexo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(126, 203);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "Email:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(54, 253);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "Teléfono celular:";
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.txtNombre.Location = new System.Drawing.Point(183, 30);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(495, 29);
            this.txtNombre.TabIndex = 0;
            // 
            // gbBusqueda
            // 
            this.gbBusqueda.Controls.Add(this.txtNombreBusqueda);
            this.gbBusqueda.Controls.Add(this.btnAgregar);
            this.gbBusqueda.Controls.Add(this.label9);
            this.gbBusqueda.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbBusqueda.Location = new System.Drawing.Point(16, 69);
            this.gbBusqueda.Name = "gbBusqueda";
            this.gbBusqueda.Size = new System.Drawing.Size(696, 116);
            this.gbBusqueda.TabIndex = 0;
            this.gbBusqueda.TabStop = false;
            this.gbBusqueda.Text = "Filtro de búsqueda";
            // 
            // txtNombreBusqueda
            // 
            this.txtNombreBusqueda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombreBusqueda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreBusqueda.Location = new System.Drawing.Point(118, 35);
            this.txtNombreBusqueda.Name = "txtNombreBusqueda";
            this.txtNombreBusqueda.Size = new System.Drawing.Size(503, 29);
            this.txtNombreBusqueda.TabIndex = 17;
            this.txtNombreBusqueda.TextChanged += new System.EventHandler(this.txtNombreBusqueda_TextChanged);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(529, 70);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(161, 37);
            this.btnAgregar.TabIndex = 1;
            this.btnAgregar.Text = "NUEVO CLIENTE";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(27, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 21);
            this.label9.TabIndex = 16;
            this.label9.Text = "Nombre:";
            // 
            // lblIns1
            // 
            this.lblIns1.AllowDrop = true;
            this.lblIns1.AutoSize = true;
            this.lblIns1.Location = new System.Drawing.Point(12, 9);
            this.lblIns1.Name = "lblIns1";
            this.lblIns1.Size = new System.Drawing.Size(594, 21);
            this.lblIns1.TabIndex = 14;
            this.lblIns1.Text = "* Si el usuario no existe, se habilitará el formulario para poder ingresar sus da" +
    "tos por";
            // 
            // lblIns2
            // 
            this.lblIns2.AllowDrop = true;
            this.lblIns2.AutoSize = true;
            this.lblIns2.Location = new System.Drawing.Point(22, 32);
            this.lblIns2.Name = "lblIns2";
            this.lblIns2.Size = new System.Drawing.Size(95, 21);
            this.lblIns2.TabIndex = 14;
            this.lblIns2.Text = "primera vez.";
            // 
            // frmCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(722, 565);
            this.Controls.Add(this.lblIns2);
            this.Controls.Add(this.lblIns1);
            this.Controls.Add(this.gbBusqueda);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.gbDatos);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.DarkCyan;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CLIENTES";
            this.Load += new System.EventHandler(this.frmCliente_Load);
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            this.gbEdad.ResumeLayout(false);
            this.gbEdad.PerformLayout();
            this.gbBusqueda.ResumeLayout(false);
            this.gbBusqueda.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCelular1;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.GroupBox gbBusqueda;
        private System.Windows.Forms.Label lblIns1;
        private System.Windows.Forms.Label lblIns2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCelular2;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.RadioButton rbHombre;
        private System.Windows.Forms.RadioButton rbMujer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkAnios;
        private System.Windows.Forms.GroupBox gbEdad;
        private System.Windows.Forms.TextBox txtAnios;
        private System.Windows.Forms.CheckBox chkMesesDias;
        private System.Windows.Forms.ComboBox cmbMeses;
        private System.Windows.Forms.ComboBox cmbDias;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNombreBusqueda;
    }
}