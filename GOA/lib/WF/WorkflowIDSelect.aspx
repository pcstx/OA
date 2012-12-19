<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkflowIDSelect.aspx.cs" Inherits="GOA.WorkflowIDSelect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>流程选择</title>

    <script language="javascript" type="text/javascript">
      function btnSelectClick(code,name)
      {
        var ret = new Array(2); 
        ret[0] = code; 
        ret[1] = name; 
        window.returnValue = ret; 
        window.close();
      return false;
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
                    <legend>
                        <asp:Label ID="Label12" runat="server" Text="搜索框"></asp:Label></legend>
                    <div class="conblk2" id="Div1" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                        <input type="hidden" id="txtFormID" runat="server" />
                        <div class="con" id="Div2">
                            <div class="formblk">
                                <div class="clear">
                                    <div class="half">
                                        <label class="char5">
                                            <!--label-->
                                            流程名称：</label><div class="iptblk">
                                                <!--textbox-->
                                                <cc2:TextBox ID="txtQWorkflowName" runat="server" Width="90"></cc2:TextBox></div>
                                    </div>
                                </div>
                                <div class="clear">
                                    <div class="half">
                                        <label class="char5">
                                            流程类型：</label><div class="iptblk">
                                                <cc2:DropDownList ID="ddlFlowTypeID" runat="server" Width="150">
                                                </cc2:DropDownList>
                                                <span style="color: Red;">*</span></div>
                                    </div>
                                    <div class="half">
                                        <label class="char5">
                                            使用表单：</label><div class="iptblk">
                                                <cc2:TextBox ID="txtFormN" runat="server" Width="150"></cc2:TextBox><span style="color: Red;">*</span><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgForm" ToolTip="搜索" runat="server" OnClientClick="return btnFormClick();" /></div>
                                    </div>
                                </div>
                                <div class="clear">
                                    <div class="half">
                                        <label class="char5">
                                            <!--label-->
                                        </label>
                                        <div class="iptblk">
                                            <!--textbox-->
                                            <cc2:Button ID="btnSearch" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false" ShowPostDiv="true" Text="Button_Search" ValidateForm="false" AutoPostBack="true" OnClick="btnSearchRecord_Click" />
                                        </div>
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
                            <asp:Label ID="lblBigTitle" runat="server" Text="流程信息"></asp:Label></legend>
                        <div class="clear">
                            <div class="oneline">
                                <fieldset>
                                    <!--page start-->
                                    <cc2:Button ID="btnSelect" runat="server" Enabled="false" ShowPostDiv="false" Text="Button_Select" ValidateForm="false" AutoPostBack="true" Width="80" OnClick="btnSelect_Click" />
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
                                    <!--page end-->
                                    <!--gridview start-->
                                    <!--gridview Browse mode start-->
                                    <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="WorkflowID">
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
                                                    <a style="color: Blue; cursor: hand" onclick="btnSelectClick('<%# DataBinder.Eval(Container, "DataItem.WorkflowID") %>','<%# DataBinder.Eval(Container, "DataItem.WorkflowName") %>');">选择</a>
                                                </ItemTemplate>
                                                <ItemStyle Width="40px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="WorkflowID" HeaderText="流程编号" />
                                            <asp:BoundField DataField="WorkflowName" HeaderText="流程名称" />
                                            <asp:BoundField DataField="WorkflowDesc" HeaderText="描述" />
                                            <asp:BoundField DataField="FlowTypeN" HeaderText="类型" />
                                            <asp:BoundField DataField="FormN" HeaderText="表单" />
                                            <asp:TemplateField HeaderText="是否可用" ItemStyle-Width="80px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsValid" runat="server" />
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
                                    <!--gridview Browse mode end-->
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
