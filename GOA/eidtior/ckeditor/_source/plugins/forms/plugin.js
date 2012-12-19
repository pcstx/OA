CKEDITOR.plugins.add('forms',
{
    init: function(editor) {
        var lang = editor.lang;

        editor.addCss(
			'form' +
			'{' +
				'border: 1px dotted #FF0000;' +
				'padding: 2px;' +
			'}\n');

        editor.addCss(
			'img.cke_hidden' +
			'{' +
				'background-image: url(' + CKEDITOR.getUrl(this.path + '../../skins/pluginsImages/groupLine.png') + ');' +
				'background-position: center center;' +
				'background-repeat: no-repeat;' +
				'border: 1px solid #a9a9a9;' +
				'width: 21px !important;' +
				'height: 21px !important;' +
			'}');

        editor.addCss(
			'img.cke_update' +
			'{' +
				'background-image: url(' + CKEDITOR.getUrl(this.path + '../../skins/pluginsImages/browsebutton.png') + ');' +
				'background-position: center center;' +
				'background-repeat: no-repeat;' +
				'border: 1px solid #a9a9a9;' +
				'width: 130px !important;' +
				'height: 30px !important;' +
			'}');

        // All buttons use the same code to register. So, to avoid
        // duplications, let's use this tool function.
        var addButtonCommand = function(buttonName, commandName, dialogFile) {
            editor.addCommand(commandName, new CKEDITOR.dialogCommand(commandName));

            editor.ui.addButton(buttonName,
				{
				    label: lang.common[buttonName.charAt(0).toLowerCase() + buttonName.slice(1)],
				    command: commandName
				});
            CKEDITOR.dialog.add(commandName, dialogFile);
        };

        var dialogPath = this.path + 'dialogs/';
        addButtonCommand('Form', 'form', dialogPath + 'form.js');
        addButtonCommand('Checkbox', 'checkbox', dialogPath + 'checkbox.js');
        addButtonCommand('Radio', 'radio', dialogPath + 'radio.js');
        addButtonCommand('TextField', 'textfield', dialogPath + 'textfield.js');
        addButtonCommand('Textarea', 'textarea', dialogPath + 'textarea.js');
        addButtonCommand('Select', 'select', dialogPath + 'select.js');
        addButtonCommand('Button', 'button', dialogPath + 'button.js');
        addButtonCommand('ImageButton', 'imagebutton', CKEDITOR.plugins.getPath('image') + 'dialogs/image.js');
        addButtonCommand('HiddenField', 'hiddenfield', dialogPath + 'hiddenfield.js');
        //  addButtonCommand('Browsebutton', 'browsebutton', dialogPath + 'browsebutton.js');

        // If the "menu" plugin is loaded, register the menu items.
        if (editor.addMenuItems) {
            editor.addMenuItems(
				{
				    form:
					{
					    label: lang.form.menu,
					    command: 'form',
					    group: 'form'
					},

				    checkbox:
					{
					    label: lang.checkboxAndRadio.checkboxTitle,
					    command: 'checkbox',
					    group: 'checkbox'
					},

				    radio:
					{
					    label: lang.checkboxAndRadio.radioTitle,
					    command: 'radio',
					    group: 'radio'
					},

				    textfield:
					{
					    label: lang.textfield.title,
					    command: 'textfield',
					    group: 'textfield'
					},

				    hiddenfield:
					{
					    label: lang.hidden.title,
					    command: 'hiddenfield',
					    group: 'hiddenfield'
					},

				    imagebutton:
					{
					    label: lang.image.titleButton,
					    command: 'imagebutton',
					    group: 'imagebutton'
					},

				    button:
					{
					    label: lang.button.title,
					    command: 'button',
					    group: 'button'
					},

				    select:
					{
					    label: "下拉框/多选框",
					    command: 'select',
					    group: 'select'
					},

				    textarea:
					{
					    label: lang.textarea.title,
					    command: 'textarea',
					    group: 'textarea'
					}

				    //browsebutton:
				    //{
				    //   label: "浏览按钮",
				    //   command:'browsebutton',
				    //   group: 'browsebutton'
				    //}
				});
        }

        // If the "contextmenu" plugin is loaded, register the listeners.
        if (editor.contextMenu) {
            editor.contextMenu.addListener(function(element) {
                if (element && element.hasAscendant('form', true) && !element.isReadOnly())
                    return { form: CKEDITOR.TRISTATE_OFF };
            });

            editor.contextMenu.addListener(function(element) {
                if (element && !element.isReadOnly()) {
                    var name = element.getName();

                    if (name == 'select')
                        return { select: CKEDITOR.TRISTATE_OFF };

                    if (name == 'textarea')
                        return { textarea: CKEDITOR.TRISTATE_OFF };

                    if (name == 'input') {
                        switch (element.getAttribute('type')) {
                            case 'button':
                            case 'submit':
                            case 'reset':
                                return { button: CKEDITOR.TRISTATE_OFF };

                            case 'checkbox':
                                return { checkbox: CKEDITOR.TRISTATE_OFF };

                            case 'radio':
                                return { radio: CKEDITOR.TRISTATE_OFF };

                            case 'image':
                                return { imagebutton: CKEDITOR.TRISTATE_OFF };

                                //      case 'file':
                                //        return { browsebutton: CKEDITOR.TRISTATE_OFF };

                            default:
                                return { textfield: CKEDITOR.TRISTATE_OFF };
                        }
                    }

                    if (name == 'img' && element.data('cke-real-element-type') == 'hiddenfield')
                        return { hiddenfield: CKEDITOR.TRISTATE_OFF };
                }
            });
        }

        editor.on('doubleclick', function(evt) {
            var element = evt.data.element;

            if (element.is('form'))
                evt.data.dialog = 'form';
            else if (element.is('select'))
                evt.data.dialog = 'select';
            else if (element.is('textarea'))
                evt.data.dialog = 'textarea';
            else if (element.is('img') && element.data('cke-real-element-type') == 'hiddenfield')
                evt.data.dialog = 'hiddenfield';
            else if (element.is('img') && element.data('cke-real-element-type') == 'update')
            {
                evt.data.dialog = 'update';
            } 
            else if (element.is('input')) {
                switch (element.getAttribute('type')) {
                    case 'button':
                    case 'submit':
                    case 'reset':
                        evt.data.dialog = 'button';
                        break;
                    case 'checkbox':
                        evt.data.dialog = 'checkbox';
                        break;
                    case 'radio':
                        evt.data.dialog = 'radio';
                        break;
                    case 'image':
                        evt.data.dialog = 'imagebutton';
                        break;
                    case 'file':
                        evt.data.dialog = 'browsebutton';
                        break;
                    case 'groupLine':
                        evt.data.dialog = 'groupLine'; 
                    default:
                        evt.data.dialog = 'textfield';
                        break;
                }
            }
        });

    },

    afterInit: function(editor) {
        var dataProcessor = editor.dataProcessor,
			htmlFilter = dataProcessor && dataProcessor.htmlFilter,
			dataFilter = dataProcessor && dataProcessor.dataFilter;

        // Cleanup certain IE form elements default values.
        if (CKEDITOR.env.ie) {
            htmlFilter && htmlFilter.addRules(
			{
			    elements:
				{
				    input: function(input) {
				        var attrs = input.attributes,
							type = attrs.type;
				        // Old IEs don't provide type for Text inputs #5522
				        if (!type)
				            attrs.type = 'text';
				        if (type == 'checkbox' || type == 'radio')
				            attrs.value == 'on' && delete attrs.value;
				    }
				}
			});
        }

        if (dataFilter) {
            dataFilter.addRules(
			{
			    elements:
				{
				    input: function(element) {
				        if (element.attributes.type == 'hidden')   //判断类型进行伪装
				            return editor.createFakeParserElement(element, 'cke_hidden', 'hiddenfield');
				        else if (element.attributes.type == 'DIY') {
				            return editor.createFakeParserElement(element, 'cke_hidden', 'DIY');
				        }
				        else if (element.attributes.type == 'groupLine') {
				            return editor.createFakeParserElement(element, 'cke_hidden', 'groupLine');
				        }
				        else if (element.attributes.type == 'update') {
				            return editor.createFakeParserElement(element, 'cke_update', 'update');
				        }
				    }
				}
			});
        }
    },
    requires: ['image', 'fakeobjects']
});

if ( CKEDITOR.env.ie )
{
	CKEDITOR.dom.element.prototype.hasAttribute = CKEDITOR.tools.override( CKEDITOR.dom.element.prototype.hasAttribute,
		function( original )
		{
			return function( name )
				{
					var $attr = this.$.attributes.getNamedItem( name );

					if ( this.getName() == 'input' )
					{
						switch ( name )
						{
							case 'class' :
								return this.$.className.length > 0;
							case 'checked' :
								return !!this.$.checked;
							case 'value' :
								var type = this.getAttribute( 'type' );
								return type == 'checkbox' || type == 'radio' ? this.$.value != 'on' : this.$.value;
						}
					}

					return original.apply( this, arguments );
				};
		});
}
 