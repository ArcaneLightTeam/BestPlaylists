<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BestPlaylists.WebForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron text-center">
        <h1>Best playlists</h1>
        <p class="lead">Whether you are looking for great YouTube playlists or you want to share yours, you have come to the right place.</p>
    </div>
    <div class="jumbotron">
        <h2 class="text-center">Top Playlists</h2>
        <asp:GridView AutoGenerateColumns="false" CssClass="table text-center table-borderer table-responsive table-hover table-striped" ItemType="BestPlaylists.Data.Models.Playlist" ID="gridTopPLaylists" runat="server">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div class="text-center">Title</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#: Item.Title.Length > 30 ? Item.Title.Substring(0, 30) + "..." : Item.Title %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div class="text-center">Description</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#: Item.Description.Length > 30 ? Item.Description.Substring(0,30) + "..." : Item.Description %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div class="text-center">Date</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#: Item.CreationDate.ToString("dd-MM-yyyy") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div class="text-center">Rating</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#: Item.CurrentRating.ToString("N2") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div class="text-center">Category</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#: Item.Category.Name %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div class="text-center">Creator</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#: Item.User.UserName %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div class="text-center">Action</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1"
                            runat="server" CssClass="btn btn-primary" Text="View"
                            NavigateUrl='<%# "Playlists/Details?Id=" + Item.Id %>'>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="table-heading" />
        </asp:GridView>
    </div>
</asp:Content>
