<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoctorInfoWebForm.aspx.cs" Inherits="DoctorServer.DoctorInfoWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="docInfo" class="middle">
            <p><asp:PlaceHolder ID="graphPlaceHolder" runat="server" /></p>
            <p>用户名：<asp:Label ID="lbl_username" runat="server" /></p>
            <p>真实姓名：<asp:Label ID="lbl_realname" runat="server" /></p>
            <p>所属医院：<asp:Label ID="lbl_hospital" runat="server" /></p>
            <p>医院地址：<asp:Label ID="lbl_hospitalPos" runat="server" /></p>
        </div>
    </form>
</body>
</html>
