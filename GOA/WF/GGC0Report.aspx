<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GGC0Report.aspx.cs" Inherits="GOA.GGC0Report" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>报表显示画面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <cc2:Button ID="btnExcel" runat="server" ButtonImgUrl="../images/excel.gif" Enabled="false" ShowPostDiv="false" Text="Button_ExportExcel" ValidateForm="false" AutoPostBack="true" OnClick="btnExcel_Click" />
                    <cc2:Button ID="btnReturn" ShowPostDiv="true" runat="server" Text="Button_ReSearch" ButtonImgUrl="../images/cache_reset.gif" AutoPostBack="true" Page_ClientValidate="false" Disable="false" ValidateForm="false" OnClick="btnReturn_Click" />
                </div>
                <div class="ManagerForm">
                    <fieldset>
                        <!--gridview start-->
                        <div style="text-align: left">
                            <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" ShowFooter="true" ShowHeader="true" FooterStyle-BackColor="LightYellow" FooterStyle-HorizontalAlign="Right" AllowSorting="false" AutoGenerateColumns="True" Width="100%" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
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
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
