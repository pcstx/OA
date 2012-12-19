<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG50020102.aspx.cs" Inherits="GOA.GG50020102" %>

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
      function btnValidTypeClick(RowIndex)
      {
        var url='GG80Select.aspx?' + Math.random();
        var ret = window.showModalDialog(url,'GG30Select','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        {
          window.document.getElementById("txtBasicValidTypeID"+RowIndex).value =ret[0]; 
          window.document.getElementById("GridView1_ctl"+RowIndex+"_BasicValidType").value =ret[1]; 
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
            <div style="margin-left: 0px; margin-top: 0px;">
                <div class="ManagerForm">
                    <fieldset>
                        <legend style="background: url(images/legendimg.jpg) no-repeat 6px 50%;">
                            <asp:Label ID="Label1" runat="server" Text="节点明细行控制"></asp:Label></legend>
                        <div class="clear">
                            <div class="oneline">
                                <label class="char5">
                                    是否显示：</label><div class="iptblk">
                                        <asp:CheckBox ID="chkIsView" runat="server" Checked="true" /></div>
                            </div>
                        </div>
                        <div class="clear">
                            <div class="oneline">
                                <label class="char5">
                                    是否可增加：</label><div class="iptblk">
                                        <asp:CheckBox ID="chkIsAdd" runat="server" /></div>
                            </div>
                        </div>
                        <div class="clear">
                            <div class="oneline">
                                <label class="char5">
                                    是否可编辑：</label><div class="iptblk">
                                        <asp:CheckBox ID="chkIsEdit" runat="server" /></div>
                            </div>
                        </div>
                        <div class="clear">
                            <div class="oneline">
                                <label class="char5">
                                    是否可删除：</label><div class="iptblk">
                                        <asp:CheckBox ID="chkIsDelete" runat="server" /></div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
            <div style="margin-left: 0px; margin-top: 0px;">
                <div class="ManagerForm">
                    <fieldset>
                        <legend style="background: url(images/legendimg.jpg) no-repeat 6px 50%;">
                            <asp:Label ID="lblBigTitle" runat="server" Text="节点明细行控制"></asp:Label></legend>
                        <div class="clear">
                            <div class="oneline">
                                <fieldset>
                                    <!--page start-->
                                    <div class="pager">
                                        <div class="PagerText">
                                            <asp:PlaceHolder ID="PHHiddenField" runat="server"></asp:PlaceHolder>
                                            <cc2:Button ID="btnSubmit" runat="server" ButtonImgUrl="../images/submit.gif" ShowPostDiv="false" ValidateForm="false" Text="Button_DafaultSubmit" AutoPostBack="true" Width="80" OnClick="btnSubmit_Click" />
                                        </div>
                                    </div>
                                    <!--page end-->
                                    <!--gridview start-->
                                    <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="FieldID">
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
                                            <asp:BoundField DataField="FieldLabel" HeaderText="字段显示名" />
                                            <asp:TemplateField HeaderText="是否显示">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="IsView" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="是否可编辑">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="IsEdit" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="是否必须输入">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="IsMandatory" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="验证方式">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="BasicValidType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BasicValidTypeN")%>'></asp:TextBox>
                                                    <asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgValidType" ToolTip="搜索" runat="server" OnClientClick="return btnValidTypeClick('02');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="验证时机">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ValidTimeType" runat="server" DataValueField="ValidTimeTypeID" DataTextField="ValidTimeTypeName" DataSource="<%# dtValidTimeType()%>">
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
