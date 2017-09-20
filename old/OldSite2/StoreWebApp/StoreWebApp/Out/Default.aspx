<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShowcaseED.Default" %>
<%@ Register src="Controls/GroupsLine.ascx" tagname="GroupsLine" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentTop" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
	<div class="gallery_container_top">
		<div class="gallery_top_header"></div>
			<div class="gallery_content_header">
				<asp:Image ID="imgTop" runat="server" ImageAlign="Top" ImageUrl="~/Content/img/top.jpg" />
			</div>
			<div class="gallery_bottom_header"></div>
	</div>
</asp:Content>

<asp:Content ID="ContentRight" ContentPlaceHolderID="ContentPlaceHolderRight" runat="server">
	<div class="gallery_container_right">
			
			<div class="gallery_top_right"></div>	
			<div class="gallery_content_right">
				<div class="right_urls">
					<h1><%=ShowcaseED.Res.Resources.Default_Catalog_Caption %></h1>
					<br/>
					<asp:PlaceHolder ID="catalogControl" runat="server"></asp:PlaceHolder>
				</div>
			</div>
			<div class="gallery_bottom_right"></div>
		</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		
		<asp:UpdatePanel ID="UpdatePanel1" runat="server">
						<ContentTemplate>
							
							<script type="text/javascript">
								var prm = Sys.WebForms.PageRequestManager.getInstance();
								prm.add_endRequest(function () {

									doAllMagic();

								});

							</script>

		<div class="gallery_container">
			<div class="gallery_credit"><h1>WinKAS</h1></div>
			<div class="gallery_type"><h2>// Images</h2></div>
			<div class="clear_both"></div>
			<div class="gallery_top"></div>
			<div class="gallery_content">
				
				
				<div class="gallery_thumbnails">
					<asp:PlaceHolder ID="galleryThumbnails1" runat="server"></asp:PlaceHolder>
				</div>

				<div class="gallery_preview">
				</div>

				<div class="clear_both"></div>
				<div class="gallery_contact">
					<p><a class="contactLink" href="mailto:at@winkas.dk">Contact Me</a></p>
					<uc1:GroupsLine ID="GroupsLine1" runat="server" />
				</div>
				<div class="gallery_caption"></div>
				<div class="clear_both"></div>
				<div class="gallery_preload_area"></div>
			</div>
			<div class="gallery_bottom"></div>
		</div>

						</ContentTemplate>
		</asp:UpdatePanel>  

	   
</asp:Content>
