<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG5001.aspx.cs" Inherits="GOA.GG5001" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>基本信息</title>

    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
      var styleToSelect;
      // Add click handlers for buttons to show and hide modal popup on pageLoad
      function pageLoad() {
      }
      
      function CheckInputContent()
      {
        if($("txtFormTypeName").value.trim() == "")
        {
          alert("请输入分类名称。");
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
                    <legend style="background: url(images/legendimg.jpg) no-repeat 6px 50%;">
                        <asp:Label ID="lblBigTitle" runat="server" Text="工作流程"></asp:Label></legend>
                    <div class="clear">
                        <div>
                            <cc2:Button ID="btnSubmit" ShowPostDiv="true" runat="server" Text="Label_NULL" CSS="SaveButtonCSS" AutoPostBack="true" ButtontypeMode="Normal" OnClick="hideModalPopupViaServer_Click" Page_ClientValidate="true" Disable="false" Width="26" ScriptContent="fCheck()" Visible="false" />
                        </div>
                        <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                            <input type="hidden" id="txtWorkflowID" runat="server" />
                            <input type="hidden" id="txtFormID" runat="server" />
                            <div class="formblk">
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
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
