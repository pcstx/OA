<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AW50.aspx.cs" Inherits="GOA.AW50" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <link href="../styles/StyleSheet.css" type="text/css" rel="stylesheet" />
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
      
         function btnUserClick(ctlOrd)
      {
        var url='SingleUserSelect.aspx?' + Math.random();
        var ret = window.showModalDialog(url,'SingleUserSelect','scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:525px') ; 

        if (ret != null) 
        { 
            if (ctlOrd==1)
            {
          window.document.getElementById("txtAgentID").value =ret[0]; 
          window.document.getElementById("txtAgentN").value =ret[1]; 
            }
            if (ctlOrd==2)
            {
          window.document.getElementById("txtBeAgentID").value =ret[0]; 
          window.document.getElementById("txtBeAgentN").value =ret[1]; 
            }
            if  (ctlOrd==3)
            {
          window.document.getElementById("txtCreatorID").value =ret[0]; 
          window.document.getElementById("txtCreator").value =ret[1]; 
            } 

        }
        return false;
      }
      
      function  btnSelectClick(requestID)
      {
        var url='RequestProcess.aspx?loginType=1&IsAgent=1&RequestID=' + requestID;
        window.open ( url , "_blank" ,"height=500,width=800,scrollbars=yes,location=no,resizable=yes,menubar=no,toolbar=no,top=10,left=50" ) ;

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
                    <cc2:Button ID="btnQuery" runat="server" ButtonImgUrl="../images/search.gif" Enabled="false" ShowPostDiv="false" Text="Button_Search" ValidateForm="false" AutoPostBack="true" Width="80" OnClick="btnQuery_Click" Page_ClientValidate="false" />
                    <cc2:Button ID="btnReturn" ShowPostDiv="false" runat="server" Text="Button_Return" ButtonImgUrl="../images/browse.gif" AutoPostBack="true" Page_ClientValidate="false" Disable="false" ValidateForm="false" OnClick="btnReturn_Click" />
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label12" Font-Bold="true" Font-Size="9pt" runat="server" Text="流程查询"></asp:Label></legend>
                        <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        <input type="hidden" id="txtCreatorID" runat="server" />
                                        <input type="hidden" id="txtWorkflowID" runat="server" />
                                        <div class="clear">
                                            <div class="third1">
                                                <label class="char5">
                                                    类型：</label><div class="iptblk">
                                                        <cc2:DropDownList ID="ddlFormType" runat="server" Width="150">
                                                        </cc2:DropDownList>
                                                    </div>
                                            </div>
                                            <div class="third1">
                                                <label class="char5">
                                                    节点类型：</label><div class="iptblk">
                                                        <cc2:DropDownList ID="ddlNodeTypeID" runat="server" Width="150">
                                                        </cc2:DropDownList>
                                                    </div>
                                            </div>
                                            <div class="third1">
                                                <label class="char5">
                                                    状况：</label><div class="iptblk">
                                                        <cc2:DropDownList ID="ddlStatus" runat="server" Width="150">
                                                            <asp:ListItem Value="0">有效</asp:ListItem>
                                                            <asp:ListItem Value="1">无效</asp:ListItem>
                                                            <asp:ListItem Value="">全部</asp:ListItem>
                                                        </cc2:DropDownList>
                                                    </div>
                                            </div>
                                            <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="clear">
                                            <div class="third1">
                                                <label class="char5">
                                                    工作流：</label><div class="iptblk">
                                                        <cc2:TextBox ID="txtWorkflowN" runat="server" Width="150"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImgForm" ToolTip="搜索" runat="server" OnClientClick="return btnWorkflowClick();" /></div>
                                            </div>
                                            <div class="third1">
                                                <label class="char5">
                                                    创建日期：</label><div class="iptblk">
                                                        <cc2:TextBox ID="txtStartDate" runat="server" Width="70" ReadOnly="true"></cc2:TextBox><cc1:CalendarExtender ID="StartDateCalendarExtender" runat="server" TargetControlID="txtStartDate" Format="yyyy-MM-dd" PopupPosition="TopRight" />
                                                        &nbsp;—&nbsp;
                                                        <cc2:TextBox ID="txtEndDate" runat="server" Width="70" ReadOnly="true"></cc2:TextBox><cc1:CalendarExtender ID="EndDateCalendarExtender" runat="server" TargetControlID="txtEndDate" Format="yyyy-MM-dd" PopupPosition="TopRight" />
                                                    </div>
                                            </div>
                                            <div class="third1">
                                                <label class="char5">
                                                    创建人：</label><div class="iptblk">
                                                        <cc2:TextBox ID="txtCreator" runat="server" Width="150"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImageButton1" ToolTip="搜索" runat="server" OnClientClick="return btnUserClick(3);" /></div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                            <input type="hidden" id="txtBeAgentID" runat="server" />
                                            <input type="hidden" id="txtAgentID" runat="server" />
                                            <div class="third1">
                                                <label class="char5">
                                                    代理人：</label><div class="iptblk">
                                                        <cc2:TextBox ID="txtAgentN" runat="server" Width="150"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImageButton3" ToolTip="搜索" runat="server" OnClientClick="return btnUserClick(1);" /></div>
                                            </div>
                                            <div class="third1">
                                                <label class="char5">
                                                    被代理人：</label><div class="iptblk">
                                                        <cc2:TextBox ID="txtBeAgentN" runat="server" Width="150"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImageButton4" ToolTip="搜索" runat="server" OnClientClick="return btnUserClick(2);" /></div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </div>
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
                            <div style="text-align: left">
                                <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" EnableModelValidation="false" Width="100%" DataKeyNames="RequestID">
                                    <Columns>
                                        <asp:BoundField DataField="CreateDate" ItemStyle-Width="120px" HeaderText="创建日期" />
                                        <asp:BoundField DataField="WorkflowName" ItemStyle-Width="100px" HeaderText="工作流" />
                                        <asp:BoundField DataField="NodeTypeName" ItemStyle-Width="80px" HeaderText="当前节点" />
                                        <asp:BoundField DataField="Creator" ItemStyle-Width="80px" HeaderText="创建人" />
                                        <asp:BoundField DataField="AgentPerson" ItemStyle-Width="80px" HeaderText="代理人" />
                                        <asp:BoundField DataField="BeAgentPerson" ItemStyle-Width="80px" HeaderText="被代理人" />
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                请求标题</HeaderTemplate>
                                            <ItemTemplate>
                                                <a style="color: Blue; cursor: hand" onclick="return btnSelectClick('<%# DataBinder.Eval(Container, "DataItem.RequestID") %>');">
                                                    <%# DataBinder.Eval(Container, "DataItem.RequestName") %></a>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RequestStatus" ItemStyle-Width="80px" HeaderText="当前状况" />
                                    </Columns>
                                    <ContextMenus>
                                        <yyc:ContextMenu Text="版权所有" NavigateUrl="http://www.geobyev.com" Target="_blank" />
                                    </ContextMenus>
                                </yyc:SmartGridView>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <input type="hidden" id="hiddenType" runat="server" /><!--区分是待办、已办、办结、我的请求（已完成、未完成）、查询流程-->
        <input type="hidden" id="hiddenIsFinish" runat="server" /><!--我的请求（已完成、未完成）-->
        <input type="hidden" id="hiddenFormTypeID" runat="server" /><!--表单类型 --待办、已办、办结、我的请求（已完成、未完成）用到-->
        <!----流程类型  <input type="hidden" id="hiddenWorkflowID" runat="server" />-->
        <input type="hidden" id="hiddenRequestID" runat="server" /><!--请求ID-->
        <input type="hidden" id="hiddenDeptID" runat="server" /><!--创建者所属部门-->
        <input type="hidden" id="hiddenTitle" runat="server" /><!--表单或单据标题-->
    </div>
    </form>
</body>
</html>
