using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAO_Usuario
    {
        public bool LoginValido(string usuario, string contraseña)
        {
            string consulta = "SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario AND Contraseña = @contraseña;";

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nombreUsuario", usuario));
            parametros.Add(new SqlParameter("@contraseña", contraseña));

            BE.BE_Usuario usuarioValido = MapearUsuarios(Conexion.ObtenerInstacia().Leer(consulta, parametros)).FirstOrDefault();

            // Este es un comentario de prueba, BORRALO
            if (usuarioValido != null)
                return true;
            else
                return false;
        }

        public bool CrearUsuario(BE.BE_Usuario usuario)
        {
            string consulta = "INSERT INTO Usuario VALUES (@tipoUsuario, @nombreUsuario, @contraseña, @email, @bloqueado);";

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@tipoUsuario", usuario.TipoUsuario?.IdTipoUsuario));
            parametros.Add(new SqlParameter("@nombreUsuario", usuario.NombreUsuario));
            parametros.Add(new SqlParameter("@contraseña", usuario.Contraseña));
            parametros.Add(new SqlParameter("@email", usuario.Email));
            parametros.Add(new SqlParameter("@bloqueado", usuario.Bloqueado));

            int accion = Conexion.ObtenerInstacia().Escribir(consulta, parametros);
            
            if (accion > 0)
                return true;
            else
                return false;
        }

        public BE.BE_Usuario ObtenerUsuario(string usuario, string contraseña)
        {
            BE.BE_Usuario retorno = null;
            string consulta = "SELECT * FROM Usuario a, TipoUsuario b WHERE a.NombreUsuario = @nombreUsuario AND a.Contraseña = @contraseña AND a.IDTipoUsuario = b.IDTipoUsuario;";

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nombreUsuario", usuario));
            parametros.Add(new SqlParameter("@contraseña", contraseña));
            DataTable tabla = Conexion.ObtenerInstacia().Leer(consulta, parametros);

            BE.BE_Usuario usuarioValido = MapearUsuarios(tabla).FirstOrDefault();

            return usuarioValido;
        }

        public List<BE.BE_Usuario> ObtenerTodosUsuarios()
        {
            string consulta = "SELECT * FROM Usuario a, TipoUsuario b WHERE a.IdTipoUsuario = b.IdTipoUsuario;";

            DataTable tabla = Conexion.ObtenerInstacia().Leer(consulta, null);
            List<BE.BE_Usuario> usuariosValidos = MapearUsuarios(tabla);

            return usuariosValidos;
        }


        private static List<BE.BE_Usuario> MapearUsuarios(DataTable tabla)
        {
            try
            {
                List<BE.BE_Usuario> listUsuarios = new List<BE.BE_Usuario>();
                foreach (DataRow item in tabla.Rows)
                {
                    BE.BE_Usuario usuario = new BE.BE_Usuario();
                    usuario.IdUsuario = Convert.ToInt32(item["IDUsuario"]);
                    usuario.NombreUsuario = item["NombreUsuario"].ToString().Trim();
                    usuario.Contraseña = item["Contraseña"].ToString().Trim();
                    usuario.Email = item["Email"].ToString().Trim();
                    usuario.Bloqueado = Convert.ToBoolean(item["Bloqueado"]);
                    
                    BE.BE_TipoUsuario tipo = new BE.BE_TipoUsuario();
                    tipo.IdTipoUsuario = Convert.ToInt32(item["IDTipoUsuario"]);
                    tipo.Descripcion = Convert.ToString(item["Descripcion"]);

                    usuario.TipoUsuario = tipo;
                    listUsuarios.Add(usuario);
                }
                return listUsuarios;
            }
            catch (Exception)
            {
                return null;
            }

        }






    }
}
