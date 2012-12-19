<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG500503.aspx.cs" Inherits="GOA.GG500503" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>触发流程的字段对应</title>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="ManagerForm">
                <fieldset>
                    <legend style="background: url(images/legendimg.jpg) no-repeat 6px 50%;">
                        <asp:Label ID="lblBigTitle" runat="server" Text="创建表达式"></asp:Label></legend>
                    <div class="clear">
                        <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                            <div class="formblk">
                                <input type="hidden" runat="server" id="txtTriggerID" />
                                <div class="clear">
                                    <div class="oneline">
                                        <label class="char5">
                                            目标字段组：</label><div class="iptblk">
                                                <asp:RadioButtonList ID="rblGroupTo" runat="server" AutoPostBack="false" RepeatDirection="Horizontal">
                                                </asp:RadioButtonList>
                                            </div>
                                    </div>
                                </div>
                                <div class="clear">
                                    <div class="oneline">
                                        <label class="char9">
                                            子流程赋值的执行次数：</label><div class="iptblk">
                                                <asp:DropDownList ID="ddlOPCycleType" runat="server" Width="100" AutoPostBack="false">
                                                    <asp:ListItem Value="0">一次</asp:ListItem>
                                                    <asp:ListItem Value="1">按明细行循环执行</asp:ListItem>
                                                </asp:DropDownList>
                                                <span style="color: Red;">针对明细字段的行数的给子流程赋值的执行次数</span></div>
                                    </div>
                                </div>
                                <div class="clear">
                                    <cc2:Button ID="btnAdd" runat="server" ButtonImgUrl="../images/add.gif" Enabled="false" ShowPostDiv="false" Text="Button_Add" ValidateForm="false" AutoPostBack="true" Width="80" OnClick="btnAdd_Click" />
                                    <cc2:Button ID="btnDel" runat="server" ButtonImgUrl="../images/del.gif" Enabled="false" ShowPostDiv="false" Text="Button_Del" ValidateForm="false" AutoPostBack="true" Width="100" OnClick="btnDel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="MappingID">
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
                            <asp:BoundField DataField="OPCycleTypeN" HeaderText="子流程赋值的执行次数" />
                            <asp:BoundField DataField="TargetGroupName" HeaderText="目标字段组" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    设置对应字段</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect" runat="server" Font-Underline="true" Text="设置对应字段" CommandName="setMappingField" CommandArgument='<%# Container.DataItemIndex%>' />
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
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
