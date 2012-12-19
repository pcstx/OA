<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewMaintain.aspx.cs" Inherits="GOA.Index.chosen.NewMaintain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
     <style>
     #more
     {
        float:right;	
     } 
    </style>
    <script src="../jquery-1.8.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function() {
            $("#more").bind("click", function() {
                parent.parent.addTabs("ZB40", "信息维护", "../Basic/NewMaintain.aspx");
            });

//            $(".request").bind("click", function(e) {
//                var obj = $(e.target);
//                var rid = obj.attr("NewsID");
//                parent.parent.addTabs("MA10_" + rid, obj.text(), "../Basic/ShowMailList.aspx?action=recv&MailID=" + rid);
//            });
        });       
    </script>
</head>
<body>
     <p>
      <a href="#" id="more">更多</a></p>
</body>
</html>
