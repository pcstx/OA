/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.dialog.add('anchor', function(editor) {
    var cssStyleClass = GPRP.getCSSStyleClass();  //获取样式
    var browseType = GPRP.getBrowseType();

    // Function called in onShow to load selected element.
    var loadElements = function(element) {
        this._.selectedElement = element;

        var attributeValue = element.data('cke-saved-fieldname');
        this.setValueOf('info', 'txtName', attributeValue || '');
    };

    function createFakeAnchor(editor, anchor) {
        return editor.createFakeElement(anchor, 'cke_anchor', 'anchor');
    }

    return {
        title: "浏览按钮",
        minWidth: 300,
        minHeight: 200,
        onOk: function()      //按下确定按钮后生成的操作
        {
            var name = this.getValueOf('info', 'FieldName');
            var fieldDesc = this.getValueOf('info', 'fieldDesc');
            var browseType = this.getValueOf('info', 'browseType');
            var cssStyleClass = this.getValueOf('info', 'cssStyleClass');
            var attributes =
			{
			//    name: name,
			    'data-cke-saved-fieldname': name,
			    'data-cke-saved-fieldDesc': fieldDesc,
			    'data-cke-saved-browseType': browseType,
			    'HTMLTypeID': '8',
			    'DataTypeID': '0',
			    'FieldTypeID': '1',
			    'sqldbtype': 'VarChar',
			    'type': 'update',
			    'SqlDbLength': '600',
			    'TextLength': '0',
			    'CSSStyleClass':cssStyleClass
			};

            if (this._.selectedElement) {
                if (this._.selectedElement.data('cke-realelement')) {
                    var newFake = createFakeAnchor(editor, editor.document.createElement('a', { attributes: attributes }));
                    newFake.replace(this._.selectedElement);
                }
                else
                    this._.selectedElement.setAttributes(attributes);
            }
            else {
                var sel = editor.getSelection(),
						range = sel && sel.getRanges()[0];

                // Empty anchor
                if (range.collapsed) {
                    if (CKEDITOR.plugins.link.synAnchorSelector)
                        attributes['class'] = 'cke_anchor_empty';

                    if (CKEDITOR.plugins.link.emptyAnchorFix) {
                        attributes['contenteditable'] = 'false';
                        attributes['data-cke-editable'] = 1;
                    }

                    var anchor = editor.document.createElement('a', { attributes: attributes });

                    // Transform the anchor into a fake element for browsers that need it.
                    if (CKEDITOR.plugins.link.fakeAnchor)
                        anchor = createFakeAnchor(editor, anchor);

                    range.insertNode(anchor);
                }
                else {
                    if (CKEDITOR.env.ie && CKEDITOR.env.version < 9)
                        attributes['class'] = 'cke_anchor';

                    // Apply style.
                    var style = new CKEDITOR.style({ element: 'a', attributes: attributes });
                    style.type = CKEDITOR.STYLE_INLINE;
                    style.apply(editor.document);
                }
            }
        },

        onHide: function() {
            delete this._.selectedElement;
        },

        onShow: function() {
            var selection = editor.getSelection(),
				fullySelected = selection.getSelectedElement(),
				partialSelected;

            // Detect the anchor under selection.
            if (fullySelected) {
                if (CKEDITOR.plugins.link.fakeAnchor) {
                    var realElement = CKEDITOR.plugins.link.tryRestoreFakeAnchor(editor, fullySelected);
                    realElement && loadElements.call(this, realElement);
                    this._.selectedElement = fullySelected;
                }
                else if (fullySelected.is('a') && fullySelected.hasAttribute('FieldName'))
                    loadElements.call(this, fullySelected);
            }
            else {
                partialSelected = CKEDITOR.plugins.link.getSelectedLink(editor);
                if (partialSelected) {
                    loadElements.call(this, partialSelected);
                    selection.selectElement(partialSelected);
                }
            }

            this.getContentElement('info', 'FieldName').focus();
        },
        contents: [
			{
			    id: 'info',
			    label: editor.lang.anchor.title,
			    accessKey: 'I',
			    elements:
				[
					{
					    type: 'text',
					    id: 'FieldName',
					    label: editor.lang.textfield.name,
					    required: true,
					    validate: GPRP.validateName,   //自定义验证方法函数	,
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
						    }
					},
		                {
		                    type: 'text',
		                    id: 'fieldDesc',
		                    label: "描述",
		                    required: true
		                },
					    {
					        type: 'select',
					        id: 'browseType',
					        label: "浏览类型",
					        items: browseType,
					        required: true,
					        onShow: function(e) {
					            this.setValue("1");
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
    };
});
