using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BuscaRango
{
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.Params["logout"]))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Login");
            }
        }

        /// <summary>
        /// Login Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidaUsuario(txtEmail.Text, txtPass.Text))
            {
                FormsAuthentication.RedirectFromLoginPage(txtEmail.Text, true);
            }
            else
            {
                lblMsg.Text = "Email e/ou usuário inválido!";
            }
        }

        /// <summary>
        /// Valida o usuário
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private bool ValidaUsuario(string user, string pass)
        {
            return true;
        }

        /// <summary>
        /// Criptografa uma string (MD5) e retorna o resultado
        /// </summary>
        /// <param name="input">String de entrada</param>
        /// <returns>String encriptada</returns>
        public static string MD5Encrypt(string input)
        {
            MD5 md5Hasher = MD5.Create();

            // Criptografa
            byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Cria um StringBuilder para passarmos os bytes gerados para ele
            StringBuilder strBuilder = new StringBuilder();

            // Converte cada byte em um valor hexadecimal e adiciona ao string builder
            for (int i = 0; i < valorCriptografado.Length; i++)
            {
                strBuilder.Append(valorCriptografado[i].ToString("x2"));
            }

            // retorna o valor criptografado
            return strBuilder.ToString().ToUpper();
        }
    }
}