<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormDemo._Default" %>

<html>
<head>
    <title></title>
    <link href="Scripts/validBox/validBox.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.8.2.min.js"></script>
    <script src="Scripts/validBox/validBox.js"></script>
</head>
<body>
    <form runat="server">
        <p>
            <label>账号</label>
            <asp:TextBox runat="server" ID="Account"></asp:TextBox>
        </p>

        <p>
            <label>密码</label>
            <asp:TextBox runat="server" ID="Password" TextMode="Password"></asp:TextBox>
        </p>
        <p>
            <label>邮箱</label>
            <asp:TextBox runat="server" ID="Email"></asp:TextBox>
        </p>

        <button type="submit">提交</button>
    </form>
</body>
</html>
