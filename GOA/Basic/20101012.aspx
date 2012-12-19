<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="20101012.aspx.cs" Inherits="HRMWeb.aspx._0101012" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>员工状态选择</title>
     <base target="_parent"/>
 <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    	    <link href="../styles/StyleSheet.css" type="text/css" rel="stylesheet" />
   
    	    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>
    	    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>
            <script type="text/javascript">
                function btnClick(name,code)
                {
                      var ret = new Array(2); 
                      ret[0] = name; 
                      ret[1] = code; 
                      window.returnValue = ret; 
                      window.close();
                      return false;
                }
            </script>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />
    <div>
     <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                   <ContentTemplate>   
                   <div class="edit">
		           <!--搜索-->  
                   <cc2:Button ID="btnSelect" runat="server" Text="Label_NULL" AutoPostBack="true" ButtontypeMode="Normal"   Page_ClientValidate="false" Disable="true"  Width="26" OnClick="btnBrowseMode_Click" ShowPostDiv="true" CSS="OkButtonCSS" />
		           </div> 
		              <!--page start-->
            <div id="pager" >
            <div class="PagerArea" >
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server"  OnPageChanging="AspNetPager1_PageChanging" onpagechanged="AspNetPager1_PageChanged"  Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Page: " 
        HorizontalAlign="left"  CenterCurrentPageButton="true" NumericButtonCount="9"></webdiyer:AspNetPager>
            </div>
            <div class="PagerText">
                  
                    <asp:Label ID="lblPageSize" runat="server" Text=""></asp:Label>&nbsp;<cc2:TextBox ID="txtPageSize" runat="server" Width="35" TextMode="SingleLine" SetFocusButtonID="btnPageSize"  ></cc2:TextBox>
                                <cc2:Button ID="btnPageSize" Text="Button_GO" runat="server" Width="60" OnClick="txtPageSize_TextChanged" ShowPostDiv="true"/>
                                </div>
                            
                                </div>
                  <ajaxToolkit:FilteredTextBoxExtender  ID="FilteredTextBoxExtender2" runat="server" 
                    TargetControlID="txtPageSize"
                    FilterType="Numbers"/>   
                                
            <!--page end-->
            <!--gridview start-->
                <!--gridview Browse mode start-->
            <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" 
        PageSize="100"  OnRowCommand="GridView1_RowCommand" onrowcreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound"
        Width="100%" BoundRowDoubleClickCommandName="" MergeCells="" >
              <Columns>
                    <asp:ButtonField CommandName="DoubleClick" Visible="False" />
                    <asp:ButtonField CommandName="RightMenuClick" Visible="False" />
                    <asp:TemplateField>
                        <headertemplate>
                            <asp:CheckBox ID="all" runat="server" />
                        </headertemplate>
                        <itemtemplate>
                                <asp:CheckBox ID="item" runat="server" />
                        </itemtemplate>
                        <itemstyle width="50px" />
                    </asp:TemplateField>
              </Columns>
            <SmartSorting AllowMultiSorting="True" AllowSortTip="True" />
            <ClientButtons>
                <yyc:ClientButton BoundCommandName="Sort" Position="First" AttributeKey="onclick"
                    AttributeValue="return confirm('确认对字段“{1}”排序吗？')" />
            </ClientButtons>
            <CascadeCheckboxes>
                <yyc:CascadeCheckbox ChildCheckboxID="item" ParentCheckboxID="all" />
            </CascadeCheckboxes>
            <FixRowColumn FixRowType="Header,Pager" TableHeight="395px" TableWidth="100%" FixColumns="2,3,4" />
            <CheckedRowCssClass CheckBoxID="item" CssClass="SelectedRow" />
            <ContextMenus>
                <yyc:ContextMenu Text="RightMenuClick" BoundCommandName="RightMenuClick" />
                <yyc:ContextMenu Text="&lt;hr /&gt;" />
                <yyc:ContextMenu Text="webabcd blog" NavigateUrl="http://lyb0002280.cnblogs.com" Target="_blank" />
            </ContextMenus>

            </yyc:SmartGridView>
             <!--gridview Browse mode end-->
          
             <!--grid view end--> 
                          <!--validate start-->
                          <!--validate end-->      
           </ContentTemplate>
      </asp:UpdatePanel>
    </div>
    </form>

</body>
</html>

