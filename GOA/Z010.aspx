<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Z010.aspx.cs" Inherits="GOA.Z010" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户管理</title>
    <script language="javascript" type="text/javascript">
      var styleToSelect;
      // Add click handlers for buttons to show and hide modal popup on pageLoad
      function pageLoad() {
        $addHandler($get("btnAdd"), 'click', showAddModalPopupViaClient);
      }
      function showAddModalPopupViaClient(ev) {
        ev.preventDefault();
        var modalPopupBehavior = $find('programmaticAddModalPopupBehavior');
        modalPopupBehavior.show();
      }
            
      function CheckContent()
      {
        if (document.all.txtUserID.value=='')
        {
          alert("请输入用户ID");
          document.all.txtUserID.focus();
          return false;
        }
        
        if (document.all.ddlUserType.value=='0'
        && document.all.txtUserCode.value=='')
        {
          alert("请输入员工编号");
          document.all.txtUserCode.focus();
          return false;
        }
        return true;
      }
      function SetViewState()
      {
        PageMethods.SetAddViewState(getSucceeded);
      }

      //调用成功后，把每一个字段进行赋空，因为是新加后的调用
      function getSucceeded(result)
      {
        document.getElementById('txtUserName').value=result;
      }
      function btnEmployeeSearchClick() 
      {
        var url = 'EmployeeSelect.aspx?GUID=' + Math.random();
        var ret = window.showModalDialog(url, 'EmployeeSelect', 'scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px');

        if (ret != null) {
          window.document.getElementById("txtUserCode").value = ret[0];
          window.document.getElementById("txtUserName").value = ret[1];
        }
          return false;
      }  
    </script>  

</head>
<body>
    <form id="form1" runat="server">
      <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
  <ContentTemplate>
  <div class="ManagerForm">
    	    <fieldset>		
		        <legend><asp:Label ID="Label12" runat="server" Text="搜索框"></asp:Label></legend>
                           <div class="conblk2" id="Div1" xmlns:fo="http://www.w3.org/1999/XSL/Format">
		                    <div class="con" id="Div2">
			                    <div class="formblk">
                            <div class="clear">
                              <div class="half"><label class="char5"><!--label-->用户ID：</label><div class="iptblk"><!--textbox--><cc2:TextBox ID="txtQUserID" runat="server" Width="90" ></cc2:TextBox></div></div>
                              <div class="half"><label class="char5"><!--label-->用户姓名：</label><div class="iptblk"><!--textbox--><cc2:TextBox ID="txtQUserName" runat="server" Width="90" ></cc2:TextBox></div></div>
                            </div>
                            <div class="clear">
                              <div class="half"><label class="char5"><!--label-->用户类型：</label><div class="iptblk"><!--textbox--><asp:DropDownList ID="ddlQUserType" runat="server" Width="100"><asp:ListItem Value=""></asp:ListItem><asp:ListItem Value="0">内部员工</asp:ListItem><asp:ListItem Value="1">外部用户</asp:ListItem></asp:DropDownList></div></div>
                              <div class="half"><label class="char5"><!--label-->员工编号：</label><div class="iptblk"><!--textbox--><cc2:TextBox ID="txtQUserCode" runat="server" Width="90"></cc2:TextBox></div></div>
                            </div>
                            <div class="clear">
                              <div class="half"><label class="char5"><!--label--></label><div class="iptblk"><!--textbox--><cc2:Button ID="btnSearch" runat="server" ButtonImgUrl="../../images/query.gif" Enabled="false"  ShowPostDiv="true" Text="Button_Search" ValidateForm="false" AutoPostBack="true"  OnClick="btnSearchRecord_Click" /> </div></div>
                            </div>  
                         </div>
                       </div>
                    </div>
		  </fieldset>
         </div>
  
  <div style="margin-left:0px; margin-top:0px; ">
  <div class="ManagerForm" >
    <fieldset>		
		<legend style="background:url(../../images/legendimg.jpg) no-repeat 6px 50%;"><asp:Label ID="lblBigTitle" runat="server" Text="用户管理"></asp:Label></legend>
                  
      <div class="clear">
        <div class="oneline" >
          <fieldset>
              <!--page start-->
              <div class="pager" >
              <div class="PagerText">
                <cc2:Button ID="btnAdd" runat="server" ButtonImgUrl="../../images/add.gif" Enabled="false"  ShowPostDiv="false" Text="Button_Add" ValidateForm="false" AutoPostBack="false" Width="80" ScriptContent="SetViewState()"/>
                <cc2:Button ID="btnDel" runat="server" ButtonImgUrl="../../images/del.gif" Enabled="false"  ShowPostDiv="false" Text="Button_Del" ValidateForm="false" AutoPostBack="true" Width="80" OnClick="btnDel_Click" />
              </div>
              </div>
              <div id="pager" >
            <div class="PagerArea" ><webdiyer:AspNetPager ID="AspNetPager1" runat="server"  OnPageChanging="AspNetPager1_PageChanging" onpagechanged="AspNetPager1_PageChanged"  Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Page: " HorizontalAlign="left"  CenterCurrentPageButton="true" NumericButtonCount="9"></webdiyer:AspNetPager></div>
            <div class="PagerText">
              <asp:Label ID="lblPageSize" runat="server" Text=""></asp:Label>&nbsp;<cc2:TextBox ID="txtPageSize" runat="server" Width="35" TextMode="SingleLine" SetFocusButtonID="btnPageSize"  ></cc2:TextBox>
              <cc2:Button ID="btnPageSize" Text="Button_GO" runat="server" Width="60" OnClick="txtPageSize_TextChanged" ShowPostDiv="false"/>
            </div>
                
           </div>
              <!--page end-->
            <!--gridview start-->
                <!--gridview Browse mode start-->
      <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20"  OnRowCommand="GridView1_RowCommand" onrowcreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="UserSerialID">
             <Columns>
               <asp:TemplateField>
                  <headertemplate>
                      序号
                  </headertemplate>
                  <itemtemplate>
                      <%# Container.DataItemIndex + 1 %>
                  </itemtemplate>
                  <itemstyle width="35px" />
               </asp:TemplateField>
                <asp:TemplateField>
                  <headertemplate>删除</headertemplate>
                  <itemtemplate>
                    <asp:CheckBox ID="Item" runat="server" />
                  </itemtemplate>
                  <itemstyle width="35px" />
                </asp:TemplateField>
                <asp:TemplateField>
                <headertemplate>操作</headertemplate>
                <itemtemplate>
                  <asp:LinkButton ID="btnSelect" runat="server"  Text="编辑" CommandName="select" CommandArgument='<%# Container.DataItemIndex%>' />&nbsp;
                </itemtemplate>
              </asp:TemplateField>
              <asp:BoundField DataField="UserID" HeaderText="用户帐号" />
              <asp:BoundField DataField="UserName" HeaderText="用户姓名" />
              <asp:BoundField DataField="UserTypeN" HeaderText="用户类型" />
              <asp:BoundField DataField="UserEmail" HeaderText="Email" />
              <asp:BoundField DataField="UserCode" HeaderText="员工编号" />
              <asp:BoundField DataField="DeptName" HeaderText="所属部门" />
              <asp:TemplateField HeaderText="是否可用" ItemStyle-Width="80px">
                <ItemTemplate>
                  <asp:CheckBox ID="chkUseFlag" runat="server"/>
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
             <!--gridview Browse mode end-->
          
             <!--grid view end--> 
        </fieldset>		
        </div>
        </div>

		</fieldset>
    </div> 
 </div>   
 </ContentTemplate>
 <Triggers>
 </Triggers>
   </asp:UpdatePanel>
   
    <asp:Button runat="server" ID="hiddenTargetControlForAddModalPopup" style="display:none"/>
  <cc1:ModalPopupExtender runat="server" ID="programmaticAddModalPopup"
      BehaviorID="programmaticAddModalPopupBehavior"
      TargetControlID="hiddenTargetControlForAddModalPopup"
      PopupControlID="programmaticPopupControlForAdd" 
      BackgroundCssClass="modalBackground"
      DropShadow="False"
      CancelControlID="AddCancelButton"
      PopupDragHandleControlID="programmaticAddPopupDragHandle"
      RepositionMode="RepositionOnWindowScroll" >
  </cc1:ModalPopupExtender>
    <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopupControlForAdd" style="display:none;width:650px;  padding:0 0px 0px 0px">
      <div id="programmaticAddPopupDragHandle" class="programmaticPopupDragHandle">
        <div class="h2blk">
          <h2 id="H2_1" ><asp:Label ID="Label1" runat="server" Text="维护用户信息"></asp:Label></h2>
          </div>
        <cc2:Button ID="AddCancelButton" ShowPostDiv="false" runat="server" Text="Label_NULL"  CSS="CancelButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="false" ToolTip="Close" Width="12" />
      </div>
          <div >
              <asp:UpdatePanel ID="UpdatePanel3" runat="server">
              <ContentTemplate> 
              <div >
                <cc2:Button ID="btnSubmit" ShowPostDiv="true" runat="server" Text="Label_NULL" CSS="SaveButtonCSS" AutoPostBack="true" ButtontypeMode="Normal" OnClick="hideModalPopupViaServer_Click" ValidateForm="false" Width="26" ScriptContent="CheckContent()"/>
                <cc2:Button ID="btnSubmitAndClose" ShowPostDiv="true" runat="server" Text="Label_NULL" CSS="SaveAndCloseCSS" AutoPostBack="true" ButtontypeMode="Normal" OnClick="hideModalPopupViaServer_Click" ValidateForm="false" Width="26" ScriptContent="CheckContent()"/>
              </div>   
              <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                <div class="con">
                  <div class="clear">
                    <div class="half"><label class="char5"><!--label-->用户ID：</label><div class="iptblk"><cc2:TextBox ID="txtUserID" runat="server" Width="150"></cc2:TextBox></div></div>
                    <div class="half"><label class="char5"><!--label-->用户姓名：</label><div class="iptblk"><cc2:TextBox ID="txtUserName" runat="server" Width="150"></cc2:TextBox></div></div>
                  </div>
                  <div class="clear">
                    <div class="half"><label class="char5"><!--label-->用户类型：</label><div class="iptblk"><asp:DropDownList ID="ddlUserType" runat="server" Width="100"><asp:ListItem Value="0">内部员工</asp:ListItem><asp:ListItem Value="1">外部用户</asp:ListItem></asp:DropDownList></div></div>
                    <div class="half"><label class="char5"><!--label-->员工编号：</label><div class="iptblk"><cc2:TextBox ID="txtUserCode" runat="server" Width="150"></cc2:TextBox><asp:ImageButton ImageUrl="../../images/arrow_black.gif"  ImageAlign="Middle" ID="ImgPosition"  ToolTip="搜索" runat="server" OnClientClick="return btnEmployeeSearchClick();"/></div></div>
                  </div>
                  <div class="clear">
                    <div class="half"><label class="char5"><!--label-->Email：</label><div class="iptblk"><cc2:TextBox ID="txtUserEmail" runat="server" Width="150"></cc2:TextBox></div></div>
                    <div class="half"><label class="char5"><!--label-->是否可用：</label><div class="iptblk"><asp:CheckBox ID="chkUseFlag" runat="server" Checked="true" /></div></div>
                  </div>
                  <input type="hidden" runat="server" id="txtUserSerialID" />
                  <input type="hidden" runat="server" id="txtDeptID" />
                  <div class="clear"><label class="char5"> <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></label></div>
                </div>
              </div>
              <!--validate start-->
              <!--validate end-->
          </ContentTemplate>
          </asp:UpdatePanel>
          </div>
</asp:Panel>

    </form>
</body>
</html>
