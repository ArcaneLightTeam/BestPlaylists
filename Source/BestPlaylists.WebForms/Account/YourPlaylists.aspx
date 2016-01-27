<%@ Page Title="Your Playlists" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="YourPlaylists.aspx.cs" Inherits="BestPlaylists.WebForms.Account.YourPlaylists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="playlists">
        <h1 class="title">Your Playlists</h1>

        <div class="row">
            <div class="form-group col-md-12">
                <label for="">Select Category</label>
                <asp:DropDownList runat="server"
                    ID="ddlCategory"
                    DataTextField="Name"
                    DataValueField="Id"
                    OnSelectedIndexChanged="CategoryChanged"
                    AutoPostBack="true"
                    AppendDataBoundItems="True"
                    CssClass="form-control">
                    <asp:ListItem Text="All Categories" Value="-1" />
                </asp:DropDownList>
            </div>
        </div>
        <asp:UpdatePanel UpdateMode="Conditional" ID="updatePanel" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlCategory" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <asp:GridView runat="server" ID="gvUserPlayLists"
                    ItemType="BestPlaylists.Data.Models.Playlist"
                    DataKeyNames="Id"
                    AllowSorting="True"
                    EnableViewState="False"
                    AllowPaging="True" PageSize="10"
                    AutoGenerateColumns="False"
                    CssClass="table table-bordered"
                    OnSorting="gvUserPlayLists_Sorting" OnPageIndexChanging="gvUserPlayLists_PageIndexChanging">
                    <%--<SortedAscendingCellStyle CssClass="ascending" />--%>
                    <PagerStyle CssClass="paging" />
                    <EmptyDataTemplate>
                        <div class="alert alert-dismissible">
                            <p>
                                You do not have any playlists yet. You can create one on the following
                                <a href="/Playlists/Create" class="alert-link">page</a>.
                            </p>
                        </div>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Title" SortExpression="Title">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server"
                                    NavigateUrl='<%# "/Playlists/Details?Id=" + Item.Id %>'>
                            <%#: Item.Title.Length > 30 ? Item.Title.Substring(0, 30) + "..." : Item.Title %>     
                                </asp:HyperLink>
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
                                <%#: Item.CreationDate.ToString("dd-MM-yyyy") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Rating" DataField="CurrentRating" DataFormatString="{0:F2}" SortExpression="CurrentRating" />

                        <asp:TemplateField HeaderText="More">
                            <ItemTemplate>
                                <asp:HyperLink CssClass="btn btn-default col-sm-6" ID="HyperLink1" runat="server"
                                    NavigateUrl='<%# "/Playlists/Details?Id=" +  Item.Id %>'>
                                    Details     
                                </asp:HyperLink>

                                <asp:HyperLink CssClass="btn btn-primary col-sm-6" ID="HyperLink2" runat="server"
                                    NavigateUrl='<%# "/Playlists/Edit?Id=" +  Item.Id %>'>
                                    Edit     
                                </asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
