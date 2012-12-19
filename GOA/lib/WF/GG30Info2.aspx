<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG30Info2.aspx.cs" Inherits="GOA.GG30Info2" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Src="UserControl/FormField.ascx" TagName="FormField" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>行规则</title>

    <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>

    <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    </script>

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" EnablePageMethods="True"
        ID="ScriptManager1" />
    <div>
        <div class="conblk2" xmlns:fo="http://www.w3.org/1999/XSL/Format">
            <div class="clear">
                <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                    <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="表单基本信息">
                        <ContentTemplate>
                            <iframe scrolling="auto" width="100%" height="700px" src="GG30Form.aspx?id=<%=Request.Params["fmid"]%>">
                            </iframe>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
