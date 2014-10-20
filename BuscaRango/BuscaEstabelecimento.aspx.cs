using BuscaRangoCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BuscaRango
{
    public partial class BuscaEstabelecimento : System.Web.UI.Page
    {
        List<BR_Estabelecimento> lstEstabelecimentos;
        List<BR_Estabelecimento> lstEstabelecimentosFiltrados;

        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var estabelecimantos = EstabelecimentoService.SelectIn();
                lstEstabelecimentosFiltrados = new List<BR_Estabelecimento>();
                if (estabelecimantos.Sucesso)
                {
                    lstEstabelecimentos = (List<BR_Estabelecimento>)estabelecimantos.RetObj;
                    lstEstabelecimentos.ForEach(x => lstEstabelecimentosFiltrados.Add(x));
                    CarregaEstabelecimentos();
                    Session["DataE"] = lstEstabelecimentos;
                    CarregaTags();
                }
                else
                {
                    Response.Write("Erro: " + estabelecimantos.MsgErro);
                }
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
                BR_Estabelecimento estab = (BR_Estabelecimento)e.Item.DataItem;
                Label nome = (Label)e.Item.FindControl("lblNome");
                HyperLink descricao = (HyperLink)e.Item.FindControl("hplDesc");
                HyperLink estabelecimento = (HyperLink)e.Item.FindControl("hplEstab");
                Label endereco = (Label)e.Item.FindControl("lblEndereco");
                Label nota = (Label)e.Item.FindControl("lblNota");
                Image img = (Image)e.Item.FindControl("img");
                Label qtd = (Label)e.Item.FindControl("lblQtd");

                if (estab.BR_Fotos_Estabelecimento.Count > 0)
                {
                    img.ImageUrl = "~/Images/Estabelecimento/" + estab.BR_Fotos_Estabelecimento.FirstOrDefault().Imagem;
                }

                nome.Text = estab.Razao_Social;
                descricao.NavigateUrl = "~/VerEstabelecimento/" + estab.Id;
                estabelecimento.NavigateUrl = "~/VerEstabelecimento/" + estab.Id;
                qtd.Text = "Pratos:" + estab.BR_Prato.Count;
            }
        }

        /// <summary>
        /// Carrega os estabelecimentos
        /// </summary>
        private void CarregaEstabelecimentos()
        {
            rptDados.DataSource = lstEstabelecimentosFiltrados;
            rptDados.DataBind();
        }

        /// <summary>
        /// Busca Simples Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            lstEstabelecimentosFiltrados = new List<BR_Estabelecimento>();
            lstEstabelecimentosFiltrados = ((List<BR_Estabelecimento>)
            Session["DataE"]).Where(x => x.Razao_Social.ToUpper().Contains(txtBusca.Text.ToUpper())).ToList();
            CarregaEstabelecimentos();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hoverSimples", "item_hover();", true);

        }

        /// <summary>
        /// Busca Avançada Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscaAvancada_OnClick(object sender, EventArgs e)
        {
            // CheckBoxList
            List<string> tagsId = chkTags.Items.Cast<ListItem>()
            .Where(x => x.Selected)
            .Select(x => x.Value)
            .ToList();

            lstEstabelecimentosFiltrados = ((List<BR_Estabelecimento>)Session["DataE"]);

            // Descrição
            if (txtBuscaDescricao.Text != "")
            {
                lstEstabelecimentosFiltrados = lstEstabelecimentosFiltrados
                    .Where(x => x.Descricao.ToUpper()
                        .Contains(txtBuscaDescricao.Text.ToUpper()))
                    .ToList();
            }

            // Entrega
            if (chkEntrega.Checked)
            {
                lstEstabelecimentosFiltrados = lstEstabelecimentosFiltrados
                    .Where(x => x.Tel_Entrega != null)
                    .ToList();
            }

            // Reserva
            if (chkTemReserva.Checked == true)
            {
                lstEstabelecimentosFiltrados = lstEstabelecimentosFiltrados
                 .Where(x => x.Tem_Reserva == true).ToList();
            }

            // Música ao Vivo
            if (chkMusica.Checked == true)
            {
                lstEstabelecimentosFiltrados = lstEstabelecimentosFiltrados
                    .Where(x => x.Tem_Musica == true).ToList();

            }

            // Estacionamento
            if (chkEstacionamento.Checked == true)
            {
                lstEstabelecimentosFiltrados = lstEstabelecimentosFiltrados
                    .Where(x => Convert.ToBoolean(x.Tem_Estacionamento) == true).ToList();
            }

            // Fraldário
            if (chkFraldario.Checked == true)
            {
                lstEstabelecimentosFiltrados = lstEstabelecimentosFiltrados
                    .Where(x => x.Tem_Fraldario == true).ToList();
            }

            // Acesso para deficientes
            if (chkAcessoDeficiente.Checked == true)
            {
                lstEstabelecimentosFiltrados = lstEstabelecimentosFiltrados
                    .Where(x => x.Tem_Acesso_Deficiente == true).ToList();
            }

            // Abre Segunda
            if (chkAbreSegunda.Checked == true)
            {
                lstEstabelecimentosFiltrados = lstEstabelecimentosFiltrados
                    .Where(x => x.Abre_Segunda == true).ToList();
            }
            
            // Abre Sábado
            if (chkAbreSabado.Checked == true)
            {
                lstEstabelecimentosFiltrados = lstEstabelecimentosFiltrados
                    .Where(x => x.Abre_Sabado == true).ToList();
            }

            // Abre Domingo
            if (chkAbreDomingo.Checked == true)
            {
                lstEstabelecimentosFiltrados = lstEstabelecimentosFiltrados
                    .Where(x => x.Abre_Domingo == true).ToList();
            }

            // Tags
            if (tagsId.Count > 0)
            {
                foreach (string item in tagsId)
                {
                    lstEstabelecimentosFiltrados = lstEstabelecimentosFiltrados
                    .Where(x => x.BR_Tag.Any(y => y.Id.ToString() == item))
                    .ToList();
                }
            }

            CarregaEstabelecimentosComFiltros();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "hoverAvancado", "item_hover();", true);
        }

        /// <summary>
        /// Carrega os estabelecimentos filtrados
        /// </summary>
        private void CarregaEstabelecimentosComFiltros()
        {
            rptDados.DataSource = lstEstabelecimentosFiltrados;
            rptDados.DataBind();
            LimparCampos();
        }

        /// <summary>
        /// Reseta os campos de busca
        /// </summary>
        private void LimparCampos()
        {
            txtBusca.Text = "";
            txtBuscaDescricao.Text = "";
            this.chkEntrega.Checked = false;
            this.chkAbreDomingo.Checked = false;
            this.chkAbreSabado.Checked = false;
            this.chkAbreSegunda.Checked = false;
            this.chkAcessoDeficiente.Checked = false;
            this.chkEstacionamento.Checked = false;
            this.chkFraldario.Checked = false;
            this.chkMusica.Checked = false;
            this.chkTemReserva.Checked = false;

            foreach (ListItem item in chkTags.Items)
            {
                item.Selected = false;
            }
        }

        /// <summary>
        /// Carrega Tags
        /// </summary>
        private void CarregaTags()
        {
            List<BR_Tag> lst = (List<BR_Tag>)TagService.SelectAllComEstabelecimento().RetObj;
            chkTags.DataSource = lst;
            chkTags.DataTextField = "Tag";
            chkTags.DataValueField = "Id";
            chkTags.DataBind();
        }
    }
}