<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataSourceSelect.aspx.cs" Inherits="GOA.DataSourceSelect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>数据源查询</title>

    <script language="javascript" type="text/javascript">
    
    function $( id ){return document.getElementById( id );}
    String.prototype.Trim = function() {return this.replace(/(^\s*)|(\s*$)/g, ''); };
    
    function setds(dsid,dsname)
    {
      var ret=new Array(2);
      ret[0]=dsid 
      ret[1]=dsname 
      window.returnValue=ret;
      window.close();
    }
     
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div class="ManagerForm">
                    <fieldset>
                        <legend style="background: url(images/legendimg.jpg) no-repeat 6px 50%;">
                            <asp:Label ID="lblBigTitle" runat="server" Text="搜索条件框"></asp:Label></legend>
                        <div class="clear">
                            <div class="half">
                                <label class="char5">
                                    <asp:Label ID="lblDataSourceID" runat="server" Text="数据源ID"></asp:Label></label><div class="iptblk">
                                        <cc2:TextBox ID="txtDataSourceID" runat="server" Width="85"></cc2:TextBox></div>
                            </div>
                            <div class="half">
                                <label class="char5">
                                    <asp:Label ID="lblDataSourceName" runat="server" Text="数据源名称"></asp:Label></label><div class="iptblk">
                                        <cc2:TextBox ID="txtDataSourceName" runat="server" Width="85"></cc2:TextBox></div>
                            </div>
                        </div>
                        <div class="clear">
                            <div class="oneline">
                                <cc2:Button ID="btnSearch" runat="server" ButtonImgUrl="../images/search.gif" Page_ClientValidate="false" ShowPostDiv="false" Text="Button_Search" AutoPostBack="true" Width="80" OnClick="SearchDataSource" />
                                <button id="btnReset" type="reset" class="ManagerButton">
                                    <img src="../images/edit.gif" alt="" />重 置</button><!--<cc2:Button ID="btnReturn" runat="server"  ScriptContent="ReturnDataSetPage()" Page_ClientValidate="false" ShowPostDiv="false"  Text="Button_Cancel" AutoPostBack="false"  Width="80"   ButtonImgUrl="../images/browse.gif" />-->
                            </div>
                        </div>
                        <div class="clear">
                            <div class="oneline">
                                <label style="width: 300px;">
                                    <!--label-->
                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></label></div>
                        </div>
                    </fieldset>
                </div>
                <!--page start-->
                <div id="pager">
                    <div class="PagerArea">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanging="AspNetPager1_PageChanging" OnPageChanged="AspNetPager1_PageChanged" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Page: " HorizontalAlign="left" CenterCurrentPageButton="true" NumericButtonCount="9">
                        </webdiyer:AspNetPager>
                    </div>
                    <div class="PagerText">
                        <asp:Label ID="lblPageSize" runat="server" Text="每页记录数"></asp:Label>&nbsp;<cc2:TextBox ID="txtPageSize" runat="server" Width="35" TextMode="SingleLine" SetFocusButtonID="btnPageSize"></cc2:TextBox>
                        <cc2:Button ID="btnPageSize" Text="Button_GO" runat="server" Width="60" OnClick="txtPageSize_TextChanged" ShowPostDiv="false" ButtonImgUrl="../images/submit.gif" />
                    </div>
                </div>
                <!--每页记录数的TextBox只能输入数字-->
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPageSize" FilterType="Numbers" />
                <!--page end-->
                <div class="clear">
                    <div class="oneline">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="true" CellSpacing="1" CellPadding="1" HeaderStyle-BackColor="buttonface" AllowPaging="true" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="操作">
                                    <HeaderStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" Width="40px"></HeaderStyle>
                                    <ItemTemplate>
                                        <a style="color: Blue; cursor: hand" onclick="setds('<%# DataBinder.Eval(Container, "DataItem.DataSourceID") %>','<%# DataBinder.Eval(Container, "DataItem.DataSourceName") %>')">选择</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="数据源ID" DataField="DataSourceID" ItemStyle-Width="40px" ReadOnly="true" />
                                <asp:BoundField HeaderText="数据源名称" DataField="DataSourceName" ItemStyle-Width="100px" ReadOnly="true" />
                                <asp:BoundField DataField="DataSourceDBType" HeaderText="数据库类型" ItemStyle-Width="50px" ReadOnly="true" />
                                <asp:BoundField DataField="ConnectString" HeaderText="连接字符串" ItemStyle-Width="200px" ReadOnly="true" />
                            </Columns>
                            <PagerSettings Visible="false" />
                        </asp:GridView>
                    </div>
                </div>
                <!--validate start-->
                <!--validate end-->
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="HiddenDataSourceID" runat="server" />
    </div>
    </form>
</body>
</html>
