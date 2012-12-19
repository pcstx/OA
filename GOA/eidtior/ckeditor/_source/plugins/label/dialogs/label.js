(function() {
var exampleDialog = function(editor) {
var cssStyleClass = GPRP.getCSSStyleClass();  //获取样式

        return {
            title: /* title in string*/"标签",   //标题
            minWidth: /*number of pixels*/350,   //最小宽度
            minHeight: /*number of pixels*/200,   //最小高度
            buttons: /*array of button definitions*/[CKEDITOR.dialog.okButton, CKEDITOR.dialog.cancelButton],    //按钮
            onOk: /*function*/
                function() {
                    var editor = this.getParentEditor(),
		            element = this.label,
		            isInsertMode = !element;

                    editor = this.getParentEditor(); //获取父级编辑器对象

                    if (isInsertMode) {  //判断是否可以新增元素状态
                        element = editor.document.createElement('label');  //创建元素
                        element.setAttribute('type', 'label'); 
                        editor.insertElement(element); 
                    }

                    this.commitContent({ element: element });  //增加窗体内元素的属性 
                },        //点击确认按钮
            onLoad: /*function*/function() { },      //第一次打开时触发
            onShow: function() {
                delete this.label;
                var editor = this.getParentEditor(),
				selection = editor.getSelection(),
				element = selection.getSelectedElement();

                if (element && element.data('cke-real-element-type') && element.data('cke-real-element-type') == 'label') {
                    this.label = element;
                    element = editor.restoreRealElement(this.label);
                    this.setupContent(element);
                    selection.selectElement(this.label);
                } 
            },     //每次打开时都触发
            onHide: /*function*/function() { },          //OK和cancel 隐藏时触发
            onCancel: /*function*/function() { },   //点击取消按钮 
            //  resizable:[CKEDITOR.DIALOG_RESIZE_HEIGHT,CKEDITOR.DIALOG_RESIZE_BOTH. Default is] ,     //是否可改变大小
            contents: /*content definition, basically the UI of the dialog*/         //界面布局
            [
            {
                id: 'page1',  /* not CSS ID attribute! */
                accessKey: 'P',
                elements: [
                        {
                            type: 'text',
                            id: 'FieldName',
                            label: editor.lang.textfield.name,
                            validate: GPRP.validateName,  //验证名称
                            setup: function(element) {
                                this.setValue(
											element.data('cke-saved-FieldName') ||
											element.getAttribute('FieldName') ||
											'');
                            },
                            commit: function(data) {
                                var element = data.element;

                                if (this.getValue()) {
                                    element.data('cke-saved-FieldName', this.getValue());
                                   // element.setText(this.getValue());
                                }
                                else {
                                    element.data('cke-saved-FieldName', false);
                                    element.removeAttribute('FieldName');
                                }

                                element.data('cke-saved-HTMLTypeID', '1');
                                element.data('cke-saved-DataTypeID', '0');
                                element.data('cke-saved-FieldTypeID', '1');
                                element.data('cke-saved-sqldbtype', 'VarChar');
                                element.data('cke-saved-SqlDbLength', '200');
                                element.data('cke-saved-TextLength', '0');

                            }
                        },
		                {
		                    type: 'text',
		                    id: 'fieldDesc',
		                    label: "描述", 
		                    setup: function(element) {   //双击已增加控件回传值
		                        this.setValue(element.data('cke-saved-fieldDesc') ||
									element.getAttribute('fieldDesc') ||
									'');
		                    },
		                    commit: function(data) {   //点击确认后增加相应的属性，名称为cke-saved- XXX
		                        var element = data.element;

		                        if (this.getValue())  {
		                            element.data('cke-saved-fieldDesc', this.getValue());
		                            element.setText(this.getValue());
		                        } 
		                        else {
		                            element.data('cke-saved-fieldDesc', false);
		                            element.removeAttribute('fieldDesc');
		                        }
		                    }

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
                ]
            }
            ]
        }
    }

    //增加对话框
    CKEDITOR.dialog.add('label', function(editor) {
        return exampleDialog(editor);
    });
})();
