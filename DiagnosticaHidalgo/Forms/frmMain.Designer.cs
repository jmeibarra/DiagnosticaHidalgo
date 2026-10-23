
namespace DiagnosticaHidalgo
{
    partial class frmMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tsConfiguraciones = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.consultarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.tsClientes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDoctores = new System.Windows.Forms.ToolStripMenuItem();
            this.laboratoriosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsNuevoLaboratorio = new System.Windows.Forms.ToolStripMenuItem();
            this.consultarLabToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCortesDeTurno = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCorteTurnoTodos = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCorteRurnoPorDia = new System.Windows.Forms.ToolStripMenuItem();
            this.tsReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsNotaVenta = new System.Windows.Forms.ToolStripMenuItem();
            this.notasPagadasConTarjetaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.devoluciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsOperaciones = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPago = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDevolucion = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlConfiguraciones = new System.Windows.Forms.Panel();
            this.chkPrimerFolio = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancelarConfig = new System.Windows.Forms.Button();
            this.btnGuardarConf = new System.Windows.Forms.Button();
            this.cmbDigitosFolio = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmpresa = new System.Windows.Forms.TextBox();
            this.txtMaquina = new System.Windows.Forms.TextBox();
            this.txtPrimerFolio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlRegistros = new System.Windows.Forms.Panel();
            this.gbFiltros = new System.Windows.Forms.GroupBox();
            this.pnlFechas = new System.Windows.Forms.Panel();
            this.btnBuscarCortes = new System.Windows.Forms.Button();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.lblDesde = new System.Windows.Forms.Label();
            this.dtpFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.pnlCheck = new System.Windows.Forms.Panel();
            this.txtTercero = new System.Windows.Forms.TextBox();
            this.txtPrimero = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.chkTercero = new System.Windows.Forms.CheckBox();
            this.chkSegundo = new System.Windows.Forms.CheckBox();
            this.chkPrimero = new System.Windows.Forms.CheckBox();
            this.txtSegundo = new System.Windows.Forms.TextBox();
            this.gvRegistros = new System.Windows.Forms.DataGridView();
            this.menuPrincipal.SuspendLayout();
            this.pnlConfiguraciones.SuspendLayout();
            this.pnlRegistros.SuspendLayout();
            this.gbFiltros.SuspendLayout();
            this.pnlFechas.SuspendLayout();
            this.pnlCheck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvRegistros)).BeginInit();
            this.SuspendLayout();
            // 
            // tsConfiguraciones
            // 
            this.tsConfiguraciones.Name = "tsConfiguraciones";
            this.tsConfiguraciones.Size = new System.Drawing.Size(135, 25);
            this.tsConfiguraciones.Text = "Configuraciones";
            this.tsConfiguraciones.Click += new System.EventHandler(this.tsConfiguraciones_Click);
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.consultarToolStripMenuItem1,
            this.tsReportes,
            this.tsNotaVenta,
            this.tsConfiguraciones,
            this.tsCerrarSesion,
            this.tsOperaciones});
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuPrincipal.Size = new System.Drawing.Size(558, 31);
            this.menuPrincipal.TabIndex = 1;
            this.menuPrincipal.Text = "msGeneral";
            // 
            // consultarToolStripMenuItem1
            // 
            this.consultarToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsUsuarios,
            this.tsClientes,
            this.tsDoctores,
            this.laboratoriosToolStripMenuItem,
            this.tsCortesDeTurno});
            this.consultarToolStripMenuItem1.Name = "consultarToolStripMenuItem1";
            this.consultarToolStripMenuItem1.Size = new System.Drawing.Size(89, 25);
            this.consultarToolStripMenuItem1.Text = "Consultar";
            // 
            // tsUsuarios
            // 
            this.tsUsuarios.Name = "tsUsuarios";
            this.tsUsuarios.Size = new System.Drawing.Size(188, 26);
            this.tsUsuarios.Text = "Usuarios";
            this.tsUsuarios.Click += new System.EventHandler(this.tsUsuarios_Click);
            // 
            // tsClientes
            // 
            this.tsClientes.Name = "tsClientes";
            this.tsClientes.Size = new System.Drawing.Size(188, 26);
            this.tsClientes.Text = "Clientes";
            this.tsClientes.Click += new System.EventHandler(this.tsClientes_Click);
            // 
            // tsDoctores
            // 
            this.tsDoctores.Name = "tsDoctores";
            this.tsDoctores.Size = new System.Drawing.Size(188, 26);
            this.tsDoctores.Text = "Doctores";
            this.tsDoctores.Click += new System.EventHandler(this.tsDoctores_Click);
            // 
            // laboratoriosToolStripMenuItem
            // 
            this.laboratoriosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNuevoLaboratorio,
            this.consultarLabToolStripMenuItem2});
            this.laboratoriosToolStripMenuItem.Name = "laboratoriosToolStripMenuItem";
            this.laboratoriosToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.laboratoriosToolStripMenuItem.Text = "Laboratorios";
            // 
            // tsNuevoLaboratorio
            // 
            this.tsNuevoLaboratorio.Name = "tsNuevoLaboratorio";
            this.tsNuevoLaboratorio.Size = new System.Drawing.Size(180, 26);
            this.tsNuevoLaboratorio.Text = "Agregar";
            this.tsNuevoLaboratorio.Click += new System.EventHandler(this.tsNuevoLaboratorio_Click);
            // 
            // consultarLabToolStripMenuItem2
            // 
            this.consultarLabToolStripMenuItem2.Name = "consultarLabToolStripMenuItem2";
            this.consultarLabToolStripMenuItem2.Size = new System.Drawing.Size(180, 26);
            this.consultarLabToolStripMenuItem2.Text = "Consultar";
            this.consultarLabToolStripMenuItem2.Click += new System.EventHandler(this.tsConsultarLab_Click);
            // 
            // tsCortesDeTurno
            // 
            this.tsCortesDeTurno.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsCorteTurnoTodos,
            this.tsCorteRurnoPorDia});
            this.tsCortesDeTurno.Name = "tsCortesDeTurno";
            this.tsCortesDeTurno.Size = new System.Drawing.Size(188, 26);
            this.tsCortesDeTurno.Text = "Cortes de turno";
            // 
            // tsCorteTurnoTodos
            // 
            this.tsCorteTurnoTodos.Name = "tsCorteTurnoTodos";
            this.tsCorteTurnoTodos.Size = new System.Drawing.Size(200, 26);
            this.tsCorteTurnoTodos.Text = "Consultar todos";
            this.tsCorteTurnoTodos.Click += new System.EventHandler(this.tsCorteTurnoTodos_Click);
            // 
            // tsCorteRurnoPorDia
            // 
            this.tsCorteRurnoPorDia.Name = "tsCorteRurnoPorDia";
            this.tsCorteRurnoPorDia.Size = new System.Drawing.Size(200, 26);
            this.tsCorteRurnoPorDia.Text = "Consultar por día";
            this.tsCorteRurnoPorDia.Click += new System.EventHandler(this.tsCorteRurnoPorDia_Click);
            // 
            // tsReportes
            // 
            this.tsReportes.Name = "tsReportes";
            this.tsReportes.Size = new System.Drawing.Size(84, 25);
            this.tsReportes.Text = "Reportes";
            this.tsReportes.Click += new System.EventHandler(this.tsReportes_Click);
            // 
            // tsNotaVenta
            // 
            this.tsNotaVenta.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notasPagadasConTarjetaToolStripMenuItem,
            this.devoluciónToolStripMenuItem});
            this.tsNotaVenta.Name = "tsNotaVenta";
            this.tsNotaVenta.Size = new System.Drawing.Size(100, 25);
            this.tsNotaVenta.Text = "Nota Venta";
            // 
            // notasPagadasConTarjetaToolStripMenuItem
            // 
            this.notasPagadasConTarjetaToolStripMenuItem.Name = "notasPagadasConTarjetaToolStripMenuItem";
            this.notasPagadasConTarjetaToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
            this.notasPagadasConTarjetaToolStripMenuItem.Text = "Notas pagadas con tarjeta";
            this.notasPagadasConTarjetaToolStripMenuItem.Click += new System.EventHandler(this.notasPagadasConTarjetaToolStripMenuItem_Click);
            // 
            // devoluciónToolStripMenuItem
            // 
            this.devoluciónToolStripMenuItem.Name = "devoluciónToolStripMenuItem";
            this.devoluciónToolStripMenuItem.Size = new System.Drawing.Size(260, 26);
            this.devoluciónToolStripMenuItem.Text = "Devolución";
            this.devoluciónToolStripMenuItem.Click += new System.EventHandler(this.devoluciónToolStripMenuItem_Click);
            // 
            // tsCerrarSesion
            // 
            this.tsCerrarSesion.Name = "tsCerrarSesion";
            this.tsCerrarSesion.Size = new System.Drawing.Size(114, 25);
            this.tsCerrarSesion.Text = "Cerrar sesión";
            this.tsCerrarSesion.Click += new System.EventHandler(this.tsCerrarSesion_Click);
            // 
            // tsOperaciones
            // 
            this.tsOperaciones.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsPago,
            this.tsDevolucion});
            this.tsOperaciones.Name = "tsOperaciones";
            this.tsOperaciones.Size = new System.Drawing.Size(148, 25);
            this.tsOperaciones.Text = "Otras operaciones";
            this.tsOperaciones.Visible = false;
            // 
            // tsPago
            // 
            this.tsPago.Name = "tsPago";
            this.tsPago.Size = new System.Drawing.Size(115, 26);
            this.tsPago.Text = "Pago";
            this.tsPago.Click += new System.EventHandler(this.tsAgregarPago_Click);
            // 
            // tsDevolucion
            // 
            this.tsDevolucion.Name = "tsDevolucion";
            this.tsDevolucion.Size = new System.Drawing.Size(115, 26);
            // 
            // pnlConfiguraciones
            // 
            this.pnlConfiguraciones.Controls.Add(this.chkPrimerFolio);
            this.pnlConfiguraciones.Controls.Add(this.label5);
            this.pnlConfiguraciones.Controls.Add(this.label4);
            this.pnlConfiguraciones.Controls.Add(this.btnCancelarConfig);
            this.pnlConfiguraciones.Controls.Add(this.btnGuardarConf);
            this.pnlConfiguraciones.Controls.Add(this.cmbDigitosFolio);
            this.pnlConfiguraciones.Controls.Add(this.label3);
            this.pnlConfiguraciones.Controls.Add(this.txtEmpresa);
            this.pnlConfiguraciones.Controls.Add(this.txtMaquina);
            this.pnlConfiguraciones.Controls.Add(this.txtPrimerFolio);
            this.pnlConfiguraciones.Controls.Add(this.label1);
            this.pnlConfiguraciones.Location = new System.Drawing.Point(0, 62);
            this.pnlConfiguraciones.Name = "pnlConfiguraciones";
            this.pnlConfiguraciones.Size = new System.Drawing.Size(557, 358);
            this.pnlConfiguraciones.TabIndex = 2;
            this.pnlConfiguraciones.Visible = false;
            // 
            // chkPrimerFolio
            // 
            this.chkPrimerFolio.AutoSize = true;
            this.chkPrimerFolio.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrimerFolio.Location = new System.Drawing.Point(134, 92);
            this.chkPrimerFolio.Name = "chkPrimerFolio";
            this.chkPrimerFolio.Size = new System.Drawing.Size(119, 25);
            this.chkPrimerFolio.TabIndex = 5;
            this.chkPrimerFolio.Text = "Primer folio:";
            this.chkPrimerFolio.UseVisualStyleBackColor = true;
            this.chkPrimerFolio.CheckedChanged += new System.EventHandler(this.chkPrimerFolio_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(177, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 21);
            this.label5.TabIndex = 4;
            this.label5.Text = "Empresa:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(180, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "Máquina:";
            // 
            // btnCancelarConfig
            // 
            this.btnCancelarConfig.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarConfig.Location = new System.Drawing.Point(254, 243);
            this.btnCancelarConfig.Name = "btnCancelarConfig";
            this.btnCancelarConfig.Size = new System.Drawing.Size(125, 37);
            this.btnCancelarConfig.TabIndex = 3;
            this.btnCancelarConfig.Text = "CANCELAR";
            this.btnCancelarConfig.UseVisualStyleBackColor = true;
            // 
            // btnGuardarConf
            // 
            this.btnGuardarConf.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarConf.Location = new System.Drawing.Point(385, 243);
            this.btnGuardarConf.Name = "btnGuardarConf";
            this.btnGuardarConf.Size = new System.Drawing.Size(125, 37);
            this.btnGuardarConf.TabIndex = 3;
            this.btnGuardarConf.Text = "GUARDAR";
            this.btnGuardarConf.UseVisualStyleBackColor = true;
            this.btnGuardarConf.Click += new System.EventHandler(this.btnGuardarConf_Click);
            // 
            // cmbDigitosFolio
            // 
            this.cmbDigitosFolio.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDigitosFolio.FormattingEnabled = true;
            this.cmbDigitosFolio.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cmbDigitosFolio.Location = new System.Drawing.Point(268, 40);
            this.cmbDigitosFolio.Name = "cmbDigitosFolio";
            this.cmbDigitosFolio.Size = new System.Drawing.Size(44, 29);
            this.cmbDigitosFolio.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(331, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ej: 5 - 00012";
            // 
            // txtEmpresa
            // 
            this.txtEmpresa.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpresa.Location = new System.Drawing.Point(268, 187);
            this.txtEmpresa.MaxLength = 20;
            this.txtEmpresa.Name = "txtEmpresa";
            this.txtEmpresa.Size = new System.Drawing.Size(160, 29);
            this.txtEmpresa.TabIndex = 1;
            // 
            // txtMaquina
            // 
            this.txtMaquina.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaquina.Location = new System.Drawing.Point(268, 140);
            this.txtMaquina.MaxLength = 20;
            this.txtMaquina.Name = "txtMaquina";
            this.txtMaquina.Size = new System.Drawing.Size(160, 29);
            this.txtMaquina.TabIndex = 1;
            this.txtMaquina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrimerFolio_KeyPress);
            // 
            // txtPrimerFolio
            // 
            this.txtPrimerFolio.Enabled = false;
            this.txtPrimerFolio.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrimerFolio.Location = new System.Drawing.Point(268, 90);
            this.txtPrimerFolio.Name = "txtPrimerFolio";
            this.txtPrimerFolio.Size = new System.Drawing.Size(82, 29);
            this.txtPrimerFolio.TabIndex = 1;
            this.txtPrimerFolio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrimerFolio_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Número de digitos en el folio:";
            // 
            // pnlRegistros
            // 
            this.pnlRegistros.BackColor = System.Drawing.Color.White;
            this.pnlRegistros.Controls.Add(this.gbFiltros);
            this.pnlRegistros.Controls.Add(this.gvRegistros);
            this.pnlRegistros.Location = new System.Drawing.Point(589, 87);
            this.pnlRegistros.Name = "pnlRegistros";
            this.pnlRegistros.Size = new System.Drawing.Size(970, 352);
            this.pnlRegistros.TabIndex = 5;
            this.pnlRegistros.Visible = false;
            // 
            // gbFiltros
            // 
            this.gbFiltros.Controls.Add(this.pnlFechas);
            this.gbFiltros.Controls.Add(this.pnlCheck);
            this.gbFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFiltros.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFiltros.Location = new System.Drawing.Point(0, 0);
            this.gbFiltros.Name = "gbFiltros";
            this.gbFiltros.Size = new System.Drawing.Size(970, 118);
            this.gbFiltros.TabIndex = 1;
            this.gbFiltros.TabStop = false;
            this.gbFiltros.Text = "Filtros";
            // 
            // pnlFechas
            // 
            this.pnlFechas.Controls.Add(this.btnBuscarCortes);
            this.pnlFechas.Controls.Add(this.lblHasta);
            this.pnlFechas.Controls.Add(this.dtpFechaDesde);
            this.pnlFechas.Controls.Add(this.lblDesde);
            this.pnlFechas.Controls.Add(this.dtpFechaHasta);
            this.pnlFechas.Location = new System.Drawing.Point(23, 24);
            this.pnlFechas.Name = "pnlFechas";
            this.pnlFechas.Size = new System.Drawing.Size(563, 81);
            this.pnlFechas.TabIndex = 2;
            // 
            // btnBuscarCortes
            // 
            this.btnBuscarCortes.Location = new System.Drawing.Point(423, 25);
            this.btnBuscarCortes.Name = "btnBuscarCortes";
            this.btnBuscarCortes.Size = new System.Drawing.Size(125, 37);
            this.btnBuscarCortes.TabIndex = 14;
            this.btnBuscarCortes.Text = "BUSCAR";
            this.btnBuscarCortes.UseVisualStyleBackColor = true;
            this.btnBuscarCortes.Click += new System.EventHandler(this.btnBuscarCortes_Click);
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHasta.Location = new System.Drawing.Point(20, 54);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(57, 21);
            this.lblHasta.TabIndex = 13;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpFechaDesde
            // 
            this.dtpFechaDesde.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaDesde.Location = new System.Drawing.Point(83, 10);
            this.dtpFechaDesde.Name = "dtpFechaDesde";
            this.dtpFechaDesde.Size = new System.Drawing.Size(306, 29);
            this.dtpFechaDesde.TabIndex = 11;
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesde.Location = new System.Drawing.Point(16, 16);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(61, 21);
            this.lblDesde.TabIndex = 12;
            this.lblDesde.Text = "Desde:";
            // 
            // dtpFechaHasta
            // 
            this.dtpFechaHasta.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaHasta.Location = new System.Drawing.Point(83, 48);
            this.dtpFechaHasta.Name = "dtpFechaHasta";
            this.dtpFechaHasta.Size = new System.Drawing.Size(306, 29);
            this.dtpFechaHasta.TabIndex = 11;
            // 
            // pnlCheck
            // 
            this.pnlCheck.Controls.Add(this.txtTercero);
            this.pnlCheck.Controls.Add(this.txtPrimero);
            this.pnlCheck.Controls.Add(this.btnBuscar);
            this.pnlCheck.Controls.Add(this.chkTercero);
            this.pnlCheck.Controls.Add(this.chkSegundo);
            this.pnlCheck.Controls.Add(this.chkPrimero);
            this.pnlCheck.Controls.Add(this.txtSegundo);
            this.pnlCheck.Location = new System.Drawing.Point(23, 24);
            this.pnlCheck.Name = "pnlCheck";
            this.pnlCheck.Size = new System.Drawing.Size(926, 84);
            this.pnlCheck.TabIndex = 3;
            // 
            // txtTercero
            // 
            this.txtTercero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTercero.Enabled = false;
            this.txtTercero.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTercero.Location = new System.Drawing.Point(522, 41);
            this.txtTercero.Name = "txtTercero";
            this.txtTercero.Size = new System.Drawing.Size(119, 29);
            this.txtTercero.TabIndex = 10;
            this.txtTercero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTercero_KeyPress);
            // 
            // txtPrimero
            // 
            this.txtPrimero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPrimero.Enabled = false;
            this.txtPrimero.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrimero.Location = new System.Drawing.Point(32, 42);
            this.txtPrimero.Name = "txtPrimero";
            this.txtPrimero.Size = new System.Drawing.Size(224, 29);
            this.txtPrimero.TabIndex = 7;
            this.txtPrimero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrimero_KeyPress);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(431, 40);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(85, 30);
            this.btnBuscar.TabIndex = 8;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // chkTercero
            // 
            this.chkTercero.AutoSize = true;
            this.chkTercero.Location = new System.Drawing.Point(498, 9);
            this.chkTercero.Name = "chkTercero";
            this.chkTercero.Size = new System.Drawing.Size(77, 25);
            this.chkTercero.TabIndex = 9;
            this.chkTercero.Text = "Banco";
            this.chkTercero.UseVisualStyleBackColor = true;
            this.chkTercero.CheckedChanged += new System.EventHandler(this.chkTercero_CheckedChanged);
            // 
            // chkSegundo
            // 
            this.chkSegundo.AutoSize = true;
            this.chkSegundo.Location = new System.Drawing.Point(287, 11);
            this.chkSegundo.Name = "chkSegundo";
            this.chkSegundo.Size = new System.Drawing.Size(71, 25);
            this.chkSegundo.TabIndex = 4;
            this.chkSegundo.Text = "Clave";
            this.chkSegundo.UseVisualStyleBackColor = true;
            this.chkSegundo.CheckedChanged += new System.EventHandler(this.chkSegundo_CheckedChanged);
            // 
            // chkPrimero
            // 
            this.chkPrimero.AutoSize = true;
            this.chkPrimero.Location = new System.Drawing.Point(14, 11);
            this.chkPrimero.Name = "chkPrimero";
            this.chkPrimero.Size = new System.Drawing.Size(93, 25);
            this.chkPrimero.TabIndex = 3;
            this.chkPrimero.Text = "Nombre";
            this.chkPrimero.UseVisualStyleBackColor = true;
            this.chkPrimero.CheckedChanged += new System.EventHandler(this.chkPrimero_CheckedChanged);
            // 
            // txtSegundo
            // 
            this.txtSegundo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSegundo.Enabled = false;
            this.txtSegundo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSegundo.Location = new System.Drawing.Point(306, 42);
            this.txtSegundo.Name = "txtSegundo";
            this.txtSegundo.Size = new System.Drawing.Size(148, 29);
            this.txtSegundo.TabIndex = 6;
            this.txtSegundo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSegundo_KeyPress);
            // 
            // gvRegistros
            // 
            this.gvRegistros.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gvRegistros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvRegistros.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gvRegistros.GridColor = System.Drawing.SystemColors.Window;
            this.gvRegistros.Location = new System.Drawing.Point(0, 114);
            this.gvRegistros.MultiSelect = false;
            this.gvRegistros.Name = "gvRegistros";
            this.gvRegistros.ReadOnly = true;
            this.gvRegistros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvRegistros.Size = new System.Drawing.Size(970, 238);
            this.gvRegistros.TabIndex = 0;
            this.gvRegistros.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvRegistros_CellContentClick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(558, 430);
            this.Controls.Add(this.pnlRegistros);
            this.Controls.Add(this.pnlConfiguraciones);
            this.Controls.Add(this.menuPrincipal);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DIAGNOTICA HIDALGO";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.pnlConfiguraciones.ResumeLayout(false);
            this.pnlConfiguraciones.PerformLayout();
            this.pnlRegistros.ResumeLayout(false);
            this.gbFiltros.ResumeLayout(false);
            this.pnlFechas.ResumeLayout(false);
            this.pnlFechas.PerformLayout();
            this.pnlCheck.ResumeLayout(false);
            this.pnlCheck.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvRegistros)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem tsConfiguraciones;
        private System.Windows.Forms.MenuStrip menuPrincipal;
        private System.Windows.Forms.Panel pnlConfiguraciones;
        private System.Windows.Forms.ComboBox cmbDigitosFolio;
        private System.Windows.Forms.TextBox txtPrimerFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancelarConfig;
        private System.Windows.Forms.Button btnGuardarConf;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMaquina;
        private System.Windows.Forms.Panel pnlRegistros;
        private System.Windows.Forms.GroupBox gbFiltros;
        private System.Windows.Forms.TextBox txtSegundo;
        private System.Windows.Forms.TextBox txtPrimero;
        private System.Windows.Forms.CheckBox chkPrimero;
        private System.Windows.Forms.CheckBox chkSegundo;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtTercero;
        private System.Windows.Forms.CheckBox chkTercero;
        private System.Windows.Forms.DateTimePicker dtpFechaHasta;
        private System.Windows.Forms.DateTimePicker dtpFechaDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.Panel pnlFechas;
        private System.Windows.Forms.Panel pnlCheck;
        private System.Windows.Forms.ToolStripMenuItem consultarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsUsuarios;
        private System.Windows.Forms.ToolStripMenuItem tsClientes;
        private System.Windows.Forms.ToolStripMenuItem tsDoctores;
        private System.Windows.Forms.ToolStripMenuItem laboratoriosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsCortesDeTurno;
        private System.Windows.Forms.ToolStripMenuItem tsOperaciones;
        private System.Windows.Forms.Button btnBuscarCortes;
        private System.Windows.Forms.ToolStripMenuItem tsCerrarSesion;
        private System.Windows.Forms.ToolStripMenuItem tsPago;
        private System.Windows.Forms.ToolStripMenuItem tsDevolucion;
        private System.Windows.Forms.ToolStripMenuItem tsReportes;
        private System.Windows.Forms.DataGridView gvRegistros;
        private System.Windows.Forms.ToolStripMenuItem tsNotaVenta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmpresa;
        private System.Windows.Forms.CheckBox chkPrimerFolio;
        private System.Windows.Forms.ToolStripMenuItem tsNuevoLaboratorio;
        private System.Windows.Forms.ToolStripMenuItem consultarLabToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem notasPagadasConTarjetaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem devoluciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsCorteTurnoTodos;
        private System.Windows.Forms.ToolStripMenuItem tsCorteRurnoPorDia;
    }
}

