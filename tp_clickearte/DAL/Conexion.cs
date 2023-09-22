using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Reflection;
using System.ComponentModel;

namespace DAL
{
    public class Conexion
    {
            private string connectionString { get; set; }

            private static Conexion Instancia;

            private Conexion()
            {
                this.connectionString = string.Format("Server={0};Database={1};User Id={2};Password={3};", Environment.MachineName, "clickearte_db", "Usuario", "Contraseña");
                //this.connectionString = string.Format("Server={0};Database={1}; Trusted_Connection=True;", Environment.MachineName, "clickearte_db"); ;
            }

            public static Conexion ObtenerInstacia()
            {
                if (Instancia == null)
                {
                    Instancia = new Conexion();
                }

                return Instancia;
            }


            public int Escribir(string query, List<SqlParameter> ListaParametros)
            {
                int valorRetorno = 0;
                using (SqlConnection conexion = new SqlConnection(this.connectionString))
                {
                    SqlCommand comando = new SqlCommand();
                    comando.Connection = conexion;
                    comando.CommandText = query;
                    comando.CommandType = CommandType.Text;
                    if (ListaParametros != null || ListaParametros.Count > 0)
                    {
                        foreach (SqlParameter item in ListaParametros)
                        {
                            comando.Parameters.Add(item);
                        }
                    }

                    conexion.Open();
                    valorRetorno = comando.ExecuteNonQuery();
                }

                return valorRetorno; //0: Error | >1: Correcto.
            }


            public DataTable Leer(string query, List<SqlParameter> ListaParametros = null)
            {
                DataTable tabla = new DataTable();
                using (SqlConnection conexion = new SqlConnection(this.connectionString))
                {
                    SqlCommand comando = new SqlCommand();
                    comando.Connection = conexion;
                    comando.CommandText = query;
                    comando.CommandType = CommandType.Text;
                    if (ListaParametros != null || ListaParametros?.Count > 0)
                    {
                        foreach (SqlParameter item in ListaParametros)
                        {
                            comando.Parameters.Add(item);
                        }
                    }

                    conexion.Open();
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    tabla.Clear();
                    adaptador.Fill(tabla);
                }

                return tabla;

            }

    }
}
