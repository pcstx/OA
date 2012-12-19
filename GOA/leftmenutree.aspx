<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="leftmenutree.aspx.cs" Inherits="GOA.leftmenutree" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>左边树型控件</title>
</head>
<body>
  <form id="form1" runat="server">
  <div>
    <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="1" ShowLines="true" OnTreeNodePopulate="treeMenu_TreeNodePopulate">
      <ParentNodeStyle Font-Bold="False" />
      <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
      <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
      <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
    </asp:TreeView>
  </div>
  </form>
</body>
</html>
