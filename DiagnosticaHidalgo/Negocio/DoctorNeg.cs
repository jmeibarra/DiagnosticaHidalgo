using DiagnosticaHidalgo.Clases;
using DiagnosticaHidalgo.Clases.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace DiagnosticaHidalgo.Negocio
{
    public class DoctorNeg
    {
        string _cadenaconn;
        DataClassDataContext context;

        #region Consructor
        public DoctorNeg(string cadenaconn)
        {
            this._cadenaconn = cadenaconn;
            this.context = new DataClassDataContext(_cadenaconn);
        }
        #endregion
        #region Agregar
        /// <summary>
        /// Insertar o actualizar un registro en la tabla Doctor
        /// </summary>
        public int Upsert(Doctor doctor)
        {
            int iddoctor = -1;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Doctor));
                StringWriter doctorxml = new StringWriter();
                xmlSerializer.Serialize(doctorxml, doctor);

                using (SqlConnection conn = new SqlConnection(_cadenaconn))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("doctor_upsert", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@DOCTOR_XML", SqlDbType.Xml);
                    command.Parameters["@DOCTOR_XML"].Value = doctorxml.ToString();
                    string mensaje = "";
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            mensaje = reader["mensaje"].ToString();
                            iddoctor = Convert.ToInt32(reader["iddoctor"].ToString());
                        }
                    }

                    if (iddoctor == -1)
                    {
                        Utilerias.MostrarMensaje(mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                Utilerias.MostrarMensaje(ex.Message);
                iddoctor = -1;
            }

            return iddoctor;
        }
        #endregion
        #region Eliminar
        /// <summary>
        /// Elimina un doctor.
        /// </summary>
        /// <returns></returns>
        public void EliminarPorId(int Id)
        {
            Doctor doctor = context.Doctor.FirstOrDefault(d => d.IdDoctor == Id);
            context.Doctor.DeleteOnSubmit(doctor);
            context.SubmitChanges();
        }
        #endregion
        #region Consultar registros
        /// <summary>
        /// Metodo para obtener un doctor por IdDoctor.
        /// </summary>
        /// <returns></returns>
        public Doctor ObtenerPorIdDoctor(int IdDoctor)
        {
            return context.Doctor.Where(c => c.IdDoctor == IdDoctor).FirstOrDefault();
        }

        /// <summary>
        /// Metodo para obtener el doctor A QUIEN CORRESPONDA.
        /// </summary>
        /// <returns></returns>
        public Doctor ObtenerA_QUIEN_CORRESPONDA()
        {
            return context.Doctor.Where(c => c.Nombre == "A QUIEN CORRESPONDA").FirstOrDefault();
        }

        ///// <summary>
        ///// Metodo para obtener doctores que coincidan con la cadena de busqueda ya sea en la columna Nombre o Apellidos.
        ///// Se agrega como último elemento "Agregar doctor".
        ///// </summary>
        ///// <returns></returns>
        //public object ObtenerPorNombreOApellidos(string cadenaBusqueda)
        //{
        //    var lDoctores = (from c in context.Doctor
        //                     where SqlMethods.Like(c.Nombre, "%" + cadenaBusqueda + "%")
        //                     select new
        //                     {
        //                         IdDoctor = c.IdDoctor,
        //                         Nombre =c.Nombre
        //                     }).ToList();

        //    context.Connection.Close();
        //    lDoctores.Add(new { IdDoctor = -1, Nombre = C.AGREGAR_DOCTOR });

        //    return lDoctores;
        //}

        /// <summary>
        /// Metodo para obtener todos los nombres de doctores para autompletar.
        /// </summary>
        /// <returns></returns>
        public List<ItemAutocompletar> ObtenerTodosAutocompletar()
        {
            List<ItemAutocompletar> lDoctor = (from c in context.Doctor
                                               select new ItemAutocompletar
                                               {
                                                   Id = c.IdDoctor,
                                                   Nombre = c.Nombre
                                               }).ToList();


            return lDoctor;
        }

        /// <summary>
        /// Metodo para obtener todos los nombres de doctores para autompletar.
        /// </summary>
        /// <returns></returns>
        public List<ItemAutocompletar> ObtenerTodosAutocompletar(string nombre)
        {
            List<ItemAutocompletar> lDoctor = (from c in context.Doctor
                                               where SqlMethods.Like(c.Nombre,"%" + nombre + "%")
                                               select new ItemAutocompletar
                                               {
                                                   Id = c.IdDoctor,
                                                   Nombre = c.Nombre
                                               }).ToList();


            return lDoctor;
        }

        /// <summary>
        /// Metodo para obtener doctores que coincidan con la cadena de busqueda en la columna Nombre.
        /// </summary>
        /// <returns></returns>
        public object ObtenerPorNombre(string cadenaBusqueda)
        {
            var lDoctores = (from c in context.Doctor
                             where SqlMethods.Like(c.Nombre, "%" + cadenaBusqueda + "%")
                             select new
                             {
                                 IdDoctor = c.IdDoctor,
                                 Nombre = c.Nombre
                             }).ToList();

            return lDoctores;
        }

        /// <summary>
        /// Metodo para obtener todos los doctores.
        /// </summary>
        /// <returns></returns>
        public object ObtenerTodos()
        {
            var lDoctores = (from c in context.Doctor
                             select new
                             {
                                 IdDoctor = c.IdDoctor,
                                 Nombre = c.Nombre
                             }).ToList();

            context.Connection.Close();

            return lDoctores;
        }

        /// <summary>
        /// Obtener doctores por los filtros nombre, especialidad.
        /// </summary>
        /// <returns>Una lista de clientes.</returns>
        public object ObtenerPorFiltros(string nombre, string especialidad)
        {
            object var = (from d in context.Doctor
                          where SqlMethods.Like(d.Nombre, "%" + nombre + "%")
                          where SqlMethods.Like(d.Especialidad, "%" + especialidad + "%")
                          orderby d.Nombre ascending
                          select new
                          {
                              Id = d.IdDoctor,
                              Nombre = d.Nombre,
                              Direccion = d.Direccion,
                              CP = d.CP,
                              Estado = d.Estado,
                              Telefono = d.Telefono,
                              Especialidad = d.Especialidad,
                              FechaCreacion = d.FechaCreacion,
                              FechaAct = d.FechaAct == null ? "" : d.FechaAct.ToString()
                          }).ToList();
            context.Connection.Close();

            return var;
        }
        #endregion
    }
}