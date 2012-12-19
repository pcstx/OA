(function () {
CKEDITOR.dialog.add('HelloWorld', function(editor) {
var cssStyleClass = GPRP.getCSSStyleClass();  //获取样式

        return {
            title: '附件上传',
            minWidth: 300,
            minHeight: 200,
            buttons: [
            CKEDITOR.dialog.okButton,
            CKEDITOR.dialog.cancelButton],
            contents:
			[
				{
				    id: 'info',
				    label: '属性名称',
				    title: '属性名称',
				    elements:
					[
						{
						    id: 'FieldName',
						    type: 'text',
						    label: editor.lang.common.name,
						    'default': '',
						    accessKey: 'N',
						    validate: GPRP.validateName,   //自定义验证方法函数						
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

						        element.data('cke-saved-HTMLTypeID', '7');
						        element.data('cke-saved-DataTypeID', '0');
						        element.data('cke-saved-FieldTypeID', '1');
						        element.data('cke-saved-sqldbtype', 'VarChar');
						        element.data('cke-saved-SqlDbLength', '50');
						        element.data('cke-saved-TextLength', '0');
						    }
						},
						{
						    id: 'fieldDesc',
						    type: 'text',
						    label: "描述",
						    labelLayout: "",
						    'default': '',
						    accessKey: 'C',
						    style: 'width:200px',
						    //	validate : CKEDITOR.dialog.validate.integer( editor.lang.common.validateNumberFailed ),
						    setup: function(element) {
						        var value = element.hasAttribute('fieldDesc') && element.getAttribute('fieldDesc');
						        this.setValue(value || '');
						    },
						    commit: function(data) {
						    var element = data.element;
						        if (this.getValue())
						            element.setAttribute('fieldDesc', this.getValue());
						        else
						            element.removeAttribute('fieldDesc');
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
						
						//////////
					]
				}
			],
		    onShow: function () {
		  //  delete this.hiddenField;

		    var editor = this.getParentEditor(),
				selection = editor.getSelection(),
				element = selection.getSelectedElement();

		    if (element && element.data('cke-real-element-type') && element.data('cke-real-element-type') == 'file') {
		        this.hiddenField = element;
		        element = editor.restoreRealElement(this.hiddenField);
		        this.setupContent(element);
		        selection.selectElement(this.hiddenField);
		    }
		},
		onOk: function() {
		var editor,
				element = this.textField,
				isInsertMode = !element;

		if (isInsertMode) {
		    editor = this.getParentEditor();
		    element = editor.document.createElement('input');
		    element.setAttribute('type', 'file');
		}

		if (isInsertMode)
		    editor.insertElement(element);
		this.commitContent({ element: element });

		////////////////////////////////////////////////////////////////////////////////
            /*
		var name = this.getValueOf('info', '_cke_saved_name'),   //获取名称的值 
				editor = this.getParentEditor(),    //获取编辑器对象
				element = CKEDITOR.env.ie && !(CKEDITOR.document.$.documentMode >= 8) ?       //判断浏览器版本？？
					editor.document.createElement('<input> type=file ' + CKEDITOR.tools.htmlEncode(name) + '</input>')
					: editor.document.createElement('input');     //编辑器中增加Input元素

                // element.setAttribute('type', 'hidden');   //元素中添加属性
                this.commitContent(element);  //
                var fakeElement = editor.createFakeElement(element, 'hello', 'text');
                //生成伪装元素。参数1：真的元素；参数2：css类；参数3：元素类型；参数4：是否可缩放
                if (!this.text)  //判断元素的类型
                    editor.insertElement(fakeElement);  //将创建的元素插入到编辑器中
                else {
                    fakeElement.replace(this.text);
                    editor.getSelection().selectElement(fakeElement);  //选中元素
                }
                return true;

                //////////////
                this.commitContent(editor);
                var fakeElement = editor.createFakeElement(element, 'hello', 'HelloWorld');

                editor.insertElement(fakeElement);

                return true;
*/
            },
            resizable: CKEDITOR.DIALOG_RESIZE_HEIGHT
        };
    });
})();