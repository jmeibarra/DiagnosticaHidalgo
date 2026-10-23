using DiagnosticaHidalgo.Clases;
using System;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;

namespace DiagnosticaHidalgo.Negocio
{
    public static class ParametroGen
    {

        //string _cadenaconn;

        //#region Consructor
        //public ParametroGenNeg()
        //{
        //    this._cadenaconn = C.CONNECTION;
        //}

        //#endregion

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
}
