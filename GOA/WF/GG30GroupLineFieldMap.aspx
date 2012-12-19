<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG30GroupLineFieldMap.aspx.cs" Inherits="GOA.GG30GroupLineFieldMap" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>表单字段</title>

    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="margin-left: 0px; margin-top: 0px;">
                <div class="ManagerForm">
                    <fieldset>
                        <legend style="background: url(images/legendimg.jpg) no-repeat 6px 50%;">
                            <asp:Label ID="lblBigTitle" runat="server" Text="该明细组中字段一览"></asp:Label></legend>
                        <div class="clear">
                            <div class="oneline">
                                <fieldset>
                                    <!--page start-->
                                    <div class="pager">
                                        <div class="PagerText">
                                            <cc2:Button ID="btnSubmit" runat="server" ButtonImgUrl="../images/submit.gif" ShowPostDiv="false" ValidateForm="false" Text="Button_DafaultSubmit" AutoPostBack="true" Width="80" OnClick="btnSubmit_Click" />
                                        </div>
                                    </div>
                                    <!--page end-->
                                    <asp:Panel ID="Panel1" runat="server" Width="100%" Height="100%">
                                        <!--gridview start-->
                                        <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="FieldID,FieldName">
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
                                                <asp:BoundField DataField="FieldID" HeaderText="字段编号" Visible="false" />
                                                <asp:BoundField DataField="FieldName" HeaderText="字段名称" />
                                                <asp:BoundField DataField="FieldDesc" HeaderText="字段描述" />
                                                <asp:BoundField DataField="HTMLTypeN" HeaderText="字段表现形式" />
                                                <asp:TemplateField HeaderText="字段显示名">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="FieldLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FieldLabel")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="数据集字段">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="DataSetColumn" runat="server" DataValueField="ColumnName" DataTextField="ColumnName" DataSource="<%# dtDataSetColumn()%>">
                                                        </asp:DropDownList>
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
                                        <!--grid view end-->
                                    </asp:Panel>
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
