<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GGB0Add.aspx.cs" Inherits="GOA.GGB0Add" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新增报表</title>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
 
 
      function CheckInputContent()
      {
        
      
                if($("txtReportName").value.trim() == "")
                {
                  alert("请输入报表名称！");
                  return false;
                }
                
                  if($("ddlReportType").value.Trim() == "0")
                {
                  alert("请选择 报表类型 ！");
                  return false;
                }
                   if($("txtFormID").value.trim() == "")
                {
                  alert("请选择 相关表单！");
                  return false;
                }
                
              if($("txtWorkflowID").value.Trim() == "")
                {
                  alert("请选择 相关流程 ！");
                  return false;
                }
                
                
        return true;
 
        }
        function btnWorkflowClick()
        {
        var url='WorkflowIDSelect.aspx?GUID=' + Math.random()+'&FormID='+window.document.getElementById("txtFormID").value;
        var ret = window.showModalDialog(url,'WorkflowIDSelect','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        { 
          window.document.getElementById("txtWorkflowID").value =ret[0]; 
          window.document.getElementById("txtWorkflowName").value =ret[1]; 
        }
        return false;

        }
        
        function btnReturn_Click()
        {
        window.location="GGB0.aspx";
       
        
        }
        
          function btnFormClick()
      {
        var url='GG30Select.aspx?' + Math.random()+'&IsSingle=1';
        var ret = window.showModalDialog(url,'GG30Select','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        { 
          window.document.getElementById("txtFormID").value =ret[0]; 
          window.document.getElementById("txtFormN").value =ret[1]; 
          
          if (ret[0]!="")
          {
          window.document.getElementById("divWF").style.visibility ="visible";  
          }
          else 
           window.document.getElementById("divWF").style.visibility ="hidden";  
        }
        return false;
      }
   
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <div id="wrapResumeContent">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div>
                        <cc2:Button ID="btnSubmit" ShowPostDiv="true" ValidateForm="false" runat="server" Text="Button_Save" ButtonImgUrl="../images/submit.gif" AutoPostBack="true" Page_ClientValidate="false" Disable="false" ScriptContent="CheckInputContent()" OnClick="btnSubmit_Click" />
                        <cc2:Button ID="btnReturn" ShowPostDiv="true" runat="server" Text="Button_Return" ButtonImgUrl="../images/browse.gif" AutoPostBack="false" Page_ClientValidate="false" Disable="false" ValidateForm="false" ScriptContent="btnReturn_Click()" />
                    </div>
                    <div class="conblk2" id="ProfileContainer" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                        <input type="hidden" id="txtFormID" runat="server" />
                        <input type="hidden" id="txtWorkflowID" runat="server" />
                        <div class="con">
                            <div class="clear">
                                <div class="online">
                                    <label class="char5">
                                        报表名称：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtReportName" runat="server" Width="120"></cc2:TextBox><span style="color: Red;">*</span></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="online">
                                    <label class="char5">
                                        报表种类：</label><div class="iptblk">
                                            <cc2:DropDownList ID="ddlReportType" runat="server" Width="120">
                                            </cc2:DropDownList>
                                        </div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="online">
                                    <label class="char5">
                                        相关表单：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtFormN" runat="server" Width="120"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgForm" ToolTip="搜索" runat="server" OnClientClick="return btnFormClick();" /><span style="color: Red;">*</span></div>
                                </div>
                            </div>
                            <div class="clear" runat="server" id="divWF" style="visibility: hidden">
                                <div class="online">
                                    <label class="char5">
                                        相关流程：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtWorkflowName" runat="server" Width="120"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgWorkflow" ToolTip="搜索" runat="server" OnClientClick="return btnWorkflowClick();" /><span style="color: Red;">*</span></div>
                                </div>
                            </div>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
