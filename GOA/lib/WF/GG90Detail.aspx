<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG90Detail.aspx.cs" Inherits="GOA.GG90Detail" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>流程代理信息明细</title>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    	

        function btnAdd_Click()
        {
            window.location="GG90Add.aspx?AgentID=";
        }
        
        function btnPersonClick(num)
        {
            var url='SingleUserSelect.aspx?' + Math.random();
            var ret = window.showModalDialog(url,'SingleUserSelect','scroll:1;status:0;help:0;resizable:1;dialogWidth:720px;dialogHeight:550px') ; 

                if (ret != null) 
                { 
                     if (num==1)
                    {
                      window.document.getElementById("txtQBeAgentPersonID").value =ret[0]; 
                      window.document.getElementById("txtQBeAgentPersonName").value =ret[1]; 
                    }
                     if (num==2)
                    {
                      window.document.getElementById("txtQAgentPersonID").value =ret[0]; 
                      window.document.getElementById("txtQAgentPersonName").value =ret[1]; 
                    }
                }
            return false;
        }
        
         function btnWorkflowIDClick(numb)
        {
            var url='WorkflowIDSelect.aspx?' + Math.random();
            var ret = window.showModalDialog(url,'WorkflowIDSelect','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

            if (ret != null) 
            { 
                  window.document.getElementById("txtQWorkflowID").value =ret[0]; 
                  window.document.getElementById("txtQWorkflowName").value =ret[1]; 
              }
        return false;

        }
        
          function ReturnMainPage()
         {
         window.location="GG90.aspx";
         }
    

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="ManagerForm">
                <fieldset>
                    <legend style="background: url(../images/legendimg.jpg) no-repeat 6px 50%;">
                        <asp:Label ID="lblBigTitle" runat="server" Text="流程代理信息"></asp:Label></legend>
                    <div class="con">
                        <input type="hidden" id="txtQWorkflowID" runat="server" />
                        <input type="hidden" id="txtQBeAgentPersonID" runat="server" />
                        <input type="hidden" id="txtQAgentPersonID" runat="server" />
                        <div>
                            <cc2:Button ID="btnSearchRecord" ShowPostDiv="false" runat="server" Text="Button_Search" AutoPostBack="true" ButtonImgUrl="../images/query.gif" Disable="false" OnClick="btnSearchRecord_Click" Page_ClientValidate="false" />
                            <cc2:Button ID="btnAdd" runat="server" ShowPostDiv="false" ValidateForm="false" Text="Button_Add" Disable="false" ScriptContent="btnAdd_Click()" AutoPostBack="false" ButtonImgUrl="../images/add.gif" />
                            <cc2:Button ID="btnReturn" ShowPostDiv="true" runat="server" Text="Button_Return" ButtonImgUrl="../images/browse.gif" AutoPostBack="false" Page_ClientValidate="false" Disable="false" ScriptContent="ReturnMainPage()" />
                            <cc2:Button ID="btnCancelAgent" runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="true" Text="Button_CancelAgent" ButtonImgUrl="../images/cancel.gif" OnClick="btnCancelAgent_Click" />
                        </div>
                        <div class="clear" style="width: 66%">
                            <div class="half">
                                <label class="char8">
                                    流程名称：</label><div class="iptblk">
                                        <cc2:TextBox ID="txtQWorkflowName" runat="server" Width="140px"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImageButton2" ToolTip="搜索" runat="server" OnClientClick="return btnWorkflowIDClick();" /></div>
                            </div>
                            <div class="half">
                                <label class="char8">
                                    是否已取消：</label>
                                <div class="iptblk">
                                    <cc2:DropDownList Width="140px" runat="server" ID="ddlIsCancel">
                                        <asp:ListItem Value="">全部</asp:ListItem>
                                        <asp:ListItem Value="0" Selected="True">未取消</asp:ListItem>
                                        <asp:ListItem Value="1">已取消</asp:ListItem>
                                    </cc2:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </div>
                            </div>
                        </div>
                        <div class="clear" style="width: 66%">
                            <div class="half">
                                <label class="char8">
                                    被代理人：</label><div class="iptblk">
                                        <cc2:TextBox ID="txtQBeAgentPersonName" runat="server" Width="140px"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgAgentPerson" ToolTip="搜索" runat="server" OnClientClick="return btnPersonClick(1);" /></div>
                            </div>
                            <div class="half">
                                <label class="char8">
                                    代理人：</label><div class="iptblk">
                                        <cc2:TextBox ID="txtQAgentPersonName" runat="server" Width="140"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImageButton1" ToolTip="搜索" runat="server" OnClientClick="return btnPersonClick(2);" /></div>
                            </div>
                        </div>
                        <div class="clear" style="width: 66%">
                            <div class="half">
                                <label class="char8">
                                    开始日期：</label><div class="iptblk">
                                        <cc2:TextBox ID="txtQAgentStartDate" runat="server" Width="140" ReadOnly="true"></cc2:TextBox><cc1:CalendarExtender ID="QBeAgentEndDateCalendarExtender" runat="server" TargetControlID="txtQAgentStartDate" Format="yyyy-MM-dd" PopupPosition="TopRight" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;</div>
                            </div>
                            <div class="half">
                                <label class="char8">
                                    结束日期：</label><div class="iptblk">
                                        <cc2:TextBox ID="txtQAgentEndDate" runat="server" Width="140" ReadOnly="true"></cc2:TextBox><cc1:CalendarExtender ID="QAgentEndDateCalendarExtender" runat="server" TargetControlID="txtQAgentEndDate" Format="yyyy-MM-dd" PopupPosition="TopRight" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;</div>
                            </div>
                        </div>
                    </div>
            </div>
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
            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPageSize" FilterType="Numbers" />
            <!--page end-->
            <!--gridview start-->
            <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="AgentID">
                <Columns>
                    <asp:BoundField DataField="FlowTypeName" HeaderText="流程类型" />
                    <asp:TemplateField>
                        <HeaderTemplate>
                            流程名称</HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnSelect" runat="server" Font-Underline="true" Text='<%# Eval("WorkflowName") %>' CommandName="select" CommandArgument='<%# Container.DataItemIndex%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="BeAgentPersonName" HeaderText="被代理人" />
                    <asp:BoundField DataField="AgentPersonName" HeaderText="代理人" />
                    <asp:BoundField DataField="Creator" HeaderText="创建者" />
                    <asp:BoundField DataField="AgentStartDate" HeaderText="开始日期" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="AgentEndDate" HeaderText="结束日期" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="IsCancelT" HeaderText="是否已取消" />
                    <asp:TemplateField>
                        <HeaderTemplate>
                            取消代理</HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="Item" runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="40px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="AllowCycleT" HeaderText="允许递归代理" />
                    <asp:BoundField DataField="AllowCreateT" HeaderText="允许代理人发起流程" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
