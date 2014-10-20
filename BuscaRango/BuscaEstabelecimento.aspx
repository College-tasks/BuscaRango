<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="BuscaEstabelecimento.aspx.cs" Inherits="BuscaRango.BuscaEstabelecimento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>Busca Rango | Busca de Estabelecimentos</title>
    <link rel="stylesheet" type="text/css" href="css/Isotope.css" />
    <link rel="stylesheet" type="text/css" href="css/owl.carousel.css" />
    <link rel="stylesheet" type="text/css" href="css/prettyPhoto.css" />
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
                        <asp:CheckBox ID="chkEntrega" runat="server" Text="Possui Tele-Entrega" />
                        <asp:CheckBox ID="chkMusica" runat="server" Text="Possui Música Ao Vivo" />
                        <asp:CheckBox ID="chkEstacionamento" runat="server" Text="Possui Estacionamento" />
                        <asp:CheckBox ID="chkFraldario" runat="server" Text="Possui Fraldário" />
                        <asp:CheckBoxList ID="chkTags" runat="server"></asp:CheckBoxList>
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
