<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG5005.aspx.cs" Inherits="GOA.GG5005" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>触发流程设置</title>

    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
   
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
       if(document.all.ddlNodeID.value.trim() == "")
        {
          alert("请选择节点！");
          
          return false;
        }
        if(document.all.txtTriggerWFID.value.trim() == "")
        {
          alert("请选择触发流程！");
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
        document.getElementById('txtTriggerWFID').value='';
        document.getElementById('txtTriggerWFN').value='';
        document.getElementById('txtTriggerID').value='';
       document.getElementById('ddlTriggerCreator').value='0';
       var exist1= document.getElementById('divNode');
   if(   exist1!=null   )
         //document.getElementById('divNode').style.visibility ="hidden" ;
         document.getElementById('divNode').style.display="none" ;
         
      var exist2= document.getElementById('divField');
   if(   exist2!=null   )
          document.getElementById('divField').style.display="none" ;
     //  document.getElementById('divField').style.visibility="hidden" ;           
      }
      
        function btnWorkflowIDClick()
        {
        var url='WorkflowIDSelect.aspx?GUID=' + Math.random()+'&IsSingle=1';
        var ret = window.showModalDialog(url,'WorkflowIDSelect','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        { 
          window.document.getElementById("txtTriggerWFID").value =ret[0]; 
          window.document.getElementById("txtTriggerWFN").value =ret[1]; 
        }
        return false;

        }
        
      function btnConditionSetup(TriggerId)
      {
      
        var url = "GG500501.aspx?GUID=" + Math.random() + "&id="+TriggerId ;
        var ret = window.showModalDialog(url, 'GG500501', 'scroll:1;status:0;help:0;resizable:1;dialogWidth:800px;dialogHeight:525px');
        return !(ret === undefined)
      }
      
      function btnTriggerFieldMapping(TriggerId)
      {
        var url = "GG500503.aspx?GUID=" + Math.random() + "&TriggerID="+TriggerId;
        var ret = window.open(url, 'GG500503', 'height=750,width=1000,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no');
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
                            <asp:Label ID="lblBigTitle" runat="server" Text="流程节点一览"></asp:Label></legend>
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
                                    <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="TriggerID">
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
                                            <asp:BoundField DataField="NodeName" HeaderText="节点" />
                                            <asp:BoundField DataField="TWorkflowName" HeaderText="触发流程" />
                                            <asp:BoundField DataField="OPTimeN" HeaderText="触发动作时机" />
                                            <asp:BoundField DataField="TriggerWFCreatorN" HeaderText="触发流程的创建者" />
                                            <asp:BoundField DataField="WFCreateNodeN" HeaderText="父流程节点操作者的节点编号" />
                                            <asp:BoundField DataField="WFCreateFieldN" HeaderText="父流程字段值的字段编号" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    触发条件设置</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnCondition" runat="server" ImageUrl="../images/icon_enter.gif" OnClientClick=<%# "btnConditionSetup('"+Eval("TriggerID")+"')" %> />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    触发流程的字段对应设置</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnTriggerFieldMapping" runat="server" ImageUrl="../images/icon_enter.gif" OnClientClick=<%# "btnTriggerFieldMapping('"+Eval("TriggerID")+"')" %> />
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
                    <asp:Label ID="Label1" runat="server" Text="节点管理"></asp:Label></h2>
            </div>
            <cc2:Button ID="AddCancelButton" ShowPostDiv="false" runat="server" Text="Label_NULL" CSS="CancelButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="false" ToolTip="Close" Width="12" ScriptContent="SetViewState()" />
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div>
                        <cc2:Button ID="btnSubmit" ShowPostDiv="true" runat="server" Text="Label_NULL" CSS="SaveButtonCSS" AutoPostBack="true" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="false" Width="26" ScriptContent="CheckInputContent()" />
                        <cc2:Button ID="btnSubmitAndClose" ShowPostDiv="true" runat="server" Text="Label_NULL" CSS="SaveAndCloseCSS" AutoPostBack="true" ButtontypeMode="Normal" OnClick="hideModalPopupViaServer_Click" Page_ClientValidate="false" Disable="false" Width="26" ScriptContent="CheckInputContent()" />
                    </div>
                    <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                        <input type="hidden" id="txtTriggerID" runat="server" />
                        <input type="hidden" id="txtWFID" runat="server" />
                        <input type="hidden" id="txtTriggerWFID" runat="server" />
                        <div class="con">
                            <!--<div class="clear">
              <div class="oneline"><label class="char5">父流程：</label><div class="iptblk"><cc2:TextBox ID="txtWFN" ReadOnly="false" runat="server" Width="150"></cc2:TextBox><span style="color:Red;">*</span></div></div>-->
                        </div>
                        <div class="clear">
                            <div class="oneline">
                                <label class="char9">
                                    节点：</label><div class="iptblk">
                                        <cc2:DropDownList ID="ddlNodeID" runat="server" AutoPostBack="false" Width="150">
                                        </cc2:DropDownList>
                                        <span style="color: Red;">*</span></div>
                            </div>
                        </div>
                        <div class="clear">
                            <div class="oneline">
                                <label class="char9">
                                    触发时机：</label><div class="iptblk">
                                        <cc2:DropDownList ID="ddlOPTime" runat="server" Width="150">
                                        </cc2:DropDownList>
                                    </div>
                            </div>
                        </div>
                        <!--         <div class="clear" visible="false">
              <div class="oneline"><label class="char9">触发条件：</label><div class="iptblk"><cc2:TextBox ID="txtOPCondition" runat="server" Width="150"></cc2:TextBox><cc2:Button  runat="server" ID="btnConditionSet" ButtonImgUrl="../images/add.gif" ShowPostDiv="false"  Text="Button_Set" AutoPostBack="false" Width="60px"  ValidateForm="false"  ScriptContent="return btnConditionSetup();"  Page_ClientValidate="false" /><span style="color:Red;">* 流程中的字段值符合某条件时触发子流程</span></div></div>
            </div>-->
                        <div class="clear">
                            <div class="oneline">
                                <label class="char9">
                                    触发流程：</label><div class="iptblk">
                                        <cc2:TextBox ID="txtTriggerWFN" runat="server" Width="150"></cc2:TextBox><span style="color: Red;">*</span><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgForm" ToolTip="搜索" runat="server" OnClientClick="return btnWorkflowIDClick();" /></div>
                            </div>
                        </div>
                        <div class="clear">
                            <div class="oneline">
                                <label class="char9">
                                    触发流程创建者：</label><div class="iptblk">
                                        <asp:DropDownList ID="ddlTriggerCreator" runat="server" Width="150" OnSelectedIndexChanged="ddlTriggerCreator_OnSelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">父流程创建者</asp:ListItem>
                                            <asp:ListItem Value="1">父流程节点操作者</asp:ListItem>
                                            <asp:ListItem Value="2">父流程字段值</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                            </div>
                        </div>
                        <div class="clear" runat="server" id="divNode" visible="false">
                            <div class="oneline">
                                <label class="char9">
                                    父流程节点操作者的节点编号：</label><div class="iptblk">
                                        <cc2:DropDownList ID="ddlWFCreateNode" runat="server" Width="150" AutoPostBack="false">
                                        </cc2:DropDownList>
                                    </div>
                            </div>
                        </div>
                        <div class="clear" runat="server" id="divField" visible="false">
                            <div class="oneline">
                                <label class="char9">
                                    父流程字段值的字段编号：</label><div class="iptblk">
                                        <cc2:DropDownList ID="ddlWFCreateFieldName" runat="server" Width="150" AutoPostBack="false">
                                        </cc2:DropDownList>
                                    </div>
                            </div>
                        </div>
                        <div class="clear">
                            <label class="char9">
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
