<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetAllEdtion.aspx.cs" Inherits="GOA.Basic.GetAllEdtion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
     <link href="../styles/StyleSheet.css" type="text/css" rel="stylesheet" />
    	    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>
    	    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>
            <script language="javascript" type="text/javascript" src="../JScript/jquery-latest.pack.js"></script>
</head>
<body>
     <form id="form1" runat="server">
        <cc1:toolkitscriptmanager id="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
        </cc1:toolkitscriptmanager>
        
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          
            <ContentTemplate>
            <%-- <div >
                <cc2:Button ID="btnRefresh" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false"  ShowPostDiv="false" Text="Button_Refresh" ValidateForm="false" AutoPostBack="true" OnClick="btnRefresh_Click"/>
	            <cc2:Button ID="btnDelet" runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Disable="false" Text="Button_MoveOut"  OnClick="btnDelet_Click"  />
                
            </div>--%>
           <div class="ManagerForm">
         	<fieldset>		
		      <legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;"><asp:Label ID="lblBigTitle" runat="server" Text=""></asp:Label>
		      </legend>

          <!--page start-->
          <div id="pager" >
            <div class="PagerArea" >
               <webdiyer:AspNetPager ID="AspNetPager1" runat="server"  OnPageChanging="AspNetPager1_PageChanging" onpagechanged="AspNetPager1_PageChanged"  Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Page: " 
                 HorizontalAlign="left"  CenterCurrentPageButton="true" NumericButtonCount="9">
               </webdiyer:AspNetPager>
            </div>
            <div class="PagerText">
                    <asp:Label ID="lblPageSize" runat="server" Text=""></asp:Label>&nbsp;
                       <cc2:TextBox ID="txtPageSize" runat="server" Width="35" TextMode="SingleLine" SetFocusButtonID="btnPageSize"  ></cc2:TextBox>
                       <cc2:Button ID="btnPageSize" Text="Button_GO" runat="server" Width="60" OnClick="txtPageSize_TextChanged" ShowPostDiv="true"/>
             </div>
          </div>

           <cc1:FilteredTextBoxExtender  ID="FilteredTextBoxExtender2" runat="server" 
                    TargetControlID="txtPageSize"
                    FilterType="Numbers"/>   
                                
            <!--page end-->
            <!--gridview start-->
                <!--gridview Browse mode start-->
             <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="FileID">
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
                      
<%--                      <asp:TemplateField>
                        <HeaderTemplate>
                          选择
                          </HeaderTemplate>
                        <ItemTemplate>
                          <asp:CheckBox ID="Item" runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="35px" />
                      </asp:TemplateField>--%>
                    
                      <asp:TemplateField>
                        <HeaderTemplate>
                          版本号</HeaderTemplate>
                        <ItemTemplate>
                          <asp:LinkButton ID="MailTitle" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FileEdition") %>' CommandName="select" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"FileUrl")%>' />&nbsp;
                        </ItemTemplate>
                      </asp:TemplateField>
                      
                       <asp:BoundField DataField="FileName" HeaderText="文件名" />
                       <asp:BoundField DataField="ModifyUser" HeaderText="修改人" />
                        <asp:BoundField DataField="ModifyDate" HeaderText="修改时间" />
                       <asp:BoundField DataField="FileNote" HeaderText="备注" />
                       
                       
             
                      
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
    </form>
</body>
</html>
