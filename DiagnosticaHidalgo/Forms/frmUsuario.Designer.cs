
namespace DiagnosticaHidalgo.Forms
{
    partial class frmUsuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUsuario));
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtCelular = new System.Windows.Forms.TextBox();
            this.gbSesion = new System.Windows.Forms.GroupBox();
            this.chkMostrarCont = new System.Windows.Forms.CheckBox();
            this.chkCambiarCont = new System.Windows.Forms.CheckBox();
            this.txtRepPass = new System.Windows.Forms.TextBox();
            this.lblRepCont = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtClaveAdm = new System.Windows.Forms.TextBox();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblIndicaciones1 = new System.Windows.Forms.Label();
            this.lblIndicaciones2 = new System.Windows.Forms.Label();
            this.gbSesion.SuspendLayout();
            this.gbDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre completo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "Correo electrónico:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(26, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "Teléfono celular:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(242, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "Clave:";
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(170, 32);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(400, 29);
            this.txtNombre.TabIndex = 0;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(170, 82);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(400, 29);
            this.txtEmail.TabIndex = 1;
            // 
            // txtCelular
            // 
            this.txtCelular.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCelular.Location = new System.Drawing.Point(170, 132);
            this.txtCelular.MaxLength = 10;
            this.txtCelular.Name = "txtCelular";
            this.txtCelular.Size = new System.Drawing.Size(108, 29);
            this.txtCelular.TabIndex = 2;
            this.txtCelular.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCelular.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCelular_KeyPress);
            // 
            // gbSesion
            // 
            this.gbSesion.Controls.Add(this.chkMostrarCont);
            this.gbSesion.Controls.Add(this.chkCambiarCont);
            this.gbSesion.Controls.Add(this.txtRepPass);
            this.gbSesion.Controls.Add(this.lblRepCont);
            this.gbSesion.Controls.Add(this.txtPassword);
            this.gbSesion.Controls.Add(this.txtClaveAdm);
            this.gbSesion.Controls.Add(this.label6);
            this.gbSesion.Location = new System.Drawing.Point(12, 343);
            this.gbSesion.Name = "gbSesion";
            this.gbSesion.Size = new System.Drawing.Size(593, 226);
            this.gbSesion.TabIndex = 1;
            this.gbSesion.TabStop = false;
            this.gbSesion.Text = "Datos de inicio de sesión";
            this.gbSesion.Visible = false;
            // 
            // chkMostrarCont
            // 
            this.chkMostrarCont.AutoSize = true;
            this.chkMostrarCont.Enabled = false;
            this.chkMostrarCont.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMostrarCont.Location = new System.Drawing.Point(443, 106);
            this.chkMostrarCont.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkMostrarCont.Name = "chkMostrarCont";
            this.chkMostrarCont.Size = new System.Drawing.Size(144, 21);
            this.chkMostrarCont.TabIndex = 8;
            this.chkMostrarCont.Text = "Mostrar Contraseña";
            this.chkMostrarCont.UseVisualStyleBackColor = true;
            this.chkMostrarCont.CheckedChanged += new System.EventHandler(this.chkMostrarCont_CheckedChanged);
            // 
            // chkCambiarCont
            // 
            this.chkCambiarCont.AutoSize = true;
            this.chkCambiarCont.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCambiarCont.Location = new System.Drawing.Point(113, 103);
            this.chkCambiarCont.Name = "chkCambiarCont";
            this.chkCambiarCont.Size = new System.Drawing.Size(185, 25);
            this.chkCambiarCont.TabIndex = 7;
            this.chkCambiarCont.Text = "Cambiar contraseña:";
            this.chkCambiarCont.UseVisualStyleBackColor = true;
            this.chkCambiarCont.CheckedChanged += new System.EventHandler(this.chkCambiarCont_CheckedChanged);
            // 
            // txtRepPass
            // 
            this.txtRepPass.Enabled = false;
            this.txtRepPass.Location = new System.Drawing.Point(304, 151);
            this.txtRepPass.Name = "txtRepPass";
            this.txtRepPass.Size = new System.Drawing.Size(131, 29);
            this.txtRepPass.TabIndex = 6;
            this.txtRepPass.UseSystemPasswordChar = true;
            // 
            // lblRepCont
            // 
            this.lblRepCont.AutoSize = true;
            this.lblRepCont.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRepCont.Location = new System.Drawing.Point(119, 154);
            this.lblRepCont.Name = "lblRepCont";
            this.lblRepCont.Size = new System.Drawing.Size(179, 21);
            this.lblRepCont.TabIndex = 5;
            this.lblRepCont.Text = "Confirmar contraseña:";
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(304, 101);
            this.txtPassword.MaxLength = 15;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(131, 29);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtClaveAdm
            // 
            this.txtClaveAdm.Location = new System.Drawing.Point(304, 40);
            this.txtClaveAdm.MaxLength = 3;
            this.txtClaveAdm.Name = "txtClaveAdm";
            this.txtClaveAdm.Size = new System.Drawing.Size(50, 29);
            this.txtClaveAdm.TabIndex = 0;
            this.txtClaveAdm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtClaveAdm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCelular_KeyPress);
            // 
            // gbDatos
            // 
            this.gbDatos.Controls.Add(this.txtClave);
            this.gbDatos.Controls.Add(this.label2);
            this.gbDatos.Controls.Add(this.label8);
            this.gbDatos.Controls.Add(this.label1);
            this.gbDatos.Controls.Add(this.txtCelular);
            this.gbDatos.Controls.Add(this.txtEmail);
            this.gbDatos.Controls.Add(this.label4);
            this.gbDatos.Controls.Add(this.label5);
            this.gbDatos.Controls.Add(this.txtNombre);
            this.gbDatos.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDatos.Location = new System.Drawing.Point(12, 58);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(593, 219);
            this.gbDatos.TabIndex = 0;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos Generales";
            // 
            // txtClave
            // 
            this.txtClave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClave.Location = new System.Drawing.Point(170, 182);
            this.txtClave.MaxLength = 3;
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(50, 29);
            this.txtClave.TabIndex = 3;
            this.txtClave.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCelular_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(108, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "Clave:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(303, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 17);
            this.label8.TabIndex = 4;
            this.label8.Text = "10 Dígitos";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(472, 294);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(135, 37);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "REGISTRARME";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(318, 294);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(135, 37);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblIndicaciones1
            // 
            this.lblIndicaciones1.AutoSize = true;
            this.lblIndicaciones1.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblIndicaciones1.Location = new System.Drawing.Point(6, 9);
            this.lblIndicaciones1.Name = "lblIndicaciones1";
            this.lblIndicaciones1.Size = new System.Drawing.Size(497, 21);
            this.lblIndicaciones1.TabIndex = 4;
            this.lblIndicaciones1.Text = "Cada campo en letra negrita es requerido para su registro, verifique su";
            // 
            // lblIndicaciones2
            // 
            this.lblIndicaciones2.AutoSize = true;
            this.lblIndicaciones2.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblIndicaciones2.Location = new System.Drawing.Point(6, 30);
            this.lblIndicaciones2.Name = "lblIndicaciones2";
            this.lblIndicaciones2.Size = new System.Drawing.Size(97, 21);
            this.lblIndicaciones2.TabIndex = 5;
            this.lblIndicaciones2.Text = "información.";
            // 
            // frmUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(621, 346);
            this.Controls.Add(this.lblIndicaciones2);
            this.Controls.Add(this.lblIndicaciones1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.gbDatos);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.gbSesion);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmUsuario";
            this.Text = "Usuario";
            this.gbSesion.ResumeLayout(false);
            this.gbSesion.PerformLayout();
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtCelular;
        private System.Windows.Forms.GroupBox gbSesion;
        private System.Windows.Forms.TextBox txtClaveAdm;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblIndicaciones1;
        private System.Windows.Forms.Label lblIndicaciones2;
        private System.Windows.Forms.TextBox txtRepPass;
        private System.Windows.Forms.Label lblRepCont;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkCambiarCont;
        private System.Windows.Forms.CheckBox chkMostrarCont;
    }
}