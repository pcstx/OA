<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GGC0Set.aspx.cs" Inherits="GOA.GGC0Set" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>自定义报表</title>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        //设置生成的控件被选中时，cbCondition也自动选中
        function cbConditionChecked(checkboxID)
        {
            document.getElementById(checkboxID).checked=true;
        }

        function btnReturn_Click()
        {
            window.location="GGC0.aspx";
            return false;
        }
        
          
            function checkkey(value,e){ 
              var key = window.event?e.keyCode:e.which; 
               if(key == 13 || key == 46)
                return true;
                if(key < 48 || key >57)
                return false;
                else
                return true;
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
                    <cc2:Button ID="btnSearch" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false" ShowPostDiv="false" Text="Button_Search" ValidateForm="false" Page_ClientValidate="false" AutoPostBack="true" OnClick="btnSearch_Click" />
                    <cc2:Button ID="btnClear" ShowPostDiv="true" runat="server" Text="Button_Reset" AutoPostBack="true" ButtonImgUrl="../images/cancel.gif" Page_ClientValidate="false" Disable="false" OnClick="btnClear_Click" />
                    <cc2:Button ID="btnReturn" ShowPostDiv="true" runat="server" Text="Button_Return" ButtonImgUrl="../images/browse.gif" AutoPostBack="false" Page_ClientValidate="false" Disable="false" ValidateForm="false" ScriptContent="btnReturn_Click()" />
                </div>
                <div class="ManagerForm">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label1" Font-Bold="true" Font-Size="9pt" runat="server" Text="报表列表"></asp:Label></legend>
                        <!--gridview start-->
                        <div style="text-align: left">
                            <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="false" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" Width="800px" DataKeyNames="ReportID,GroupID,FieldID,FieldName,DataTypeID,HTMLTypeID">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            报表字段</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbIsShow" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            报表条件</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbCondition" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FieldLabel" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Left" HeaderText="字段名称" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            条件</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:PlaceHolder ID="placeHolder" runat="server" EnableViewState="false"></asp:PlaceHolder>
                                        </ItemTemplate>
                                        <ItemStyle Width="500px" HorizontalAlign="Left" VerticalAlign="Middle" />
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
