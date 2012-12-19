<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG500203.aspx.cs" Inherits="GOA.GG500203" %>

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
        if(document.all.ddlFieldIDTo.value == "0")
        {
          alert("请选择赋值字段。");
          document.all.ddlFieldIDTo.focus();
          return false;
        }
//        if(document.all.txtDataSetName.value == "")
//        {
//          alert("请选择数据集。");
//          document.all.txtDataSetName.focus();
//          return false;
//        }
//        if(document.all.ddlValueColumn.value == "")
//        {
//          alert("请选择值字段。");
//          document.all.ddlValueColumn.focus();
//          return false;
//        }
//        if(document.all.txtDataSourceName.value == "")
//        {
//          alert("请选择数据源。");
//          document.all.txtDataSourceName.focus();
//          return false;
//        }
//        if(document.all.ddlStorageProcedure.value == "")
//        {
//          alert("请选择存储过程。");
//          document.all.ddlStorageProcedure.focus();
//          return false;
//        }
//        if(document.all.ddlOutputParameter.value == "")
//        {
//          alert("请选择输出参数。");
//          document.all.ddlOutputParameter.focus();
//          return false;
//        }
//        return true;
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
      
      function btnDataSourceClick()
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
      
      function ShowParameter(id)
      {
        var url = "GG50020301.aspx?GUID=" + Math.random() + "&opid=" + id;
        var ret = window.open(url, 'GG50020301', 'height=680,width=760,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no');
        return !(ret === undefined)
      }
       
      function ShowParameter2(id)
      {
        var url = "GG50020302.aspx?GUID=" + Math.random() + "&opid=" + id;
        var ret = window.open(url, 'GG50020302', 'height=680,width=760,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no');
        return !(ret === undefined)
      }
       
      function ShowParameter3(id)
      {
        var url = "GG50020303.aspx?GUID=" + Math.random() + "&opid=" + id;
        var ret = window.open(url, 'GG50020303', 'height=680,width=760,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no');
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
                        <asp:Label ID="lblBigTitle" runat="server" Text="创建表达式"></asp:Label></legend>
                    <div class="clear">
                        <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                            <div class="formblk">
                                <input type="hidden" runat="server" id="txtAddInOPID" />
                                <input type="hidden" id="txtDataSetID" runat="server" />
                                <input type="hidden" id="txtDataSourceID" runat="server" />
                                <div class="clear">
                                    <div class="oneline">
                                        <label class="char5">
                                            赋值字段：</label><div class="iptblk">
                                                <cc2:DropDownList ID="ddlFieldIDTo" runat="server" Width="150" AutoPostBack="true" OnSelectedIndexChanged="ddlFieldIDTo_SelectedIndexChanged">
                                                </cc2:DropDownList>
                                                <span style="color: Red;">*</span></div>
                                    </div>
                                </div>
                                <div runat="server" id="divOperationCulculate">
                                    <div class="clear">
                                        <div class="oneline">
                                            <asp:RadioButtonList runat="server" ID="rblAddInOperationType" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblAddInOperationType_SelectedIndexChanged">
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
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
                                                    主字段：</label><div class="iptblk">
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
                                                    常量：</label><div class="iptblk">
                                                        <cc2:TextBox ID="txtConstantValue" runat="server" Width="50"></cc2:TextBox><asp:Button ID="btnConstant" runat="server" OnClick="btnConstant_Click" Text="加入常量" /></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="divOperationDataSet">
                                        <div class="clear">
                                            <div class="half">
                                                <label class="char5">
                                                    数据集：</label><div class="iptblk">
                                                        <cc2:TextBox ID="txtDataSetName" runat="server" Text="" OnTextChanged="txtDataSet_TextChanged" Width="80"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgDataSet" ToolTip="搜索" runat="server" OnClientClick="return btnDataSetClick();" /><span style="color: Red;">*</span></div>
                                            </div>
                                            <div class="half">
                                                <label class="char5">
                                                    值字段：</label><div class="iptblk">
                                                        <cc2:DropDownList ID="ddlValueColumn" runat="server">
                                                        </cc2:DropDownList>
                                                        <span style="color: Red;">*</span></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="divOperationDataSource">
                                        <div class="clear">
                                            <div class="half">
                                                <label class="char5">
                                                    数据源：</label><div class="iptblk">
                                                        <cc2:TextBox ID="txtDataSourceName" runat="server" Text="" OnTextChanged="txtDataSource_TextChanged" Width="80"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImageButton1" ToolTip="搜索" runat="server" OnClientClick="return btnDataSourceClick();" /><span style="color: Red;">*</span></div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                            <div class="half">
                                                <label class="char5">
                                                    存储过程：</label><div class="iptblk">
                                                        <cc2:DropDownList ID="ddlStorageProcedure" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStorageProcedure_SelectedIndexChanged">
                                                        </cc2:DropDownList>
                                                        <span style="color: Red;">*</span></div>
                                            </div>
                                            <div class="half">
                                                <label class="char5">
                                                    输出参数：</label><div class="iptblk">
                                                        <cc2:DropDownList ID="ddlOutputParameter" runat="server">
                                                        </cc2:DropDownList>
                                                        <span style="color: Red;">*</span></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="divOperationBrowse">
                                    <input runat="server" type="hidden" id="txtBrowseValue" />
                                    <div class="clear">
                                        <div class="oneline">
                                            <label class="char5">
                                                使用表单：</label><div class="iptblk">
                                                    <cc2:TextBox ID="txtBrowseText" runat="server" Width="150"></cc2:TextBox><span style="color: Red;">*</span><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgBrowse" ToolTip="搜索" runat="server" OnClientClick="return btnBrowseClick('txtBrowseValue','txtBrowseText','UserSelect.aspx');" /></div>
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
                            <asp:BoundField DataField="FieldLabel" HeaderText="目标字段" />
                            <asp:BoundField DataField="OPTimeN" HeaderText="操作时机" />
                            <asp:BoundField DataField="CaculateTypeN" HeaderText="计算方式" />
                            <asp:BoundField DataField="CaculateValueN" HeaderText="计算值" />
                            <asp:BoundField DataField="OPCondition" HeaderText="条件" Visible="false" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    数据集</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="txtDataSetN" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DataSetN")%>'></asp:Label>
                                    <asp:LinkButton ID="btnSelect" runat="server" Text="参数" CommandName="select" OnClientClick=<%#"ShowParameter('"+Eval("AddInOPID")+"')"%> />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ValueField" HeaderText="值字段" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    数据源</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="txtDataSourceN" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DataSourceN")%>'></asp:Label>
                                    <asp:LinkButton ID="btnSelectDataSource" runat="server" Text="参数" CommandName="select" OnClientClick=<%#"ShowParameter3('"+Eval("AddInOPID")+"')"%> />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="StorageProcedure" HeaderText="存储过程" />
                            <asp:BoundField DataField="OutputParameter" HeaderText="输出参数" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    触发条件</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect2" runat="server" Text="设定" CommandName="select" OnClientClick=<%#"ShowParameter2('"+Eval("AddInOPID")+"')"%> />
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
