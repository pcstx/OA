<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Z050.aspx.cs" Inherits="GOA.Z050" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>部门负责人</title>

  <script language="javascript" type="text/javascript">
      // Add click handlers for buttons to show and hide modal popup on pageLoad
      function pageLoad() {
        if($get("btnAdd") != null)
        $addHandler($get("btnAdd"), 'click', showAddModalPopupViaClient);
      }
      function showAddModalPopupViaClient(ev) {
        ev.preventDefault();
        var modalPopupBehavior = $find('programmaticAddModalPopupBehavior');
        modalPopupBehavior.show();
      }
      
      function CheckContent()
      {
        if(document.all.txtDeptID.value == "" || document.all.txtDeptID.value == "0")
        {
          alert("请选择所属部门");
          document.all.txtDeptName.focus();
          return false;
        }
        if(document.all.txtUserSerialID.value == "" || document.all.txtUserSerialID.value == "0")
        {
          alert("请选择部门负责人");
          document.all.txtUserName.focus();
          return false;
        }
        return true;
      }
      
      function btnDepartmentClick()
      {
        var url='DeptSelect.aspx?GUID=' + Math.random() ;
        var ret = window.showModalDialog(url,'DeptSelect','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        { 
          window.document.getElementById("txtQDeptID").value =ret[0]; 
          window.document.getElementById("txtQDeptName").value =ret[1]; 
        } 
        return false;
      }
      
      function btnUserListClick()
      {
        var url='UserSelect.aspx?GUID=' + Math.random() ;
        var ret = window.showModalDialog(url,'UserSelect','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        {
          window.document.getElementById("txtQUserSerialID").value =ret[0]; 
          window.document.getElementById("txtQUserName").value =ret[1]; 
        } 
        return false;
      }
      
      function btnIDepartmentClick()
      {
        var url='DeptSelect.aspx?GUID=' + Math.random() ;
        var ret = window.showModalDialog(url,'DeptSelect','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        { 
          window.document.getElementById("txtDeptID").value =ret[0]; 
          window.document.getElementById("txtDeptName").value =ret[1]; 
        } 
        return false;
      }
      
      function btnIUserListClick()
      {
        var url='UserSelect.aspx?GUID=' + Math.random() ;
        var ret = window.showModalDialog(url,'UserSelect','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        {
          window.document.getElementById("txtUserSerialID").value =ret[0]; 
          window.document.getElementById("txtUserName").value =ret[1]; 
        } 
        return false;
      }
        
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
  </cc1:ToolkitScriptManager>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div class="ManagerForm">
        <fieldset>
          <legend>
            <asp:Label ID="Label12" runat="server" Text="搜索框"></asp:Label></legend>
          <div class="conblk2" id="Div1" xmlns:fo="http://www.w3.org/1999/XSL/Format">
            <div class="con" id="Div2">
              <div class="formblk">
                <input type="hidden" id="txtQUserSerialID" runat="server" />
                <input type="hidden" id="txtQDeptID" runat="server" />
                <div class="clear">
                  <div class="half">
                    <label class="char5">
                      <!--label-->
                      所属部门：</label><div class="iptblk">
                        <!--textbox-->
                        <cc2:TextBox ID="txtQDeptName" runat="server" Width="90"></cc2:TextBox><asp:ImageButton ImageUrl="../../images/arrow_black.gif" ImageAlign="Middle" ID="ImageButton2" ToolTip="搜索" runat="server" OnClientClick="return btnDepartmentClick();" /></div>
                  </div>
                  <div class="half">
                    <label class="char5">
                      <!--label-->
                      部门负责人：</label><div class="iptblk">
                        <!--textbox-->
                        <cc2:TextBox ID="txtQUserName" runat="server" Width="90"></cc2:TextBox><asp:ImageButton ImageUrl="../../images/arrow_black.gif" ImageAlign="Middle" ID="ImageButton1" ToolTip="搜索" runat="server" OnClientClick="return btnUserListClick();" /></div>
                  </div>
                </div>
                <div class="clear">
                  <div class="half">
                    <label class="char5">
                      <!--label-->
                    </label>
                    <div class="iptblk">
                      <!--textbox-->
                      <cc2:Button ID="btnSearch" runat="server" ButtonImgUrl="../../images/query.gif" Enabled="false" ShowPostDiv="true" Text="Button_Search" ValidateForm="false" AutoPostBack="true" OnClick="btnSearchRecord_Click" />
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </fieldset>
      </div>
      <div style="margin-left: 0px; margin-top: 0px;">
        <div class="ManagerForm">
          <fieldset>
            <legend style="background: url(../../images/legendimg.jpg) no-repeat 6px 50%;">
              <asp:Label ID="lblBigTitle" runat="server" Text="部门负责人管理"></asp:Label></legend>
            <div class="clear">
              <div class="oneline">
                <fieldset>
                  <!--page start-->
                  <div id="pager">
                    <div class="PagerArea">
                      <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanging="AspNetPager1_PageChanging" OnPageChanged="AspNetPager1_PageChanged" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Page: " HorizontalAlign="left" CenterCurrentPageButton="true" NumericButtonCount="9">
                      </webdiyer:AspNetPager>
                    </div>
                    <div class="PagerText">
                      <asp:Label ID="lblPageSize" runat="server" Text=""></asp:Label>&nbsp;<cc2:TextBox ID="txtPageSize" runat="server" Width="35" TextMode="SingleLine" SetFocusButtonID="btnPageSize"></cc2:TextBox>
                      <cc2:Button ID="btnPageSize" Text="Button_GO" runat="server" Width="60" OnClick="txtPageSize_TextChanged" ShowPostDiv="false" />
                    </div>
                  </div>
                  <!--page end-->
                  <!--gridview start-->
                  <!--gridview Browse mode start-->
                  <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="DeptID">
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
                          删除</HeaderTemplate>
                        <ItemTemplate>
                          <asp:CheckBox ID="Item" runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="35px" />
                      </asp:TemplateField>
                      <asp:TemplateField>
                        <HeaderTemplate>
                          操作</HeaderTemplate>
                        <ItemTemplate>
                          <asp:LinkButton ID="btnSelect" runat="server" Text="编辑" CommandName="select" CommandArgument='<%# Container.DataItemIndex%>' />&nbsp;
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="DeptName" HeaderText="所属部门" />
                      <asp:BoundField DataField="UserName" HeaderText="部门负责人" />
                      <asp:TemplateField HeaderText="是否可用" ItemStyle-Width="80px" Visible="false">
                        <ItemTemplate>
                          <asp:CheckBox ID="chkUseFlag" runat="server" />
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
  <asp:Button runat="server" ID="hiddenTargetControlForAddModalPopup" Style="display: none" />
  <cc1:ModalPopupExtender runat="server" ID="programmaticAddModalPopup" BehaviorID="programmaticAddModalPopupBehavior" TargetControlID="hiddenTargetControlForAddModalPopup" PopupControlID="programmaticPopupControlForAdd" BackgroundCssClass="modalBackground" DropShadow="False" CancelControlID="AddCancelButton" PopupDragHandleControlID="programmaticAddPopupDragHandle" RepositionMode="RepositionOnWindowScroll">
  </cc1:ModalPopupExtender>
  <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopupControlForAdd" Style="display: none; width: 650px; padding: 0 0px 0px 0px">
    <div id="programmaticAddPopupDragHandle" class="programmaticPopupDragHandle">
      <div class="h2blk">
        <h2 id="H2_1">
          <asp:Label ID="Label1" runat="server" Text="部门负责人管理"></asp:Label></h2>
      </div>
      <cc2:Button ID="AddCancelButton" ShowPostDiv="false" runat="server" Text="Label_NULL" CSS="CancelButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="false" ToolTip="Close" Width="12" />
    </div>
    <div>
      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
          <div>
            <cc2:Button ID="btnSubmit" ShowPostDiv="true" runat="server" Text="Label_NULL" CSS="SaveButtonCSS" AutoPostBack="true" ButtontypeMode="Normal" OnClick="hideModalPopupViaServer_Click" ValidateForm="false" Width="26" ScriptContent="CheckContent()" />
            <cc2:Button ID="btnSubmitAndClose" ShowPostDiv="true" runat="server" Text="Label_NULL" CSS="SaveAndCloseCSS" AutoPostBack="true" ButtontypeMode="Normal" OnClick="hideModalPopupViaServer_Click" ValidateForm="false" Width="26" ScriptContent="CheckContent()" />
          </div>
          <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
            <div class="con">
              <input type="hidden" id="txtUserSerialID" runat="server" />
              <input type="hidden" id="txtDeptID" runat="server" />
              <div class="clear">
                <div class="half">
                  <label class="char5">
                    <!--label-->
                    所属部门：</label><div class="iptblk">
                      <!--textbox-->
                      <cc2:TextBox ID="txtDeptName" runat="server" Width="150"></cc2:TextBox><asp:ImageButton ImageUrl="../../images/arrow_black.gif" ImageAlign="Middle" ID="ImageButton3" ToolTip="搜索" runat="server" OnClientClick="return btnIDepartmentClick();" /></div>
                </div>
                <div class="half">
                  <label class="char5">
                    <!--label-->
                    部门负责人：</label><div class="iptblk">
                      <!--textbox-->
                      <cc2:TextBox ID="txtUserName" runat="server" Width="150"></cc2:TextBox><asp:ImageButton ImageUrl="../../images/arrow_black.gif" ImageAlign="Middle" ID="ImageButton4" ToolTip="搜索" runat="server" OnClientClick="return btnIUserListClick();" /></div>
                </div>
              </div>
              <div class="clear">
                <div class="half">
                  <label class="char5">
                    <!--label-->
                    是否可用：</label><div class="iptblk">
                      <!--textbox-->
                      <asp:CheckBox ID="chkUseFlag" runat="server" Checked="true" /></div>
                </div>
              </div>
              <div class="clear">
                <label class="char5">
                  <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></label></div>
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
