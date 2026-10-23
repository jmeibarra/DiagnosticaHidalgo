using System;

namespace DiagnosticaHidalgo.Clases.Modelos
{
    public class LabResumido
    {
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Costo { get; set; }

        public string Familia { get; set; }
        public string Reservacion { get; set; }
    }
}
