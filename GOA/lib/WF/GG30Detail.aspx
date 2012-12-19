<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG30Detail.aspx.cs" Inherits="GOA.GG30Detail" %>

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
      function btnFieldDictClick()
      {
        var FieldTypeID = document.all.txtFieldTypeID.value
        var url='GG20Select.aspx?FT='+FieldTypeID+'&GUID=' + Math.random() ;
        var ret = window.showModalDialog(url,'GG20Select','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        { 
          window.document.getElementById("txtFieldID").value =ret[0]; 
          window.document.getElementById("txtFieldNameDesc").value =ret[1]; 
        }
        return false;
      }
      
      function btnDataSetClick(RowIndex)
      {
        var url='GG70Select.aspx?GUID=' + Math.random() ;
        var ret = window.showModalDialog(url,'GG70Select','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px'); 

        if (ret != null) 
        {
          window.document.getElementById("txtDataSetID"+RowIndex).value =ret[0]; 
          window.document.getElementById("GridView1_ctl"+RowIndex+"_DataSetN").value =ret[1]; 
        }
        return false;
      }
      
      function ShowGroupLineField(fmid,fdid)
      {
        var url = "GG30GroupLineFieldMap.aspx?GUID=" + Math.random() + "&fmid="+fmid + "&fdid="+fdid;
        var ret = window.open(url, 'GG30GroupLineFieldMap', 'height=750,width=1000,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no');
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
                    <legend>
                        <asp:Label ID="Label12" runat="server" Text="输入框"></asp:Label></legend>
                    <div class="conblk2" id="Div1" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                        <div class="con" id="Div2">
                            <div class="formblk">
                                <input type="hidden" id="txtFieldID" runat="server" />
                                <input type="hidden" id="txtFieldTypeID" runat="server" />
                                <asp:PlaceHolder runat="server" ID="PHHiddenField"></asp:PlaceHolder>
                                <div class="clear">
                                    <div class="oneline">
                                        <label class="char5" runat="server" id="lblFieldTypeName">
                                            <!--label-->
                                            主字段：</label><div class="iptblk">
                                                <!--textbox-->
                                                <cc2:TextBox ID="txtFieldNameDesc" runat="server" Width="400"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgFieldDict" ToolTip="搜索" runat="server" OnClientClick="return btnFieldDictClick();" /><cc2:Button ID="btnAdd" runat="server" ButtonImgUrl="../images/add.gif" Enabled="false" ShowPostDiv="false" Text="Button_Add" ValidateForm="false" AutoPostBack="true" Width="80" OnClick="btnAdd_Click" /></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div style="margin-left: 0px; margin-top: 0px;">
                <div class="ManagerForm">
                    <fieldset>
                        <legend style="background: url(images/legendimg.jpg) no-repeat 6px 50%;">
                            <asp:Label ID="lblBigTitle" runat="server" Text="已选主字段"></asp:Label></legend>
                        <div class="clear">
                            <div class="oneline">
                                <fieldset>
                                    <!--page start-->
                                    <div class="pager">
                                        <div class="PagerText">
                                            <cc2:Button ID="btnDel" runat="server" ButtonImgUrl="../images/del.gif" ShowPostDiv="false" ValidateForm="false" Text="Button_Del" AutoPostBack="true" Width="100" OnClick="btnDel_Click" />
                                            <cc2:Button ID="btnSubmit" runat="server" ButtonImgUrl="../images/submit.gif" ShowPostDiv="false" ValidateForm="false" Text="Button_DafaultSubmit" AutoPostBack="true" Width="80" OnClick="btnSubmit_Click" />
                                        </div>
                                    </div>
                                    <!--page end-->
                                    <asp:Panel ID="Panel1" runat="server" Width="100%" Height="100%" ScrollBars="Auto" >
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
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        选择</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Item" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="35px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FieldID" HeaderText="字段编号" Visible="false" />
                                                <asp:BoundField DataField="FieldName" HeaderText="字段名称" />
                                                <asp:BoundField DataField="FieldDesc" HeaderText="字段描述" />
                                                <asp:BoundField DataField="HTMLTypeN" HeaderText="字段表现形式" />
                                                <asp:TemplateField HeaderText="字段显示名">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="FieldLabel" runat="server" Width="100" Text='<%# DataBinder.Eval(Container, "DataItem.FieldLabel")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="显示样式">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="CSSStyle" runat="server" DataValueField="CSSStyleID" DataTextField="CSSStyleClass" DataSource="<%# dtCSSStyle()%>">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="对应数据集">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="DataSetN" runat="server" Width="100" Text='<%# DataBinder.Eval(Container, "DataItem.DataSetName")%>'></asp:TextBox>
                                                        <asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgDataSet" ToolTip="搜索" runat="server" OnClientClick="return btnDataSetClick('02');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="对应明细组">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="TargetGroupID" runat="server" DataValueField="GroupID" DataTextField="GroupName" DataSource="<%# dtDetailFieldGroup()%>">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="字段匹配">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnGroupLineField" runat="server" ImageUrl="../images/icon_enter.gif" OnClientClick='<%# "ShowGroupLineField("+Eval("FormID")+","+Eval("FieldID")+")" %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="显示顺序">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="DisplayOrder" runat="server" Width="50" Text='<%# DataBinder.Eval(Container, "DataItem.DisplayOrder")%>'></asp:TextBox>
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
                                            <FixRowColumn FixRowType="Header"  TableHeight="450px" TableWidth="100%" FixColumns="" />
                                        </yyc:SmartGridView>
                                        <!--grid view end-->
                                    </asp:Panel>
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
