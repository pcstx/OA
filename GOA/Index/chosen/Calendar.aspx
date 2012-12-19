<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="GOA.Index.chosen.Calendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
            parent.parent.addTabs("MC10", "个人日历", "../MyCalendar/Calendar.aspx");
            }); 
        });       
    </script>
</head>
<body>
     <p>
      <a href="#" id="more">更多</a></p>
</body>
</html>
