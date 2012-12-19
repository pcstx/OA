<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="logout.aspx.cs"   Inherits="GOA.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>退出系统</title>

  <script type="text/javascript">
			if(top.location!=self.location)
			{
				top.location.href = "logout.aspx";
			}
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <br />
  <br />
  <div style="width: 100%" align="center">
    <div align="center" style="width: 600px; border: 1px dotted #FF6600; background-color: #FFFCEC; margin: auto; padding: 20px;">
      <img src="../../images/hint.gif" border="0" alt="提示:" align="absmiddle" width="11" height="13" />
      &nbsp; 您已成功退出系统
    </div>
  </div>
  </form>
</body>
</html>

