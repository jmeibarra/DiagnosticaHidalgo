using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace DiagnosticaHidalgo.Clases.Seguridad
{
    /// <summary>
    /// Lee la cadena de conexión del App.config. Si empieza con "enc:", la descifra
    /// con la clase Cifrado (AES + HMAC), es decir, con lo generado en la página web.
    /// Requiere referencia a System.Configuration.
    /// </summary>
    public static class Conexion
    {

        //// La clave se toma de esta variable de entorno de Windows (recomendado)...
        //private const string VariableEntorno = "DH_CLAVE_MAESTRA";
        //// ...y si no existe, de esta constante. Debe ser la MISMA clave que usaste en la página.
        //private const string ClaveIncrustada = "CAMBIA-ESTA-CLAVE";

        //private static string _cache;

        private const string Prefijo = "enc:";
        private const string Nombre = "ClinicaDB";
        private static string _conexion;
        private static readonly object _candado = new object();

        public static string ConexionDB()
        {
            lock (_candado)
            {
                if (_conexion != null) return _conexion;   // ya descifrada: sin espera

                var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var item = cfg.ConnectionStrings.ConnectionStrings[Nombre]
                    ?? throw new ConfigurationErrorsException(
                        "No existe la cadena de conexión '" + Nombre + "' en App.config.");

                string valor = item.ConnectionString;

                // Ya está cifrada: solo descifrar
                if (valor.StartsWith(Prefijo, StringComparison.Ordinal))
                {
                    _conexion = Cifrado.DesprotegerDpapi(valor.Substring(Prefijo.Length));
                    return _conexion;
                }

                // Texto plano: cifrar y guardar para la próxima vez
                try
                {
                    item.ConnectionString = Prefijo + Cifrado.ProtegerDpapi(valor);
                    cfg.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("connectionStrings");
                }
                catch (Exception)
                {
                    // Sin permisos de escritura: se usa la cadena tal cual
                    // y se intentará cifrar en la siguiente ejecución.
                }

                _conexion = valor;
                return _conexion;
            }
        }
    }

    /// <summary>
    /// Cifrado AES-256 (CBC) con HMAC-SHA256 para verificar integridad.
    /// El mismo valor cifrado se puede descifrar en cualquier PC que tenga esta misma clave.
    /// </summary>
    public static class Cifrado
    {
        // ============================================================
        // CLAVE MAESTRA: cámbiala por una frase larga y única (40+ caracteres).
        // IMPORTANTE: si la cambias después, todo lo que ya cifraste
        // dejará de poder descifrarse. Guarda los textos originales.
        // ============================================================
        private const string ClaveMaestra = "lKutk+enWJBBmz%3mVJaVN&l5fJE5mW$eWS9%xt28m6KrX65ncGdJOPAX3X3W=3$";


        private const int Iteraciones = 600000;
        private const byte Version = 1;
        private const int TamSal = 16;
        private const int TamIv = 16;
        private const int TamMac = 32;
        // Derivar la clave tarda ~1 segundo; se guarda el resultado para no repetirlo
        // cada vez que una clase de negocio pide la cadena de conexión.
        private static readonly Dictionary<string, string> Cache = new Dictionary<string, string>();
        private static readonly object Candado = new object();

        // ---------- Con clave maestra interna (lo que usa tu sistema) ----------

        public static string Proteger(string textoPlano)
        {
            return Cifrar(textoPlano, ClaveMaestra);
        }

        public static string Desproteger(string base64)
        {
            lock (Candado)
            {
                string claro;
                if (Cache.TryGetValue(base64, out claro)) return claro;
                claro = Descifrar(base64, ClaveMaestra);
                Cache[base64] = claro;
                return claro;
            }
        }

        // ---------- Compatibilidad con Seguridad.cs (mismas firmas que antes) ----------
        public static string ProtegerDpapi(string textoPlano)
        {
            return Proteger(textoPlano);
        }

        public static string DesprotegerDpapi(string base64)
        {
            return Desproteger(base64);
        }

        // ---------- Con clave explícita ----------

        public static string Cifrar(string texto, string clave)
        {
            if (texto == null) throw new ArgumentNullException("texto");
            if (string.IsNullOrEmpty(clave)) throw new ArgumentNullException("clave");

            byte[] sal = Aleatorio(TamSal);
            byte[] iv = Aleatorio(TamIv);
            byte[] claveAes, claveMac;
            Derivar(clave, sal, out claveAes, out claveMac);

            byte[] ct;
            using (var aes = Aes.Create())
            {
                aes.Key = claveAes;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                byte[] datos = Encoding.UTF8.GetBytes(texto);
                using (var enc = aes.CreateEncryptor())
                    ct = enc.TransformFinalBlock(datos, 0, datos.Length);
            }

            byte[] msg = new byte[1 + TamSal + TamIv + ct.Length];
            msg[0] = Version;
            Buffer.BlockCopy(sal, 0, msg, 1, TamSal);
            Buffer.BlockCopy(iv, 0, msg, 1 + TamSal, TamIv);
            Buffer.BlockCopy(ct, 0, msg, 1 + TamSal + TamIv, ct.Length);

            byte[] mac;
            using (var h = new HMACSHA256(claveMac))
                mac = h.ComputeHash(msg);

            byte[] todo = new byte[msg.Length + TamMac];
            Buffer.BlockCopy(msg, 0, todo, 0, msg.Length);
            Buffer.BlockCopy(mac, 0, todo, msg.Length, TamMac);
            return Convert.ToBase64String(todo);
        }

        public static string Descifrar(string base64, string clave)
        {
            if (string.IsNullOrEmpty(base64)) throw new ArgumentNullException("base64");
            if (string.IsNullOrEmpty(clave)) throw new ArgumentNullException("clave");

            byte[] todo = Convert.FromBase64String(base64.Trim());
            if (todo.Length < 1 + TamSal + TamIv + 16 + TamMac || todo[0] != Version)
                throw new CryptographicException("El texto no tiene un formato cifrado válido.");

            int finMsg = todo.Length - TamMac;
            byte[] sal = new byte[TamSal];
            byte[] iv = new byte[TamIv];
            Buffer.BlockCopy(todo, 1, sal, 0, TamSal);
            Buffer.BlockCopy(todo, 1 + TamSal, iv, 0, TamIv);

            byte[] claveAes, claveMac;
            Derivar(clave, sal, out claveAes, out claveMac);

            byte[] msg = new byte[finMsg];
            byte[] mac = new byte[TamMac];
            Buffer.BlockCopy(todo, 0, msg, 0, finMsg);
            Buffer.BlockCopy(todo, finMsg, mac, 0, TamMac);

            byte[] esperado;
            using (var h = new HMACSHA256(claveMac))
                esperado = h.ComputeHash(msg);
            if (!IgualesTiempoConstante(mac, esperado))
                throw new CryptographicException("Clave incorrecta o datos alterados.");

            int inicioCt = 1 + TamSal + TamIv;
            byte[] ct = new byte[finMsg - inicioCt];
            Buffer.BlockCopy(todo, inicioCt, ct, 0, ct.Length);

            using (var aes = Aes.Create())
            {
                aes.Key = claveAes;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                using (var dec = aes.CreateDecryptor())
                    return Encoding.UTF8.GetString(dec.TransformFinalBlock(ct, 0, ct.Length));
            }
        }

        // ---------- Internos ----------

        private static void Derivar(string clave, byte[] sal, out byte[] claveAes, out byte[] claveMac)
        {
            byte[] bits = Pbkdf2Sha256(Encoding.UTF8.GetBytes(clave), sal, Iteraciones, 64);
            claveAes = new byte[32];
            claveMac = new byte[32];
            Buffer.BlockCopy(bits, 0, claveAes, 0, 32);
            Buffer.BlockCopy(bits, 32, claveMac, 0, 32);
        }

        // PBKDF2 con HMAC-SHA256 (Rfc2898DeriveBytes de .NET Framework solo usa SHA1
        // en versiones anteriores a 4.7.2, por eso se implementa aquí).
        private static byte[] Pbkdf2Sha256(byte[] password, byte[] salt, int iteraciones, int largo)
        {
            byte[] resultado = new byte[largo];
            using (var hmac = new HMACSHA256(password))
            {
                int bloques = (largo + 31) / 32;
                for (int b = 1; b <= bloques; b++)
                {
                    byte[] saltBloque = new byte[salt.Length + 4];
                    Buffer.BlockCopy(salt, 0, saltBloque, 0, salt.Length);
                    saltBloque[salt.Length] = (byte)(b >> 24);
                    saltBloque[salt.Length + 1] = (byte)(b >> 16);
                    saltBloque[salt.Length + 2] = (byte)(b >> 8);
                    saltBloque[salt.Length + 3] = (byte)b;

                    byte[] u = hmac.ComputeHash(saltBloque);
                    byte[] t = (byte[])u.Clone();
                    for (int i = 1; i < iteraciones; i++)
                    {
                        u = hmac.ComputeHash(u);
                        for (int j = 0; j < t.Length; j++) t[j] ^= u[j];
                    }

                    int copiar = Math.Min(32, largo - (b - 1) * 32);
                    Buffer.BlockCopy(t, 0, resultado, (b - 1) * 32, copiar);
                }
            }
            return resultado;
        }

        private static byte[] Aleatorio(int n)
        {
            byte[] r = new byte[n];
            using (var rng = new RNGCryptoServiceProvider())
                rng.GetBytes(r);
            return r;
        }

        private static bool IgualesTiempoConstante(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++) diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}