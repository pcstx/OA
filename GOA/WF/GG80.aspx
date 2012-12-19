<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG80.aspx.cs" Inherits="GOA.GG80" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>基本验证方式</title>

    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    	  
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
        if($("txtValidTypeDesc").value.Trim() == "")
        {
          alert("验证方式描述 ！");
          return false;
        }
        if($("txtValidErrorMsg").value == "")
        {
          alert("验证不通过时的提示信息！");
          return false;
        }
        
        if($("txtValidRule").value.Trim() == "")
        {
          alert("验证用的正则表达式！");
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
                <cc2:Button ID="btnQuery" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false" ShowPostDiv="false" Text="Button_Search" ValidateForm="false" AutoPostBack="false" />
                <cc2:Button ID="btnAdd" runat="server" ShowPostDiv="false" ValidateForm="false" Text="Button_Add" Disable="false" ScriptContent="SetViewState()" AutoPostBack="false" ButtonImgUrl="../images/add.gif" />
                <cc2:Button ID="btnDel" runat="server" ButtonImgUrl="../images/del.gif" Enabled="false" ShowPostDiv="false" Text="Button_Del" ValidateForm="false" AutoPostBack="true" Width="100" OnClick="btnDel_Click" />
            </div>
            <div style="margin-left: 0px; margin-top: 0px;">
                <div class="ManagerForm">
                    <fieldset>
                        <legend style="background: url(../images/legendimg.jpg) no-repeat 6px 50%;">
                            <asp:Label ID="lblBigTitle" runat="server" Text="基本验证方式"></asp:Label></legend>
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
                                    <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" BoundRowDoubleClickCommandName="" HorizontalAlign="Left" MergeCells="" DataKeyNames="ValidTypeID">
                                        <Columns>
                                            <asp:BoundField DataField="ValidTypeID" HeaderText="验证方式ID" />
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
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ValidTypeDesc" HeaderText="验证方式描述" />
                                            <asp:BoundField DataField="ValidErrorMsg" HeaderText="验证不通过时的提示信息" />
                                            <asp:BoundField DataField="ValidRule" HeaderText="验证用的正则表达式" />
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
    <cc1:ModalPopupExtender runat="server" ID="programmaticAddModalPopup" BehaviorID="programmaticAddModalPopupBehavior" TargetControlID="hiddenTargetControlForAddModalPopup" PopupControlID="programmaticPopup" BackgroundCssClass="modalBackground" DropShadow="False" CancelControlID="AddCancelButton" PopupDragHandleControlID="programmaticPopupDragHandle" RepositionMode="RepositionOnWindowScroll">
    </cc1:ModalPopupExtender>
    <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup" Style="display: none; width: 650px; padding: 0 0px 0px 0px">
        <div id="programmaticPopupDragHandle" class="programmaticPopupDragHandle">
            <div class="h2blk">
                <h2 id="Profile_title">
                    <asp:Label ID="lblTitle" runat="server" Text="基本验证方式"></asp:Label></h2>
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
                    <div class="conblk2" id="ProfileContainer" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                        <input type="hidden" id="txtValidTypeID" runat="server" />
                        <div class="con">
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char9">
                                        验证方式描述</label>
                                    <div class="iptblk">
                                        <cc2:TextBox ID="txtValidTypeDesc" runat="server" Width="150"></cc2:TextBox><span style="color: Red;">*</span></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="online">
                                    <label class="char9">
                                        验证不通过时的提示信息</label>
                                    <div class="iptblk">
                                        <cc2:TextBox ID="txtValidErrorMsg" runat="server" Width="150"></cc2:TextBox><span style="color: Red;">*</span></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="online">
                                    <label class="char9">
                                        验证用的正则表达式</label>
                                    <div class="iptblk">
                                        <cc2:TextBox ID="txtValidRule" runat="server" Width="150"></cc2:TextBox><span style="color: Red;">*</span></div>
                                </div>
                            </div>
                            <div class="clear">
                                <label class="char5">
                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></label>
                            </div>
                        </div>
                    </div>
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
                                                    验证方式描述：</label><div class="iptblk">
                                                        <cc2:TextBox ID="txtQValidTypeDesc" runat="server" Width="90"></cc2:TextBox></div>
                                            </div>
                                            <div class="third2">
                                                <label class="char5">
                                                    验证不通过时的提示信息：</label><div class="iptblk">
                                                        <cc2:TextBox ID="txtQValidErrorMsg" runat="server" Width="90"></cc2:TextBox></div>
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
