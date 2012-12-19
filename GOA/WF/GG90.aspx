<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG90.aspx.cs" Inherits="GOA.GG90" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>流程代理信息</title>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    function btnAdd_Click()
    {
      window.location="GG90Add.aspx?AgentID=";
    }

    function btnSearch_click()
    {
      window.location="GG90Detail.aspx";
    }
    function confirmCancel()
    {
     if(window.confirm("是否全部收回此代理人的 未取消 流程？")==true )
      return true;
     else 
      return false ;
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <cc2:Button ID="btnSearch" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false" ShowPostDiv="false" Text="Button_Search" ValidateForm="false" AutoPostBack="false" ScriptContent="btnSearch_click()" />
                <cc2:Button ID="btnAdd" runat="server" ShowPostDiv="false" ValidateForm="false" Text="Button_Add" Disable="false" ScriptContent="btnAdd_Click()" AutoPostBack="false" ButtonImgUrl="../images/add.gif" />
            </div>
            <div class="ManagerForm">
                <fieldset>
                    <legend style="background: url(../images/legendimg.jpg) no-repeat 6px 50%;">
                        <asp:Label ID="lblBigTitle" runat="server" Text="当前用户流程代理状况"></asp:Label></legend>
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
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPageSize" FilterType="Numbers" />
                    <!--page end-->
                    <!--gridview start-->
                    <div style="text-align: left">
                        <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" PageSize="10" OnRowCommand="GridView1_RowCommand" Width="550px" DataKeyNames="BeAgentPersonID,AgentPersonID">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        序号
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="BeAgentPersonName" ItemStyle-Width="100px" HeaderText="被代理人" />
                                <asp:BoundField DataField="AgentPersonName" ItemStyle-Width="100px" HeaderText="代理人" />
                                <asp:BoundField DataField="AgentNumber" ItemStyle-Width="100px" HeaderText="数量" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        流程列表</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSelect" runat="server" Font-Underline="true" Text="详细" CommandName="select" CommandArgument='<%# Container.DataItemIndex%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        取消代理</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnCancel" runat="server" Font-Underline="true" OnClientClick="return confirmCancel();" Text="取消代理" CommandName="cancelagent" CommandArgument='<%# Container.DataItemIndex%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
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
                    </div>
                    <!--grid view end-->
                </fieldset>
            </div>
            <div class="ManagerForm">
                <fieldset>
                    <legend style="background: url(../images/legendimg.jpg) no-repeat 6px 50%;">
                        <asp:Label ID="Label1" runat="server" Text="其他用户流程代理状况"></asp:Label></legend>
                    <!--page start-->
                    <div id="pager2">
                        <div class="PagerArea">
                            <webdiyer:AspNetPager ID="AspNetPager2" runat="server" OnPageChanging="AspNetPager2_PageChanging" OnPageChanged="AspNetPager2_PageChanged" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Page: " HorizontalAlign="left" CenterCurrentPageButton="true" NumericButtonCount="9">
                            </webdiyer:AspNetPager>
                        </div>
                        <div class="PagerText">
                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>&nbsp;<cc2:TextBox ID="txtPageSize2" runat="server" Width="35" TextMode="SingleLine" SetFocusButtonID="btnPageSize2"></cc2:TextBox>
                            <cc2:Button ID="btnPageSize2" Text="Button_GO" runat="server" Width="60" OnClick="txtPageSize2_TextChanged" ShowPostDiv="false" />
                        </div>
                    </div>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPageSize2" FilterType="Numbers" />
                    <!--page end-->
                    <!--gridview start-->
                    <div style="text-align: left">
                        <yyc:SmartGridView ID="GridView2" runat="server" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView2_RowCommand" Width="550px" DataKeyNames="BeAgentPersonID,AgentPersonID">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        序号
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="BeAgentPersonName" ItemStyle-Width="100px" HeaderText="被代理人" />
                                <asp:BoundField DataField="AgentPersonName" ItemStyle-Width="100px" HeaderText="代理人" />
                                <asp:BoundField DataField="AgentNumber" ItemStyle-Width="100px" HeaderText="数量" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        流程列表</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSelect2" runat="server" Text="详细" CommandName="select" CommandArgument='<%# Container.DataItemIndex%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        取消代理</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnCancel2" runat="server" OnClientClick="return confirmCancel();" Text="取消代理" CommandName="cancel" CommandArgument='<%# Container.DataItemIndex%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
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
                    </div>
                    <!--grid view end-->
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
