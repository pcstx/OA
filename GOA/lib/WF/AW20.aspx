<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AW20.aspx.cs" Inherits="GOA.AW20" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <link href="../styles/StyleSheet.css" type="text/css" rel="stylesheet" />
    <title>代理流程 待办、已办、办结事宜查看</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="float: left; width: 100%; overflow: auto">
                    <fieldset style="height: 100%;">
                        <legend>
                            <asp:Label ID="lbltitle" runat="server" Font-Size="10pt" Text="待办事宜 * 查看"></asp:Label></legend>
                        <hr style="width: 100%; height: 2px" />
                        <asp:DataList ID="dlMain" runat="server" OnItemDataBound="dlMain_ItemDataBound" RepeatColumns="3" RepeatLayout="Flow" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <table width="33%">
                                    <tr>
                                        <td style="height: 22px; font-size: 10pt; font-weight: bold">
                                            <a href="<%# "AW50.aspx?Type="+ Request.QueryString["Type"].ToString() +"&WorkflowID=&FormTypeID=" + DataBinder.Eval(Container,"DataItem.FlowTypeID") %>">
                                                <%# Eval("FormTypeName")%></a>
                                            <asp:Label ID="lblMain" runat="server">（ <%# Eval("TotalNum")%> ）</asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="repeaterSub" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="height: 22px; vertical-align: middle">
                                                    <ul style="list-style-type: circle">
                                                        <li>
                                                            <img src="../images/star.gif" alt="" />
                                                            <a href="<%# "AW50.aspx?Type="+ Request.QueryString["Type"].ToString() +"&FormTypeID=&WorkflowID=" + DataBinder.Eval(Container,"DataItem.WorkFlowID") %>">
                                                                <%#DataBinder.Eval(Container, "DataItem.WorkFlowName")%>
                                                            </a>
                                                            <asp:Label ID="lblSub" runat="server">( <%# Eval("TotalNum")%> )</asp:Label></li></ul>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </fieldset>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
