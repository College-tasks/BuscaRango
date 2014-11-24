using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BuscaRango
{
    public partial class Principal : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Context.User.Identity.Name))
            {
                btnLogin.Text = "Logar";
            }
            else
            {
                lblUser.Text = "Olá, " + Context.User.Identity.Name + " - ";
                btnLogout.Text = "Sair";
            }
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Home");
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login");
        }
    }
}