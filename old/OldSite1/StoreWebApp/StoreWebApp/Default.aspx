<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StoreWebApp.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentTop" ContentPlaceHolderID="ContentPlaceHolderTop" runat="server">
	<div class="gallery_container_top">
	    <div class="gallery_top_header"></div>
			<div class="gallery_content_header">
				<div class="gallery_credit_header"><h1>Модная одежда в Харькове</h1></div>
			</div>
			<div class="gallery_bottom_header"></div>
    </div>
</asp:Content>

<asp:Content ID="ContentRight" ContentPlaceHolderID="ContentPlaceHolderRight" runat="server">
    <div class="gallery_container_right">
        	
            <div class="gallery_top_right"></div>	
            <div class="gallery_content_right">
                <div class="right_urls">
                <a href="http://google.com" title="Джинсы">Джинсы</a><br/>
                <a href="http://google.com" title="Джинсы">Джинсы</a><br/>
                <a href="http://google.com" title="Джинсы">Джинсы</a><br/>
                <a href="http://google.com" title="Джинсы">Джинсы</a><br/>
                <a href="http://google.com" title="Джинсы">Джинсы</a><br/>
                </div>
            </div>
			<div class="gallery_bottom_right"></div>
		</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<div class="gallery_container">
			<div class="gallery_credit"><h1>Chris Converse</h1></div>
			<div class="gallery_type"><h2>// Photography</h2></div>
			<div class="clear_both"></div>
			<div class="gallery_top"></div>
			<div class="gallery_content">
				<div class="gallery_thumbnails">
					<a href="Content/gallery/lantern_fullsize.jpg" title="Caption for lantern goes here"><img src="Content/gallery/lantern_thumbnail.jpg"/></a>
					<a href="Content/gallery/eighteighteight_fullsize.jpg" title="Caption for eight-eight-eight goes here"><img src="Content/gallery/eighteighteight_thumbnail.jpg"/></a>
					<div class="clear_both"></div>
				</div>
				<div class="gallery_preview">
					<a href="Content/gallery/acrobats_large.jpg"></a>
				</div>
				<div class="clear_both"></div>
				<div class="gallery_contact"><p><a class="contactLink" href="mailto:info@lynda.com">Contact Me</a></p></div>
				<div class="gallery_caption"></div>
				<div class="clear_both"></div>
				<div class="gallery_preload_area"></div>
			</div>
			<div class="gallery_bottom"></div>
		</div>
</asp:Content>
