<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG70.aspx.cs" Inherits="GOA.GG70" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>数据集信息</title>

    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
   
      function BtnAdd_Click()
      {
      window.location="GG70Add.aspx?DataSetID=";
      
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
                <cc2:Button ID="btnQuery" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false" ShowPostDiv="false" Text="Button_Search" ValidateForm="false" OnClick="btnSearchRecord_Click" AutoPostBack="true" />
                <cc2:Button ID="btnAdd" runat="server" ButtonImgUrl="../images/add.gif" ShowPostDiv="false" ValidateForm="false" Text="Button_Add" Disable="false" ScriptContent="BtnAdd_Click()" AutoPostBack="false" />
                <cc2:Button ID="btnDel" runat="server" ButtonImgUrl="../images/del.gif" Enabled="false" ShowPostDiv="false" Text="Button_Del" ValidateForm="false" AutoPostBack="true" Width="100" OnClick="btnDel_Click" />
            </div>
            <div style="margin-left: 0px; margin-top: 0px; overflow: scroll">
                <div class="ManagerForm">
                    <fieldset>
                        <legend style="background: url(../images/legendimg.jpg) no-repeat 6px 50%;">
                            <asp:Label ID="lblBigTitle" runat="server" Text="数据集管理"></asp:Label></legend>
                        <div class="clear">
                            <div class="third1">
                                <label class="char6">
                                    数据集名称：</label><div class="iptblk">
                                        <cc2:TextBox ID="txtQDataSetName" runat="server" Width="150"></cc2:TextBox></div>
                            </div>
                            <div class="third1">
                                <label class="char6">
                                    数据集类型：</label>
                                <div class="iptblk">
                                    <cc2:DropDownList Width="140px" runat="server" ID="ddlDataSetType">
                                        <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                        <asp:ListItem Value="1">SQL语句</asp:ListItem>
                                        <asp:ListItem Value="2">存储过程</asp:ListItem>
                                    </cc2:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </div>
                            </div>
                        </div>
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
                                    <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" PageSize="10" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="DataSetID">
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
                                                    选择</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Item" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="35px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    操作</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelect" runat="server" Text="编辑" CommandName="select" CommandArgument='<%# Container.DataItemIndex %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DataSourceName" HeaderText="数据源名称" />
                                            <asp:BoundField DataField="DataSetName" HeaderText="数据集名称" />
                                            <asp:BoundField DataField="DataSetTypeT" HeaderText="数据集类型" />
                                            <asp:BoundField DataField="TableList" HeaderText="表" />
                                            <asp:BoundField DataField="DataSetPKColumns" HeaderText="数据集PK" />
                                            <asp:BoundField DataField="ReturnColumns" HeaderText="字段一览" />
                                            <asp:BoundField DataField="ReturnColumnsName" HeaderText="字段描述" />
                                            <asp:BoundField DataField="QueryCondition" HeaderText="查询条件" />
                                            <asp:BoundField DataField="OrderBy" HeaderText="表排序字段" />
                                            <asp:BoundField DataField="QuerySql" HeaderText="完整查询语句" />
                                        </Columns>
                                        <CascadeCheckboxes>
                                            <yyc:CascadeCheckbox ChildCheckboxID="item" ParentCheckboxID="all" />
                                        </CascadeCheckboxes>
                                        <CheckedRowCssClass CheckBoxID="Item" CssClass="SelectedRow" />
                                        <ContextMenus>
                                            <yyc:ContextMenu Text="版权所有" NavigateUrl="http://www.geobyev.com" Target="_blank" />
                                        </ContextMenus>
                                    </yyc:SmartGridView>
                                    <!--  <asp:BoundField DataField="FieldList" HeaderText="表字段名" /> -->
                                    <!--gridview Browse mode end-->
                                </fieldset>
                            </div>
                        </div>
                        <div class="clear">
                            <div class="oneline">
                                <fieldset>
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
                                    <!--page end-->
                                    <!--gridview start-->
                                    <yyc:SmartGridView ID="GridView2" runat="server" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" PageSize="10" OnRowCommand="GridView2_RowCommand" OnRowCreated="GridView2_RowCreated" OnRowDataBound="GridView2_RowDataBound" Width="100%" DataKeyNames="DataSetID">
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
                                                    选择</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Item" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="35px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    操作</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelect" runat="server" Text="编辑" CommandName="select" CommandArgument='<%# Container.DataItemIndex %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DataSourceName" HeaderText="数据源名称" />
                                            <asp:BoundField DataField="DataSetName" HeaderText="数据集名称" />
                                            <asp:BoundField DataField="DataSetTypeT" HeaderText="数据集类型" />
                                            <asp:BoundField DataField="TableList" HeaderText="存储过程名" />
                                            <asp:BoundField DataField="DataSetPKColumns" HeaderText="数据集PK" />
                                            <asp:BoundField DataField="ReturnColumns" HeaderText="字段一览" />
                                            <asp:BoundField DataField="ReturnColumnsName" HeaderText="字段描述" />
                                            <asp:BoundField DataField="QueryCondition" HeaderText="存储过程参数值" />
                                        </Columns>
                                        <CascadeCheckboxes>
                                            <yyc:CascadeCheckbox ChildCheckboxID="item" ParentCheckboxID="all" />
                                        </CascadeCheckboxes>
                                        <CheckedRowCssClass CheckBoxID="Item" CssClass="SelectedRow" />
                                        <ContextMenus>
                                            <yyc:ContextMenu Text="版权所有" NavigateUrl="http://www.geobyev.com" Target="_blank" />
                                        </ContextMenus>
                                    </yyc:SmartGridView>
                                    <!--gridview end-->
                                </fieldset>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
