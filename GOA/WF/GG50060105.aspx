<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG50060105.aspx.cs" Inherits="GOA.GG50060105" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>附加动作1(出发条件)</title>

    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
          
      function CheckInputContent()
      {
//        if(document.all.ddlOperatorTypeDetail.value.trim() == "")
//        {
//          alert("请选择操作者类型。");
//          document.all.ddlOperatorTypeDetail.focus();
//          return false;
//        }
        return true;
      }
          
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
                            <asp:Label ID="lblBigTitle" runat="server" Text="条件批次一览"></asp:Label></legend>
                        <div class="clear">
                            <div class="oneline">
                                <fieldset>
                                    <yyc:SmartGridView ID="GridView2" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView2_RowCommand" OnRowDataBound="GridView2_RowDataBound" Width="100%" DataKeyNames="BranchBatchSeq">
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <HeaderTemplate>
                                                    序号
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="35px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <HeaderTemplate>
                                                    操作</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelect" runat="server" Text="删除" CommandName="select" CommandArgument='<%# Container.DataItemIndex%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BranchBatchSeq" HeaderText="分支" />
                                            <asp:TemplateField HeaderText="字段">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="FieldID" runat="server" DataValueField="FieldID" DataTextField="FieldLabel" DataSource="<%# dtFieldID()%>">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="比较方式">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="SymbolCode" runat="server" DataValueField="SymbolCode" DataTextField="SymbolName" DataSource="<%# dtSymbolCode()%>">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="比较对象">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="CompareToValue" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CompareToValue")%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="分支关系">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="AndOr" runat="server" DataValueField="AndOrValue" DataTextField="AndOrText" DataSource="<%# dtAndOr()%>" OnSelectedIndexChanged="AndOr_SelectedIndexChanged" AutoPostBack="true">
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
                                </fieldset>
                                <fieldset>
                                    <div id="pager">
                                        <cc2:Button ID="btnAdd" runat="server" ButtonImgUrl="../images/add.gif" Enabled="false" ShowPostDiv="false" Text="Button_Add" ValidateForm="false" AutoPostBack="true" Width="80" OnClick="btnAdd_Click" ScriptContent="CheckInputContent()" />
                                        <cc2:Button ID="btnSubmit" runat="server" ButtonImgUrl="../images/submit.gif" ShowPostDiv="false" ValidateForm="false" Text="Button_DafaultSubmit" AutoPostBack="true" Width="80" OnClick="btnSubmit_Click" />
                                    </div>
                                    <!--gridview start-->
                                    <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="ConditionID">
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
                                            <asp:BoundField DataField="BatchSeq" HeaderText="条件批次" />
                                            <asp:BoundField DataField="BranchBatchSeq" HeaderText="分支" />
                                            <asp:BoundField DataField="FieldLabel" HeaderText="字段标题" />
                                            <asp:BoundField DataField="SymbolName" HeaderText="比较方式" />
                                            <asp:BoundField DataField="CompareToValue" HeaderText="比较对象" />
                                            <asp:BoundField DataField="AndOr" HeaderText="分支关系" />
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
