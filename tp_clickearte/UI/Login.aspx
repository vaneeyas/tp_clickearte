<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UI.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>LOGIN</h1>
        <br />
        <asp:Label ID="lblMensajes" runat="server" Text=""></asp:Label>
        <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtContraseña" runat="server"></asp:TextBox>
        <asp:Button ID="btnLogin" runat="server" Text="Login" onclick="btnLogin_Click"/>
        <asp:Button ID="btnCrearUsuario" runat="server" Text="Crear Usuario" onclick="btnCrearUsuario_Click"/>
        <br />
    </form>
</body>
</html>
