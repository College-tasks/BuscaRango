<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="BuscaEstabelecimento.aspx.cs" Inherits="BuscaRango.BuscaEstabelecimento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>Busca Rango | Busca de Estabelecimentos</title>
    <script>
        function item_hover(){
            $('.item-container').hover(function () {
                $(this).find('img').fadeTo(500, 0.6);
                $(this).find('a.link_to_image, a.link_to_video').css({ 'left': '-50px', 'display': 'block' }).stop().animate({ 'left': '30%', opacity: 1 }, 600);
                $(this).find('a.dragable-slider, a.link_to_video').css({ 'left': '-50px', 'display': 'block' }).stop().animate({ 'left': '45%', opacity: 1 }, 600);

                $(this).find('a.link_to_post').css({ 'right': '-50px', 'display': 'block' }).stop().animate({ 'right': '30%', opacity: 1 }, 600);

            }, function () {
                $(this).find('img').fadeTo(500, 1);
                $(this).find('a.link_to_image, a.link_to_video').css({ 'left': '50', 'display': 'block' }).stop().animate({ 'left': '-30%', opacity: 0 }, 600);
                $(this).find('a.dragable-slider, a.link_to_video').css({ 'left': '50', 'display': 'block' }).stop().animate({ 'left': '-30%', opacity: 0 }, 600);
                $(this).find('a.link_to_post').css({ 'right': '50px', 'display': 'block' }).stop().animate({ 'right': '-30%', opacity: 0 }, 600);
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphConteudo" runat="server">

    <asp:UpdatePanel ID="upBusca" runat="server">
        <ContentTemplate>
            <!--Container Start-->
            <section id="container-fluid">
                <section id="container">
                    <!--Busca Simples-->
                    <div>
                        <asp:TextBox ID="txtBusca" runat="server" Width="250px" placeholder="Buscar Estabelecimento" CssClass="txt-busca"></asp:TextBox>
                        <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" CssClass="btn-busca" />
                        <asp:Button ID="btnFiltrar" runat="server" Text="Filtros Avançados" OnClientClick="$('#busca-avancada').toggle(); return false;" CssClass="btn-busca" />
                    </div>
                    <!--Busca Avançada-->
                    <div id="busca-avancada">
                        <asp:TextBox ID="txtBuscaDescricao" runat="server" placeholder="Busca por descrição" CssClass="txt-busca"></asp:TextBox>
                        <asp:CheckBox ID="chkAbreSegunda" runat="server" Text=" Abre Segunda " />
                        <asp:CheckBox ID="chkAbreDomingo" runat="server" Text=" Abre Domingo " />
                        <asp:CheckBox ID="chkAbreSabado" runat="server" Text=" Abre Sábado " />
                        <asp:CheckBox ID="chkEntrega" runat="server" Text=" Possui Entrega " />
                        <asp:CheckBox ID="chkAcessoDeficiente" runat="server" Text=" Possui Acesso para Deficientes " />
                        <asp:CheckBox ID="chkEstacionamento" runat="server" Text=" Possui Estacionamento " />
                        <asp:CheckBox ID="chkFraldario" runat="server" Text=" Possui Fraldário " />
                        <asp:CheckBox ID="chkMusica" runat="server" Text=" Possui Música ao Vivo " />
                        <asp:CheckBox ID="chkTemReserva" runat="server" Text=" Possui Reserva " />
                        <div style="height: 75px; overflow: auto;">
                            <asp:CheckBoxList ID="chkTags" runat="server"></asp:CheckBoxList>
                        </div>
                        <br />
                        <asp:Button ID="btnBuscaAvancada" runat="server" OnClick="btnBuscaAvancada_OnClick" Text="Busca Avançada" CssClass="btn-busca" />
                    </div>
                    <!--food menu start..-->
                    <ul class="portfolio_items isotope-container clearfix portfolio-page-template gallery">
                        <!-- Dados -->
                        <asp:Repeater ID="rptDados" runat="server" OnItemDataBound="rptDados_ItemDataBound">
                            <ItemTemplate>
                                <li class="isotope-item all illustration">
                                    <div class="item-container">
                                        <asp:Image ID="img" runat="server" />
                                        <asp:Label ID="lblQtd" runat="server" Text="" CssClass="item_price"></asp:Label>
                                        <asp:HyperLink ID="hplDesc" runat="server" CssClass="link_to_image"></asp:HyperLink>
                                        <asp:HyperLink ID="hplEstab" runat="server" CssClass="link_to_post"></asp:HyperLink>
                                    </div>
                                    <div class="portfolio_post_content">
                                        <h4>
                                            <asp:Label ID="lblNome" runat="server" Text=""></asp:Label>
                                        </h4>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <!--food menu end..-->
                </section>
            </section>
            <!--Container End-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
