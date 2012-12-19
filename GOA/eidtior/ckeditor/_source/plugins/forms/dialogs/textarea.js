CKEDITOR.dialog.add( 'textarea', function( editor ) {
var cssStyleClass = GPRP.getCSSStyleClass();  //获取样式

	return {
		title : editor.lang.textarea.title,
		minWidth : 350,
		minHeight : 250,
		onShow : function()
		{
			delete this.textarea;

			var element = this.getParentEditor().getSelection().getSelectedElement();
			if ( element && element.getName() == "textarea" )
			{
				this.textarea = element;
				this.setupContent( element );
			}
		},
		onOk : function()
		{
			var editor,
				element = this.textarea,
				isInsertMode = !element;

			if ( isInsertMode )
			{
				editor = this.getParentEditor();
				element = editor.document.createElement( 'textarea' );
			}
			this.commitContent( element );

			if ( isInsertMode )
				editor.insertElement( element );
		},
		contents : [
			{
				id : 'info',
				label : editor.lang.textarea.title,
				title : editor.lang.textarea.title,
				elements : [
					{
						id : '_cke_saved_name',
						type : 'text',
						label : editor.lang.common.name,
						'default' : '',
						accessKey: 'N',
						validate: GPRP.validateName,   //自定义验证方法函数						
						setup : function( element )
						{
							this.setValue(
									element.data('cke-saved-FieldName') ||
									element.getAttribute('FieldName') ||
									'' );
						},
						commit : function( element )
						{
							if ( this.getValue() )
							    element.data('cke-saved-FieldName', this.getValue());
							else
							{
							    element.data('cke-saved-FieldName', false);
							    element.removeAttribute('FieldName');
				            }
 
				            element.data('cke-saved-HTMLTypeID', '3');
				            element.data('cke-saved-DataTypeID', '0');
				            element.data('cke-saved-FieldTypeID', '1');
				            element.data('cke-saved-sqldbtype', 'VarChar');
				            element.data('cke-saved-SqlDbLength', '1000');
				            element.data('cke-saved-TextLength', '0');
						}
					},
					 
							{
							    id: 'fieldDesc',
								type : 'text',
								label: "描述",
								labelLayout: "",
								'default' : '',
								accessKey : 'C',
								style : 'width:200px',
							//	validate : CKEDITOR.dialog.validate.integer( editor.lang.common.validateNumberFailed ),
								setup : function( element )
								{
								    var value = element.hasAttribute('fieldDesc') && element.getAttribute('fieldDesc');
									this.setValue( value || '' );
								},
								commit : function( element )
								{
									if ( this.getValue() )
									    element.setAttribute('fieldDesc', this.getValue());
									else
									    element.removeAttribute('fieldDesc');
								}
							},
							{
								id : 'Rows',
								type : 'text',
								label: "高度",
								labelLayout: "",
								'default': '0',
								accessKey : 'R',
								style: 'width:200px;',
								validate : CKEDITOR.dialog.validate.integer( editor.lang.common.validateNumberFailed ),
								setup : function( element )
								{
								    var value = element.hasAttribute('Rows') && element.getAttribute('Rows');
									this.setValue( value || '' );
								},
								commit : function( element )
								{
									if ( this.getValue() )
									    element.setAttribute('Rows', this.getValue());
									else
									    element.removeAttribute('Rows');

									element.data('cke-saved-TextHeight', this.getValue());
								}
							}
				 ,
					{
						id : 'IsHTML',
						type: 'checkbox',
						label: "HTML编辑字段",
						labelLayout: "horizontal",
						'default' : '',
						setup: function(element) {
						var value = element.hasAttribute('IsHTML') && element.getAttribute('IsHTML');
						    this.setValue(value || '');
						},
						commit: function(element) {
						    if (this.getValue())
						        element.setAttribute('IsHTML', this.getValue());
						    else
						        element.removeAttribute('IsHTML');
						}
		            },
					 {
					     type: 'select',
					     id: 'cssStyleClass',
					     label: "样式",
					     items: cssStyleClass,
				//	     required: true,
					     setup: function(element) {   //双击已增加控件回传值
					     this.setValue(element.data('cke-saved-cssStyleClass') ||
								element.getAttribute('cke-saved-cssStyleClass') ||
								'');
					     },
					     commit: function(element) {   //点击确认后增加相应的属性，名称为cke-saved- XXX 
					         if (this.getValue())
					             element.data('cke-saved-cssStyleClass', this.getValue());
					         else {
					             element.data('cke-saved-cssStyleClass', false);
					             element.removeAttribute('cke-saved-cssStyleClass');
					         }
					     }
					 }

				]
			}
		]
	};
});
