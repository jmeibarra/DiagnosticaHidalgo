
namespace DiagnosticaHidalgo.Forms
{
    partial class frmVenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVenta));
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaquina = new System.Windows.Forms.TextBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.txtClaveUs = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gbDatosPrincipales = new System.Windows.Forms.GroupBox();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsNotasVenta = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCotizaciones = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCorteDeTurno = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSaldoPorCliente = new System.Windows.Forms.ToolStripMenuItem();
            this.tsImprimirLaboratorios = new System.Windows.Forms.ToolStripMenuItem();
            this.msItemAgregarUs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCorregirFolios = new System.Windows.Forms.ToolStripMenuItem();
            this.msItemSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCitas = new System.Windows.Forms.Button();
            this.chkCotizacion = new System.Windows.Forms.CheckBox();
            this.txtDoctor = new System.Windows.Forms.TextBox();
            this.btnAgregarDoctor = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAgregarCliente = new System.Windows.Forms.Button();
            this.txtFolio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gvLaboratorios = new System.Windows.Forms.DataGridView();
            this.gbLaboratorio = new System.Windows.Forms.GroupBox();
            this.chkDescripcion = new System.Windows.Forms.CheckBox();
            this.chkPUnitario = new System.Windows.Forms.CheckBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.txtPUnitario = new System.Windows.Forms.TextBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkDescuento = new System.Windows.Forms.CheckBox();
            this.rtbObservaciones = new System.Windows.Forms.RichTextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnPagar = new System.Windows.Forms.Button();
            this.gbTotales = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtTotalConDescuento = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.gbDatosPrincipales.SuspendLayout();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLaboratorios)).BeginInit();
            this.gbLaboratorio.SuspendLayout();
            this.gbTotales.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(181, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Maquina:";
            // 
            // txtMaquina
            // 
            this.txtMaquina.Enabled = false;
            this.txtMaquina.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaquina.Location = new System.Drawing.Point(269, 20);
            this.txtMaquina.Name = "txtMaquina";
            this.txtMaquina.Size = new System.Drawing.Size(40, 29);
            this.txtMaquina.TabIndex = 1;
            this.txtMaquina.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCliente
            // 
            this.txtCliente.Enabled = false;
            this.txtCliente.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCliente.Location = new System.Drawing.Point(85, 66);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(322, 29);
            this.txtCliente.TabIndex = 1;
            // 
            // txtClaveUs
            // 
            this.txtClaveUs.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaveUs.Location = new System.Drawing.Point(462, 20);
            this.txtClaveUs.MaxLength = 10;
            this.txtClaveUs.Name = "txtClaveUs";
            this.txtClaveUs.Size = new System.Drawing.Size(100, 29);
            this.txtClaveUs.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(339, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Clave usuario:";
            // 
            // gbDatosPrincipales
            // 
            this.gbDatosPrincipales.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gbDatosPrincipales.ContextMenuStrip = this.menu;
            this.gbDatosPrincipales.Controls.Add(this.btnCitas);
            this.gbDatosPrincipales.Controls.Add(this.chkCotizacion);
            this.gbDatosPrincipales.Controls.Add(this.txtDoctor);
            this.gbDatosPrincipales.Controls.Add(this.btnAgregarDoctor);
            this.gbDatosPrincipales.Controls.Add(this.label12);
            this.gbDatosPrincipales.Controls.Add(this.label4);
            this.gbDatosPrincipales.Controls.Add(this.btnAgregarCliente);
            this.gbDatosPrincipales.Controls.Add(this.txtFolio);
            this.gbDatosPrincipales.Controls.Add(this.txtMaquina);
            this.gbDatosPrincipales.Controls.Add(this.txtClaveUs);
            this.gbDatosPrincipales.Controls.Add(this.label1);
            this.gbDatosPrincipales.Controls.Add(this.txtCliente);
            this.gbDatosPrincipales.Controls.Add(this.label3);
            this.gbDatosPrincipales.Controls.Add(this.label2);
            this.gbDatosPrincipales.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDatosPrincipales.Location = new System.Drawing.Point(0, 0);
            this.gbDatosPrincipales.Name = "gbDatosPrincipales";
            this.gbDatosPrincipales.Size = new System.Drawing.Size(938, 115);
            this.gbDatosPrincipales.TabIndex = 0;
            this.gbDatosPrincipales.TabStop = false;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNotasVenta,
            this.tsCotizaciones,
            this.tsCorteDeTurno,
            this.tsSaldoPorCliente,
            this.tsImprimirLaboratorios,
            this.msItemAgregarUs,
            this.tsCorregirFolios,
            this.msItemSesion});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(254, 202);
            this.menu.Text = "Administrador";
            // 
            // tsNotasVenta
            // 
            this.tsNotasVenta.Name = "tsNotasVenta";
            this.tsNotasVenta.Size = new System.Drawing.Size(253, 22);
            this.tsNotasVenta.Text = "Notas de venta";
            this.tsNotasVenta.Click += new System.EventHandler(this.tsNotasVenta_Click);
            // 
            // tsCotizaciones
            // 
            this.tsCotizaciones.Name = "tsCotizaciones";
            this.tsCotizaciones.Size = new System.Drawing.Size(253, 22);
            this.tsCotizaciones.Text = "Cotizaciones";
            this.tsCotizaciones.Click += new System.EventHandler(this.tsCotizaciones_Click);
            // 
            // tsCorteDeTurno
            // 
            this.tsCorteDeTurno.Name = "tsCorteDeTurno";
            this.tsCorteDeTurno.Size = new System.Drawing.Size(253, 22);
            this.tsCorteDeTurno.Text = "Corte de turno";
            this.tsCorteDeTurno.Click += new System.EventHandler(this.tsCorteDeTurno_Click);
            // 
            // tsSaldoPorCliente
            // 
            this.tsSaldoPorCliente.Name = "tsSaldoPorCliente";
            this.tsSaldoPorCliente.Size = new System.Drawing.Size(253, 22);
            this.tsSaldoPorCliente.Text = "Saldo por cliente";
            this.tsSaldoPorCliente.Click += new System.EventHandler(this.tsSaldoPorCliente_Click);
            // 
            // tsImprimirLaboratorios
            // 
            this.tsImprimirLaboratorios.Name = "tsImprimirLaboratorios";
            this.tsImprimirLaboratorios.Size = new System.Drawing.Size(253, 22);
            this.tsImprimirLaboratorios.Text = "Relación de Laboratorios";
            // 
            // msItemAgregarUs
            // 
            this.msItemAgregarUs.Name = "msItemAgregarUs";
            this.msItemAgregarUs.Size = new System.Drawing.Size(253, 22);
            this.msItemAgregarUs.Text = "Agregar usuario";
            this.msItemAgregarUs.Click += new System.EventHandler(this.msItemAgregarUs_Click);
            // 
            // tsCorregirFolios
            // 
            this.tsCorregirFolios.Name = "tsCorregirFolios";
            this.tsCorregirFolios.Size = new System.Drawing.Size(253, 22);
            this.tsCorregirFolios.Text = "Corregir folios";
            this.tsCorregirFolios.Click += new System.EventHandler(this.tsCorregirFolios_Click);
            // 
            // msItemSesion
            // 
            this.msItemSesion.Name = "msItemSesion";
            this.msItemSesion.Size = new System.Drawing.Size(253, 22);
            this.msItemSesion.Text = "Iniciar sesión como administrador";
            this.msItemSesion.Click += new System.EventHandler(this.msItemSesion_Click);
            // 
            // btnCitas
            // 
            this.btnCitas.Location = new System.Drawing.Point(850, 15);
            this.btnCitas.Name = "btnCitas";
            this.btnCitas.Size = new System.Drawing.Size(76, 37);
            this.btnCitas.TabIndex = 10;
            this.btnCitas.Text = "CITAS";
            this.btnCitas.UseVisualStyleBackColor = true;
            this.btnCitas.Visible = false;
            this.btnCitas.Click += new System.EventHandler(this.btnCitas_Click);
            // 
            // chkCotizacion
            // 
            this.chkCotizacion.AutoSize = true;
            this.chkCotizacion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCotizacion.Location = new System.Drawing.Point(612, 22);
            this.chkCotizacion.Name = "chkCotizacion";
            this.chkCotizacion.Size = new System.Drawing.Size(128, 25);
            this.chkCotizacion.TabIndex = 9;
            this.chkCotizacion.Text = "Es cotización";
            this.chkCotizacion.UseVisualStyleBackColor = true;
            this.chkCotizacion.CheckedChanged += new System.EventHandler(this.chkCotizacion_CheckedChanged);
            // 
            // txtDoctor
            // 
            this.txtDoctor.Enabled = false;
            this.txtDoctor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDoctor.Location = new System.Drawing.Point(542, 66);
            this.txtDoctor.Name = "txtDoctor";
            this.txtDoctor.Size = new System.Drawing.Size(322, 29);
            this.txtDoctor.TabIndex = 8;
            // 
            // btnAgregarDoctor
            // 
            this.btnAgregarDoctor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarDoctor.Location = new System.Drawing.Point(863, 66);
            this.btnAgregarDoctor.Name = "btnAgregarDoctor";
            this.btnAgregarDoctor.Size = new System.Drawing.Size(26, 29);
            this.btnAgregarDoctor.TabIndex = 2;
            this.btnAgregarDoctor.Text = "+";
            this.btnAgregarDoctor.UseVisualStyleBackColor = true;
            this.btnAgregarDoctor.Click += new System.EventHandler(this.btnAgregarDoctor_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(27, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 21);
            this.label12.TabIndex = 4;
            this.label12.Text = "Folio:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(476, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "Doctor:";
            // 
            // btnAgregarCliente
            // 
            this.btnAgregarCliente.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarCliente.Location = new System.Drawing.Point(405, 66);
            this.btnAgregarCliente.Name = "btnAgregarCliente";
            this.btnAgregarCliente.Size = new System.Drawing.Size(26, 29);
            this.btnAgregarCliente.TabIndex = 1;
            this.btnAgregarCliente.Text = "+";
            this.btnAgregarCliente.UseVisualStyleBackColor = true;
            this.btnAgregarCliente.Click += new System.EventHandler(this.btnAgregarCliente_Click);
            // 
            // txtFolio
            // 
            this.txtFolio.Enabled = false;
            this.txtFolio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolio.Location = new System.Drawing.Point(85, 20);
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtFolio.Size = new System.Drawing.Size(60, 29);
            this.txtFolio.TabIndex = 0;
            this.txtFolio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Cliente:";
            // 
            // gvLaboratorios
            // 
            this.gvLaboratorios.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gvLaboratorios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvLaboratorios.Dock = System.Windows.Forms.DockStyle.Top;
            this.gvLaboratorios.Location = new System.Drawing.Point(0, 115);
            this.gvLaboratorios.MultiSelect = false;
            this.gvLaboratorios.Name = "gvLaboratorios";
            this.gvLaboratorios.ReadOnly = true;
            this.gvLaboratorios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gvLaboratorios.Size = new System.Drawing.Size(938, 295);
            this.gvLaboratorios.TabIndex = 4;
            this.gvLaboratorios.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvLaboratorios_CellContentClick);
            // 
            // gbLaboratorio
            // 
            this.gbLaboratorio.ContextMenuStrip = this.menu;
            this.gbLaboratorio.Controls.Add(this.chkDescripcion);
            this.gbLaboratorio.Controls.Add(this.chkPUnitario);
            this.gbLaboratorio.Controls.Add(this.btnAgregar);
            this.gbLaboratorio.Controls.Add(this.txtDescripcion);
            this.gbLaboratorio.Controls.Add(this.txtImporte);
            this.gbLaboratorio.Controls.Add(this.txtPUnitario);
            this.gbLaboratorio.Controls.Add(this.txtCantidad);
            this.gbLaboratorio.Controls.Add(this.btnBuscar);
            this.gbLaboratorio.Controls.Add(this.txtClave);
            this.gbLaboratorio.Controls.Add(this.label8);
            this.gbLaboratorio.Controls.Add(this.label6);
            this.gbLaboratorio.Controls.Add(this.label5);
            this.gbLaboratorio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLaboratorio.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbLaboratorio.Location = new System.Drawing.Point(0, 410);
            this.gbLaboratorio.Name = "gbLaboratorio";
            this.gbLaboratorio.Size = new System.Drawing.Size(938, 314);
            this.gbLaboratorio.TabIndex = 1;
            this.gbLaboratorio.TabStop = false;
            this.gbLaboratorio.Text = "Agregar Laboratorio";
            // 
            // chkDescripcion
            // 
            this.chkDescripcion.AutoSize = true;
            this.chkDescripcion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDescripcion.Location = new System.Drawing.Point(15, 78);
            this.chkDescripcion.Name = "chkDescripcion";
            this.chkDescripcion.Size = new System.Drawing.Size(123, 25);
            this.chkDescripcion.TabIndex = 5;
            this.chkDescripcion.Text = "Descripción:";
            this.chkDescripcion.UseVisualStyleBackColor = true;
            this.chkDescripcion.CheckedChanged += new System.EventHandler(this.chkDescripcion_CheckedChanged);
            // 
            // chkPUnitario
            // 
            this.chkPUnitario.AutoSize = true;
            this.chkPUnitario.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPUnitario.Location = new System.Drawing.Point(473, 37);
            this.chkPUnitario.Name = "chkPUnitario";
            this.chkPUnitario.Size = new System.Drawing.Size(146, 25);
            this.chkPUnitario.TabIndex = 3;
            this.chkPUnitario.Text = "Precio unitario:";
            this.chkPUnitario.UseVisualStyleBackColor = true;
            this.chkPUnitario.CheckedChanged += new System.EventHandler(this.chkPUnitario_CheckedChanged);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Enabled = false;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(801, 78);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(125, 37);
            this.btnAgregar.TabIndex = 7;
            this.btnAgregar.Text = "AGREGAR";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Enabled = false;
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(144, 76);
            this.txtDescripcion.MaxLength = 100;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(291, 29);
            this.txtDescripcion.TabIndex = 6;
            // 
            // txtImporte
            // 
            this.txtImporte.Enabled = false;
            this.txtImporte.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImporte.Location = new System.Drawing.Point(826, 36);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(100, 29);
            this.txtImporte.TabIndex = 4;
            // 
            // txtPUnitario
            // 
            this.txtPUnitario.Enabled = false;
            this.txtPUnitario.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPUnitario.Location = new System.Drawing.Point(625, 36);
            this.txtPUnitario.Name = "txtPUnitario";
            this.txtPUnitario.Size = new System.Drawing.Size(100, 29);
            this.txtPUnitario.TabIndex = 4;
            this.txtPUnitario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPUnitario_KeyPress);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Enabled = false;
            this.txtCantidad.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidad.Location = new System.Drawing.Point(403, 35);
            this.txtCantidad.MaxLength = 4;
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCantidad.Size = new System.Drawing.Size(36, 29);
            this.txtCantidad.TabIndex = 2;
            this.txtCantidad.Text = "1";
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(197, 34);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(96, 29);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtClave
            // 
            this.txtClave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClave.Location = new System.Drawing.Point(68, 35);
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(123, 29);
            this.txtClave.TabIndex = 0;
            this.txtClave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtClave_KeyDown);
            this.txtClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClave_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(752, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 21);
            this.label8.TabIndex = 0;
            this.label8.Text = "Importe:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(322, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "Cantidad:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "Clave:";
            // 
            // chkDescuento
            // 
            this.chkDescuento.AutoSize = true;
            this.chkDescuento.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDescuento.Location = new System.Drawing.Point(809, 67);
            this.chkDescuento.Name = "chkDescuento";
            this.chkDescuento.Size = new System.Drawing.Size(96, 25);
            this.chkDescuento.TabIndex = 10;
            this.chkDescuento.Text = "Habilitar";
            this.chkDescuento.UseVisualStyleBackColor = true;
            this.chkDescuento.CheckedChanged += new System.EventHandler(this.chkDescuento_CheckedChanged);
            // 
            // rtbObservaciones
            // 
            this.rtbObservaciones.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbObservaciones.Location = new System.Drawing.Point(131, 28);
            this.rtbObservaciones.MaxLength = 300;
            this.rtbObservaciones.Name = "rtbObservaciones";
            this.rtbObservaciones.Size = new System.Drawing.Size(308, 109);
            this.rtbObservaciones.TabIndex = 0;
            this.rtbObservaciones.Text = "";
            // 
            // txtTotal
            // 
            this.txtTotal.Enabled = false;
            this.txtTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(703, 28);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(100, 29);
            this.txtTotal.TabIndex = 8;
            // 
            // txtDescuento
            // 
            this.txtDescuento.Enabled = false;
            this.txtDescuento.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescuento.Location = new System.Drawing.Point(703, 65);
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Size = new System.Drawing.Size(100, 29);
            this.txtDescuento.TabIndex = 8;
            this.txtDescuento.TextChanged += new System.EventHandler(this.txtDescuento_TextChanged);
            this.txtDescuento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescuento_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(628, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 21);
            this.label11.TabIndex = 0;
            this.label11.Text = "Total   $";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(585, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 21);
            this.label10.TabIndex = 0;
            this.label10.Text = "Descuento   $";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(5, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 21);
            this.label9.TabIndex = 0;
            this.label9.Text = "Observaciones:";
            // 
            // btnPagar
            // 
            this.btnPagar.Location = new System.Drawing.Point(801, 148);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(125, 37);
            this.btnPagar.TabIndex = 2;
            this.btnPagar.Text = "PAGAR";
            this.btnPagar.UseVisualStyleBackColor = true;
            this.btnPagar.Click += new System.EventHandler(this.btnPagar_Click);
            // 
            // gbTotales
            // 
            this.gbTotales.ContextMenuStrip = this.menu;
            this.gbTotales.Controls.Add(this.btnCancelar);
            this.gbTotales.Controls.Add(this.chkDescuento);
            this.gbTotales.Controls.Add(this.btnPagar);
            this.gbTotales.Controls.Add(this.rtbObservaciones);
            this.gbTotales.Controls.Add(this.label9);
            this.gbTotales.Controls.Add(this.label10);
            this.gbTotales.Controls.Add(this.txtTotalConDescuento);
            this.gbTotales.Controls.Add(this.label13);
            this.gbTotales.Controls.Add(this.txtTotal);
            this.gbTotales.Controls.Add(this.label11);
            this.gbTotales.Controls.Add(this.txtDescuento);
            this.gbTotales.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbTotales.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTotales.Location = new System.Drawing.Point(0, 534);
            this.gbTotales.Name = "gbTotales";
            this.gbTotales.Size = new System.Drawing.Size(938, 190);
            this.gbTotales.TabIndex = 2;
            this.gbTotales.TabStop = false;
            this.gbTotales.Text = "Datos generales de la Venta";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(670, 148);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(125, 37);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtTotalConDescuento
            // 
            this.txtTotalConDescuento.Enabled = false;
            this.txtTotalConDescuento.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalConDescuento.Location = new System.Drawing.Point(703, 108);
            this.txtTotalConDescuento.Name = "txtTotalConDescuento";
            this.txtTotalConDescuento.Size = new System.Drawing.Size(100, 29);
            this.txtTotalConDescuento.TabIndex = 8;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(447, 111);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(250, 21);
            this.label13.TabIndex = 0;
            this.label13.Text = "Total aplicando el descuento   $";
            // 
            // frmVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(938, 724);
            this.ContextMenuStrip = this.menu;
            this.Controls.Add(this.gbTotales);
            this.Controls.Add(this.gbLaboratorio);
            this.Controls.Add(this.gvLaboratorios);
            this.Controls.Add(this.gbDatosPrincipales);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmVenta";
            this.Text = "Nota de Venta v06.07.2024";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmVenta_FormClosed);
            this.Load += new System.EventHandler(this.frmVenta_Load);
            this.gbDatosPrincipales.ResumeLayout(false);
            this.gbDatosPrincipales.PerformLayout();
            this.menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvLaboratorios)).EndInit();
            this.gbLaboratorio.ResumeLayout(false);
            this.gbLaboratorio.PerformLayout();
            this.gbTotales.ResumeLayout(false);
            this.gbTotales.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaquina;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.TextBox txtClaveUs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbDatosPrincipales;
        private System.Windows.Forms.Button btnAgregarCliente;
        private System.Windows.Forms.DataGridView gvLaboratorios;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbLaboratorio;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPUnitario;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.TextBox txtDescuento;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox rtbObservaciones;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkDescuento;
        private System.Windows.Forms.Button btnPagar;
        private System.Windows.Forms.GroupBox gbTotales;
        private System.Windows.Forms.Button btnAgregarDoctor;
        private System.Windows.Forms.TextBox txtDoctor;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem msItemSesion;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtFolio;
        private System.Windows.Forms.TextBox txtTotalConDescuento;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ToolStripMenuItem msItemAgregarUs;
        private System.Windows.Forms.CheckBox chkPUnitario;
        private System.Windows.Forms.CheckBox chkDescripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem tsCorteDeTurno;
        private System.Windows.Forms.ToolStripMenuItem tsSaldoPorCliente;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ToolStripMenuItem tsNotasVenta;
        private System.Windows.Forms.ToolStripMenuItem tsImprimirLaboratorios;
        private System.Windows.Forms.ToolStripMenuItem tsCotizaciones;
        private System.Windows.Forms.CheckBox chkCotizacion;
        private System.Windows.Forms.Button btnCitas;
        private System.Windows.Forms.ToolStripMenuItem tsCorregirFolios;
    }
}