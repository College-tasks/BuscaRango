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
            if (!IsPostBack)
            {
                if (Page.RouteData.Values.Count < 1) Response.Redirect("~/Prato");
                Int32.TryParse(Page.RouteData.Values["idPrato"].ToString(), out Id);

                var prato = PratoService.SelectById(Id);
                if (prato.Sucesso && prato != null)
                {
                    CarregaCaracteristicas();
                    DetalhesPrato = ((BR_Prato)(prato.RetObj));
                    hplEstab.Text = DetalhesPrato.BR_Estabelecimento.Razao_Social;
                    hplEstab.NavigateUrl = "~/VerEstabelecimento/" + DetalhesPrato.BR_Estabelecimento.Id;
                    img.ImageUrl = "~/Images/Prato/" + DetalhesPrato.Imagem;
                    lblNome.Text = DetalhesPrato.Nome;
                    lblDesc.Text = DetalhesPrato.Descricao;
                    lblPreco.Text = "R$ " + DetalhesPrato.Preco;
                    litTeleEntrega.Text = DetalhesPrato.Tem_Entrega != true ? "Tele-Entrega: Não" : "Tele-Entrega: Sim";
                    CarregaAvaliacoes();
                    CarregaComentarios();
                }
                else
                {
                    Response.Redirect("~/Prato");
                }
            }
        }

        protected void rptCaracteristica_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var caract = (BR_Caracteristica_Prato)e.Item.DataItem;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var lblCaracteristica = (Label)e.Item.FindControl("lblCaracteristica");
                var rtrAvaliacao = (Rater)e.Item.FindControl("rtrAvaliacao");
                var lblNota = (Label)e.Item.FindControl("lblNota");

                lblCaracteristica.Text = caract.Caracteristica;
                // Para verificar a média
                var valor = AvaliacaoPratoService.SelectNotaByAvaliacao(Id, caract.Id).RetObj == null ? 0 : (double)AvaliacaoPratoService.SelectNotaByAvaliacao(Id, caract.Id).RetObj;
                var valorInt = Convert.ToInt32(Math.Ceiling(valor));
                rtrAvaliacao.Value = valorInt;
                lblNota.Text = "Média:" + valor.ToString("0.##");

            }

        }

        protected void rptComentario_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var coment = (BR_Comentario_Prato) e.Item.DataItem;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var lblNomeComentario = (Label) e.Item.FindControl("lblNomeComentario");
                var lblDescComentario = (Label) e.Item.FindControl("lblDescComentario");

                lblNomeComentario.Text = coment.BR_Usuario.Nome;
                lblDescComentario.Text = coment.Comentario;
            }
        }

        private void CarregaCaracteristicas()
        {
            List<BR_Caracteristica_Prato> lst = new List<BR_Caracteristica_Prato>();
            lst.Add(new BR_Caracteristica_Prato() { Id = 0, Caracteristica = "= Selecione =" });

            lst.AddRange((List<BR_Caracteristica_Prato>)CaracteristicaPratoService.SelectAll().RetObj);
            ddlCaracteristicasUsuario.DataSource = lst;
            ddlCaracteristicasUsuario.DataTextField = "Caracteristica";
            ddlCaracteristicasUsuario.DataValueField = "Id";
            ddlCaracteristicasUsuario.DataBind();
            ddlCaracteristicasUsuario.SelectedIndex = 0;
        }

        private void CarregaAvaliacoes()
        {
            var caracteristicas = (List<BR_Caracteristica_Prato>)CaracteristicaPratoService.SelectAll().RetObj;
            rptCaracteristica.DataSource = caracteristicas;
            rptCaracteristica.DataBind();
        }

        private void CarregaComentarios()
        {
            var comentarios = (List<BR_Comentario_Prato>)ComentarioPratoService.SelectById(Id).RetObj;
            rptComentario.DataSource = comentarios;
            rptComentario.DataBind();
        }

        protected void RaterAvaliacaoUsuario_Command(object sender, CommandEventArgs e)
        {
            var obj = new BR_Avaliacao_Prato();

            Int32.TryParse(Page.RouteData.Values["idPrato"].ToString(), out Id);

            var idCarac = Int32.Parse(ddlCaracteristicasUsuario.SelectedValue.ToString());
            var idUsuario = ((BR_Usuario)UsuarioService.SelectIdByName(Context.User.Identity.Name).RetObj).Id;
            var idPrato = Id;

            obj.Id_Caracteristica = idCarac;
            obj.Id_Prato = idPrato;
            obj.Id_Usuario = idUsuario;
            obj.Nota = rtrAvaliacaoUsuario.Value;
            obj.Timestamp = DateTime.Now;

            AvaliacaoPratoService.Insert(obj);

            rtrAvaliacaoUsuario.Value = 0;
            CarregaAvaliacoes();
        }


        protected void btnComentar_OnClick(object sender, EventArgs e)
        {
            if (txtComentar.Text != String.Empty)
            {
                var obj = new BR_Comentario_Prato();
                Int32.TryParse(Page.RouteData.Values["idPrato"].ToString(), out Id);

                obj.Id_Usuario = ((BR_Usuario)UsuarioService.SelectIdByName(Context.User.Identity.Name).RetObj).Id;
                obj.Id_Prato = Id;
                obj.Comentario = txtComentar.Text;

                ComentarioPratoService.Insert(obj);

                txtComentar.Text = "";

                CarregaComentarios();
            }
        }
    }
}