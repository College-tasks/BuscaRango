using BuscaRangoCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BuscaRango
{
    public partial class Default : System.Web.UI.Page
    {
        List<BR_Avaliacao_Estabelecimento> avEstabelecimento;
        List<BR_Avaliacao_Prato> avPrato;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Não logado
            if (String.IsNullOrEmpty(Context.User.Identity.Name))
            {
                pnlSemLogin.Visible = true;
                pnlLogin.Visible = false;
            }
            else // Logado
            {
                pnlSemLogin.Visible = false;
                pnlLogin.Visible = true;
                lblUser.Text = "Bem-vindo(a), " + Context.User.Identity.Name;
                LoadPratos();
                LoadLugar();
                LoadRepeater(false);
            }
        }

        /// <summary>
        /// Carrega o número de pratos avaliados
        /// </summary>
        private void LoadPratos()
        {
            int idUser = ((BR_Usuario)UsuarioService.SelectIdByName(Context.User.Identity.Name).RetObj).Id;
            this.avPrato = (List<BR_Avaliacao_Prato>)AvaliacaoPratoService.SelectAll().RetObj;
            avPrato = avPrato.Where(x => x.Id_Usuario == idUser).ToList();

            lblPratos.Text = "Você avaliou " + avPrato.GroupBy(x => x.Id_Prato).Select(s => new { Id_Prato = s.Key}).ToList().Count + " prato(s).";
        }

        /// <summary>
        /// Carrega o número de lugares carregados
        /// </summary>
        private void LoadLugar()
        {
            int idUser = ((BR_Usuario)UsuarioService.SelectIdByName(Context.User.Identity.Name).RetObj).Id;
            this.avEstabelecimento = (List<BR_Avaliacao_Estabelecimento>)AvaliacaoEstabelecimentoService.SelectAll().RetObj;
            avEstabelecimento = avEstabelecimento.Where(x => x.Id_Usuario == idUser).ToList();

            lblLugares.Text = "Você avaliou " + avEstabelecimento.GroupBy(x => x.Id_Estabelecimento).Select(s => new { Id_Estabelecimento = s.Key }).ToList().Count + " lugares(s).";
        }

        /// <summary>
        /// Carrega o Repeater
        /// </summary>
        private void LoadRepeater(bool todoHistorico)
        {
            List<ItemHistorico> lstHist = new List<ItemHistorico>();
            avEstabelecimento.ForEach(x => lstHist.Add(new ItemHistorico() { Caracteristica = x.BR_Caracteristica_Estabelecimento.Caracteristica, Nota = x.Nota, Img = x.BR_Estabelecimento.BR_Fotos_Estabelecimento.First().Imagem, TimeStamp = x.Timestamp, IsPrato = false, Nome = x.BR_Estabelecimento.Razao_Social }));
            avPrato.ForEach(x => lstHist.Add(new ItemHistorico() { Caracteristica = x.BR_Caracteristica_Prato.Caracteristica, Nota = x.Nota, Img = x.BR_Prato.Imagem, TimeStamp = x.Timestamp, IsPrato = true, Nome = x.BR_Prato.Nome }));

            lstHist = lstHist.OrderByDescending(x => x.TimeStamp).ToList();

            if (!todoHistorico)
            {
                rptHistorico.DataSource = lstHist.Take(7);
            }
            else
            {
                rptHistorico.DataSource = lstHist;
            }
            
            rptHistorico.DataBind();
        }

        /// <summary>
        /// Repeater ItemDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptHistorico_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var itemHist = (ItemHistorico)e.Item.DataItem;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var img = (Image)e.Item.FindControl("imgHistorico");
                var lblCarac = (Label)e.Item.FindControl("lblCarac");
                var lblNota = (Label)e.Item.FindControl("lblNota");
                var lblNome = (Label)e.Item.FindControl("lblNome");

                lblNota.Text = "Nota: " + itemHist.Nota.ToString();
                lblCarac.Text = "Característica: " + itemHist.Caracteristica;
                lblNome.Text = itemHist.Nome;

                img.Width = 100;
                img.Height = 100;
                img.ImageUrl = itemHist.IsPrato ? "~/images/prato/" + itemHist.Img : "~/images/estabelecimento/" + itemHist.Img;
            }
        }

        /// <summary>
        /// Ver Histórico Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnHistorico_Click(object sender, EventArgs e)
        {
            LoadRepeater(true);
            btnHistorico.Enabled = false;
            btnHistorico.CssClass = "btn-historico disabled";
        }
    }
}