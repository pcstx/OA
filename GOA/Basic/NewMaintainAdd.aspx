<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewMaintainAdd.aspx.cs" Inherits="GOA.Basic.NewMaintainAdd" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
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
                function CheckInputContent() {

                    if ($("#txtTitle").val() == "") {
                        alert("标题不能为空");
                        $("#txtTitle").focus();
                        return false;
                    }

                    if ($("#dpType").val() == "--请选择--") {
                        alert("请选择类别");
                        $("#dpType").focus();
                        return false;
                    }

          
                    if ($("#ddlOperatorTypeDetail").val() == "") {
                        alert("请选择操作者类型");
                        $("#ddlOperatorTypeDetail").focus();
                        return false;
                    }
                    if ($("#txtObjectValueN").val() == "") {
                        alert("类型不能为空");
                        $("#txtObjectValueN").focus();
                        return false;
                    }

                     var atContent = FCKeditorAPI.GetInstance("FCKeditor1");
                     var getValue = atContent.GetXHTML(true);
        
                     if (getValue == "") {
                        alert("内容不能为空");
                        return false;
                    }

                    if ($("#txtExpDate").val() != "") {
                        if (!DoReg($("#txtExpDate").val(), "datetime")) {
                            alert("过期日期格式不正确")
                            $("#txtExpDate").focus();
                            return false;
                        }
                    }

                    return true;
                }


                function DoReg(value, type) {
                    var Reg = "";
                    if (type == "varchar(50)")   //字符串
                        Reg = /^[a-z,A-Z]+$/;
                    else if (type == "int")  //数字
                        Reg = /^\d.+$/
                    else if (type == "datetime") //日期
                        Reg = /^(?:(?!0000)[0-9]{4}-(?:(?:0[1-9]|1[0-2])-(?:0[1-9]|1[0-9]|2[0-8])|(?:0[13-9]|1[0-2])-(?:29|30)|(?:0[13578]|1[02])-31)|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)-02-29)$/;
                    if (!Reg.exec(value)) {
                        return false;
                    }
                    return true;
                }

                function FCKUpdateLinkedField(id) {
                    try {
                        if (typeof (FCKeditorAPI) == "object") {
                            FCKeditorAPI.GetInstance(id).UpdateLinkedField();
                        }
                    }
                    catch (err) {
                    }
                }

                function btnObjectValueClick() {
                    var TypeCode = $("#ddlOperatorTypeDetail").val();
                    var url;
                    if (TypeCode == '11' || TypeCode == '12') {
                        url = '../DeptSelect.aspx?' + Math.random();
                    }
                    else if (TypeCode == '13') {
                        url = '../Z060Select.aspx?' + Math.random();
                    }
                    else if (TypeCode == '14') {
                        url = '../UserSelect.aspx?' + Math.random();
                    }
                    var ret = window.showModalDialog(url, 'UserSelect', 'scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px');
                    if (ret != null) {
                        $("#txtObjectValueN").val(ret[1]);
                     }
                       
                    return false;
                }
          
            </script>
</head>
<body>
    <form id="form1" runat="server">
      <cc1:toolkitscriptmanager id="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
        </cc1:toolkitscriptmanager>
              
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          
            <ContentTemplate>
                 <div class="clear">
                           <cc2:Button ID="btnAdd" runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Disable="false" Text="Button_MoveIn" OnClick="btnAdd_Click" ScriptContent="CheckInputContent()"/>
                           <cc2:Button ID="btnExit" runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Disable="false" Text="Button_Return" OnClick="btnExit_Click"/>
                </div> 
       <div style="margin-left: 0px; margin-top: 0px;">
           <div class="ManagerForm">
         	  <fieldset>		
		         <legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;">
		            <asp:Label ID="lblBigTitle" runat="server" Text="添加信息"></asp:Label>
		         </legend>
		               <div class="clear">
                              <label class="char5">
                                    <asp:Label ID="lblTitle" runat="server" Text="信息标题"></asp:Label>
                               </label>
                               <div class="iptblk">
                                    <cc2:TextBox ID="txtTitle" runat="server" Width="200" ></cc2:TextBox>
                               </div>
                       </div>
                       
                       <div class="clear">
                              <label class="char5">
                                    <asp:Label ID="lblType" runat="server" Text="信息类别"></asp:Label>
                               </label>
                               <div class="iptblk">
                                    <cc2:DropDownList ID="dpType" runat="server" Width="150" Enabled="true" ></cc2:DropDownList>
                               </div>
                       </div>
                       
                      <div class="clear">
                              <label class="char5">
                                    <asp:Label ID="lblIsPublish" runat="server" Text="是否发布"></asp:Label>
                               </label>
                               <div class="iptblk">
                                    <input type="checkbox" id="chkIsPublish" runat="server" />
                               </div>
                       </div>
                       
                      <div class="clear">
                              <label class="char5">
                                    <asp:Label ID="lblExpDate" runat="server" Text="过期日期"></asp:Label>
                               </label>
                               <div class="iptblk">
                                    <cc2:TextBox ID="txtExpDate" runat="server" Width="100"  ></cc2:TextBox>
                                     <asp:ImageButton runat="Server" ID="ImageExpDate" ImageUrl="../images/calendar.gif" AlternateText="Click to show calendar" />
                                      <cc1:CalendarExtender ID="calendarButtonExtender" runat="server" TargetControlID="txtExpDate" PopupButtonID="ImageExpDate" Format="yyyy-MM-dd" Animated="true" PopupPosition="TopLeft" />
                               </div>
                    </div>
                    
                       <div class="clear">
                             <label class="char5">
                                    <asp:Label ID="lblOperator" runat="server" Text="操作者类型"></asp:Label>
                               </label>
                             <div class="iptblk">
                                    <cc2:DropDownList ID="ddlOperatorTypeDetail" runat="server" Width="120" >
                                    </cc2:DropDownList>
                                     <cc2:TextBox ID="txtObjectValueN" runat="server" Width="100"></cc2:TextBox>
                                     <img alt="" src="../images/arrow_black.gif"  onclick="btnObjectValueClick();" />
                              </div>
                           
                       </div>
                       
                       <div class="clear">
                             <label class="char5">
                                    <asp:Label ID="lblBody" runat="server" Text="信息内容"></asp:Label>
                               </label>
                               <div class="iptblk">
                               <!--
                                   <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" DefaultLanguage="zh-cn" Width="500" Height="300"></FCKeditorV2:FCKeditor>     -->
                                    <FTB:FreeTextBox ID="FreeTextBox1" runat="server"   Width="650" Height="400" 
                                   toolbarlayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,Italic,
                                   Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;
                                   CreateLink,Unlink|Cut,Copy,Paste,Delete;Undo,Redo,Print,Save|SymbolsMenu,StylesMenu,InsertHtmlMenu|InsertRule,InsertDate,InsertTime|InsertTable,
                                   EditTable;InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,DeleteTableColumn|InsertForm,InsertTextBox,
                                   InsertTextArea,InsertRadioButton,InsertCheckBox,InsertDropDownList,InsertButton|InsertDiv,EditStyle,InsertImageFromGallery,Preview,SelectAll,WordClean,NetSpell"
                                   ToolbarStyleConfiguration="Office2000" ImageGalleryPath="~/Basic/image/upload"  >
                                   </FTB:FreeTextBox>                             
                               </div>
                       </div>
		             
		    </div>
		      </fieldset>
		      </div>
		      </div>
            </ContentTemplate>
            </asp:UpdatePanel>
    </form>
</body>
</html>
