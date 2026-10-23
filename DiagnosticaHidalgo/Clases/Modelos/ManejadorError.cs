using System;
using System.Text;

namespace DiagnosticaHidalgo.Clases.Modelos
{
    class ManejadorError
    {
        public string AppPath = System.AppDomain.CurrentDomain.BaseDirectory;

        public bool ErrHandler(Error exc)
        {
            try
            {
                CrearCarpeta();
                StringBuilder cadena = new StringBuilder();
                cadena.Append("<Error");
                cadena.Append(" usuario=" + exc.Usuario + " ");
                cadena.Append(" fecha= " + exc.Fecha.ToString() + " ");
                cadena.Append(" clase= " + exc.Clase + " ");
                cadena.Append(" metodo= " + exc.Metodo + " ");
                cadena.Append(" linea= " + exc.Linea + " ");
                cadena.Append(" mensajeError= " + exc.MensajeError + " ");
                cadena.Append(" innerException= " + exc.InnerException + " ");
                cadena.Append(" innerExceptionMessage= " + exc.InnerExceptionMessage + " ");
                cadena.Append(" stackTrace= " + exc.StackTrace + " ");
                cadena.Append(" source= " + exc.Source + " ");
                cadena.Append(" mensajeAdicional= " + exc.MensajeAdicional + Environment.NewLine);                
                System.IO.File.AppendAllText(String.Format("{0}\\Logs\\Log_error_{1:yyyy_MM_dd}.xml", AppPath, DateTime.Now), cadena.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public void CrearCarpeta()
        {
            try
            {
                if (!System.IO.Directory.Exists(String.Format("{0}\\Logs", AppPath)))
                    System.IO.Directory.CreateDirectory(String.Format("{0}\\Logs", AppPath));
            }
            catch (Exception ex){ }
        }
    }
}
