function getColumns() {
    return [{ display: '名称', name: 'FieldName', width: "30%", align: 'left', isSort: true },
                { display: '描述', name: 'FieldDesc', width: "20%", align: 'left', isSort: true },
                { display: '表现形式', name: 'HTMLTypeID', width: "20%", align: 'left', render: function(rowdata, rowindex, value) {
                    return rowdata.HTMLTypeN;
                }
                },
                { display: '操作', name: 'FieldID', width: "20%", align: 'center', render: function(rowdata, rowindex, value) {
                    var h = "<a href='#' onclick='InsertHTML(" + rowindex + ")'>增加</a> ";
                    return h;
                }
                },
            ];
}

var g = null;

$(function() {
    var FormID = GetQueryString("FormID");
    if (FormID != null) {
        $.ajax({
            type: "get",
            url: "../../ajax/json.ashx",   //ajax后台处理文件
            async: true,
            data: "type=toForm&FormID="+FormID+"&t=" + (new Date()).valueOf(),      //发送json格式数据
            success: function(msg) {
                $("#editor1").val(msg);
            }
        });
    }

    initTable();  //生成表格
    CKEDITOR.replace('editor1');   //生成编辑器 

    $.ajax({
        type: "get",
        url: "../../ajax/json.ashx",   //ajax后台处理文件
        async: true,
        data: "type=getHTMLType",      //发送json格式数据
        success: function(msg) {
            var json = eval("[" + msg + "]");
            for (var i = 0; i < json[0].Rows.length; i++) {
                var varItem = new Option(json[0].Rows[i].HTMLTypeDesc, json[0].Rows[i].HTMLTypeID);
                $("#htmlType")[0].options.add(varItem);
            }
        }
    });

});

var initTable = function(searchItems) {
    log.profile("表格加载");
    g = $("#maingrid1").ligerGrid({
        title: "内置元素",
        columns: getColumns(), pageSize: 10,
        url: encodeURI("../../ajax/json.ashx?type=getFieldData&FieldTypeID=1&" + searchItems),
        rownumbers: true,
        delayLoad: false,     //初始化是是否不加载
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        width: '40%', height: '350'
    });
    log.profile("表格加载");
}

function GetHTML() {
    var oEditor = CKEDITOR.instances.editor1;
    var result = oEditor.getData();

    //result = result.replace(/</g, "&lt;").replace(/>/g, "&gt;");
    
    alert(result);
}

//获取html提取出关键词成json
function GetContents() {
    var oEditor = CKEDITOR.instances.editor1;
    var options = "{\"SelectNo\":\"0\",\"FieldID\":\"0\",\"LabelWord\":\"fff\",\"DisplayOrder\":\"10\"}";

    var html = "<div>" + oEditor.getData() + "</div>";   //编辑器html字符串
    var obj = GPRP.parseDom(html);   //成DOM对象

    var formLength = $(obj).find("form").length;
    if (formLength <= 0) {
        GPRP.warn('没有表单'); 
        return false;
    }

    var FormID = $(obj).find("form").attr("formid") ? $(obj).find("form").attr("formid") : "";
    var FormName = $(obj).find("form").attr("name") ? $(obj).find("form").attr("name") : "";
    var FormDesc = $(obj).find("form").attr("FormDesc") ? $(obj).find("form").attr("FormDesc") : "";
    var FormTypeID = $(obj).find("form").attr("FormTypeID") ? $(obj).find("form").attr("FormTypeID") : "";
    var DisplayOrder = $(obj).find("form").attr("DisplayOrder") ? $(obj).find("form").attr("DisplayOrder") : "9990";
    var Useflag = $(obj).find("form").attr("Useflag") ? 1 : 0;

    var form = "{\"FormName\":\"" + FormName + "\"," +
                             "\"FormDesc\":\"" + FormDesc + "\"," +
                             "\"FormTypeID\":\"" + FormTypeID + "\"," +
                             "\"DisplayOrder\":\"" + DisplayOrder + "\"," +
                             "\"Useflag\":\"" + Useflag + "\"," +
                               "\"FormID\":\"" + FormID + "\"" +
                             "}";

    var json = "";
    var options = "";

    $(obj).find("form").find("input,a,select,textarea,label").each(function(i, n) {   //遍历form下所有元素
        var input = $(n);

        if (input.attr("type") == "list" || input.attr("type") == "select-one") {
            if (input.attr("isdynamic") == "0") {
                input.find("option").each(function(i, n) {
                    var option = $(n);
                    var LabelWord = option.val();
                    var SelectNo = option.attr("selectno") || "0";
                    var FieldID = option.attr("fieldid") || "0";
                    var DisplayOrder = option.attr("DisplayOrder") || ((i + 1) * 10);

                    options += "{\"SelectNo\":\"" + SelectNo + "\",\"FieldID\":\"" + FieldID + "\",\"LabelWord\":\"" + LabelWord + "\",\"DisplayOrder\":\"" + DisplayOrder + "\"},";

                });
                options = options.substring(0, options.length - 1);
            }
        }

        if (input.attr("type") == "groupLine" || input.attr("type") == "file" || input.attr("type") == "label" || input.attr("type") == "list" || input.attr("type") == "select-one" || input.attr("type") == "text" || input.attr("type") == "textarea" || input.attr("type") == "checkbox" || input.attr("type") == "update" || input.attr("type") == "hidden") {          //文本类型
            var FieldID = input.attr("FieldID") ? input.attr("FieldID") : "";
            var FieldName = input.attr("FieldName") ? input.attr("FieldName") : "";
            var FieldDesc = input.attr("FieldDesc") ? input.attr("FieldDesc") : "";
            var DataTypeID = input.attr("DataTypeID") ? input.attr("DataTypeID") : "";
            var SqlDbType = input.attr("SqlDbType") ? input.attr("SqlDbType") : "";    //可以合并
            var SqlDbLength = input.attr("SqlDbLength") ? input.attr("SqlDbLength") : "";  //可以合并
            var HTMLTypeID = input.attr("HTMLTypeID") ? input.attr("HTMLTypeID") : "";
            var FieldTypeID = input.attr("FieldTypeID") ? input.attr("FieldTypeID") : "";
            var TextLength = input.attr("TextLength") ? input.attr("TextLength") : "0";
            var Dateformat = input.attr("Dateformat") ? input.attr("Dateformat") : "";
            var IsHTML = input.attr("IsHTML") ? "1" : "0";
            var TextHeight = input.attr("TextHeight") ? input.attr("TextHeight") : "0";
            var IsDynamic = input.attr("IsDynamic") ? input.attr("IsDynamic") : "0";
            var BrowseType = input.attr("BrowseType") ? input.attr("BrowseType") : "0";
            var DataSetID = input.attr("DataSetID") ? input.attr("DataSetID") : "0";
            var CSSStyleClass = input.attr("CSSStyleClass") ? input.attr("CSSStyleClass") : "";
            var GroupLineDataSetID = input.attr("GroupLineDataSetID") ? input.attr("GroupLineDataSetID") : "0";
            var group = input.attr("group") ? input.attr("group") : "";

            if (input.attr("type") == "select-one") {
                TextHeight = input.attr("size") ? input.attr("size") : "0";
            }

            if (SqlDbType.toLocaleLowerCase() == "varchar") {
                var FieldDBType = SqlDbType + "(" + SqlDbLength + ")";
            }
            else {
                var FieldDBType = SqlDbType;
            }

            json += "{\"FieldName\":\"" + FieldName
                            + "\",\"FieldDesc\":\"" + FieldDesc
                            + "\",\"DataTypeID\":\"" + DataTypeID
                            + "\",\"FieldDBType\":\"" + FieldDBType
                            + "\",\"SqlDbType\":\"" + SqlDbType
                            + "\",\"SqlDbLength\":\"" + SqlDbLength
                            + "\",\"HTMLTypeID\":\"" + HTMLTypeID
                            + "\",\"FieldTypeID\":\"" + FieldTypeID
                            + "\",\"TextLength\":\"" + TextLength
                            + "\",\"Dateformat\":\"" + Dateformat
                            + "\",\"IsHTML\":\"" + IsHTML
                            + "\",\"IsDynamic\":\"" + IsDynamic
                            + "\",\"BrowseType\":\"" + BrowseType
                            + "\",\"DataSetID\":\"" + DataSetID
                            + "\",\"TextHeight\":\"" + TextHeight
                            + "\",\"CSSStyleClass\":\"" + CSSStyleClass
                             + "\",\"GroupLineDataSetID\":\"" + GroupLineDataSetID
                            + "\",\"group\":\"" + group
                              + "\",\"FieldID\":\"" + FieldID
                            + "\"},";
        }
    });

    json = json.substring(0, json.length - 1);

    var teststr = "{\"items\":[" + json + "],\"form\":[" + form + "],\"options\":[" + options + "],\"json\":[" + JSON.stringify(GPRP.json) + "]}";
 
    GPRP.submit(teststr);
}

function InsertHTML(rowindex) {
    var oEditor = CKEDITOR.instances.editor1;
    var FieldName = g.rows[rowindex].FieldName;
    var FieldDesc = g.rows[rowindex].FieldDesc;
    var HTMLTypeID = g.rows[rowindex].HTMLTypeID;   //控件类型
    var SqlDbType = g.rows[rowindex].SqlDbType;
    var SqlDbLength = g.rows[rowindex].SqlDbLength;
    var TextLength = g.rows[rowindex].TextLength;
    var FieldTypeID = g.rows[rowindex].FieldTypeID;
    var DataTypeID = g.rows[rowindex].DataTypeID;
    var Dateformat = g.rows[rowindex].Dateformat;
    var TextHeight = g.rows[rowindex].TextHeight;
    var IsHTML = g.rows[rowindex].IsHTML;
    var BrowseType = g.rows[rowindex].BrowseType;
    var IsDynamic = g.rows[rowindex].IsDynamic;
    var DataSetID = g.rows[rowindex].DataSetID;
    var ValueColumn = g.rows[rowindex].ValueColumn;
    var TextColumn = g.rows[rowindex].TextColumn;
    var FieldID = g.rows[rowindex].FieldID;

    var value = "";
    var command = "datatypeid='" + DataTypeID + "'  fielddesc='" + FieldDesc + "' fieldtypeid='" + FieldTypeID + "' " +
         "htmltypeid='" + HTMLTypeID + "' name='" + FieldName + "' sqldblength='" + SqlDbLength + "'" +
         " sqldbtype='" + SqlDbType + "' textlength='" + TextLength + "";
         
    switch (HTMLTypeID) {
        case 1:
            value = "<label datatypeid='" + DataTypeID + "' FieldID='" + FieldID + "'  fielddesc='" + FieldDesc + "' fieldtypeid='" + FieldTypeID + "' " +
         "htmltypeid='" + HTMLTypeID + "' name='" + FieldName + "' sqldblength='" + SqlDbLength + "'" +
         " sqldbtype='" + SqlDbType + "' textlength='" + TextLength + "' type='label'>" + FieldName + "</label>";
            break;   //标签
        case 2:
            value = "<input datatypeid='" + DataTypeID + "' FieldID='" + FieldID + "' fielddesc='" + FieldDesc + "' fieldname='" + FieldName + "'  dateformat='" + Dateformat + "' " +
    " fieldtypeid='" + FieldTypeID + "' htmltypeid='" + HTMLTypeID + "' sqldblength='" + SqlDbLength + "' sqldbtype='" + SqlDbType + "' textlength='" + TextLength + "' type='text' />";
            break;
        case 3:
            value = "<textarea datatypeid='" + DataTypeID + "' FieldID='" + FieldID + "' fielddesc='" + FieldDesc + "' fieldname='" + FieldName + "' fieldtypeid='" + FieldTypeID + "' " +
            "htmltypeid='" + HTMLTypeID + "' ishtml='" + IsHTML + "' sqldblength='" + SqlDbLength + "' sqldbtype='" + SqlDbType + "' textheight='" + TextHeight + "' textlength='0'></textarea>";
            break; //多行文本
        case 4:
        case 5:
            if (IsDynamic == 1) {
                value = "<select datasetid='" + DataSetID + "' FieldID='" + FieldID + "' datatypeid='" + DataTypeID + "' size='" + TextHeight + "' fielddesc='" + FieldDesc + "' fieldname='" + FieldName + "' " +
        "fieldtypeid='" + FieldTypeID + "' htmltypeid='" + HTMLTypeID + "' isdynamic='" + IsDynamic + "'  sqldblength='" + SqlDbLength + "' " +
        "sqldbtype='" + SqlDbType + "' textcolumn='" + TextColumn + "' textlength='" + TextLength + "' type='select' valuecolumn='" + ValueColumn + "'></select>";
            }
            else {
                var result = GPRP.getAjax("type=getFieldDictSelect&FieldID=" + FieldID);
                var json = eval('(' + result + ')');


                var tmp = "<option selectNo='{0}' value='{1}'>{1}</option>";
                var option = "";
                for (var i = 0; i < json.Rows.length; i++) {
                    option += tmp.replace("{0}", json.Rows[i].selectNo).replace("{1}", json.Rows[i].LabelWord).replace("{1}", json.Rows[i].LabelWord);
                }

                value = "<select datasetid='" + DataSetID + "' FieldID='" + FieldID + "' datatypeid='" + DataTypeID + "' size='" + TextHeight + "' fielddesc='" + FieldDesc + "' fieldname='" + FieldName + "' " +
        "fieldtypeid='" + FieldTypeID + "' htmltypeid='" + HTMLTypeID + "' isdynamic='" + IsDynamic + "'  sqldblength='" + SqlDbLength + "' " +
        "sqldbtype='" + SqlDbType + "' textcolumn='" + TextColumn + "' textlength='" + TextLength + "' type='select' valuecolumn='" + ValueColumn + "'>" + option + "</select>";
            }
            break;
        case 6:
            value = "<input datatypeid='" + DataTypeID + "' FieldID='" + FieldID + "' fielddesc='" + FieldDesc + "' fieldname='" + FieldName + "' fieldtypeid='" + FieldTypeID + "' " +
            "htmltypeid='" + HTMLTypeID + "' sqldblength='" + SqlDbLength + "' sqldbtype='" + SqlDbType + "' textlength='" + TextLength + "' type='checkbox' />";
            break;   //checkbox
        case 7:
            value = "<input datatypeid='" + DataTypeID + "' FieldID='" + FieldID + "' fielddesc='" + FieldDesc + "' fieldname='" + FieldName + "' fieldtypeid='" + FieldTypeID + "' " +
            "htmltypeid='" + HTMLTypeID + "' sqldblength='" + SqlDbLength + "' sqldbtype='" + SqlDbType + "' textlength='" + TextLength + "' type='file' />";
            break;  //附件上传
        case 8:
            value = "<input browsetype='" + BrowseType + "' FieldID='" + FieldID + "' datatypeid='" + DataTypeID + "'  fielddesc='" + FieldDesc + "' fieldname='" + FieldName + "' fieldtypeid='" + FieldTypeID + "' " +
            "htmltypeid='" + HTMLTypeID + "' name='" + FieldName + "' sqldblength='" + SqlDbLength + "' sqldbtype='" + SqlDbType + "' textlength='" + TextLength + "'  type='update' />";
            break;   //浏览按钮
        case 9:
            value = "<input datatypeid='" + DataTypeID + "' FieldID='" + FieldID + "' fielddesc='" + FieldDesc + "' fieldname='" + FieldName + "' " +
            " fieldtypeid='" + FieldTypeID + "' htmltypeid='" + HTMLTypeID + "' sqldblength='" + SqlDbLength + "' sqldbtype='" + SqlDbType + "' textlength='" + TextLength + "' type='groupLine' />";
            break;  //明细行
    }

    if (oEditor.mode == 'wysiwyg') {
        oEditor.insertHtml(value);
    }
    else
        alert('You must be in WYSIWYG mode!');
}

function searchItems() {
    var item = $("#search").serialize();
    initTable(item);
}

//获取url传值
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        return unescape(r[2]);
    return null;
};