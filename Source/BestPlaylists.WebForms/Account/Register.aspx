<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="BestPlaylists.WebForms.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <div class="form-horizontal">
        <h4>Create a new account</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group col-md-8">
            <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">Username</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="UserName" AutoPostBack="true" CssClass="form-control col-md-6" TextMode="SingleLine" OnTextChanged="UserName_TextChanged" />
                <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="UserName" EventName="TextChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <p class="help-block col-md-6" runat="server" visible="false" id="panelUserExist">
                            <asp:Image ImageUrl="imageurl" ID="Image1" Width="25" AlternateText="Image" runat="server" />
                            <asp:Label ID="labelUserExists" runat="server" />
                        </p>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                    CssClass="text-danger col-md-12" ErrorMessage="The username field is required." />
            </div>
        </div>
        <div class="form-group col-md-8">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="The email field is required." />
            </div>
        </div>
        <div class="form-group col-md-8">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="The password field is required." />
            </div>
        </div>
        <div class="form-group col-md-8">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Confirm password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
            </div>
        </div>
        <div class="form-group col-md-8">
            <asp:Label runat="server" AssociatedControlID="FirstName" CssClass="col-md-2 control-label">First name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="FirstName" CssClass="form-control" TextMode="SingleLine" />
                <br />
            </div>
        </div>
        <div class="form-group col-md-8">
            <asp:Label runat="server" AssociatedControlID="LastName" CssClass="col-md-2 control-label">Last name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="LastName" CssClass="form-control" TextMode="SingleLine" />
                <br />
            </div>
        </div>
        <div class="form-group col-md-8">
            <asp:Label runat="server" AssociatedControlID="FacebookAccount" CssClass="col-md-2 control-label">Facebook Account</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="FacebookAccount" CssClass="form-control" TextMode="SingleLine" />
                <br />
            </div>
        </div>
        <div class="form-group col-md-8">
            <asp:Label runat="server" AssociatedControlID="YouTubeAccount" CssClass="col-md-2 control-label">YouTube Account</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="YouTubeAccount" CssClass="form-control" TextMode="SingleLine" />
                <br />
            </div>
        </div>
        <div class="form-group col-md-8">
            <asp:Label runat="server" AssociatedControlID="AvatarUrl" CssClass="col-md-2 control-label">Avatar Url</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="AvatarUrl" CssClass="form-control" TextMode="SingleLine" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
