/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/
CKEDITOR.dialog.add( 'checkbox', function( editor ) {
var cssStyleClass = GPRP.getCSSStyleClass();  //获取样式
	return {
		title : editor.lang.checkboxAndRadio.checkboxTitle,
		minWidth : 350,
		minHeight : 140,
		onShow : function()
		{
			delete this.checkbox;

			var element = this.getParentEditor().getSelection().getSelectedElement();

			if ( element && element.getAttribute( 'type' ) == 'checkbox' )
			{
				this.checkbox = element;
				this.setupContent( element );
			}
		},
		onOk : function()
		{
			var editor,
				element = this.checkbox,
				isInsertMode = !element;

			if ( isInsertMode )
			{
				editor = this.getParentEditor();
				element = editor.document.createElement( 'input' );
				element.setAttribute( 'type', 'checkbox' );
				editor.insertElement( element );
			}
			this.commitContent( { element : element } );
		},
		contents : [
			{
				id : 'info',
				label : editor.lang.checkboxAndRadio.checkboxTitle,
				title : editor.lang.checkboxAndRadio.checkboxTitle,
				startupFocus: '_cke_saved_FieldName',
				elements : [
					{
					    id: '_cke_saved_FieldName',
						type : 'text',
						label : editor.lang.common.name,
						'default' : '',
						accessKey: 'N',
						validate: GPRP.validateName,
						setup : function( element )
						{
							this.setValue(
									element.data( 'cke-saved-Fieldname' ) ||
									element.getAttribute('FieldName') ||
									'' );
						},
						commit : function( data ) { 
							var element = data.element;

							// IE failed to update 'name' property on input elements, protect it now.
							if ( this.getValue() )
								element.data( 'cke-saved-Fieldname', this.getValue() );
							else
							{
								element.data( 'cke-saved-Fieldname', false );
								element.removeAttribute('FieldName');
				            }

				element.data('cke-saved-HTMLTypeID', '6');
				element.data('cke-saved-DataTypeID', '0');
				element.data('cke-saved-FieldTypeID', '1');
				element.data('cke-saved-sqldbtype', 'VarChar');
				element.data('cke-saved-SqlDbLength', '50');
				element.data('cke-saved-TextLength', '0');
							
						}
					},
					{
					    id: 'fieldDesc',
						type : 'text',
						label : "描述",
						'default' : '',
						accessKey : 'V',
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
	};
});
