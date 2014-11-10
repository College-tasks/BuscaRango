
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
    public partial class VerEstabelecimento : System.Web.UI.Page
    {
        public BR_Estabelecimento DetalhesEstab;
        int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    litWifi.Text = DetalhesEstab.Tem_Wifi != true ? "WiFi: Não" : "WiFi: Sim";
                    litEstacionamento.Text = DetalhesEstab.Tem_Estacionamento != true ? "Estacionamento: Não" : "Estacionamento: Sim";
                    litAcessoDeficiente.Text = DetalhesEstab.Tem_Acesso_Deficiente != true ? "Acesso a deficiente: Não" : "Acesso a deficiente: Sim";
                    litFraldario.Text = DetalhesEstab.Tem_Fraldario != true ? "Fraldário: Não" : "Fraldário: Sim";
                    litReserva.Text = DetalhesEstab.Tem_Reserva != true ? "Reserva: Não" : "Reserva: Sim";
                    litEspacoKids.Text = DetalhesEstab.Tem_Espaco_Kids != true ? "Espaço Kids: Não" : "Espaço Kids: Sim";
                    litReserva.Text = DetalhesEstab.Tem_Reserva != true ? "Reserva: Não" : "Reserva: Sim";
                    litTemMusica.Text = DetalhesEstab.Tem_Musica != true ? "Música: Não" : "Música: Sim";
                    litCustomizacao.Text = DetalhesEstab.Tem_Customizacao != true ? "Customização: Não" : "Customização: Sim";
                    litChamaGarcom.Text = DetalhesEstab.Tem_Chamada_Garcom != true ? "Chamar garçom: Não" : "Chamar garçom: Sim";

                    rptDados.DataSource = DetalhesEstab.BR_Prato;
                    rptDados.DataBind();
                    //TODO: Duvida?????
                    CarregaCaracterirticas();
                }
                else
                {
                    Response.Redirect("~/Estabelecimento");
                }
            }
            else
            {
                Response.Redirect("~/Estabelecimento");
            }
        }

        /// <summary>
        /// Repeater DataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptDados_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var prato = (BR_Prato)e.Item.DataItem;
                var nome = (Label)e.Item.FindControl("lblNome");
                var estabelecimento = (HyperLink)e.Item.FindControl("hplEstab");
                var descricao = (HyperLink)e.Item.FindControl("hplDesc");
                var preco = (Label)e.Item.FindControl("lblPreco");
                var img = (Image)e.Item.FindControl("img");

                nome.Text = prato.Nome;
                preco.Text = "R$ " + prato.Preco;
                descricao.NavigateUrl = "~/VerPrato/" + prato.Id;
                //estabelecimento.Text = prato.BR_Estabelecimento.Razao_Social;
                estabelecimento.NavigateUrl = "~/VerEstabelecimento/" + prato.BR_Estabelecimento.Id;
                //descricao.Text = prato.Descricao;
                img.ImageUrl = "~/Images/Prato/" + prato.Imagem;

                var caracteristicas = (List<BR_Caracteristica_Estabelecimento>)CaracteristicaEstabelecimentoService.SelectAll().RetObj;
                rptCaracteristica.DataSource = caracteristicas;
                rptCaracteristica.DataBind();

            }
        }
        protected void rptCaracteristica_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var caract = (BR_Caracteristica_Estabelecimento)e.Item.DataItem;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var lblNota = (Label)e.Item.FindControl("lblNota");
                var lblCaracteristica = (Label)e.Item.FindControl("lblCaracteristica");
                var rtrAvaliacao = (Rater)e.Item.FindControl("rtrAvaliacao");


                // Para verificar a média
                var valor = AvaliacaoEstabelecimentoService.SelectNotaByAvaliacao(id, caract.Id).RetObj == null ? 0 : (double)AvaliacaoEstabelecimentoService.SelectNotaByAvaliacao(id, caract.Id).RetObj;
                var valorInt = Convert.ToInt32(Math.Ceiling(valor));
                rtrAvaliacao.Value = valorInt;
                lblNota.Text = valor.ToString();             
            }
        }

        private void CarregaCaracterirticas()
        {
            var lst = (List<BR_Caracteristica_Estabelecimento>)CaracteristicaEstabelecimentoService.SelectAll().RetObj;
            ddlCaracteristicas.DataSource = lst;
            ddlCaracteristicas.DataTextField = "Caracteristica";
            ddlCaracteristicas.DataValueField = "Id";
            ddlCaracteristicas.DataBind();
        }

        protected void RaterAvaliacaoUsuario_Command(object sender, CommandEventArgs e)
        {
            var obj = new BR_Avaliacao_Estabelecimento();

            var idCarac = Int32.Parse(ddlCaracteristicas.SelectedValue);
            var idUsuario = ((BR_Usuario)UsuarioService.SelectIdByName(Context.User.Identity.Name).RetObj).Id;
            var idEstabelecimento = id;

            obj.Id_Caracteristica = idCarac;
            obj.Id_Estabelecimento = id;
            obj.Id_Usuario = idUsuario;
            obj.Nota = rtrAvaliacaoUsuario.Value;
            //Todo: Joelso data: 07/11/2014
            //obj.Timestamp = DateTime.Now;

            AvaliacaoEstabelecimentoService.Insert(obj);           

        }

    }
}
