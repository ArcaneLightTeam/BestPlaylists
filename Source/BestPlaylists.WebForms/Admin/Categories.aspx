<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="BestPlaylists.WebForms.Admin.Categories" MasterPageFile="~/Site.Master"
    Title="Categories" %>

<asp:Content ID="ContentBody" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2 class="text-center">Categories</h2>
    <asp:ListView runat="server" ID="ListViewCategories" ItemType="BestPlaylists.Data.Models.Category" SelectMethod="ListViewCategories_GetData" DeleteMethod="ListViewCategories_DeleteItem" UpdateMethod="ListViewCategories_UpdateItem" DataKeyNames="ID" InsertMethod="ListViewCategories_InsertItem" InsertItemPosition="LastItem">
        <LayoutTemplate>
            <table class="table table-bordered table-striped table-hover" id="MainContent_GridViewCategories">
               <tbody>
                    <tr>
                        <th class="text-center col-lg-10">
                            <asp:LinkButton Text="Caterory Name" runat="server" ID="LinkButtonSortByCategory" CommandName="Sort" CommandArgument="Name" />
                        </th>
                        <th class="text-center col-md-2">Action</th>
                    </tr>
                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                </tbody>
            </table>
            <div class="text-center">
                <asp:DataPager runat="server" ID="DataPagerCategories" PagedControlID="ListViewCategories" PageSize="5">
                    <Fields>
                        <asp:NumericPagerField NumericButtonCssClass="btn btn-sm btn-primary" CurrentPageLabelCssClass="btn btn-sm btn-primary active" PreviousPageText="<<" NextPageText=">>" NextPreviousButtonCssClass="btn btn-sm btn-primary" />
                    </Fields>
                </asp:DataPager>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td><%#: Item.Name %></td>
                <td class="text-center">
                    <asp:LinkButton runat="server" ID="LinkButtonEdit" Text="Edit" CommandName="Edit" CssClass="btn btn-primary"/>
                    <asp:LinkButton runat="server" ID="LinkButtonDelete" Text="Delete" CommandName="Delete" CssClass="btn btn-danger"/>
                </td>
            </tr>
        </ItemTemplate>
        <EditItemTemplate>
            <tr>
                <td>
                    <asp:TextBox runat="server" ID="TextBoxName" Text="<%#: BindItem.Name %>" CssClass="col-md-12"/>
                </td>
                <td class="text-center">
                    <asp:LinkButton runat="server" ID="LinkButton1" Text="Save" CommandName="Update" CssClass="btn btn-success"/>
                    <asp:LinkButton runat="server" ID="LinkButton2" Text="Cancel" CommandName="Cancel" CssClass="btn btn-primary"/>
                </td>
            </tr>
        </EditItemTemplate>
        <InsertItemTemplate>
            <tr>
                <td>
                    <asp:TextBox runat="server" ID="TextBox1" Text="<%#: BindItem.Name %>" CssClass="col-md-12"/>
                </td>
                <td class="text-center"> 
                    <asp:LinkButton runat="server" ID="LinkButton3" Text="Insert" CommandName="Insert" CssClass="btn btn-success"/>
                    <asp:LinkButton runat="server" ID="LinkButton4" Text="Cancel" CommandName="Cancel" CssClass="btn btn-primary"/>
                </td>
            </tr>
        </InsertItemTemplate>
    </asp:ListView>
</asp:Content>
