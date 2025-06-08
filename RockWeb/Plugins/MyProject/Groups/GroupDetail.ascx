<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupDetail.ascx.cs" Inherits="RockWeb.Plugins.MyProject.Groups.GroupDetail" %>

<asp:UpdatePanel ID="upnlContent" runat="server">
    <ContentTemplate>

        <asp:Panel ID="pnlDetails" runat="server" CssClass="panel panel-block">
        
            <div class="panel-heading">
                <h1 class="panel-title">
                    <i class="fa fa-users"></i> 
                    Group Details
                </h1>

             </div>

            <div class="panel-body">
                <asp:Literal ID="lGroupDetails" runat="server" />
            </div>
        
        </asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>