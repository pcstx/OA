/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/
CKEDITOR.dialog.add('form', function(editor) {
    var formItems;

    $.ajax({
        type: "get",
        url: "../../../../../../ajax/json.ashx",   //ajax后台处理文件
        async: false,
        data: "type=getFormType",      //发送json格式数据
        success: function(msg) {
            formItems = msg;
        }
    });

    var formItems = eval('(' + formItems + ')');


    var autoAttributes =
	{
	    action: 1,
	    id: 1,
	    method: 1,
	    enctype: 1,
	    target: 1
	};

    return {
        title: editor.lang.form.title,
        minWidth: 350,
        minHeight: 200,
        onShow: function() {
            delete this.form;

            var element = this.getParentEditor().getSelection().getStartElement();
            var form = element && element.getAscendant('form', true);
            if (form) {
                this.form = form;
                this.setupContent(form);
            }
        },
        onOk: function() {
            var editor,
				element = this.form,
				isInsertMode = !element;

            if (isInsertMode) {
                editor = this.getParentEditor();
                element = editor.document.createElement('form');
                !CKEDITOR.env.ie && element.append(editor.document.createElement('br'));
            }

            if (isInsertMode)
                editor.insertElement(element);
            this.commitContent(element);
        },
        onLoad: function() {
            function autoSetup(element) {
                this.setValue(element.getAttribute(this.id) || '');
            }

            function autoCommit(element) {
                if (this.getValue())
                    element.setAttribute(this.id, this.getValue());
                else
                    element.removeAttribute(this.id);
            }

            this.foreach(function(contentObj) {
                if (autoAttributes[contentObj.id]) {
                    contentObj.setup = autoSetup;
                    contentObj.commit = autoCommit;
                }
            });
        },
        contents: [
			{
			    id: 'info',
			    label: editor.lang.form.title,
			    title: editor.lang.form.title,
			    elements: [
					{
					    id: 'txtName',
					    type: 'text',
					    label: editor.lang.common.name,
					    'default': '',
					     validate: validateName,   //自定义验证方法函数 
					    accessKey: 'N',
					    setup: function(element) {
					        this.setValue(element.data('cke-saved-name') ||
									element.getAttribute('name') ||
									'');
					    },
					    commit: function(element) {
					        if (this.getValue())
					            element.data('cke-saved-name', this.getValue());
					        else {
					            element.data('cke-saved-name', false);
					            element.removeAttribute('name');
					        }
					    }
					},
					{
					    id: 'formDesc',
					    type: 'text',
					    label: "描述",
					    'default': '',
					    accessKey: 'T',
					    setup: function(element) {   //双击已增加控件回传值
					        this.setValue(element.data('cke-saved-formDesc') ||
									element.getAttribute('formDesc') ||
									'');
					    },
					    commit: function(element) {   //点击确认后增加相应的属性，名称为cke-saved- XXX
					        if (this.getValue())
					            element.data('cke-saved-formDesc', this.getValue());
					        else {
					            element.data('cke-saved-formDesc', false);
					            element.removeAttribute('formDesc');
					        }
					    }
					},
					{
					    type: 'hbox',
					    widths: ['45%', '55%'],
					    children:
						[
							{
							    id: 'formTypeID',
							    type: 'select',
							    label: "表单分类",
							    'default': '',
							    accessKey: 'I',
							    items: formItems,  //ajax获取的值
							    onShow: function() {
							        this.setValue(this.items[0][1]);
							    },
							    setup: function(element) {   //双击已增加控件回传值
							        this.setValue(element.data('cke-saved-formTypeID') ||
									element.getAttribute('formTypeID') ||
									'');
							    },
							    commit: function(element) {   //点击确认后增加相应的属性，名称为cke-saved- XXX
							        if (this.getValue())
							            element.data('cke-saved-formTypeID', this.getValue());
							        else {
							            element.data('cke-saved-formTypeID', false);
							            element.removeAttribute('formTypeID');
							        }
							    }
							},
							{
							    id: 'displayOrder',
							    type: 'text',
							    label: "显示顺序",
							    validate: CKEDITOR.dialog.validate.integer(editor.lang.common.validateNumberFailed),
							    accessKey: 'E',
							    'default': '9990',
							    setup: function(element) {   //双击已增加控件回传值
							        this.setValue(element.data('cke-saved-displayOrder') ||
									element.getAttribute('displayOrder') ||
									'');
							    },
							    commit: function(element) {   //点击确认后增加相应的属性，名称为cke-saved- XXX
							        if (this.getValue())
							            element.data('cke-saved-displayOrder', this.getValue());
							        else {
							            element.data('cke-saved-displayOrder', false);
							            element.removeAttribute('displayOrder');
							        }
							    }
							}
						]
					},
                    {
                        id: 'Useflag',
                        type: 'checkbox',
                        label: "是否显示",
                        'default': 'true',
                        accessKey: 'T',
                        setup: function(element) {   //双击已增加控件回传值
                            this.setValue(element.data('cke-saved-Useflag') ||
									element.getAttribute('Useflag') ||
									'');
                        },
                        commit: function(element) {   //点击确认后增加相应的属性，名称为cke-saved- XXX
                            if (this.getValue())
                                element.data('cke-saved-Useflag', this.getValue());
                            else {
                                element.data('cke-saved-Useflag', false);
                                element.removeAttribute('Useflag');
                            }
                        }
                    }
				]
			}
		]
    };


    function getFormType() {
        $.ajax({
            type: "get",
            url: "../../../../../../ajax/json.ashx",   //ajax后台处理文件
            async: false,
            data: "type=getFormType",      //发送json格式数据
            success: function(msg) {
                return msg;
            }
        });
    }

});

function validateName(e) {
    var b = false;
    var FormName = this.getValue();
    if (FormName == "") {
        alert("名称不能为空");
        return false;
    }
    else {
        var formid = GetQueryString("FormID");
        if (formid == null) {
            $.ajax({
                type: "POST",
                url: "../../ajax/json.ashx",   //ajax后台处理文件
                async: false,
                data: "type=ValidateFormName&FormName=" + FormName,      //发送json格式数据
                success: function(msg) {
                    if (msg == "1") {
                        alert("名称已存在！");
                        b = false;
                        return false;
                    }
                    else
                        b = true;
                    return true;
                }
            });
        }
        else {
            return true;
        }
    }
    
   

    return b;
}