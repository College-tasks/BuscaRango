using BuscaRangoCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BuscaRango
{
    public partial class VerPrato : System.Web.UI.Page
    {
        public BR_Prato DetalhesPrato;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!Page.IsPostBack) && (Page.RouteData.Values.ContainsKey("idPrato")))
            {
                int id = 0;
                Int32.TryParse(Page.RouteData.Values["idPrato"].ToString(), out id);

                if (id > 0)
                {
                    var prato = PratoService.SelectById(id);
                    if (prato.Sucesso && prato != null)
                    {
                        DetalhesPrato = ((BR_Prato)(prato.RetObj));
                        hplEstab.Text = DetalhesPrato.BR_Estabelecimento.Razao_Social;
                        hplEstab.NavigateUrl = "~/VerEstabelecimento/" + DetalhesPrato.BR_Estabelecimento.Id;
                        img.ImageUrl = "~/Images/Prato/" + DetalhesPrato.Imagem;
                        lblNome.Text = DetalhesPrato.Nome;
                        lblDesc.Text = DetalhesPrato.Descricao;
                        lblPreco.Text = "R$ " + DetalhesPrato.Preco;
                        litTeleEntrega.Text = DetalhesPrato.Tem_Entrega != true ? "Tele-Entrega: Não" : "Tele-Entrega: Sim";
                    }
                    else
                    {
                        Response.Redirect("~/Prato");
                    }
                }
            }
            else
            {
                Response.Redirect("~/Prato");
            }
        }
    }
}