<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG500601.aspx.cs" Inherits="GOA.GG500601" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>附加动作</title>

    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
      // Add click handlers for buttons to show and hide modal popup on pageLoad
      function pageLoad() {
      }
      
      function CheckInputContent()
      {
        if(document.all.ddlCaculateType.value == "")
        {
          alert("请选择处理方式。");
          document.all.ddlCaculateType.focus();
          return false;
        }
        
        if(document.all.txtDataSourceName.value == "")
        {
          alert("请选择数据源。");
          document.all.txtDataSourceName.focus();
          return false;
        }
        
        if(document.all.ddlDataSourceTable.value == "")
        {
          alert("请选择数据对象。");
          document.all.ddlDataSourceTable.focus();
          return false;
        }
        
        if(document.all.ddlGroupID.value == "")
        {
          alert("请选择字段组。");
          document.all.ddlGroupID.focus();
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
        var url='GG60Choose.aspx?GUID=' + Math.random() ;
        var ret = window.showModalDialog(url,'GG60Choose','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        { 
          window.document.getElementById("txtDataSourceID").value =ret[0]; 
          window.document.getElementById("txtDataSourceName").value =ret[1];
          //document.all.form1.submit();
          __doPostBack("txtDataSourceName","");
        }
        return false;
      }
      
      function ShowParameter1(id)
      {
        var url = "GG50060101.aspx?GUID=" + Math.random() + "&opid=" + id;
        var ret = window.open(url, 'GG50060101', 'height=680,width=860,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no');
        return !(ret === undefined)
      }
      function ShowParameter2(id)
      {
        var url = "GG50060102.aspx?GUID=" + Math.random() + "&opid=" + id;
        var ret = window.open(url, 'GG50060102', 'height=680,width=860,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no');
        return !(ret === undefined)
      }
      function ShowParameter3(id)
      {
        var url = "GG50060103.aspx?GUID=" + Math.random() + "&opid=" + id;
        var ret = window.open(url, 'GG50060103', 'height=680,width=860,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no');
        return !(ret === undefined)
      }
      function ShowParameter4(id)
      {
        var url = "GG50060104.aspx?GUID=" + Math.random() + "&opid=" + id;
        var ret = window.open(url, 'GG50060104', 'height=680,width=860,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no');
        return !(ret === undefined)
      }
      function ShowParameter5(id)
      {
        var url = "GG50060105.aspx?GUID=" + Math.random() + "&opid=" + id;
        var ret = window.open(url, 'GG50060105', 'height=680,width=860,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no');
        return !(ret === undefined)
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
                        <asp:Label ID="lblBigTitle" runat="server" Text="创建动作"></asp:Label></legend>
                    <div class="clear">
                        <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                            <div class="formblk">
                                <input type="hidden" runat="server" id="txtAddInOPID" />
                                <input type="hidden" id="txtDataSourceID" runat="server" />
                                <div class="clear">
                                    <div class="oneline">
                                        <label class="char5">
                                            处理方式：</label><div class="iptblk">
                                                <cc2:DropDownList ID="ddlCaculateType" runat="server" Width="150" AutoPostBack="true" OnSelectedIndexChanged="ddlCaculateType_SelectedIndexChanged">
                                                </cc2:DropDownList>
                                                <span style="color: Red;">*</span></div>
                                    </div>
                                </div>
                                <div class="clear">
                                    <div class="half">
                                        <label class="char5">
                                            数据源：</label><div class="iptblk">
                                                <cc2:TextBox ID="txtDataSourceName" runat="server" Text="" OnTextChanged="txtDataSource_TextChanged" Width="145"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgDataSet" ToolTip="搜索" runat="server" OnClientClick="return btnDataSetClick();" /><span style="color: Red;">*</span></div>
                                    </div>
                                    <div class="half">
                                        <label class="char5">
                                            数据对象：</label><div class="iptblk">
                                                <cc2:DropDownList ID="ddlDataSourceTable" runat="server" Width="150">
                                                </cc2:DropDownList>
                                                <span style="color: Red;">*</span></div>
                                    </div>
                                </div>
                                <div class="clear">
                                    <div class="half">
                                        <label class="char5">
                                            字段组：</label><div class="iptblk">
                                                <cc2:DropDownList ID="ddlGroupID" runat="server" Width="150" AutoPostBack="true" OnSelectedIndexChanged="ddlGroupID_SelectedIndexChanged">
                                                </cc2:DropDownList>
                                                <span style="color: Red;">*</span></div>
                                    </div>
                                    <div class="half">
                                        <label class="char5">
                                            执行方式：</label><div class="iptblk">
                                                <cc2:DropDownList ID="ddlOPCycleType" runat="server" Width="150">
                                                </cc2:DropDownList>
                                                <span style="color: Red;">*</span></div>
                                    </div>
                                </div>
                                <div class="clear">
                                    <div class="oneline">
                                        <cc2:Button ID="btnAdd" runat="server" ButtonImgUrl="../images/add.gif" Enabled="false" ShowPostDiv="false" Text="Button_Add" ValidateForm="false" AutoPostBack="true" Width="80" OnClick="btnAdd_Click" ScriptContent="CheckInputContent()" />
                                        <cc2:Button ID="btnDel" runat="server" ButtonImgUrl="../images/del.gif" Enabled="false" ShowPostDiv="false" Text="Button_Del" ValidateForm="false" AutoPostBack="true" Width="100" OnClick="btnDel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="AddInOPID">
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
                            <asp:BoundField DataField="CaculateTypeN" HeaderText="处理方式" />
                            <asp:BoundField DataField="DataSourceN" HeaderText="数据源" />
                            <asp:BoundField DataField="DataSourceTable" HeaderText="数据对象" />
                            <asp:BoundField DataField="OPTimeN" HeaderText="操作时机" />
                            <asp:BoundField DataField="GroupName" HeaderText="字段组" />
                            <asp:BoundField DataField="OPCycleTypeN" HeaderText="执行方式" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    取值范围</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect" runat="server" Text="设定" CommandName="select" OnClientClick=<%#"ShowParameter1('"+Eval("AddInOPID")+"')"%> />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    对象值匹配</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect2" runat="server" Text="设定" CommandName="select" OnClientClick=<%#"ShowParameter2('"+Eval("AddInOPID")+"')"%> />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    对象值筛选</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect3" runat="server" Text="设定" CommandName="select" OnClientClick=<%#"ShowParameter3('"+Eval("AddInOPID")+"')"%> />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    SP参数</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect4" runat="server" Text="设定" CommandName="select" OnClientClick=<%#"ShowParameter4('"+Eval("AddInOPID")+"')"%> />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    触发条件</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect5" runat="server" Text="设定" CommandName="select" OnClientClick=<%#"ShowParameter5('"+Eval("AddInOPID")+"')"%> />
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
