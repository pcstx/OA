<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MW10.aspx.cs" Inherits="GOA.MW10" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <link href="../styles/StyleSheet.css" type="text/css" rel="stylesheet" />
    <title>新建流程</title>
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
                            <asp:Label ID="Label12" runat="server" Text="新建 * 请求"></asp:Label></legend>
                        <asp:DataList ID="dlMain" runat="server" OnItemDataBound="dlMain_ItemDataBound" RepeatColumns="3" RepeatLayout="Flow" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <table width="33%">
                                    <tr>
                                        <td style="height: 22px;">
                                            <asp:Label ID="lbl" runat="server" Font-Bold="true" Font-Size="10pt"><%# Eval("FormTypeName")%></asp:Label>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="repeaterSub" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="height: 22px; vertical-align: middle">
                                                    <img src="../images/star.gif" alt="" />
                                                    <a target="_blank" href="<%# "RequestAdd.aspx?IsAgent=0&WorkflowID=" + DataBinder.Eval(Container,"DataItem.WorkFlowID") %>">
                                                        <%#DataBinder.Eval(Container, "DataItem.WorkFlowName")%>
                                                    </a>
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
