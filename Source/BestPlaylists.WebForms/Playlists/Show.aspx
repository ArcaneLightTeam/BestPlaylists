<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="Show.aspx.cs"
    Inherits="BestPlaylists.WebForms.Playlists.Show"
    MasterPageFile="~/Site.Master"
    Title="All Playlists" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="playlists">
        <h1 class="title">All Playlists</h1>

        <asp:GridView runat="server"
            ID="playlistsGrid"
            ItemType="BestPlaylists.Data.Models.Playlist"
            DataKeyNames="Id"
            SelectMethod="ListViewPlaylists_GetData"
            AllowSorting="True"
            AllowPaging="True" PageSize="10"
            AutoGenerateColumns="False"
            CssClass="table table-bordered">
            
            <PagerStyle CssClass="paging" />

            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        Title
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#: Item.Title.Length > 30 ? Item.Title.Substring(0,30) + "..." : Item.Title %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>
                        Description
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#: Item.Description.Length > 30 ? Item.Description.Substring(0, 30) + "..." : Item.Description %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField>
                    <HeaderTemplate>
                        Category
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#: Item.Category.Name %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>
                        Creation Data
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#: Item.CreationDate.ToString("d") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>
                        Rating
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#: Item.CurrentRating %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

