using System;

namespace DiagnosticaHidalgo.Clases.Modelos
{
    public class Error
    {
        public DateTime Fecha { get; set; }
        public string MensajeError { get; set; }
        public string Clase { get; set; }
        public string Metodo { get; set; }
        public int Linea { get; set; }
        public string InnerException { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string TargetSite { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string MensajeAdicional { get; set; }
        public string Usuario { get; set; }

        public Error(Exception ex, string clase, string metodo, int linea)
        {
            this.Fecha = DateTime.Now;
            this.MensajeError = ex.Message;
            try
            {
                this.Clase = clase;
                this.Metodo = metodo;
                this.Linea = linea;
                if (ex.InnerException != null)
                {
                    this.InnerException = ex.InnerException.ToString();
                    this.InnerExceptionMessage = ex.InnerException.Message;

                }
                this.TargetSite = this.TargetSite != null ? ex.TargetSite.Name : "";
                this.StackTrace = this.StackTrace != null ? ex.StackTrace : "";
                this.Source = this.Source != null ? ex.Source : "";
                this.MensajeAdicional = ex.Message;
                //this.IP = LogHandler.ObtenerIP();
            }
            catch (Exception e)
            {
                this.Clase = "";
                this.Metodo = "";
                this.Linea = 0;
            }

        }
    }
}
