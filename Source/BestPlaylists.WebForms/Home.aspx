<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BestPlaylists.WebForms.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">

        <h2 class="text-center text-success">Top Playlists</h2>
        <asp:GridView AutoGenerateColumns="false" CssClass="table text-center table-borderer" ItemType="BestPlaylists.Data.Models.Playlist" ID="gridTopPLaylists" runat="server">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:BoundField DataField="Date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Date" />
                <asp:BoundField DataField="Rating" DataFormatString="{0:N2}" HeaderText="Rating" />
                <asp:BoundField DataField="Category" HeaderText="Category" />
                <asp:BoundField DataField="Username" HeaderText="User" />
            </Columns>
            <HeaderStyle CssClass="bg-success" />
        </asp:GridView>
    </div>
</asp:Content>
