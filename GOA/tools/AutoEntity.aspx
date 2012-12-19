<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoEntity.aspx.cs" Inherits="GPRPWeb.tools.AutoEntity" validateRequest="false"%>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:Label ID="Label1" runat="server" Text="表名"></asp:Label><cc1:TextBox ID="TextBox1" runat="server" CanBeNull="必填"></cc1:TextBox>
    
    <cc1:Button ID="Button1" runat="server" Text="Button_Entity" ShowPostDiv="false" AutoPostBack="true" ButtontypeMode="Normal" OnClick="hideModalPopupViaServer_Click" Page_ClientValidate="true"  />
    <cc1:Button ID="Button2" runat="server" Text="Button_AddEntity" ShowPostDiv="false" AutoPostBack="true" ButtontypeMode="Normal" OnClick="Button_AddEntity_Click" Page_ClientValidate="true"  />
    <cc1:Button ID="Button3" runat="server" Text="Button_UpdateEntity" ShowPostDiv="false" AutoPostBack="true" ButtontypeMode="Normal" OnClick="Button_UpdateEntity_Click" Page_ClientValidate="true"  />
    <cc1:Button ID="Button4" runat="server" Text="Button_GetEntity" ShowPostDiv="false" AutoPostBack="true" ButtontypeMode="Normal" OnClick="Button_GetEntity_Click" Page_ClientValidate="true"  />
    <cc1:Button ID="Button5" runat="server" Text="Button_aspx" ShowPostDiv="false" AutoPostBack="true" ButtontypeMode="Normal" OnClick="Button_aspx_Click" Page_ClientValidate="true"  />
    <cc1:Button ID="Button6" runat="server" Text="Button_aspxcs" ShowPostDiv="false" AutoPostBack="true" ButtontypeMode="Normal" OnClick="Button_aspxcs_Click" Page_ClientValidate="true"  />

<br />

    <cc1:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Rows="80" Cols="100" Height="400"></cc1:TextBox>
    </div>
    </form>
</body>
</html>
