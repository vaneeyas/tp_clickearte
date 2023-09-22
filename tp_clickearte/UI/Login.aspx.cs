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
            BE.BE_Usuario usuarioLogueado = null;
            DAL.DAO_Usuario usuarioDao = new DAL.DAO_Usuario();

            if (usuarioDao.LoginValido("admin", "admin"))
            {
                usuarioLogueado = usuarioDao.ObtenerUsuario("admin", "admin");
            }


        }
    }
}