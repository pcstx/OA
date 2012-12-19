<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edition.aspx.cs" Inherits="GOA.Basic.Edition" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:CheckBox ID="ckEdition"  runat="server"/> 是否进行版本控制
    <br />
    版本名:<asp:TextBox  ID="txEdition" runat="server"></asp:TextBox>
    <br />
    <asp:Button  ID="EditionBtn" runat="server" Text="确定" OnClick="EditionBtn_Click"  />
    <br />
    <asp:Label ID="ShowStr" runat="server" ></asp:Label>
    </div>
    </form>
</body>
</html>
