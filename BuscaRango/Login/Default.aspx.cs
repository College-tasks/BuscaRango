using ASPSnippets.FaceBookAPI;
using BuscaRangoCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
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
            //FormsAuthentication.RedirectFromLoginPage("52.marcos.vinicius@gmail.com", true);

            if (!String.IsNullOrEmpty(Request.Params["logout"]))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Login");
            }

            // Facebook Login
            FaceBookConnect.API_Key = "366974283471779";
            FaceBookConnect.API_Secret = "fdeb8030a58b50111a76f1fad3010c49";

            if (!IsPostBack)
            {
                if (Request.QueryString["error"] == "access_denied")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User has denied access.')", true);
                    return;
                }

                string code = Request.QueryString["code"];
                if (!string.IsNullOrEmpty(code))
                {
                    string data = FaceBookConnect.Fetch(code, "me");
                    FacebookUser faceBookUser = new JavaScriptSerializer().Deserialize<FacebookUser>(data);
                    faceBookUser.PictureUrl = string.Format("https://graph.facebook.com/{0}/picture", faceBookUser.Id);

                    Retorno ret = UsuarioService.LogarFB(faceBookUser);

                    if (ret.Sucesso)
                    {
                        FormsAuthentication.RedirectFromLoginPage(faceBookUser.Email, true);
                    }
                    else
                    {
                        lblMsg.Text = "Erro: " + ret.MsgErro;
                    }
                }

                
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
            string password = MD5Encrypt(pass);
            Retorno ret = UsuarioService.Logar(user, password);
            return ret.Sucesso;
        }

        /// <summary>
        /// Criptografa uma string (MD5) e retorna o resultado
        /// </summary>
        /// <param name="input">String de entrada</param>
        /// <returns>String encriptada</returns>
        public static string MD5Encrypt(string inp)
        {
            MD5 md5Hasher = MD5.Create();

            string input = "d5aw8e9d4D8SW89d" + inp + "DS84dsa8d418das6dwe8";

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

        /// <summary>
        /// Facebook Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFacebook_Click(object sender, EventArgs e)
        {
            FaceBookConnect.Authorize("user_photos,email", Request.Url.AbsoluteUri.Split('?')[0]);
        }
    }
}