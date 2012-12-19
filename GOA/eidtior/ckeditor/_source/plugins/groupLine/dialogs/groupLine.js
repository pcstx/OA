(function() {
    var exampleDialog = function(editor) {
        var cssStyleClass = GPRP.getCSSStyleClass();  //获取样式

        return {
            title: /* title in string*/"明细行选择",   //标题
            minWidth: /*number of pixels*/350,   //最小宽度
            minHeight: /*number of pixels*/250,   //最小高度
            buttons: /*array of button definitions*/[CKEDITOR.dialog.okButton, CKEDITOR.dialog.cancelButton],    //按钮
            onOk: /*function*/
                function() {
                    var editor = this.getParentEditor(),
		            element = this.input,
		            isInsertMode = !element;

                    editor = this.getParentEditor(); //获取父级编辑器对象

                    if (isInsertMode) {  //判断是否可以新增元素状态
                        element = editor.document.createElement('input');  //创建元素
                        element.setAttribute('type', 'groupLine'); 
                    }

                    this.commitContent({ element: element });  //增加窗体内元素的属性

                    var fakeElement = editor.createFakeElement(element, 'cke_hidden', 'groupLine');
                    //生成伪装元素。参数1：真的元素；参数2：css类；参数3：元素类型；参数4：是否可缩放
                    if (!this.label)  //判断元素的类型
                        editor.insertElement(fakeElement);  //将创建的元素插入到编辑器中
                    else {
                        fakeElement.replace(this.input);
                        editor.getSelection().selectElement(fakeElement);  //选中元素
                    }
                    return true;

                },        //点击确认按钮
            onLoad: /*function*/function() { },      //第一次打开时触发
            onShow: function() {
                delete this.groupLine;
                var editor = this.getParentEditor(),
				selection = editor.getSelection(),
				element = selection.getSelectedElement();

                if (element && element.getAttribute('data-cke-real-element-type') == "groupLine") {
                    this.groupLine = element;
                    element = editor.restoreRealElement(this.groupLine);
                    this.setupContent(element);
                    selection.selectElement(this.groupLine);
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
                id: 'info',  /* not CSS ID attribute! */
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
                                    element.setText(this.getValue());
                                }
                                else {
                                    element.data('cke-saved-fieldname', false);
                                    element.removeAttribute('fieldname');
                                }

                                element.data('cke-saved-HTMLTypeID', '9');
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
                            id: 'DataSetID',
                            type: 'text',
                            //      widths: ['25%', '75%'],
                            //     labelLayout: 'horizontal',
                            label: "对应数据集",
                            'default': '',
                            accessKey: 'V',
                            onLoad: function() {
                                this.getInputElement().setAttribute('readOnly', true);
                            },
                            onClick: function() {
                                $fieldDesc = this.getDialog().getContentElement("info", "DataSetID");
                                $datasetID = this.getDialog().getContentElement("info", "DataSetIDValue");

                               // ExecuteCommand('detailGroup');
                                $.ligerDialog.open({ title: '选择数据集', name: 'winselector', width: 700, height: 400, isResize: true, url: '../demo/DataSet.htm', buttons: [
                        { text: '确定', onclick: f_selectContactOK2 },
                        { text: '取消', onclick: f_selectContactCancel2 }
                         ]
                                });
                            },
                            setup: function(name, element) {
                                if (name == 'clear')
                                    this.setValue(this['default'] || '');
                                else if (name == 'select') {
                                    this.setValue(element.data('cke-saved-GroupLineDataSetName') || element.getAttribute('GroupLineDataSetName') || '');
                                }
                            },
                            commit: function(data) {   //点击确认后增加相应的属性，名称为cke-saved- XXX
                                var element = data.element;

                                if (this.getValue())
                                    element.data('cke-saved-GroupLineDataSetName', this.getValue());
                                else {
                                    element.data('cke-saved-GroupLineDataSetName', false);
                                    element.removeAttribute('GroupLineDataSetName');
                                }
                            }
                        }, //描述
                        {
                        id: 'DataSetIDValue',
                        type: 'text',
                        labelLayout: 'horizontal',
                        label: "值",
                        'default': '',
                        style: 'display:none',
                        setup: function(name, element) {
                            if (name == 'clear')
                                this.setValue(this['default'] || '');
                            else if (name == 'select') {
                                this.setValue(element.data('cke-saved-GroupLineDataSetID') || element.getAttribute('GroupLineDataSetID') || '');
                            }
                        },
                        commit: function(data) {   //点击确认后增加相应的属性，名称为cke-saved- XXX
                            var element = data.element;

                            if (this.getValue())
                                element.data('cke-saved-GroupLineDataSetID', this.getValue());
                            else {
                                element.data('cke-saved-GroupLineDataSetID', false);
                                element.removeAttribute('GroupLineDataSetID');
                            }
                        }
                    }, //隐藏值
                           {
                           type: 'select',
                           id: 'group',
                           label: "对应明细组",
                           items: [],
                           //   items: detailFieldGroup,
                           //   required: true,
                           onShow: function() {
                               var g = this.getDialog().getContentElement("info", "group");
                               g.clear();
                               g.add("");
                               for (var i = 0; i < GPRP.json.items.length; i++) {
                                   g.add(GPRP.json.items[i].groupName);
                               }

                           },
                           setup: function(element) {   //双击已增加控件回传值

                               this.setValue(element.data('cke-saved-group') ||
								element.getAttribute('group') ||
								'');
                           },
                           commit: function(data) {   //点击确认后增加相应的属性，名称为cke-saved- XXX
                               var element = data.element;

                               if (this.getValue())
                                   element.data('cke-saved-group', this.getValue());
                               else {
                                   element.data('cke-saved-group', false);
                                   element.removeAttribute('group');
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
    CKEDITOR.dialog.add('groupLine', function(editor) {
        return exampleDialog(editor);
    });
})();

function f_selectContactOK2(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        GPRP.warn('请选择行!');
        return;
    }

    $fieldDesc.setValue(data.DataSetName);  //
    $datasetID.setValue(data.DataSetID);
    dialog.close();
}

function f_selectContactCancel2(item, dialog) {
    dialog.close();
}

function getItems() { 
    var item = "";
    for (var i = 0; i < GPRP.json.items.length; i++) {
          item += "[\"" + GPRP.json.items[i].groupName + "\"],";
    }

    if (item != "") { 
         item = item.substr(0, item.length - 1);
    }

    var items = "[" + item + "]"; 
    return eval('(' + items + ')');
}

function ExecuteCommand(commandName) {
    // Get the editor instance that we want to interact with.
    var oEditor = CKEDITOR.instances.editor1;

    // Check the active editing mode.
    if (oEditor.mode == 'wysiwyg') {
        // Execute the command.
        // http://docs.cksource.com/ckeditor_api/symbols/CKEDITOR.editor.html#execCommand
        oEditor.execCommand(commandName);
    }
    else
        alert('You must be in WYSIWYG mode!');
}