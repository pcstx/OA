<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG90Add.aspx.cs" Inherits="GOA.GG90Add" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>流程代理设置</title>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
 
 
      function CheckInputContent()
      {
        if($("txtWorkflowID").value.Trim() == "")
        {
          alert("请选择 流程 ！");
          return false;
        }
        if($("txtBeAgentPersonID").value == "")
        {
          alert("请选择 被代理人！");
          return false;
        }
        
           if($("txtAgentPersonID").value == "")
        {
          alert("请选择 代理人！");
          return false;
        }
      /*  
        if($("txtAgentStartDate").value.Trim() == "")
        {
          alert("请选择  代理日期起！");
          return false;
        }
         
            if($("txtAgentEndDate").value.Trim() == "")
        {
          alert("请选择 代理日期止！");
          return false;
        }      */
        return true;
      }

        function btnWorkflowIDClick()
        {
        var url='WorkflowIDSelect.aspx?GUID=' + Math.random();
        var ret = window.showModalDialog(url,'WorkflowIDSelect','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        { 
          window.document.getElementById("txtWorkflowID").value =ret[0]; 
          window.document.getElementById("txtWorkflowName").value =ret[1]; 
        }
        return false;

        }
        
      
        
        function btnPersonClick(id)
        {
        var url='SingleUserSelect.aspx?GUID=' + Math.random();
        var ret = window.showModalDialog(url,'AgentPersonSelect','scroll:1;status:0;help:0;resizable:1;dialogWidth:720px;dialogHeight:550px') ; 

        if (ret != null) 
        { 
          if (id==2)
            {
              window.document.getElementById("txtAgentPersonID").value =ret[0]; 
              window.document.getElementById("txtAgentPersonName").value =ret[1]; 
             }
        else 
               {   
                window.document.getElementById("txtBeAgentPersonID").value =ret[0]; 
                  window.document.getElementById("txtBeAgentPersonName").value =ret[1]; }
                }
        return false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div id="wrapResumeContent">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div>
                    <cc2:Button ID="btnSubmit" ShowPostDiv="true" runat="server" Text="Button_Save" ButtonImgUrl="../images/submit.gif" AutoPostBack="true" Page_ClientValidate="false" Disable="false" ScriptContent="CheckInputContent()" OnClick="btnSubmit_Click" />
                    <cc2:Button ID="btnReturn" ShowPostDiv="true" runat="server" Text="Button_Return" ButtonImgUrl="../images/browse.gif" AutoPostBack="true" Page_ClientValidate="false" Disable="false" OnClick="btnReturn_Click" />
                    <cc2:Button ID="btnClear" ShowPostDiv="true" runat="server" Text="Button_Reset" AutoPostBack="true" ButtonImgUrl="../images/cancel.gif" Page_ClientValidate="false" Disable="false" OnClick="btnClear_Click" />
                </div>
                <div class="conblk2" id="ProfileContainer" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                    <input type="hidden" id="txtAgentID" runat="server" />
                    <input type="hidden" id="txtWorkflowID" runat="server" />
                    <input type="hidden" id="txtBeAgentPersonID" runat="server" />
                    <input type="hidden" id="txtAgentPersonID" runat="server" />
                    <div class="con">
                        <div class="clear">
                            <div class="online">
                                <label class="char9">
                                    流程编号：</label>
                                <div class="iptblk">
                                    <cc2:TextBox ID="txtWorkflowName" ReadOnly="true" runat="server" Width="150"></cc2:TextBox><span style="color: Red;">*</span><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgWorkflowID" ToolTip="搜索" runat="server" OnClientClick="return btnWorkflowIDClick();" /></div>
                            </div>
                        </div>
                        <div class="clear">
                            <div class="online">
                                <label class="char9">
                                    被代理人：</label>
                                <div class="iptblk">
                                    <cc2:TextBox ID="txtBeAgentPersonName" ReadOnly="true" runat="server" Width="150"></cc2:TextBox><span style="color: Red;">*</span><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgBeAgentPerson" ToolTip="搜索" runat="server" OnClientClick="return btnPersonClick(1);" /></div>
                            </div>
                        </div>
                        <div class="clear">
                            <div class="online">
                                <label class="char9">
                                    代理人：</label>
                                <div class="iptblk">
                                    <cc2:TextBox ID="txtAgentPersonName" runat="server" ReadOnly="true" Width="150"></cc2:TextBox><span style="color: Red;">*</span><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgAgentPerson" ToolTip="搜索" runat="server" OnClientClick="return btnPersonClick(2);" /></div>
                            </div>
                        </div>
                        <div class="clear">
                            <div class="oneline">
                                <label class="char9">
                                    代理开始日期：</label>
                                <div class="iptblk">
                                    <cc2:TextBox ID="txtAgentStartDate" runat="server" Width="150"></cc2:TextBox><cc1:CalendarExtender ID="AgentStartDateCalendarExtender" runat="server" TargetControlID="txtAgentStartDate" Format="yyyy-MM-dd" PopupPosition="TopRight" />
                                    <span style="color: Red;">*</span></div>
                            </div>
                        </div>
                        <div class="clear">
                            <div class="online">
                                <label class="char9">
                                    代理结束日期：</label>
                                <div class="iptblk">
                                    <cc2:TextBox ID="txtAgentEndDate" runat="server" Width="150"></cc2:TextBox><cc1:CalendarExtender ID="AgentEndDateCalendarExtender" runat="server" TargetControlID="txtAgentEndDate" Format="yyyy-MM-dd" PopupPosition="TopRight" />
                                    <span style="color: Red;">*</span></div>
                            </div>
                        </div>
                        <div class="clear">
                            <div class="online">
                                <label class="char9">
                                    是否允许递归代理：</label>
                                <div class="iptblk">
                                    <asp:CheckBox ID="chkAllowCycle" runat="server" Checked="true" /></div>
                            </div>
                        </div>
                        <div class="clear">
                            <div class="online">
                                <label class="char9">
                                    是否允许代理人发起流程：</label>
                                <div class="iptblk">
                                    <asp:CheckBox ID="chkAllowCreate" runat="server" Checked="true" /></div>
                            </div>
                        </div>
                        <div class="clear" runat="server" id="divIsCancel">
                            <div class="online">
                                <label class="char9">
                                    是否已取消：</label>
                                <div class="iptblk">
                                    <asp:Label ID="lblIsCancel" Width="20" runat="server" Text=""></asp:Label></div>
                            </div>
                        </div>
                        <!--   <div  class="clear">
                                                <div class="online"> <label class="char9"> <asp:Label ID="lblMsg" Width="200px"  ForeColor="Red" runat="server" Text=""></asp:Label></label></div>                                       </div>-->
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
