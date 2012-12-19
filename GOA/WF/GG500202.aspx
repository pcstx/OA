<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG500202.aspx.cs" Inherits="GOA.GG500202" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>节点操作者</title>
    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>
    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
          
      function CheckInputContent()
      {
        if(document.all.ddlOperatorTypeDetail.value.trim() == "")
        {
          alert("请选择操作者类型。");
          document.all.ddlOperatorTypeDetail.focus();
          return false;
        }
        return true;
      }
      
      function ShowCondition(id)
      {
        var url = "GG50020201.aspx?GUID=" + Math.random() + "&id="+id;
        var ret = window.open(url, 'GG50020201', 'height=680,width=760,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no');
        return !(ret === undefined)
      }
      
      function btnObjectValueClick(TypeCode)
      {
        var url;
        if(TypeCode=='11' || TypeCode=='12')
        {
          url = '../DeptSelect.aspx?' + Math.random();
        }
        else if(TypeCode=='13')
        {
          url = '../Z060Select.aspx?' + Math.random();
        }
        else if(TypeCode=='14')
        {
          url = '../UserSelect.aspx?' + Math.random();
        }
        var ret = window.showModalDialog(url,'UserSelect','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 
        if (ret != null) 
        { 
          window.document.getElementById("txtObjectValue").value =ret[0]; 
          window.document.getElementById("txtObjectValueN").value =ret[1]; 
        }
        return false;
      }
          
    </script>

    <style type="text/css">
        td label
        {
            border: 1px #fff solid;
            width: 88px;
            _width: 89px;
            float: none;
            padding-top: 2px;
            margin-top: 1px;
            margin-right: 1px;
        }
    </style>
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
                            <asp:Label ID="lblBigTitle" runat="server" Text="规则一览"></asp:Label></legend>
                        <div class="clear">
                            <div class="oneline">
                                <fieldset>
                                    <div class="clear">
                                        <div id="divRadioboxList" class="oneline">
                                            <asp:RadioButtonList runat="server" ID="rblOperatorType" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblOperatorType_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="clear">
                                        <input type="hidden" runat="server" id="txtObjectValue" />
                                        <div class="third1">
                                            <label class="char5">
                                                操作者类型：</label>
                                                <div class="iptblk">
                                                    <cc2:DropDownList ID="ddlOperatorTypeDetail" runat="server" Width="120" OnSelectedIndexChanged="ddlOperatorTypeDetail_SelectedIndexChanged" AutoPostBack="true">
                                                    </cc2:DropDownList>
                                                    <span style="color: Red;">*</span></div>
                                        </div>
                                        <div class="third2" runat="server" id="divCtrlObject1">
                                            <div class="iptblk">
                                                <cc2:TextBox ID="txtObjectValueN" runat="server" Width="100"></cc2:TextBox><span style="color: Red;">*</span><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgObjectValue" ToolTip="搜索" runat="server" /></div>
                                        </div>
                                        <div class="third2" runat="server" id="divCtrlObject2">
                                            <div class="iptblk">
                                                <cc2:DropDownList ID="ddlObjectValue" runat="server" Width="120">
                                                </cc2:DropDownList>
                                                <span style="color: Red;">*</span></div>
                                        </div>
                                        <div class="third3" runat="server" id="divCtrlObject3">
                                            <label class="char5" style="float: left">
                                                级别：</label><div class="iptblk">
                                                    <cc2:TextBox ID="txtLevelS" runat="server" Width="20"></cc2:TextBox>-<cc2:TextBox ID="txtLevelE" runat="server" Width="20"></cc2:TextBox><span style="color: Red;">*</span></div>
                                        </div>
                                    </div>
                                    <div class="clear">
                                        <div class="third1">
                                            <label class="char5" style="float: left">
                                                是否会签：</label><div class="iptblk">
                                                    <asp:CheckBox ID="chkSignType" runat="server" Checked="false" /></div>
                                        </div>
                                        <div class="third2">
                                            <label class="char5" style="float: left">
                                                批次：</label><div class="iptblk">
                                                    <cc2:TextBox ID="txtRuleSeq" runat="server" Width="80"></cc2:TextBox></div>
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset>
                                    <div id="pager">
                                        <cc2:Button ID="btnAdd" runat="server" ButtonImgUrl="../images/add.gif" Enabled="false" ShowPostDiv="false" Text="Button_Add" ValidateForm="false" AutoPostBack="true" Width="80" OnClick="btnAdd_Click" ScriptContent="CheckInputContent()" />
                                        <cc2:Button ID="btnSubmit" runat="server" ButtonImgUrl="../images/submit.gif" ShowPostDiv="false" ValidateForm="false" Text="Button_DafaultSubmit" AutoPostBack="true" Width="80" OnClick="btnSubmit_Click" />
                                    </div>
                                    <!--gridview start-->
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
                                                    操作</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelect" runat="server" Text="删除" CommandName="select" CommandArgument='<%# Container.DataItemIndex%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="RuleTypeN" HeaderText="类型" />
                                            <asp:BoundField DataField="RuleName" HeaderText="名称" />
                                            <asp:BoundField DataField="LevelN" HeaderText="级别范围" />
                                            <asp:BoundField DataField="SignTypeN" HeaderText="会签属性" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    条件</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnCondition" runat="server" ImageUrl="../images/icon_enter.gif" OnClientClick=<%#"ShowCondition('"+Eval("RuleID")+"')"%> />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="规则批次">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="RuleSeq" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RuleSeq")%>'></asp:TextBox>
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
