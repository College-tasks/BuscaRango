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
