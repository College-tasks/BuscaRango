<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="VerEstabelecimento.aspx.cs" Inherits="BuscaRango.VerEstabelecimento" %>

<%@ Register Namespace="ASPnetRater" Assembly="ASPnetRater" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript" src="jquery-ui.custom.min.js"></script>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=ColoqueASuaKeyAqui&amp;sensor=false"></script>
    <style type="text/css">
        #mapa {
            width: 300px;
            height: 300px;
        }
    </style>
    <script type="text/javascript" src="http://maps.google.com/maps/api/js? sensor=true"></script>
    <script type="text/javascript">
        var mapa;
        function IniciaMapa() {
            //var longitude = document.getElementById('lblLat').value;
            //var latitude = document.getElementById('lblLog').value;

            var longitude = -29.9906740;
            var latitude = -51.1293523;
            var latlng = new google.maps.LatLng(-29.9906740, -51.1293523);
            var Opcoes = {
                zoom: 16,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            mapa = new google.maps.Map(document.getElementById("mapa"), Opcoes);
            var marker = new google.maps.Marker
            (
                {
                    position: new google.maps.LatLng(-29.9906740, -51.1293523),
                    map: mapa,
                    title: 'Clique aqui'
                }
            );
            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(mapa, marker);
            });
        }
        window.onload = IniciaMapa;
    </script>
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
                <div>
                    <asp:Label ID="lblLat" runat="server"></asp:Label>
                    <asp:Label ID="lblLog" runat="server"></asp:Label>
                </div>
                <!--<div style="float: left" id="mapa"></div>-->
                <div class="sidebar_container">
                    <h3>Avalie este estabelecimento!</h3>
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
                                            <hr />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                
                <div class="sidebar_container">
                <h3>Comentários</h3>
                <asp:UpdatePanel ID="udpComentario" runat="server">
                    <ContentTemplate>
                        <!-- Comentários -->
                        <div class="comments">
                            <asp:Repeater ID="rptComentario" runat="server" OnItemDataBound="rptComentario_OnItemDataBound">
                                <ItemTemplate>
                                    <div class="comments-list">
                                        <div class="description">
                                            <div class="comment-meta">
                                                <cite>
                                                    <asp:Label ID="lblNomeComentario" runat="server" Text="Label"></asp:Label></cite>
                                            </div>
                                            <p>
                                                <asp:Label ID="lblDescComentario" runat="server" Text="Label"></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <!-- Fim Comentários -->

                        <!-- Comentar -->
                        <div class="respond_box">
                            <h2>Faça um comentário!</h2>
                            <p class="comment-form-comment">
                                <!-- TextBox para receber o comentário -->
                                <asp:TextBox ID="txtComentar" runat="server" MaxLength="300" Height="40" TextMode="MultiLine" Style="width: 97%;"></asp:TextBox>
                            </p>
                            <p class="form-submit">
                                <!-- Botão para enviar o comentário -->
                                <asp:Button ID="btnComentar" runat="server" Text="Comentar" CssClass="submit" OnClick="btnComentar_OnClick" />
                            </p>
                        </div>
                        <!-- Fim Comentar -->

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            </div>
            <div class="clear"></div>
            <!-- estabelecimnrtos -->
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
            
            <div class="clear"></div>
            
        </section>
    </section>
    <!--Container End-->

</asp:Content>
