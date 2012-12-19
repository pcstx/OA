<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeftCalendar.aspx.cs" Inherits="GOA.MyCalendar.jscalendar.LeftCalendar" %>

<?xml version="1.0" encoding="utf-8"?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">


<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<!-- $Id: index.html,v 1.15 2005/03/05 14:38:10 mishoo Exp $ -->

<head runat="server">
<meta http-equiv="content-type" content="text/xml; charset=utf-8" />
<title></title>
<link rel="stylesheet" type="text/css" media="all" href="skins/aqua/theme.css" title="Aqua" />
<link rel="alternate stylesheet" type="text/css" media="all" href="calendar-blue.css" title="winter" />
<link rel="alternate stylesheet" type="text/css" media="all" href="calendar-blue2.css" title="blue" />
<link rel="alternate stylesheet" type="text/css" media="all" href="calendar-brown.css" title="summer" />
<link rel="alternate stylesheet" type="text/css" media="all" href="calendar-green.css" title="green" />
<link rel="alternate stylesheet" type="text/css" media="all" href="calendar-win2k-1.css" title="win2k-1" />
<link rel="alternate stylesheet" type="text/css" media="all" href="calendar-win2k-2.css" title="win2k-2" />
<link rel="alternate stylesheet" type="text/css" media="all" href="calendar-win2k-cold-1.css" title="win2k-cold-1" />
<link rel="alternate stylesheet" type="text/css" media="all" href="calendar-win2k-cold-2.css" title="win2k-cold-2" />
<link rel="alternate stylesheet" type="text/css" media="all" href="calendar-system.css" title="system" />


<script type="text/javascript" src="calendar.js"></script>


<script type="text/javascript" src="lang/calendar-en.js"></script>


<script type="text/javascript">
    function pageLoad() {
        $addHandler($get("btnSendTo"), 'click', showUserListModalPopup);
    }


    function showUserListModalPopup(ev) {

        parent.showUserListModalPopup();

        //        ev.preventDefault();
        //        var UserListPopupBehavior = $find('UserListPopupBehavior');
        //        UserListPopupBehavior.show();
    }


    var oldLink = null;
    // code to change the active stylesheet
    function setActiveStyleSheet(link, title) {
        var i, a, main;
        for (i = 0; (a = document.getElementsByTagName("link")[i]); i++) {
            if (a.getAttribute("rel").indexOf("style") != -1 && a.getAttribute("title")) {
                a.disabled = true;
                if (a.getAttribute("title") == title)
                    a.disabled = false;
            }
        }
        if (oldLink) oldLink.style.fontWeight = 'normal';
        oldLink = link;
        // link.style.fontWeight = 'bold';
        return false;
    }

    // This function gets called when the end-user clicks on some date.
    function selected(cal, date) {
        cal.sel.value = date; // just update the date in the input field.
        if (cal.dateClicked && (cal.sel.id == "sel1" || cal.sel.id == "sel3"))
        // if we add this call we close the calendar on single-click.
        // just to exemplify both cases, we are using this only for the 1st
        // and the 3rd field, while 2nd and 4th will still require double-click.
            cal.callCloseHandler();
    }

    // And this gets called when the end-user clicks on the _selected_ date,
    // or clicks on the "Close" button.  It just hides the calendar without
    // destroying it.
    function closeHandler(cal) {
        cal.hide();                        // hide the calendar
        //  cal.destroy();
        _dynarch_popupCalendar = null;
    }

    // This function shows the calendar under the element having the given id.
    // It takes care of catching "mousedown" signals on document and hiding the
    // calendar if the click was outside.
    function showCalendar(id, format, showsTime, showsOtherMonths) {
        var el = document.getElementById(id);
        if (_dynarch_popupCalendar != null) {
            // we already have some calendar created
            _dynarch_popupCalendar.hide();                 // so we hide it first.
        } else {
            // first-time call, create the calendar.
            var cal = new Calendar(1, null, selected, closeHandler);
            // uncomment the following line to hide the week numbers
            // cal.weekNumbers = false;
            if (typeof showsTime == "string") {
                cal.showsTime = true;
                cal.time24 = (showsTime == "24");
            }
            if (showsOtherMonths) {
                cal.showsOtherMonths = true;
            }
            _dynarch_popupCalendar = cal;                  // remember it in the global var
            cal.setRange(1900, 2070);        // min/max year allowed.
            cal.create();
        }
        _dynarch_popupCalendar.setDateFormat(format);    // set the specified date format
        _dynarch_popupCalendar.parseDate(el.value);      // try to parse the text in field
        _dynarch_popupCalendar.sel = el;                 // inform it what input field we use

        // the reference element that we pass to showAtElement is the button that
        // triggers the calendar.  In this example we align the calendar bottom-right
        // to the button.
        _dynarch_popupCalendar.showAtElement(el.nextSibling, "Br");        // show the calendar

        return false;
    }

    var MINUTE = 60 * 1000;
    var HOUR = 60 * MINUTE;
    var DAY = 24 * HOUR;
    var WEEK = 7 * DAY;

    // If this handler returns true then the "date" given as
    // parameter will be disabled.  In this example we enable
    // only days within a range of 10 days from the current
    // date.
    // You can use the functions date.getFullYear() -- returns the year
    // as 4 digit number, date.getMonth() -- returns the month as 0..11,
    // and date.getDate() -- returns the date of the month as 1..31, to
    // make heavy calculations here.  However, beware that this function
    // should be very fast, as it is called for each day in a month when
    // the calendar is (re)constructed.
    function isDisabled(date) {
        var today = new Date();
        return (Math.abs(date.getTime() - today.getTime()) / DAY) > 10;
    }

    function flatSelected(cal, date) {
        var el = document.getElementById("preview");
        el.innerHTML = date;
    }

    function showFlatCalendar() {
        var parent = document.getElementById("display");

        // construct a calendar giving only the "selected" handler.
        var cal = new Calendar(0, null, flatSelected);

        // hide week numbers
        cal.weekNumbers = false;

        // We want some dates to be disabled; see function isDisabled above
        // cal.setDisabledHandler(isDisabled);
        cal.setDateFormat("%A, %B %e");

        // this call must be the last as it might use data initialized above; if
        // we specify a parent, as opposite to the "showCalendar" function above,
        // then we create a flat calendar -- not popup.  Hidden, though, but...
        cal.create(parent);

        // ... we can show it here.
        cal.show();
    }


   




</script>

<style type="text/css">
    
.ex { font-weight: bold; background: #fed; color: #080 }
.help { color: #080; font-style: italic; }
body { background: #fea; font: 10pt tahoma,verdana,sans-serif; }
table { font: 13px verdana,tahoma,sans-serif; }
a { color: #00f; }
a:visited { color: #00f; }
a:hover { color: #f00; background: #fefaf0; }
a:active { color: #08f; }
.key { border: 1px solid #000; background: #fff; color: #008;
padding: 0px 5px; cursor: default; font-size: 80%; }
</style>

</head>
<body>  <!--onload="showFlatCalendar()">-->
<script language="javascript" type="text/javascript">
    function find(Permission) {
        var UserName = document.getElementById("dpOtherCalendar").value;
        window.parent.document.getElementById('right').contentWindow.OtherUserPemission(UserName, Permission);

    }

    function SetValue(Userlist) {
        document.getElementById("txtSendTo").value = Userlist;
    }


    function CheckEmpty() {
//        if (document.getElementById("txtSendTo").value == "") {
//            alert("共享人员不能为空");
//            document.getElementById("txtSendTo").focus();
//            return false;
//            
//        }
    }
</script>
    <form id="form1" runat="server">
      <cc1:toolkitscriptmanager id="ToolkitScriptManager1" runat="server" EnablePageMethods="True" EnablePartialRendering="true">
        </cc1:toolkitscriptmanager>
        
          <asp:UpdatePanel ID="UpdatePanel" runat="server"  UpdateMode="Conditional">
                   <ContentTemplate> 
<table  width="100%">
<tr valign="top" align="left">
           <td style="padding: 5px; margin: 5px; border: 1px solid #984; background: #ed9; width: 19em;">
            <!-- the calendar will be inserted here -->
          <%--<div id="display" style="float: left; clear: both;"></div>
          <div id="preview" style="font-size: 80%; text-align: center; padding: 2px">&nbsp;</div>--%>
          <asp:Calendar id="calendar" 
Runat="server" 
ShowGridLines="True" 
ShowDayHeader="True" 
SelectionMode="Day"
PrevMonthText="<<" 
NextMonthText=">>" 
DayNameFormat="Full" 
DayHeaderStyle-HorizontalAlign="Center"
Width="100%" 
TitleStyle-Font-Size="13px" 
TodayDayStyle-BackColor="#ffffcc" 
DayStyle-Font-Size="13px"
DayStyle-Font-Bold=True
DayHeaderStyle-Font-Size="12px"
OtherMonthDayStyle-ForeColor=#cccccc>
</asp:Calendar> 


          </td>
                      
        </tr>
      <tr>
        <td>
           <div class="clear">
                              <label class="char5">
                      
                                    <asp:Label ID="lblSendTo" runat="server">共享日历</asp:Label>
                               </label>
                               <div class="iptblk">
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" >
                                      <ContentTemplate>
                                        <cc2:TextBox ID="txtSendTo" runat="server"   Enabled="false"  Width="100"></cc2:TextBox>
                                         <asp:ImageButton ID="btnSendTo" runat="server" ImageAlign="Middle" ImageUrl="~/images/arrow_black.gif"  ToolTip="添加" />  
                                       
		          <cc2:Button ID="btnSendMail" runat="server" ShowPostDiv="false" ValidateForm="false"   AutoPostBack="true" Disable="false" Text="Button_Share" OnClick="Share_Button" ScriptContent="CheckEmpty()"/>
                      
                                       </ContentTemplate>
                                    </asp:UpdatePanel>
                               
                               </div>
                       </div>
        
        </td>
      
      </tr>
      
            <tr>
        <td>
           <div class="clear">
                              <label class="char5">
                      
                                    <asp:Label ID="Label1" runat="server" Text="查看它人日历"></asp:Label>
                               </label>
                               <div class="iptblk">
                                 <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" >
                                      <ContentTemplate>
                                                        <cc2:DropDownList ID="dpOtherCalendar" runat="server"   >
                                                                   </cc2:DropDownList>
                                           <asp:ImageButton ID="ImageButton1" runat="server" ImageAlign="Middle" ImageUrl="~/images/arrow_black.gif"  ToolTip="查看"  OnClick="GetValue"/>    
                                    
                                       </ContentTemplate>
                                    </asp:UpdatePanel>
                               
                               </div>
                       </div>
        
        </td>
      
      </tr>
       
 </table>
 </ContentTemplate>
 </asp:UpdatePanel>
 
 
 <%-- <asp:Button runat="server" ID="hiddenTargetControl2" style="display:none"/>
  <cc1:ModalPopupExtender runat="server" ID="UserListModalPopupExtender"
      BehaviorID="UserListPopupBehavior"
      TargetControlID="hiddenTargetControl2"
      PopupControlID="PopupShowUserList" 
      BackgroundCssClass="modalBackground"
      DropShadow="False"
      CancelControlID="CancelButton2"
      PopupDragHandleControlID="PopupDragHandle"
      RepositionMode="RepositionOnWindowScroll" >
  </cc1:ModalPopupExtender>  
        
  <asp:Panel runat="server" CssClass="modalPopup" ID="PopupShowUserList" style="display:none;width:650px;  padding:0 0px 0px 0px">
      <div id="Div1" class="programmaticPopupDragHandle">
          <div class="h2blk">
          <h2 id="H1" >
            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
            </h2>
        </div>   
        <cc2:Button ID="Button2" ScriptContent="MaxQueryFrom();" ShowPostDiv="false" runat="server" Text="Label_NULL"  CSS="MaxButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="true" ToolTip="Max" Width="12" />
		<cc2:Button ID="CancelButton2" ShowPostDiv="false" runat="server" Text="Label_NULL"  CSS="CancelButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="false" ToolTip="Close" Width="12" />
       </div>
           
            <div>
                  <asp:LinkButton ID="AllChecked" runat="server"  OnClientClick ="return AllChecked_Click()">
                  全选
                  </asp:LinkButton>
                  
                  <asp:LinkButton ID="AllNoChecked" runat="server" OnClientClick="return AllNoChecked_Click()">
                  全不选
                  </asp:LinkButton>
                  
          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
              <ContentTemplate>  
		                   
          <div >
         	<fieldset>		
		      <legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;"><asp:Label ID="Label5" runat="server" Text=""></asp:Label>
		      </legend>

          <!--page start-->
          <div id="pager" >
            <div class="PagerArea" >
               <webdiyer:AspNetPager ID="AspNetPager1" runat="server"  OnPageChanging="AspNetPager1_PageChanging" onpagechanged="AspNetPager1_PageChanged"  Width="50%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Page: " 
                 HorizontalAlign="left"  CenterCurrentPageButton="true" NumericButtonCount="9">
               </webdiyer:AspNetPager>
            </div>
            <div class="PagerText">
                    <asp:Label ID="lblPageSize" runat="server" Text=""></asp:Label>&nbsp;
                       <cc2:TextBox ID="txtPageSize" runat="server" Width="35" TextMode="SingleLine" SetFocusButtonID="btnPageSize"  ></cc2:TextBox>
                       <cc2:Button ID="btnPageSize" Text="Button_GO" runat="server" Width="60" OnClick="txtPageSize_TextChanged" ShowPostDiv="true"/>
             </div>    
           <cc1:FilteredTextBoxExtender  ID="FilteredTextBoxExtender3" runat="server" 
                    TargetControlID="txtPageSize"
                    FilterType="Numbers"/>   
          </div>
             
                  <cc2:Button ID="AddUser" runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Disable="false" Text="Button_Add"  OnClick="AddUser_Click" />
                
           
       <br />
                                
            <!--page end-->
            <!--gridview start-->
                <!--gridview Browse mode start-->
             <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="">
                    <Columns>
                      <asp:TemplateField>
                        <HeaderTemplate>
                          序号
                        </HeaderTemplate>
                        
                        <ItemTemplate>
                          <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <ItemStyle Width="35px" />
                      </asp:TemplateField>
                      
                      <asp:TemplateField>
                        <HeaderTemplate>
                          选择
                          </HeaderTemplate>
                          
                        <ItemTemplate>
                          <asp:CheckBox ID="Item" runat="server" Checked="false" />
                        </ItemTemplate>
                        <ItemStyle Width="35px" />
                      </asp:TemplateField>
              
                       <asp:BoundField DataField="UserName" HeaderText="用户名" />
                       <asp:BoundField DataField="UserAddress" HeaderText="地址" />
                       
                       <asp:TemplateField>
                         <HeaderTemplate>
                           权限
                         </HeaderTemplate>
                         
                         <ItemTemplate>
                             <asp:DropDownList ID="DropDownList1" runat="server"  DataValueField="permission" DataTextField="permission">
                                <asp:ListItem Text="无权限" Value="无权限"></asp:ListItem>
                                 <asp:ListItem Text="可读事件" Value="可读事件"></asp:ListItem>
                                 <asp:ListItem Text="可删除修改事件" Value="可删除修改事件"></asp:ListItem>
                                <asp:ListItem Text="可创建事件" Value="可创建事件"></asp:ListItem>
                             </asp:DropDownList>
                         </ItemTemplate>
                       </asp:TemplateField>
                       
                    </Columns>
                    <CascadeCheckboxes>
                      <yyc:CascadeCheckbox ChildCheckboxID="item" ParentCheckboxID="all" />
                    </CascadeCheckboxes>
                    <CheckedRowCssClass CheckBoxID="Item" CssClass="SelectedRow" />
                    <ContextMenus>
                      <yyc:ContextMenu Text="版权所有" NavigateUrl="http://www.geobyev.com" Target="_blank" />
                    </ContextMenus>
                  </yyc:SmartGridView>
             
		</fieldset>
     </div> 
                   </ContentTemplate>
                 </asp:UpdatePanel>
       </div>
     </asp:Panel> --%>
     
     
<hr />
</form>
</body></html>
