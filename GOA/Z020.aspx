<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Z020.aspx.cs" Inherits="GOA.Z020" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolKit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>用户权限管理</title>

  <script language="javascript" type="text/javascript">
    // Add click handlers for buttons to show and hide modal popup on pageLoad
    function pageLoad() {
      $addHandler($get("btnSearch"), 'click', showQueryModalPopupViaClient);
    }
    function showQueryModalPopupViaClient(ev) {
      ev.preventDefault();
      var modalPopupBehavior = $find('programmaticQueryModalPopupBehavior');
      modalPopupBehavior.show();
    }
    
// 点击复选框时触发事件
function postBackByObject()
{
    
var ele = event.srcElement; 
if(ele.type=='checkbox') 
{ 
var childrenDivID = ele.id.replace('CheckBox','Nodes'); 
var div = document.getElementById(childrenDivID); 
if(div==null)return; 
var checkBoxs = div.getElementsByTagName('INPUT'); 
for(var i=0;i<checkBoxs.length;i++) 
{ 
if(checkBoxs[i].type=='checkbox') 
checkBoxs[i].checked=ele.checked; 
} 
} 

}

    //checkbox点击事件
    function OnCheckEvent()
    {
        var objNode = event.srcElement;
        if(objNode.tagName != "INPUT" || objNode.type != "checkbox")
            return;
        //获得当前树结点

        var ck_ID = objNode.getAttribute("ID");
        var node_ID = ck_ID.substring(0,ck_ID.indexOf("CheckBox")) + "Nodes";
        var curTreeNode = document.getElementById(node_ID);
        //级联选择
        SetChildCheckBox(curTreeNode,objNode.checked);
        SetParentCheckBox(objNode);
    }
    
    //子结点字符串
    var childIds = "";
    //获取子结点ID数组
    function GetChildIdArray(parentNode)
    {
        if (parentNode == null)
            return;
        var childNodes = parentNode.children;
        var count = childNodes.length;
        for(var i = 0;i < count;i ++)
        {
            var tmpNode = childNodes[i];
            if(tmpNode.tagName == "INPUT" && tmpNode.type == "checkbox")
            {
                childIds = tmpNode.id + ":" + childIds;
            }
            GetChildIdArray(tmpNode);
        }
    }
    
    //设置子结点的checkbox
    function SetChildCheckBox(parentNode,checked)
    {
        if(parentNode == null)
            return;
        var childNodes = parentNode.children;
        var count = childNodes.length;
        for(var i = 0;i < count;i ++)
        {
            var tmpNode = childNodes[i];
            if(tmpNode.tagName == "INPUT" && tmpNode.type == "checkbox")
            {
                tmpNode.checked = checked;
            }
            SetChildCheckBox(tmpNode,checked);
        }
    }
    
    //设置父结点的checkbox
    function SetParentCheckBox(childNode)
    {
        if(childNode == null)
            return;
        var parent = childNode.parentNode;
        if(parent == null || parent == "undefined")
            return;
        do 
        {
        parent = parent.parentNode;
        }
        while (parent && parent.tagName != "DIV");
        if(parent == "undefined" || parent == null)
            return;
        var parentId = parent.getAttribute("ID");
        var objParent = document.getElementById(parentId);
        childIds = "";
        GetChildIdArray(objParent);
        //判断子结点状态

        childIds = childIds.substring(0,childIds.length - 1);
        var aryChild = childIds.split(":");
        var result = false;
        //当子结点的checkbox状态有一个为true，其父结点checkbox状态即为true,否则为false
        for(var i in aryChild)
        {
            var childCk = document.getElementById(aryChild[i]);
            if(childCk.checked)
                result = true;
        }
        parentId = parentId.replace("Nodes","CheckBox");
        var parentCk = document.getElementById(parentId);
        if(parentCk == null)
            return;
        if(result)
            parentCk.checked = true;
        else
            parentCk.checked = false;
        SetParentCheckBox(parentCk);
    }


  </script>

</head>
<body>
  <form id="form1" runat="server">
  <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
  </cc1:ToolkitScriptManager>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div style="margin-left: 0px; margin-top: 0px;">
        <div class="ManagerForm">
          <fieldset>
            <legend style="background: url(../../images/legendimg.jpg) no-repeat 6px 50%;">
              <asp:Label ID="lblBigTitle" runat="server" Text="用户权限"></asp:Label></legend>
            <div class="clear">
              <div class="half">
                <!--label-->
                <div class="iptblk">
                  <asp:Label ID="lblUserList" runat="server" Text="用户列表"></asp:Label>&nbsp;<cc2:Button ID="btnSearch" runat="server" ButtonImgUrl="../../images/query.gif" Enabled="false" ShowPostDiv="false" Text="Button_Search" ValidateForm="false" AutoPostBack="false" /></div>
                <br />
                <asp:ListBox ID="UserList" runat="server" Width="150" Height="400" OnSelectedIndexChanged="showUserRight" AutoPostBack="true"></asp:ListBox>
              </div>
              <div class="half">
                <div style="text-align: left; float: left;">
                  <asp:TreeView ID="RightTree" runat="server" OnTreeNodePopulate="TreeNodePopulate" ShowCheckBoxes="All">
                  </asp:TreeView>
                </div>
              </div>
              <div class="clear">
                <div class="half2">
                  <asp:Button ID="btnSave" runat="server" Text="提 交" CssClass="btnSave" OnClick="SaveUserRight" />&nbsp;&nbsp;<input id="Reset1" type="reset" value="重 置" class="btnSave" />&nbsp;&nbsp;<asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></div>
                <div style="width: 10%;">
                  &nbsp;</div>
              </div>
          </fieldset>
        </div>
      </div>
    </ContentTemplate>
    <Triggers>
      <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
      <asp:AsyncPostBackTrigger ControlID="UserList" EventName="SelectedIndexChanged" />
    </Triggers>
  </asp:UpdatePanel>
  <asp:Button runat="server" ID="hiddenTargetControlForQueryModalPopup" Style="display: none" />
  <cc1:ModalPopupExtender runat="server" ID="programmaticQueryModalPopup" BehaviorID="programmaticQueryModalPopupBehavior" TargetControlID="hiddenTargetControlForQueryModalPopup" PopupControlID="programmaticPopupQuery" BackgroundCssClass="modalBackground" DropShadow="False" CancelControlID="QueryCancelButton" PopupDragHandleControlID="programmaticQueryPopupDragHandle" RepositionMode="RepositionOnWindowScroll">
  </cc1:ModalPopupExtender>
  <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopupQuery" Style="display: none; width: 650px; padding: 0 0px 0px 0px">
    <div id="programmaticQueryPopupDragHandle" class="programmaticPopupDragHandle">
      <div class="h2blk">
        <h2 id="H2_2">
          <asp:Label ID="lblSearchTitle" runat="server" Text="搜索框"></asp:Label></h2>
      </div>
      <cc2:Button ID="MaxButton" ScriptContent="MaxQueryFrom();" ShowPostDiv="false" runat="server" Text="Label_NULL" CSS="MaxButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="true" ToolTip="Max" Width="12" />
      <cc2:Button ID="QueryCancelButton" ShowPostDiv="false" runat="server" Text="Label_NULL" CSS="CancelButtonCSS" AutoPostBack="false" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="false" ToolTip="Close" Width="12" />
    </div>
    <div>
      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
          <div class="edit">
            <cc2:Button ID="btnSearchCustomer" ShowPostDiv="false" runat="server" Text="Label_NULL" AutoPostBack="true" ButtontypeMode="Normal" Page_ClientValidate="false" Disable="true" Width="16" CSS="SearchButtonCSS" OnClick="btnSearchCustomer_Click" />
          </div>
          <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
            <div class="con" id="Div2">
              <div class="formblk">
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
                      用户类型：</label><div class="iptblk">
                        <!--textbox-->
                        <asp:DropDownList ID="ddlQUserType" runat="server" Width="100">
                          <asp:ListItem Value=""></asp:ListItem>
                          <asp:ListItem Value="0">内部员工</asp:ListItem>
                          <asp:ListItem Value="1">外部用户</asp:ListItem>
                        </asp:DropDownList>
                      </div>
                  </div>
                  <div class="half">
                    <label class="char5">
                      <!--label-->
                      员工编号：</label><div class="iptblk">
                        <!--textbox-->
                        <cc2:TextBox ID="txtQUserCode" runat="server" Width="90"></cc2:TextBox></div>
                  </div>
                </div>
              </div>
            </div>
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

