<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MW40.aspx.cs" Inherits="GOA.MW40" %>

<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>流程查询</title>

    <script language="javascript" type="text/javascript">
        function btnWorkflowClick()
      {
        var url='WorkflowIDSelect.aspx?' + Math.random();
        var ret = window.showModalDialog(url,'WorkflowIDSelect','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        { 
          window.document.getElementById("txtWorkflowID").value =ret[0]; 
          window.document.getElementById("txtWorkflowN").value =ret[1]; 
        }
        return false;
      }
      
         function btnUserClick()
      {
        var url='SingleUserSelect.aspx?' + Math.random();
        var ret = window.showModalDialog(url,'SingleUserSelect','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        { 
          window.document.getElementById("txtCreatorID").value =ret[0]; 
          window.document.getElementById("txtCreator").value =ret[1]; 
        }
        return false;
      }
      
      function btnDeptClick()
      {
        var url='DeptSelect.aspx?GUID=' + Math.random() ;
        var ret = window.showModalDialog(url,'DeptSelect','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        { 
          window.document.getElementById("txtDeptID").value =ret[0]; 
          window.document.getElementById("txtDeptName").value =ret[1]; 
        } 
        return false;
      }
      
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="float: left; width: 100%; overflow: auto">
                    <cc2:Button ID="btnQuery" runat="server" ButtonImgUrl="../images/search.gif" Enabled="false" Page_ClientValidate="false" ShowPostDiv="false" Text="Button_Search" ValidateForm="false" AutoPostBack="true" Width="80" OnClick="btnQuery_Click" />
                    <cc2:Button ID="btnClear" ShowPostDiv="true" runat="server" Text="Button_Reset" ValidateForm="false" AutoPostBack="true" ButtonImgUrl="../images/cancel.gif" Page_ClientValidate="false" Disable="false" OnClick="btnClear_Click" />
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label12" Font-Bold="true" Font-Size="9pt" runat="server" Text="流程查询"></asp:Label></legend>
                        <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                            <input type="hidden" id="txtWorkflowID" runat="server" />
                            <!--  <table width="750px"><tr><td>-->
                            <br />
                            <div class="clear">
                                <div class="oneline">
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" Font-Bold="true" Font-Size="9pt" runat="server" Text="请求"></asp:Label></div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <hr style="width: 96%; height: 1px" />
                                </div>
                            </div>
                            <div class="clear">
                                <div class="half">
                                    <label class="char5">
                                        编号：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtRequestID" runat="server" Width="150"></cc2:TextBox></div>
                                </div>
                                <div class="half">
                                    <label class="char6">
                                        状况：</label><div class="iptblk">
                                            <cc2:DropDownList ID="ddlStatus" runat="server" Width="150">
                                                <asp:ListItem Value="0">有效</asp:ListItem>
                                                <asp:ListItem Value="1">无效</asp:ListItem>
                                                <asp:ListItem Value="">全部</asp:ListItem>
                                            </cc2:DropDownList>
                                        </div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="half">
                                    <label class="char5">
                                        类型：</label><div class="iptblk">
                                            <cc2:DropDownList ID="ddlFormType" runat="server" Width="150">
                                            </cc2:DropDownList>
                                        </div>
                                </div>
                                <div class="half">
                                    <label class="char6">
                                        工作流：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtWorkflowN" runat="server" Width="150"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgForm" ToolTip="搜索" runat="server" OnClientClick="return btnWorkflowClick();" /></div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="half">
                                    <label class="char5">
                                        节点类型：</label><div class="iptblk">
                                            <cc2:DropDownList ID="ddlNodeTypeID" runat="server" Width="150">
                                            </cc2:DropDownList>
                                        </div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="clear">
                                <div class="oneline">
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" Font-Bold="true" Font-Size="9pt" runat="server" Text="创建"></asp:Label></div>
                            </div>
                            <input type="hidden" id="txtCreatorID" runat="server" />
                            <input type="hidden" id="txtDeptID" runat="server" />
                            <div class="clear">
                                <div class="oneline">
                                    <hr style="width: 96%; height: 1px" />
                                </div>
                            </div>
                            <div class="clear">
                                <div class="half">
                                    <label class="char5">
                                        日期：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtStartDate" runat="server" Width="80" ReadOnly="true"></cc2:TextBox><cc1:CalendarExtender ID="StartDateCalendarExtender" runat="server" TargetControlID="txtStartDate" Format="yyyy-MM-dd" PopupPosition="TopRight" />
                                            &nbsp;—&nbsp;
                                            <cc2:TextBox ID="txtEndDate" runat="server" Width="80" ReadOnly="true"></cc2:TextBox><cc1:CalendarExtender ID="EndDateCalendarExtender" runat="server" TargetControlID="txtEndDate" Format="yyyy-MM-dd" PopupPosition="TopRight" />
                                        </div>
                                </div>
                            </div>
                            <div class="clear">
                                <div class="half">
                                    <label class="char5">
                                        创建人：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtCreator" runat="server" Width="150"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImageButton1" ToolTip="搜索" runat="server" OnClientClick="return btnUserClick();" /></div>
                                </div>
                                <div class="half">
                                    <label class="char6">
                                        创建人所属部门：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtDeptName" runat="server" Width="150"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImageButton2" ToolTip="搜索" runat="server" OnClientClick="return btnDeptClick();" /></div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="clear">
                                <div class="oneline">
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label3" Font-Bold="true" Font-Size="9pt" runat="server" Text="表单或单据"></asp:Label></div>
                            </div>
                            <div class="clear">
                                <div class="oneline">
                                    <hr style="width: 96%; height: 1px" />
                                </div>
                            </div>
                            <div class="clear">
                                <div class="half">
                                    <label class="char5">
                                        标题：</label><div class="iptblk">
                                            <cc2:TextBox ID="txtFormTitle" runat="server" Width="150"></cc2:TextBox></div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <!--     </td></tr>
                  </table>-->
                        </div>
                    </fieldset>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
