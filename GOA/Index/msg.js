/// <reference path="jquery-1.3.2.min.js" >
$(document).ready(function() {
    function send(username) {
        //向comet_broadcast.asyn发送请求，消息体为文本框content中的内容，请求接收类为AsnyHandler
        var c = "aaa";
        var u=username||$("#btText").val();

        $.post("comet_broadcast.asyn", { content: c, username:u  });

        //清空内容
        //  $("#content").val("");
    }

    function wait() {
        $.post("comet_broadcast.asyn", { content: "-1" },
         function(data, status) {
             //var result = $("#divResult");
             // result.html(result.html() + "<br/>" + data);

             if (data != "false#@!") {
                 f_tip(data);
             }

             //服务器返回消息,再次立连接
             wait();
         }, "html"
         );
    }

    //初始化连接
    wait();

    $("#btsend").click(function() { send(); });

});