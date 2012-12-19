(function($) {
    var myflow = $.myflow;

    //流程属性
    $.extend(true, myflow.config.props.props, {
        WorkflowName: { name: 'WorkflowName', label: '流程名称', value: '', editor: function() { return new myflow.editors.inputEditor({ readonly: true }); } },
        WorkflowDesc: { name: 'WorkflowDesc', label: '流程描述', value: '', editor: function() { return new myflow.editors.inputEditor({ readonly: true }); } },
        FlowTypeID: { name: 'FlowTypeID', label: '流程类型', value: '', editor: function() { return new myflow.editors.inputEditor({ readonly: true }); } },
        FormID: { name: 'FormID', label: '使用表单', value: '', editor: function() { return new myflow.editors.inputEditor({ readonly: true }); } },
        IsValid: { name: 'IsValid', label: '是否有效', value: '', editor: function() { return new myflow.editors.checkboxEditor({ readonly: true }); } },
        IsMsgNotice: { name: 'IsMsgNotice', label: '是否短信提醒', value: '', editor: function() { return new myflow.editors.checkboxEditor({ readonly: true }); } },
        IsMailNotice: { name: 'IsMailNotice', label: '是否邮件提醒', value: '', editor: function() { return new myflow.editors.checkboxEditor({ readonly: true }); } },
        IsTransfer: { name: 'IsTransfer', label: '是否允许转发', value: true, editor: function() { return new myflow.editors.checkboxEditor({ readonly: true }); } }
        // DisplayOrder: { name: 'DisplayOrder', label: '显示顺序', value: '10', editor: function() { return new myflow.editors.inputEditor(); } }
        //key : {name:'key', label:'标识', value:'', editor:function(){return new myflow.editors.inputEditor();}} 
    });

    //画线属性
    $.extend(true, myflow.config.path.props, {
        LinkName: { name: 'LinkName', label: '出口名称', value: '出口名称', editor: function() { return new myflow.editors.inputEditor(); } },
        SqlCondition: { name: 'SqlCondition', label: '条件', value: '(1=1)', condition: "", editor: function() { return new myflow.editors.popTextboxEditor({ condition: true, url: "" }); } }
        //    IsRejected: { name: 'IsRejected', label: '是否退回转向', value: false, editor: function() { return new myflow.editors.checkboxEditor(); } }
    });

    //工具箱控件
    $.extend(true, myflow.config.tools.states, {
        start: {
            showType: 'image',
            type: 'start',
            name: { text: '<<start>>' },
            text: { text: '开始' },
            img: { src: 'img/48/start_event_empty.png', width: 48, height: 48 },
            attr: { width: 50, heigth: 50 },
            props: {
                NodeName: { name: 'NodeName', label: '节点名称', value: '开始', editor: function() { return new myflow.editors.inputEditor(); } },
                NodeDesc: { name: 'NodeDesc', label: '节点描述', value: '开始节点', editor: function() { return new myflow.editors.inputEditor(); } },
                OverTimeLen: { name: 'OverTimeLen', label: '超时时长', value: '0', editor: function() { return new myflow.editors.inputEditor(); } },
                SignType: { name: 'SignType', label: '是否会签', value: false, editor: function() { return new myflow.editors.checkboxEditor(); } },
                Operator: { name: 'Operator', label: '操作者', value: '', editor: function() { return new myflow.editors.popTextboxEditor({ url: "operator.htm?id=" + WorkflowID }); } }
            }
        },     //开始
        end: { showType: 'image', type: 'end',
            name: { text: '<<end>>' },
            text: { text: '结束' },
            img: { src: 'img/48/end_event_terminate.png', width: 48, height: 48 },
            attr: { width: 50, heigth: 50 },
            props: {
                NodeName: { name: 'NodeName', label: '节点名称', value: '结束', editor: function() { return new myflow.editors.inputEditor(); } },
                NodeDesc: { name: 'NodeDesc', label: '节点描述', value: '结束节点', editor: function() { return new myflow.editors.inputEditor(); } },
                OverTimeLen: { name: 'OverTimeLen', label: '超时时长', value: '0', editor: function() { return new myflow.editors.inputEditor(); } },
                SignType: { name: 'SignType', label: '是否会签', value: false, editor: function() { return new myflow.editors.checkboxEditor(); } },
                Operator: { name: 'Operator', label: '操作者', value: '', editor: function() { return new myflow.editors.popTextboxEditor({ url: "operator.htm?id=" + WorkflowID }); } }
            }
        },    //结束
        'end-cancel': { showType: 'image', type: 'end-cancel',
            name: { text: '<<end-cancel>>' },
            text: { text: '取消' },
            img: { src: 'img/48/end_event_cancel.png', width: 48, height: 48 },
            attr: { width: 50, heigth: 50 },
            props: {
                text: { name: 'text', label: '显示', value: '', editor: function() { return new myflow.editors.textEditor(); }, value: '取消' },
                temp1: { name: 'temp1', label: '文本', value: '', editor: function() { return new myflow.editors.inputEditor(); } },
                temp2: { name: 'temp2', label: '选择', value: '', editor: function() { return new myflow.editors.selectEditor([{ name: 'aaa', value: 1 }, { name: 'bbb', value: 2}]); } }
            }
        },       //结束取消
        'end-error': { showType: 'image', type: 'end-error',
            name: { text: '<<end-error>>' },
            text: { text: '错误' },
            img: { src: 'img/48/end_event_error.png', width: 48, height: 48 },
            attr: { width: 50, heigth: 50 },
            props: {
                text: { name: 'text', label: '显示', value: '', editor: function() { return new myflow.editors.textEditor(); }, value: '错误' },
                temp1: { name: 'temp1', label: '文本', value: '', editor: function() { return new myflow.editors.inputEditor(); } },
                temp2: { name: 'temp2', label: '选择', value: '', editor: function() { return new myflow.editors.selectEditor([{ name: 'aaa', value: 1 }, { name: 'bbb', value: 2}]); } }
            }
        },          //结束错误
        state: {
            showType: 'text',  //显示类型
            type: 'state',     //类型
            name: { text: '<<state>>' },     //名称
            text: { text: '审核' },          //文本
            img: { src: 'img/48/task_empty.png', width: 48, height: 48 },
            props: {  //属性
                NodeName: { name: 'NodeName', label: '节点名称', value: '审核', editor: function() { return new myflow.editors.inputEditor(); } },
                NodeDesc: { name: 'NodeDesc', label: '节点描述', value: '审核节点', editor: function() { return new myflow.editors.inputEditor(); } },
                OverTimeLen: { name: 'OverTimeLen', label: '超时时长', value: '0', editor: function() { return new myflow.editors.inputEditor(); } },
                SignType: { name: 'SignType', label: '是否会签', value: false, editor: function() { return new myflow.editors.checkboxEditor(); } },
                Operator: { name: 'Operator', label: '操作者', value: '', editor: function() { return new myflow.editors.popTextboxEditor({ url: "operator.htm?id=" + WorkflowID }); } }
                // DisplayOrder: { name: 'DisplayOrder', label: '显示顺序', value: '10', editor: function() { return new myflow.editors.inputEditor(); } }
            }
        },         //状态
        fork: { showType: 'image', type: 'fork',
            name: { text: '<<fork>>' },
            text: { text: '分支' },
            img: { src: 'img/48/gateway_parallel.png', width: 48, height: 48 },
            attr: { width: 50, heigth: 50 },
            props: {
                text: { name: 'text', label: '显示', value: '', editor: function() { return new myflow.editors.textEditor(); }, value: '分支' },
                temp1: { name: 'temp1', label: '文本', value: '', editor: function() { return new myflow.editors.inputEditor(); } },
                temp2: { name: 'temp2', label: '选择', value: '', editor: function() { return new myflow.editors.selectEditor('select.json'); } }
            }
        },
        join: { showType: 'image', type: 'join',
            name: { text: '<<join>>' },
            text: { text: '合并' },
            img: { src: 'img/48/gateway_parallel.png', width: 48, height: 48 },
            attr: { width: 50, heigth: 50 },
            props: {
                text: { name: 'text', label: '显示', value: '', editor: function() { return new myflow.editors.textEditor(); }, value: '合并' },
                temp1: { name: 'temp1', label: '文本', value: '', editor: function() { return new myflow.editors.inputEditor(); } },
                temp2: { name: 'temp2', label: '选择', value: '', editor: function() { return new myflow.editors.selectEditor('select.json'); } }
            }
        },
        task: { showType: 'text', type: 'task',
            name: { text: '<<task>>' },
            text: { text: '处理' },
            img: { src: 'img/48/task_empty.png', width: 48, height: 48 },
            props: {
                  NodeName: { name: 'NodeName', label: '节点名称', value: '处理', editor: function() { return new myflow.editors.inputEditor(); } },
                NodeDesc: { name: 'NodeDesc', label: '节点描述', value: '处理节点', editor: function() { return new myflow.editors.inputEditor(); } },
                OverTimeLen: { name: 'OverTimeLen', label: '超时时长', value: '0', editor: function() { return new myflow.editors.inputEditor(); } },
                SignType: { name: 'SignType', label: '是否会签', value: false, editor: function() { return new myflow.editors.checkboxEditor(); } },
                Operator: { name: 'Operator', label: '操作者', value: '', editor: function() { return new myflow.editors.popTextboxEditor({ url: "operator.htm?id=" + WorkflowID }); } }
            }
        }
    });
})(jQuery);