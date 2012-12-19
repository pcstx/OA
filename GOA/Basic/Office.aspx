<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Office.aspx.cs" Inherits="GOA.Basic.Office" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<%--<head runat="server">
<title>在线查看</title>

<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
 <SCRIPT ID=clientEventHandlersJS LANGUAGE=javascript>
function command_onclick() {
 	eval(document.all.commandtext.value);
 }
 function FramerControl1_WORD_DocumentBeforeClose() {
   

}
function FramerControl1_WORD_DocumentBeforePrint() {  }
function FramerControl1_WORD_WindowActivate() {  }
function FramerControl1_WORD_DocumentChange() {  }
function FramerControl1_WORD_WindowBeforeDoubleClick() {  }
function FramerControl1_WORD_WindowBeforeRightClick() {  }
function FramerControl1_WORD_WindowSelectionChange() {  }
function FramerControl1_BeforeDocumentSaved() {return false;}
function FramerControl1_OnDocumentClosed() {  }
function FramerControl1_BeforeDocumentClosed() {  }
function FramerControl1_OnActivationChange() { }
function FramerControl1_OnDocumentOpened() {  }
function FramerControl1_OnFileCommand() {
    return false;
}
function FramerControl1_OnPrintPreviewExit() {  }
</SCRIPT>
 
<SCRIPT LANGUAGE=javascript FOR=FramerControl1 EVENT=NotifyCtrlReady>
<!--
 FramerControl1_NotifyCtrlReady()
-->
</SCRIPT>
<SCRIPT LANGUAGE=javascript FOR=FramerControl1 EVENT=WORD_DocumentBeforeClose>
<!--
 FramerControl1_WORD_DocumentBeforeClose()
//-->
</SCRIPT>
<SCRIPT LANGUAGE=javascript FOR=FramerControl1 EVENT=WORD_DocumentBeforePrint>
<!--
 FramerControl1_WORD_DocumentBeforePrint()
//-->
</SCRIPT>
 
<SCRIPT LANGUAGE=javascript FOR=FramerControl1 EVENT=WORD_WindowActivate>
<!--
 FramerControl1_WORD_WindowActivate()
//-->
</SCRIPT>
<SCRIPT LANGUAGE=javascript FOR=FramerControl1 EVENT=WORD_DocumentChange>
<!--
 FramerControl1_WORD_DocumentChange()
//-->
</SCRIPT>
<SCRIPT LANGUAGE=javascript FOR=FramerControl1 EVENT=WORD_WindowBeforeDoubleClick>
<!--
 FramerControl1_WORD_WindowBeforeDoubleClick()
//-->
</SCRIPT>
<SCRIPT LANGUAGE=javascript FOR=FramerControl1 EVENT=WORD_WindowBeforeRightClick>
<!--
 FramerControl1_WORD_WindowBeforeRightClick()
//-->
</SCRIPT>
<SCRIPT LANGUAGE=javascript FOR=FramerControl1 EVENT=WORD_WindowSelectionChange>
<!--
 FramerControl1_WORD_WindowSelectionChange()
//-->
</SCRIPT>
<SCRIPT LANGUAGE=javascript FOR=FramerControl1 EVENT=BeforeDocumentSaved>
<!--
 FramerControl1_BeforeDocumentSaved()
//-->
</SCRIPT>
<SCRIPT LANGUAGE=javascript FOR=FramerControl1 EVENT=OnDocumentClosed>
<!--
 FramerControl1_OnDocumentClosed()
//-->
</SCRIPT>
<SCRIPT LANGUAGE=javascript FOR=FramerControl1 EVENT=BeforeDocumentClosed>
<!--
 FramerControl1_BeforeDocumentClosed()
//-->
</SCRIPT>
<SCRIPT LANGUAGE=javascript FOR=FramerControl1 EVENT=OnActivationChange>
<!--
 FramerControl1_OnActivationChange()
//-->
</SCRIPT>
<SCRIPT LANGUAGE=javascript FOR=FramerControl1 EVENT=OnDocumentOpened>
<!--
 FramerControl1_OnDocumentOpened()
//-->
</SCRIPT>
<SCRIPT LANGUAGE=javascript FOR=FramerControl1 EVENT=OnFileCommand>
<!--
FramerControl1_OnFileCommand()
//-->
</SCRIPT>
<SCRIPT LANGUAGE=javascript FOR=FramerControl1 EVENT=OnPrintPreviewExit>
<!--
 FramerControl1_OnPrintPreviewExit()
//-->
</SCRIPT>
</head>
 
<body onkeydown="return false;">
 <script language="javascript" type="text/javascript">



     function FramerControl1_NotifyCtrlReady() {
         document.all.FramerControl1.Menubar = false;
         var Path = '<%=MyPath %>'
         var TailName = '<%=MyTailName %>'

         if (TailName == "xls" || TailName == "xlsx")
             document.all.FramerControl1.Open(Path, false, "Excel.Sheet");
         else if (TailName == "doc") {
         document.all.FramerControl1.ProtectDoc(1, 1, "pwd");
             document.all.FramerControl1.Open(Path, true, "Word.Document");
         }
         else if (TailName == "ppt")
             document.all.FramerControl1.Open(Path, false, "PowerPoint.Show");
     }
     

 </script>
 


<TABLE bgColor=#0000FF border=0 cellPadding=4 cellSpacing=1 width=98%>
  <TR bgColor=#ffffff >
    <TD width="100%" height="100%" align=center >
<OBJECT classid="clsid:00460182-9E5E-11D5-B7C8-B8269041DD57"  id=FramerControl1 style="LEFT: 0px; TOP: 0px; WIDTH: 100%; HEIGHT: 750px" VIEWASTEXT codebase=dsoframer.ocx#version=2,2,0,8>
	<PARAM NAME="_ExtentX" VALUE="6350">
	<PARAM NAME="_ExtentY" VALUE="6350">
	<PARAM NAME="BorderColor" VALUE="-2147483632">
	<PARAM NAME="BackColor" VALUE="-2147483643">
	<PARAM NAME="ForeColor" VALUE="-2147483640">
	<PARAM NAME="TitlebarColor" VALUE="-2147483635">
	<PARAM NAME="TitlebarTextColor" VALUE="-2147483634">
	<PARAM NAME="BorderStyle" VALUE="0">
	<PARAM NAME="Titlebar" VALUE="0">
	<PARAM NAME="Toolbars" VALUE="0">
	<PARAM NAME="Menubar" VALUE="0"></OBJECT>
</TD>
  </TR>
 </TABLE>
</body>--%>

<head id="Head1" runat="server"><title>在线查看</title></head>

<script src="../WebOffice/main.js" language="javascript" type="text/javascript"></script>
<body onunload="CloseWord()">
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
    /****************************************************
    *
    *		控件初始化WebOffice方法
    *
    ****************************************************/

    function WebOffice1_NotifyCtrlReady() {
        document.all.WebOffice1.HideMenuArea("hideall", "", "", "");
        document.all.WebOffice1.SetToolBarButton2("Standard", 1, 8);
        document.all.WebOffice1.SetToolBarButton2("Formatting", 1, 8);
        document.all.WebOffice1.SetToolBarButton2("Menu Bar", 1, 8);
        bToolBar_onclick();

        document.all.WebOffice1.OptionFlag |= 128;
        document.all.WebOffice1.ReadOnly = 1;
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
    /****************************************************
    *
    *		接收office事件处理方法
    *
    ****************************************************/
    var vNoCopy = 0;
    var vNoPrint = 1;
    var vNoSave = 1;
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


<script src="../WebOffice/LoadWebOffice.js" language="javascript" type="text/javascript"></script>


</body>
</html> 
