<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCOperationBanner.ascx.cs" Inherits="AJAXWeb.ascx.UCOperationBanner" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc1" %>
    <cc1:Button ID="btnBrowseMode" runat="server" ShowPostDiv="false" ValidateForm="false" Text="Button_BrowseMode" Enabled="false" OnClick="btnBrowseMode_Click"  />
    <cc1:Button ID="btnEditMode" runat="server" ShowPostDiv="false" ValidateForm="false" Text="Button_EditMode" OnClick="btnBrowseMode_Click"   />
    <cc1:Button ID="btnAdd" runat="server" ShowPostDiv="false" ValidateForm="false" Text="Button_Add" OnClick="btnBrowseMode_Click"   />
	<cc1:Button ID="btnSubmit" runat="server" Text="Button_DafaultSubmit" OnClick="btnBrowseMode_Click"  />
	<cc1:Button ID="btnDel" runat="server" ShowPostDiv="false" ValidateForm="false" AutoPostBack="false" Text="Button_Del" ButtonImgUrl="../images/del.gif" OnClick="btnBrowseMode_Click"  />
