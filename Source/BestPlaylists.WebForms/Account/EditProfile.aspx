<%@ Page Title="Edit Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="BestPlaylists.WebForms.Account.EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="text-center">Edit profile:</h3>
    <div class="row">
        <div class="col-md-6 col-md-offset-3">
            <asp:DetailsView runat="server" ID="details"
                AutoGenerateEditButton="false"
                DefaultMode="ReadOnly"
                EnableViewState="false"
                GridLines="None"
                CssClass="table"
                BorderStyle="None"
                BorderColor="Transparent"
                ItemType="BestPlaylists.Data.Models.User"
                AutoGenerateRows="false"
                DataKeyNames="Id">
                
                <Fields>
                    <%-- First Name --%>
                    <asp:TemplateField HeaderStyle-BorderWidth="0" ItemStyle-BorderWidth="0" HeaderStyle-VerticalAlign="Middle">
                        <HeaderTemplate>
                            <label class="control-label">
                                First Name:
                            </label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server"
                                ID="tbFirstName"
                                CssClass="form-control"
                                Text="<%# Item.FirstName %>" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- Last Name --%>
                    <asp:TemplateField HeaderStyle-BorderWidth="0" ItemStyle-BorderWidth="0" HeaderStyle-VerticalAlign="Middle">
                        <HeaderTemplate>
                            <label class="control-label">
                                First Name:
                            </label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server"
                                ID="tbLastName"
                                CssClass="form-control"
                                Text="<%# Item.LastName %>" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- Email --%>
                    <asp:TemplateField HeaderStyle-BorderWidth="0" ItemStyle-BorderWidth="0" HeaderStyle-VerticalAlign="Middle">
                        <HeaderTemplate>
                            <label class="control-label">
                                Email:
                            </label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server"
                                ID="tbEmail"
                                CssClass="form-control"
                                Type="Email"
                                Text="<%# Item.Email %>" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- Youtube --%>
                    <asp:TemplateField HeaderStyle-BorderWidth="0" ItemStyle-BorderWidth="0" HeaderStyle-VerticalAlign="Middle">
                        <HeaderTemplate>
                            <label class="control-label">
                                YOutube profile:
                            </label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server"
                                ID="tbYouTube"
                                CssClass="form-control"
                                Text="<%# Item.YouTubeAccount %>" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- FaceBook --%>
                    <asp:TemplateField HeaderStyle-BorderWidth="0" ItemStyle-BorderWidth="0" HeaderStyle-VerticalAlign="Middle">
                        <HeaderTemplate>
                            <label class="control-label">
                                Facebook profile:
                            </label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server"
                                ID="tbFacebook"
                                CssClass="form-control"
                                Text="<%# Item.FacebookAccount %>" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- Avatar --%>
                    <asp:TemplateField HeaderStyle-BorderWidth="0" ItemStyle-BorderWidth="0" HeaderStyle-VerticalAlign="Middle">
                        <HeaderTemplate>
                            <label class="control-label">
                                Avatar:
                            </label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox runat="server"
                                ID="tbAvatar"
                                CssClass="form-control"
                                Text="<%# Item.AvatarUrl %>" />
                            <asp:FileUpload ID="fileAvatar" accept=".png, .jpg, .jpeg, .gif" runat="server" AllowMultiple="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Fields>
            </asp:DetailsView>


            <asp:Panel ID="editBtns"  runat="server">
                <asp:Button Text="Update" runat="server" CssClass="btn btn-warning" OnClick="UpdateUser_Click"/>
                <asp:LinkButton PostBackUrl="~/Account/Manage.aspx" Text="Cancel" runat="server" CssClass="btn btn-default" />
            </asp:Panel>

        </div>
    </div>
</asp:Content>
