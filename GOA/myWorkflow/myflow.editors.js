(function($) {
    var myflow = $.myflow;

    // 
    $.extend(true, myflow.editors, {
        inputEditor: function(arg) {
            var _props, _k, _div, _src, _r;
            this.init = function(props, k, div, src, r) {
                _props = props; _k = k; _div = div; _src = src; _r = r;
                var input = '<input style="width:100%;"/>';
                if (arg != null && arg.readonly) {
                    input = '<input style="width:100%;" readonly />';
                }

                $(input).val(props[_k].value).change(function() {
                    props[_k].value = $(this).val();
                }).appendTo('#' + _div);

                $('#' + _div).data('editor', this);
            }
            this.destroy = function() {
                $('#' + _div + ' input').each(function() {
                    _props[_k].value = $(this).val();
                });
            }
        },
        checkboxEditor: function(arg) {
            var _props, _k, _div, _src, _r;
            this.init = function(props, k, div, src, r) {
                _props = props; _k = k; _div = div; _src = src; _r = r;

                var input = '<input type="checkbox" style="width:100%;"/>';
                if (arg != null && arg.readonly) {
                    input = '<input type="checkbox" style="width:100%;" disabled="disabled" />';
                }

                var chk = $(input).attr("checked", props[_k].value).click(function(e) {
                    props[_k].value = $(this).attr("checked");
                    e.stopPropagation();  //停止事件冒泡
                }).appendTo('#' + _div);

                $('#' + _div).data('editor', this);
            }
            this.destroy = function() {
                $('#' + _div + ' input').each(function() {
                    _props[_k].value = $(this).attr("checked");
                });
            }
        },
        popTextboxEditor: function(arg) {
            var _props, _k, _div, _src, _r;
            this.init = function(props, k, div, src, r) {
                _props = props; _k = k; _div = div; _src = src; _r = r;

                var btn = $('<input style="width:80%;" />').val(props[_k].value);
                btn.appendTo('#' + _div);

                $('<a class="chose" href="#">chose</a>').click(function(e) {

                    $.ligerDialog.open({
                        url: arg.url || "nodeCondition.aspx?id=" + WorkflowID,
                        height: arg.height || 600,
                        width: arg.width || 800,
                        isResize: true,
                        buttons: [{
                            text: '确定',
                            onclick: function(item, dialog) {
                                var fn = dialog.frame.getSelectValue || dialog.frame.window.getSelectValue;
                                var data = fn();

//                                props[_k].value = data.sqlCondition;
//                                btn.val(data.sqlCondition);

                               if (typeof (data) == "string") {
                                    props[_k].value = data;
                                    btn.val(data);
                                }
                                else {
                                    props[_k].value = data.sqlCondition;
                                    btn.val(data.sqlCondition);

                                    if (arg.condition) {
                                        props[_k].condition = data.condition;
                                    }
                                } 
                                dialog.close();
                            }
                        },
                    {
                        text: '取消',
                        onclick: function(item, dialog) {
                            dialog.close();
                        }
}]
                    });
                }).appendTo(btn).appendTo('#' + _div);


                $('#' + _div).data('editor', this);
            }
            this.destroy = function() {
                $('#' + _div + ' input').each(function() {
                    _props[_k].value = $(this).val();
                });
            }
        },
        selectEditor: function(arg) {
            var _props, _k, _div, _src, _r;
            this.init = function(props, k, div, src, r) {
                _props = props; _k = k; _div = div; _src = src; _r = r;

                var sle = $('<select  style="width:100%;"/>').val(props[_k].value).change(function() {
                    props[_k].value = $(this).val();
                }).appendTo('#' + _div);

                if (typeof arg === 'string') {
                    $.ajax({
                        type: "GET",
                        url: arg,
                        success: function(data) {
                            var opts = eval(data);
                            if (opts && opts.length) {
                                for (var idx = 0; idx < opts.length; idx++) {
                                    sle.append('<option value="' + opts[idx].value + '">' + opts[idx].name + '</option>');
                                }
                                sle.val(_props[_k].value);
                            }
                            sle.focus();
                        }
                    });
                } else {
                    for (var idx = 0; idx < arg.length; idx++) {
                        sle.append('<option value="' + arg[idx].value + '">' + arg[idx].name + '</option>');
                    }
                    sle.val(_props[_k].value);
                }

                $('#' + _div).data('editor', this);
            };
            this.destroy = function() {
                $('#' + _div + ' input').each(function() {
                    _props[_k].value = $(this).val();
                });
            };
        }
    });
})(jQuery);