<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG5004.aspx.cs" Inherits="GOA.GG5004" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>功能管理</title>

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
                            <asp:Label ID="lblBigTitle" runat="server" Text="功能管理"></asp:Label></legend>
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
                                    <!--gridview start-->
                                    <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="NodeID">
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
                                            <asp:BoundField DataField="NodeName" HeaderText="节点名称" />
                                            <asp:TemplateField HeaderText="强制收回">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="WithdrawTypeID" runat="server" DataValueField="WithdrawTypeID" DataTextField="WithdrawTypeName" DataSource="<%# dtWithdrawType()%>">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="强制归档">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ArchiveFlag" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <CascadeCheckboxes>
                                            <yyc:CascadeCheckbox ChildCheckboxID="Archive" ParentCheckboxID="all" />
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
