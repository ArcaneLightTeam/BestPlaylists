<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="BestPlaylists.WebForms.Admin.AdminPanel" MasterPageFile="~/Site.Master"
    Title="Admin Panel" %>

<asp:Content ID="ContentBody" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <h1 class="text-center">Admin panel</h1>
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/Categories.aspx" Text="Edit categories" CssClass="btn btn-lg btn-primary col-md-5 col-sm-5" />
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/Users.aspx" Text="Edit useres" CssClass="btn btn-lg btn-primary col-md-offset-2 col-sm-offset-2 col-md-5 col-sm-5" />
    </div>
</asp:Content>