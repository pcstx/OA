<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeSelect.aspx.cs" Inherits="GOA.EmployeeSelect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>使用人员选择</title>
    <script language="JavaScript1.2" src="JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>
    <script language="JavaScript1.2" src="JScript/common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
      function btnSelectClick(code,name)
      {
        var ret = new Array(2); 
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
  <div class="ManagerForm">
    	    <fieldset>		
		        <legend><asp:Label ID="Label12" runat="server" Text="搜索框"></asp:Label></legend>
                           <div class="conblk2" id="Div1" xmlns:fo="http://www.w3.org/1999/XSL/Format">
		                    <div class="con" id="Div2">
			                    <div class="formblk">
                            <div class="clear">
                              <div class="half"><label class="char5"><!--label-->人员名称：</label><div class="iptblk"><!--textbox--><cc2:TextBox ID="txtQEmpName" runat="server" Width="90" ></cc2:TextBox></div></div>
                              <div class="half"><label class="char5"><!--label-->部门名称：</label><div class="iptblk"><!--textbox--><cc2:TextBox ID="txtQDeptName" runat="server" Width="90" ></cc2:TextBox></div></div>
                            </div>
                            
                            <div class="clear">
                              <div class="half"><label class="char5"><!--label--></label><div class="iptblk"><!--textbox--><cc2:Button ID="btnSearch" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false"  ShowPostDiv="true" Text="Button_Search" ValidateForm="false" AutoPostBack="true"  OnClick="btnSearchRecord_Click" /> </div></div>
                            </div>  
                         </div>
                       </div>
                    </div>
		  </fieldset>
         </div>
  
  <div style="margin-left:0px; margin-top:0px; ">
  <div class="ManagerForm" >
    <fieldset>		
		<legend style="background:url(images/legendimg.jpg) no-repeat 6px 50%;"><asp:Label ID="lblBigTitle" runat="server" Text="使用人员选择"></asp:Label></legend>
                  
      <div class="clear">
        <div class="oneline" >
          <fieldset>
              <!--page start-->
              <cc2:Button ID="btnSelect" runat="server" Enabled="false" ShowPostDiv="false" Text="Button_Select" ValidateForm="false" AutoPostBack="true" Width="100" OnClick="btnSelect_Click" />
          <div id="pager" >
            <div class="PagerArea" ><webdiyer:AspNetPager ID="AspNetPager1" runat="server"  OnPageChanging="AspNetPager1_PageChanging" onpagechanged="AspNetPager1_PageChanged"  Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Page: " HorizontalAlign="left"  CenterCurrentPageButton="true" NumericButtonCount="9"></webdiyer:AspNetPager></div>
            <div class="PagerText">
              <asp:Label ID="lblPageSize" runat="server" Text=""></asp:Label>&nbsp;<cc2:TextBox ID="txtPageSize" runat="server" Width="35" TextMode="SingleLine" SetFocusButtonID="btnPageSize"  ></cc2:TextBox>
              <cc2:Button ID="btnPageSize" Text="Button_GO" runat="server" Width="60" OnClick="txtPageSize_TextChanged" ShowPostDiv="false"/>
            </div>    
           </div>
              <!--page end-->
            <!--gridview start-->
      <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20"  OnRowCommand="GridView1_RowCommand" onrowcreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="PEEBIEC,PEEBIEN">
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
                <asp:TemplateField Visible="false" >
                  <headertemplate>选择</headertemplate>
                  <itemtemplate>
                    <asp:CheckBox ID="Item" runat="server" />
                  </itemtemplate>
                  <itemstyle width="35px" />
                </asp:TemplateField>
                <asp:TemplateField>
                <headertemplate>操作</headertemplate>
                <itemtemplate>
                  <asp:LinkButton ID="btnSelect" runat="server"  Text="选择" CommandName="select" CommandArgument='<%# Container.DataItemIndex%>' />&nbsp;
                </itemtemplate>
              </asp:TemplateField>
              <asp:BoundField DataField="PEEBIEC" HeaderText="员工编号" />
              <asp:BoundField DataField="PEEBIEN" HeaderText="员工姓名" />
              <asp:BoundField DataField="PBDEPDN" HeaderText="所属部门"  />
              

            </Columns>

            <CascadeCheckboxes>
                <yyc:CascadeCheckbox ChildCheckboxID="item" ParentCheckboxID="all" />
            </CascadeCheckboxes>
            <CheckedRowCssClass CheckBoxID="Item" CssClass="SelectedRow" />
            <ContextMenus>
                <yyc:ContextMenu Text="版权所有" NavigateUrl="http://www.geobyev.com" Target="_blank" />
            </ContextMenus>

            </yyc:SmartGridView>
          
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

    </form>
</body>
</html>
