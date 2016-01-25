<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="Details.aspx.cs"
    Inherits="BestPlaylists.WebForms.Playlists.Details"
    MasterPageFile="~/Site.Master"
    Title="Details"
    ValidateRequest="false" %>

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
            <asp:Button runat="server" CssClass="btn btn-primary" Text="Add Comment" OnClick="AddComment_Click" />
        </div>
    </div>
</asp:Content>
