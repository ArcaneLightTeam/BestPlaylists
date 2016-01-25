<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="Details.aspx.cs"
    Inherits="BestPlaylists.WebForms.Playlists.Details"
    MasterPageFile="~/Site.Master"
    Title="Details" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h1 class="title">Details</h1>
    <div class="row">
        <div class="col-md-6">
            <h2>Title:</h2>
            <div runat="server" id="plTitle"></div>
        </div>
        <div class="col-md-6">
            <h2>Description:</h2>
            <div runat="server" id="plDescription"></div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <asp:Repeater runat="server" ID="repeaterVideos"
                ItemType="BestPlaylists.Data.Models.Video">
                <HeaderTemplate>
                    <h2>Videos:</h2>
                </HeaderTemplate>
                <ItemTemplate>
                    <p>
                        <a href="<%#: Item.Url %>" target="_blank">Watch Video - <%#: Item.Playlist.Title %></a>
                    </p>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="col-md-6">
            <h2>Rating: <strong runat="server" id="plRating"></strong></h2>
            <asp:DropDownList runat="server" ID="Rating" 
                OnSelectedIndexChanged="Rating_OnSelectedIndexChanged" 
                AutoPostBack="True"
                AppendDataBoundItems="True"
                CssClass="form-control" />
        </div>
    </div>
    <div class="row">
        
    </div>
</asp:Content>
