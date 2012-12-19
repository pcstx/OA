<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetFilePermissions.aspx.cs" Inherits="GOA.Basic.SetFilePermissions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>权限设置</title>
        <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
//        function pageLoad() {
//            $addHandler($get("btnSearch"), 'click', showQueryModalPopupViaClient);
//        }
//        function showQueryModalPopupViaClient(ev) {
//            ev.preventDefault();
//            var modalPopupBehavior = $find('programmaticQueryModalPopup');
//            modalPopupBehavior.show();
//     
//        }
      
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
     <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
  </cc1:ToolkitScriptManager>

    <div>
        <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
         
            <div class="clear">
                <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="权限">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="PanelUserContrl" Height="400px">
                             <div class="half">
                              <div class="iptblk">
                             <%--    <asp:Label ID="lblUserList" runat="server" Text="选择列表"></asp:Label>&nbsp;
                                    <cc2:Button ID="btnSearch" runat="server" ButtonImgUrl="../../images/query.gif" Enabled="false" ShowPostDiv="false" Text="Button_Search" ValidateForm="false" AutoPostBack="false" />
                  --%>
                        <asp:Label ID="lblList" runat="server" Text="选择："></asp:Label>
                                     <asp:DropDownList ID="ListChoice" runat="server" OnSelectedIndexChanged="Bind" AutoPostBack="true">
                                       <asp:ListItem Selected="True" Text="用户" Value="1"></asp:ListItem>
                                         <asp:ListItem  Text="部门" Value="2"></asp:ListItem>
                                           <asp:ListItem Text="角色" Value="3"></asp:ListItem>
                                     </asp:DropDownList>
                             </div>
                <br />
                <asp:ListBox ID="MyList" runat="server" Width="150" Height="200" OnSelectedIndexChanged="showRight" AutoPostBack="true"></asp:ListBox>
              </div>
               <div class="clear">
                <div class="half2">
                <asp:RadioButton  ID="rdPermission"/>
                   
                  <%--  <asp:CheckBox  ID="ckRead" runat="server" />只读
                    <asp:CheckBox ID="ckWrite" runat="server"/>可读可写
                 
                   <asp:CheckBox ID="ckDelete" runat="server"/>可读可写可删除
                    <asp:CheckBox ID="ckCancel" runat="server"/>可读可写可删可作废--%>
                    <asp:CheckBox  ID="ckRead" runat="server" Enabled="false" />可读
                    <asp:CheckBox ID="ckWrite" runat="server" Enabled="false" />可修改
                   <asp:CheckBox ID="ckDelete" runat="server" Enabled="false" />可删除
                    <asp:CheckBox ID="ckCancel" runat="server" Enabled="false" />可作废
                    
               <asp:Button ID="btnSave" runat="server" Text="提 交" CssClass="btnSave"  OnClick="SaveRight"/>&nbsp;&nbsp;
          <div class="clear">
                <label class="char5">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></label></div>
                   
                  </div>
                <div style="width: 10%;">
                  &nbsp;</div>
              </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    
                    
                    
                    
                    
                    <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="目录">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="Panel2" Height="300px">
                                
                            </asp:Panel>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    
                    
                    
                </ajaxToolkit:TabContainer>
                
                
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
            </div>
        </div>
    </div>
    </form>
</body>
</html>
