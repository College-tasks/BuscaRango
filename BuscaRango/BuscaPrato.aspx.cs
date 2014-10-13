using BuscaRangoCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            //MasterPage master = this.Master;
            //Button home = (Button)master.FindControl(btn);
            //home.CssClass = home.CssClass + " current";
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

        protected void btnBuscaAvancada_OnClick(object sender, EventArgs e)
        {
            var precoInicial = txtPrecoDe.Text.Equals("") ? 0 : double.Parse(txtPrecoDe.Text);
            var precoFinal = txtPrecoAte.Text.Equals("") ? 0 : double.Parse(txtPrecoAte.Text);

            LstPratosFiltrados = ((List<BR_Prato>)Session["Data"]);

            if (txtBuscaNome.Text != "")
            {
                LstPratosFiltrados = LstPratosFiltrados
                    .Where(x => x.Nome.ToUpper()
                        .Contains(txtBuscaNome.Text.ToUpper()))
                    .ToList();
            }

            if (txtBuscaDescricao.Text != "")
            {
                LstPratosFiltrados = LstPratosFiltrados
                    .Where(x => x.Descricao.ToUpper()
                        .Contains(txtBuscaDescricao.Text.ToUpper()))
                    .ToList();
            }

            if (!((precoInicial.Equals(0)) && (precoFinal.Equals(0))))
            {
                LstPratosFiltrados = LstPratosFiltrados
                    .Where(x => x.Preco >= precoInicial
                        && x.Preco <= precoFinal)
                    .ToList();
            }

            LstPratosFiltrados = LstPratosFiltrados
                    .Where(x => x.Tem_Entrega == chkEntrega.Checked)
                    .ToList();
            CarregaPratosFiltrados();

        }
    }
}