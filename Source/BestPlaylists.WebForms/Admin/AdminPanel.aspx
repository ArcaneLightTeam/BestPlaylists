<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="BestPlaylists.WebForms.Admin.AdminPanel" MasterPageFile="~/Site.Master"
    Title="Admin Panel" %>

<asp:Content ID="ContentBody" ContentPlaceHolderID="MainContent" runat="server">
    <br/>
    <div class="container">
        <div class="col-md-offset-1 col-md-10">
            <img src="../Images/admin-zone.jpg" alt="admin-zone-picture" class="img-responsive" />
        </div>
    </div>
    <div class="container-fluid">
        <br/>
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/Categories.aspx" Text="Edit categories" CssClass="btn btn-lg btn-primary col-md-5 col-sm-5" />
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/Users.aspx" Text="Edit users" CssClass="btn btn-lg btn-primary col-md-offset-2 col-sm-offset-2 col-md-5 col-sm-5" />
    </div>
</asp:Content>
