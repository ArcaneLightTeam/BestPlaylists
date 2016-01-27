<%@ Page Title="Edit Playlist" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="BestPlaylists.WebForms.Playlists.Edit" %>

<%@ Register TagName="VideoPreview" TagPrefix="youtube" Src="~/UserControls/YouTubePreview.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="playlistNotFoundPanel" runat="server"
        Visible="False"
        EnableViewState="false">
        <br />
        <br />
        <br />
        <div class="alert alert-dismissible alert-danger">
            <p>
                Cannot find the playlist! You can check
                <a href="/Account/YourPlaylists" class="alert-link">your playlists</a>
                or return to 
               <a href="/" class="alert-link">Home</a>
            </p>
        </div>
    </asp:Panel>
    <asp:Panel ID="editPlaylistPanel" runat="server">
        <h1 class="title">Edit Playlist</h1>
        <div class="row">
            <div class="col-md-5">
                <div class="form-group">
                    <div class="pb-20 pt-10">
                        <asp:RequiredFieldValidator
                            runat="server"
                            ID="rfvTitel"
                            Display="Dynamic"
                            ErrorMessage="Title is required!"
                            ControlToValidate="tbTitle"
                            EnableClientScript="True"
                            CssClass="alert alert-danger" />

                        <asp:RegularExpressionValidator
                            runat="server"
                            ID="regxTitle"
                            Display="Dynamic"
                            ErrorMessage="Title must be between 2 and 250 symbols!"
                            ControlToValidate="tbTitle" EnableClientScript="True"
                            CssClass="alert alert-danger"
                            ValidationExpression=".{2,250}" />
                    </div>

                    <label for="MainContent_tbTitle">Title</label>
                    <asp:TextBox runat="server" ID="tbTitle" Mode="Encode" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <div class="pb-20 pt-10">
                        <asp:RequiredFieldValidator
                            runat="server"
                            ID="rfvDescription"
                            Display="Dynamic"
                            ErrorMessage="Description is required!"
                            ControlToValidate="tbDescription"
                            EnableClientScript="True"
                            CssClass="alert alert-danger" />
                    </div>

                    <label for="MainContent_tbDescription">Description</label>
                    <asp:TextBox runat="server" ID="tbDescription" Mode="Encode" TextMode="MultiLine"
                        Style="min-width: 100%; max-width: 100%; min-height: 100px; max-height: 150px"
                        CssClass="form-control" />
                </div>
                <div class="form-group">
                    <div class="pb-20 pt-10">
                        <asp:RequiredFieldValidator
                            runat="server"
                            ID="rfvCategory"
                            Display="Dynamic"
                            ErrorMessage="Category is required!"
                            ControlToValidate="ddlCategory"
                            EnableClientScript="True"
                            InitialValue="-1"
                            CssClass="alert alert-danger" />
                    </div>

                    <label for="MainContent_ddlCategory">Category</label>
                    <asp:DropDownList runat="server"
                        ID="ddlCategory"
                        DataTextField="Name"
                        DataValueField="Id"
                        AutoPostBack="False"
                        AppendDataBoundItems="True"
                        CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="checkbox">
                    <label for="MainContent_cbPrivate">
                        <asp:CheckBox runat="server" ID="cbPrivate" />
                        <strong>Is Private</strong>
                    </label>
                </div>
                <div class="pt-20">
                    <div class="btn-group col-sm-6">
                        <asp:Button runat="server" ID="deletePlaylist"
                            Text="Delete"
                            CssClass="btn btn-danger"
                            OnClick="DeletePlaylist_Click" />
                        <asp:Button ID="btnAddPlaylist" runat="server"
                            Text="Update"
                            OnClick="BtnUpdatePlaylist_Click"
                            CssClass="btn btn-primary" />
                    </div>
                </div>
            </div>

            <div class="col-md-3 col-md-offset-2">
                <h3 class="text-primary">Videos
                    <span id="videoCount" runat="server" class="badge badge-primary"></span>
                </h3>
                <div class="row panel panel-primary videos-container">
                    <asp:Repeater ItemType="BestPlaylists.Data.Models.Video" ID="videosRepeater" runat="server">
                        <ItemTemplate>
                            <div class="row">
                                <iframe width="190" height="90" class="pull-left"
                                    src=' <%#"https://www.youtube.com/embed/" + Item.Url.Substring(Item.Url.IndexOf("=") + 1) %>'>s
                                </iframe>
                                <asp:Button runat="server"
                                    Text="X"
                                    ID="deleteButton"
                                    CommandArgument="<%# Item.Id %>"
                                    Title="Video will be deleted"
                                    OnCommand="DeleteVideo_Command"
                                    CssClass="btn btn-danger pull-left" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <br />
                <div>
                    <div class="form-group">
                        <label class="label-control" for="tbAddVideo">Add video</label>
                        <youtube:VideoPreview runat="server" ID="ytPreview"
                            Side="Left"
                            EventName="Click"
                            AssociatedControlId="btnPreview" />
                        <asp:UpdatePanel ID="updateButtonText" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnAddVideo" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="input-group">
                                    <asp:TextBox ID="tbAddVideo" runat="server"
                                        Placeholder="https://www.youtube.com/watch?v=Ebbkkfp-YBk"
                                        CssClass="form-control search-query"
                                        Width="300" />
                                    <span class="input-group-btn">
                                        <asp:Button Text="Add" ID="btnAddVideo" CssClass="btn btn-success" runat="server" OnClick="AddVideo_Click" />
                                        <asp:Button Text="Preview" ID="btnPreview" runat="server"
                                            Title="Preview your link"
                                            OnClick="PreviewVideo_Click"
                                            CssClass="btn btn-default" />
                                    </span>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <asp:RegularExpressionValidator ID="revYouTubeUrl"
                        ControlToValidate="tbAddVideo"
                        CssClass="text-danger"
                        Width="300"
                        ValidationExpression="(https?:\/\/.*?youtube\.com)\/watch\?v=(.*)"
                        ErrorMessage="Url Should match https://www.youtube.com/watch?v="
                        runat="server" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
