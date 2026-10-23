using DiagnosticaHidalgo.Forms;
using System;
using System.Windows.Forms;

namespace DiagnosticaHidalgo
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new fPrincipal());
            //Application.Run(new frmVenta());
            Application.Run(new frmMain());
            //Application.Run(new frmLogin(false));
        }
    }
}
