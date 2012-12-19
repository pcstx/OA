<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GGC0.aspx.cs" Inherits="GOA.GGC0" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>定义报表</title>

    <script language="javascript" type="text/javascript">
   
    function btnDetail_click(rid)
    {
      window.location="GGC0Set.aspx?ReportID="+rid;
      return false ;
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
                    <cc2:Button ID="btnSearch" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false" ShowPostDiv="false" Text="Button_Search" ValidateForm="false" AutoPostBack="true" OnClick="btnSearch_Click" />
                </div>
                <div style="float: left; width: 100%; overflow: auto">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label12" Font-Bold="true" Font-Size="9pt" runat="server" Text="查询框"></asp:Label></legend>
                        <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                            <input type="hidden" id="txtFormID" runat="server" />
                            <div class="clear">
                                <div class="third1">
                                    <label class="char4">
                                        报表名称：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtReportName" runat="server" Width="120"></cc2:TextBox></div>
                                </div>
                                <div class="third1">
                                    <label class="char4">
                                        报表种类：</label><div class="iptblk">
                                            <cc2:DropDownList ID="ddlReportType" runat="server" Width="120">
                                            </cc2:DropDownList>
                                        </div>
                                </div>
                                <div class="third1">
                                    <label class="char4">
                                        相关表单：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtFormN" runat="server" Width="120"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgForm" ToolTip="搜索" runat="server" OnClientClick="return btnFormClick();" /></div>
                                </div>
                            </div>
                    </fieldset>
                </div>
                <br />
                <br />
                <div class="ManagerForm">
                    <fieldset>
                        <!--page start-->
                        <div id="pager">
                            <div class="PagerArea">
                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanging="AspNetPager1_PageChanging" OnPageChanged="AspNetPager1_PageChanged" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Page: " HorizontalAlign="left" CenterCurrentPageButton="true" NumericButtonCount="9">
                                </webdiyer:AspNetPager>
                            </div>
                            <div class="PagerText">
                                <asp:Label ID="lblPageSize" runat="server" Text=""></asp:Label>&nbsp;<cc2:TextBox ID="txtPageSize" runat="server" Width="35" TextMode="SingleLine" SetFocusButtonID="btnPageSize"></cc2:TextBox>
                                <cc2:Button ID="btnPageSize" Text="Button_GO" runat="server" Width="60" OnClick="txtPageSize_TextChanged" ShowPostDiv="false" />
                            </div>
                        </div>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPageSize" FilterType="Numbers" />
                        <!--page end-->
                        <!--gridview start-->
                        <div style="text-align: left">
                            <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" Width="600px" DataKeyNames="ReportID">
                                <Columns>
                                    <asp:BoundField DataField="ReportTypeName" ItemStyle-Width="100px" HeaderText="报表种类" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            报表名称</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSelect2" runat="server" Font-Underline="true" Text='<%# Eval("ReportName") %>' CommandName="select" OnClientClick=<%# "return btnDetail_click('"+Eval("ReportID")+"');" %> />
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FormName" ItemStyle-Width="100px" HeaderText="相关表单" />
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
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
