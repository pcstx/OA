<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormDetailGroup.ascx.cs" Inherits="GOA.UserControl.FormDetailGroup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="ManagerForm">
            <div class="clear">
                <div class="oneline">
                    <!--page start-->
                    <div class="pager">
                        <!--textbox-->
                        <cc2:Button ID="btnAdd" runat="server" ButtonImgUrl="../images/add.gif" ShowPostDiv="false" ValidateForm="false" Text="Button_Add" AutoPostBack="true" Width="100" OnClick="AddDetailNewRow" />&nbsp;<cc2:Button ID="btnDel" runat="server" ButtonImgUrl="../images/del.gif" ShowPostDiv="false" ValidateForm="false" Text="Button_Del" AutoPostBack="true" Width="100" OnClick="DeleteDetailRow" />
                        <div class="PagerText">
                            <div class="PagerArea">
                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanging="AspNetPager1_PageChanging" OnPageChanged="AspNetPager1_PageChanged" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Page: " HorizontalAlign="left" CenterCurrentPageButton="true" NumericButtonCount="9">
                                </webdiyer:AspNetPager>
                            </div>
                            <div class="PagerText">
                                <cc2:TextBox ID="txtPageSize" runat="server" Width="35" TextMode="SingleLine" SetFocusButtonID="btnPageSize"></cc2:TextBox>
                                <cc2:Button ID="btnPageSize" Text="Button_GO" runat="server" Width="60" OnClick="txtPageSize_TextChanged" ShowPostDiv="false" />
                            </div>
                        </div>
                    </div>
                    <!--page end-->
                    <!--gridview start-->
                    <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="true" AutoGenerateColumns="False" PageSize="20" Width="100%"  OnRowDataBound="GridView1_RowDataBound" EmptyDataText="暂无数据">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    序号
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    选择</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Item" runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Visible="false" />
                        <FixRowColumn FixRowType="Pager"   TableWidth="100%" FixColumns="" />
                    </yyc:SmartGridView>
                    <!--grid view end-->
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnDel" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnPageSize" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
