WorkflowID = 0;
Content_states = null;

$(function() {
    WorkflowID = GetQueryString("workflowID") || 0;

    if (WorkflowID == 0) {
        initNew(); //弹出窗口
        $("#myflow_new").click(initNew);
    }
    else {
        var json = "type=toHtml&WorkflowID=" + WorkflowID;

        $.ajax({
            url: "workFlow.ashx",
            data: json,
            async: false,
            success: function(d) {
             Content_states = eval(d);
                loadData(d);
            }
        })


    }
});

//获取url传值
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        return unescape(r[2]);
    return null;
};

function loadData(data) {
    $('#myflow').myflow({
        basePath: "",   //新建元素和路径
        restore:eval(data),
        tools: {
            save: {
                onclick: saveButton
            }
        }
    });
}

function saveButton(data) {
    if (WorkflowID == 0) {
        alert("请新建流程");
        return false;
    }

    var getData = eval("(" + data + ")");
    var re = new RegExp('"', "g");

    var flownode = "[";
    for (var i in getData.states) {
        var type = getData.states[i].type;

        var nodeText = i;
        var NodeName = getData.states[i].props.NodeName.value;
        var NodeDesc = getData.states[i].props.NodeDesc.value;
        var SignType = getData.states[i].props.SignType.value ? 1 : 0;
        var Operator = getData.states[i].props.Operator.value ? getData.states[i].props.Operator.value : "[]";
        var x = getData.states[i].attr.x || '';
        var y = getData.states[i].attr.y || '';
        var width = getData.states[i].attr.width || '';
        var height = getData.states[i].attr.height || '';

        Operator = Operator.replace(re, "'");

        switch (type) {
            case "state":  //审核
                flownode += '{"NodeID":0,"NodeName":"' + NodeName +
						                             '","NodeDesc":"' + NodeDesc + '","nodeText":"' + nodeText +
						                             '","WorkflowID":' + WorkflowID + ',"NodeTypeID":2,"DisplayOrder":9990,"IsOverTime":0,"OverTimeLen":0,"SignType":' + SignType +
						                             ',"WithdrawTypeID":10,"ArchiveFlag":0,"OperatorDetail":' + Operator + ',"x":' + x + ',"y":' + y + ',"width":' + width + ',"height":' + height + '},';
                break;
            case "task":  //处理
                flownode += '{"NodeID":0,"NodeName":"' + NodeName +
						                             '","NodeDesc":"' + NodeDesc + '","nodeText":"' + nodeText +
						                             '","WorkflowID":' + WorkflowID + ',"NodeTypeID":3,"DisplayOrder":9990,"IsOverTime":0,"OverTimeLen":0,"SignType":' + SignType +
						                              ',"WithdrawTypeID":10,"ArchiveFlag":0,"OperatorDetail":' + Operator + ',"x":' + x + ',"y":' + y + ',"width":' + width + ',"height":' + height + '},';
                break;
            case "start":  //新建
                flownode += '{"NodeID":0,"NodeName":"' + NodeName +
						                             '","NodeDesc":"' + NodeDesc + '","nodeText":"' + nodeText +
						                             '","WorkflowID":' + WorkflowID + ',"NodeTypeID":1,"DisplayOrder":9990,"IsOverTime":0,"OverTimeLen":0,"SignType":' + SignType +
						                             ',"WithdrawTypeID":10,"ArchiveFlag":0,"OperatorDetail":' + Operator + ',"x":' + x + ',"y":' + y + ',"width":' + width + ',"height":' + height + '},';
                break;
            case "end":   //归档
                flownode += '{"NodeID":0,"NodeName":"' + NodeName +
						                             '","NodeDesc":"' + NodeDesc + '","nodeText":"' + nodeText +
						                             '","WorkflowID":' + WorkflowID + ',"NodeTypeID":4,"DisplayOrder":9990,"IsOverTime":0,"OverTimeLen":0,"SignType":' + SignType +
						                              ',"WithdrawTypeID":10,"ArchiveFlag":0,"OperatorDetail":' + Operator + ',"x":' + x + ',"y":' + y + ',"width":' + width + ',"height":' + height + '},';
                break;
        }
    }

    flownode = flownode.substr(0, flownode.length - 1);
    flownode += "]";
    flownode = flownode == "]" ? "" : flownode;

    //////////////
    var nodelink = "[";
    for (var i in getData.paths) {
        var LinkName = getData.paths[i].props.LinkName.value;
        var SqlCondition = getData.paths[i].props.SqlCondition.value;

        var StartNodeName = getData.paths[i].from;
        var TargetNodeName = getData.paths[i].to;
        var NodeCondition = getData.paths[i].props.SqlCondition.condition ? getData.paths[i].props.SqlCondition.condition : "[]";

        var x = getData.paths[i].textPos.x;  //连线坐标点
        var y = getData.paths[i].textPos.y;

        SqlCondition = SqlCondition.replace(re, "'");

        nodelink += '{"LinkID":0,' +
						                          '"LinkName":"' + LinkName +
						                          '","SqlCondition":"' + SqlCondition +
                                                  '","WorkflowID":"' + WorkflowID +
						                          '","StartNodeName":"' + StartNodeName +
						                           '","NodeCondition":' + NodeCondition +  //nodecondition表
						                          ',"TargetNodeName":"' + TargetNodeName +
						                           '","x":' + x +
						                            ',"y":' + y +
						                          ' },';
    }
    nodelink = nodelink.substr(0, nodelink.length - 1);
    nodelink += "]";
    nodelink = nodelink == "]" ? "" : nodelink;

    var json = 'flownode=' + flownode + "&nodeLink=" + nodelink + "&WorkflowID=" + WorkflowID;
 //   postData(eval(json));
    postData(json);
}

function postData(data) {
    $.post("workFlow.ashx", data, function(success) {
        if (success == 0) {
            alert("保存成功");
        }
    });
}

//新建
var initNew = function() {
    $.ligerDialog.open({
        url: "newWorkFlow.htm",
        height: 300,
        width: 400,
        isResize: true,
        buttons: [{
            text: '确定',
            onclick: function(item, dialog) {
                var fn = dialog.frame.getSelectValue || dialog.frame.window.getSelectValue;
                var data = fn();
                if (!data) return false;

                WorkflowID = data.WorkflowID;
                $("#pWorkflowName input").val(data.WorkflowName);
                $("#pWorkflowDesc input").val(data.WorkflowDesc);
                $("#pFlowTypeID input").val(data.FlowTypeID);
                $("#pFormID input").val(data.FormID);
                data.IsValid == 1 ? $("#pIsValid input").attr("checked", true) : $("#pIsValid input").removeAttr("checked");
                data.IsMailNotice == 1 ? $("#pIsMailNotice input").attr("checked", true) : $("#pIsMailNotice input").removeAttr("checked");
                data.IsMsgNotice == 1 ? $("#pIsMsgNotice input").attr("checked", true) : $("#pIsMsgNotice input").removeAttr("checked");
                data.IsTransfer == 1 ? $("#pIsTransfer input").attr("checked", true) : $("#pIsTransfer input").removeAttr("checked");

                dialog.close();
            }
}]
        });

        $('#myflow').myflow({
            basePath: "",
            restore: eval("({states:{},paths:{},props:{props:{WorkflowName:{value:'fwfa'},WorkflowDesc:{value:'wjqoi'},FlowTypeID:{value:'质量相关'},FormID:{value:'基建项目立项申请表'},IsValid:{value:'true'},IsMsgNotice:{value:'true'},IsMailNotice:{value:'true'},IsTransfer:{value:'true'}}}})"),
            tools: {
                save: {
                    onclick: saveButton
                }
            }
        });


    } 