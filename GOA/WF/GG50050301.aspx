<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG50050301.aspx.cs" Inherits="GOA.GG50050301" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>触发流程的字段对应</title>

    <script language="javascript" type="text/javascript">
      
      function CheckInputContent()
      {
      /*  if(document.all.ddlFieldIDTo.value == "0")
        {
          alert("请选择赋值字段。");
          document.all.ddlFieldIDTo.focus();
          return false;
        }*/
        return true;
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
                    <legend style="background: url(images/legendimg.jpg) no-repeat 6px 50%;">
                        <asp:Label ID="lblBigTitle" runat="server" Text="创建表达式"></asp:Label></legend>
                    <div class="clear">
                        <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                            <div class="formblk">
                                <input type="hidden" runat="server" id="txtTriggerID" />
                                <div class="clear">
                                    <div class="oneline">
                                        <label class="char9">
                                            子流程赋值的执行次数：</label><div class="iptblk">
                                                <asp:DropDownList ID="ddlOPCycleType" runat="server" Width="100" Enabled="false" AutoPostBack="false">
                                                    <asp:ListItem Value="0">一次</asp:ListItem>
                                                    <asp:ListItem Value="1">按明细行循环执行</asp:ListItem>
                                                </asp:DropDownList>
                                                <span style="color: Red;">针对明细字段的行数的给子流程赋值的执行次数</span></div>
                                    </div>
                                </div>
                                <div class="clear">
                                    <div class="oneline">
                                        <label class="char5">
                                            目标字段组：</label><div class="iptblk">
                                                <asp:RadioButtonList ID="rblGroupTo" runat="server" Enabled="false" AutoPostBack="false" RepeatDirection="Horizontal">
                                                </asp:RadioButtonList>
                                            </div>
                                    </div>
                                </div>
                                <div class="clear">
                                    <div class="oneline">
                                        <label class="char5">
                                            目标字段：</label><div class="iptblk">
                                                <cc2:DropDownList ID="ddlFieldIDTo" runat="server" Width="150" AutoPostBack="true" OnSelectedIndexChanged="ddlFieldIDTo_SelectedIndexChanged">
                                                </cc2:DropDownList>
                                                <span style="color: Red;">*</span></div>
                                    </div>
                                </div>
                                <div runat="server" id="divOperationCulculate">
                                    <div runat="server" id="divOperationExpression">
                                        <div class="clear">
                                            <div class="oneline">
                                                <label class="char5">
                                                    赋值表达式：</label><div class="iptblk">
                                                        <asp:Label ID="lblExpression" runat="server" Width="360"></asp:Label></div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                            <div class="oneline">
                                                <label class="char5">
                                                    来源字段组：</label><div class="iptblk">
                                                        <asp:RadioButtonList ID="rblFieldType" runat="server" OnSelectedIndexChanged="rblFieldType_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                                        </asp:RadioButtonList>
                                                    </div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                            <div class="oneline">
                                                <label class="char5">
                                                    来源字段：</label><div class="iptblk">
                                                        <asp:ListBox ID="lbFieldList" runat="server" Width="150" Rows="6" AutoPostBack="true" OnSelectedIndexChanged="lbFieldList_SelectedIndexChanged"></asp:ListBox>
                                                        <span style="color: Red;">单击选择字段</span></div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                            <div class="oneline">
                                                <label class="char5">
                                                    操作符号：</label><div class="iptblk">
                                                        <asp:PlaceHolder runat="server" ID="phOperationButtonList"></asp:PlaceHolder>
                                                    </div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                            <div class="half">
                                                <label class="char5">
                                                    函数类型：</label><div class="iptblk">
                                                        <asp:ListBox ID="lbFunctionType" runat="server" Width="150" Rows="6" AutoPostBack="true" OnSelectedIndexChanged="lbFunctionType_SelectedIndexChanged"></asp:ListBox>
                                                    </div>
                                            </div>
                                            <div class="half">
                                                <label class="char5">
                                                    具体函数：</label><div class="iptblk">
                                                        <asp:ListBox ID="lbFunctionList" runat="server" Width="150" Rows="6" AutoPostBack="true" OnSelectedIndexChanged="lbFunctionList_SelectedIndexChanged"></asp:ListBox>
                                                        <span style="color: Red;">单击选择函数</span></div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                            <div class="half" style="display: none">
                                                <label class="char5">
                                                    常量类型：</label><div class="iptblk">
                                                        <asp:RadioButtonList ID="rblConstantType" runat="server">
                                                            <asp:ListItem Text="数字" Selected="True" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="其他" Value="4"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                            </div>
                                            <div class="half">
                                                <label class="char5">
                                                    常量：</label><div class="iptblk">
                                                        <cc2:TextBox ID="txtConstantValue" runat="server" Width="50"></cc2:TextBox><asp:Button ID="btnConstant" runat="server" OnClick="btnConstant_Click" Text="加入常量" /></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clear">
                                <cc2:Button ID="btnUndo" runat="server" ButtonImgUrl="../images/arrow_undo.gif" Enabled="false" ShowPostDiv="false" Text="Button_Undo" Page_ClientValidate="false" AutoPostBack="true" Width="80" OnClick="btnUndo_Click" />
                                <cc2:Button ID="btnAdd" runat="server" ButtonImgUrl="../images/add.gif" Enabled="false" ShowPostDiv="false" Text="Button_Add" ValidateForm="false" AutoPostBack="true" Width="80" OnClick="btnAdd_Click" ScriptContent="CheckInputContent()" />
                                <cc2:Button ID="btnDel" runat="server" ButtonImgUrl="../images/del.gif" Enabled="false" ShowPostDiv="false" Text="Button_Del" ValidateForm="false" AutoPostBack="true" Width="100" OnClick="btnDel_Click" />
                            </div>
                        </div>
                    </div>
            </div>
            <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="SubMappingID">
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
                    <asp:BoundField DataField="TargetFieldName" HeaderText="目标字段" />
                    <asp:BoundField DataField="SourceGroupName" HeaderText="来源字段组" />
                    <asp:BoundField DataField="SourceFieldName" HeaderText="来源" />
                </Columns>
                <CascadeCheckboxes>
                    <yyc:CascadeCheckbox ChildCheckboxID="item" ParentCheckboxID="all" />
                </CascadeCheckboxes>
                <CheckedRowCssClass CheckBoxID="Item" CssClass="SelectedRow" />
                <ContextMenus>
                    <yyc:ContextMenu Text="版权所有" NavigateUrl="http://www.geobyev.com" Target="_blank" />
                </ContextMenus>
            </yyc:SmartGridView>
            </fieldset> </div>
            <!--          <asp:BoundField DataField="SourceFieldTypeName" HeaderText="来源字段类型" />
-->
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
