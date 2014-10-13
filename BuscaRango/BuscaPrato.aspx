<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="BuscaPrato.aspx.cs" Inherits="BuscaRango.BuscaPrato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>Busca Rango | Busca de Pratos</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphConteudo" runat="server">

    <asp:UpdatePanel ID="upBusca" runat="server">
        <ContentTemplate>

            <!--Container Start-->
            <section id="container-fluid">
                <section id="container">
                    <!--Busca Simples-->
                    <div>
                        <asp:TextBox ID="txtBusca" runat="server" placeholder="Buscar Prato"></asp:TextBox>
                        <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_OnClick" Text="Buscar" />
                        <asp:Button ID="btnFiltrar" runat="server" Text="+ Filtros" />
                    </div>
                    <!--Busca Avançada-->
                    <div>
                        <br />
                        <asp:Label ID="lblNome" runat="server" Text="Nome: "></asp:Label>
                        <asp:TextBox ID="txtBuscaNome" runat="server" placeholder="Digite aqui o nome do prato"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblDescricao" runat="server" Text="Descrição: "></asp:Label>
                        <asp:TextBox ID="txtBuscaDescricao" runat="server" placeholder="Digite aqui algum termo que descreva o prato"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblPrecoDe" runat="server" Text="Preço de R$: "></asp:Label>
                        <asp:TextBox ID="txtPrecoDe" runat="server" placeholder="1,00"></asp:TextBox>
                        <asp:Label ID="lblPrecoAte" runat="server" Text="Até R$: "></asp:Label>
                        <asp:TextBox ID="txtPrecoAte" runat="server" placeholder="100,00"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="lblEntrega" runat="server" Text="Possui tele-entrega: "></asp:Label>
                        <asp:CheckBox ID="chkEntrega" runat="server" />
                        <br />
                        <asp:Label ID="lblTags" runat="server" Text="Tags: "></asp:Label>
                        <asp:TextBox ID="txtTags" runat="server" placeholder="Digite aqui algumas palavras-chave"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblCaracteristicas" runat="server" Text="Características: "></asp:Label>
                        <asp:DropDownList ID="ddlCaracteristicas" runat="server"></asp:DropDownList>
                        <br />
                        <asp:Label ID="lblAvaliacao" runat="server" Text="Avaliação: "></asp:Label>
                        <asp:DropDownList ID="ddlAvaliacao" runat="server"></asp:DropDownList>
                        <br />
                        <asp:Button ID="btnBuscaAvancada" runat="server" OnClick="btnBuscaAvancada_OnClick" Text="Busca Avançada" />
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
    <!--<Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnBuscaAvancada" EventName="Click" />
        </Triggers>-->
</asp:Content>
