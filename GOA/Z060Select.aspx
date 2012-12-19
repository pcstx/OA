<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Z060Select.aspx.cs" Inherits="GOA.Z060Select" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>角色管理</title>
    <script language="JavaScript1.2" src="JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>
    <script language="JavaScript1.2" src="JScript/common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
      var styleToSelect;
      function pageLoad() {
        $addHandler($get("btnQuery"), 'click', showQueryModalPopupViaClient);
      }
      function showQueryModalPopupViaClient(ev) {
        ev.preventDefault();
        var modalPopupBehavior = $find('programmaticQueryModalPopupBehavior');
        modalPopupBehavior.show();
      }
      
      function btnSelectClick(code,name)
      {
        var ret = new Array(3);
        ret[0] = code; 
        ret[1] = name; 
        window.returnValue = ret; 
        window.close();
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
  <div>
    <cc2:Button ID="btnQuery" runat="server" ButtonImgUrl="images/search.gif" Enabled="false"  ShowPostDiv="false" Text="Button_Search" ValidateForm="false" AutoPostBack="false" Width="80" />
  </div>
  
  <div style="margin-left:0px; margin-top:0px; ">
  <div class="ManagerForm" >
    <fieldset>		
		<legend style="background:url(images/legendimg.jpg) no-repeat 6px 50%;"><asp:Label ID="lblBigTitle" runat="server" Text="字段管理"></asp:Label></legend>
                  
      <div class="clear">
        <div class="oneline" >
          <fieldset>
           <!--page start-->
           <cc2:Button ID="btnSelect" runat="server" Enabled="false"  ShowPostDiv="false" Text="Button_Select" ValidateForm="false" AutoPostBack="true" Width="80" OnClick="btnSelect_Click" />
           <!--page start-->
           <div id="pager">
             <div class="PagerArea" ><webdiyer:AspNetPager ID="AspNetPager1" runat="server"  OnPageChanging="AspNetPager1_PageChanging" onpagechanged="AspNetPager1_PageChanged"  Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Page: " HorizontalAlign="left"  CenterCurrentPageButton="true" NumericButtonCount="9"></webdiyer:AspNetPager></div>
             <div class="PagerText">
               <asp:Label ID="lblPageSize" runat="server" Text=""></asp:Label>&nbsp;<cc2:TextBox ID="txtPageSize" runat="server" Width="35" TextMode="SingleLine" SetFocusButtonID="btnPageSize"  ></cc2:TextBox>
               <cc2:Button ID="btnPageSize" Text="Button_GO" runat="server" Width="60" OnClick="txtPageSize_TextChanged" ShowPostDiv="false"/>
             </div>   
           </div>
              <!--page end-->
            <!--gridview start-->
                <!--gridview Browse mode start-->
      <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20"  OnRowCommand="GridView1_RowCommand" onrowcreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="RoleID">
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
                  <headertemplate>选择</headertemplate>
                  <itemtemplate>
                    <asp:CheckBox ID="Item" runat="server" />
                  </itemtemplate>
                  <itemstyle width="35px" />
                </asp:TemplateField>
              <asp:TemplateField>
                <HeaderTemplate>操作</HeaderTemplate>
                <itemtemplate>
                  <asp:LinkButton ID="btnSelect" runat="server"  Text="选择" CommandName="select" CommandArgument='<%# Container.DataItemIndex%>' />
                </itemtemplate>
              </asp:TemplateField>
              <asp:BoundField DataField="RoleName" HeaderText="名称" />
              <asp:BoundField DataField="RoleDesc" HeaderText="描述" />
              <asp:TemplateField HeaderText="是否可用" ItemStyle-Width="80px">
                <ItemTemplate>
                  <asp:CheckBox ID="chkUseflag" runat="server"/>
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

<asp:Button runat="server" ID="hiddenTargetControlForQueryModalPopup" style="display:none"/>
<cc1:ModalPopupExtender runat="server" ID="programmaticQueryModalPopup"
  BehaviorID="programmaticQueryModalPopupBehavior"
  TargetControlID="hiddenTargetControlForQueryModalPopup"
  PopupControlID="programmaticPopupQuery" 
  BackgroundCssClass="modalBackground"
  DropShadow="False"
  CancelControlID="QueryCancelButton"
  PopupDragHandleControlID="programmaticQueryPopupDragHandle"
  RepositionMode="RepositionOnWindowScroll" >
</cc1:ModalPopupExtender>
<asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopupQuery" style="display:none;width:650px;  padding:0 0px 0px 0px">
  <div id="programmaticQueryPopupDragHandle" class="programmaticPopupDragHandle">
    <div class="h2blk">
      <h2 id="H2_2" ><asp:Label ID="lblSearchTitle" runat="server" Text="搜索框"></asp:Label></h2>
    </div>
    <cc2:Button ID="QueryCancelButton" ShowPostDiv="false" runat="server" Text="Label_NULL"  CSS="CancelButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="false" ToolTip="Close" Width="12" />
   </div>
   <div>
     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
       <ContentTemplate>   
       <div class="edit">
         <cc2:Button ID="btnSubmitSearch" ShowPostDiv="true" runat="server" Text="Label_NULL" CSS="SearchButtonCSS" AutoPostBack="true" ButtontypeMode="Normal" OnClick="btnSearchRecord_Click" Page_ClientValidate="true" Disable="false" Width="16" />
       </div> 
           <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
            <div class="con">
              <div id="Div5">
              <div class="formblk">
              <div class="textblk">
                <div class="clear">
                  <div class="half"><label class="char5">名称：</label><div class="iptblk"><cc2:TextBox ID="txtQRoleName" runat="server" Width="90" ></cc2:TextBox></div></div>
                  <div class="half"><label class="char5">描述：</label><div class="iptblk"><cc2:TextBox ID="txtQRoleDesc" runat="server" Width="90" ></cc2:TextBox></div></div>
                </div>
              </div>
              </div>
              </div>
              </div>
           </div>
         </ContentTemplate>
       </asp:UpdatePanel>
  </div>
</asp:Panel>

    </form>
</body>
</html>
