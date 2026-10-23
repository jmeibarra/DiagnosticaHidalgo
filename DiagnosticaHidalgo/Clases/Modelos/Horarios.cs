using DiagnosticaHidalgo.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DiagnosticaHidalgo.Clases.Modelos
{
    public class Horarios
    {
        public const string SIN_HORARIOS = "Sin horarios disponibles";
        public const string CLAVES_COVID = "8049 8060 8047";

        /// <summary>
        /// Método para saber si un laboratorio debe llevar horario de reservación
        /// </summary>
        public static bool tienehorario(string familia)
        {
            if (familia == TOMOGRAFIA || familia == RAYOSXSIMPLE || familia == RAYOSXESPECIAL || familia == ULTRASONIDO ||
                familia == ELECTROCARDIOGRAMA || familia == COVID)
                return true;
            return false;
        }

        private const string TOMOGRAFIA = "TOMOGRAFIA";
        private const string RAYOSXSIMPLE = "RAYOS X SIMPLE";
        private const string RAYOSXESPECIAL = "RAYOS X ESPECIAL";
        private const string ULTRASONIDO = "ULTRASONIDO";
        private const string ELECTROCARDIOGRAMA = "ELECTROCARDIOGRAMA";
        private const string COVID = "ANALISIS SARS";

        /// <summary>
        /// Horarios para tomografía
        /// </summary>
        public static string[] HORARIOS_TOMOGRAFIA_RAYOSESPECIALES = {"08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30","15:00", "15:30",
        "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00", "20:30"};
        /// <summary>
        /// rayos x simple
        /// </summary>
        public static string[] HORARIOS_RAYOSX_SIMPLE = {"08:00", "08:10", "08:20", "08:30", "08:40", "08:50", "09:00", "09:10", "09:20", "09:30", "09:40", "09:50",
        "10:00", "10:10", "10:20", "10:30", "10:40", "10:50", "11:00", "11:10", "11:20", "11:30", "11:40", "11:50", "12:00", "12:10", "12:20", "12:30", "12:40", "12:50",
        "13:00", "13:10", "13:20", "13:30", "13:40", "13:50", "14:00", "14:10", "14:20", "14:30", "14:40", "14:50", "15:00", "15:10", "15:20", "15:30", "15:40", "15:50",
        "16:00", "16:10", "16:20", "16:30", "16:40", "16:50", "17:00", "17:10", "17:20", "17:30", "17:40", "17:50", "18:00", "18:10", "18:20", "18:30", "18:40", "18:50",
        "19:00", "19:10", "19:20", "19:30", "19:40", "19:50", "20:00", "20:10", "20:20", "20:30", "20:40", "20:50"};

        /// <summary>
        /// Horarios para ultrasonido
        /// </summary>
        public static string[] HORARIOS_ULTRASONIDO = {"09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30",
        "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00", "20:30", "21:00"};
        /// <summary>
        /// Horarios para estudios de electrocardiograma y covid
        /// </summary>
        public static string[] HORARIOS_ELECTROCARDIOGRAMA_COVID = {"07:30", "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00",
        "16:00", "16:30", "17:00", "17:30"};

        /*Horarios de la nota actual*/
        public List<Horario> lHorarioTomografiaRayosEspec;
        public List<Horario> lHorarioRayosXsimple;
        public List<Horario> lHorarioUltrasonido;
        public List<Horario> lHorarioElectrocardiogramaCovid;

        public Horarios()
        {
            lHorarioTomografiaRayosEspec = new List<Horario>();
            lHorarioRayosXsimple = new List<Horario>();
            lHorarioUltrasonido = new List<Horario>();
            lHorarioElectrocardiogramaCovid = new List<Horario>();
        }

        public List<string> ObtenerHorariosDisponibles(string tipofamilia, string clave, DateTime fechaseleccionada,string cadconn)
        {
            List<string> lHorasDisponibles = new List<string>();
            List<Horario> horariosocupados = new List<Horario>();
            if (fechaseleccionada.DayOfWeek == DayOfWeek.Sunday)
            {
                lHorasDisponibles.Add(SIN_HORARIOS);
                return lHorasDisponibles;
            }

            switch (tipofamilia)
            {
                case TOMOGRAFIA:
                case RAYOSXESPECIAL:
                    lHorasDisponibles = HORARIOS_TOMOGRAFIA_RAYOSESPECIALES.ToList();
                    horariosocupados = lHorarioTomografiaRayosEspec;
                    break;
                case RAYOSXSIMPLE:
                    lHorasDisponibles = HORARIOS_RAYOSX_SIMPLE.ToList();
                    horariosocupados = lHorarioRayosXsimple;
                    break;
                case ULTRASONIDO:
                    lHorasDisponibles = HORARIOS_ULTRASONIDO.ToList();
                    horariosocupados = lHorarioUltrasonido;
                    break;
                case ELECTROCARDIOGRAMA:
                case COVID:
                    lHorasDisponibles = HORARIOS_ELECTROCARDIOGRAMA_COVID.ToList();
                    horariosocupados = lHorarioElectrocardiogramaCovid;
                    break;               
            }

            //Si es sábado quitar después de las tres
            if (fechaseleccionada.DayOfWeek == DayOfWeek.Saturday)
            {
                //Eliminar los horarios que ya pasaron
                lHorasDisponibles.RemoveAll(delegate (string hora)
                {
                    string[] arrhora = hora.Split(':');
                    int h = Convert.ToInt32(arrhora[0]);
                    if (h > 14)
                        return true;
                    return false;
                });
            }

            bool esHoy = DateTime.Now.Year == fechaseleccionada.Year && DateTime.Now.Month == fechaseleccionada.Month && DateTime.Now.Day == fechaseleccionada.Day ? true : false;
            //Eliminar los horarios que están antes de la hora actual
            if (esHoy)
            {
                //Eliminar los horarios que ya pasaron
                lHorasDisponibles.RemoveAll(delegate (string hora)
                {
                    string[] arrhora = hora.Split(':');
                    int h = Convert.ToInt32(arrhora[0]);
                    int m = Convert.ToInt32(arrhora[1]);
                    if (h < fechaseleccionada.Hour || (h == fechaseleccionada.Hour && m < fechaseleccionada.Minute))
                        return true;
                    return false;
                });
            }

            //Eliminar horarios registrados en base de datos
            LabPorVentaNeg labPorVentaNeg = new LabPorVentaNeg(cadconn);
            DataTable tabla = labPorVentaNeg.ObtenerReservacionesPorFamilia(tipofamilia, fechaseleccionada);
            if (tabla != null && tabla.Rows != null)
            {
                DataRowCollection renglones = tabla.Rows;
                for (int i = 0; i < renglones.Count; i++)
                {
                    lHorasDisponibles.Remove(renglones[i][0].ToString());
                }
            }

            DataTable dt = null;            
            switch (tipofamilia)
            {
                case TOMOGRAFIA:
                    dt = labPorVentaNeg.ObtenerReservacionesPorFamilia(RAYOSXESPECIAL, fechaseleccionada);                    
                    break;
                case RAYOSXESPECIAL:
                    dt= labPorVentaNeg.ObtenerReservacionesPorFamilia(TOMOGRAFIA, fechaseleccionada);
                    break;
                case ELECTROCARDIOGRAMA:
                    dt= labPorVentaNeg.ObtenerReservacionesPorFamilia(COVID, fechaseleccionada);
                    break;
                case COVID:
                    dt= labPorVentaNeg.ObtenerReservacionesPorFamilia(ELECTROCARDIOGRAMA, fechaseleccionada);
                    break;
            }

            if (dt != null && dt.Rows != null)
            {
                DataRowCollection renglonesempalmados = dt.Rows;
                for (int i = 0; i < renglonesempalmados.Count; i++)
                {
                    lHorasDisponibles.Remove(renglonesempalmados[i][0].ToString());
                }
            }

            if (lHorasDisponibles.Count == 0)
                lHorasDisponibles.Add(SIN_HORARIOS);
            else
            {
                if (horariosocupados.Count > 0)
                {
                    //Eliminar los horarios que ya están en la nota
                    foreach (Horario item in horariosocupados)
                    {
                        if(fechaseleccionada.ToString("yyyy-MM-dd") == item.fecha.ToString("yyyy-MM-dd"))
                            lHorasDisponibles.Remove(item.horario);
                    }
                    if (lHorasDisponibles.Count == 0)
                        lHorasDisponibles.Add(SIN_HORARIOS);
                }
            }
            return lHorasDisponibles;
        }

        public void AgregarReservacion(string horario, DateTime fecha, string familia)
        {
            Horario oHorario = new Horario()
            {
                horario = horario,
                fecha = fecha,
                familia = familia
            };

            switch (familia)
            {
                case TOMOGRAFIA:
                case RAYOSXESPECIAL:
                    lHorarioTomografiaRayosEspec.Add(oHorario);
                    break;
                case ELECTROCARDIOGRAMA:
                case COVID:
                    lHorarioElectrocardiogramaCovid.Add(oHorario);
                    break;
                case RAYOSXSIMPLE:
                    lHorarioRayosXsimple.Add(oHorario);
                    break;
                case ULTRASONIDO:
                    lHorarioUltrasonido.Add(oHorario);
                    break;
            }
        }
    }
}
public class Horario
{
    public DateTime fecha { get; set; }
    public string horario { get; set; }
    public string familia { get; set; }
}