/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/
CKEDITOR.dialog.add('textfield', function(editor) {
    var fieldDataType = GPRP.getFieldDataType();
    var cssStyleClass = GPRP.getCSSStyleClass();  //获取样式

    var autoAttributes =
	{
	    value: 1,
	    size: 1,
	    maxLength: 1
	};

    return {
        title: editor.lang.textfield.title,
        minWidth: 350,
        minHeight: 250,
        onShow: function() {
            delete this.textField;

            var element = this.getParentEditor().getSelection().getSelectedElement();
            if (element && element.getAttribute('type') == "text") {
                this.textField = element;
                this.setupContent(element);
            }
            
        },
        onOk: function() {   //确定按钮事件
            var editor,
				element = this.textField,
				isInsertMode = !element;

            if (isInsertMode) {
                editor = this.getParentEditor();
                element = editor.document.createElement('input');
                element.setAttribute('type', 'text');
            }

            if (isInsertMode)
                editor.insertElement(element);
            this.commitContent({ element: element });
        },
        onLoad: function() {
            var autoSetup = function(element) {
                var value = element.hasAttribute(this.id) && element.getAttribute(this.id);
                this.setValue(value || '');
            };

            var autoCommit = function(data) {
                var element = data.element;
                var value = this.getValue();

                if (value)
                    element.setAttribute(this.id, value);
                else
                    element.removeAttribute(this.id);
            };

            this.foreach(function(contentObj) {
                if (autoAttributes[contentObj.id]) {
                    contentObj.setup = autoSetup;
                    contentObj.commit = autoCommit;
                }
            });
        },
        contents: [    //界面代码
			{
			id: 'info',
			label: editor.lang.textfield.title,
			title: editor.lang.textfield.title,
			elements: [
					{
					    id: '_cke_saved_FieldName',    //名称
					    type: 'text',
					    label: editor.lang.textfield.name,
					    validate: GPRP.validateName,   //自定义验证方法函数
					    'default': '',
					    accessKey: 'N',
					    setup: function(element) {
					        this.setValue(
											element.data('cke-saved-FieldName') ||
											element.getAttribute('FieldName') ||
											'');
					    },
					    commit: function(data) {
					        var element = data.element;

					        if (this.getValue())
					            element.data('cke-saved-FieldName', this.getValue());
					        else {
					            element.data('cke-saved-FieldName', false);
					            element.removeAttribute('FieldName');
					        }

					        element.data('cke-saved-HTMLTypeID', '2');
					        element.data('cke-saved-DataTypeID', '1');

					    }
					},
					{
					    id: 'fieldDesc',
					    type: 'text',
					    label: "描述",
					    'default': '',
					    accessKey: 'V',
					    setup: function(element) {   //双击已增加控件回传值
					        this.setValue(element.data('cke-saved-fieldDesc') ||
									element.getAttribute('fieldDesc') ||
									'');
					    },
					    commit: function(data) {   //点击确认后增加相应的属性，名称为cke-saved- XXX
					        var element = data.element;

					        if (this.getValue())
					            element.data('cke-saved-fieldDesc', this.getValue());
					        else {
					            element.data('cke-saved-fieldDesc', false);
					            element.removeAttribute('fieldDesc');
					        }
					    }
					},
			 		{
			 		    id: 'DataTypeID',    //数据类型
			 		    type: 'select',         //下拉选择框
			 		    label: "表现形式",
			 		    'default': '',
			 		    accessKey: 'M',
			 		    items: fieldDataType,
			 		    onShow: function(e) {
			 		        this.setValue("1");
			 		    },
			 		    onChange: function(e) {
			 		        var dateFormat = this.getDialog().getContentElement("info", "dateFormat");
			 		        var textLength = this.getDialog().getContentElement("info", "SqlDbLength");

			 		        var dateFormatHideID = dateFormat.domId;
			 		        var textLengthHideID = textLength.domId;
			 		        $("#" + dateFormatHideID + "").parent().hide();   //隐藏父级td元素

			 		        var selectText = e.sender.getValue();

			 		        switch (selectText) {
			 		            case "1":  //文本
			 		                $("#" + dateFormatHideID + "").hide();
			 		                $("#" + dateFormatHideID + "").parent().hide();
			 		                $("#" + textLengthHideID + "").show();
			 		                $("#" + textLengthHideID + "").parent().show();

			 		                dateFormat.clear();
			 		                break;
			 		            case "2":   //整数
			 		                $("#" + dateFormatHideID + "").hide();
			 		                $("#" + dateFormatHideID + "").parent().hide();
			 		                $("#" + textLengthHideID + "").hide();
			 		                $("#" + textLengthHideID + "").parent().hide();
			 		                textLength.setValue("");
			 		                dateFormat.clear();
			 		                break;
			 		            case "3":   //浮点
			 		                $("#" + dateFormatHideID + "").hide();
			 		                $("#" + dateFormatHideID + "").parent().hide();
			 		                $("#" + textLengthHideID + "").hide();
			 		                $("#" + textLengthHideID + "").parent().hide();
			 		                textLength.setValue("");
			 		                dateFormat.clear();
			 		                break;
			 		            case "4":   //日期
			 		                $("#" + dateFormatHideID + "").show();
			 		                $("#" + dateFormatHideID + " select").show();
			 		                $("#" + dateFormatHideID + "").parent().show();
			 		                $("#" + textLengthHideID + "").hide();
			 		                $("#" + textLengthHideID + "").parent().hide();
			 		                textLength.setValue("");
			 		                var json = getDateType("1");  // 1是日期；2是时间
			 		                dateFormat.clear();
			 		                for (var i = 0; i < json.length; i++) {
			 		                    dateFormat.add(json[i][0]);
			 		                }
			 		                break;
			 		            case "5":     //时间
			 		                $("#" + dateFormatHideID + "").show();
			 		                $("#" + dateFormatHideID + " select").show();
			 		                $("#" + dateFormatHideID + "").parent().show();
			 		                $("#" + textLengthHideID + "").hide();
			 		                $("#" + textLengthHideID + "").parent().hide();
			 		                textLength.setValue("");
			 		                var json = getDateType("2");  // 1是日期；2是时间
			 		                dateFormat.clear();
			 		                for (var i = 0; i < json.length; i++) {
			 		                    dateFormat.add(json[i][0]);
			 		                }
			 		                break;
			 		        }

			 		    },
			 		    setup: function(element) {   //双击已增加控件回传值
			 		        this.setValue(element.data('cke-saved-DataTypeID') ||
								element.getAttribute('DataTypeID') ||
									'');
			 		    },
			 		    commit: function(data) {   //点击确认后增加相应的属性，名称为cke-saved- XXX
			 		        var element = data.element;

			 		        if (this.getValue())
			 		            element.data('cke-saved-DataTypeID', this.getValue());
			 		        else {
			 		            element.data('cke-saved-DataTypeID', false);
			 		            element.removeAttribute('DataTypeID');
			 		        }

			 		        var datatype = this.getValue();
			 		        element.data('cke-saved-FieldTypeID', '1');
			 		        switch (datatype) {
			 		            case "1":
			 		                element.data('cke-saved-SqlDbType', 'VarChar');
			 		                break;
			 		            case "2":
			 		                element.data('cke-saved-SqlDbType', 'BigInt');
			 		                element.data('cke-saved-SqlDbLength', '4');
			 		                break;
			 		            case "3":
			 		                element.data('cke-saved-SqlDbType', 'Float');
			 		                element.data('cke-saved-SqlDbLength', '8');
			 		                break;
			 		            case "4":
			 		                element.data('cke-saved-SqlDbType', 'DateTime');
			 		                element.data('cke-saved-SqlDbLength', '16');
			 		                break;
			 		            case "5":
			 		                element.data('cke-saved-SqlDbType', 'VarChar');
			 		                element.data('cke-saved-SqlDbLength', '8');
			 		                break;
			 		        }

			 		    }
			 		},
			 	    {
			 	        type: 'hbox',
			 	        widths: ['50%', '50%'],
			 	        children: [
                        {
                            id: 'SqlDbLength',
                            type: 'text',
                            style: 'display:none;width:200px',
                            label: '文本长度',
                            validate: validateLength,
                            //validate: CKEDITOR.dialog.validate.notEmpty("只能为整数"),
                            'default': '10',
                            accessKey: 'V',
                            setup: function(element) {
                                this.setValue(
											element.data('cke-saved-SqlDbLength') ||
											element.getAttribute('SqlDbLength') ||
											'');
                            },
                            commit: function(data) {
                                var element = data.element;

                                if (this.getValue()) {
                                    element.data('cke-saved-TextLength', this.getValue());
                                    element.data('cke-saved-SqlDbLength', this.getValue());
                                }
                                else {
                                    element.data('cke-saved-TextLength', '0');
                                }

                                //                                else {
                                //                                    element.data('cke-saved-SqlDbLength', false);
                                //                                    element.removeAttribute('SqlDbLength');
                                //                                }


                            }
                        },
                        {
                            id: 'dateFormat',
                            type: 'select',
                            label: '日期类型',
                            style: 'display:none',
                            'default': '',
                            accessKey: 'V',
                            items: [],
                            setup: function(element) {
                                this.setValue(
											element.data('cke-saved-dateFormat') ||
											element.getAttribute('dateFormat') ||
											'');
                            },
                            commit: function(data) {
                                var element = data.element;

                                if (this.getValue())
                                    element.data('cke-saved-dateFormat', this.getValue());
                                else {
                                    element.data('cke-saved-dateFormat', false);
                                    element.removeAttribute('dateFormat');
                                }
                            }
                        }
                        ]
			 	    },
                    {
                        type: 'select',
                        id: 'cssStyleClass',
                        label: "样式",
                        items: cssStyleClass,
                        required: true,
                        setup: function(element) {   //双击已增加控件回传值
                            this.setValue(element.data('cke-saved-cssStyleClass') ||
								element.getAttribute('cssStyleClass') ||
								'');
                        },
                        commit: function(data) {   //点击确认后增加相应的属性，名称为cke-saved- XXX
                            var element = data.element;

                            if (this.getValue())
                                element.data('cke-saved-cssStyleClass', this.getValue());
                            else {
                                element.data('cke-saved-cssStyleClass', false);
                                element.removeAttribute('cssStyleClass');
                            }
                        }
                    }
				]}
		]


    };
});


function getDateType(type) { 
    var data = "type=getDateFormat&DateType=" + type;
    var fieldDataType= GPRP.getAjax(data);

    return fieldDataType = eval('(' + fieldDataType + ')');
}

function validateLength() {
    var DataTypeID = this.getDialog().getContentElement("info", "DataTypeID");

    if (DataTypeID.getValue() == "1") {
        if (this.getValue() == "") {
            alert("长度不能为空");
            return false;
        }
        else {
            var patten = new RegExp(/^\d*$/);  //整数
            var r = patten.test(this.getValue());
            if (!r)
                alert("长度只能为整数");
            return r;
        }

    }
    else {
        return true;
    }
}