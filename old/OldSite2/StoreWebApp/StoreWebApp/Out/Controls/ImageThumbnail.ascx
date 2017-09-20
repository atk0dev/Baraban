<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageThumbnail.ascx.cs" Inherits="ShowcaseED.Controls.ImageThumbnail" %>

<a href="<%# this.FileUrlFullsize %>" class="img<%# this.Id %>" title="<%# this.Title %>"><img src="<%# this.FileUrlThumbnail %>" alt="image<%# this.Id %>"/></a>