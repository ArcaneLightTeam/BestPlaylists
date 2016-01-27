<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YouTubePreview.ascx.cs" Inherits="BestPlaylists.WebForms.UserControls.YouTubePreview" %>

<asp:UpdatePanel ID="updatePanelVideo" runat="server" UpdateMode="Conditional"  ChildrenAsTriggers="true">
    <ContentTemplate>
        <iframe  id="videoFramePreview" visible="false" runat="server"> 
        <button class="btn btn-danger">
            X
        </button>
        </iframe>
    </ContentTemplate>
</asp:UpdatePanel>