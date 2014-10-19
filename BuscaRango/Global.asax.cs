using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace BuscaRango
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Rotas
            RouteTable.Routes.MapPageRoute("RotaHome", "Home", "~/Default.aspx");
            RouteTable.Routes.MapPageRoute("RotaBuscaEstabelecimento", "Lugar", "~/BuscaEstabelecimento.aspx");
            RouteTable.Routes.MapPageRoute("RotaBuscaPrato", "Prato", "~/BuscaPrato.aspx");
            RouteTable.Routes.MapPageRoute("RotaVerPrato", "VerPrato/{IdPrato}/", "~/VerPrato.aspx");
            RouteTable.Routes.MapPageRoute("RotaVerEstabelecimento", "VerEstabelecimento/{IdEstabelecimento}/", "~/VerEstabelecimento.aspx");
            RouteTable.Routes.MapPageRoute("RotaConfiguracao", "Configuracao", "~/Configuracao.aspx");
            RouteTable.Routes.MapPageRoute("RotaLogin", "Login", "~/Login.aspx");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}