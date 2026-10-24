using DiagnosticaHidalgo.Clases.Modelos;
using DiagnosticaHidalgo.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DiagnosticaHidalgo.Clases
{
    public class Utilerias
    {
        /// <summary>
        /// Genera el hash SHA256 de una cadena especificada.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetSha256(string str)
        {
            using (SHA256 oSHA256 = SHA256.Create())
            {
                byte[] bytes = oSHA256.ComputeHash(Encoding.UTF8.GetBytes(str));

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i <= bytes.Length - 1; i++)
                    sb.Append(bytes[i].ToString("x2"));

                return sb.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Muestra mensaje en pantalla.
        /// </summary>
        public static void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "¡Notificación!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Muestra mensaje de confirmación en pantalla.
        /// </summary>
        public static bool MostrarMensajeConfirmacion(string mensaje)
        {
            if (MessageBox.Show(mensaje, "¡Confirmación!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                return true;
            return false;
        }

        /// <summary>
        /// Limpiar textbox y RichTextBox de un grupo de controles.
        /// </summary>
        public static void LimpiarControles(GroupBox gb)
        {
            foreach (Control c in gb.Controls)
            {
                if (c is TextBox || c is RichTextBox)
                    c.Text = "";
                else if (c is GroupBox)
                {
                    GroupBox gbControl = (GroupBox)c;
                    LimpiarControles(gbControl);
                }
            }
        }

        /// <summary>
        /// Obtiene la cadena que se refiere al tipo de pago para la base de datos
        /// </summary>
        /// <param name="tipoPago"></param>
        /// <returns></returns>
        public static string ObtenerTipoPagoParaBD(string tipoPago)
        {
            string res = "";
            switch (tipoPago)
            {
                case "Efectivo":
                    res = C.PAGO_EFECTIVO;
                    break;
                case "Tarjeta":
                    res = C.PAGO_TARJETA;
                    break;
            }

            return res;

        }

        public static void RegistrarLogError(Exception ex, string clase, string metodo, string usuario)
        {
            StackTrace st = new StackTrace(ex, true);
            Error logError = new Error(ex, clase, metodo, st.FrameCount > 0 ? st.GetFrame(st.FrameCount - 1).GetFileLineNumber() : 0);
            logError.Usuario = usuario;

            ManejadorError mError = new ManejadorError();
            mError.ErrHandler(logError);
        }

        public static string ObtenerConDosDecimales(double number)
        {
            return string.Format("{0:N2}", number);
        }

        /// <summary>
        /// Método para obtener un arreglo de bytes que es la imagen de cancelar
        /// </summary>
        /// <returns></returns>
        public static byte[] ObtenerImgCancelar()
        {
            return new System.Drawing.ImageConverter().ConvertTo(Resources.cancelar, typeof(byte[])) as byte[];
        }

        /// <summary>
        /// Método para mandar a imprimir un archivo(que es un arreglo de bytes).
        /// Es necesario tener instalado Adobe Acrobat
        /// </summary>
        /// <param name="documento"></param>
        /// <param name="pathFile"></param>
        public static bool Imprimir(byte[] documento, string pathFile)
        {
            try
            {
                if (documento != null)
                {
                    File.WriteAllBytes(pathFile, documento);

                    Process.Start(pathFile);

                    Thread.Sleep(4000);
                }
            }
            catch (Exception ex)
            {
                RegistrarLogError(ex, "Utilerias", "Imprimir", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Método para verificar si el precio debe ser de emergencia
        /// </summary>
        /// <returns></returns>
        public static bool EsPrecioEmergencia()
        {
            if ((DateTime.Now.DayOfWeek == DayOfWeek.Saturday && DateTime.Now.Hour >= 14) || DateTime.Now.Hour >= 20)
                return true;
            return false;
        }

        /// <summary>
        /// Función que encripta una valor tipo string
        /// </summary>
        /// <param name="textoPlano"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        public static string Encriptar(string textoPlano, string clave)
        {
            // Convertimos la clave a bytes (32 bytes = 256 bits)
            byte[] keyBytes = Encoding.UTF8.GetBytes(clave.PadRight(32).Substring(0, 32));
            byte[] ivBytes = Encoding.UTF8.GetBytes(clave.PadRight(16).Substring(0, 16));

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = ivBytes;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(textoPlano);
                    sw.Close();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// Desencriptar una cadena, para encriptar se usa la clase Utilerias.Encriptar
        /// </summary>
        /// <param name="textoEncriptado"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        public static string Desencriptar(string textoEncriptado, string clave)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(clave.PadRight(32).Substring(0, 32));
            byte[] ivBytes = Encoding.UTF8.GetBytes(clave.PadRight(16).Substring(0, 16));
            byte[] buffer = Convert.FromBase64String(textoEncriptado);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = ivBytes;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream ms = new MemoryStream(buffer))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public static bool EnviarCorteTurnoPorCorreo(string correodestino, string fechacorte, string rutapdf, string rutaexcel)
        {
            bool res = false;
            try
            {
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(ParametrosSistema.CorreoClinica);
                correo.To.Add(correodestino);
                correo.Subject = "Corte Turno Diagnostica Ajalpan " + fechacorte;
                correo.Body = "Correo enviado desde Diagnostica Ajalpan, que indica se realizó un corte de turno, los detalles se encuentran en los archivos adjuntos.";

                // Adjuntar PDF
                Attachment adjuntoPdf = new Attachment(rutapdf);
                correo.Attachments.Add(adjuntoPdf);

                // Adjuntar Excel
                Attachment adjuntoExcel = new Attachment(rutaexcel);
                correo.Attachments.Add(adjuntoExcel);

                // Configuración del cliente SMTP
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); // puerto TLS típico
                smtp.Credentials = new NetworkCredential(ParametrosSistema.CorreoClinica, ParametrosSistema.TokenCorreoClinica);
                smtp.EnableSsl = true; // activa conexión segura

                smtp.Send(correo);
                Console.WriteLine("Correo enviado correctamente.");
                res = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error general: " + ex.Message);
            }
            return res;
        }
    }
}
