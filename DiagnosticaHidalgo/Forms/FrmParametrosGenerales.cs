using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Negocio;
// Agrega aquí el using del namespace donde está definida la clase C (colores del sistema).

namespace DiagnosticaHidalgo.Forms
{
    /// <summary>
    /// Formulario para consultar y editar ParametrosGenerales.
    /// No usa diseñador: agrega este archivo al proyecto y listo.
    /// Respeta la paleta del sistema (C.COLOR_SISTEMA y C.COLOR_SISTEMA_CLARO).
    ///
    /// Uso:
    ///   var repo = new ParametrosRepositorio(Conexion.ObtenerCadena());
    ///   using (var frm = new FrmParametrosGenerales(repo))
    ///       frm.ShowDialog(this);
    /// </summary>
    public class FrmParametrosGenerales : Form
    {
        // Cambia aquí la fuente si el sistema usa otra distinta.
        private const string NombreFuente = "Segoe UI";
        private static readonly Font FuenteNormal = new Font(NombreFuente, 9.75F);
        private static readonly Font FuenteNegrita = new Font(NombreFuente, 9.75F, FontStyle.Bold);

        private enum Modo { Navegando, Editando, Nuevo }

        private readonly ParametrosRepositorio _repo;
        private Modo _modo = Modo.Navegando;
        private bool _cargando;
        private bool _sucio;
        private byte[] _bytes;

        private DataGridView dgv;
        private TextBox txtClave, txtValor, txtDescripcion;
        private ComboBox cmbTipo;
        private CheckBox chkValor;
        private Panel panelBytes;
        private Label lblBytes, lblEstado;
        private Button btnArchivo, btnNuevo, btnEditar, btnGuardar, btnCancelar;

        public FrmParametrosGenerales(ParametrosRepositorio repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            ConstruirControles();

            Load += (s, e) =>
            {
                _repo.Recargar();
                CargarGrid(null);
                AplicarModo();
            };

            FormClosing += (s, e) =>
            {
                if (_modo != Modo.Navegando && _sucio &&
                    MessageBox.Show(this, "Hay cambios sin guardar. ¿Cerrar y descartarlos?", Text,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            };
        }

        // ------------------------------------------------------------------
        // Construcción de la interfaz
        // ------------------------------------------------------------------

        private void ConstruirControles()
        {
            Text = "PARÁMETROS GENERALES";
            Font = FuenteNormal;
            BackColor = Color.White;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(940, 560);

            var lblNota = new Label
            {
                Text = "* Selecciona un parámetro para editarlo o presiona NUEVO para crear uno.",
                Location = new Point(18, 10),
                AutoSize = true
            };

            // ---- Grupo: lista de parámetros ----
            var gbLista = new GroupBox
            {
                Text = "Parámetros",
                Location = new Point(12, 40),
                Size = new Size(520, 480)
            };

            dgv = new DataGridView
            {
                Location = new Point(12, 28),
                Size = new Size(496, 440),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                AutoGenerateColumns = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                GridColor = Color.Gainsboro,
                EnableHeadersVisualStyles = false
            };
            dgv.Columns.Add("Clave", "Clave");
            dgv.Columns.Add("Tipo", "Tipo");
            dgv.Columns.Add("Valor", "Valor");
            dgv.Columns.Add("Descripcion", "Descripción");
            dgv.Columns[0].FillWeight = 32;
            dgv.Columns[1].FillWeight = 14;
            dgv.Columns[2].FillWeight = 24;
            dgv.Columns[3].FillWeight = 30;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = C.COLOR_SISTEMA_CLARO;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = C.COLOR_SISTEMA;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = C.COLOR_SISTEMA_CLARO;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = C.COLOR_SISTEMA;
            dgv.ColumnHeadersDefaultCellStyle.Font = FuenteNegrita;
            dgv.DefaultCellStyle.Font = FuenteNormal;
            dgv.DefaultCellStyle.ForeColor = C.COLOR_SISTEMA;
            dgv.DefaultCellStyle.SelectionBackColor = C.COLOR_SISTEMA_CLARO;
            dgv.DefaultCellStyle.SelectionForeColor = C.COLOR_SISTEMA;
            dgv.SelectionChanged += (s, e) => AlSeleccionar();

            gbLista.Controls.Add(dgv);

            // ---- Grupo: datos del parámetro ----
            var gbDatos = new GroupBox
            {
                Text = "Datos del parámetro",
                Location = new Point(545, 40),
                Size = new Size(383, 400)
            };

            const int x = 15;
            const int ancho = 352;

            var lblClave = Etiqueta("Clave:", x, 28);
            txtClave = new TextBox { Location = new Point(x, 50), Width = ancho, MaxLength = 100 };

            var lblTipo = Etiqueta("Tipo:", x, 86);
            cmbTipo = new ComboBox
            {
                Location = new Point(x, 108),
                Width = ancho,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTipo.Items.AddRange(Enum.GetValues(typeof(TipoParametro)).Cast<object>().ToArray());
            cmbTipo.SelectedItem = TipoParametro.String;

            var lblValor = Etiqueta("Valor:", x, 144);
            txtValor = new TextBox
            {
                Location = new Point(x, 166),
                Size = new Size(ancho, 120),
                Multiline = true,
                AcceptsReturn = true,
                ScrollBars = ScrollBars.Vertical
            };
            chkValor = new CheckBox { Location = new Point(x, 168), Text = "Verdadero", AutoSize = true, Visible = false };

            panelBytes = new Panel { Location = new Point(x, 166), Size = new Size(ancho, 70), Visible = false };
            lblBytes = new Label { Location = new Point(0, 6), AutoSize = true, Text = "Sin archivo cargado" };
            btnArchivo = new Button { Location = new Point(0, 34), Size = new Size(170, 30), Text = "CARGAR ARCHIVO…" };
            panelBytes.Controls.Add(lblBytes);
            panelBytes.Controls.Add(btnArchivo);

            var lblDescripcion = Etiqueta("Descripción:", x, 298);
            txtDescripcion = new TextBox
            {
                Location = new Point(x, 320),
                Size = new Size(ancho, 64),
                Multiline = true,
                MaxLength = 250,
                ScrollBars = ScrollBars.Vertical
            };

            gbDatos.Controls.AddRange(new Control[]
            {
                lblClave, txtClave, lblTipo, cmbTipo, lblValor, txtValor, chkValor, panelBytes,
                lblDescripcion, txtDescripcion
            });

            // ---- Botones ----
            btnNuevo = Boton("NUEVO", 545, 452);
            btnEditar = Boton("EDITAR", 643, 452);
            btnGuardar = Boton("GUARDAR", 741, 452);
            btnCancelar = Boton("CANCELAR", 839, 452);

            lblEstado = new Label { Location = new Point(12, 530), AutoSize = true };

            Controls.AddRange(new Control[]
            {
                lblNota, gbLista, gbDatos, btnNuevo, btnEditar, btnGuardar, btnCancelar, lblEstado
            });

            // Colores y fuente del sistema para todos los controles
            AplicarEstilo(Controls);
            lblEstado.ForeColor = SystemColors.GrayText;

            CancelButton = btnCancelar; // Esc cancela la edición (solo si el botón está habilitado)

            // ---- Eventos ----
            txtClave.TextChanged += (s, e) => Marcar();
            txtValor.TextChanged += (s, e) => Marcar();
            txtDescripcion.TextChanged += (s, e) => Marcar();
            chkValor.CheckedChanged += (s, e) => Marcar();
            cmbTipo.SelectedIndexChanged += (s, e) => { ActualizarEditorValor(); Marcar(); };
            btnArchivo.Click += (s, e) => CargarArchivo();
            btnNuevo.Click += (s, e) => Nuevo();
            btnEditar.Click += (s, e) => Editar();
            btnGuardar.Click += (s, e) => Guardar();
            btnCancelar.Click += (s, e) => Cancelar();

            ActualizarEditorValor();
        }

        private static Label Etiqueta(string texto, int x, int y)
        {
            return new Label { Text = texto, Location = new Point(x, y), AutoSize = true };
        }

        private static Button Boton(string texto, int x, int y)
        {
            return new Button { Text = texto, Location = new Point(x, y), Size = new Size(88, 36) };
        }

        // ------------------------------------------------------------------
        // Estilo del sistema
        // ------------------------------------------------------------------

        private static void AplicarEstilo(Control.ControlCollection controles)
        {
            foreach (Control c in controles)
            {
                if (c is DataGridView)
                    continue; // ya tiene su propio estilo

                if (c is Button boton)
                {
                    EstilizarBoton(boton);
                }
                else if (c is GroupBox)
                {
                    c.Font = FuenteNegrita;
                    c.ForeColor = C.COLOR_SISTEMA;
                }
                else
                {
                    c.Font = FuenteNormal;
                    c.ForeColor = C.COLOR_SISTEMA;
                }

                if (c.HasChildren)
                    AplicarEstilo(c.Controls);
            }
        }

        private static void EstilizarBoton(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderColor = Color.Silver;
            b.UseVisualStyleBackColor = false;
            b.BackColor = C.COLOR_SISTEMA_CLARO;
            b.ForeColor = C.COLOR_SISTEMA;
            b.Font = FuenteNegrita;
            b.Cursor = Cursors.Hand;
        }

        // ------------------------------------------------------------------
        // Acciones: Nuevo, Editar, Guardar, Cancelar
        // ------------------------------------------------------------------

        private void Nuevo()
        {
            _modo = Modo.Nuevo;
            LimpiarCampos();
            AplicarModo();
            Estado("Capturando un parámetro nuevo.");
            txtClave.Focus();
        }

        private void Editar()
        {
            if (ParametroSeleccionado() == null) return;

            _modo = Modo.Editando;
            _sucio = false;
            AplicarModo();
            Estado("Editando. Presiona GUARDAR para aplicar los cambios.");
            EnfocarValor();
        }

        private void Cancelar()
        {
            if (_sucio &&
                MessageBox.Show(this, "¿Descartar los cambios?", Text,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            _modo = Modo.Navegando;

            ParametroGeneral p = ParametroSeleccionado();
            if (p != null) MostrarParametro(p);
            else LimpiarCampos();

            AplicarModo();
            Estado("Cambios cancelados.");
        }

        private void Guardar()
        {
            try
            {
                string clave = txtClave.Text.Trim();
                if (clave.Length == 0)
                {
                    Aviso("Escribe la clave del parámetro.", txtClave);
                    return;
                }

                if (_modo == Modo.Nuevo && _repo.Existe(clave))
                {
                    Aviso("Ya existe un parámetro con la clave «" + clave + "».", txtClave);
                    return;
                }

                if (!TryObtenerValor(out object valor))
                    return;

                _repo.Guardar(clave, valor, txtDescripcion.Text.Trim());

                _modo = Modo.Navegando;
                _sucio = false;
                CargarGrid(clave);
                AplicarModo();
                Estado("Parámetro «" + clave + "» guardado.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "No se pudo guardar el parámetro:\n\n" + ex.Message, Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ------------------------------------------------------------------
        // Lectura del valor capturado según el tipo
        // ------------------------------------------------------------------

        private bool TryObtenerValor(out object valor)
        {
            valor = null;

            switch (TipoSeleccionado())
            {
                case TipoParametro.String:
                    valor = txtValor.Text;
                    return true;

                case TipoParametro.Int:
                    string textoInt = txtValor.Text.Trim();
                    if (int.TryParse(textoInt, NumberStyles.Integer, CultureInfo.CurrentCulture, out int entero) ||
                        int.TryParse(textoInt, NumberStyles.Integer, CultureInfo.InvariantCulture, out entero))
                    {
                        valor = entero;
                        return true;
                    }
                    Aviso("El valor debe ser un número entero (por ejemplo 3).", txtValor);
                    return false;

                case TipoParametro.Double:
                    string textoDouble = txtValor.Text.Trim();
                    if ((double.TryParse(textoDouble, NumberStyles.Float, CultureInfo.CurrentCulture, out double real) ||
                         double.TryParse(textoDouble, NumberStyles.Float, CultureInfo.InvariantCulture, out real)) &&
                        !double.IsNaN(real) && !double.IsInfinity(real))
                    {
                        valor = real;
                        return true;
                    }
                    Aviso("El valor debe ser un número decimal (por ejemplo 16.5).", txtValor);
                    return false;

                case TipoParametro.Bool:
                    valor = chkValor.Checked;
                    return true;

                case TipoParametro.Bytes:
                    if (_bytes == null)
                    {
                        Aviso("Selecciona un archivo con «CARGAR ARCHIVO».", btnArchivo);
                        return false;
                    }
                    valor = _bytes;
                    return true;

                default:
                    return false;
            }
        }

        // ------------------------------------------------------------------
        // Lista y campos
        // ------------------------------------------------------------------

        private void CargarGrid(string claveSeleccionar)
        {
            try
            {
                var lista = _repo.ObtenerTodos()
                    .OrderBy(p => p.Clave, StringComparer.OrdinalIgnoreCase)
                    .ToList();

                dgv.Rows.Clear();
                foreach (ParametroGeneral p in lista)
                {
                    int i = dgv.Rows.Add(p.Clave, p.Tipo.ToString(), Resumen(p), p.Descripcion);
                    dgv.Rows[i].Tag = p;
                }

                DataGridViewRow fila = dgv.Rows.Cast<DataGridViewRow>().FirstOrDefault(r =>
                    string.Equals(((ParametroGeneral)r.Tag).Clave, claveSeleccionar,
                        StringComparison.OrdinalIgnoreCase));

                if (fila == null && dgv.Rows.Count > 0)
                    fila = dgv.Rows[0];

                if (fila != null)
                {
                    dgv.CurrentCell = fila.Cells[0];
                    fila.Selected = true;
                }
                else
                {
                    LimpiarCampos();
                }

                Estado(lista.Count + " parámetro(s).");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "No se pudieron cargar los parámetros:\n\n" + ex.Message, Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string Resumen(ParametroGeneral p)
        {
            switch (p.Tipo)
            {
                case TipoParametro.Bytes:
                    try
                    {
                        return "(" + Convert.FromBase64String(p.Valor).Length.ToString("N0") + " bytes)";
                    }
                    catch (FormatException)
                    {
                        return "(Base64 no válido)";
                    }

                case TipoParametro.Bool:
                    return p.Valor;

                default:
                    string v = (p.Valor ?? "").Replace("\r", " ").Replace("\n", " ");
                    return v.Length > 80 ? v.Substring(0, 80) + "…" : v;
            }
        }

        private ParametroGeneral ParametroSeleccionado()
        {
            return dgv.CurrentRow?.Tag as ParametroGeneral;
        }

        private void AlSeleccionar()
        {
            if (_modo != Modo.Navegando) return;

            ParametroGeneral p = ParametroSeleccionado();
            if (p == null) LimpiarCampos();
            else MostrarParametro(p);

            AplicarModo();
        }

        private void MostrarParametro(ParametroGeneral p)
        {
            _cargando = true;
            try
            {
                txtClave.Text = p.Clave;
                cmbTipo.SelectedItem = p.Tipo;
                txtValor.Text = "";
                chkValor.Checked = false;
                _bytes = null;

                switch (p.Tipo)
                {
                    case TipoParametro.Bool:
                        chkValor.Checked = bool.TryParse(p.Valor, out bool logico) && logico;
                        break;

                    case TipoParametro.Bytes:
                        try { _bytes = Convert.FromBase64String(p.Valor); }
                        catch (FormatException) { _bytes = null; }
                        break;

                    default:
                        txtValor.Text = p.Valor;
                        break;
                }

                txtDescripcion.Text = p.Descripcion ?? "";
                ActualizarBytes();
                ActualizarEditorValor();
            }
            finally
            {
                _cargando = false;
                _sucio = false;
            }
        }

        private void LimpiarCampos()
        {
            _cargando = true;
            try
            {
                txtClave.Text = "";
                cmbTipo.SelectedItem = TipoParametro.String;
                txtValor.Text = "";
                chkValor.Checked = false;
                txtDescripcion.Text = "";
                _bytes = null;
                ActualizarBytes();
                ActualizarEditorValor();
            }
            finally
            {
                _cargando = false;
                _sucio = false;
            }
        }

        // ------------------------------------------------------------------
        // Estado de la interfaz
        // ------------------------------------------------------------------

        private void AplicarModo()
        {
            bool editando = _modo != Modo.Navegando;

            dgv.Enabled = !editando;
            txtClave.ReadOnly = _modo != Modo.Nuevo;
            cmbTipo.Enabled = editando;
            txtValor.ReadOnly = !editando;
            chkValor.Enabled = editando;
            btnArchivo.Enabled = editando;
            txtDescripcion.ReadOnly = !editando;

            btnNuevo.Enabled = !editando;
            btnEditar.Enabled = !editando && ParametroSeleccionado() != null;
            btnGuardar.Enabled = editando;
            btnCancelar.Enabled = editando;
        }

        private void ActualizarEditorValor()
        {
            TipoParametro tipo = TipoSeleccionado();
            txtValor.Visible = tipo == TipoParametro.String || tipo == TipoParametro.Int || tipo == TipoParametro.Double;
            chkValor.Visible = tipo == TipoParametro.Bool;
            panelBytes.Visible = tipo == TipoParametro.Bytes;
        }

        private void ActualizarBytes()
        {
            lblBytes.Text = _bytes == null
                ? "Sin archivo cargado"
                : "Archivo cargado: " + _bytes.Length.ToString("N0") + " bytes";
        }

        private void CargarArchivo()
        {
            using (var dlg = new OpenFileDialog { Title = "Selecciona el archivo", Filter = "Todos los archivos|*.*" })
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    _bytes = File.ReadAllBytes(dlg.FileName);
                    ActualizarBytes();
                    Marcar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "No se pudo leer el archivo:\n\n" + ex.Message, Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void EnfocarValor()
        {
            switch (TipoSeleccionado())
            {
                case TipoParametro.Bool: chkValor.Focus(); break;
                case TipoParametro.Bytes: btnArchivo.Focus(); break;
                default: txtValor.Focus(); break;
            }
        }

        private TipoParametro TipoSeleccionado()
        {
            return cmbTipo.SelectedItem is TipoParametro t ? t : TipoParametro.String;
        }

        private void Marcar()
        {
            if (!_cargando) _sucio = true;
        }

        private void Estado(string texto)
        {
            lblEstado.Text = texto;
        }

        private void Aviso(string mensaje, Control foco)
        {
            MessageBox.Show(this, mensaje, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (foco != null) foco.Focus();
        }
    }
}