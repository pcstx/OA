<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MW30.aspx.cs" Inherits="GOA.MW30" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <link href="../styles/StyleSheet.css" type="text/css" rel="stylesheet" />
    <title>我的请求</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="float: left; width: 100%; overflow: auto">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Labeltitle" runat="server">我的请求 * 查看</asp:Label></legend>
                        <table width="100%">
                            <tr>
                                <td style="width: 50%" valign="top">
                                    <asp:Label ID="lblUnfinish" runat="server">未完成</asp:Label><br />
                                    <hr style="width: 90%; height: 2px" />
                                    <asp:DataList ID="dlUMain" runat="server" OnItemDataBound="dlUMain_ItemDataBound" RepeatColumns="2" RepeatLayout="Flow" RepeatDirection="Vertical">
                                        <ItemTemplate>
                                            <table width="50%">
                                                <tr>
                                                    <td style="height: 22px; font-size: 10pt; font-weight: bold">
                                                        <a href="<%# "MW50.aspx?IsFinished=0&Type=4&WorkflowID=&FormTypeID=" + DataBinder.Eval(Container,"DataItem.FlowTypeID") %>">
                                                            <%# Eval("FormTypeName")%></a>
                                                        <asp:Label ID="lblUMain" runat="server">（ <%# Eval("TotalNum")%> ）</asp:Label>
                                                    </td>
                                                </tr>
                                                <asp:Repeater ID="repeaterUSub" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="height: 22px; vertical-align: middle">
                                                                <img src="../images/star.gif" alt="" />
                                                                <a href="<%# "MW50.aspx?IsFinished=0&Type=4&FormTypeID=&WorkflowID=" + DataBinder.Eval(Container,"DataItem.WorkFlowID") %>">
                                                                    <%#DataBinder.Eval(Container, "DataItem.WorkFlowName")%>
                                                                </a>
                                                                <asp:Label ID="lblUSub" runat="server">( <%# Eval("TotalNum")%> )</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                                <td style="width: 50%" valign="top">
                                    <asp:Label ID="lblFinish" runat="server">已完成</asp:Label><br />
                                    <hr style="width: 90%; height: 2px" />
                                    <asp:DataList ID="dlMain" runat="server" OnItemDataBound="dlMain_ItemDataBound" RepeatColumns="2" RepeatLayout="Flow" RepeatDirection="Vertical">
                                        <ItemTemplate>
                                            <table width="50%">
                                                <tr>
                                                    <td style="height: 22px; font-size: 10pt; font-weight: bold">
                                                        <a href="<%# "MW50.aspx?IsFinished=1&Type=4&WorkflowID=&FormTypeID=" + DataBinder.Eval(Container,"DataItem.FlowTypeID") %>">
                                                            <%# Eval("FormTypeName")%></a>
                                                        <asp:Label ID="lblMain" runat="server">（ <%# Eval("TotalNum")%> ）</asp:Label>
                                                    </td>
                                                </tr>
                                                <asp:Repeater ID="repeaterSub" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="height: 22px; vertical-align: middle">
                                                                <img src="../images/star.gif" alt="" />
                                                                <a href="<%# "MW50.aspx?IsFinished=1&Type=4&FormTypeID=&WorkflowID=" + DataBinder.Eval(Container,"DataItem.WorkFlowID") %>">
                                                                    <%#DataBinder.Eval(Container, "DataItem.WorkFlowName")%>
                                                                </a>
                                                                <asp:Label ID="lblSub" runat="server">( <%# Eval("TotalNum")%> )</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
