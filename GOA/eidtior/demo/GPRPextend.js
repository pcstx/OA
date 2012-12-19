(function($) {
    $.extend({ GPRP: {} });

    $.extend($.GPRP, {
        'warn': function(msg) {
            $.ligerDialog.warn(msg);
        },
        'success': function(msg) {
            $.ligerDialog.success(msg);
        },
        'postAjax': postAjax,
        'getAjax': getAjax
    });

    var postAjax = function(data, asyncType) {
        var result;
        $.ajax({
            type: "POST",
            url: "../../ajax/json.ashx",   //ajax后台处理文件
            async: asyncType ? true : false, //是否异步操作
            data: data,      //发送json格式数据
            success: function(msg) {
                result = msg;
            }
        });
        return result;
    }

    var getAjax = function(data, asyncType) {
        var result;
        $.ajax({
            type: "get",
            url: "../../ajax/json.ashx",   //ajax后台处理文件
            async: asyncType ? true : false,
            data: data,      //发送json格式数据
            success: function(msg) {
                result = msg;
            }
        });
        return result;
    }

    
})(jQuery)
 
