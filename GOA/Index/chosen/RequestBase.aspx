<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestBase.aspx.cs" Inherits="GOA.Index.chosen.RequestBase" EnableTheming="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style>
        body
        {
        	    width:auto;
        	    height:auto;
        }
     #more
     {
        float:right;	
     }
    </style>
     <script src="../jquery-1.8.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function() {
            $("#more").bind("click", function() {
                parent.parent.addTabs("MW20", "待办事宜", "http://localhost:8888/WF/MW20.aspx?Type=1");
            });

            $(".request").bind("click", function(e) {
                var obj = $(e.target);
                parent.parent.addTabs("MW50", obj.text(), "http://localhost:8888/WF/MW50.aspx?Type=1&FormTypeID=&WorkflowID=" + obj.WorkflowID);
            });
        });       
    </script>
</head>
<body> 
    
    <p>
      <a href="#" id="more">更多</a></p>
</body>
</html>
