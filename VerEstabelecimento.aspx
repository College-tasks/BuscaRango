
<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="VerEstabelecimento.aspx.cs" Inherits="BuscaRango.VerEstabelecimento" %>

<%@ Register Namespace="ASPnetRater" Assembly="ASPnetRater" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphConteudo" runat="server">
    <!--Container Start-->
    <section id="container">
        <section id="container-fluid">
            <!--bx slider start..-->
            <div class="two-third">
                <ul class="bxslider">
                    <li>
                        <asp:Image ID="img" runat="server" />
                    </li>
                </ul>
            </div>
            <!--bx slider start..-->
            
            <!-- sidebar start..-->
            <div class="one-third-last">
                <h3>
                    <asp:Label ID="lblNome" runat="server" Text=""></asp:Label>
                </h3>
                <div class="text-container">
                    <p>
                        <asp:Label ID="lblDesc" runat="server" Text=""></asp:Label>
                    </p>
                </div>
                <div class="sidebar_container">
                    <h3>Características</h3>
                    <ul>
                        <li>
                            <asp:Literal ID="litAcessoDeficiente" runat="server"></asp:Literal>
                        </li>
                        <li>
                            <asp:Literal ID="litCustomizacao" runat="server"></asp:Literal>
                        </li>
                        <li>
                            <asp:Literal ID="litChamaGarcom" runat="server"></asp:Literal>
                        </li>
                        <li>
                            <asp:Literal ID="litEstacionamento" runat="server"></asp:Literal>
                        </li>
                        <li>
                            <asp:Literal ID="litEspacoKids" runat="server"></asp:Literal>
                        </li>
                        <li>
                            <asp:Literal ID="litFraldario" runat="server"></asp:Literal>
                        </li>
                        <li>
                            <asp:Literal ID="litTemMusica" runat="server"></asp:Literal>
                        </li>
                        <li>
                            <asp:Literal ID="litReserva" runat="server"></asp:Literal>
                        </li>
                        <li>
                            <asp:Literal ID="litWifi" runat="server"></asp:Literal>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="clear"></div>
            <!-- Pratos -->
            <ul class="portfolio_items isotope-container clearfix portfolio-page-template">
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
             <div class="sidebar_container">
                    <h3>Características</h3>
                    <ul>
                        <li>
                            <asp:Literal ID="litTeleEntrega" runat="server"></asp:Literal>
                        </li>
                    </ul>
                </div>            
            <div class="sidebar_container">
                    <h3>Avaliações</h3>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="text-container">
                                <ul>
                                    <asp:Repeater ID="rptCaracteristica" runat="server" OnItemDataBound="rptCaracteristica_ItemDataBound">
                                        <ItemTemplate>
                                            <li>
                                                <%--<%# Container.DataItem %>--%>
                                                <asp:Label ID="lblCaracteristica" runat="server" Text="Label"></asp:Label>
                                            </li>
                                            <li>
                                                <!-- Rating -->
                                                <cc1:Rater ID="rtrAvaliacao" runat="server" Value='0' MaxValue="5" Enabled="false"
                                                    ImageOff="~/images/Rating/rating_grey_star.gif" ImageOn="~/images/Rating/rating_red_star.gif"
                                                    ImageOver="~/images/Rating/rating_yellow_star.gif"></cc1:Rater>
                                                <asp:Label ID="lblNota" runat="server" Text="Label"></asp:Label>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="sidebar_container">
                    <h3>Avalie este estabelecimento!</h3>
                    <asp:UpdatePanel ID="udpAvaliacaoUsuario" runat="server">
                        <ContentTemplate>
                            <div class="text-container">
                                <ul>
                                    <li>
                                        <asp:DropDownList ID="ddlCaracteristicas" CssClass="" runat="server"></asp:DropDownList>
                                    </li>
                                    <li>
                                        <!-- Rating -->
                                        <cc1:Rater ID="rtrAvaliacaoUsuario" runat="server" Value='0' MaxValue="5" ToolTip="Avalie!"
                                            ImageOff="~/images/rating_grey_star.gif" ImageOn="~/images/rating_red_star.gif"
                                            ImageOver="~/images/rating_yellow_star.gif" CommandName='EditItem' OnCommand="RaterAvaliacaoUsuario_Command"></cc1:Rater>
                                    </li>
                                    <li>
                                        <!-- Resultado -->
                                        <asp:Label ID="lblResultado" runat="server" Visible="False" Text=""></asp:Label>
                                    </li>
                                </ul>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
        </section>
    </section>
    <!--Container End-->
</asp:Content>
