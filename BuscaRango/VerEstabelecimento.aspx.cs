using BuscaRangoCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BuscaRango
{
    public partial class VerEstabelecimento : System.Web.UI.Page
    {
        public BR_Estabelecimento DetalhesEstab;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!Page.IsPostBack) && (Page.RouteData.Values.ContainsKey("idEstabelecimento")))
            {
                int id = 0;
                Int32.TryParse(Page.RouteData.Values["idEstabelecimento"].ToString(), out id);

                if (id > 0)
                {
                    var estab = EstabelecimentoService.SelectById(id);
                    if (estab.Sucesso && estab != null)
                    {
                        DetalhesEstab = ((BR_Estabelecimento)(estab.RetObj));
                        img.ImageUrl = "~/Images/Estabelecimento/" + DetalhesEstab.BR_Fotos_Estabelecimento.First().Imagem;
                        lblNome.Text = DetalhesEstab.Razao_Social;
                        lblDesc.Text = DetalhesEstab.Descricao;
                        litTeleEntrega.Text = DetalhesEstab.Tem_Wifi != true ? "WiFi: Não" : "WiFi: Sim";
                    }
                    else
                    {
                        Response.Redirect("~/Estabelecimento");
                    }
                }
            }
        }
    }
}