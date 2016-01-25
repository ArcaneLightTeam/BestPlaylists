<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnauthorizedAccess.aspx.cs" Inherits="BestPlaylists.WebForms.UnauthorizedAccess" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br/>
    <div class="container text-center">
        <div class="panel panel-danger">
            <div class="panel-heading">
                <h3 class="panel-title">Unauthorized Access</h3>
            </div>
            <div class="panel-body">
                <h1 class="text-danger">Unauthorized Access!</h1>
                <h3>You are not allowed to access the specified resource</h3>
            </div>
        </div>
    </div>
</asp:Content>
