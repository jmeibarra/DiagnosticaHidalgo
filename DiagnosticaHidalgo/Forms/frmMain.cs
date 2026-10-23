using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Clases.Modelos;
using DiagnosticaHidalgo.Negocio;
using DiagnosticaHidalgo.Properties;
using DiagnosticaHidalgo.Forms;

namespace DiagnosticaHidalgo
{
    public partial class frmMain : Form
    {
        #region Variables
        public enum TipoPanel
        {
            CorteTurnoTodos,
            CorteTurnoPordia,
            Usuarios,
            Clientes,
            Configuracion,
            Laboratorios,
            Doctores,
            Ninguno
        }
        private Usuario oUsuario = new Usuario();
        public Configuraciones configuraciones = new Configuraciones();
        public TipoPanel tipoPanel;

        public static string C_CLASE = "frmMain";
        #endregion

        #region Eventos
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                /*Cambiar colores, estilos*/
                gbFiltros.ForeColor = C.COLOR_SISTEMA;
                foreach (Control ctr in gbFiltros.Controls)
                    ctr.ForeColor = C.COLOR_SISTEMA;

                pnlCheck.ForeColor = C.COLOR_SISTEMA;
                foreach (Control ctr in pnlCheck.Controls)
                    ctr.ForeColor = C.COLOR_SISTEMA;

                pnlConfiguraciones.ForeColor = C.COLOR_SISTEMA;
                foreach (Control ctr in pnlConfiguraciones.Controls)
                    ctr.ForeColor = C.COLOR_SISTEMA;

                pnlFechas.ForeColor = C.COLOR_SISTEMA;
                foreach (Control ctr in pnlFechas.Controls)
                    ctr.ForeColor = C.COLOR_SISTEMA;

                btnBuscar.BackColor = C.COLOR_SISTEMA;
                btnBuscar.ForeColor = C.COLOR_SISTEMA_CLARO;
                btnBuscarCortes.BackColor = C.COLOR_SISTEMA;
                btnBuscarCortes.ForeColor = C.COLOR_SISTEMA_CLARO;
                btnGuardarConf.BackColor = C.COLOR_SISTEMA;
                btnGuardarConf.ForeColor = C.COLOR_SISTEMA_CLARO;
                btnCancelarConfig.BackColor = C.COLOR_SISTEMA;
                btnCancelarConfig.ForeColor = C.COLOR_SISTEMA_CLARO;

                gvRegistros.ColumnHeadersDefaultCellStyle.ForeColor = C.COLOR_SISTEMA;
                gvRegistros.ForeColor = C.COLOR_SISTEMA;
                gvRegistros.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);

                menuPrincipal.Enabled = false;
                this.tipoPanel = TipoPanel.Ninguno;

                //Eliminar archivos temporales
                string[] txtList = Directory.GetFiles(C.PathTEMP, "*.*");
                foreach (string f in txtList)
                {
                    File.Delete(f);
                }

                this.LeerConfiguracionesDeArchivo();

                //Verificar si existe un archivo de instalación en descargas
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje(ex.Message);
                Utilerias.RegistrarLogError(ex, C_CLASE, "frmMain_Load", "");
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            this.MostrarFrmVenta();
        }

        private void tsUsuarios_Click(object sender, EventArgs e)
        {
            //Obtener los usuarios para mostrarlos en el GridView
            UsuarioNeg usuarioNeg = new UsuarioNeg(oUsuario.Cadenaconn);
            gvRegistros.DataSource = null;
            gvRegistros.DataSource = usuarioNeg.ObtenerTodos();
            tipoPanel = TipoPanel.Usuarios;
            Actualizar_gvRegistros();

            CambiarPanel();
        }
        #endregion

        #region Funciones Auxiliares


        /// <summary>
        /// Método para mostrar el formulario de captura de venta.
        /// </summary>
        public void MostrarFrmVenta()
        {
            try
            {
                bool bIniciarSesion = false;
                using (frmVenta f = new frmVenta(InstanciarUsSinSesion(),this.configuraciones))
                {
                    this.Visible = false;
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        this.Visible = true;
                        bIniciarSesion = f.bIniciarSesion;
                        if (!bIniciarSesion)
                            this.Close();
                    }
                }

                if (bIniciarSesion)
                    IniciarSesion();
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje(ex.Message);
                Utilerias.RegistrarLogError(ex, C_CLASE, "MostrarFrmVenta", "");
            }
        }
        #endregion

        #region Configuraciones
        /// <summary>
        /// Ver configuraciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsConfiguraciones_Click(object sender, EventArgs e)
        {
            tipoPanel = TipoPanel.Configuracion;
            CambiarPanel();
            this.CargarConfiguraciones();
        }
        /// <summary>
        /// Guarda los cambios hechos en configuraciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardarConf_Click(object sender, EventArgs e)
        {
            try
            {
                string msjError = ValidarCamposConfig();
                if (string.IsNullOrEmpty(msjError))
                {
                    Configuraciones nuevaConfig = new Configuraciones
                    {
                        DigitosFolio = Convert.ToInt32(cmbDigitosFolio.Text),
                        PrimerFolio = Convert.ToInt32(txtPrimerFolio.Text),
                        Maquina = txtMaquina.Text.Trim(),
                        Empresa = txtEmpresa.Text.Trim()
                    };
                    configuraciones = this.GuardarConfiguraciones(nuevaConfig);
                    Utilerias.MostrarMensaje("¡Las configuraciones han sido guardadas con éxito!");
                }
                else { Utilerias.MostrarMensaje(msjError); }
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje(ex.Message);
                Utilerias.RegistrarLogError(ex, C_CLASE, "btnGuardarConf_Click", oUsuario.Nombre);
            }
            CargarConfiguraciones();
        }

        /// <summary>
        /// Evento al presionar una tecla en el control "txtPrimerFolio".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPrimerFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
               (e.KeyChar != '.'))
                e.Handled = true;
        }

        public string ValidarCamposConfig()
        {
            if (string.IsNullOrEmpty(cmbDigitosFolio.Text.Trim()))
                return "Número de digitos en el folio es requerido.";
            else if (string.IsNullOrEmpty(txtPrimerFolio.Text.Trim()))
                return "Primer folio es requerido.";
            else if (string.IsNullOrEmpty(txtMaquina.Text.Trim()))
                return "Máquina es requerido.";
            else if (!Regex.IsMatch(txtMaquina.Text.Trim(), "^[^|]{1,20}$"))
                return "Máquina inválido.";
            else if (string.IsNullOrEmpty(txtEmpresa.Text.Trim()))
                return "Empresa es requerido.";
            else if (!Regex.IsMatch(txtEmpresa.Text.Trim(), "^[^|]{1,20}$"))
                return "Empresa inválido.";

            //Validar si el número de folio no esta repetido
            if (chkPrimerFolio.Checked)
            {
                VentaNeg ventaNeg = new VentaNeg(oUsuario.Cadenaconn);
                int folio = ventaNeg.ObtenerUltimoFolio() + 1;
                if (folio > Convert.ToInt32(txtPrimerFolio.Text.Trim()))
                    return "Primer folio inválido (existe una nota de venta con un folio mayor).";
            }

            return "";
        }

        /// <summary>
        /// Moistrar en el panel los datos del objeto Configuraciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CargarConfiguraciones()
        {
            cmbDigitosFolio.Text = configuraciones.DigitosFolio.ToString();
            txtPrimerFolio.Enabled = chkPrimerFolio.Checked = false;
            txtPrimerFolio.Text = configuraciones.PrimerFolio.ToString();
            txtMaquina.Text = configuraciones.Maquina;
            txtEmpresa.Text = configuraciones.Empresa;
        }

        /// <summary>
        /// Guardar las configuraciones en un archivo.
        /// </summary>
        public Configuraciones GuardarConfiguraciones(Configuraciones config)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuraciones));
            using (var writer = XmlWriter.Create(C.PathConfigFile, new XmlWriterSettings { Indent = true }))
            {
                xmlSerializer.Serialize(writer, config);
            }

            return config;
        }

        /// <summary>
        /// Leer las configuraciones del sistema desde un archivo.
        /// </summary>
        public void LeerConfiguracionesDeArchivo()
        {
            var xmlSerializerR = new XmlSerializer(typeof(Configuraciones));

            using (var reader = XmlReader.Create(C.PathConfigFile))
            {
                this.configuraciones = xmlSerializerR.Deserialize(reader) as Configuraciones;

            }
        }
        #endregion

        private void tsClientes_Click(object sender, EventArgs e)
        {
            //Obtener los usuarios para mostrarlos en el GridView
            ClienteNeg clienteNeg = new ClienteNeg(oUsuario.Cadenaconn);
            gvRegistros.DataSource = null;
            gvRegistros.DataSource = clienteNeg.ObtenerPorFiltroNombre("");
            Actualizar_gvRegistros();

            tipoPanel = TipoPanel.Clientes;
            CambiarPanel();
        }

        /// <summary>
        /// Evento que se dispará al dar clic sobre el control chkPrimero,
        /// es el primer filtro de búsqueda para los registros.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPrimero_CheckedChanged(object sender, EventArgs e)
        {
            txtPrimero.Enabled = chkPrimero.Checked;
            txtPrimero.Text = "";
        }

        /// <summary>
        /// Evento que se dispará al dar clic sobre el control chkSegundo,
        /// es el segundo filtro de búsqueda para los registros.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSegundo_CheckedChanged(object sender, EventArgs e)
        {
            txtSegundo.Enabled = chkSegundo.Checked;
            txtSegundo.Text = "";
        }

        /// <summary>
        /// Evento que se dispará al dar clic sobre el control chkTercero,
        /// es el tercer filtro de búsqueda para los registos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTercero_CheckedChanged(object sender, EventArgs e)
        {
            txtTercero.Enabled = chkTercero.Checked;
            txtTercero.Text = "";
        }

        /// <summary>
        /// Función para actualizar DataGridView que contiene los registros de consulta.
        /// Puede ser Usuarios, Clientes, Laboratorios...
        /// </summary>
        public void Actualizar_gvRegistros()
        {
            if (gvRegistros.Columns.Count > 0)
            {
                try
                {
                    gvRegistros.Columns["FechaCreacion"].HeaderText = "Fecha de creación";
                    gvRegistros.Columns["FechaAct"].HeaderText = "Fecha de edición";

                    //Eliminar las columnas de editar y eliminar
                    if (gvRegistros.Columns[0].Name != "Id")
                    {
                        gvRegistros.Columns.RemoveAt(0);
                        if (gvRegistros.Columns[0].Name == C.COLUMNA_ELIMINAR) gvRegistros.Columns.RemoveAt(0);
                        if (gvRegistros.Columns[0].Name == C.COLUMNA_HUELLA) gvRegistros.Columns.RemoveAt(0);
                    }

                    //Ocultar la columna que contiene el identificador de cada registro
                    gvRegistros.Columns["Id"].Visible = false;
                    gvRegistros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                    //Agregar botones de acciones (Edición|Eliminar|Ver notas)
                    if (tipoPanel == TipoPanel.CorteTurnoTodos || tipoPanel == TipoPanel.CorteTurnoPordia)
                    {
                        DataGridViewButtonColumn btnPrimer = new DataGridViewButtonColumn();
                        btnPrimer.Text = C.COLUMNA_VERNOTAS;
                        btnPrimer.Name = C.COLUMNA_VERNOTAS;
                        btnPrimer.UseColumnTextForButtonValue = true;
                        gvRegistros.Columns.Add(btnPrimer);

                        gvRegistros.Columns["TotalEfectivo"].DefaultCellStyle.Format = "C2";
                        gvRegistros.Columns["TotalEfectivo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                        gvRegistros.Columns["TotalCuentas"].DefaultCellStyle.Format = "C2";
                        gvRegistros.Columns["TotalCuentas"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                        gvRegistros.Columns["Total"].DefaultCellStyle.Format = "C2";
                        gvRegistros.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                    }
                    else
                    {
                        DataGridViewImageColumn btnEd = new DataGridViewImageColumn();
                        btnEd.MinimumWidth = 25;
                        btnEd.Image = Resources.edicion;
                        btnEd.Name = C.COLUMNA_MODIFICAR;
                        btnEd.ToolTipText = "Editar registro";
                        btnEd.HeaderText = "";
                        btnEd.ImageLayout = DataGridViewImageCellLayout.Stretch;
                        btnEd.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        gvRegistros.Columns.Add(btnEd);

                        DataGridViewImageColumn btnE = new DataGridViewImageColumn();
                        btnE.Width = 100;
                        btnE.MinimumWidth = 25;
                        btnE.Image = Resources.eliminar25;
                        btnE.Name = C.COLUMNA_ELIMINAR;
                        btnE.ToolTipText = "Eliminar registro";
                        btnE.HeaderText = "";
                        btnE.ImageLayout = DataGridViewImageCellLayout.Stretch;
                        btnE.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        gvRegistros.Columns.Add(btnE);

                        if (tipoPanel == TipoPanel.Usuarios)
                        {
                            DataGridViewImageColumn btnH = new DataGridViewImageColumn();
                            btnH.Width = 100;
                            btnH.MinimumWidth = 25;
                            btnH.Image = Resources.cargarNota;
                            btnH.Name = C.COLUMNA_HUELLA;
                            //btnH.ToolTipText = "Eliminar registro";
                            btnH.HeaderText = "Huella";
                            btnH.ImageLayout = DataGridViewImageCellLayout.Stretch;
                            btnH.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            gvRegistros.Columns.Add(btnH);
                        }
                    }

                    foreach (DataGridViewColumn item in gvRegistros.Columns) 
                        item.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                catch (Exception ex)
                {
                    Utilerias.MostrarMensaje("No se pudieron cargar los registros.");
                    Utilerias.RegistrarLogError(ex, C_CLASE, "Actualizar_gvRegistros", oUsuario.Nombre);
                }
            }
        }

        /// <summary>
        /// Evento al dar clic en algúna celda del gridView de consultas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvRegistros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                switch (this.tipoPanel)
                {
                    case TipoPanel.Usuarios:
                        EliminarEditarUsuario(e.ColumnIndex, e.RowIndex);
                        break;
                    case TipoPanel.Clientes:
                        EliminarEditarCliente(e.ColumnIndex, e.RowIndex);
                        break;
                    case TipoPanel.Doctores:
                        EliminarEditarDoctor(e.ColumnIndex, e.RowIndex);
                        break;
                    case TipoPanel.CorteTurnoPordia:
                        VerNotas(e.ColumnIndex, e.RowIndex);
                        break;
                    case TipoPanel.CorteTurnoTodos:
                        VerNotas(e.ColumnIndex, e.RowIndex);
                        break;
                }
            }
        }
        public void CambiarPanel()
        {
            this.pnlCheck.Visible = true;
            this.gbFiltros.Visible = true;
            this.pnlConfiguraciones.Visible = false;
            this.pnlRegistros.Visible = true;
            this.pnlRegistros.Dock = DockStyle.Fill;
            this.chkTercero.Checked = this.chkSegundo.Checked = this.chkPrimero.Checked = false;
            this.txtPrimero.Text = txtSegundo.Text = "";
            chkTercero.Visible = txtTercero.Visible = false;
            this.pnlFechas.Visible = false;
            this.gbFiltros.Height = 118;
            this.Size = new Size(900, 500);

            switch (this.tipoPanel)
            {
                case TipoPanel.Configuracion:
                    this.pnlRegistros.Visible = false;
                    this.pnlConfiguraciones.Visible = true;
                    this.pnlConfiguraciones.Dock = DockStyle.Fill;
                    this.Size = new Size(550, 375);
                    break;
                case TipoPanel.Usuarios:
                    chkPrimero.Text = "Nombre";
                    chkSegundo.Text = "Clave";
                    this.chkSegundo.Visible = this.txtSegundo.Visible = true;
                    this.txtSegundo.Size = new Size(50, 29);
                    this.btnBuscar.Location = new Point(400, 43);
                    break;
                case TipoPanel.Clientes:
                    chkPrimero.Text = "Nombre";
                    this.chkSegundo.Visible = this.txtSegundo.Visible = false;
                    this.btnBuscar.Location = new Point(325, 43);
                    break;
                case TipoPanel.Doctores:
                    chkPrimero.Text = "Nombre";
                    chkSegundo.Text = "Especialidad";
                    this.chkSegundo.Visible = this.txtSegundo.Visible = true;
                    this.txtSegundo.Size = new Size(200, 29);
                    this.btnBuscar.Location = new Point(527, 43);
                    break;
                case TipoPanel.CorteTurnoTodos:
                    pnlCheck.Visible = false;
                    pnlFechas.Visible = true;
                    pnlFechas.Location = new Point(10, 27);
                    this.Size = new Size(730, 500);
                    break;
                case TipoPanel.CorteTurnoPordia:
                    pnlCheck.Visible = false;
                    pnlFechas.Visible = true;
                    pnlFechas.Location = new Point(10, 27);
                    this.Size = new Size(730, 500);
                    break;
            }
        }

        /// <summary>
        /// Evento al presionar una tecla sobre el primer filtro de búsqueda.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPrimero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnBuscar_Click(btnBuscar, null);
            }
        }

        /// <summary>
        /// Evento al presionar una tecla sobre el segundo filtro de búsqueda.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSegundo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnBuscar_Click(btnBuscar, null);
            }
            else if (this.tipoPanel == TipoPanel.Usuarios)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Evento al presionar una tecla sobre el tercer filtro de búsqueda.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTercero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnBuscar_Click(btnBuscar, null);
            }
        }

        /// <summary>
        /// Evento clic del botón buscar por filtros seleccionados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            gvRegistros.DataSource = null;
            CorteTurnoNeg corteTurnoNeg = new CorteTurnoNeg(oUsuario.Cadenaconn);
            DateTime fechaInicio;
            DateTime fechaFin;
            switch (this.tipoPanel)
            {
                case TipoPanel.Usuarios:
                    UsuarioNeg usuarioNeg = new UsuarioNeg(oUsuario.Cadenaconn);
                    gvRegistros.DataSource = usuarioNeg.ObtenerPorFiltros(txtPrimero.Text.Trim(), txtSegundo.Text.Trim());
                    break;
                case TipoPanel.Clientes:
                    ClienteNeg clienteNeg = new ClienteNeg(oUsuario.Cadenaconn);
                    gvRegistros.DataSource = clienteNeg.ObtenerPorFiltroNombre(txtPrimero.Text.Trim());
                    break;
                case TipoPanel.Doctores:
                    DoctorNeg doctorNeg = new DoctorNeg(oUsuario.Cadenaconn);
                    gvRegistros.DataSource = doctorNeg.ObtenerPorFiltros(txtPrimero.Text.Trim(), txtSegundo.Text.Trim());
                    break;
                case TipoPanel.CorteTurnoTodos:
                    
                    fechaInicio = new DateTime(dtpFechaDesde.Value.Year, dtpFechaDesde.Value.Month, dtpFechaDesde.Value.Day, 0,0,0);
                    fechaFin = new DateTime(dtpFechaHasta.Value.Year, dtpFechaHasta.Value.Month, dtpFechaHasta.Value.Day,23,59,59);
                    gvRegistros.DataSource = corteTurnoNeg.ObtenerPorFiltros(fechaInicio, fechaFin);
                    break;
                case TipoPanel.CorteTurnoPordia:

                    fechaInicio = new DateTime(dtpFechaDesde.Value.Year, dtpFechaDesde.Value.Month, dtpFechaDesde.Value.Day, 0, 0, 0);
                    fechaFin = new DateTime(dtpFechaHasta.Value.Year, dtpFechaHasta.Value.Month, dtpFechaHasta.Value.Day, 23, 59, 59);
                    gvRegistros.DataSource = corteTurnoNeg.ObtenerPorFiltros(fechaInicio, fechaFin);
                    break;
            }
            Actualizar_gvRegistros();
        }
        #region Usuarios
        /// <summary>
        /// Función para editar o eliminar un usuario.
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="renglon"></param>
        public void EliminarEditarUsuario(int columna, int renglon)
        {
            try
            {
                int Id = 0;
                UsuarioNeg usuarioNeg = new UsuarioNeg(oUsuario.Cadenaconn);
                string msjError = "";
                string nombreColumna = gvRegistros.Columns[columna].Name;
                if (nombreColumna == C.COLUMNA_ELIMINAR) //Eliminar
                {
                    if (gvRegistros["Administrador", renglon].Value.ToString() == "Si") throw new Exception("No se puede eliminar el usuario administrador.");

                    string nombreUsuario = gvRegistros["Nombre", renglon].Value.ToString();
                    if (Utilerias.MostrarMensajeConfirmacion("Esta seguro de eliminar al usuario " + nombreUsuario))
                    {
                        Id = (int)gvRegistros["Id", renglon].Value;
                        //Un usuario no se puede eliminar si tiene notas hechas
                        if (usuarioNeg.Eliminar(Id, ref msjError) > 0)
                        {
                            Utilerias.MostrarMensaje("El usuario ha sido eliminado correctamente.");
                            gvRegistros.DataSource = null;
                            gvRegistros.DataSource = usuarioNeg.ObtenerTodos();
                            Actualizar_gvRegistros();
                        }

                        if (msjError.Contains(C.FK_VENTA_IDUSUARIO))
                            Utilerias.MostrarMensaje("El usuario no puede ser eliminado porque ha hecho notas de ventas");
                    }
                }
                else if (C.COLUMNA_MODIFICAR == nombreColumna) //Editar 
                {
                    Id = (int)gvRegistros["Id", renglon].Value;
                    using (frmUsuario f = new frmUsuario(oUsuario.Cadenaconn, false, Id))
                    {
                        this.Visible = false;
                        if (f.ShowDialog() == DialogResult.OK)
                        {                            
                            gvRegistros.DataSource = null;
                            gvRegistros.DataSource = usuarioNeg.ObtenerTodos();
                            Actualizar_gvRegistros();
                        }
                        this.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "EliminarEditarUsuario", oUsuario.Nombre);
                Utilerias.MostrarMensaje(ex.Message);
            }
        }
        #endregion
        #region Clientes
        /// <summary>
        /// Función para editar o eliminar un cliente.
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="renglon"></param>
        public void EliminarEditarCliente(int columna, int renglon)
        {
            try
            {
                int Id = 0;
                ClienteNeg clienteNeg = new ClienteNeg(oUsuario.Cadenaconn);
                string nombreColumna = gvRegistros.Columns[columna].Name;
                Id = (int)gvRegistros["Id", renglon].Value;

                if (nombreColumna == C.COLUMNA_ELIMINAR) //Eliminar
                {
                    string nombreC = gvRegistros["Nombre", renglon].Value.ToString();
                    if (Utilerias.MostrarMensajeConfirmacion("Esta seguro de eliminar al cliente " + nombreC))
                    {
                        //Un cliente no se puede eliminar si existe en alguna nota de venta.
                        clienteNeg.EliminarPorId(Id);
                        gvRegistros.DataSource = null;
                        gvRegistros.DataSource = clienteNeg.ObtenerPorFiltroNombre("");
                        Actualizar_gvRegistros();
                    }
                }
                else if (C.COLUMNA_MODIFICAR == nombreColumna) //Editar 
                {
                    using (frmCliente f = new frmCliente(oUsuario,Id))
                    {
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            gvRegistros.DataSource = null;
                            gvRegistros.DataSource = clienteNeg.ObtenerTodos();
                            Actualizar_gvRegistros();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(C.FK_VENTA_IDCLIENTE))
                    Utilerias.MostrarMensaje("El cliente no puede ser eliminado porque tiene notas de ventas");
                else Utilerias.MostrarMensaje(ex.Message);
                Utilerias.RegistrarLogError(ex, C_CLASE, "EliminarEditarCliente", oUsuario.Nombre);
            }
        }
        #endregion

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //tipoPanel = TipoPanel.CtaBancaria;
            CambiarPanel();
            btnBuscar_Click(btnBuscar, null);
        }

        #region CorteCuenta
        /// <summary>
        /// Función para ver las notas de venta del corte de turno.
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="renglon"></param>
        public void VerNotas(int columna, int renglon)
        {
            string nombreColumna = gvRegistros.Columns[columna].Name;

            if (nombreColumna == C.COLUMNA_VERNOTAS)
            {
                int Id = (int)gvRegistros["Id", renglon].Value;
                using (frmCorteTurno f = new frmCorteTurno(oUsuario.Cadenaconn,Id, true,false,false, this.configuraciones))
                {
                    f.ShowDialog();
                }
            }
        }
        #endregion
        #region Doctorres
        /// <summary>
        /// Clic en el menú Consultas -> Doctores.
        /// Consultar todos los doctores para editar o eliminar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsDoctores_Click(object sender, EventArgs e)
        {
            //Obtener los usuarios para mostrarlos en el GridView
            DoctorNeg doctorNeg = new DoctorNeg(oUsuario.Cadenaconn);
            gvRegistros.DataSource = null;
            gvRegistros.DataSource = doctorNeg.ObtenerPorFiltros("", "");
            tipoPanel = TipoPanel.Doctores;
            Actualizar_gvRegistros();
            CambiarPanel();
        }

        /// <summary>
        /// Función para editar o eliminar un doctor.
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="renglon"></param>
        public void EliminarEditarDoctor(int columna, int renglon)
        {
            int Id = 0;
            DoctorNeg doctorNeg = new DoctorNeg(oUsuario.Cadenaconn);
            string nombreColumna = gvRegistros.Columns[columna].Name;
            Id = (int)gvRegistros["Id", renglon].Value;
            try
            {
                if (nombreColumna == C.COLUMNA_ELIMINAR) //Eliminar
                {
                    string nombreD = gvRegistros["Nombre", renglon].Value.ToString();
                    if (Utilerias.MostrarMensajeConfirmacion("Esta seguro de eliminar al doctor " + nombreD))
                    {
                        //Un doctor no se puede eliminar si existe en alguna nota de venta.
                        doctorNeg.EliminarPorId(Id);
                        gvRegistros.DataSource = null;
                        gvRegistros.DataSource = doctorNeg.ObtenerPorFiltros("", "");
                        Actualizar_gvRegistros();
                    }
                }
                else if (C.COLUMNA_MODIFICAR == nombreColumna) //Editar 
                {
                    using (frmDoctor f = new frmDoctor(oUsuario,Id))
                    {
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            btnBuscar_Click(btnBuscar, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje(ex.Message);
                Utilerias.RegistrarLogError(ex, C_CLASE, "EliminarEditarDoctor", oUsuario.Nombre);
            }
        }
        #endregion
        #region Laboratorios
        /// <summary>
        /// Clic en el menú Consultas -> Laboratorios.
        /// Consultar todos los laboratorios para editar o eliminar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsConsultarLab_Click(object sender, EventArgs e)
        {
            using (frmConsultarLaboratorios frm = new frmConsultarLaboratorios(oUsuario.Cadenaconn,true, -1))
            {
                tipoPanel = TipoPanel.Ninguno;
                CambiarPanel();
                frm.ShowDialog();
            }
        }
        #endregion
        private void btnBuscarCortes_Click(object sender, EventArgs e)
        {
            btnBuscar_Click(btnBuscar, null);
            if (gvRegistros.RowCount == 0) Utilerias.MostrarMensaje("No existen cortes de turno en las fechas especificadas.");
        }               

        #region Sesión de Usuario Administrador
        /// <summary>
        /// Clic en el botón cerrar sesión.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsCerrarSesion_Click(object sender, EventArgs e)
        {
            this.CerrarSesion();
        }

        /// <summary>
        /// Método para cerrar sesión del usuario administrador.
        /// </summary>
        public void CerrarSesion()
        {            
            this.oUsuario = new Usuario();
            foreach (Control c in this.Controls)
            {
                if (c is Panel)
                    c.Visible = false;
            }
            this.MostrarFrmVenta();
        }

        /// <summary>
        /// Método para mostrar el formulario de inicio de sesión como adminisrtador.
        /// </summary>
        private void IniciarSesion()
        {
            using (frmLogin f = new frmLogin(false))
            {
                f.ShowDialog();
                oUsuario = f.oUsuario;
                if (oUsuario == null)
                {
                    this.MostrarFrmVenta();
                }
                else
                    menuPrincipal.Enabled = true;
            }
        }

        #endregion

        /// <summary>
        /// Opción para mostrar estadisticas de las ventas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsReportes_Click(object sender, EventArgs e)
        {
            using (frmReportes f = new frmReportes(oUsuario,this.configuraciones))
            {
                f.ShowDialog();

            }
        }

        private void chkPrimerFolio_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkPrimerFolio.Checked)
            {
                txtPrimerFolio.Text = this.configuraciones.PrimerFolio.ToString();
            }
                txtPrimerFolio.Enabled = chkPrimerFolio.Checked;
        }

        private void tsNuevoLaboratorio_Click(object sender, EventArgs e)
        {
            using (frmLaboratorio frm = new frmLaboratorio(null))
            {
                frm.ShowDialog();
            }
        }

        #region Codigo no usado
        /// <summary>
        /// Agregar un pago que podría liquidar mas de una nota pendiente.
        /// Puede ser un pago o una devolución.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsAgregarPago_Click(object sender, EventArgs e)
        {
            using (frmPagoCliente f = new frmPagoCliente(oUsuario))
            {
                f.ShowDialog();

            }
        }
        #endregion

        private void notasPagadasConTarjetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (fNotasTarjeta frm = new fNotasTarjeta(oUsuario,this.configuraciones))
            {
                frm.ShowDialog();
            }
        }

        private void devoluciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmDevolucion f = new frmDevolucion(oUsuario))
            {
                f.ShowDialog();

            }
        }

        /// <summary>
        /// Clic en la opción de consultar cortes de turno.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsCorteTurnoTodos_Click(object sender, EventArgs e)
        {
            tipoPanel = TipoPanel.CorteTurnoTodos;
            CambiarPanel();
            btnBuscar_Click(btnBuscar, null);
        }

        private void tsCorteRurnoPorDia_Click(object sender, EventArgs e)
        {
            tipoPanel = TipoPanel.CorteTurnoPordia;
            CambiarPanel();
            btnBuscar_Click(btnBuscar, null);
        }

        private Usuario InstanciarUsSinSesion() {
            Usuario usss = new Usuario() { Administrador=false, Cadenaconn=C.CONNECTION };
            return usss;
        }
    }
}
