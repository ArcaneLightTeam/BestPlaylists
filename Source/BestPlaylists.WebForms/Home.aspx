<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BestPlaylists.WebForms.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2 class="text-center text-success">Top Playlists</h2>
        <asp:GridView AutoGenerateColumns="false" CssClass="table text-center table-borderer" ItemType="BestPlaylists.Data.Models.Playlist" ID="gridTopPLaylists" runat="server">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div class="text-center">Title</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Item.Title %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div class="text-center">Description</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Item.Description %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div class="text-center">Date</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Item.CreationDate.ToString("dd-MM-yyyy") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div class="text-center">Rating</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Item.Ratings.Average(c => c.Value).ToString("N2") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div class="text-center">Category</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Item.Category.Name %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div class="text-center">User</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Item.User.UserName %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="bg-success" />
        </asp:GridView>
    </div>
</asp:Content>
