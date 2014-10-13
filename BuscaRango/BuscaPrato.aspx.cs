using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BuscaRangoCode;

namespace BuscaRango
{
    public partial class BuscaPrato : System.Web.UI.Page
    {
        public List<BR_Prato> LstPratos;
        public List<BR_Prato> LstPratosFiltrados;

        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Deixa o botão Home com Danger
                MudaCorBotao("btnPrato");

                var pratos = PratoService.SelectAll();

                LstPratosFiltrados = new List<BR_Prato>();
                if (pratos.Sucesso)
                {
                    LstPratos = (List<BR_Prato>)pratos.RetObj;
                    LstPratos.ForEach(x => LstPratosFiltrados.Add(x));
                    CarregaPratosFiltrados();
                    Session["Data"] = LstPratos;
                }
                else
                {
                    Response.Write("Erro: " + pratos.MsgErro);
                }
            }
        }

        /// <summary>
        /// Muda a cor do botão da página atual
        /// </summary>
        /// <param name="btn"></param>
        private void MudaCorBotao(string btn)
        {
            MasterPage master = this.Master;
            Button home = (Button)master.FindControl(btn);
            home.CssClass = home.CssClass + " btn-danger";
        }

        /// <summary>
        /// Carrega os pratos que foram filtrados
        /// </summary>
        private void CarregaPratosFiltrados()
        {
            rptDados.DataSource = LstPratosFiltrados;
            rptDados.DataBind();
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
                BR_Prato prato = (BR_Prato)e.Item.DataItem;
                LinkButton nome = (LinkButton)e.Item.FindControl("lnkNome");
                LinkButton estabelecimento = (LinkButton)e.Item.FindControl("lnkEstabelecimento");
                Label descricao = (Label)e.Item.FindControl("lblDescricao");
                Label nota = (Label)e.Item.FindControl("lblNota");
                Image img = (Image)e.Item.FindControl("img");

                nome.Text = prato.Nome;
                nome.PostBackUrl = "~/VerPrato/" + prato.Id;
                estabelecimento.Text = prato.BR_Estabelecimento.Razao_Social;
                estabelecimento.PostBackUrl = "~/VerEstabelecimento/" + prato.BR_Estabelecimento.Id;
                descricao.Text = prato.Descricao;
                img.ImageUrl = "~/Img/Prato/" + prato.Imagem.ToUpper().Replace(".JPG", "_T.JPG").Replace(".PNG", "_T.PNG");

            }
        }

        /// <summary>
        /// Btn Buscar Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_OnClick(object sender, EventArgs e)
        {
            /*
            LstPratosFiltrados = ((List<BR_Prato>)Session["Data"])
                .Where(x => x.Nome.ToUpper()
                    .Contains(txtBusca.Text.ToUpper()))
                    .ToList();
            CarregaPratosFiltrados();
             */
        }
    }
}