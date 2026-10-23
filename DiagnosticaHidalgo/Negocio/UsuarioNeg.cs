using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.SqlClient;
using DiagnosticaHidalgo.Clases;
using System.Data;
using System.Data.SqlClient;

namespace DiagnosticaHidalgo.Negocio
{
    public class UsuarioNeg
    {
        string _cadenaconn;
        DataClassDataContext context;

        #region Consructor
        public UsuarioNeg(string cadenaconn)
        {
            this._cadenaconn = cadenaconn;
            this.context = new DataClassDataContext(_cadenaconn);
        }
        #endregion

        /// <summary>
        /// Valida para su ingreso a un usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Usuario AutenticarUsuario(string clave, string password)
        {
            password = Utilerias.GetSha256(password);
            return context.Usuario.Where(u => u.Clave == clave && u.Pass == password).FirstOrDefault();
        }

        #region Agregar
        /// <summary>
        /// Inserta un nuevo registro en la tabla Usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public void Agregar(Usuario usuario)
        {
            //Sólo existe un usuario administrador
            usuario.Administrador = false;
            usuario.FechaCreacion = DateTime.Now;
            context.Usuario.InsertOnSubmit(usuario);
            context.SubmitChanges();
        }
        #endregion
        #region Eliminar
        /// <summary>
        /// Eliina un usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public int Eliminar(int IdUsuario, ref string msjError)
        {
            int res = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_cadenaconn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Usuario WHERE IdUsuario=@IdUsuario", conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("IdUsuario", IdUsuario);

                    res = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                context.Transaction.Rollback();
                msjError = ex.Message;
            }

            return res;

        }
        #endregion
        #region Actualizar
        /// <summary>
        /// Se modifican los datos del usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public void Actualizar(Usuario usuario)
        {
            //Sólo existe un usuario administrador
            Usuario usAnt = context.Usuario.FirstOrDefault(u => u.IdUsuario == usuario.IdUsuario);
            usAnt.Nombre = usuario.Nombre;
            usAnt.Email = usuario.Email;
            usAnt.Telefono = usuario.Telefono;
            usAnt.Clave = usuario.Clave;
            if (!string.IsNullOrEmpty(usuario.Pass))
                usAnt.Pass = Utilerias.GetSha256(usuario.Pass);
            usuario.FechaAct = DateTime.Now;

            context.SubmitChanges();
        }
        #endregion
        #region Consultar Usuarios
        /// <summary>
        /// Obtener el identificador del usaurio, por su clave.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Usuario ObtenerPorIdUsuario(int IdUsuario)
        {
            return context.Usuario.Where(u => u.IdUsuario == IdUsuario).FirstOrDefault();
        }
        /// <summary>
        /// Obtener el identificador del usaurio, por su clave.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public int ObtenerIdUsuarioPorClave(string clave)
        {
            int IdUsuario = -1;
            Usuario oUsuario = context.Usuario.Where(u => u.Clave == clave).FirstOrDefault();
            if (oUsuario != null)
                IdUsuario = oUsuario.IdUsuario;

            return IdUsuario;
        }

        /// <summary>
        /// Busca un usaurio tipo administrador por su password
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ExistePassAdministrador(string password)
        {
            password = Utilerias.GetSha256(password);
            return context.Usuario.Where(u => u.Pass == password && u.Administrador == true).FirstOrDefault() == null ? false : true;
        }

        /// <summary>
        /// Obtener el identificador del usaurio, por su clave.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Usuario ObtenerUsuarioPorClaveONombre(string clave, string nombre)
        {
            return context.Usuario.Where(u => u.Clave == clave || u.Nombre == nombre).FirstOrDefault();
        }

        /// <summary>
        /// Obtener usuario por clave o nombre que no tenga el id pasado por parámetro.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Usuario ObtenerUsuarioPorClaveONombre(string clave, string nombre, int IdUsuario)
        {
            return context.Usuario.Where(u => (u.Clave == clave || u.Nombre == nombre) && u.IdUsuario != IdUsuario).FirstOrDefault();
        }

        /// <summary>
        /// Obtener usuarios por los filtros nombre o clave.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>Una lista de usuarios.</returns>
        public object ObtenerPorFiltros(string nombre, string clave)
        {
            object var = (from u in context.Usuario
                          where SqlMethods.Like(u.Nombre, "%" + nombre + "%") &&
                                SqlMethods.Like(u.Clave, "%" + clave + "%")
                          orderby u.Nombre ascending
                          select new
                          {
                              Id = u.IdUsuario,
                              Nombre = u.Nombre,
                              Clave = u.Clave,
                              Email = u.Email,
                              Telefono = u.Telefono,
                              Administrador = u.Administrador == true ? "Si" : "No",
                              FechaCreacion = u.FechaCreacion,
                              FechaAct = u.FechaAct == null ? "" : u.FechaAct.ToString()
                          }).ToList();
            context.Connection.Close();

            return var;
        }

        /// <summary>
        /// Obtener todos los usuarios del sistema
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public object ObtenerTodos()
        {
            object usuarios = (from u in context.Usuario
                               orderby u.Nombre descending
                               select new
                               {
                                   Id = u.IdUsuario,
                                   Nombre = u.Nombre,
                                   Clave = u.Clave,
                                   Email = u.Email,
                                   Telefono = u.Telefono,
                                   Administrador = u.Administrador == true ? "Si" : "No",
                                   FechaCreacion = u.FechaCreacion,
                                   FechaAct = u.FechaAct == null ? "" : u.FechaAct.ToString()
                               }).ToList();
            context.Connection.Close();
            return usuarios;
        }
        #endregion
    }
}
