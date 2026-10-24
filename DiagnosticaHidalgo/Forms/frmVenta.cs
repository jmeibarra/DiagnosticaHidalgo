using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Clases.Modelos;
using DiagnosticaHidalgo.Negocio;
using DiagnosticaHidalgo.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DiagnosticaHidalgo.Clases.Seguridad;

namespace DiagnosticaHidalgo.Forms
{
    public partial class frmVenta : Form
    {
        #region Variables
        private const string C_CLASE = "frmVenta";

        private Venta venta = new Venta();
        private VentaNeg ventaNeg;
        private LaboratorioNeg laboratorioNeg;
        private List<LabPorVenta> laboratorios = new List<LabPorVenta>();
        private List<LabResumido> lLabResumido = new List<LabResumido>();
        private Laboratorio laboratorio;
        private Horarios horariosocupados;
        private int IdCorteTurno = -1;

        public bool bIniciarSesion = false;
        public Configuraciones config;
        /// <summary>
        /// Usuario que esta manejando el sistema
        /// </summary>
        public Usuario us = null;
        #endregion
        public frmVenta(Usuario usuario, Configuraciones configuraciones)
        {
            InitializeComponent();
            this.us = usuario;
            this.config = configuraciones;
            laboratorioNeg = new LaboratorioNeg(Conexion.ConexionDB());

            /*Cambiar estilos*/
            foreach (Control gb in this.Controls)
            {
                if (gb is GroupBox)
                {
                    gb.ForeColor = C.COLOR_SISTEMA;
                    foreach (Control ctrl in gb.Controls)
                    {
                        if (ctrl is Button)
                        {
                            ctrl.BackColor = C.COLOR_SISTEMA;
                            ctrl.ForeColor = C.COLOR_SISTEMA_CLARO;
                        }
                        else ctrl.ForeColor = C.COLOR_SISTEMA;
                    }
                }
                else if (gb is DataGridView)
                {
                    DataGridView gv = (DataGridView)gb;
                    gv.ColumnHeadersDefaultCellStyle.ForeColor = C.COLOR_SISTEMA;
                    gv.ForeColor = C.COLOR_SISTEMA;
                    gv.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
            }
            /*Termina cambiar estilos*/
        }

        private void frmVenta_Load(object sender, EventArgs e)
        {
            try
            {
                //Verificar si ya hubo corte de turno
                if (this.IdCorteTurno == -1)
                {
                    CorteTurnoNeg corteTurnoNeg = new CorteTurnoNeg(Conexion.ConexionDB());
                    CorteTurno corteTurno = corteTurnoNeg.ObtenerSinFechaFin();
                    if (corteTurno != null)
                        this.IdCorteTurno = corteTurno.IdCorteTurno;
                }

                this.InicializarVenta();
            }
            catch (Exception ex) { Utilerias.RegistrarLogError(ex, C_CLASE, "frmVenta_Load", ""); }

        }

        #region Eventos

        #region Click
        /// <summary>
        /// Evento del botón "Agregar cliente". Al mostrar el formuario se puede agregar o seleccionar un cliente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmCliente fNuevo = new frmCliente(us, -1))
                {
                    fNuevo.ShowDialog();

                    if (!string.IsNullOrEmpty(fNuevo.nombre))
                    {
                        txtCliente.Text = fNuevo.nombre;
                        venta.IdCliente = fNuevo.IdCliente;
                    }
                }
            }
            catch (Exception ex)
            { Utilerias.RegistrarLogError(ex, C_CLASE, "btnAgregarCliente_Click", ""); }
        }
        /// <summary>
        /// Evento del botón "Agregar doctor". Al mostrar el formuario se puede agregar o seleccionar un doctor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btnAgregarDoctor_Click(object sender, EventArgs e)
        {
            try
            {
                us.Clave = txtClaveUs.Text;
                using (fBusqueda f = new fBusqueda(us, true))
                {
                    f.ShowDialog();
                    if (f.nuevo_registro)
                    {
                        using (frmDoctor fNuevo = new frmDoctor(us, -1))
                        {
                            fNuevo.ShowDialog();

                            if (!string.IsNullOrEmpty(fNuevo.nombre))
                            {
                                txtDoctor.Text = fNuevo.nombre;
                                venta.IdDoctor = fNuevo.IdDoctor;
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(f.nombre))
                    {
                        txtDoctor.Text = f.nombre;
                        venta.IdDoctor = f.Id;
                    }
                }


            }
            catch (Exception ex)
            { Utilerias.RegistrarLogError(ex, C_CLASE, "btnAgregarDoctor_Click", ""); }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                chkDescuento.Checked = false;
                using (frmConsultarLaboratorios f = new frmConsultarLaboratorios(Conexion.ConexionDB(), false, -1))
                {
                    f.ShowDialog();

                    if (f.laboratorio != null)
                    {
                        this.laboratorio = f.laboratorio;
                        CargarLaboratorio();
                    }
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "btnBuscar_Click", "");
            }
        }

        /// <summary>
        /// Evento que se ejecuta al presionar el botón "Agregar", 
        /// para agregar a la nota de venta un laboratorio.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string msjError = ValidarCamposLaboratorio();

                if (string.IsNullOrEmpty(msjError))
                {
                    CalcularImporte();
                    double subTotal = Convert.ToDouble(txtImporte.Text) / 1.16;
                    double impuestos = subTotal * 0.16;
                    LabPorVenta labPorVenta = new LabPorVenta()
                    {
                        IdLaboratorio = this.laboratorio.IdLaboratorio,
                        NumLaboratorios = Convert.ToInt32(txtCantidad.Text),
                        SubTotal = Convert.ToDecimal(subTotal),
                        Costo = Convert.ToDecimal(txtPUnitario.Text.Trim()),
                        CostoOriginal = Utilerias.EsPrecioEmergencia() ? this.laboratorio.PrecioVentaUrgencia : this.laboratorio.PrecioVenta
                    };


                    labPorVenta.Impuestos = Convert.ToDecimal(impuestos);
                    labPorVenta.Total = decimal.Round(labPorVenta.Impuestos.Value + labPorVenta.SubTotal.Value, 2);
                    decimal desc = string.IsNullOrEmpty(txtDescuento.Text.Trim()) ? 0 : Convert.ToDecimal(txtDescuento.Text.Trim());
                    venta.Total = (venta.Total == null ? 0 : venta.Total.Value) + labPorVenta.Total;
                    txtTotal.Text = venta.Total.ToString();
                    txtTotalConDescuento.Text = (venta.Total.Value - desc).ToString();

                    LabResumido labResumido = new LabResumido()
                    {
                        Clave = this.laboratorio.Clave,
                        Nombre = this.laboratorio.Descripcion,
                        Cantidad = labPorVenta.NumLaboratorios,
                        Costo = Convert.ToDecimal(labPorVenta.Total),
                        Familia = this.laboratorio.Familia
                    };

                    laboratorios.Add(labPorVenta);
                    lLabResumido.Add(labResumido);
                    ActualizarGVLaboratorios();
                    ReiniciarLaboratorio();
                }
                else Utilerias.MostrarMensaje(msjError);
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje(ex.Message);
                Utilerias.RegistrarLogError(ex, C_CLASE, "btnAgregar_Click", "");
            }
        }

        /// <summary>
        /// Evento que se ejecuta al presionar el botón "Pagar".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPagar_Click(object sender, EventArgs e)
        {
            btnPagar.Enabled = false;
            byte[] bytesPDF;
            try
            {
                //Validar datos de la venta
                string msjError = ValidarCampos();
                if (string.IsNullOrEmpty(msjError))
                {
                    this.AlimentarVenta();
                    if (chkCotizacion.Checked)
                    {
                        ventaNeg = new VentaNeg(Conexion.ConexionDB());
                        this.venta.IdCorteTurno = this.IdCorteTurno;
                        if (ventaNeg.AgregarCotizacion(this.venta, laboratorios, ref msjError))
                        {
                            string fileName = txtCliente.Text.Split(' ')[0] + "_cotizacion_" + DateTime.Now.ToString("ddMMMMyyyy_HHmmss") + ".pdf";
                            string filePath = C.PathTEMP + fileName;
                            bytesPDF = ventaNeg.CreatePDF(this.venta.IdVenta, this.config.Empresa, -1);
                            if (Utilerias.Imprimir(bytesPDF, filePath) == false)
                                Utilerias.MostrarMensaje("Cotización agregada, pero no pudo ser impresa.");
                            else Utilerias.MostrarMensaje("Cotización agregada.");

                            this.InicializarVenta();
                        }
                        else
                        {
                            btnPagar.Enabled = true;
                            Utilerias.MostrarMensaje(msjError);
                        }
                    }
                    else
                    {
                        using (frmPago f = new frmPago(Conexion.ConexionDB(), false, venta.Total.Value, venta.Total.Value, 1, -1))
                        {
                            if (f.ShowDialog() == DialogResult.OK)
                            {
                                //Agregar Venta, laboratorios por venta y primer Pago
                                ventaNeg = new VentaNeg(Conexion.ConexionDB());
                                this.venta.IdCorteTurno = this.IdCorteTurno;
                                if (f.pago.Cantidad > this.venta.Total)
                                {
                                    btnPagar.Enabled = true;
                                    Utilerias.MostrarMensaje("La cantidad del pago no puede ser mayor al total de la nota de venta");
                                }
                                else
                                {
                                    if (ventaNeg.Agregar(this.venta, laboratorios, f.pago, ref msjError))
                                    {
                                        string fileName = txtCliente.Text.Split(' ')[0] + "_" + DateTime.Now.ToString("ddMMMMyyyy_HHmmss") + ".pdf";
                                        string filePath = C.PathTEMP + fileName;
                                        bytesPDF = ventaNeg.CreatePDF(this.venta.IdVenta, this.config.Empresa, this.config.DigitosFolio);
                                        if (Utilerias.Imprimir(bytesPDF, filePath) == false)
                                            Utilerias.MostrarMensaje("Nota de venta agregada, pero no pudo ser impresa.");
                                        else Utilerias.MostrarMensaje("Nota de venta agregada.");

                                        this.InicializarVenta();
                                    }
                                    else
                                    {
                                        btnPagar.Enabled = true;
                                        Utilerias.MostrarMensaje(msjError);
                                    }
                                }
                            }
                            else { btnPagar.Enabled = true; }
                        }
                    }
                }
                else
                {
                    btnPagar.Enabled = true;
                    Utilerias.MostrarMensaje(msjError);
                }
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje(ex.Message);
                Utilerias.RegistrarLogError(ex, C_CLASE, "btnPagar_Click", "");
                btnPagar.Enabled = true;
            }
        }
        #endregion
        /// <summary>
        /// Evento para habilitar un descuento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkDescuento_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDescuento.Checked)
                {
                    //Mostrar formulario para agregar clave de administrador
                    using (frmLogin f = new frmLogin(true))
                    {
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            this.txtDescuento.Enabled = true;
                            chkDescuento.Checked = true;
                        }
                        else
                        {
                            this.txtDescuento.Enabled = false;
                            chkDescuento.Checked = false;
                        }
                    }
                }
                else this.txtDescuento.Enabled = chkDescuento.Checked;
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "chkDescuento_CheckedChanged", "");
            }
        }


        /// <summary>
        /// Evento al checar el precio unitario, para habilitarlo y cambiar el precio unitario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPUnitario_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (laboratorio != null)
                {
                    this.txtPUnitario.Enabled = chkPUnitario.Checked;
                    if (!chkPUnitario.Checked)
                    {
                        if (laboratorio.PrecioVenta.HasValue)
                            txtPUnitario.Text = laboratorio.PrecioVenta.Value.ToString();
                        else txtPUnitario.Text = "";
                        //Recalcular el importe
                        CalcularImporte();
                    }
                }
                else chkPUnitario.Checked = false;
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "chkPUnitario_CheckedChanged", "");
            }
        }

        private void chkDescripcion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (laboratorio != null)
                {
                    this.txtDescripcion.Enabled = chkDescripcion.Checked;
                    if (!chkDescripcion.Checked) txtDescripcion.Text = laboratorio.Descripcion;
                }
                else chkDescripcion.Checked = false;
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "chkDescripcion_CheckedChanged", "");
            }
        }

        /// <summary>
        /// Evento que se ejecuta al presionar una tecla sobre el control txtCantidad, control que muestra
        /// la cantidad de cantidad de laboratorios.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    //Recalcular el importe
                    this.CalcularImporte();
                }
                else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "txtCantidad_KeyPress", "");
            }
            //else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            //  (e.KeyChar != '.'))
            //    e.Handled = true;
        }

        /// <summary>
        /// Evento al presionar una tecla sobre el control qeu se refiere al precio unitario del laboratorio.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    //Recalcular el importe
                    CalcularImporte();
                }
                else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                   (e.KeyChar != '.'))
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "txtPUnitario_KeyPress", "");
            }
        }

        /// <summary>
        /// Evento al teclear sobre el control de clave del laboratorio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    string cadena = txtClave.Text.Trim();
                    //Buscar el laboratorio
                    laboratorio = laboratorioNeg.ObtenerPorClave(cadena);
                    if (laboratorio == null)
                    {
                        Utilerias.LimpiarControles(this.gbLaboratorio);
                        btnAgregar.Enabled = false;
                    }
                    else
                    {
                        this.CargarLaboratorio();
                    }
                    txtClave.Text = cadena;
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "txtClave_KeyPress", "");
            }
        }

        /// <summary>
        /// Evento al presionar una tecla en el control de clave de laboratorio.
        /// Si la tecla es F2 muestra un listado con todos los laboratorios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2) { btnBuscar_Click(btnBuscar, null); }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "txtClave_KeyDown", "");
            }
        }

        /// <summary>
        /// Evento que se ejecuta al cerrar este formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmVenta_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        #endregion
        #region Funciones Auxiliares
        /// <summary>
        /// Función para válidar que todos los campos sean correctos.
        /// Se válidan campos requeridos y expresiones regulares.
        /// Se valida si existe al menos un laboratorio
        /// Se valida si la clave de usuario existe.
        /// </summary>
        /// <returns></returns>
        public string ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtMaquina.Text.Trim()))
                return "Maquina es requerido.";
            else if (string.IsNullOrEmpty(txtCliente.Text.Trim()))
                return "Cliente es requerido.";
            else if (string.IsNullOrEmpty(txtClaveUs.Text.Trim()))
                return "Clave usuario es requerido.";
            else if (!Regex.IsMatch(txtClaveUs.Text.Trim(), "^[^|]{1,10}$"))
                return "Clave usuario inválido.";
            else if (!string.IsNullOrEmpty(rtbObservaciones.Text.Trim()) && !Regex.IsMatch(rtbObservaciones.Text.Trim(), "^[^|]{1,300}$"))
                return "Clave usuario inválido.";
            else if (txtDescuento.Enabled && !Regex.IsMatch(txtDescuento.Text.Trim(), "^[\\d]{1,5}(\\.[\\d]{1,2})?$"))
                return "Descuento inválido.";
            else if (txtDescuento.Enabled && Convert.ToDecimal(txtDescuento.Text.Trim()) > Convert.ToDecimal(txtTotal.Text.Trim()))
                return "El descuento no puede ser mayor al total de la venta.";
            else if (lLabResumido.Count < 1) //Verificar que la venta tenga al menos un laboratorio
                return "Debe existir al menos un laboratorio.";
            else
            { //Verificar si el usuario existe
                UsuarioNeg oUsNeg = new UsuarioNeg(Conexion.ConexionDB());
                int IdUs = oUsNeg.ObtenerIdUsuarioPorClave(txtClaveUs.Text.Trim());
                if (IdUs > 0)
                    this.venta.IdUsuario = IdUs;
                else return "Clave usuario no existe";
            }

            return "";
        }

        /// <summary>
        /// Función para válidar que todos los campos del laboratorio elegido sean correctos, para agregarlo a la nota de venta.
        /// </summary>
        /// <returns></returns>
        public string ValidarCamposLaboratorio()
        {
            string msjError = "";
            if (string.IsNullOrEmpty(txtPUnitario.Text.Trim()))
                return "Precio unitario es requerido.";
            else if (!Regex.IsMatch(txtPUnitario.Text.Trim(), C.EREG_DECIMAL))
                return "Precio unitario inválido.";
            decimal precio = Convert.ToDecimal(txtPUnitario.Text.Trim());
            if (!(precio > 0))
                return "Precio unitario debe ser mayor a cero pesos.";
            if (chkDescripcion.Checked)
            {
                if (string.IsNullOrEmpty(txtDescripcion.Text.Trim()))
                    return "Descripción es requerido.";
                else if (!Regex.IsMatch(txtDescripcion.Text.Trim(), "^[^|]{1,300}$"))
                    return "Descripción inválido.";
            }

            return msjError;
        }

        /// <summary>
        /// El número de maquina y el número de ceros del folio de la venta esta en el archivo de configuraciones
        /// </summary>
        public void InicializarVenta()
        {
            try
            {
                btnPagar.Enabled = true;
                ventaNeg = new VentaNeg(Conexion.ConexionDB());
                //Inicializamos los objetos
                this.lLabResumido = new List<LabResumido>();
                this.laboratorios = new List<LabPorVenta>();
                this.laboratorio = null;
                this.venta = new Venta();

                //Limpiamos los controles de captura
                Utilerias.LimpiarControles(this.gbDatosPrincipales);
                Utilerias.LimpiarControles(this.gbLaboratorio);
                Utilerias.LimpiarControles(this.gbTotales);
                ActualizarGVLaboratorios();
                chkCotizacion.Checked = false;
                chkPUnitario.Checked = false;
                chkDescuento.Checked = false;
                chkDescripcion.Checked = false;
                txtDescuento.Enabled = false;
                txtPUnitario.Enabled = false;
                txtDescripcion.Enabled = false;

                //Obtener el doctor -A QUIEN CORRESPONDA-
                DoctorNeg doctorNeg = new DoctorNeg(Conexion.ConexionDB());
                Doctor doctor = doctorNeg.ObtenerA_QUIEN_CORRESPONDA();
                this.venta.IdDoctor = doctor.IdDoctor;
                txtDoctor.Text = doctor.Nombre;

                //obtener folio
                txtFolio.Text = ventaNeg.ObtenerSigFolio().ToString().PadLeft(config.DigitosFolio, '0');

                //obtener maquina
                txtMaquina.Text = config.Maquina;
                horariosocupados = new Horarios();
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "InicializarVenta", "");
            }
        }

        /// <summary>
        /// Se alimenta el objeto venta con los datos capturados por el usuario
        /// </summary>
        /// <returns></returns>
        public void AlimentarVenta()
        {
            try
            {
                this.venta.Folio = Convert.ToInt32(txtFolio.Text);
                this.venta.NumMaquina = txtMaquina.Text.Trim();
                //IdCliente se obtiene al dar click en el botón "Agregar cliente"
                //IdUsuario se obtiene al validar los campos
                //IdDoctor se obtiene al dar click en el botón "Agregar doctor"
                this.venta.Total = Convert.ToDecimal(txtTotal.Text.Trim());
                double subTotal = (double)this.venta.Total / 1.16;

                if (string.IsNullOrEmpty(txtDescuento.Text)) { this.venta.Descuento = -1; }
                else
                {
                    double monto = Convert.ToDouble(txtDescuento.Text.Trim());
                    if (monto > 0)
                    {
                        this.venta.Descuento = (decimal)monto;
                        subTotal = ((double)this.venta.Total.Value - monto) / 1.16;
                        this.venta.Total = Convert.ToDecimal((double)this.venta.Total.Value - monto);
                    }
                }

                double impuestos = subTotal * 0.16;
                this.venta.SubTotal = (decimal)subTotal;
                this.venta.Impuestos = (decimal)impuestos;
                this.venta.Observaciones = rtbObservaciones.Text.Trim();
                this.venta.Estatus = chkCotizacion.Checked ? VentaNeg.EST_COTIZACION : "V";
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "AlimentarVenta", "");
            }
        }

        /// <summary>
        /// Método para actualizar la información del control donde se visualizan los laboratorios
        /// agregados a la venta
        /// </summary>
        public void ActualizarGVLaboratorios()
        {
            try
            {
                gvLaboratorios.DataSource = null;
                //Eliminar la columna eliminar
                if (gvLaboratorios.Columns.Count > 0) gvLaboratorios.Columns.RemoveAt(0);
                if (lLabResumido.Count > 0)
                {
                    gvLaboratorios.DataSource = lLabResumido;
                    if (gvLaboratorios.Columns.Count > 0)
                    {
                        gvLaboratorios.Columns["Costo"].DefaultCellStyle.Format = "C2";
                        gvLaboratorios.Columns["Costo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                    }
                    gvLaboratorios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    gvLaboratorios.Columns["Familia"].Visible = false;
                    gvLaboratorios.Columns["Reservacion"].Visible = false;

                    DataGridViewImageColumn btnE = new DataGridViewImageColumn();
                    btnE.MinimumWidth = 25;
                    btnE.Image = Resources.eliminar25;
                    btnE.Name = C.COLUMNA_ELIMINAR;
                    btnE.ToolTipText = "Eliminar laboratorio de la venta";
                    btnE.HeaderText = "";
                    btnE.ImageLayout = DataGridViewImageCellLayout.Stretch;
                    //btnE.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    gvLaboratorios.Columns.Add(btnE);

                    //foreach (DataGridViewColumn item in gvLaboratorios.Columns)
                    //    item.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "ActualizarGVLaboratorios", "");
            }
        }

        /// <summary>
        /// Multiplica el valor del control precio unitario por la cantidad.
        /// Agrega el calculo al control de importe
        /// </summary>
        public void CalcularImporte()
        {
            try
            {
                if (this.laboratorio != null)
                {
                    double cantidad = 0, pUnitario = 0;
                    if (!string.IsNullOrEmpty(txtCantidad.Text))
                        cantidad = Convert.ToDouble(txtCantidad.Text);
                    if (!string.IsNullOrEmpty(txtPUnitario.Text))
                        pUnitario = Convert.ToDouble(txtPUnitario.Text);

                    double importe = cantidad * pUnitario;
                    txtImporte.Text = importe.ToString();
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "CalcularImporte", "");
            }
        }

        /// <summary>
        /// Cargar los campos del laboratorio con la información del mismo objeto
        /// </summary>
        public void CargarLaboratorio()
        {
            try
            {
                txtDescripcion.Text = this.laboratorio.Descripcion;
                txtClave.Text = this.laboratorio.Clave;
                txtCantidad.Text = "1";
                /*Precio de emergencia si es sábado después de las dos de la tarde*/
                //if (Utilerias.EsPrecioEmergencia())
                //{
                //    txtPUnitario.Text = this.laboratorio.PrecioVentaUrgencia != null ? this.laboratorio.PrecioVentaUrgencia.Value.ToString() : this.laboratorio.PrecioVenta != null ? this.laboratorio.PrecioVenta.Value.ToString() : "";
                //}
                //else
                txtPUnitario.Text = this.laboratorio.PrecioVenta != null ? this.laboratorio.PrecioVenta.Value.ToString() : "";
                txtImporte.Text = txtPUnitario.Text;
                txtTotal.Text = venta.Total + txtImporte.Text;

                //Deshabilitar los campos del laboratorio
                txtCantidad.Enabled = true;
                txtPUnitario.Enabled = false;
                txtDescripcion.Enabled = false;
                this.btnAgregar.Enabled = true;
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "CargarLaboratorio", "");
            }
        }

        /// <summary>
        /// Limpiar los campos y deshabilitarlos.
        /// </summary>
        public void ReiniciarLaboratorio()
        {
            try
            {
                this.laboratorio = null;
                Utilerias.LimpiarControles(this.gbLaboratorio);

                //Deshabilitar los campos del laboratorio
                txtCantidad.Enabled = false;
                chkPUnitario.Checked = false;
                txtPUnitario.Enabled = false;
                chkDescripcion.Checked = false;
                txtDescripcion.Enabled = false;

                this.btnAgregar.Enabled = false;
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "ReiniciarLaboratorio", "");
            }
        }
        #endregion
        #region Menu contextual
        /// <summary>
        /// Evento click del item "Iniciar sesión como administrador".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void msItemSesion_Click(object sender, EventArgs e)
        {
            try
            {
                this.bIniciarSesion = true;
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "msItemSesion_Click", "");
            }
        }

        /// <summary>
        /// Evento click del item "Agregar usuario".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void msItemAgregarUs_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmUsuario f = new frmUsuario(Conexion.ConexionDB(), true, -1))
                {
                    if (f.ShowDialog() == DialogResult.OK) Utilerias.MostrarMensaje("Usuario agregado correctamente.");
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "msItemAgregarUs_Click", "");
            }
        }

        /// <summary>
        /// Evento clic del item "Corte de turno".
        /// Se mostrarán todas las ventas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsCorteDeTurno_Click(object sender, EventArgs e)
        {
            try
            {
                //Mostrar modal para contraseña de administrador
                using (frmLogin f = new frmLogin(true))
                {
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        //Mostrar modal de confirmación
                        if (Utilerias.MostrarMensajeConfirmacion("¿Esta seguro de hacer el cierre del día?"))
                        {
                            //Crear el corte de turno
                            string mensajeerror = "";
                            CorteTurnoNeg corteTurnoNeg = new CorteTurnoNeg(Conexion.ConexionDB());
                            int idCorteTurnoFinalizado = corteTurnoNeg.Finalizar(ref mensajeerror);
                            if (string.IsNullOrEmpty(mensajeerror))
                            {
                                /********************* Enviar correo con el corte de turno, adjunto el pdf y excel ******************************/
                                if (ParametrosSistema.EnviarCorreoCorteTurnoPorCorreo)
                                {
                                    CorteTurno corte = corteTurnoNeg.ObtenerPorId(idCorteTurnoFinalizado);
                                    string fechainiciocorte = corte.FechaInicio?.ToString("ddMMMMyyyy_HHmmss");
                                    string directorio = C.PathTEMP;
                                    string nombrearchexcel = "ReporteDetallado" + fechainiciocorte + ".xlsx";
                                    string filePathexcel = directorio + nombrearchexcel;
                                    corteTurnoNeg.CrearReporteDetalladoPorCorteTurno(idCorteTurnoFinalizado, filePathexcel);
                                    byte[] arch = corteTurnoNeg.CrearPDF(config.Empresa, corte.FechaInicio?.ToString()
                                        , Utilerias.ObtenerConDosDecimales(Convert.ToDouble(corte.TotalCtas.Value))
                                        , Utilerias.ObtenerConDosDecimales(Convert.ToDouble(corte.TotalEfectivo))
                                        , Utilerias.ObtenerConDosDecimales(Convert.ToDouble(corte.MontoTotal))
                                        , idCorteTurnoFinalizado);
                                    string fileName = "CORTE_TURNO_" + fechainiciocorte + ".pdf";
                                    string filePathpdf = directorio + fileName;
                                    System.IO.File.WriteAllBytes(filePathpdf, arch);
                                    Utilerias.EnviarCorteTurnoPorCorreo(ParametrosSistema.CorreoEnvioCorteTurno, fechainiciocorte, filePathpdf, filePathexcel);
                                }
                                /*********************Enviar correo con el corte de turno, adjunto el pdf ******************************/
                                using (frmCorteTurno fCT = new frmCorteTurno(Conexion.ConexionDB(), idCorteTurnoFinalizado, true, false, false, this.config))
                                {
                                    fCT.ShowDialog();
                                }
                            }
                            else { Utilerias.MostrarMensaje(mensajeerror); }

                            this.IdCorteTurno = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje("No se pudo crear el corte de turno. Cierre el programa y vuelva a abrir e intente de nuevo hacer el corte de turno.");
                Utilerias.RegistrarLogError(ex, C_CLASE, "tsCorteDeTurno_Click", "");
            }
        }
        #endregion

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDescuento.Text.Trim()) && !string.IsNullOrEmpty(txtTotal.Text.Trim()))
                {
                    double importe = Convert.ToDouble(txtTotal.Text.Trim()) - Convert.ToDouble(txtDescuento.Text.Trim());
                    txtTotalConDescuento.Text = importe.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "txtDescuento_KeyPress", "");
            }
        }

        /// <summary>
        /// Evento al cambiar el 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDescuento.Text.Trim()) && !string.IsNullOrEmpty(txtTotal.Text.Trim()))
            {
                double importe = Convert.ToDouble(txtTotal.Text.Trim()) - Convert.ToDouble(txtDescuento.Text.Trim());
                txtTotalConDescuento.Text = importe.ToString("N2");
            }
        }


        private void tsSaldoPorCliente_Click(object sender, EventArgs e)
        {
            //Mostrar formulario para mostrar saldo pendiente de un cliente
            using (frmSaldoPendiente f = new frmSaldoPendiente(Conexion.ConexionDB()))
            {
                f.ShowDialog();
            }
        }

        /// <summary>
        /// Evento al dar clic en "CANCELAR".
        /// Inicializar una nueva venta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            InicializarVenta();
        }

        private void tsNotasVenta_Click(object sender, EventArgs e)
        {
            this.MostrarVentas(false);
        }

        private void gvLaboratorios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                try
                {
                    string nombreColumna = gvLaboratorios.Columns[e.ColumnIndex].Name;
                    if (nombreColumna == C.COLUMNA_ELIMINAR) //Eliminar
                    {
                        //gvLaboratorios.DataSource = null;
                        double importe = Convert.ToDouble(lLabResumido[e.RowIndex].Cantidad * lLabResumido[e.RowIndex].Costo);
                        this.lLabResumido.RemoveAt(e.RowIndex);
                        this.laboratorios.RemoveAt(e.RowIndex);
                        ActualizarGVLaboratorios();
                        double total = Convert.ToDouble(txtTotal.Text);
                        double totalDesc = Convert.ToDouble(txtTotalConDescuento.Text);

                        total = total - importe;
                        totalDesc = totalDesc - importe;
                        venta.Total = Convert.ToDecimal(total);
                        txtTotal.Text = total.ToString("N2");
                        txtTotalConDescuento.Text = totalDesc.ToString("N2");

                        Utilerias.MostrarMensaje("Se elimino el laboratorio de la venta.");
                    }
                }
                catch (Exception ex)
                {
                    Utilerias.RegistrarLogError(ex, C_CLASE, "EliminarEditarUsuario", "");
                    Utilerias.MostrarMensaje(ex.Message);
                }
            }
        }

        /// <summary>
        /// Opción del menu contextual para mostrar las cotizaciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsCotizaciones_Click(object sender, EventArgs e)
        {
            this.MostrarVentas(true);
        }

        private void MostrarVentas(bool cotizaciones)
        {
            try
            {
                //Mostrar modal de confirmación
                using (frmCorteTurno f = new frmCorteTurno(Conexion.ConexionDB(), this.IdCorteTurno, false, true, cotizaciones, this.config))
                {
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        int idVenta = f.IdVenta;
                        //cargar nota de venta a partir de una cotización
                        this.InicializarVenta();
                        //Obtener nota de venta
                        VentaNeg oVentaNeg = new VentaNeg(Conexion.ConexionDB());
                        Cotizacion oCotizacion = oVentaNeg.ObtenerCotizacionPorId(idVenta);
                        ActualizarFormVenta(oCotizacion);
                    }
                }
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogError(ex, C_CLASE, "MostrarVentas", "Clave Usuario: " + txtClaveUs.Text);
                Utilerias.MostrarMensaje(ex.Message);
            }
        }

        private void ActualizarFormVenta(Cotizacion cotizacion)
        {
            txtClaveUs.Text = cotizacion.ClaveUsuario;
            txtCliente.Text = cotizacion.NombreCliente;
            txtDoctor.Text = cotizacion.NombreDoctor;

            venta = new Venta()
            {
                IdVenta = cotizacion.IdVenta,
                IdDoctor = cotizacion.IdDoctor,
                IdCliente = cotizacion.IdCliente,
                IdUsuario = cotizacion.IdUsuario,
                Total = cotizacion.Total,
                Impuestos = cotizacion.Impuestos,
                SubTotal = cotizacion.SubTotal
            };

            txtTotal.Text = venta.Total.Value.ToString("N2");
            if (cotizacion.Descuento > -1)
            {
                venta.Descuento = cotizacion.Descuento;
                txtDescuento.Text = venta.Descuento.Value.ToString("N2");
                txtTotalConDescuento.Text = (venta.Total.Value - venta.Descuento.Value).ToString("N2");
            }
            else
            {
                txtTotalConDescuento.Text = txtTotal.Text;
            }

            for (int i = 0; i < cotizacion.laboratorios.Count; i++)
            {
                if (Horarios.tienehorario(cotizacion.labResumidos[i].Familia))
                {
                    using (FHorario f = new FHorario(Conexion.ConexionDB(), cotizacion.labResumidos[i].Familia, cotizacion.labResumidos[i].Clave, cotizacion.labResumidos[i].Nombre, horariosocupados))
                    {
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            if (f.reservacion != null)
                            {
                                //cotizacion.labResumidos[i].Reservacion = f._horarioLaboratorio;
                                cotizacion.laboratorios[i].Reservacion = f.reservacion;
                                this.horariosocupados = f._horarios;
                            }
                        }
                    }
                }
            }
            laboratorios.AddRange(cotizacion.laboratorios);
            lLabResumido.AddRange(cotizacion.labResumidos);
            ActualizarGVLaboratorios();
            ReiniciarLaboratorio();
        }

        private void chkCotizacion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCotizacion.Checked)
            {
                btnPagar.Text = "AGREGAR";
            }
            else
            {
                btnPagar.Text = "PAGAR";
            }
        }

        /// <summary>
        /// Evento al dar en el botón citas, para ver los pendientes por semana
        /// </summary>
        private void btnCitas_Click(object sender, EventArgs e)
        {
            try
            {
                using (fCitas f = new fCitas(Conexion.ConexionDB()))
                {
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            { Utilerias.RegistrarLogError(ex, C_CLASE, "btnCitas_Click", ""); }
        }

        private void tsCorregirFolios_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmCorregirFolio f = new frmCorregirFolio())
                {
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            { Utilerias.RegistrarLogError(ex, C_CLASE, "btnCitas_Click", ""); }
        }
    }
}
