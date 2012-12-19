<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileUploadControl.ascx.cs" Inherits="GOA.UserControl.FileUploadControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="ManagerForm">
            <table border="0">
                <tr>
                    <td>
                    <div>
                        <div id="DivEditArea" runat="server">
                            档案:<asp:FileUpload ID="fileUp" runat="server" />&nbsp;&nbsp;
                            <cc2:Button ID="btnUpload" runat="server" ButtonImgUrl="../images/add.gif" ShowPostDiv="false" ValidateForm="false" Text="Button_UploadFile" AutoPostBack="true" Width="100" OnClick="UploadFile" />&nbsp;&nbsp;
                            <cc2:Button ID="btnDel" runat="server" ButtonImgUrl="../images/del.gif" ShowPostDiv="false" ValidateForm="false" Text="Button_DelAttach" AutoPostBack="true" Width="100" OnClick="RemoveAttach" /></div>
                            <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="false" AutoGenerateColumns="False" PageSize="20" Width="100%" EmptyDataText="暂无附件" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            序号
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            选择</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Item" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            文件</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFile" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <CheckedRowCssClass CheckBoxID="Item" CssClass="SelectedRow" />
                                <PagerSettings Visible="false" />
                            </yyc:SmartGridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnUpload" />
        <asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
