<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG40.aspx.cs" Inherits="GOA.GG40" %>

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
      var styleToSelect;
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
        if(document.all.txtWorkflowName.value.trim() == "")
        {
          alert("请输入流程名称。");
          document.all.txtWorkflowName.focus();
          return false;
        }
        if(document.all.txtWorkflowDesc.value.trim() == "")
        {
          alert("请输入流程描述。");
          document.all.txtWorkflowDesc.focus();
          return false;
        }
        if(document.all.ddlFlowTypeID.value.trim() == "")
        {
          alert("请选择流程类型。");
          document.all.ddlFlowTypeID.focus();
          return false;
        }
        if(document.all.txtFormID.value.trim() == "")
        {
          alert("请选择使用表单。");
          document.all.ddlFormID.focus();
          return false;
        }
        if(document.all.txtDisplayOrder.value.trim() == "")
        {
          alert("请输入显示顺序。");
          document.all.txtDisplayOrder.focus();
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
      
      function ShowNodesDetail(id)
      {
        var url = "GG5002.aspx?GUID=" + Math.random() + "&id="+id;
        window.location=url;
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
            <div>
                <cc2:Button ID="btnQuery" runat="server" ButtonImgUrl="../images/search.gif" Enabled="false" ShowPostDiv="false" Text="Button_Search" ValidateForm="false" AutoPostBack="false" Width="80" />
                <cc2:Button ID="btnAdd" runat="server" ButtonImgUrl="../images/add.gif" Enabled="false" ShowPostDiv="false" Text="Button_Add" ValidateForm="false" AutoPostBack="false" Width="80" ScriptContent="SetViewState()" />
                <cc2:Button ID="btnDel" runat="server" ButtonImgUrl="../images/del.gif" Enabled="false" ShowPostDiv="false" Text="Button_Del" ValidateForm="false" AutoPostBack="true" Width="100" OnClick="btnDel_Click" />
            </div>
            <div style="margin-left: 0px; margin-top: 0px;">
                <div class="ManagerForm">
                    <fieldset>
                        <legend style="background: url(images/legendimg.jpg) no-repeat 6px 50%;">
                            <asp:Label ID="lblBigTitle" runat="server" Text="工作流程一览"></asp:Label></legend>
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
                                                    <asp:LinkButton ID="btnSelect" runat="server" Text="编辑" CommandName="select" CommandArgument='<%# Container.DataItemIndex%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="WorkflowName" HeaderText="名称" />
                                            <asp:BoundField DataField="WorkflowDesc" HeaderText="描述" />
                                            <asp:BoundField DataField="FlowTypeN" HeaderText="类型" />
                                            <asp:BoundField DataField="FormN" HeaderText="表单" />
                                            <asp:TemplateField HeaderText="节点信息">
                                              <ItemTemplate>
                                                <asp:LinkButton ID="btnSelectNodes" runat="server" Text="详细" CommandName="select" CommandArgument='<%# Container.DataItemIndex%>' OnClientClick=<%#"return ShowNodesDetail('"+Eval("WorkflowID")+"');"%> />
                                              </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DisplayOrder" HeaderText="显示顺序" />
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
                    <asp:Label ID="Label1" runat="server" Text="工作流程设置"></asp:Label></h2>
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
                        <input type="hidden" id="txtWorkflowID" runat="server" />
                        <input type="hidden" id="txtFormID" runat="server" />
                        <div class="con">
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        流程名称：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtWorkflowName" runat="server" Width="150"></cc2:TextBox><span style="color: Red;">*</span></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        流程描述：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtWorkflowDesc" runat="server" Width="300"></cc2:TextBox><span style="color: Red;">*</span></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        流程类型：</label><div class="iptblk">
                                            <cc2:DropDownList ID="ddlFlowTypeID" runat="server" Width="150">
                                            </cc2:DropDownList>
                                            <span style="color: Red;">*</span></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        使用表单：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtFormN" runat="server" Width="150"></cc2:TextBox><span style="color: Red;">*</span><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgForm" ToolTip="搜索" runat="server" OnClientClick="return btnFormClick();" /></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        是否有效：</label><div class="iptblk">
                                            <asp:CheckBox ID="chkIsValid" runat="server" Checked="true" /></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        是否短信提醒：</label><div class="iptblk">
                                            <asp:CheckBox ID="chkIsMsgNotice" runat="server" Checked="false" /></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        是否邮件提醒：</label><div class="iptblk">
                                            <asp:CheckBox ID="chkIsMailNotice" runat="server" Checked="true" /></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        是否允许转发：</label><div class="iptblk">
                                            <asp:CheckBox ID="chkIsTransfer" runat="server" Checked="false" /></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        显示顺序：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtDisplayOrder" runat="server" Width="300"></cc2:TextBox><span style="color: Red;">*</span></div>
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
                                            <div class="half">
                                                <label class="char5">
                                                    名称：</label><div class="iptblk">
                                                        <cc2:TextBox ID="txtQWorkflowName" runat="server" Width="90"></cc2:TextBox></div>
                                            </div>
                                            <div class="half">
                                                <label class="char5">
                                                    描述：</label><div class="iptblk">
                                                        <cc2:TextBox ID="txtQWorkflowDesc" runat="server" Width="90"></cc2:TextBox></div>
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
