<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="BestPlaylists.WebForms.Admin.Users" MasterPageFile="~/Site.Master"
    Title="Users" %>

<asp:Content ID="ContentBody" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center">Users</h2>
    <asp:ListView runat="server" ID="usersView" ItemType="BestPlaylists.Data.Models.User" SelectMethod="UsersView_GetData" UpdateMethod="UsersView_Update" DataKeyNames="ID">
        <LayoutTemplate>
            <table class="table table-bordered table-striped table-hover table-responsive" id="MainContent_ViewUsers">
                <tbody>
                    <tr>
                        <th class="text-center">
                            <asp:LinkButton Text="Username" runat="server" ID="LinkButtonSortByUsername" CommandName="Sort" CommandArgument="Username" />
                        </th>
                        <th class="text-center">
                            <asp:LinkButton Text="Firstname" runat="server" ID="LinkButtonSortByFirstname" CommandName="Sort" CommandArgument="FirstName" />
                        </th>
                        <th class="text-center">
                            <asp:LinkButton Text="Firstname" runat="server" ID="LinkButtonSortByLastname" CommandName="Sort" CommandArgument="LastName" />
                        </th>
                        <th class="text-center">
                            <asp:LinkButton Text="E-mail" runat="server" ID="LinkButtonSortByLastEmail" CommandName="Sort" CommandArgument="Email" />
                        </th>
                        <th class="text-center">Action</th>
                    </tr>
                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                </tbody>
            </table>
            <div class="text-center">
                <asp:DataPager runat="server" ID="DataPagerUsers" PagedControlID="usersView" PageSize="5">
                    <Fields>
                        <asp:NumericPagerField NumericButtonCssClass="btn btn-sm btn-primary" CurrentPageLabelCssClass="btn btn-sm btn-primary active" PreviousPageText="<<" NextPageText=">>" NextPreviousButtonCssClass="btn btn-sm btn-primary" />
                    </Fields>
                </asp:DataPager>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <tr class="text-center">
                <td class="col-md-2"><%#: Item.UserName %></td>
                <td class="col-md-3"><%#: Item.FirstName %></td>
                <td class="col-md-3"><%#: Item.LastName %></td>
                <td class="col-md-2"><%#: Item.Email %></td>
                <td class="text-center col-md-2">
                    <asp:LinkButton runat="server" ID="LinkButtonEdit" Text="Edit" CommandName="Edit" CssClass="btn btn-primary" />
                </td>
            </tr>
        </ItemTemplate>
        <EditItemTemplate>
            <tr>
                <td class="col-md-2">
                    <asp:TextBox runat="server" ID="TextBoxName" Text="<%#: BindItem.UserName %>" CssClass="col-md-12" />
                </td>
                <td class="col-md-2">
                    <asp:TextBox runat="server" ID="TextBoxFirstName" Text="<%#: BindItem.FirstName %>" CssClass="col-md-12" />
                </td>
                <td class="col-md-2">
                    <asp:TextBox runat="server" ID="TextBoxLastName" Text="<%#: BindItem.LastName %>" CssClass="col-md-12" />
                </td>
                <td class="col-md-2">
                    <asp:TextBox runat="server" ID="TextBoxEmail" Text="<%#: BindItem.Email %>" CssClass="col-md-12" />
                </td>
                <td class="text-center">
                    <asp:LinkButton runat="server" ID="LinkButton1" Text="Save" CommandName="Update" CssClass="btn btn-success" />
                    <asp:LinkButton runat="server" ID="LinkButton2" Text="Cancel" CommandName="Cancel" CssClass="btn btn-primary" />
                </td>
            </tr>
        </EditItemTemplate>
    </asp:ListView>
</asp:Content>
