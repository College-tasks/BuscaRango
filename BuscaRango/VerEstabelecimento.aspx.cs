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
        public int Id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.RouteData.Values.Count < 1) Response.Redirect("~/Lugar");
                Int32.TryParse(Page.RouteData.Values["IdEstabelecimento"].ToString(), out Id);
                var estab = EstabelecimentoService.SelectById(Id);

                if (estab.Sucesso && estab != null)
                {
                    CarregaCaracterirticas();
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
                    CarregaAvaliacoes();
                    CarregaComentarios();
                    //CarregarMapa(DetalhesEstab.Endereco.ToString());

                    
                }
                else
                {
                    Response.Redirect("~/Lugar");
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

        //Comentários
        protected void rptComentario_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var coment = (BR_Comentario_Estabelecimento)e.Item.DataItem;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var lblNomeComentario = (Label)e.Item.FindControl("lblNomeComentario");
                var lblDescComentario = (Label)e.Item.FindControl("lblDescComentario");

                lblNomeComentario.Text = coment.BR_Usuario.Nome;
                lblDescComentario.Text = coment.Comentario;
            }
        }


        // Características
        protected void rptCaracteristica_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var caract = (BR_Caracteristica_Estabelecimento)e.Item.DataItem;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var lblCaracteristica = (Label)e.Item.FindControl("lblCaracteristica");
                var rtrAvaliacao = (Rater)e.Item.FindControl("rtrAvaliacao");
                var lblNota = (Label)e.Item.FindControl("lblNota");

                lblCaracteristica.Text = caract.Caracteristica;
                // Para verificar a média
                var valor = AvaliacaoEstabelecimentoService.SelectNotaByAvaliacao(Id, caract.Id).RetObj == null ? 0 : (double)AvaliacaoEstabelecimentoService.SelectNotaByAvaliacao(Id, caract.Id).RetObj;
                var valorInt = Convert.ToInt32(Math.Ceiling(valor));
                rtrAvaliacao.Value = valorInt;
                lblNota.Text = "Média:" + valor.ToString("0.##");

            }
        }

        protected void btnBuscarMapa_Click(object sender, EventArgs e)
        {

        }

        private void CarregarMapa(string endereco)
        {
            GoogleService google = new GoogleService();
            //Buscar latitude e longitude para montar o mapa conforme endereço de cada estabelecimento.
            google.PegarLatitudeLongitude(endereco);
            //Chamar a montagem do mapa

        }

        private void CarregaComentarios()
        {
            var comentarios = (List<BR_Comentario_Estabelecimento>)ComentariosEstabelecimentosService.SelectById(Id).RetObj;
            rptComentario.DataSource = comentarios;
            rptComentario.DataBind();
        }
        protected void btnComentar_OnClick(object sender, EventArgs e)
        {
            if (txtComentar.Text != String.Empty)
            {
                var obj = new BR_Comentario_Estabelecimento();
                Int32.TryParse(Page.RouteData.Values["idEstabelecimento"].ToString(), out Id);

                obj.Id_Usuario = ((BR_Usuario)UsuarioService.SelectIdByName(Context.User.Identity.Name).RetObj).Id;
                obj.Id_Estabelecimento = Id;
                obj.Comentario = txtComentar.Text;

                ComentariosEstabelecimentosService.Insert(obj);

                txtComentar.Text = "";

                CarregaComentarios();
            }
        }



        private void CarregaCaracterirticas()
        {
            List<BR_Caracteristica_Estabelecimento> lst = new List<BR_Caracteristica_Estabelecimento>();
            lst.Add(new BR_Caracteristica_Estabelecimento() { Id = 0, Caracteristica = "= Selecione =" });

            lst.AddRange((List<BR_Caracteristica_Estabelecimento>)CaracteristicaEstabelecimentoService.SelectAll().RetObj);
            ddlCaracteristicasUsuario.DataSource = lst;
            ddlCaracteristicasUsuario.DataTextField = "Caracteristica";
            ddlCaracteristicasUsuario.DataValueField = "Id";
            ddlCaracteristicasUsuario.DataBind();
            ddlCaracteristicasUsuario.SelectedIndex = 0;
        }

        private void CarregaAvaliacoes()
        {
            // Faltou dar um DataBind() no Repeater
            var caracteristicas = (List<BR_Caracteristica_Estabelecimento>)CaracteristicaEstabelecimentoService.SelectAll().RetObj;
            rptCaracteristica.DataSource = caracteristicas;
            rptCaracteristica.DataBind();
        }

        protected void RaterAvaliacaoUsuario_Command(object sender, CommandEventArgs e)
        {
            var obj = new BR_Avaliacao_Estabelecimento();

            Int32.TryParse(Page.RouteData.Values["IdEstabelecimento"].ToString(), out Id);

            var idCarac = Int32.Parse(ddlCaracteristicasUsuario.SelectedValue.ToString());
            var idUsuario = ((BR_Usuario)UsuarioService.SelectIdByName(Context.User.Identity.Name).RetObj).Id;
            var idEstab = Id;

            obj.Id_Caracteristica = idCarac;
            obj.Id_Estabelecimento = idEstab;
            obj.Id_Usuario = idUsuario;
            obj.Nota = rtrAvaliacaoUsuario.Value;
            obj.Timestamp = DateTime.Now;

            AvaliacaoEstabelecimentoService.Insert(obj);

            rtrAvaliacaoUsuario.Value = 0;
            CarregaAvaliacoes();

        }
    }
}
