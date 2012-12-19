function getColumns() {
    return [{ display: '名称', name: 'FieldName', width: "30%", align: 'left', isSort: true },
                { display: '描述', name: 'FieldDesc', width: "20%", align: 'left', isSort: true },
                { display: '表现形式', name: 'HTMLTypeID', width: "20%", align: 'left', render: function(rowdata, rowindex, value) {
                    return rowdata.HTMLTypeN;
                }
                }
                ];
}

var g = null;

$(function() {
    initTable();

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
    g = $("#maingrid1").ligerGrid({
        title: "内置元素",
        columns: getColumns(), pageSize: 10,
        url: encodeURI("../../ajax/json.ashx?type=getFieldData&FieldTypeID=2&" + searchItems),
        rownumbers: true,
        delayLoad: false,     //初始化是是否不加载
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        checkbox: true,
        width: '90%', isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow
    });
}

function f_onCheckAllRow(checked) {
    for (var rowid in this.records) {
        var row = { "FieldID": this.records[rowid]['FieldID'], "FieldDesc": this.records[rowid]['FieldDesc'] }; 
        if (checked){
            addCheckedCustomer(row); 
        } 
        else
        {
            removeCheckedCustomer(row); 
        }  
    }
}

var checkedCustomer = [];
function findCheckedCustomer(CustomerID) {
    for (var i = 0; i < checkedCustomer.length; i++) {
        if (checkedCustomer[i] == CustomerID) return i;
    }
    return -1;
}
function addCheckedCustomer(CustomerID) {
    if (findCheckedCustomer(CustomerID) == -1)
        checkedCustomer.push(CustomerID);
}
function removeCheckedCustomer(CustomerID) {
    var i = findCheckedCustomer(CustomerID);
    if (i == -1) return;
    checkedCustomer.splice(i, 1);
}
function f_isChecked(rowdata) {
    if (findCheckedCustomer(rowdata.FieldID) == -1)
        return false;
    return true;
}
function f_onCheckRow(checked, data) {
    var row = { "FieldID": data.FieldID, "FieldDesc": data.FieldDesc }; 
    if (checked) { 
        addCheckedCustomer(row); 
    }
    else {
        removeCheckedCustomer(row); 
    }
}
function f_getChecked() {
    alert(checkedCustomer.join(','));
}

function searchItems() {
    var item = $("#search").serialize();
    checkedCustomer = [];
    initTable(item);

}

function f_select() {
    return checkedCustomer; 
}