<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RatingControl.ascx.cs" Inherits="BestPlaylists.WebForms.UserControls.RatingControl.RatingControl" %>

<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <h2>Rating: <strong runat="server" id="plRating"></strong></h2>
        <asp:DropDownList runat="server" ID="Rating"
            OnSelectedIndexChanged="Rating_OnSelectedIndexChanged"
            AutoPostBack="True"
            AppendDataBoundItems="True"
            CssClass="form-control"
            DataTextField="Name"
            DataValueField="Value" />
    </ContentTemplate>
</asp:UpdatePanel>