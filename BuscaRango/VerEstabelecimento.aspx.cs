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

            }
        }
    }
}