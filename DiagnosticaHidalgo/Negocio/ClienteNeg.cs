using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Clases.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Xml.Serialization;

namespace DiagnosticaHidalgo.Negocio
{
    public class ClienteNeg
    {
        string _cadenaconn;
        DataClassDataContext context;
        public ClienteNeg(string cadenaconn)
        {
            this._cadenaconn = cadenaconn;
            this.context = new DataClassDataContext(cadenaconn);
        }
        #region Agregar
        /// <summary>
        /// Insertar o actualizar un registro en la tabla Cliente
        /// </summary>
        public int Upsert(Cliente cliente)
        {
            int idcliente = -1;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Cliente));
                StringWriter clientexml = new StringWriter();
                xmlSerializer.Serialize(clientexml, cliente);

                using (SqlConnection conn = new SqlConnection(_cadenaconn))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("cliente_upsert", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@CLIENTE_XML", SqlDbType.Xml);
                    command.Parameters["@CLIENTE_XML"].Value = clientexml.ToString();
                    string mensaje = "";
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            mensaje = reader["mensaje"].ToString();
                            idcliente = Convert.ToInt32(reader["idcliente"].ToString());
                        }
                    }

                    if (idcliente == -1)
                    {
                        Utilerias.MostrarMensaje(mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje(ex.Message);
                idcliente = -1;
            }

            return idcliente;
        }
        #endregion
        #region Eliminar
        /// <summary>
        /// Elimina un cliente.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public void EliminarPorId(int IdCliente)
        {
            Cliente cliente = context.Cliente.FirstOrDefault(u => u.IdCliente == IdCliente);
            context.Cliente.DeleteOnSubmit(cliente);
            context.SubmitChanges();
        }
        #endregion
        #region Consultar registros
        /// <summary>
        /// Metodo para obtener un cliente por IdCliente.
        /// </summary>
        /// <returns></returns>
        public Cliente ObtenerPorIdCliente(int IdCliente)
        {
            return context.Cliente.Where(c => c.IdCliente == IdCliente).FirstOrDefault();
        }

        /// <summary>
        /// Metodo para obtener todos los clientes para autompletar.
        /// </summary>
        /// <returns></returns>
        public List<ItemAutocompletar> ObtenerTodosAutocompletar()
        {
            List<ItemAutocompletar> lClientes = (from c in context.Cliente
                                                 select new ItemAutocompletar
                                                 {
                                                     Id = c.IdCliente,
                                                     Nombre = c.Nombre
                                                 }).ToList();

            return lClientes;
        }

        /// <summary>
        /// Metodo para obtener todos los clientes para autompletar.
        /// </summary>
        /// <returns></returns>
        public List<ItemAutocompletar> ObtenerTodosAutocompletar(string nombre)
        {
            List<ItemAutocompletar> lClientes = (from c in context.Cliente
                                                 where SqlMethods.Like(c.Nombre, "%" + nombre + "%")
                                                 select new ItemAutocompletar
                                                 {
                                                     Id = c.IdCliente,
                                                     Nombre = c.Nombre
                                                 }).ToList();

            return lClientes;
        }

        /// <summary>
        /// Metodo para obtener clientes que coincidan con la cadena de busqueda en la columna Nombre
        /// y que tengan notas pendientes.
        /// </summary>
        /// <returns></returns>
        public object ObtenerPorNombrePendiente(string cadenaBusqueda)
        {
            var lClientes = (from c in context.Cliente
                             join v in context.Venta on c.IdCliente equals v.IdCliente
                             where SqlMethods.Like(c.Nombre, "%" + cadenaBusqueda + "%") &&
                                v.Estatus == "P"
                             select new
                             {
                                 IdCliente = c.IdCliente,
                                 Nombre = c.Nombre
                             }).Distinct().ToList();
            return lClientes;
        }

        /// <summary>
        /// Metodo para obtener todos clientes.
        /// </summary>
        /// <returns></returns>
        public object ObtenerTodos()
        {
            var lClientes = (from c in context.Cliente
                             select new
                             {
                                 IdCliente = c.IdCliente,
                                 Nombre = c.Nombre
                             }).ToList();
            context.Connection.Close();

            return lClientes;
        }

        /// <summary>
        /// Obtener clientes por los filtros nombre.
        /// </summary>
        /// <returns>Una lista de clientes.</returns>
        public object ObtenerPorFiltroNombre(string nombre)
        {
            object var = (from c in context.Cliente
                          where SqlMethods.Like(c.Nombre, "%" + nombre + "%")
                          orderby c.Nombre ascending
                          select new
                          {
                              Id = c.IdCliente,
                              Nombre = c.Nombre,
                              Email = c.Email,
                              Telefono = c.Telefono,
                              Observaciones = c.Observaciones,
                              FechaCreacion = c.FechaCreacion,
                              FechaAct = c.FechaAct == null ? "" : c.FechaAct.ToString()
                          }).ToList();
            context.Connection.Close();

            return var;
        }

        public void ObtenerPorNombres_()
        {

            context.Cliente.Where(c => c.IdCliente == 1).ToList();
        }
        #endregion
    }
}
