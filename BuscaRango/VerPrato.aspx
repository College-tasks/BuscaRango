<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="VerPrato.aspx.cs" Inherits="BuscaRango.VerPrato" %>

<%@ Register Namespace="ASPnetRater" Assembly="ASPnetRater" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>Busca Rango | Ver Prato</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphConteudo" runat="server">
    <!--Container Start-->
    <section id="container-fluid">
        <section id="container">

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
                    <asp:Label ID="lblNome" runat="server"></asp:Label>
                </h3>
                <div class="text-container">
                    <p>
                        <asp:Label ID="lblPreco" runat="server"></asp:Label>
                        <br />
                        <asp:HyperLink ID="hplEstab" runat="server"></asp:HyperLink>
                        <br />
                        <asp:Label ID="lblDesc" runat="server"></asp:Label>
                        <br />
                    </p>
                </div>
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
                    <asp:UpdatePanel ID="udpAvaliacao" runat="server">
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
                    <h3>Avalie este prato!</h3>
                    <asp:UpdatePanel ID="udpAvaliacaoUsuario" runat="server">
                        <ContentTemplate>
                            <div class="text-container">
                                <ul>
                                    <li>
                                        <asp:DropDownList ID="ddlCaracteristicasUsuario" CssClass="" runat="server"></asp:DropDownList>
                                    </li>
                                    <li>
                                        <!-- Rating -->
                                        <cc1:Rater ID="rtrAvaliacaoUsuario" runat="server" Value='0' MaxValue="5" ToolTip="Avalie!"
                                            ImageOff="~/images/Rating/rating_grey_star.gif" ImageOn="~/images/Rating/rating_red_star.gif"
                                            ImageOver="~/images/Rating/rating_yellow_star.gif" CommandName='EditItem' OnCommand="RaterAvaliacaoUsuario_Command"></cc1:Rater>
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
                <div class="clear"></div>
            </div>
        </section>
    </section>
    <!--Container End-->
</asp:Content>
