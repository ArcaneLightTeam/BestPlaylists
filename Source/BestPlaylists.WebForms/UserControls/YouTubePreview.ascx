<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YouTubePreview.ascx.cs" Inherits="BestPlaylists.WebForms.UserControls.YouTubePreview" %>

<asp:UpdatePanel ID="updatePanelVideo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <div>
            <iframe id="videoFramePreview" visible="false" runat="server"></iframe>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
