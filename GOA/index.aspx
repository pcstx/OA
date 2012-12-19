<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="GOA.index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>OA</title>
  <!--这里的样式的重点是指当鼠标移到三角按钮时变成手掌-->
  <style type="text/css">
    .navPoint
    {
      color: white;
      cursor: hand;
      font-family: Webdings;
      font-size: 9pt;
    }
    
    #newsList li{
	text-indent:10px;
	font-size:13px;
	line-height: 20px;
	float: left;
	width:320px;
	white-space:nowrap;
	overflow:hidden;
	font-weight:bold;
	font-family:Nina ,Arial  ;
	
}
  </style>
</head>
<!--onresize 事件 发生在窗口被调整大小的时候。carnoc是左边FRAME的ID。-->
<body style="margin: 0px" scroll="yes"  onresize="javascript:parent.carnoc.location.reload()">
  <form id="form1" runat="server">
  <div>
    <!--顶部区域-->
    <div id="Top">
      <div id="Top_links">
        <a href="javascript:;" title="设置捷奥比工作流程为浏览器首页" onclick="this.style.behavior='url(#default#homepage)';this.setHomePage('http://www.geobyev.com');" style="color: #FFDF55">设为首页</a><%-- | <a href="http://www.geobyev.com" target="_blank">捷奥比官方网站</a> | <a href="http://geobyev.cn.alibaba.com/" target="_blank">阿里巴巴·捷奥比网上商城</a> | <a href="http://shop35334500.taobao.com/" target="_blank">淘宝·捷奥比网上商城</a>--%></div>
      <div id="Top_CurrentUser">
        当前用户：<asp:Label ID="lblCurrentUserName" runat="server" Text=""></asp:Label>
        <a href="logout.aspx">[ 注销 ]</a></div>
    </div>
    <!--以下代码就是重点，屏幕切换点击后相应的向左或者向右展开-->

    <script language="javascript" type="text/javascript"> 
if(self!=top){top.location=self.location;}

function switchSysBar()
{ 
    if (switchPoint.innerText==3){ 
        switchPoint.innerText=4 ;
        document.all("frmTitle").style.display="none" ;
    }else{ 
        switchPoint.innerText=3 ;
        document.all("frmTitle").style.display="" ;
    }
}
function OpenNewWindow(gopage)
{
  var url=gopage;
  window.open (url, "gopage" ,"height=720,width=800,scrollbars=yes,location=no,resizable=yes,menubar=no,toolbar=no,top=10,left=50" ) ;
  return false;
}

function btnAnnounceClose_Click()
{
 document.all("divAnnounce").style.display="none" ;
}

function btnMoreAnnounce_Click()
{
 window.document.getElementById("main").src="SM/AnnounceList.aspx" ;
 document.all("divAnnounce").style.display="none" ;
}

    </script>

    <!--屏幕切换点击后相应的向左或者向右展开-->
    <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
      <%--<tr><td><div id="loadMsg" style=" position: relative; border: 1px dotted #DBDDD3; background-color: #FDFFF2; margin: auto; padding: 10px" width="90%">
            <img border="0" alt="" src="../../images/ajax-loader.gif" />页面加载中,请稍候.....</div>
            </td>
       </tr>--%>
      <tr>
        <td valign="top" align="center" nowrap id="frmTitle">
          <!--注意这里的ID，它返回给上面那段javascript的-->
          <!--以下是左边的FRAME,你只要做一个宽为180PX的页面嵌套进去就可以了。当然你也可以修改这句里WIDTH的值为你叶子的宽度-->
          
          <iframe frameborder="0" id="carnoc" name="carnoc" scrolling="auto" src="leftmenutree.aspx" style="width: 180px; height: 700px;"></iframe>
        </td>
        <td bgcolor="#62a3d0" style="width: 5px;">
          <table width="5" height="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
              <td style="height: 100%" onclick="switchSysBar()">
                <font style="font-size: 9pt; cursor: default; color: #ffffff">
                  <span class="navPoint" id="switchPoint" title="关闭/打开左栏">3</span></font>
              </td>
            </tr>
          </table>
        </td>
      <td width="768" style="width: 100%; height: 700px; vertical-align: top; padding: 10px 10px 10px 10px; overflow:auto ">
           <div id="loadMsg" style=" position: relative; border: 1px dotted #DBDDD3; background-color: #FDFFF2; margin: auto; padding: 10px" width="90%">
           <!-- <img border="0" alt="" src="../../images/ajax-loader.gif" />页面加载中,请稍候.....</div> -->
          <iframe frameborder="0" runat="server" id="main" name="main" scrolling="auto" width="100%" height="700px" src="aboutMES.aspx"></iframe>
        </td>
      </tr>
    </table>
    
    <table style="border-collapse: collapse;" cellspacing="0" cellpadding="0" width="100%" border="0">
      <tr>
        <td align="center" width="100%" height="30" style="color: #330099; font: 10.5pt; background-color: #B1C1E0;">
          版权信息
        </td>
      </tr>
    </table>
  </div>
  
  <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel runat="server" ID="Up1">
        <ContentTemplate>
                <div runat="server" id="divAnnounce" style="width:320px;height:200px; background-color: #FFFFFF;border-width:2px; " >
                   <table   style="height:100%">
                   <tr style=" border-bottom-color:Purple ; border-bottom-style:dotted; border-bottom-width:2px; height:20px"><td style=" font-size:14px; font-weight:bold; width:80%"> 系统公告:</td>
                   <td style="width:20%; text-align:center">
                         <img alt="Close" id="btnAnnounceClose" style="border-color:Black; border-width:1px ; cursor:pointer;" runat="server"  src="~/images/close.gif"   onclick="btnAnnounceClose_Click()" />
                      </td>
                  </tr>
                        <tr style="vertical-align:top "><td colspan="2">
                            <div  style="width: 100%; height: 100%; vertical-align: middle; text-align:left ;">
                                <span id="AnnounceContent"   runat="server" />
                            </div>
                            </td>
                       </tr>
                       <tr style="height:20px">
                       <td colspan="2">
                       <div style="width: 100%; vertical-align: middle; text-align:right ;"> <img alt="更多公告" id="ImgMore" runat="server"  src="~/images/more.JPG"  style="cursor:pointer "  onclick="btnMoreAnnounce_Click()" /></div>
                       </td></tr>
                    </table>
                    <cc1:AlwaysVisibleControlExtender ID="avce" runat="server"
                        TargetControlID="divAnnounce"
                        VerticalSide="Bottom"
                        VerticalOffset="10"
                        HorizontalSide="Right"
                        HorizontalOffset="10"
                        ScrollEffectDuration=".1" />
                        
                 
                </div>
                   <cc1:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server"
            BehaviorID="RoundedCornersBehavior1"
            TargetControlID="divAnnounce"
            Radius="8" BorderColor ="#5377A9"  
            Corners="All" />
           </ContentTemplate>
        </asp:UpdatePanel>
        
  </form>
</body>

</html>
