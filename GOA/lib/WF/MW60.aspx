<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MW60.aspx.cs" Inherits="GOA.MW60" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>流程监控</title>

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
      
      function  btnSelectClick(requestID)
      {
        var url='RequestProcess.aspx?loginType=1&IsAgent=0&RequestID=' + requestID;
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
                    <cc2:Button ID="btnDel" runat="server" ButtonImgUrl="../images/del.gif" Enabled="false" Page_ClientValidate="false" ShowPostDiv="false" Text="Button_Del" ValidateForm="false" AutoPostBack="true" Width="80" OnClick="btnDel_Click" />
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
                                                        <cc2:TextBox ID="txtCreator" runat="server" Width="150"></cc2:TextBox><asp:ImageButton ImageUrl="../images/arrow_black.gif" ImageAlign="Middle" ID="ImageButton1" ToolTip="搜索" runat="server" OnClientClick="return btnUserClick();" /></div>
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
                                <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True" AutoGenerateColumns="False" EnableModelValidation="false" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" Width="100%" DataKeyNames="RequestID">
                                    <Columns>
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
                                                请求标题</HeaderTemplate>
                                            <ItemTemplate>
                                                <a style="color: Blue; cursor: hand" onclick="return btnSelectClick('<%# DataBinder.Eval(Container, "DataItem.RequestID") %>');">
                                                    <%# DataBinder.Eval(Container, "DataItem.RequestName") %></a>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CreateDate" ItemStyle-Width="100px" HeaderText="创建日期" />
                                        <asp:BoundField DataField="Creator" ItemStyle-Width="100px" HeaderText="创建人" />
                                        <asp:BoundField DataField="WorkflowName" ItemStyle-Width="100px" HeaderText="工作流" />
                                        <asp:BoundField DataField="NodeTypeName" ItemStyle-Width="100px" HeaderText="当前节点" />
                                        <asp:BoundField DataField="RequestStatus" ItemStyle-Width="100px" HeaderText="当前状况" />
                                        <asp:BoundField DataField="CurrentOperator" ItemStyle-Width="100px" HeaderText="未操作者" />
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                功能管理</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btnselect" Text="强制收回" CommandName="callback" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="btnS" Text="强制归档" CommandName="process" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
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
    </div>
    </form>
</body>
</html>
