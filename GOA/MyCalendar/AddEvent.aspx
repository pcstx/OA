<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEvent.aspx.cs" Inherits="GOA.Calendar.AddEvent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
     <script src="../JScript/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../JScript/DateTimePicker/WdatePicker.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
    function EventInfoSubmit() {
        $.ajax({
            type: "Get",
            dataType: "text",
            url: "event.ashx",
            data: "sf=1 &action=add&TxtTitle=" + $("#TxtTitle").val() + "&TxtContent=" + $("#TxtContent").val() + "&drType=" + $("#drType").val() + "&TxtBeginTime=" + $("#TxtBeginTime").val() + "&TxtEndTime=" + $("#TxtEndTime").val() + "&TxtInvite=" + $("#TxtInvite").val() + "&ckTiXing=" + $("#ckTiXing").attr("checked") + "&drTiXing=" + $("#drTiXing").val() + "&ckRepeat=" + $("#ckRepeat").attr("checked") + "&drRepeatRate=" + $("#drRepeatRate").val() + "&ckEmailNote=" + $("#ckEmailNote").attr("checked"),
            complete: function() { $("#load").hide(); },
            success: function(msg) {
                value = parseInt(msg);
                if (value > 0) {
                    alert("新增信息成功!");


                }
                else
                    alert("新增信息失败!");
            } 
        });
    }

    function AllChecked_Click() {

        var GridViewtableSearchList = document.getElementById("GridView1");
        var inputs = GridViewtableSearchList.getElementsByTagName("input");
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].type == "checkbox" && inputs[i].id != "item") {
                inputs[i].checked = true;
            }
        }
        return false;

    }

    function AllNoChecked_Click() {

        var GridViewtableSearchList = document.getElementById("GridView1");
        var inputs = GridViewtableSearchList.getElementsByTagName("input");
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].type == "checkbox" && inputs[i].id != "item") {
                inputs[i].checked = false;
            }
        }
        return false;

    }

    function pageLoad() {
        $addHandler($get("btnSendTo"), 'click', showUserListModalPopup);
    }


    function showUserListModalPopup(ev) {
        ev.preventDefault();
        var UserListPopupBehavior = $find('UserListPopupBehavior');
        UserListPopupBehavior.show();
    }
    
</script>
</head>
<body>
    <form id="form1" runat="server">
   <cc1:toolkitscriptmanager id="ToolkitScriptManager1" runat="server" EnablePageMethods="True" EnablePartialRendering="true">
        </cc1:toolkitscriptmanager>
           <asp:UpdatePanel ID="UpdatePanel" runat="server"  UpdateMode="Conditional">
                   <ContentTemplate> 
 
        <asp:Label ID="lTitle" runat="server">标题</asp:Label>
        <asp:TextBox ID="TxtTitle" runat ="server" ></asp:TextBox>
  
        <br />
         <asp:Label ID="lContent" runat="server">描述</asp:Label>
        <asp:TextBox ID="TxtContent" runat ="server" ></asp:TextBox>
             <br />
         <asp:Label ID="lType" runat="server">类型</asp:Label>
        <asp:DropDownList  runat="server" ID="drType">
          <asp:ListItem Text="" Value="0" Selected="True"></asp:ListItem>
        <asp:ListItem Text="提醒" Value="1"></asp:ListItem>
        <asp:ListItem Text="会议邀请" Value="2"></asp:ListItem>
       </asp:DropDownList>
         <br />
         <asp:Label ID="lBeginTime" runat="server">开始时间</asp:Label>
        <asp:TextBox ID="TxtBeginTime" runat ="server" onFocus="new WdatePicker(this,'%Y-%M-%D %h:%m',true,'default')"></asp:TextBox>
      
         <asp:Label ID="lEndTime" runat="server">结束时间</asp:Label>
        <asp:TextBox ID="TxtEndTime" runat ="server" onFocus="new WdatePicker(this,'%Y-%M-%D %h:%m',true,'default')"></asp:TextBox>
         <br />
         <asp:Label ID="lInvite" runat="server">邀请人员</asp:Label>
       <asp:TextBox ID="TxtInvite" runat="server"></asp:TextBox> 
          <asp:ImageButton ID="btnSendTo" runat="server" ImageAlign="Middle" ImageUrl="~/images/arrow_black.gif"  ToolTip="添加" />  
         <br />
         

          <asp:Label ID="lEmailNote" runat="server">是否邮件通知</asp:Label>
        <asp:CheckBox  runat="server" ID="ckEmailNote" Checked="false"/>
         <br />
         
         <asp:Label ID="lTiXing" runat="server">是否提醒</asp:Label>
        <asp:CheckBox  runat="server" ID="ckTiXing" Checked="false"/>
        
         <asp:Label ID="lTiXingBefore" runat="server">提前多久提醒</asp:Label>
         <asp:DropDownList runat="server" ID="drTiXing">
          <asp:ListItem Text="" Value="0" Selected="True"></asp:ListItem>
           <asp:ListItem Text="10分钟" Value="2"></asp:ListItem>
          <asp:ListItem Text="20分钟" Value="3"></asp:ListItem>
           <asp:ListItem Text="30分钟" Value="4"></asp:ListItem>
            <asp:ListItem Text="60分钟" Value="5"></asp:ListItem>
         
         </asp:DropDownList>
     <br />
        

        
         <asp:Label ID="lRepeat" runat="server">是否是重复事件</asp:Label>
       <asp:CheckBox  runat="server" ID="ckRepeat"/>
       
         <asp:Label ID="lRepeatRate" runat="server">重复频率</asp:Label>
         <asp:DropDownList ID="drRepeatRate"  runat="server">
         <asp:ListItem Text="" Value="0" Selected="True"></asp:ListItem>
         <asp:ListItem  Text="每天" Value="1"></asp:ListItem>
         <asp:ListItem  Text="每周" Value="2"></asp:ListItem>
         <asp:ListItem  Text="每月" Value="3"></asp:ListItem>
         
         </asp:DropDownList>
          <br />
 
  
           
         <input type="button" name="AddBtn" id="AddBtn"  onclick="EventInfoSubmit()" value="提交"/>
     </ContentTemplate>
     </asp:UpdatePanel>
     
      <asp:Button runat="server" ID="hiddenTargetControl2" style="display:none"/>
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
     </asp:Panel> 
    </form>
</body>
</html>
