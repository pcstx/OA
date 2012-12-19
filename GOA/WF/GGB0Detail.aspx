<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GGB0Detail.aspx.cs" Inherits="GOA.GGD0" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>报表信息</title>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
    	function btnReturn_Click()
        {
            window.location="GGB0.aspx";
            return false ;
        }
   
        
        function btnAdd_Click()
    {
      window.location="GGB0Add.aspx?ReportID=";
      return false ;
    }
    
    function btnEditReport_Click()
    {
        window.location="GGB0DetailAdd.aspx?ReportID="+ document.getElementById("txtReportID").value;
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
                    <cc2:Button ID="btnSave" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false" ShowPostDiv="false" Text="Button_Save" ValidateForm="false" AutoPostBack="true" OnClick="btnSave_Click" />
                    <cc2:Button ID="btnAdd" runat="server" ShowPostDiv="false" ValidateForm="false" Text="Button_Add" Disable="false" ScriptContent="btnAdd_Click()" AutoPostBack="false" ButtonImgUrl="../images/add.gif" />
                    <cc2:Button ID="btnDel" runat="server" ButtonImgUrl="../images/del.gif" Enabled="false" Page_ClientValidate="false" ShowPostDiv="false" Text="Button_MoveOut" ValidateForm="false" AutoPostBack="true" Width="100" OnClick="btnDel_Click" />
                    <cc2:Button ID="btnReturn" ShowPostDiv="true" runat="server" Text="Button_Return" ButtonImgUrl="../images/browse.gif" AutoPostBack="false" Page_ClientValidate="false" Disable="false" ValidateForm="false" ScriptContent="btnReturn_Click()" />
                    <cc2:Button ID="btnEditReport" ShowPostDiv="true" runat="server" Text="Button_EditReport" ButtonImgUrl="../images/icon_enter.gif" AutoPostBack="false" Page_ClientValidate="false" Disable="false" ValidateForm="false" ScriptContent="btnEditReport_Click()" />
                </div>
                <div style="float: left; width: 100%; overflow: auto">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label12" Font-Bold="true" Font-Size="9pt" runat="server" Text="报表"></asp:Label></legend>
                        <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                            <input type="hidden" id="txtReportID" runat="server" />
                            <div class="clear">
                                <div class="third1">
                                    <label class="char4">
                                        报表名称：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtReportName" ReadOnly="true" runat="server" Width="120"></cc2:TextBox></div>
                                </div>
                                <div class="third22">
                                    <label class="char4">
                                        报表种类：</label><div class="iptblk">
                                            <cc2:DropDownList ID="ddlReportType" runat="server" Width="120">
                                            </cc2:DropDownList>
                                        </div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="third1">
                                    <label class="char4">
                                        相关表单：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtFormN" runat="server" Width="120" ReadOnly="true"></cc2:TextBox></div>
                                </div>
                                <div class="third22">
                                    <label class="char4">
                                        相关流程：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtWorkflowName" runat="server" Width="300" ReadOnly="true"></cc2:TextBox></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="online">
                                    <span style="color: Red">
                                        <label class="char9" id="lblmsg" runat="server">
                                        </label>
                                    </span>
                                </div>
                            </div>
                    </fieldset>
                </div>
                <div class="ManagerForm">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label1" Font-Bold="true" Font-Size="9pt" runat="server" Text="报表显示项"></asp:Label></legend>
                        <!--gridview start-->
                        <div style="text-align: left">
                            <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" PageSize="10" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="650px" DataKeyNames="ReportID,FieldID">
                                <Columns>
                                    <asp:BoundField DataField="FieldLabel" ItemStyle-Width="250px" HeaderText="字段名称" />
                                    <asp:BoundField DataField="IsStatisticsN" ItemStyle-Width="80px" HeaderText="是否统计" />
                                    <asp:BoundField DataField="IsOrderN" ItemStyle-Width="80px" HeaderText="是否排序" ItemStyle-ForeColor="Red" />
                                    <asp:BoundField DataField="OrderPatternN" ItemStyle-Width="80px" HeaderText="升/降序" />
                                    <asp:BoundField DataField="OrderIndexN" ItemStyle-Width="80px" HeaderText="排序顺序" />
                                    <asp:BoundField DataField="DisplayOrder" ItemStyle-Width="100px" HeaderText="显示顺序" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            删除</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="../images/del.gif" OnClientClick=" return   window.confirm('确定删除此项？');" CommandName="deleteField" CommandArgument='<%# Container.DataItemIndex  %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" />
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
