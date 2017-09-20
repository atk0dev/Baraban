<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageGrid.ascx.cs" Inherits="ShowcaseED.Controls.ImageGrid" %>
<%@ Import Namespace="ShowcaseED.Model" %>

<div>
            <asp:Panel ID="pnlEdit" runat="server">
                <asp:DropDownList ID="ddlGroup" runat="server" DataValueField="Id" DataTextField="Name">                
                </asp:DropDownList> 
            
                <asp:Button ID="btnSave" runat="server" OnClick="BtnSave_Click" Text="Save" />
            </asp:Panel>
</div>

<asp:ListView runat="server" ID="ImageListView" ItemPlaceholderID="itemPlaceHolder" 
     GroupPlaceholderID="groupPlaceHolder" OnItemCommand="ImageListView_ItemCommand">
    <LayoutTemplate>
        <h1>
            <asp:Label Text="" runat="server" ID="titleLabel" OnLoad="TitleLabel_Load" />
        </h1>
        <div runat="server" id="groupPlaceHolder">
        </div>
    </LayoutTemplate>
    <GroupTemplate>
        <span>
            <div id="itemPlaceHolder" runat="server"></div>
        </span>
    </GroupTemplate>
    <ItemTemplate>
        
        <div style="border-width: thin; border-style: solid; background-color: #C0C0C0; width: auto">
            <asp:ImageButton ID="itemImageButton" runat="server" CommandName="SelectItem"
                CommandArgument="<%# (Container.DataItem as EditImage).Id %>"
                ImageUrl="<%# (Container.DataItem as EditImage).Url %>" Width="160" Height="120"/>

            <asp:LinkButton ID="deleteLinkButton" runat="server" CommandName="Remove"
                CommandArgument="<%# (Container.DataItem as EditImage).Id %>" Text="Delete" Visible="true" />
            
            <asp:LinkButton ID="updateLinkButton" runat="server" CommandName="Update"
                CommandArgument="<%# (Container.DataItem as EditImage).Id %>" Text="Update" Visible="true" />

            Group:
            <asp:Label runat="server" Text="<%# (Container.DataItem as EditImage).GroupId %>"></asp:Label>
        </div>
    
    </ItemTemplate>
    <EmptyItemTemplate>
        <td />
    </EmptyItemTemplate>
    <EmptyDataTemplate>
        <h3>No images available</h3>
    </EmptyDataTemplate>
    <InsertItemTemplate>
        <p>
            <asp:Label Text="Please upload an image" runat="server" ID="imageUploadLabel" />
            <asp:FileUpload runat="server" ID="imageUpload" OnLoad="ImageUpload_Load" />
            <asp:Button ID="uploadButton" Text="Upload" runat="server" />
        </p>
        <p>
            <asp:Label Text="" runat="server" ID="imageUploadStatusLabel" />
        </p>
    </InsertItemTemplate>
</asp:ListView>