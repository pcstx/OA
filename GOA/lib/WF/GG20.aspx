<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG20.aspx.cs" Inherits="GOA.GG20" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>字段管理</title>

    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
      // Add click handlers for buttons to show and hide modal popup on pageLoad
      function pageLoad() {
        //$addHandler($get("btnAdd"), 'click', showAddModalPopupViaClient);
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
        if(!document.all.txtFieldName.value.isAlpha())
        {
          alert("请按规则输入字段名称。");
          document.all.txtFieldName.focus();
          return false;
        }
        return true;
      }
      
      function CheckDetailInputContent()
      {
        if(document.all.txtLabelWord.value.Trim() == "")
        {
          alert("请输入可选项文字。");
          return false;
        }
        if(document.all.txtDisplayOrder.value.Trim() == "")
        {
          alert("请输入排序。");
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
         
    </script>

    <style type="text/css">
        #divCheckboxList label
        {
            border: 1px #fff solid;
            width: 88px;
            _width: 89px;
            float: none;
            padding-top: 2px;
            margin-top: 1px;
            margin-right: 1px;
        }
        #divCheckboxList2 label
        {
            border: 1px #fff solid;
            width: 88px;
            _width: 89px;
            float: none;
            padding-top: 2px;
            margin-top: 1px;
            margin-right: 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <cc2:Button ID="btnQuery" runat="server" ButtonImgUrl="../images/search.gif" Enabled="false" ShowPostDiv="false" Text="Button_Search" ValidateForm="false" AutoPostBack="false" Width="80" />
                <cc2:Button ID="btnAdd" runat="server" ButtonImgUrl="../images/add.gif" Enabled="false" ShowPostDiv="false" Text="Button_Add" ValidateForm="false" AutoPostBack="true" Width="80" OnClick="btnAdd_Click" />
                <cc2:Button ID="btnDel" runat="server" ButtonImgUrl="../images/del.gif" Enabled="false" ShowPostDiv="false" Text="Button_Del" ValidateForm="false" AutoPostBack="true" Width="100" OnClick="btnDel_Click" />
            </div>
            <div style="margin-left: 0px; margin-top: 0px;">
                <div class="ManagerForm">
                    <fieldset>
                        <legend style="background: url(images/legendimg.jpg) no-repeat 6px 50%;">
                            <asp:Label ID="lblBigTitle" runat="server" Text="字段管理"></asp:Label></legend>
                        <div class="clear">
                            <div class="oneline">
                                <fieldset>
                                    <!--page start-->
                                    <div id="pager">
                                        <div style="float: left;" id="divCheckboxList">
                                            <asp:RadioButtonList ID="rblFieldType" runat="server" OnSelectedIndexChanged="rblFieldType_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="主字段" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="明细字段"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
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
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    操作</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelect" runat="server" Text="详细" CommandName="select" CommandArgument='<%# Container.DataItemIndex%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FieldName" HeaderText="名称" />
                                            <asp:BoundField DataField="FieldDesc" HeaderText="描述" />
                                            <asp:BoundField DataField="HTMLTypeN" HeaderText="表现形式" />
                                            <asp:BoundField DataField="FieldDBType" HeaderText="字段类型" />
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
                    <asp:Label ID="Label1" runat="server" Text="字段管理"></asp:Label></h2>
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
                        <input type="hidden" id="txtFieldID" runat="server" />
                        <input type="hidden" id="txtDataSetID" runat="server" />
                        <div class="con">
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        名称：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtFieldName" runat="server" Width="150"></cc2:TextBox><span style="color: Red;">*字段名不能用中文,而且必须以英文字母开头(如f4),长度不能超过30位</span></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        描述：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtFieldDesc" runat="server" Width="300"></cc2:TextBox></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="third1">
                                    <label class="char5">
                                        表现形式：</label><div class="iptblk">
                                            <cc2:DropDownList ID="ddlHTMLType" runat="server" Width="90" AutoPostBack="true" OnSelectedIndexChanged="ddlDataType_SelectedIndexChanged">
                                            </cc2:DropDownList>
                                        </div>
                                </div>
                                <div class="third2" runat="server" id="divDataType">
                                    <label class="char5">
                                        数据类型：</label><div class="iptblk">
                                            <cc2:DropDownList ID="ddlDataType" runat="server" Width="90" AutoPostBack="true" OnSelectedIndexChanged="ddlDataType_SelectedIndexChanged">
                                            </cc2:DropDownList>
                                        </div>
                                </div>
                                <div class="third3" runat="server" id="divTextLength">
                                    <label class="char5">
                                        文本长度：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtTextLength" runat="server" Width="90"></cc2:TextBox></div>
                                </div>
                                <div class="third3" runat="server" id="divDateformat">
                                    <label class="char5">
                                        日期类型：</label><div class="iptblk">
                                            <cc2:DropDownList ID="ddlDateformat" runat="server" Width="120">
                                            </cc2:DropDownList>
                                        </div>
                                </div>
                                <div class="third2" runat="server" id="divTextHeight">
                                    <label class="char5">
                                        高度：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtTextHeight" runat="server" Width="90"></cc2:TextBox></div>
                                </div>
                                <div class="third3" runat="server" id="divIsHTML">
                                    <label class="char5">
                                        Html编辑字段：</label><div class="iptblk">
                                            <asp:CheckBox ID="chkIsHTML" runat="server" Width="90"></asp:CheckBox></div>
                                </div>
                                <div class="third2" runat="server" id="divBrowseType">
                                    <label class="char5">
                                        浏览类型：</label><div class="iptblk">
                                            <cc2:DropDownList ID="ddlBrowseType" runat="server" Width="90">
                                            </cc2:DropDownList>
                                        </div>
                                </div>
                            </div>
                            <div id="divSelectInfoDetail" runat="server">
                                <div runat="server" id="divSelectInfo">
                                    <div class="half">
                                        <div class="iptblk" id="divCheckboxList2">
                                            <asp:RadioButtonList runat="server" ID="rblStaticOrDynamic" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblStaticOrDynamic_SelectedIndexChanged">
                                                <asp:ListItem Text="静态" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="动态" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <div id="divSelectInfoDetailStatic" runat="server">
                                    <div class="clear">
                                        <div class="third1">
                                            <label class="char5">
                                                可选项文字：</label><div class="iptblk">
                                                    <cc2:TextBox ID="txtLabelWord" runat="server" Width="90"></cc2:TextBox></div>
                                        </div>
                                        <div class="third2">
                                            <label class="char5">
                                                排序：</label><div class="iptblk">
                                                    <cc2:TextBox ID="txtDisplayOrder" runat="server" Width="90"></cc2:TextBox></div>
                                        </div>
                                        <div class="third3">
                                            <cc2:Button ID="btnAddRow" runat="server" Enabled="false" ShowPostDiv="false" Text="Button_MoveIn" ValidateForm="false" AutoPostBack="true" Width="80" ScriptContent="CheckDetailInputContent()" OnClick="btn_AddRow" />
                                            <cc2:Button ID="btnDeleteRows" runat="server" Enabled="false" ShowPostDiv="false" Text="Button_MoveOut" ValidateForm="false" AutoPostBack="true" Width="80" OnClick="btn_DeleteRows" />
                                        </div>
                                    </div>
                                    <div class="clear">
                                        <div class="oneline">
                                            <label class="char5">
                                                可选项一览：</label>
                                            <div class="iptblk">
                                                <yyc:SmartGridView ID="SmartGridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="100" Width="100%" DataKeyNames="SelectNo">
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
                                                        <asp:BoundField DataField="SelectNo" HeaderText="" ItemStyle-Width="0px" Visible="false" />
                                                        <asp:BoundField DataField="LabelWord" HeaderText="可选项文字" />
                                                        <asp:BoundField DataField="DisplayOrder" HeaderText="排序" />
                                                    </Columns>
                                                    <CascadeCheckboxes>
                                                        <yyc:CascadeCheckbox ChildCheckboxID="item" ParentCheckboxID="all" />
                                                    </CascadeCheckboxes>
                                                    <CheckedRowCssClass CheckBoxID="Item" CssClass="SelectedRow" />
                                                    <ContextMenus>
                                                        <yyc:ContextMenu Text="版权所有" NavigateUrl="http://www.geobyev.com" Target="_blank" />
                                                    </ContextMenus>
                                                </yyc:SmartGridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divSelectInfoDetailDynamic" runat="server">
                                    <div class="clear">
                                        <div class="half">
                                            <label class="char5">
                                                数据集：</label><div class="iptblk">
                                                    <cc2:TextBox ID="txtDataSetName" runat="server" Text="" OnTextChanged="txtDataSet_TextChanged" Width="80"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgDataSet" ToolTip="搜索" runat="server" OnClientClick="return btnDataSetClick();" /><span style="color: Red;">*</span></div>
                                        </div>
                                    </div>
                                    <div class="clear">
                                        <div class="half">
                                            <label class="char5">
                                                值字段：</label><div class="iptblk">
                                                    <cc2:DropDownList ID="ddlValueColumn" runat="server">
                                                    </cc2:DropDownList>
                                                    <span style="color: Red;">*</span></div>
                                        </div>
                                        <div class="half">
                                            <label class="char5">
                                                显示字段：</label><div class="iptblk">
                                                    <cc2:DropDownList ID="ddlTextColumn" runat="server">
                                                    </cc2:DropDownList>
                                                    <span style="color: Red;">*</span></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <label class="char5">
                                        字段类型：</label><div class="iptblk">
                                            <asp:Label ID="lblDictType" runat="server" Text=""></asp:Label></div>
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
                                                        <cc2:TextBox ID="txtQFieldName" runat="server" Width="90"></cc2:TextBox></div>
                                            </div>
                                            <div class="half">
                                                <label class="char5">
                                                    描述：</label><div class="iptblk">
                                                        <cc2:TextBox ID="txtQFieldDesc" runat="server" Width="90"></cc2:TextBox></div>
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
