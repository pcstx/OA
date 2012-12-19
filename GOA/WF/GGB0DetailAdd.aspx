<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GGB0DetailAdd.aspx.cs" Inherits="GOA.GGB0DetailAdd" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑报表显示项</title>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
    
        function btnReturn_Click()
        {
            window.location="GGB0Detail.aspx?ReportID="+ document.getElementById("txtReportID").value;
            return false;
        }
    	    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <cc2:Button ID="btnSave" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false" ShowPostDiv="false" Text="Button_Save" ValidateForm="false" AutoPostBack="true" OnClick="btnSave_Click" Page_ClientValidate="false" />
                    <cc2:Button ID="btnReturn" ShowPostDiv="true" runat="server" Text="Button_Return" ButtonImgUrl="../images/browse.gif" AutoPostBack="false" Page_ClientValidate="false" Disable="false" ValidateForm="false" ScriptContent="btnReturn_Click()" />
                </div>
                <input type="hidden" id="txtReportID" runat="server" />
                <div class="ManagerForm">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label1" Font-Bold="true" Font-Size="9pt" runat="server" Text="报表显示项"></asp:Label></legend>
                        <!--gridview start-->
                        <div style="text-align: left">
                            <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" PageSize="10" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="620px" DataKeyNames="ReportID,FieldID">
                                <Columns>
                                    <asp:BoundField DataField="FieldLabel" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Left" HeaderText="字段名称" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            是否显示</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbIsShow" runat="server" AutoPostBack="true" OnCheckedChanged="cbIsShow_OnCheckedChanged" />
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            是否统计</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbIsStatistics" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            是否排序/排序类型/排序关键字顺序</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbIsOrder" runat="server" AutoPostBack="true" OnCheckedChanged="cbIsOrder_OnCheckedChanged" />&nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlOrderPattern" runat="server" Width="40">
                                                <asp:ListItem Value=""></asp:ListItem>
                                                <asp:ListItem Value="1">升</asp:ListItem>
                                                <asp:ListItem Value="2">降</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                                            <cc2:TextBox ID="txtOrderIndex" runat="server" Width="50" ValidationExpression="/^[+-]?[0-9.]*$/"></cc2:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="200px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            显示顺序</HeaderTemplate>
                                        <ItemTemplate>
                                            <cc2:TextBox ID="txtDisplayOrder" runat="server" Width="80" ValidationExpression="/^[+-]?[0-9.]*$/"></cc2:TextBox>
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
                            <!--grid view end-->
                        </div>
                    </fieldset>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
