GPRP = {};
 
GPRP.warn = function(msg) {
    $.ligerDialog.warn(msg);
}

GPRP.success = function(msg) {
    $.ligerDialog.success(msg);
}

GPRP.postAjax = function(data, asyncType) {
    var result;
    $.ajax({
        type: "POST",
        url: "../../ajax/json.ashx",   //ajax后台处理文件
        async: asyncType ? true:false, //是否异步操作
        data: data,      //发送json格式数据
        success: function(msg) {
            result = msg;
        }
    });
    return result;
}

GPRP.getAjax = function(data, asyncType) {
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

//转换成dom对象
GPRP.parseDom = function(arg) {
    var objE = document.createElement("div");
    objE.innerHTML = arg;
    return objE.childNodes;
};
 
GPRP.getCSSStyleClass = function() {
    var cssStyleClass = GPRP.getAjax("type=getCSSStyleClass");
    return cssStyleClass = eval('(' + cssStyleClass + ')'); 
}

GPRP.validateName = function() {
    var patten = new RegExp(/^[a-zA-Z][a-zA-Z0-9_]{1,30}$/);
    var FieldName = this.getValue();
    var b = patten.test(FieldName);

    if (!b) {
        GPRP.warn("只能输入英文字母开头的30位字符，仅包含英文字母、数字和下划线");
        return false;
    }

    var formid = GetQueryString("FormID");

    if (formid == null) {
        var data = "type=ValidateFieldName&FieldName=" + FieldName;
        var msg = GPRP.postAjax(data);
        if (msg == "1") {
            GPRP.warn("名称已存在！");
            return false;
        }
        else
            return true;
    }
    else {
        return true;
    } 
}
 
GPRP.validateGroup = function(groupName,index) { 
    if (groupName == "" || groupName == null) {
        GPRP.warn("不能为空");
        return false;
    }

    for (var i = 0; i < GPRP.json.items.length; i++) {
        if (i != index && GPRP.json.items[i].groupName == groupName) {
            GPRP.warn("名称已存在！");
            return false; 
        }
    }

    var data = "type=ValidateFormFieldGroupName&GroupName=" + groupName;
    var msg = GPRP.postAjax(data);
    if (msg == "1") {
        GPRP.warn("名称已存在！");
        return false;
    }
    else
        return true;
}
 
GPRP.getFieldDataType = function() {
    var result = GPRP.getAjax("type=getFieldDataType"); 
    return eval('(' + result + ')');  
}

GPRP.getBrowseType = function() {
    var browseType = GPRP.getAjax("type=getBrowseType");
    return eval('(' + browseType + ')'); 
}

GPRP.getDetailFieldGroup = function() {
    var detailFieldGroup = GPRP.getAjax("type=getDetailFieldGroup");
    return eval('(' + detailFieldGroup + ')');
}

GPRP.getFormType = function() {
    var formType = GPRP.getAjax("type=getFormType");
    GPRP.warn(formType);
}

GPRP.submit = function(data) {
    var submit = GPRP.postAjax("type=submit&jsonstr=" + data);
    if (submit > 0) {
        GPRP.success("添加成功！");
        SetContents("");
    }
    //    GPRP.warn(submit);
}
 
function SetContents(data) {
    var oEditor = CKEDITOR.instances.editor1;
    oEditor.setData(data);
}

GPRP.json = { "items": [] };

GPRP.addOption =function (combo, optionText, optionValue, documentObject, index) {
    combo = getSelect(combo);
    var oOption;
    if (documentObject)
        oOption = documentObject.createElement("OPTION");
    else
        oOption = document.createElement("OPTION");

    if (combo && oOption && oOption.getName() == 'option') {
        if (CKEDITOR.env.ie) {
            if (!isNaN(parseInt(index, 10)))
                combo.$.options.add(oOption.$, index);
            else
                combo.$.options.add(oOption.$);

            oOption.$.innerHTML = optionText.length > 0 ? optionText : '';
            oOption.$.value = optionValue;
        }
        else {
            if (index !== null && index < combo.getChildCount())
                combo.getChild(index < 0 ? 0 : index).insertBeforeMe(oOption);
            else
                combo.append(oOption);

            oOption.setText(optionText.length > 0 ? optionText : '');
            oOption.setValue(optionValue);
        }
    }
    else
        return false;

    return oOption;
}

function changeOptionPosition(combo, steps, documentObject) {
    combo = getSelect(combo);
    var iActualIndex = getSelectedIndex(combo);
    if (iActualIndex < 0)
        return false;

    var iFinalIndex = iActualIndex + steps;
    iFinalIndex = (iFinalIndex < 0) ? 0 : iFinalIndex;
    iFinalIndex = (iFinalIndex >= combo.getChildCount()) ? combo.getChildCount() - 1 : iFinalIndex;

    if (iActualIndex == iFinalIndex)
        return false;

    var oOption = combo.getChild(iActualIndex),
			sText = oOption.getText(),
			sValue = oOption.getValue();

    oOption.remove();

    oOption = addOption(combo, sText, sValue, (!documentObject) ? null : documentObject, iFinalIndex);
    setSelectedIndex(combo, iFinalIndex);
    return oOption;
}
function getSelectedIndex(combo) {
    combo = getSelect(combo);
    return combo ? combo.$.selectedIndex : -1;
}
function setSelectedIndex(combo, index) {
    combo = getSelect(combo);
    if (index < 0)
        return null;
    var count = combo.getChildren().count();
    combo.$.selectedIndex = (index >= count) ? (count - 1) : index;
    return combo;
}
function getOptions(combo) {
    combo = getSelect(combo);
    return combo ? combo.getChildren() : false;
}
function getSelect(obj) {
    if (obj && obj.domId && obj.getInputElement().$)				// Dialog element.
        return obj.getInputElement();
    else if (obj && obj.$)
        return obj;
    return false;
}


function st(selectText) {
    var state = this.getDialog().getContentElement("info", "IsDynamic");
    var dym = this.getDialog().getContentElement("info", "dym");
    var jintai = this.getDialog().getContentElement("info", "jintai");
    //var selectText = state.getValue();
    var jintaiHideID = jintai.domId;
    var dymHideID = dym.domId;

    switch (selectText) {
        case "0":
            $("#" + dymHideID + "").hide();
            $("#" + jintaiHideID + "").show();
            break;
        case "1":
            $("#" + jintaiHideID + "").hide();
            $("#" + dymHideID + "").show();
            break;
    }
}

//获取url传值
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        return unescape(r[2]);
    return null;
};
