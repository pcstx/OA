<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG30Info.aspx.cs" Inherits="GOA.GG30Info" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Src="UserControl/FormField.ascx" TagName="FormField" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>表单管理</title>

    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    </script>

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" EnablePageMethods="True" ID="ScriptManager1" />
    <div>
        <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
            <div class="clear">
                <label class="char5">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></label></div>
            <div class="clear">
                <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="主子段">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="PanelUserContrl" Height="400px">
                                <uc1:FormField ID="FormField1" runat="server" />
                            </asp:Panel>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="明细组">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="Panel2" Height="300px">
                                <div style="margin-left: 0px; margin-top: 0px;">
                                    <div class="ManagerForm">
                                        <fieldset>
                                            <legend>
                                                <asp:Label ID="Label12" runat="server" Text="明细组输入框"></asp:Label></legend>
                                            <div class="conblk2" id="Div1" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                                                <div class="con" id="Div2">
                                                    <div class="formblk">
                                                        <div class="clear">
                                                            <div class="third1">
                                                                <label class="char5">
                                                                    名称：</label><div class="iptblk">
                                                                        <cc2:TextBox ID="txtGroupName" runat="server" Width="85"></cc2:TextBox></div>
                                                            </div>
                                                            <div class="third2">
                                                                <label class="char5">
                                                                    描述：</label><div class="iptblk">
                                                                        <cc2:TextBox ID="txtGroupDesc" runat="server" Width="85"></cc2:TextBox></div>
                                                            </div>
                                                            <div class="third3">
                                                                <label class="char5">
                                                                    显示顺序：</label><div class="iptblk">
                                                                        <cc2:TextBox ID="txtDisplayOrder" runat="server" Width="85"></cc2:TextBox></div>
                                                            </div>
                                                        </div>
                                                        <div class="clear">
                                                            <div class="oneline">
                                                                <cc2:Button ID="btnAdd" runat="server" ButtonImgUrl="../images/add.gif" Enabled="false" ShowPostDiv="false" Text="Button_Add" ValidateForm="false" AutoPostBack="true" Width="80" OnClick="btnAdd_Click" />
                                                                <cc2:Button ID="btnSubmit" runat="server" ButtonImgUrl="../images/submit.gif" ShowPostDiv="false" ValidateForm="false" Text="Button_DafaultSubmit" AutoPostBack="true" Width="80" OnClick="btnSubmit_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="clear">
                                                            <div class="oneline">
                                                                <fieldset>
                                                                    <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="GroupID">
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
                                                                                    操作</HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="btnSelect" runat="server" Text="删除" CommandName="select" CommandArgument='<%# Container.DataItemIndex%>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="名称">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="GroupName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GroupName")%>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="描述">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="GroupDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GroupDesc")%>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="显示顺序">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="DisplayOrder" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DisplayOrder")%>'></asp:TextBox>
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
                                                                </fieldset>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
