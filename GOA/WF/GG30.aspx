﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG30.aspx.cs" Inherits="GOA.GG30" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>表单管理</title>
    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>
    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
      // Add click handlers for buttons to show and hide modal popup on pageLoad
      function pageLoad() {
        $addHandler($get("btnAdd"), 'click', showAddModalPopupViaClient);
        $addHandler($get("btnQuery"), 'click', showQueryModalPopupViaClient);
      }
      function showAddModalPopupViaClient(ev) {
        ev.preventDefault();
        var modalPopupBehavior = $find('programmaticAddModalPopupBehavior');
        modalPopupBehavior.show();
      }
      function showQueryModalPopupViaClient(ev) {
        ev.preventDefault();
        var modalPopupBehavior = $find('programmaticQueryModalPopupBehavior');
        modalPopupBehavior.show();
      }
      
      function CheckInputContent()
      {
        if($("txtFormName").value.Trim() == "")
        {
          alert("请输入表单名称。");
          return false;
        }
        if($("ddlFormTypeID").value == "0")
        {
          alert("请选择表单分类。");
          return false;
        }
        
        if($("txtDisplayOrder").value.Trim() == "")
        {
          alert("请输入显示顺序。");
          return false;
        }
        if(!$("txtDisplayOrder").value.isDigit())
        {
          alert("显示顺序必须为数字。");
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
      
      function ShowDetail(id)
      {
        var url = "GG30Info.aspx?GUID=" + Math.random() + "&fmid=" + id;
        var ret = window.open(url, 'GG30Info', 'height=760,width=1000,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no');
        return !(ret === undefined)
      }
      function ShowDetail2(id)
      {
        var url = "GG30Info2.aspx?GUID=" + Math.random() + "&fmid=" + id;
        var ret = window.open(url, 'GG30Info', 'height=760,width=1000,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no');
        return !(ret === undefined)
      }
      function ShowDetail3(id)
      {
        var url = "GG30Info3.aspx?GUID=" + Math.random() + "&fmid=" + id;
        var ret = window.open(url, 'GG30Info', 'height=760,width=760,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no');
        return !(ret === undefined)
      }

      function viewAdd() {
          parent.addTabs("edit", "可视化表单设计器", "../eidtior/demo/editPage.htm" + "?t=" + (new Date()).valueOf());
      }
      
      function ShowView(FormID) {
          parent.addTabs("edit", "可视化表单设计器", "../eidtior/demo/editPage.htm?FormID=" + FormID + "&t=" + (new Date()).valueOf());
      }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <cc2:Button ID="btnQuery" runat="server" ButtonImgUrl="../images/search.gif" Enabled="false" ShowPostDiv="false" Text="Button_Search" ValidateForm="false" AutoPostBack="false" Width="80" />
                <cc2:Button ID="btnAdd" runat="server" ButtonImgUrl="../images/add.gif" Enabled="false" ShowPostDiv="false" Text="Button_Add" ValidateForm="false" AutoPostBack="false" Width="80" ScriptContent="SetViewState()" />
                <cc2:Button ID="btnDel" runat="server" ButtonImgUrl="../images/del.gif" Enabled="false" ShowPostDiv="false" Text="Button_Del" ValidateForm="false" AutoPostBack="true" Width="100" OnClick="btnDel_Click" />
               <button class="ManagerButton" onclick='parent.addTabs("edit", "可视化表单设计器", "../eidtior/demo/editPage.htm");'>可视化新增</button>
            </div>
            <div style="margin-left: 0px; margin-top: 0px;">
                <div class="ManagerForm">
                    <fieldset>
                        <legend style="background: url(images/legendimg.jpg) no-repeat 6px 50%;">
                            <asp:Label ID="lblBigTitle" runat="server" Text="表单管理"></asp:Label></legend>
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
                                    <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="FormID">
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
                                                    <asp:LinkButton ID="btnSelect" runat="server" Text="编辑" CommandName="select" CommandArgument='<%# Container.DataItemIndex%>' />
                                                  <asp:LinkButton ID="btnSelectView" runat="server" Text="可视化编辑" OnClientClick=<%#"ShowView('"+Eval("FormID")+"')"%> />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FormName" HeaderText="名称" />
                                            <asp:BoundField DataField="FormDesc" HeaderText="描述" />
                                            <asp:BoundField DataField="FormTypeName" HeaderText="表单分类" />
                                            <asp:BoundField DataField="DisplayOrder" HeaderText="显示顺序" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    表单字段</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnNodeField" runat="server" ImageUrl="../images/icon_enter.gif" OnClientClick=<%#"ShowDetail('"+Eval("FormID")+"')"%> />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    行规则</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnNodeField2" runat="server" ImageUrl="../images/icon_enter.gif" OnClientClick=<%#"ShowDetail2('"+Eval("FormID")+"')"%> />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    列规则</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnNodeField3" runat="server" ImageUrl="../images/icon_enter.gif" OnClientClick=<%#"ShowDetail3('"+Eval("FormID")+"')"%> />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="是否可用" ItemStyle-Width="80px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkUseflag" runat="server" />
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
    <asp:Button runat="server" ID="hiddenTargetControlForAddModalPopup" Style="display: none" />
    <cc1:ModalPopupExtender runat="server" ID="programmaticAddModalPopup" BehaviorID="programmaticAddModalPopupBehavior" TargetControlID="hiddenTargetControlForAddModalPopup" PopupControlID="programmaticPopupControlForAdd" BackgroundCssClass="modalBackground" DropShadow="False" CancelControlID="AddCancelButton" PopupDragHandleControlID="programmaticAddPopupDragHandle" RepositionMode="RepositionOnWindowScroll">
    </cc1:ModalPopupExtender>
    <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopupControlForAdd" Style="display: none; width: 650px; padding: 0 0px 0px 0px">
        <div id="programmaticAddPopupDragHandle" class="programmaticPopupDragHandle">
            <div class="h2blk">
                <h2 id="H2_1">
                    <asp:Label ID="Label1" runat="server" Text="表单管理"></asp:Label></h2>
            </div>
            <cc2:Button ID="AddCancelButton" ShowPostDiv="false" runat="server" Text="Label_NULL" CSS="CancelButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="false" ToolTip="Close" Width="12" />
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div>
                        <cc2:Button ID="btnSubmit" ShowPostDiv="true" runat="server" Text="Label_NULL" CSS="SaveButtonCSS" AutoPostBack="true" ButtontypeMode="Normal" OnClick="hideModalPopupViaServer_Click" Page_ClientValidate="false" Disable="false" Width="26" ScriptContent="CheckInputContent()" />
                        <cc2:Button ID="btnSubmitAndClose" ShowPostDiv="true" runat="server" Text="Label_NULL" CSS="SaveAndCloseCSS" AutoPostBack="true" ButtontypeMode="Normal" OnClick="hideModalPopupViaServer_Click" Page_ClientValidate="false" Disable="false" Width="26" ScriptContent="CheckInputContent()" />
                    </div>
                    <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                        <input type="hidden" id="txtFormID" runat="server" />
                        <div class="con">
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        名称：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtFormName" runat="server" Width="150"></cc2:TextBox><span style="color: Red;">*</span></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        描述：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtFormDesc" runat="server" Width="300"></cc2:TextBox></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        表单分类：</label><div class="iptblk">
                                            <cc2:DropDownList ID="ddlFormTypeID" runat="server" Width="150">
                                            </cc2:DropDownList>
                                            <span style="color: Red;">*</span></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        显示顺序：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtDisplayOrder" runat="server" Width="150"></cc2:TextBox><span style="color: Red;">*</span></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        <!--label-->
                                        是否可用：</label><div class="iptblk">
                                            <asp:CheckBox ID="chkUseFlag" runat="server" Checked="true" /></div>
                                </div>
                            </div>
                            <div class="clear">
                                <label class="char5">
                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></label></div>
                        </div>
                    </div>
                    <!--validate start-->
                    <!--validate end-->
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    <asp:Button runat="server" ID="hiddenTargetControlForQueryModalPopup" Style="display: none" />
    <cc1:ModalPopupExtender runat="server" ID="programmaticQueryModalPopup" BehaviorID="programmaticQueryModalPopupBehavior" TargetControlID="hiddenTargetControlForQueryModalPopup" PopupControlID="programmaticPopupQuery" BackgroundCssClass="modalBackground" DropShadow="False" CancelControlID="QueryCancelButton" PopupDragHandleControlID="programmaticQueryPopupDragHandle" RepositionMode="RepositionOnWindowScroll">
    </cc1:ModalPopupExtender>
    <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopupQuery" Style="display: none; width: 650px; padding: 0 0px 0px 0px">
        <div id="programmaticQueryPopupDragHandle" class="programmaticPopupDragHandle">
            <div class="h2blk">
                <h2 id="H2_2">
                    <asp:Label ID="lblSearchTitle" runat="server" Text="搜索框"></asp:Label></h2>
            </div>
            <cc2:Button ID="QueryCancelButton" ShowPostDiv="false" runat="server" Text="Label_NULL" CSS="CancelButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="false" ToolTip="Close" Width="12" />
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="edit">
                        <cc2:Button ID="btnSubmitSearch" ShowPostDiv="true" runat="server" Text="Label_NULL" CSS="SearchButtonCSS" AutoPostBack="true" ButtontypeMode="Normal" OnClick="btnSearchRecord_Click" Page_ClientValidate="true" Disable="false" Width="16" />
                    </div>
                    <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                        <div class="con">
                            <div id="Div5">
                                <div class="formblk">
                                    <div class="textblk">
                                        <div class="clear">
                                            <div class="third1">
                                                <label class="char5">
                                                    名称：</label><div class="iptblk">
                                                        <cc2:TextBox ID="txtQFormName" runat="server" Width="90"></cc2:TextBox></div>
                                            </div>
                                            <div class="third2">
                                                <label class="char5">
                                                    描述：</label><div class="iptblk">
                                                        <cc2:TextBox ID="txtQFormDesc" runat="server" Width="90"></cc2:TextBox></div>
                                            </div>
                                            <div class="third3">
                                                <label class="char5">
                                                    表单分类：</label><div class="iptblk">
                                                        <cc2:DropDownList ID="ddlQFormTypeID" runat="server" Width="90">
                                                        </cc2:DropDownList>
                                                    </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    </form>
</body>
</html>
