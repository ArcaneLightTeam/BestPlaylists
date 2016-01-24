<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="Show.aspx.cs"
    Inherits="BestPlaylists.WebForms.Playlists.Show"
    MasterPageFile="~/Site.Master"
    Title="All Playlists" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="playlists">
        <h1 class="title">All Playlists</h1>

        <div class="row">
            <div class="form-group col-md-12">
                <label for="">Select Category</label>
                <asp:DropDownList runat="server"
                    ID="ddlCategory"
                    DataTextField="Name"
                    DataValueField="Id"
                    OnSelectedIndexChanged="CategoryChanged"
                    AutoPostBack="True"
                    AppendDataBoundItems="True" CssClass="form-control">
                    <asp:ListItem Text="All Categories" Value="-1" />
                </asp:DropDownList>
            </div>
        </div>

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
                <asp:TemplateField HeaderText="Title" SortExpression="Title">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btnDetails" on>
                            <%#: Item.Title.Length > 30 ? Item.Title.Substring(0,30) + "..." : Item.Title %>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <%#: Item.Description.Length > 30 ? Item.Description.Substring(0, 30) + "..." : Item.Description %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <%#: Item.Category.Name %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Creation Data" SortExpression="CreationDate">
                    <ItemTemplate>
                        <%#: Item.CreationDate.ToString("d") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="Rating" DataField="CurrentRating" SortExpression="CurrentRating" />
            </Columns>

        </asp:GridView>
    </div>
</asp:Content>

