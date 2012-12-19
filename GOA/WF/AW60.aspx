<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AW60.aspx.cs" Inherits="GOA.AW60" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <link href="../styles/StyleSheet.css" type="text/css" rel="stylesheet" />
    <title>代理流程--暂时不用</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="titlebox" style="float: left; width: 100%; overflow: auto">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label12" runat="server" Text=" * 代理请求"></asp:Label></legend>
                        <table id="Table3" cellspacing="0" cellpadding="0" width="90%" border="0">
                            <tbody>
                                <tr>
                                    <td valign="top">
                                        <!--主菜单开始-->
                                        <asp:DataList ID="dlMain" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" OnItemDataBound="dlMain_ItemDataBound" Width="100%">
                                            <ItemTemplate>
                                                <table id="Table13" cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                                    <tr bgcolor="#f5f5f5">
                                                        <td width="30%" height="30">
                                                            <asp:Label ID="lbl" runat="server" Font-Bold="true" Font-Size="10pt"><%# Eval("FormTypeName")%></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <!--子菜单开始-->
                                                    <asp:DataList ID="dlSub" Width="100%" runat="server" RepeatColumns="1">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="height: 24px; vertical-align: middle">
                                                                    <img src="../images/star.gif" alt="" />
                                                                    <a target="_blank" href="RequestAdd.aspx?IsAgent=1&WorkflowID=<%# DataBinder.Eval(Container.DataItem,"WorkflowID") %>">
                                                                        <%# DataBinder.Eval(Container.DataItem, "WorkflowName")%>
                                                                    </a>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </table>
                                                <!--子菜单结束-->
                                            </ItemTemplate>
                                        </asp:DataList>
                                        <!--主菜单结束-->
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </fieldset>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
