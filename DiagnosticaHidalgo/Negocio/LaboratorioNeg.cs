using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Clases.Modelos;
using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;

namespace DiagnosticaHidalgo.Negocio
{
    public class LaboratorioNeg
    {
        string _cadenaconn;
        DataClassDataContext context;
        #region Consructor
        public LaboratorioNeg(string cadenaconn)
        {
            this._cadenaconn = cadenaconn;
            this.context = new DataClassDataContext(_cadenaconn);
        }
        #endregion
        #region Agregar
        /// <summary>
        /// Inserta un nuevo registro en la tabla Laboratorio
        /// </summary>
        public void Agregar(Laboratorio laboratorio)
        {
            laboratorio.FechaCreacion = DateTime.Now;
            context.Laboratorio.InsertOnSubmit(laboratorio);
            context.SubmitChanges();
        }
        #endregion
        #region Actualizar
        /// <summary>
        /// Actualiza la información de un Laboratorio
        /// </summary>
        public void Actualizar(Laboratorio lab)
        {
            Laboratorio labAnt = context.Laboratorio.FirstOrDefault(c => c.IdLaboratorio == lab.IdLaboratorio);
            labAnt.Clave = lab.Clave;
            if (lab.Descripcion != null)  labAnt.Descripcion = lab.Descripcion;
            labAnt.Familia = lab.Familia;
            labAnt.Linea = lab.Linea;
            labAnt.Formula = lab.Formula;
            if(lab.PrecioVenta != null) labAnt.PrecioVenta = lab.PrecioVenta;
            if (lab.PrecioCosto != null) labAnt.PrecioCosto = lab.PrecioCosto;
            if (lab.PrecioVentaUrgencia != null) labAnt.PrecioVentaUrgencia = lab.PrecioVentaUrgencia;
            if (lab.Existencias != null) labAnt.Existencias = lab.Existencias;
            if (lab.Indicaciones != null) labAnt.Indicaciones = lab.Indicaciones;
            labAnt.FechaAct = DateTime.Now;

            context.SubmitChanges();
        }
        #endregion
        #region Consultar registros
        /// <summary>
        /// Metodo para obtener doctores que coincidan con la cadena de busqueda ya sea en la columna Nombre o Apellidos.
        /// Se agrega como último elemento "Agregar laboratorio".
        /// </summary>
        /// <returns></returns>
        public List<Laboratorio> ObtenerPorFiltros(string clave, string descripcion, string familia, string linea, string formula)
        {
            List <Laboratorio> lista =  (from l in context.Laboratorio
                             where SqlMethods.Like(l.Clave, "%" + clave + "%") &&
                                   SqlMethods.Like(l.Descripcion, "%" +descripcion + "%") &&
                                   SqlMethods.Like(l.Familia, "%"+ familia + "%") &&
                                   SqlMethods.Like(l.Linea, "%"+linea + "%") &&
                                   SqlMethods.Like(l.Formula, "%"+formula + "%") 
                                   orderby l.Descripcion ascending
                             select l).ToList();

            context.Connection.Close();

            return lista;
        }

        /// <summary>
        /// Metodo para obtener todos los laboratorios.
        /// </summary>
        /// <returns></returns>
        public List<Laboratorio> ObtenerTodos()
        {
            return  context.Laboratorio.ToList();


        }

        /// <summary>
        /// Metodo para obtener un laboratorio por clave
        /// </summary>
        /// <returns></returns>
        public Laboratorio ObtenerPorClave(string clave)
        {
            return context.Laboratorio.Where(l => l.Clave == clave).FirstOrDefault();
        }

        /// <summary>
        /// Metodo para obtener todos los Laboratorios para autompletar.
        /// </summary>
        /// <returns></returns>
        public List<ItemAutocompletar> ObtenerTodosAutocompletar()
        {
            List<ItemAutocompletar> lClientes = (from lab in context.Laboratorio
                                                 select new ItemAutocompletar
                                                 {
                                                     Id = lab.IdLaboratorio,
                                                     Nombre = lab.Descripcion
                                                 }).ToList();

            return lClientes;
        }
        #endregion
    }
}
