/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/
var win1;
function f_open() {
    if (win1) win1.show();
    else win1 = $.ligerDialog.open({ height: 200, url: '../../welcome.htm', width: null, showMax: true, showToggle: true, showMin: true, isResize: true, slide: false });
}

CKEDITOR.dialog.add( 'hiddenfield', function( editor ) {
var cssStyleClass = GPRP.getCSSStyleClass();  //获取样式

	return {
		title : editor.lang.hidden.title,
		hiddenField : null,
		minWidth : 350,
		minHeight : 110,
		onShow : function()
		{
			delete this.hiddenField;

			var editor = this.getParentEditor(),
				selection = editor.getSelection(),
				element = selection.getSelectedElement();

			if ( element && element.data( 'cke-real-element-type' ) && element.data( 'cke-real-element-type' ) == 'hiddenfield' )
			{
				this.hiddenField = element;
				element = editor.restoreRealElement( this.hiddenField );
				this.setupContent( element );
				selection.selectElement( this.hiddenField );
			}
		},
		onOk : function()
		{
			var name = this.getValueOf( 'info', '_cke_saved_name' ),   //获取名称的值
				value = this.getValueOf( 'info', 'value' ),             //获取值的值
				editor = this.getParentEditor(),    //获取编辑器对象
				element = CKEDITOR.env.ie && !( CKEDITOR.document.$.documentMode >= 8 ) ?       //判断浏览器版本？？
					editor.document.createElement( '<input name="' + CKEDITOR.tools.htmlEncode( name ) + '">' )
					: editor.document.createElement( 'input' );     //编辑器中增加Input元素

			element.setAttribute( 'type', 'hidden' );   //元素中添加属性
			this.commitContent( element );  //
			var fakeElement = editor.createFakeElement(element, 'cke_hidden', 'hiddenfield');  
            //生成伪装元素。参数1：真的元素；参数2：css类；参数3：元素类型；参数4：是否可缩放
			if ( !this.hiddenField )  //判断元素的类型
				editor.insertElement( fakeElement );  //将创建的元素插入到编辑器中
			else
			{
				fakeElement.replace( this.hiddenField );
				editor.getSelection().selectElement( fakeElement );  //选中元素
			}
			return true;
		},
		contents : [
			{
				id : 'info',
				label : editor.lang.hidden.title,
				title : editor.lang.hidden.title,
				elements : [
					{
						id : '_cke_saved_name',
						type : 'text',
						label : editor.lang.hidden.name,
						'default' : '',
						accessKey : 'N',
						setup : function( element )
						{
							this.setValue(
										element.data('cke-saved-FieldName') ||
									element.getAttribute('FieldName') ||
									'' );
						},
						commit : function( element )
						{
						    if (this.getValue())
						        element.data('cke-saved-FieldName', this.getValue());
						    else {
						        element.data('cke-saved-FieldName', false);
						        element.removeAttribute('FieldName');
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
                            type: 'html',
                            html: '  <input type="button" onclick="f_open()" value="open window" />',
                            onShow: function() {
//                            $("#txtContactName").ligerComboBox({
//                                onBeforeOpen: f_selectContact, valueFieldID: 'hidCustomerID', width: 300
//                            });
                             //   $("#c2").html("helloworld");
                            //    $("#c2").bind("click", function() { alert(1); });
                            }
                        },
					{
						id : 'value',
						type : 'text',
						label : "描述",
						'default' : '',
						accessKey: 'V',						
						setup : function( element )
						{
							this.setValue( element.getAttribute( 'value' ) || '' );
						},
						commit : function( element )
						{
							if ( this.getValue() )
								element.setAttribute( 'value', this.getValue() );
							else
								element.removeAttribute( 'value' );
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
