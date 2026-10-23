using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiagnosticaHidalgo.Clases.Modelos
{
    public class Cotizacion
    {
        //Usuario
        public int IdUsuario;
        public string ClaveUsuario;
        
        //Cliente
        public int IdCliente;
        public string NombreCliente;
        
        //Doctor
        public int IdDoctor;
        public string NombreDoctor;

        //Laboratorios
        public List<LabPorVenta> laboratorios = new List<LabPorVenta>();
        public List<LabResumido> labResumidos = new List<LabResumido>();
        //Datos de Venta
        public int IdVenta;
        public decimal Descuento;
        public decimal SubTotal;
        public decimal Impuestos;
        public decimal Total;
    }
}
