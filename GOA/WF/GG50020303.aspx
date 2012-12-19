<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG50020303.aspx.cs" Inherits="GOA.GG50020303" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SP参数</title>

    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
      // Add click handlers for buttons to show and hide modal popup on pageLoad
      function pageLoad() {
      }
      
      function CheckInputContent()
      {
        if(document.all.ddlParameterList.value == "")
        {
          alert("请选择目标参数。");
          document.all.ddlParameterList.focus();
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
      
      function btnBrowseClick(ctrValue,ctrText, urllink)
      {
        var url=urllink+'?'+Math.random();
        var ret = window.showModalDialog(url,'urllink','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        { 
          window.document.getElementById(ctrValue).value =ret[0]; 
          window.document.getElementById(ctrText).value =ret[1]; 
        }
        return false;
      }
      
      function btnDataSetClick()
      {
        var url='GG70Select.aspx?GUID=' + Math.random() ;
        var ret = window.showModalDialog(url,'GG70Select','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        { 
          window.document.getElementById("txtDataSetID").value =ret[0]; 
          window.document.getElementById("txtDataSetName").value =ret[1]; 
          //document.all.form1.submit();
          __doPostBack("txtDataSetName","");
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
                    <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                        <div class="formblk">
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        目标参数：</label><div class="iptblk">
                                            <cc2:DropDownList ID="ddlParameterList" runat="server" Width="150">
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
                                <div class="half">
                                    <label class="char5">
                                        主字段：</label><div class="iptblk">
                                            <asp:ListBox ID="lbFieldList" runat="server" Width="150" Rows="6" AutoPostBack="true" OnSelectedIndexChanged="lbFieldList_SelectedIndexChanged"></asp:ListBox>
                                            <span style="color: Red;">单击选择一字段</span></div>
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
                                        常量：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtConstantValue" runat="server" Width="50"></cc2:TextBox><asp:Button ID="btnConstant" runat="server" OnClick="btnConstant_Click" Text="加入常量" /></div>
                                </div>
                            </div>
                            <div class="clear">
                                <cc2:Button ID="btnUndo" runat="server" ButtonImgUrl="../images/arrow_undo.gif" Enabled="false" ShowPostDiv="false" Text="Button_Undo" Page_ClientValidate="false" AutoPostBack="true" Width="80" OnClick="btnUndo_Click" />
                                <cc2:Button ID="btnAdd" runat="server" ButtonImgUrl="../images/add.gif" Enabled="false" ShowPostDiv="false" Text="Button_Add" ValidateForm="false" AutoPostBack="true" Width="80" OnClick="btnAdd_Click" ScriptContent="CheckInputContent()" />
                                <cc2:Button ID="btnDel" runat="server" ButtonImgUrl="../images/del.gif" Enabled="false" ShowPostDiv="false" Text="Button_Del" ValidateForm="false" AutoPostBack="true" Width="100" OnClick="btnDel_Click" />
                            </div>
                        </div>
                    </div>
                    <legend style="background: url(images/legendimg.jpg) no-repeat 6px 50%;">
                        <asp:Label ID="lblBigTitle" runat="server" Text="存储过程参数一览"></asp:Label></legend>
                    <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="SpParameter">
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
                            <asp:BoundField DataField="SpParameter" HeaderText="参数名" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    参数值</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="txtTartgetValueN" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TartgetValueN")%>'></asp:Label>
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
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
