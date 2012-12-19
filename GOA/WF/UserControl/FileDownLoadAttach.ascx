<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileDownLoadAttach.ascx.cs" Inherits="GOA.WF.UserControl.FileDownLoadAttach" %>

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
                 
                         
                            <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="false" AutoGenerateColumns="False" PageSize="20" Width="100%" EmptyDataText="暂无附件" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" DataKeyNames="AttachmentUrl,AttachClientName">
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
                         <%--           <asp:TemplateField>
                                        <HeaderTemplate>
                                            选择</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Item" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>--%>
                                    
                               <asp:TemplateField>
                                        <HeaderTemplate>
                                            文件</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFile" runat="server" ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <CheckedRowCssClass CheckBoxID="Item" CssClass="SelectedRow" />
                             
                            </yyc:SmartGridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>