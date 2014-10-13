<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="VerPrato.aspx.cs" Inherits="BuscaRango.VerPrato" %>

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
                    <asp:Label ID="lblNome" runat="server" Text=""></asp:Label>
                </h3>
                <div class="text-container">
                    <p>
                        <asp:Label ID="lblPreco" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:HyperLink ID="hplEstab" runat="server"></asp:HyperLink>
                        <br />
                        <asp:Label ID="lblDesc" runat="server" Text=""></asp:Label>
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
            </div>
            <div class="clear"></div>
        </section>
    </section>
    <!--Container End-->
</asp:Content>
