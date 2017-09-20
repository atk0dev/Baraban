<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="ShowcaseED.Edit" %>

<%@ Register TagPrefix="cc" TagName="ImageControl" Src="~/Controls/ImageGrid.ascx" %>
<%@ Register Src="~/Controls/Login.ascx" TagPrefix="cc" TagName="Login" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <cc:Login runat="server" id="Login" />
        </div>
        <div>
            <cc:ImageControl runat="server" ID="MyImageGrid" Title="C# Wallpapers" AdminMode="true" />
        </div>
    </form>
</body>
</html>
