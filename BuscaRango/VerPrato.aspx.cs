using System.Globalization;
using ASPnetRater;
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
        public int Id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Int32.TryParse(Page.RouteData.Values["idPrato"].ToString(), out Id);

            if (Id > 0)
            {
                var prato = PratoService.SelectById(Id);
                if (prato.Sucesso && prato != null)
                {
                    CarregaCaracterirticas()
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
            else
            {
                Response.Redirect("~/Prato");
            }
        }

        protected void rptCaracteristica_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var avaliacao = (BR_Avaliacao_Prato)AvaliacaoPratoService.SelectAvaliacaoPratoById(Id).RetObj;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var lblCaracteristica = (Label)e.Item.FindControl("lblCaracteristica");
                var rtrAvaliacao = (Rater)e.Item.FindControl("rtrAvaliacao");

                lblCaracteristica.Text = avaliacao.BR_Caracteristica_Prato.Caracteristica;
                rtrAvaliacao.Value = avaliacao.Nota;

            }

        }

        private void CarregaCaracterirticas()
        {
            var lst = (List<BR_Caracteristica_Prato>)CaracteristicaPratoService.SelectAll().RetObj;
            ddlCaracteristicasUsuario.DataSource = lst;
            ddlCaracteristicasUsuario.DataTextField = "Caracteristica";
            ddlCaracteristicasUsuario.DataValueField = "Id";
            ddlCaracteristicasUsuario.DataBind();
        }

        protected void RaterAvaliacaoUsuario_Command(object sender, CommandEventArgs e)
        {
            var obj = new BR_Avaliacao_Prato();
            var idUsuario = (BR_Usuario)UsuarioService.SelectIdByName(Context.User.Identity.Name).RetObj;
            var avaliacao = (BR_Avaliacao_Prato)AvaliacaoPratoService.SelectAvaliacaoPratoById(Id).RetObj;
            if (avaliacao != null)
            {
                obj.Id_Prato = Id;
                obj.Id_Caracteristica = ddlCaracteristicasUsuario.SelectedIndex;
                obj.Id_Usuario = idUsuario.Id;
                obj.Nota = rtrAvaliacaoUsuario.Value;
                AvaliacaoPratoService.Update(obj, Id);
            }
            else
            {
                obj.Id_Prato = Id;
                obj.Id_Caracteristica = ddlCaracteristicasUsuario.SelectedIndex;
                obj.Id_Usuario = idUsuario.Id;
                obj.Nota = rtrAvaliacaoUsuario.Value;
                AvaliacaoPratoService.Insert(obj);
            }
        }

    }
}