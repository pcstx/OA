<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SingleUserSelect.aspx.cs" Inherits="GOA.SingleUserSelect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户选择</title>

    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
      function btnSelectClick(code,name)
      {
        var ret = new Array(2); 
        ret[0] = code; 
        ret[1] = name; 
        window.returnValue = ret; 
        window.close();
      }
      
       function btnDepartmentClick()
      {
        var url='DeptSelect.aspx?GUID=' + Math.random() ;
        var ret = window.showModalDialog(url,'DeptSelect','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        { 
          window.document.getElementById("txtQDeptID").value =ret[0]; 
          window.document.getElementById("txtQDeptName").value =ret[1]; 
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
                        <div class="con" id="Div2">
                            <div class="formblk">
                                <input type="hidden" id="txtQDeptID" runat="server" />
                                <div class="clear">
                                    <div class="half">
                                        <label class="char5">
                                            <!--label-->
                                            用户ID：</label><div class="iptblk">
                                                <!--textbox-->
                                                <cc2:TextBox ID="txtQUserID" runat="server" Width="90"></cc2:TextBox></div>
                                    </div>
                                    <div class="half">
                                        <label class="char5">
                                            <!--label-->
                                            用户姓名：</label><div class="iptblk">
                                                <!--textbox-->
                                                <cc2:TextBox ID="txtQUserName" runat="server" Width="90"></cc2:TextBox></div>
                                    </div>
                                </div>
                                <div class="clear">
                                    <div class="half">
                                        <label class="char5">
                                            <!--label-->
                                            所属部门：</label><div class="iptblk">
                                                <!--textbox-->
                                                <cc2:TextBox ID="txtQDeptName" runat="server" Width="90"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImageButton2" ToolTip="搜索" runat="server" OnClientClick="return btnDepartmentClick();" /></div>
                                    </div>
                                    <div class="half">
                                        <label class="char5">
                                            <!--label-->
                                            员工编号：</label><div class="iptblk">
                                                <!--textbox-->
                                                <cc2:TextBox ID="txtQUserCode" runat="server" Width="90"></cc2:TextBox></div>
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
                            <asp:Label ID="lblBigTitle" runat="server" Text="用户信息"></asp:Label></legend>
                        <div class="clear">
                            <div class="oneline">
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
                                    <!--page end-->
                                    <!--gridview start-->
                                    <!--gridview Browse mode start-->
                                    <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="true" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="UserSerialID">
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
                                                    <a style="color: Blue; cursor: hand" onclick="btnSelectClick('<%# DataBinder.Eval(Container, "DataItem.UserSerialID") %>','<%# DataBinder.Eval(Container, "DataItem.UserName") %>');">选择</a>
                                                </ItemTemplate>
                                                <ItemStyle Width="35px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UserID" HeaderText="用户帐号" />
                                            <asp:BoundField DataField="UserName" HeaderText="用户姓名" />
                                            <asp:BoundField DataField="UserTypeN" HeaderText="用户类型" />
                                            <asp:BoundField DataField="UserEmail" HeaderText="Email" />
                                            <asp:BoundField DataField="UserCode" HeaderText="员工编号" />
                                            <asp:BoundField DataField="DeptName" HeaderText="所属部门" />
                                            <asp:TemplateField HeaderText="是否可用" ItemStyle-Width="80px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkUseFlag" runat="server" />
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
