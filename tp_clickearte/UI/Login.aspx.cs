using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



        }

        protected void btnCrearUsuario_Click(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;


            DAL.DAO_Usuario usuarioDao = new DAL.DAO_Usuario();

            if (usuarioDao.LoginValido(usuario, contraseña))
            {
               BE.BE_Usuario usuarioLogueado = usuarioDao.ObtenerUsuario(usuario, contraseña);
                if (usuarioLogueado != null)
                {
                    Session["usuarioLogueado"] = usuarioLogueado;
                    ObtenerVista(usuarioLogueado);
                }
            }
            else
            {
                lblMensajes.Text = "Error de usuario y/o contraseña";
            }



        }

        private void ObtenerVista(BE_Usuario usuarioLogueado)
        {
            switch (usuarioLogueado.TipoUsuario.IdTipoUsuario)
            {
                case 1:
                    Response.Redirect("MenuAdministrador.aspx");
                    break;
                case 2: 
                    Response.Redirect("MenuClientes.aspx");
                    break;
                default:
                    break;
            }
        }
    }
}