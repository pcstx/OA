(function() {
    var exampleDialog = function(editor) {
        var cssStyleClass = GPRP.getCSSStyleClass();  //获取样式

        return {
        title: /* title in string*/"上传附件",   //标题
            minWidth: /*number of pixels*/350,   //最小宽度
            minHeight: /*number of pixels*/200,   //最小高度
            buttons: /*array of button definitions*/[CKEDITOR.dialog.okButton, CKEDITOR.dialog.cancelButton],    //按钮
            onOk: /*function*/
                function() {
                    var editor = this.getParentEditor(),
		            element = this.browsebutton,
		            isInsertMode = !element;

                    editor = this.getParentEditor(); //获取父级编辑器对象

                    if (isInsertMode) {  //判断是否可以新增元素状态
                        element = editor.document.createElement('input');  //创建元素
                        element.setAttribute('type', 'file');

                        //      element.setAttribute('id', '1');      //设置增加属性

                        //     label = editor.document.createElement('label');
                        //     label.setText("aa")
                        editor.insertElement(element);
                        //     editor.insertElement(element);
                        //editor.insertElement(element); //插入元素


                    }

                    this.commitContent({ element: element });  //增加窗体内元素的属性
                    /*
                    var fakeElement = editor.createFakeElement(element, 'cke_hidden', 'label');
                    //生成伪装元素。参数1：真的元素；参数2：css类；参数3：元素类型；参数4：是否可缩放
                    if (!this.label)  //判断元素的类型
                    editor.insertElement(fakeElement);  //将创建的元素插入到编辑器中
                    else {
                    fakeElement.replace(this.label);
                    editor.getSelection().selectElement(fakeElement);  //选中元素
                    }
                    return true;
                    */
                },        //点击确认按钮
            onLoad: /*function*/function() { },      //第一次打开时触发
            onShow: function() {
            delete this.browsebutton;
                var editor = this.getParentEditor(),
				selection = editor.getSelection(),
				element = selection.getSelectedElement();

                if (element && element.getAttribute('type') == "file") {
                   this.browsebutton = element;
                 //   element = editor.restoreRealElement(this.browsebutton);
                    this.setupContent(element);
                   // selection.selectElement(this.browsebutton);
                }

                /*
                delete this.button;
                var element = this.getParentEditor().getSelection().getSelectedElement();
                if (element && element.is('input')) {
                var type = element.getAttribute('type');
                if (type in { button: 1, reset: 1, submit: 1 }) {
                this.button = element;
                this.setupContent(element);
                }
                }
                */
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
											element.data('cke-saved-fieldname') ||
											element.getAttribute('fieldname') ||
											'');
                            },
                            commit: function(data) {
                                var element = data.element;

                                if (this.getValue()) {
                                    element.data('cke-saved-fieldname', this.getValue());
                                  //  element.setText(this.getValue());
                                }
                                else {
                                    element.data('cke-saved-fieldname', false);
                                    element.removeAttribute('fieldname');
                                }

                                element.data('cke-saved-HTMLTypeID', '7');
                                element.data('cke-saved-DataTypeID', '0');
                                element.data('cke-saved-FieldTypeID', '1');
                                element.data('cke-saved-sqldbtype', 'VarChar');
                                element.data('cke-saved-SqlDbLength', '50');
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

		                        if (this.getValue())
		                            element.data('cke-saved-fieldDesc', this.getValue());
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
    CKEDITOR.dialog.add('browsebutton', function(editor) {
        return exampleDialog(editor);
    });
})();
