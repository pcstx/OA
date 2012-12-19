<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="GOA.Index.Index"
    StylesheetTheme="" EnableTheming="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="nav.css" rel="stylesheet" type="text/css" />
    <link href="lib/ligerUI/skins/Aqua/css/ligerui-tab.css" rel="stylesheet" type="text/css" />
    <link href="lib/ligerUI/skins/Aqua/css/ligerui-menu.css" rel="stylesheet" type="text/css" />
    <link href="lib/ligerUI/skins/Aqua/css/ligerui-dialog.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/ligerui.min.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="ocscript.js" type="text/javascript"></script>
    <script type="text/javascript">
        var tab;
        $(function() {
            $.getJSON("index.ashx?type=menuList", function(jsonStr) {  //获取目录
                //二级目录
                var liStr = '<li class="imatm"><a class="jj" href="{0}" value="{1}" ><span class="imea imeam"><span></span></span>{2}</a>{3}</li>';

                var liHtml = genMenuHtml(jsonStr, liStr);
                $("#imouter0").html(liHtml);
                // $("#ultree").append('<form class="navbar-search pull-right"><input type="text" class="search-query text" placeholder="搜索"></form>');

                var height = $(window).height() - $(".nav_bg").height() - 2;
                $("#navtab1").ligerTab({ height: height });
                tab = $("#navtab1").ligerGetTabManager();

                adjustWidthHeight();
            });

            //左侧边栏点击
            $("#sildeContent>li>a").live("click", function(e) {
                var sc = $(e.target);
                var url = "../" + sc.attr("url");
                var text = sc.text();
                var id = sc.attr("value");
                addTabs(id, text, url);
            });

            //顶部导航栏点击
            $(".nav_bg ul > li a").live("click", function(e) {
                var ul = $(this).next();
                if (ul.length > 0) {
                    ul.show();
                    return false;
                }

                var t = $(this).attr("value");
                var v = $(this).text();
                $.getJSON("index.ashx?type=treeList&parentId=" + t, function(jsonStr) {
                    var headHtml = '<div class="nav-header">' + v + '</div>';
                    var liHtml = "";
                    for (var i in jsonStr) {
                        var secText = jsonStr[i].text;
                        var secUrl = jsonStr[i].navigateUrl == "" ? "#" : jsonStr[i].navigateUrl;
                        var secValue = jsonStr[i].value;
                        liHtml += '<li><a href="#" url="{0}" value="{1}">{2}</a></li>'.replace("{0}", secUrl).replace("{1}", secValue).replace("{2}", secText);
                    }
                    var ulHtml = headHtml + ' <div id="content"><ul class="nav nav-list" id="sildeContent">{0}</ul> </div>'.replace("{0}", liHtml);
                    $(".span2").html(ulHtml);
                });
            });

            $(window).resize(function() {
                adjustWidthHeight();
            });

            //////////////////////////////////////// 
            //初始化连接
            waitMsg(); 
            ///////////////////////////////////
        })

        //生成顶部目录，无限级
        function genMenuHtml(jsonStr, liStr) {
            var MenuHtml = "   <div class='imsc'><div class='imsubc' style='width:140px;top:-4px;left:2px;'><ul style=''>{0}</ul></div></div>";  //二级目录 <ul style="">  <ul class='dropdown-menu'>
            var m = "";
            if (liStr == null) {       //子集目录
                liStr = '<li><a class="jj" href="{0}" value="{1}">{2}</a>{3}</li>';    // <li><a href="#">
            }
            else {
                MenuHtml = '<ul id="imenus0">{0}</ul>';  //一级目录   <ul id="imenus0">   <ul  class="nav nav-pills">
            }

            $(jsonStr).each(function(index, element) {
                var itemText = element.text;
                var itemValue = element.value;
                var itemUrl = element.navigateUrl == "" ? "#" : element.navigateUrl;
                var l = "";

                if (element.children.length > 0) {
                    l = genMenuHtml(element.children);
                }
                m += liStr.replace("{0}", itemUrl).replace("{1}", itemValue).replace("{2}", itemText).replace("{3}", l);
            });

            MenuHtml = MenuHtml.replace("{0}", m);

            return MenuHtml;
        }

        function adjustWidthHeight() {
            var height = $(window).height() - $(".nav_bg").height() - $('.topBackgroup').height();
            var ss = $("#navtab1").height();

            $(".container-fluid").height(height + 4);
            tab.addHeight(height - ss);
        }

        function addTabs(id, text, url) {
            tab.addTabItem({
                tabid: id,
                text: text,
                url: url
            });
        }


        function f_tip(data) {
            var diaglogShow = $.ligerDialog.tip({ title: '提示信息', content: data, height: 200 });
            setTimeout(function() { diaglogShow.close(); }, 10000);
        }

        /////////////////////////////////////////
        function sendMsg(username) {
            //向comet_broadcast.asyn发送请求，消息体为文本框content中的内容，请求接收类为AsnyHandler
            var c = arguments[1];
            var u = username;
            var lu = $("#localuser").val();
            $.post("comet_broadcast.asyn", { content: c, username: u,localuser:lu });

            //清空内容
            //  $("#content").val("");
        }

        function waitMsg() {
            $.post("comet_broadcast.asyn", { content: "-1" },
            function(data, status) {
                 if (data != "false#@!") {
                     f_tip(data);
                 }
             //服务器返回消息,再次立连接
             waitMsg();
            }, "html");
        }
         
    </script>

    <style type="text/css">
        a:focus
        {
            outline: none;
        }
        .topBackgroup
        {
            background-color: #fefefd;
            background-image: url("images/header.jpg");
            background-position: right;
            background-repeat: no-repeat;
            height: 58px;
        }
        .jj
        {
            text-decoration: none;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }
        .jj:hover
        {
            text-decoration: none;
        }
    </style>
</head>
<body>
<form method="post" id="form"  name="form" runat="server" style="display:none">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
       
   </asp:ScriptManager>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>
       <asp:TextBox runat="server" ID="t1"></asp:TextBox>
           <asp:Button ID="Button1" runat="server" Text="Button" 
    onclick="Button1_Click" />
           <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
       </ContentTemplate>
   </asp:UpdatePanel>
     
    </form>
    <div>
        <div class="topBackgroup">
            <div style="font-size: 27px; color: #1073bf; top: 20px; left: 60px; font-family: '黑体';
                position: absolute;">
                OA协同办公系统              
            </div>
            <div style="float: right; color: White; position: absolute; top: 20px; right: 10px;
                font-weight: bold;">
                您好，
                <asp:Literal ID="Literalusername" runat="server"></asp:Literal>
                <a href="logout.aspx" style="color: White; font-weight: bold;">[ 退出 ]</a>
            </div>
        </div>
        <div class="nav_bg" style="z-index: 999999; position: relative; margin-top: -18px;">
            <div class="imcm imde" id="imouter0">
                <!--一级菜单end-->
            </div>
            <div class="imclear">
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">
            <!--  <div class="span2" style="border-right-color: #d2e0f2; border-right-style: solid;
                border-right-width: 5px;border-top:solid 1px #a3c0e8"> -->
            <div class="span2">
                <!--Sidebar content-->
                <div class="nav-header">
                    主 页</div>
                <div id="content">
                    <ul class="nav nav-list" id="sildeContent">
                    </ul>
                </div>
            </div>
            <div id="status">
                <div class="middle">
                </div>
            </div>
            <div class="span10">
                <!--Body content-->
                <div id="navtab1" style="width: 100%; overflow: hidden; border: 1px solid #A3C0E8;">
                    <div tabid="home" title="主页" lselected="true">
                        <iframe frameborder="0" name="showmessage" src="chosen/Index.htm"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div>
    </div>
    <%--    <div unselectable="on" style="background:#000;filter:alpha(opacity=10);opacity:.1;left:0px;top:0px;position:fixed;height:100%;width:100%;overflow:hidden;z-index:10000;"></div>
<div style="text-align:center;">

<div  style="MARGIN-RIGHT: auto; MARGIN-LEFT: auto;position:fixed;height:500px;width:500px;z-index:100001;background-color:White;border:solid 6px #a6c9d7;">
    <span style="float:right;line-height:20px;color:#555"><a>关闭</a></span>
    <h3>网站导航</h3>
</div></div>--%>
</body>
</html>
