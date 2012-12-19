<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="GOA.myWorkflow.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
      <script type="text/javascript">

        function show()
        {
            var value=document.getElementById("Hidden1").value;
            window.alert(value);
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input id="Hidden1" type="hidden" runat="server"/>
        <input id="Button1" type="button" value="button" onclick="show();"/>
        <br />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Button" />
    </div>
    </form>
</body>
</html>
