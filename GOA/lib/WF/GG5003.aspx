<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG5003.aspx.cs" Inherits="GOA.GG5003" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工作流管理</title>

    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
      // Add click handlers for buttons to show and hide modal popup on pageLoad
      function pageLoad() {
        $addHandler($get("btnAdd"), 'click', showAddModalPopupViaClient);
      }
      function showAddModalPopupViaClient(ev) {
        ev.preventDefault();
        var modalPopupBehavior = $find('programmaticAddModalPopupBehavior');
        modalPopupBehavior.show();
      }
           
      function CheckInputContent()
      {
        if(document.all.ddlStartNodeID.value.trim() == "0")
        {
          alert("请选择起始节点。");
          document.all.ddlStartNodeID.focus();
          return false;
        }
        if(document.all.txtLinkName.value.trim() == "")
        {
          alert("请输入出口名称。");
          document.all.txtLinkName.focus();
          return false;
        }
        if(document.all.ddlTargetNodeID.value.trim() == "")
        {
          alert("请选择目标节点。");
          document.all.ddlTargetNodeID.focus();
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
      
      function ShowNodeCondition(id)
      {
        var url = "GG500301.aspx?GUID=" + Math.random() + "&id="+id;
        var ret = window.open(url, 'GG500201', 'height=680,width=760,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no');
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
            <div>
                <cc2:Button ID="btnAdd" runat="server" ButtonImgUrl="../images/add.gif" Enabled="false" ShowPostDiv="false" Text="Button_Add" ValidateForm="false" AutoPostBack="false" Width="80" ScriptContent="SetViewState()" />
                <cc2:Button ID="btnDel" runat="server" ButtonImgUrl="../images/del.gif" Enabled="false" ShowPostDiv="false" Text="Button_Del" ValidateForm="false" AutoPostBack="true" Width="100" OnClick="btnDel_Click" />
            </div>
            <div style="margin-left: 0px; margin-top: 0px;">
                <div class="ManagerForm">
                    <fieldset>
                        <legend style="background: url(images/legendimg.jpg) no-repeat 6px 50%;">
                            <asp:Label ID="lblBigTitle" runat="server" Text="出口信息"></asp:Label></legend>
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
                                    <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="LinkID">
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
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="StartNodeName" HeaderText="起始节点" />
                                            <asp:TemplateField HeaderText="是否退回转向" ItemStyle-Width="80px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsRejected" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    条件</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnNodeCondition" runat="server" ImageUrl="../images/icon_enter.gif" OnClientClick=<%#"ShowNodeCondition('"+Eval("LinkID")+"')"%> />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="LinkName" HeaderText="出口名称" />
                                            <asp:BoundField DataField="TargetNodeName" HeaderText="目标节点" />
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
    <cc1:ModalPopupExtender runat="server" ID="programmaticAddModalPopup" BehaviorID="programmaticAddModalPopupBehavior" TargetControlID="hiddenTargetControlForAddModalPopup" PopupControlID="programmaticPopupControlForAdd" BackgroundCssClass="modalBackground" DropShadow="False" CancelControlID="AddCancelButton" PopupDragHandleControlID="programmaticAddPopupDragHandle" RepositionMode="RepositionOnWindowScroll">
    </cc1:ModalPopupExtender>
    <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopupControlForAdd" Style="display: none; width: 650px; padding: 0 0px 0px 0px">
        <div id="programmaticAddPopupDragHandle" class="programmaticPopupDragHandle">
            <div class="h2blk">
                <h2 id="H2_1">
                    <asp:Label ID="Label1" runat="server" Text="节点管理"></asp:Label></h2>
            </div>
            <cc2:Button ID="AddCancelButton" ShowPostDiv="false" runat="server" Text="Label_NULL" CSS="CancelButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="false" ToolTip="Close" Width="12" />
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div>
                        <cc2:Button ID="btnSubmit" ShowPostDiv="true" runat="server" Text="Label_NULL" CSS="SaveButtonCSS" AutoPostBack="true" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="false" Width="26" ScriptContent="CheckInputContent()" />
                        <cc2:Button ID="btnSubmitAndClose" ShowPostDiv="true" runat="server" Text="Label_NULL" CSS="SaveAndCloseCSS" AutoPostBack="true" ButtontypeMode="Normal" OnClick="hideModalPopupViaServer_Click" Page_ClientValidate="false" Disable="false" Width="26" ScriptContent="CheckInputContent()" />
                    </div>
                    <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                        <input type="hidden" id="txtLinkID" runat="server" />
                        <div class="con">
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        起始节点：</label><div class="iptblk">
                                            <cc2:DropDownList ID="ddlStartNodeID" runat="server" AutoPostBack="false" Width="150">
                                            </cc2:DropDownList>
                                            <span style="color: Red;">*</span></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        是否退回转向：</label><div class="iptblk">
                                            <asp:CheckBox ID="chkIsRejected" runat="server" /><span style="color: Red;">*</span></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        出口名称：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtLinkName" runat="server" Width="150"></cc2:TextBox><span style="color: Red;">*</span></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        目标节点：</label><div class="iptblk">
                                            <cc2:DropDownList ID="ddlTargetNodeID" runat="server" Width="150">
                                            </cc2:DropDownList>
                                            <span style="color: Red;">*</span></div>
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
    </form>
</body>
</html>
