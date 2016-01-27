﻿<%@ Page Title="Manage Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="BestPlaylists.WebForms.Account.Manage" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>

    <div>
        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
            <p class="text-success"><%: SuccessMessage %></p>
        </asp:PlaceHolder>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <h4>Change your account settings</h4>
                <hr />
                <dl class="dl-horizontal">
                    <dt>Your playlists:</dt>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/YourPlaylists" Text="Playlists" CssClass="btn btn-default col-md-1" runat="server" />
                    </dd>
                    <br />

                    <dt>Password:</dt>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="Change" CssClass="btn btn-primary col-md-1" Visible="false" ID="ChangePassword" runat="server" />
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="Create" CssClass="btn btn-primary col-md-1" Visible="false" ID="CreatePassword" runat="server" />
                    </dd>
                    <br />
                    
                    <%--<dt>External Logins:</dt>
                    <dd><%: LoginsCount %>
                        <asp:HyperLink NavigateUrl="/Account/ManageLogins" Text="[Manage]" runat="server" />

                    </dd>--%>
                    
                    <dt>Edit profile:</dt>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/EditProfile"  CssClass="btn btn-primary col-md-1" Text="Edit" runat="server" />
                    </dd>
                    <%--
                        Phone Numbers can used as a second factor of verification in a two-factor authentication system.
                        See <a href="http://go.microsoft.com/fwlink/?LinkId=403804">this article</a>
                        for details on setting up this ASP.NET application to support two-factor authentication using SMS.
                        Uncomment the following blocks after you have set up two-factor authentication
                    --%>
                    <%--
                    <dt>Phone Number:</dt>
                    <% if (HasPhoneNumber)
                       { %>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/AddPhoneNumber" runat="server" Text="[Add]" />
                    </dd>
                    <% }
                       else
                       { %>
                    <dd>
                        <asp:Label Text="" ID="PhoneNumber" runat="server" />
                        <asp:HyperLink NavigateUrl="/Account/AddPhoneNumber" runat="server" Text="[Change]" /> &nbsp;|&nbsp;
                        <asp:LinkButton Text="[Remove]" OnClick="RemovePhone_Click" runat="server" />
                    </dd>
                    <% } %>
                    --%>

                    <%-- /<dt>Two-Factor Authentication:</dt>
                    <dd>
                        <p>
                            There are no two-factor authentication providers configured. See <a href="http://go.microsoft.com/fwlink/?LinkId=403804">this article</a>
                            for details on setting up this ASP.NET application to support two-factor authentication.
                        </p>
                        <% if (TwoFactorEnabled)
                          { %> 
                        <%--
                        Enabled
                        <asp:LinkButton Text="[Disable]" runat="server" CommandArgument="false" OnClick="TwoFactorDisable_Click" />
                        
                        <% }
                          else
                          { %> 
                        <%--
                        Disabled
                        <asp:LinkButton Text="[Enable]" CommandArgument="true" OnClick="TwoFactorEnable_Click" runat="server" />
                        
                        <% } %>
                    </dd>
                    --%>
                </dl>

                <div class="col-md-6">
                    <asp:DetailsView runat="server" ID="userDetails"
                        GridLines="None"
                        CssClass="table col-md-5"
                        AutoGenerateRows="false"
                        ItemType="BestPlaylists.Data.Models.User">
                        <Fields>
                            <asp:TemplateField  HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="150" HeaderStyle-BorderWidth="0" ItemStyle-BorderWidth="0" >
                                <HeaderTemplate>
                                   <strong>Profile Image:</strong>    
                                </HeaderTemplate>
                                <ItemTemplate >
                                    <img src="<%#: string.IsNullOrEmpty(Item.AvatarUrl)? "/Images/default.jpg": Item.AvatarUrl %>" Width="250" alt="Avatar" class="img-thumbnail" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-BorderWidth="0" ItemStyle-BorderWidth="0">
                                <HeaderTemplate>
                                    <strong>First name:</strong>        
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal runat="server" Mode="Encode" Text="<%# Item.FirstName %>" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right"  HeaderStyle-BorderWidth="0" ItemStyle-BorderWidth="0">
                                <HeaderTemplate>
                                    <strong>Last name:</strong>        
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal runat="server" Mode="Encode" Text="<%# Item.LastName %>" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-BorderWidth="0" ItemStyle-BorderWidth="0">
                                <HeaderTemplate>
                                    <strong>Email name:</strong>        
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal runat="server" Mode="Encode" Text="<%# Item.Email %>" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-BorderWidth="0" ItemStyle-BorderWidth="0">
                                <HeaderTemplate>
                                    <strong>Youtube account:</strong>        
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink NavigateUrl="<%#: Item.YouTubeAccount %>" runat="server" 
                                        Text='<%# string.IsNullOrEmpty(Item.YouTubeAccount)? "Not provided" : "Link" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderStyle-BorderWidth="0" ItemStyle-BorderWidth="0">
                                <HeaderTemplate>
                                    <strong>Facebook account:</strong>        
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink NavigateUrl="<%#: Item.FacebookAccount %>" runat="server"
                                         Text='<%# string.IsNullOrEmpty(Item.FacebookAccount)? "Not provided" : "Link" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Fields>
                    </asp:DetailsView>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
