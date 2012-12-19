<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OfficeCheckInOut.aspx.cs" Inherits="GOA.Basic.OfficeCheckInOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head id="Head1" runat="server"><title>在线查看</title></head>

<script src="../WebOffice/main.js" language="javascript" type="text/javascript"></script>
<body onunload="CloseWord()">
<form id="Form1"  runat="server">
<SCRIPT language=javascript event=NotifyCtrlReady for=WebOffice1>
/****************************************************
*
*	在装载完Weboffice(执行<object>...</object>)
*	控件后执行 "WebOffice1_NotifyCtrlReady"方法
*
****************************************************/
	WebOffice1_NotifyCtrlReady()
</SCRIPT>

<SCRIPT language=javascript event=NotifyWordEvent(eventname) for=WebOffice1>
<!--
 WebOffice1_NotifyWordEvent(eventname)
//-->
</SCRIPT>

<SCRIPT language=javascript type="text/javascript">
    function WebOffice1_NotifyCtrlReady() {
        document.all.WebOffice1.OptionFlag |= 128;
        var Path = '<%=MyPath %>'
        var TailName = '<%=MyTailName %>'
        document.all.WebOffice1.LoadOriginalFile(Path, TailName);

    }
    var flag = false;
    function menuOnClick(id) {
        var id = document.getElementById(id);
        var dis = id.style.display;
        if (dis != "none") {
            id.style.display = "none";

        } else {
            id.style.display = "block";
        }
    }
    var vNoCopy = 0;
    var vNoPrint = 0;
    var vNoSave = 0;
    var vClose = 0;
    function no_copy() {
        vNoCopy = 1;
    }
    function yes_copy() {
        vNoCopy = 0;
    }


    function no_print() {
        vNoPrint = 1;
    }
    function yes_print() {
        vNoPrint = 0;
    }


    function no_save() {
        vNoSave = 1;
    }
    function yes_save() {
        vNoSave = 0;
    }
    function EnableClose(flag) {
        vClose = flag;
    }
    function CloseWord() {

        document.all.WebOffice1.CloseDoc(0);
    }

    function WebOffice1_NotifyWordEvent(eventname) {

        if (eventname == "DocumentBeforeSave") {
            if (vNoSave) {
                document.all.WebOffice1.lContinue = 0;
                alert("无权对文档进行修改");
            } else {
            document.all.WebOffice1.lContinue = 1;
            if ('<%=fileEdtion %>' != "") {
                document.getElementById("Button1").click();
              }
            }
        } else if (eventname == "DocumentBeforePrint") {
            if (vNoPrint) {
                document.all.WebOffice1.lContinue = 0;
                alert("此文档已经禁止打印");
            } else {
                document.all.WebOffice1.lContinue = 1;
            }
        } else if (eventname == "WindowSelectionChange") {
            if (vNoCopy) {
                document.all.WebOffice1.lContinue = 0;
                alert("此文档已经禁止复制");
            } else {
                document.all.WebOffice1.lContinue = 1;
            }
        } else if (eventname == "DocumentBeforeClose") {
            if (vClose == 0) {
                document.all.WebOffice1.lContinue = 0;
            } else {
                //alert("word");
                document.all.WebOffice1.lContinue = 1;
            }
        }
        //alert(eventname); 
    }
</SCRIPT>
<style type="text/css">
.btn
{
	display:none;
	
	}
</style>

<asp:Button ID="Button1" runat="server"  CssClass="btn"  Text="Button" OnClick="Button1_Click" />

<script src="../WebOffice/LoadWebOffice.js" language="javascript" type="text/javascript"></script>

</form>
</body>
</html> 

