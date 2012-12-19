$(function() {
    CKEDITOR.replace('editor1');   //生成编辑器
});

//转换成dom对象
function parseDom(arg) {
    var objE = document.createElement("div");
    objE.innerHTML = arg;
    return objE.childNodes;
};

//获取html提取出关键词成json
function GetContents() {
    var oEditor = CKEDITOR.instances.editor1;
    var json = "{\"items\":[";

    var html = "<div>" + oEditor.getData() + "</div>";   //编辑器html字符串
    var obj = parseDom(html);   //成DOM对象

    $(obj).find("form").attr("FormDesc");


    $(obj).find("form").children().each(function(i, n) {   //遍历form下所有元素
        var input = $(n);
        if (input.attr("type") == "text") {          //文本类型
            var FieldName = input.attr("FieldName");
            var FieldDesc = input.attr("FieldDesc");
            var DataTypeID = input.attr("DataTypeID");
            var FieldDBType = input.attr("FieldDBType");
            var SqlDbType = input.attr("SqlDbType");    //可以合并
            var SqlDbLength = input.attr("SqlDbLength");  //可以合并
            var HTMLTypeID = input.attr("HTMLTypeID");
            var FieldTypeID = input.attr("FieldTypeID");
            var TextLength = input.attr("TextLength");
            var BuiltInFlag = input.attr("BuiltInFlag");

            json += "{\"FieldName\":\"" + FieldName
                            + "\",\"FieldDesc\":\"" + FieldDesc
                            + "\",\"DataTypeID\":\"" + DataTypeID
                            + "\",\"FieldDBType\":\"" + FieldDBType
                            + "\",\"SqlDbType\":\"" + SqlDbType
                            + "\",\"SqlDbLength\":\"" + SqlDbLength
                            + "\",\"HTMLTypeID\":\"" + HTMLTypeID
                            + "\",\"FieldTypeID\":\"" + FieldTypeID
                            + "\",\"TextLength\":\"" + TextLength
                            + "\",\"BuiltInFlag\":\"" + BuiltInFlag
                            + "\"},";
        }
    });

    json = json.substring(0, json.length - 1);
    json += "]}";
    postData(json);
}

//提交数据
function postData(data) {
        $.ajax({
            type: "POST",
            url: "../../ajax/json.ashx",   //ajax后台处理文件
            data: "jsonstr=" + data,      //发送json格式数据
            success: function(msg) {
                alert(msg);
            }
        });
}