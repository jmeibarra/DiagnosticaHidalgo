using System.IO;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing;

namespace DiagnosticaHidalgo.Clases
{
    public class C
    {
        #region Enum
        public enum TipoConsulta
        {
            Laboratorios
        }
        #endregion

        #region rutas
        /// <summary>
        /// Obtiene el directorio principal del sistema.
        /// </summary>
        public static string AppPath = Path.GetDirectoryName(Application.ExecutablePath) + "//";

        public static string PathConfigFile = AppPath + "ConfigFile_.xml";
        public static string PathPlantillasPDF = AppPath + "//PlantillasPDF//";
        public static string PathTEMP = AppPath + "//temp//";
        public static string PathImages = AppPath + "//imagenes//";
        public static string PathPlantillaCargarNotasTarjeta = AppPath + "//PlantillasPDF//PlantillaCargarNotas.xlsx";
        #endregion

        #region EXP REGULARES
        public static string EREG_NOMBRE = "^([A-Z]|[a-z]| |Ñ|ñ|á|é|í|ó|ú|Á|É|Í|Ó|Ú|ü|Ü){1,300}$";
        public static string EREG_DECIMAL = "^[0-9]{1,18}(.[0-9]{1,6})?$";
        public static string EREG_NUMERO_ENTERO = "^([\\d]{1,100})$";

        #endregion
        public static string SinSeleccion = "Sin selección";
        public static string AGREGAR_CLIENTE = "Agregar cliente";
        public static string AGREGAR = "AGREGAR";
        public static string EDITAR = "EDITAR";
        public static string AGREGAR_DOCTOR = "Agregar doctor";
        public static string COLUMNA_MODIFICAR = "Modificar";
        public static string COLUMNA_ELIMINAR = "Eliminar";
        public static string COLUMNA_VERNOTAS = "Ver notas";
        public static string COLUMNA_AGREGARPAGO = "Agregar pago";
        public static string COLUMNA_CANCELAR = "Cancelar";
        public static string COLUMNA_VIGENTE = "Vigente";
        public static string COLUMNA_SELECCION = "Seleccionar";
        public static string COLUMNA_IMPRIMIR = "Imprimir";
        public static string COLUMNA_ESTUDIOS = "Estudios";
        public static string COLUMNA_CREAR_NOTA = "Nota";
        public static string COLUMNA_HUELLA = "Huella Digital";
        public static Color COLOR_SISTEMA = Color.FromArgb(24, 69, 91);
        public static Color COLOR_SISTEMA_CLARO = Color.FromArgb(32, 155, 164);
        public static string EMPRESA_ANALIZER = "analizer";
        public static string EMPRESA_DH = "DIAGNOSTICA HIDALGO";
        public static string HOMBRE = "H";
        public static string MUJER = "M";

        #region Constraints
        public static string FK_VENTA_IDUSUARIO = "fk_Venta_IdUsuario";
        public static string FK_VENTA_IDCLIENTE = "fk_Venta_IdCliente";
        #endregion

        #region Pagos
        public const string PAGO_EFECTIVO = "EFECTIVO";
        public const string PAGO_TARJETA = "TARJETA";
        //public const string PAGO_TDD = "TARJETA_CREDITO";
        //public const string PAGO_TRANSFERENCIA = "TRANSFERENCIA";
        #endregion
        public static string NOMBRE_IMPRESORA = ConfigurationManager.AppSettings["printerName"].ToString();
        public static string SUCURSAL = ConfigurationManager.AppSettings["Sucursal"].ToString();
    }
}
