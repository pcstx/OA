<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG50Route.aspx.cs" Inherits="GOA.GG50Route" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>路径设置</title>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" EnablePageMethods="True" ID="ScriptManager1" />
    <div>
        <div class="clear">
            <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="基本信息">
                    <ContentTemplate>
                        <iframe frameborder="0" scrolling="auto" width="100%" height="700px" src="GG5001.aspx?id=<%=Request.Params["id"]%>"></iframe>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="出口信息">
                    <ContentTemplate>
                        <iframe frameborder="0" scrolling="auto" width="100%" height="700px" src="GG5003.aspx?id=<%=Request.Params["id"]%>"></iframe>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="功能管理">
                    <ContentTemplate>
                        <iframe frameborder="0" scrolling="auto" width="100%" height="700px" src="GG5004.aspx?id=<%=Request.Params["id"]%>"></iframe>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="触发流程">
                    <ContentTemplate>
                        <iframe frameborder="0" scrolling="auto" width="100%" height="700px" src="GG5005.aspx?id=<%=Request.Params["id"]%>"></iframe>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel6" runat="server" HeaderText="数据集成">
                    <ContentTemplate>
                        <iframe frameborder="0" scrolling="auto" width="100%" height="700px" src="GG5006.aspx?id=<%=Request.Params["id"]%>"></iframe>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </div>
    </div>
    </form>
</body>
</html>
