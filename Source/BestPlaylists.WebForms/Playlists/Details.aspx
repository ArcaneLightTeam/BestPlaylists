<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="Details.aspx.cs"
    Inherits="BestPlaylists.WebForms.Playlists.Details"
    MasterPageFile="~/Site.Master"
    Title="Details"
    ValidateRequest="false" %>

<%@ Register Src="~/UserControls/RatingControl/RatingControl.ascx" TagPrefix="userControls" TagName="RatingControl" %>


<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="row">
        <h1 class="title">Playlist details:</h1>
        <div class="pull-left">
            <a runat="server" id="btnEdit" href=' <%# "Edit?id=" + this.Request.Params["id"] %>' class="btn btn-primary">Edit Playlist</a>
        </div>
    </div>
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
        <h2 class="pl-20">Videos <span runat="server" id="videoCount"></span>:</h2>
        <div class="col-md-6 videoPlayer">
            <asp:Repeater runat="server" ID="repeaterVideos" ItemType="BestPlaylists.Data.Models.Video">
                <ItemTemplate>
                    <div class="text-center">
                        <p>
                            <a href='<%#: "https://www.youtube.com/watch?v=" + Item.Url %>' target="_blank" class="btn btn-default">See in YouTube</a>
                        </p>
                        <iframe width="400" height="320" src='<%#: "https://www.youtube.com/embed/" + Item.Url %>' runat="server" frameborder="0" allowfullscreen></iframe>
                    </div>
                </ItemTemplate>
                <SeparatorTemplate>
                    <hr />
                </SeparatorTemplate>
            </asp:Repeater>
        </div>
        <div class="col-md-6">
            <userControls:RatingControl ID="RatingControlPanel1" OnRate="RatingControlPanel1_OnRate" runat="server" CanRate="<%#this.CanRate %>" UserId="<%#this.GetUserId()%>" DataId="<%#this.GetDataId() %>"></userControls:RatingControl>
        </div>
    </div>
    <div class="row">
        <h2 class="pl-10">Comments <span runat="server" id="commentsCount"></span>: </h2>
        <asp:Repeater runat="server" ID="plComments"
            ItemType="BestPlaylists.Data.Models.Comment">
            <ItemTemplate>
                <div class="row mt-15 mb-15">
                    <div class="col-md-1 col-md-offset-1">
                        <%#: Item.CreationDate.ToShortDateString() %>
                    by <%#: Item.User.UserName %>
                    </div>
                    <div class="col-md-6">
                        <%#: Item.Text %>
                    </div>
                </div>
            </ItemTemplate>
            <SeparatorTemplate>
                <hr />
            </SeparatorTemplate>
        </asp:Repeater>
    </div>
    <div class="row" runat="server" id="postComment">
        <div class="col-md-12 form-group">

            <div class="mt-15 mb-15">
                <asp:RequiredFieldValidator
                    runat="server"
                    ID="rfvVideo"
                    Display="Dynamic"
                    ErrorMessage="Comment is required!"
                    ControlToValidate="tbUserComment"
                    EnableClientScript="True"
                    CssClass="alert alert-danger" />
            </div>

            <label for="MainContent_tbUserComment">Post your comment: </label>
            <asp:TextBox runat="server" TextMode="MultiLine" Mode="Encode" ID="tbUserComment" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-12">
            <asp:Button runat="server" CssClass="btn btn-success" Text="Add Comment" OnClick="AddComment_Click" />
        </div>
    </div>
</asp:Content>