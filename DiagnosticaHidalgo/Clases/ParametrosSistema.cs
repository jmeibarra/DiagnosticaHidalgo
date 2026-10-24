using DiagnosticaHidalgo.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace DiagnosticaHidalgo.Clases
{
    /// <summary>
    /// Parámetros generales del sistema, cargados una vez y disponibles por nombre,
    /// ya convertidos a su tipo (string, int, double, bool, byte[]).
    ///
    /// 1) Al iniciar el sistema (después de tener la cadena de conexión):
    ///      ParametrosSistema.Inicializar(new ParametrosRepositorio(Conexion.ObtenerCadena()));
    ///
    /// 2) En cualquier parte del sistema:
    ///      lblClinica.Text = ParametrosSistema.NombreClinica;
    ///      decimal total = subtotal * (1 + (decimal)ParametrosSistema.IvaPorcentaje / 100m);
    ///
    /// 3) Después de editar parámetros (por ejemplo al cerrar el formulario):
    ///      ParametrosSistema.Recargar();
    /// </summary>
    public static class ParametrosSistema
    {
        /// <summary>Nombres (claves) de los parámetros tal como están en la tabla.</summary>
        public static class Claves
        {
            public const string tokencorreoclinica = "token.correo.clinica";
            public const string correoclinica = "correo.clinica";
            public const string correoenviocorteturno = "correo.envio.corte.turno";
            public const string enviarcorreocorteturnoporcorreo = "enviar.correo.corte.turno.por.correo";
        }

        private static readonly object Candado = new object();
        private static readonly List<string> _advertencias = new List<string>();
        private static ParametrosRepositorio _repo;
        private static bool _inicializado;

        // Valores cargados (se llenan en Cargar)
        private static string _tokencorreoclinica;
        private static string _correoclinica;
        private static string _correoenviocorteturno;
        private static bool _enviarcorreocorteturnoporcorreo;

        // ------------------------------------------------------------------
        // Parámetros del sistema (agrega aquí uno por cada parámetro nuevo)
        // ------------------------------------------------------------------

        /// <summary>Nombre que se imprime en los tickets.</summary>
        public static string TokenCorreoClinica { get { Verificar(); return _tokencorreoclinica; } }

        /// <summary>Intentos fallidos antes de bloquear al usuario.</summary>
        public static string CorreoClinica { get { Verificar(); return _correoclinica; } }

        /// <summary>Porcentaje de IVA aplicado a las ventas (16 = 16%).</summary>
        public static string CorreoEnvioCorteTurno { get { Verificar(); return _correoenviocorteturno; } }

        public static bool EnviarCorreoCorteTurnoPorCorreo { get { Verificar(); return _enviarcorreocorteturnoporcorreo; } }

        // ------------------------------------------------------------------
        // Carga
        // ------------------------------------------------------------------

        /// <summary>Carga todos los parámetros desde la base de datos. Llamar una vez al iniciar.</summary>
        public static void Inicializar(ParametrosRepositorio repositorio)
        {
            if (repositorio == null)
                throw new ArgumentNullException(nameof(repositorio));

            lock (Candado)
            {
                _repo = repositorio;
                Cargar();
            }
        }

        /// <summary>Vuelve a leer los parámetros de la base de datos (por ejemplo, después de editarlos).</summary>
        public static void Recargar()
        {
            lock (Candado)
            {
                if (_repo == null)
                    throw new InvalidOperationException(
                        "Primero llama a ParametrosSistema.Inicializar(...) al iniciar el sistema.");
                Cargar();
            }
        }

        ///// <summary>
        ///// Avisos de la última carga: parámetros que no existen o con un tipo incorrecto
        ///// (en esos casos se usa el valor por defecto). Útil para mostrarlos al iniciar.
        ///// </summary>
        //public static IReadOnlyList<string> Advertencias
        //{
        //    get
        //    {
        //        lock (Candado)
        //        {
        //            return _advertencias.ToArray();
        //        }
        //    }
        //}

        /// <summary>Repositorio con el que se inicializó (por ejemplo, para abrir el formulario de edición).</summary>
        public static ParametrosRepositorio Repositorio
        {
            get { Verificar(); return _repo; }
        }

        private static void Cargar()
        {
            _repo.Recargar(); // descarta la caché para leer lo último de la base de datos
            _advertencias.Clear();

            // Cada línea: clave, valor por defecto si no existe o está mal capturado
            _tokencorreoclinica = Leer(Claves.tokencorreoclinica, "");
            _correoclinica = Leer(Claves.correoclinica, "");
            _correoenviocorteturno = Leer(Claves.correoenviocorteturno, "");
            _enviarcorreocorteturnoporcorreo = Leer(Claves.enviarcorreocorteturnoporcorreo, false);

            _inicializado = true;
        }

        private static T Leer<T>(string clave, T porDefecto)
        {
            if (!_repo.Existe(clave))
            {
                _advertencias.Add("No existe el parámetro '" + clave + "'. Se usa el valor por defecto.");
                return porDefecto;
            }

            try
            {
                return _repo.Obtener(clave, porDefecto);
            }
            catch (Exception ex) when (ex is InvalidCastException || ex is FormatException)
            {
                _advertencias.Add(ex.Message + " Se usa el valor por defecto.");
                return porDefecto;
            }
        }

        private static void Verificar()
        {
            if (!_inicializado)
                throw new InvalidOperationException(
                    "Los parámetros no se han cargado. Llama a ParametrosSistema.Inicializar(...) al iniciar el sistema.");
        }

        /* ------------------- CÓMO AGREGAR UN PARÁMETRO NUEVO -------------------
 
        1. Crea el parámetro en la tabla (o con el formulario de parámetros).
        2. Agrega su nombre en Claves:          public const string Moneda = "Moneda";
        3. Agrega el campo privado:             private static string _moneda;
        4. Agrega la propiedad:                 public static string Moneda { get { Verificar(); return _moneda; } }
        5. Cárgalo en Cargar():                 _moneda = Leer(Claves.Moneda, "MXN");
 
        -------------------------------------------------------------------- */
    }
}