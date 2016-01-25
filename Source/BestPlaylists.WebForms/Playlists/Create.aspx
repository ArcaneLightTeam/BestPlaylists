<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="Create.aspx.cs"
    Inherits="BestPlaylists.WebForms.Playlists.Create"
    MasterPageFile="~/Site.Master"
    Title="Create Playlist"
    ValidateRequest="false" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h1 class="title">Create Playlist</h1>
    <div class="row">
        <div class="col-md-12">
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
                        ValidationExpression="[\w+|\d+|\s+]{2,250}" />

                </div>

                <label for="MainContent_tbTitle">Title</label>
                <asp:TextBox runat="server" ID="tbTitle" Mode="Encode" TextMode="MultiLine" CssClass="form-control" />
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
                <asp:TextBox runat="server" ID="tbDescription" Mode="Encode" TextMode="MultiLine" CssClass="form-control" />
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
                    <asp:ListItem Text="Select Category" Value="-1" />
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <div class="pb-20 pt-10">
                    <asp:RequiredFieldValidator
                        runat="server"
                        ID="rfvVideo"
                        Display="Dynamic"
                        ErrorMessage="Video is required!"
                        ControlToValidate="tbVideo"
                        EnableClientScript="True"
                        CssClass="alert alert-danger" />
                </div>

                <label for="MainContent_tbVideo">Video <em>(separate with comma)</em></label>
                <asp:TextBox runat="server" ID="tbVideo" Mode="Encode" TextMode="MultiLine" CssClass="form-control" />
            </div>
            <div class="checkbox">
                <label for="MainContent_cbPrivate">
                    <asp:CheckBox runat="server" ID="cbPrivate" />
                    <strong>Is Private</strong>
                </label>
            </div>
            <div class="pt-20">
                <asp:Button ID="btnAddPlaylist" runat="server" Text="Add Playlist" OnClick="BtnAddPlaylist_Click" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>
</asp:Content>
