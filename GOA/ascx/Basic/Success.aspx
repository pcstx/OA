<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="GOA.Basic.Success" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
      <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    	    <link href="../styles/StyleSheet.css" type="text/css" rel="stylesheet" />
    	    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>
    	    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>
            <script language="javascript" type="text/javascript" src="../JScript/jquery-latest.pack.js"></script>
            <script language="javascript" type="text/javascript">
                function countDown(secs) {
                    $("#tiao").innerText = secs;
                    if (--secs > 0) {
                        setTimeout("countDown(" + secs + ")", 1000);
                    } else {
                    location.href = "WriteMail.aspx?action=write";
                    }
                }
                countDown(3); 
            </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      发送成功，<span id="tiao">3</span><a href="javascript:countDown"></a>秒后自动跳转…


    </div>
    </form>
</body>
</html>
