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
                var pratos = PratoService.SelectAll();

                LstPratosFiltrados = new List<BR_Prato>();
                if (pratos.Sucesso)
                {
                    LstPratos = (List<BR_Prato>)pratos.RetObj;
                    LstPratos.ForEach(x => LstPratosFiltrados.Add(x));
                    CarregaPratosFiltrados();
                    Session["Data"] = LstPratos;
                    CarregaTags();
                }
                else
                {
                    Response.Write("Erro: " + pratos.MsgErro);
                }
            }
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
                CarregaCaracterirticas();
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
            LstPratosFiltrados = ((List<BR_Prato>)Session["Data"])
                .Where(x => x.Nome.ToUpper()
                    .Contains(txtBusca.Text.ToUpper()))
                    .ToList();
            CarregaPratosFiltrados();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "hoverSimples", "item_hover();", true);
        }

        /// <summary>
        /// Busca Avançada Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscaAvancada_OnClick(object sender, EventArgs e)
        {
            var precoInicial = txtPrecoDe.Text.Equals("") ? 0 : double.Parse(txtPrecoDe.Text);
            var precoFinal = txtPrecoAte.Text.Equals("") ? 0 : double.Parse(txtPrecoAte.Text);

            // CheckBoxList
            List<string> tagsId = chkTags.Items.Cast<ListItem>()
            .Where(x => x.Selected)
            .Select(x => x.Value)
            .ToList();

            LstPratosFiltrados = ((List<BR_Prato>)Session["Data"]);

            // Descrição
            if (txtBuscaDescricao.Text != "")
            {
                LstPratosFiltrados = LstPratosFiltrados
                    .Where(x => x.Descricao.ToUpper()
                        .Contains(txtBuscaDescricao.Text.ToUpper()))
                    .ToList();
            }

            // Preço inicial
            if (precoInicial > 0)
            {
                LstPratosFiltrados = LstPratosFiltrados
                                    .Where(x => x.Preco >= precoInicial)
                                    .ToList();
            }

            // Preço final
            if (precoFinal > 0)
            {
                LstPratosFiltrados = LstPratosFiltrados
                    .Where(x => x.Preco <= precoFinal)
                    .ToList();
            }

            // Entrega
            if (chkEntrega.Checked)
            {
                LstPratosFiltrados = LstPratosFiltrados
                    .Where(x => x.Tem_Entrega == true)
                    .ToList();
            }

            // Tags
            if (tagsId.Count > 0)
            {
                foreach (string item in tagsId)
                {
                    LstPratosFiltrados = LstPratosFiltrados
                    .Where(x => x.BR_Tag.Any(y => y.Id.ToString() == item))
                    .ToList();
                }
            }

            CarregaPratosFiltrados();
            ResetaCamposBusca();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hoverAvancado", "item_hover();", true);
        }

        /// <summary>
        /// Reseta os campos de busca
        /// </summary>
        private void ResetaCamposBusca()
        {
            txtBusca.Text = "";
            txtBuscaDescricao.Text = "";
            txtPrecoAte.Text = "";
            txtPrecoDe.Text = "";
            chkEntrega.Checked = false;

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
            List<BR_Tag> lst = (List<BR_Tag>)TagService.SelectAllComPrato().RetObj;
            chkTags.DataSource = lst;
            chkTags.DataTextField = "Tag";
            chkTags.DataValueField = "Id";
            chkTags.DataBind();
        }
        protected void ddlBuscaOrdenada_OnLoad(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlBuscaOrdenada.Items.Insert(0, "Orderar por");
                ddlBuscaOrdenada.Items.Insert(1, "Maior Preço");
                ddlBuscaOrdenada.Items.Insert(2, "Menor Preço");
                ddlBuscaOrdenada.Items.Insert(3, "Nome A-Z");
                ddlBuscaOrdenada.Items.Insert(4, "Nome Z-A");
                ddlBuscaOrdenada.SelectedIndex = 0;
            }
        }

        protected void ddlBuscaOrdenada_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //Maior Preço
            if (ddlBuscaOrdenada.SelectedIndex == 1)
            {
                LstPratosFiltrados = ((List<BR_Prato>)Session["Data"]).OrderByDescending(x => x.Preco).ToList();
            }

            //Menor Preço
            if (ddlBuscaOrdenada.SelectedIndex == 2)
            {
                LstPratosFiltrados = ((List<BR_Prato>)Session["Data"]).OrderBy(x => x.Preco).ToList();
            }

            //Nome A-Z
            if (ddlBuscaOrdenada.SelectedIndex == 3)
            {
                LstPratosFiltrados = ((List<BR_Prato>)Session["Data"]).OrderBy(x => x.Nome).ToList();
            }

            //Nome Z-A
            if (ddlBuscaOrdenada.SelectedIndex == 4)
            {
                LstPratosFiltrados = ((List<BR_Prato>)Session["Data"]).OrderByDescending(x => x.Nome).ToList();
            }
            CarregaPratosFiltrados();
        }

        private void CarregaCaracterirticas()
        {
            var lst = (List<BR_Caracteristica_Prato>)CaracteristicaPratoService.SelectAll().RetObj;
            ddlCaracteristicas.DataSource = lst;
            ddlCaracteristicas.DataTextField = "Caracteristica";
            ddlCaracteristicas.DataValueField = "Id";
            ddlCaracteristicas.DataBind();
        }

        protected void ddlCaracteristicas_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LstPratosFiltrados = ((List<BR_Prato>)Session["Data"])
                .OrderByDescending(x => x.BR_Avaliacao_Prato
                    .Where(y => y.Id_Caracteristica == Convert.ToInt32(ddlCaracteristicas.SelectedValue))
                    .Average(y => y.Nota)).ToList();
            CarregaPratosFiltrados();
        }
    }
}