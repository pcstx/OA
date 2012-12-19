﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestProcess.aspx.cs" Inherits="GOA.RequestProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="UserControl/FormDetailGroup.ascx" TagName="FormDetailGroup" TagPrefix="uc1" %>
<%@ Register Src="UserControl/FileUploadControl.ascx" TagName="FileUploadControl" TagPrefix="uc2" %>
<%@ Register Src="UserControl/TransferRoute.ascx" TagName="TransferRoute" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../JScript/DateTimePicker/WdatePicker.js"></script>
</head>
<body style="padding-left: 20px; padding-top: 20px; padding-right: 10px;">
    <form id="form1" runat="server">
    <div>
        <div id="loadMsg" style="display: none; position: relative; border: 1px dotted #DBDDD3; background-color: #FDFFF2; margin: auto; padding: 10px" width="90%">
            <img border="0" alt="" src="../images/ajax-loader.gif" />
            页面加载中,请稍候.....</div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True" EnablePartialRendering="true">
        </cc1:ToolkitScriptManager>
        <fieldset>
            <div style="padding-left: 10px; padding-top: 10px; padding-right: 10px;">
                <asp:panel id="description_MainPanel" runat="server" style="cursor: pointer;">
          <asp:imagebutton id="img_MainToggle" runat="server" imageurl="~/images/collapse.jpg" alternatetext="收缩/展开" />基本信息
<asp:label id="lblMain" runat="server">(隐藏详细情况)</asp:label>
        </asp:panel>
                <asp:panel id="description_MainContentPanel" runat="server">
                    <asp:updatepanel id="UpdatePanel1" runat="server" updatemode="Conditional">
                        <contenttemplate>
              <div style="margin-left: 0px; margin-top: 0px;">
                <fieldset>
                  <div style="text-align: center; font-size: larger; font-weight: bold">
                    <asp:Label ID="lblRequestName" runat="server"></asp:Label></div>
                    <div style="text-align: :right; font-size: larger; font-weight: bold"><asp:HyperLink  Text="打印" ID="lkbPrint" runat="server" ></asp:HyperLink>
                  </div>
                  <div>
                  </div><br />
                  <div class="clear">
                    <asp:PlaceHolder ID="placeHolder" runat="server"></asp:PlaceHolder>
                  </div>
                </fieldset>
              </div>
            </contenttemplate>
                    </asp:updatepanel>
                </asp:panel>
                <asp:panel id="description_DetailPanel" runat="server" style="cursor: pointer;">
          <asp:imagebutton id="img_DetailToggle" runat="server" imageurl="~/images/collapse.jpg" alternatetext="收缩/展开" />明细信息
<asp:label id="lblDetail" runat="server">(隐藏详细情况)</asp:label>
        </asp:panel>
                <asp:panel id="description_DetailContentPanel" runat="server">
                    <div class="clear conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                        <asp:placeholder id="placeHolderDetail" runat="server"></asp:placeholder>
                    </div>
                </asp:panel>
                <asp:panel id="description_TransferPanel" runat="server" style="cursor: pointer;">
          <asp:imagebutton id="img_TransferToggle" runat="server" imageurl="~/images/collapse.jpg" alternatetext="收缩/展开" />流转过程
<asp:label id="lblTransfer" runat="server">(隐藏详细情况)</asp:label>
        </asp:panel>
                <asp:panel id="description_TransferContentPanel" runat="server">
                    <fieldset>
                        <uc3:TransferRoute ID="TransferRoute1" runat="server" />
                    </fieldset>
                </asp:panel>
                <asp:panel id="description_OperatePanel" runat="server" style="cursor: pointer;">
          <asp:imagebutton id="img_OperateToggle" runat="server" imageurl="~/images/collapse.jpg" alternatetext="收缩/展开" />操作动作
<asp:label id="lblOperate" runat="server">(隐藏操作动作)</asp:label>
        </asp:panel>
                <asp:panel id="description_OperateContentPanel" runat="server">
          <div runat="server" id="divOperateComment">
              <fieldset>
                  <div class="clear conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
                      <label class="char5">
                          意见</label><div class="iptblk">
                              <cc2:TextBox ID="txtOperateComment" runat="server" Width="200" TextMode="MultiLine" Rows="4"></cc2:TextBox></div>
                  </div>
              </fieldset>
          </div>
          <cc2:Button ID="btnSave" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false" ShowPostDiv="true" Text="Button_Save" ValidateForm="false" Page_ClientValidate="true" AutoPostBack="true" OnClick="SaveRequestData" />&nbsp;&nbsp;
          <cc2:Button ID="btnSubmit" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false" ShowPostDiv="true" Text="Button_DafaultSubmit" ValidateForm="false" Page_ClientValidate="true" AutoPostBack="true" OnClick="SubmitRequestData"  />&nbsp;&nbsp;
          <cc2:Button ID="btnAgree" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false" ShowPostDiv="true" Text="Button_Agree" ValidateForm="false" Page_ClientValidate="true" AutoPostBack="true" OnClick="SubmitRequestData"  />&nbsp;&nbsp;
          <cc2:Button ID="btnReturn" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false" ShowPostDiv="true" Text="Button_DisAgree" ValidateForm="false" Page_ClientValidate="true" AutoPostBack="true" OnClick="SubmitRequestData" />&nbsp;&nbsp;
          <cc2:Button ID="btnProcess" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false" ShowPostDiv="true" Text="Button_Process" ValidateForm="false" Page_ClientValidate="true" AutoPostBack="true" OnClick="SubmitRequestData" />&nbsp;&nbsp;
          <cc2:Button ID="btnClose" runat="server" ButtonImgUrl="../images/query.gif" Enabled="false" ShowPostDiv="true" Text="Button_Close" ValidateForm="false" Page_ClientValidate="true" AutoPostBack="false" />
            <asp:TextBox ID="TextBox1" runat="server" style="display:none"></asp:TextBox>
        </asp:panel>
            </div>
        </fieldset>
    </div>
    <cc1:CollapsiblePanelExtender ID="cpeOperate" runat="Server" TargetControlID="description_OperateContentPanel" ExpandControlID="description_OperatePanel" CollapseControlID="description_OperatePanel" Collapsed="false" TextLabelID="lblOperate" ExpandedText="(隐藏详细情况)" CollapsedText="(显示详细情况)" ExpandedImage="~/images/collapse_blue.jpg" CollapsedImage="~/images/expand_blue.jpg" ImageControlID="img_OperateToggle" SuppressPostBack="true" />
    <cc1:CollapsiblePanelExtender ID="cpeMain" runat="Server" TargetControlID="description_MainContentPanel" ExpandControlID="description_MainPanel" CollapseControlID="description_MainPanel" Collapsed="false" TextLabelID="lblMain" ExpandedText="(隐藏详细情况)" CollapsedText="(显示详细情况)" ExpandedImage="~/images/collapse_blue.jpg" CollapsedImage="~/images/expand_blue.jpg" ImageControlID="img_MainToggle" SuppressPostBack="true" />
    <cc1:CollapsiblePanelExtender ID="cpeDetail" runat="Server" TargetControlID="description_DetailContentPanel" ExpandControlID="description_DetailPanel" CollapseControlID="description_DetailPanel" Collapsed="false" TextLabelID="lblDetail" ExpandedText="(隐藏详细情况)" CollapsedText="(显示详细情况)" ExpandedImage="~/images/collapse_blue.jpg" CollapsedImage="~/images/expand_blue.jpg" ImageControlID="img_DetailToggle" SuppressPostBack="true" />
    <cc1:CollapsiblePanelExtender ID="cpeTransfer" runat="Server" TargetControlID="description_TransferContentPanel" ExpandControlID="description_TransferPanel" CollapseControlID="description_TransferPanel" Collapsed="true" TextLabelID="lblTransfer" ExpandedText="(隐藏详细情况)" CollapsedText="(显示详细情况)" ExpandedImage="~/images/collapse_blue.jpg" CollapsedImage="~/images/expand_blue.jpg" ImageControlID="img_TransferToggle" SuppressPostBack="true" />
    </form>
</body>
</html>
