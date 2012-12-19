<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG30ColRule.aspx.cs" Inherits="GOA.GG30ColRule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>行规则</title>

    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
      
      function CheckInputContent()
      {
        if(document.all.ddlFieldIDTo.value == "0")
        {
          alert("请选择目标字段。");
          document.all.ddlFieldIDTo.focus();
          return false;
        }
        return true;
      }
      function SetViewState()
      {
        PageMethods.SetAddViewState(getSucceeded);
      }

      //调用成功后，把每一个字段进行赋空，因为是新加后的调用
      function getSucceeded(result)
      {
        document.getElementById('txtDisplayOrder').value='99990';
      }
      
      function btnFormClick()
      {
        var url='GG30Select.aspx?' + Math.random();
        var ret = window.showModalDialog(url,'GG30Select','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        { 
          window.document.getElementById("txtFormID").value =ret[0]; 
          window.document.getElementById("txtFormN").value =ret[1]; 
        }
        return false;
      }
         
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
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
                                <div class="clear">
                                    <div class="oneline">
                                        <label class="char5">
                                            目标字段：</label><div class="iptblk">
                                                <cc2:DropDownList ID="ddlFieldIDTo" runat="server" Width="150">
                                                </cc2:DropDownList>
                                                <span style="color: Red;">*</span></div>
                                    </div>
                                </div>
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
                                            明细字段：</label><div class="iptblk">
                                                <asp:ListBox ID="lbFieldList" runat="server" Width="150" Rows="6" AutoPostBack="true" OnSelectedIndexChanged="lbFieldList_SelectedIndexChanged"></asp:ListBox>
                                                <span style="color: Red;">单击选择一明细字段</span></div>
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
                                                <span style="color: Red;">单击选择一函数</span></div>
                                    </div>
                                </div>
                                <div class="clear">
                                    <div class="half" style="display: none">
                                        <label class="char5">
                                            常量类型：</label><div class="iptblk">
                                                <asp:RadioButtonList ID="rblConstantType" runat="server">
                                                    <asp:ListItem Text="数字" Selected="True" Value="3"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                    </div>
                                    <div class="half">
                                        <label class="char5">
                                            常数：</label><div class="iptblk">
                                                <cc2:TextBox ID="txtConstantValue" runat="server" Width="50"></cc2:TextBox><asp:Button ID="btnConstant" runat="server" OnClick="btnConstant_Click" Text="加入数字" /></div>
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
                    <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="RuleID">
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
                                    <asp:LinkButton ID="btnSelect" runat="server" Text="更新" CommandName="select" CommandArgument='<%# Container.DataItemIndex%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FieldIDToN" HeaderText="赋值字段" />
                            <asp:BoundField DataField="RuleDetail" HeaderText="赋值表达式" Visible="false" />
                            <asp:BoundField DataField="RuleDetailN" HeaderText="赋值表达式" />
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
