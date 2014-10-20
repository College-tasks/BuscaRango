<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="BuscaPrato.aspx.cs" Inherits="BuscaRango.BuscaPrato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>Busca Rango | Busca de Pratos</title>
    <script>
        function item_hover() {
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
                        <asp:TextBox ID="txtBusca" runat="server" placeholder="Buscar Prato" CssClass="txt-busca" Width="250px"></asp:TextBox>
                        <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_OnClick" Text="Buscar" CssClass="btn-busca" />
                        <asp:Button ID="btnFiltrar" runat="server" Text="Filtros Avançados" OnClientClick="$('#busca-avancada').toggle(); return false;" CssClass="btn-busca" />
                    </div>
                    <!--Busca Avançada-->
                    <div id="busca-avancada">
                        <asp:TextBox ID="txtBuscaDescricao" runat="server" placeholder="Busca por descrição" CssClass="txt-busca"></asp:TextBox>
                        <asp:TextBox ID="txtPrecoDe" runat="server" placeholder="Preço DE" type="number" step="any" CssClass="txt-busca"></asp:TextBox>
                        <asp:TextBox ID="txtPrecoAte" runat="server" placeholder="Preço ATÉ" type="number" step="any" CssClass="txt-busca"></asp:TextBox>
                        <asp:CheckBox ID="chkEntrega" runat="server" Text=" Possui Entrega " />
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
                                        <asp:Label ID="lblPreco" runat="server" Text="" CssClass="item_price"></asp:Label>
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
