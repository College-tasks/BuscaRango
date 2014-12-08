<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BuscaRango.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>Busca Rango | Home</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphConteudo" runat="server">
    <!--Container Start-->
    <section id="container">

        <!--Drop Cap Start-->
        <asp:Panel ID="pnlSemLogin" runat="server" CssClass="dropcap" Visible="false">
            <div class="one-third">
                <asp:HyperLink ID="linkEstab" runat="server" NavigateUrl="~/Lugar"><i class="fa fa-cutlery"></i></asp:HyperLink>
                <h4>Estabelecimentos</h4>
            </div>
            <div class="one-third-last">
                <asp:HyperLink ID="linkPrato" runat="server" NavigateUrl="~/Prato"><i class="fa fa-glass"></i></asp:HyperLink>
                <h4>Pratos</h4>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlLogin" runat="server" CssClass="meio" Visible="false">
            <div class="usuario">
                <asp:Label ID="lblUser" runat="server" Text=""></asp:Label>
            </div>
            <div class="pratos">
                <asp:Label ID="lblPratos" runat="server" Text=""></asp:Label>
            </div>
            <div class="lugares">
                <asp:Label ID="lblLugares" runat="server" Text=""></asp:Label>
            </div>

            <asp:UpdatePanel ID="upHistorico" runat="server">
                <ContentTemplate>
                    <asp:Repeater ID="rptHistorico" runat="server" OnItemDataBound="rptHistorico_ItemDataBound">
                        <ItemTemplate>
                            <div class="item-historico">
                                <asp:Image ID="imgHistorico" runat="server" CssClass="item-img" />
                                <div class="texto">
                                    <asp:Label ID="lblNome" runat="server" Text=""></asp:Label>
                                </div>
                                <br />
                                <div class="texto">
                                    <asp:Label ID="lblCarac" runat="server" Text=""></asp:Label>
                                </div>
                                <br />
                                <div class="texto">
                                    <asp:Label ID="lblNota" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="wrapper-btn-historico" runat="server" ID="wrapperBtnHistorico">
                        <asp:Button ID="btnHistorico" runat="server" Text="Ver Histórico Completo" CssClass="btn-historico" OnClick="btnHistorico_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <!--Drop Cap End-->

    </section>
    <!--Container End-->
</asp:Content>
