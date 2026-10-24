using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DiagnosticaHidalgo.Negocio
{
    public static class ParametroGen
    {

        /// <summary>Nombres (claves) de los parámetros tal como están en la tabla.</summary>
        public static class Claves
        {
            public const string NombreClinica = "NombreClinica";
        }

        #region Consultar
        public static string Cadena_conexion_sucursa_da()
        {
            return "Data Source=100.106.68.15\\SQLEXPRESS;" +
                    "Initial Catalog=DAjalpan;" +
                    "Persist Security Info=True;" +
                    "User ID=sa;" +
                    "Password=_8CU4*'+EMTqaw$;";
        }
        #endregion
    }

    public enum TipoParametro
    {
        String,
        Int,
        Double,
        Bytes,
        Bool
    }

    /// <summary>Un renglón de la tabla ParametrosGenerales.</summary>
    public class ParametroGeneral
    {
        private TipoParametro _tipo;
        private string _valor;
        private bool _convertido;
        private object _valorTipado;

        public int Id { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaModificacion { get; set; }

        public TipoParametro Tipo
        {
            get { return _tipo; }
            set { _tipo = value; _convertido = false; }
        }

        /// <summary>Valor tal como está en la base de datos (texto).</summary>
        public string Valor
        {
            get { return _valor; }
            set { _valor = value; _convertido = false; }
        }

        /// <summary>
        /// Valor convertido según Tipo: string, int, double, byte[] o bool.
        /// Lanza FormatException si el texto guardado no corresponde al tipo.
        /// </summary>
        public object ValorTipado
        {
            get
            {
                if (!_convertido)
                {
                    _valorTipado = Convertir();
                    _convertido = true;
                }
                return _valorTipado;
            }
        }

        /// <summary>
        /// Devuelve el valor como T, validando que T coincida con el tipo guardado.
        /// Solo se permite ampliar un int a double o long. Cualquier otra
        /// combinación (por ejemplo pedir int de un parámetro double) lanza InvalidCastException.
        /// </summary>
        public T ObtenerValor<T>()
        {
            object v = ValorTipado;
            Type pedido = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

            if (pedido == typeof(object) || pedido == v.GetType())
                return (T)v;

            if (v is int)
            {
                if (pedido == typeof(double)) return (T)(object)(double)(int)v;
                if (pedido == typeof(long)) return (T)(object)(long)(int)v;
            }

            throw new InvalidCastException(
                "El parámetro '" + Clave + "' es de tipo " + _tipo +
                " y se solicitó como " + typeof(T).Name + ".");
        }

        private object Convertir()
        {
            if (_valor == null)
                throw new InvalidOperationException("El parámetro '" + Clave + "' no tiene valor.");

            switch (_tipo)
            {
                case TipoParametro.String:
                    return _valor;

                case TipoParametro.Int:
                    if (int.TryParse(_valor, NumberStyles.Integer, CultureInfo.InvariantCulture, out int entero))
                        return entero;
                    throw Invalido("un entero (int)", null);

                case TipoParametro.Double:
                    if (double.TryParse(_valor, NumberStyles.Float, CultureInfo.InvariantCulture, out double real))
                        return real;
                    throw Invalido("un número decimal (double)", null);

                case TipoParametro.Bytes:
                    try
                    {
                        return System.Convert.FromBase64String(_valor);
                    }
                    catch (FormatException ex)
                    {
                        throw Invalido("Base64 válido (bytes)", ex);
                    }

                case TipoParametro.Bool:
                    if (bool.TryParse(_valor, out bool logico))
                        return logico;
                    throw Invalido("un valor lógico (true o false)", null);

                default:
                    throw new InvalidOperationException("Tipo de parámetro no soportado: " + _tipo);
            }
        }

        private FormatException Invalido(string esperado, Exception interna)
        {
            return new FormatException(
                "El valor guardado del parámetro '" + Clave + "' no es " + esperado + ".", interna);
        }
    }

    /// <summary>
    /// Lee y guarda ParametrosGenerales. Carga la tabla una sola vez y la mantiene en memoria
    /// hasta que se llame a Recargar() o a Guardar().
    /// </summary>
    public class ParametrosRepositorio
    {
        private const string SqlSelect =
            "SELECT Id, Clave, Tipo, Valor, Descripcion, FechaModificacion " +
            "FROM dbo.ParametrosGenerales";

        private const string SqlGuardar =
            "UPDATE dbo.ParametrosGenerales " +
            "SET Tipo = @Tipo, Valor = @Valor, " +
            "    Descripcion = COALESCE(@Descripcion, Descripcion), " +
            "    FechaModificacion = SYSDATETIME() " +
            "WHERE Clave = @Clave; " +
            "IF @@ROWCOUNT = 0 " +
            "    INSERT INTO dbo.ParametrosGenerales (Clave, Tipo, Valor, Descripcion) " +
            "    VALUES (@Clave, @Tipo, @Valor, @Descripcion);";

        private readonly string _cadenaConexion;
        private readonly object _candado = new object();
        private Dictionary<string, ParametroGeneral> _cache;

        public ParametrosRepositorio(string cadenaConexion)
        {
            if (string.IsNullOrWhiteSpace(cadenaConexion))
                throw new ArgumentException("La cadena de conexión es obligatoria.", nameof(cadenaConexion));
            _cadenaConexion = cadenaConexion;
        }

        // ----------------------------- Lectura -----------------------------

        public IEnumerable<ParametroGeneral> ObtenerTodos()
        {
            return ObtenerCache().Values;
        }

        public bool Existe(string clave)
        {
            return ObtenerCache().ContainsKey(clave);
        }

        public ParametroGeneral ObtenerParametro(string clave)
        {
            if (!ObtenerCache().TryGetValue(clave, out ParametroGeneral p))
                throw new KeyNotFoundException("No existe el parámetro '" + clave + "'.");
            return p;
        }

        /// <summary>Valor ya convertido a su tipo real (string, int, double o byte[]).</summary>
        public object ObtenerValor(string clave)
        {
            return ObtenerParametro(clave).ValorTipado;
        }

        /// <summary>Valor como T; lanza excepción si no existe o si T no coincide con el tipo guardado.</summary>
        public T Obtener<T>(string clave)
        {
            return ObtenerParametro(clave).ObtenerValor<T>();
        }

        /// <summary>Valor como T; si el parámetro no existe devuelve valorPorDefecto.</summary>
        public T Obtener<T>(string clave, T valorPorDefecto)
        {
            if (!ObtenerCache().TryGetValue(clave, out ParametroGeneral p))
                return valorPorDefecto;
            return p.ObtenerValor<T>();
        }

        public void Recargar()
        {
            lock (_candado) { _cache = null; }
        }

        // ---------------------------- Escritura ----------------------------

        /// <summary>
        /// Crea o actualiza un parámetro. El tipo se deduce del valor:
        /// string, int (o long dentro del rango de int), double/float/decimal, byte[].
        /// </summary>
        public void Guardar(string clave, object valor, string descripcion = null)
        {
            if (string.IsNullOrWhiteSpace(clave))
                throw new ArgumentException("La clave es obligatoria.", nameof(clave));
            if (valor == null)
                throw new ArgumentNullException(nameof(valor));

            TipoParametro tipo;
            string texto;

            switch (valor)
            {
                case string s:
                    tipo = TipoParametro.String;
                    texto = s;
                    break;
                case bool bo:
                    tipo = TipoParametro.Bool;
                    texto = bo ? "true" : "false";
                    break;
                case int i:
                    tipo = TipoParametro.Int;
                    texto = i.ToString(CultureInfo.InvariantCulture);
                    break;
                case long l when l >= int.MinValue && l <= int.MaxValue:
                    tipo = TipoParametro.Int;
                    texto = l.ToString(CultureInfo.InvariantCulture);
                    break;
                case double d:
                    tipo = TipoParametro.Double;
                    texto = DoubleATexto(d);
                    break;
                case float f:
                    tipo = TipoParametro.Double;
                    texto = DoubleATexto(f);
                    break;
                case decimal m:
                    tipo = TipoParametro.Double;
                    texto = DoubleATexto((double)m);
                    break;
                case byte[] b:
                    tipo = TipoParametro.Bytes;
                    texto = Convert.ToBase64String(b);
                    break;
                default:
                    throw new ArgumentException("Tipo no soportado: " + valor.GetType().Name +
                                                ". Usa string, int, double o byte[].", nameof(valor));
            }

            using (var cn = new SqlConnection(_cadenaConexion))
            using (var cmd = new SqlCommand(SqlGuardar, cn))
            {
                cmd.Parameters.Add(Parametro("@Clave", SqlDbType.NVarChar, 100, clave));
                cmd.Parameters.Add(Parametro("@Tipo", SqlDbType.VarChar, 10, TipoATexto(tipo)));
                cmd.Parameters.Add(Parametro("@Valor", SqlDbType.NVarChar, -1, texto));
                cmd.Parameters.Add(Parametro("@Descripcion", SqlDbType.NVarChar, 250, descripcion));

                cn.Open();
                cmd.ExecuteNonQuery();
            }

            Recargar();
        }

        // ----------------------------- Privados ----------------------------

        private Dictionary<string, ParametroGeneral> ObtenerCache()
        {
            lock (_candado)
            {
                if (_cache == null)
                    _cache = CargarDesdeBaseDeDatos();
                return _cache;
            }
        }

        private Dictionary<string, ParametroGeneral> CargarDesdeBaseDeDatos()
        {
            var resultado = new Dictionary<string, ParametroGeneral>(StringComparer.OrdinalIgnoreCase);

            using (var cn = new SqlConnection(_cadenaConexion))
            using (var cmd = new SqlCommand(SqlSelect, cn))
            {
                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var p = new ParametroGeneral
                        {
                            Id = rd.GetInt32(0),
                            Clave = rd.GetString(1),
                            Tipo = TextoATipo(rd.GetString(2)),
                            Valor = rd.GetString(3),
                            Descripcion = rd.IsDBNull(4) ? null : rd.GetString(4),
                            FechaModificacion = rd.GetDateTime(5)
                        };
                        resultado[p.Clave] = p;
                    }
                }
            }

            return resultado;
        }

        private static string DoubleATexto(double d)
        {
            if (double.IsNaN(d) || double.IsInfinity(d))
                throw new ArgumentException("NaN e infinito no se pueden guardar como parámetro.");
            return d.ToString("R", CultureInfo.InvariantCulture);
        }

        private static SqlParameter Parametro(string nombre, SqlDbType tipo, int tamano, object valor)
        {
            var p = new SqlParameter(nombre, tipo, tamano);
            p.Value = valor ?? DBNull.Value;
            return p;
        }

        private static string TipoATexto(TipoParametro tipo)
        {
            switch (tipo)
            {
                case TipoParametro.String: return "string";
                case TipoParametro.Int: return "int";
                case TipoParametro.Double: return "double";
                case TipoParametro.Bytes: return "bytes";
                default: return "bool";
            }
        }

        private static TipoParametro TextoATipo(string texto)
        {
            switch ((texto ?? "").Trim().ToLowerInvariant())
            {
                case "string": return TipoParametro.String;
                case "int": return TipoParametro.Int;
                case "double": return TipoParametro.Double;
                case "bytes": return TipoParametro.Bytes;
                case "bool": return TipoParametro.Bool;
                default: throw new InvalidOperationException("Tipo de parámetro desconocido: '" + texto + "'.");
            }
        }
    }
}
