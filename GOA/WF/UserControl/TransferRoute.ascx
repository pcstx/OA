<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransferRoute.ascx.cs" Inherits="GOA.UserControl.TransferRoute" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="ManagerForm">
            <fieldset>
                <div class="clear">
                    <!--page start-->
                    <div class="pager">
                        <div class="PagerText">
                            <input type="hidden" id="txtRequestID" runat="server" />
                            <cc2:TextBox ID="txtPageSize" runat="server" Width="35" TextMode="SingleLine" SetFocusButtonID="btnPageSize"></cc2:TextBox>
                            <cc2:Button ID="btnPageSize" Text="Button_GO" runat="server" Width="60" OnClick="txtPageSize_TextChanged" ShowPostDiv="false" />
                        </div>
                    </div>
                    <!--page end-->
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="20" OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging" Width="100%" DataKeyNames="ID">
                            <Columns>
                                <asp:BoundField DataField="NodeName" HeaderText="节点" ItemStyle-Width="90px" />
                                <asp:BoundField DataField="OperateComment" HeaderText="意见" ItemStyle-Width="180px" />
                                <asp:BoundField DataField="OperatorName" HeaderText="操作者" ItemStyle-Width="100px" />
                                <asp:BoundField DataField="OperateDateTime" HeaderText="操作时间" ItemStyle-Width="100px" DataFormatString="{0:yyy-MM-dd HH:mm}" />
                                <asp:BoundField DataField="OperateTypeN" HeaderText="操作" ItemStyle-Width="60px" />
                                <asp:BoundField DataField="ReceivListN" HeaderText="接受人" />
                            </Columns>
                        </yyc:SmartGridView>
                    </asp:Panel>
                </div>
            </fieldset>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnPageSize" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
