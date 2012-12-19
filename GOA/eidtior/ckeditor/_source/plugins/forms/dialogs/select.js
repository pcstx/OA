/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/
CKEDITOR.dialog.add('select', function(editor) {
    var cssStyleClass = GPRP.getCSSStyleClass();  //获取样式
    // Add a new option to a SELECT object (combo or list).
    function addOption(combo, optionText, optionValue, documentObject, index) {
        combo = getSelect(combo);
        var oOption;
        if (documentObject)
            oOption = documentObject.createElement("OPTION");
        else
            oOption = document.createElement("OPTION");

        if (combo && oOption && oOption.getName() == 'option') {
            if (CKEDITOR.env.ie) {
                if (!isNaN(parseInt(index, 10)))
                    combo.$.options.add(oOption.$, index);
                else
                    combo.$.options.add(oOption.$);

                oOption.$.innerHTML = optionText.length > 0 ? optionText : '';
                oOption.$.value = optionValue;
            }
            else {
                if (index !== null && index < combo.getChildCount())
                    combo.getChild(index < 0 ? 0 : index).insertBeforeMe(oOption);
                else
                    combo.append(oOption);

                oOption.setText(optionText.length > 0 ? optionText : '');
                oOption.setValue(optionValue);
            }
        }
        else
            return false;

        return oOption;
    }
    // Remove all selected options from a SELECT object.
    function removeSelectedOptions(combo) {
        combo = getSelect(combo);

        // Save the selected index
        var iSelectedIndex = getSelectedIndex(combo);

        // Remove all selected options.
        for (var i = combo.getChildren().count() - 1; i >= 0; i--) {
            if (combo.getChild(i).$.selected)
                combo.getChild(i).remove();
        }

        // Reset the selection based on the original selected index.
        setSelectedIndex(combo, iSelectedIndex);
    }
    //Modify option  from a SELECT object.
    function modifyOption(combo, index, title, value) {
        combo = getSelect(combo);
        if (index < 0)
            return false;
        var child = combo.getChild(index);
        child.setText(title);
        child.setValue(value);
        return child;
    }
    function removeAllOptions(combo) {
        combo = getSelect(combo);
        while (combo.getChild(0) && combo.getChild(0).remove())
        { /*jsl:pass*/ }
    }
    // Moves the selected option by a number of steps (also negative).
    function changeOptionPosition(combo, steps, documentObject) {
        combo = getSelect(combo);
        var iActualIndex = getSelectedIndex(combo);
        if (iActualIndex < 0)
            return false;

        var iFinalIndex = iActualIndex + steps;
        iFinalIndex = (iFinalIndex < 0) ? 0 : iFinalIndex;
        iFinalIndex = (iFinalIndex >= combo.getChildCount()) ? combo.getChildCount() - 1 : iFinalIndex;

        if (iActualIndex == iFinalIndex)
            return false;

        var oOption = combo.getChild(iActualIndex),
			sText = oOption.getText(),
			sValue = oOption.getValue();

        oOption.remove();

        oOption = addOption(combo, sText, sValue, (!documentObject) ? null : documentObject, iFinalIndex);
        setSelectedIndex(combo, iFinalIndex);
        return oOption;
    }
    function getSelectedIndex(combo) {
        combo = getSelect(combo);
        return combo ? combo.$.selectedIndex : -1;
    }
    function setSelectedIndex(combo, index) {
        combo = getSelect(combo);
        if (index < 0)
            return null;
        var count = combo.getChildren().count();
        combo.$.selectedIndex = (index >= count) ? (count - 1) : index;
        return combo;
    }
    function getOptions(combo) {
        combo = getSelect(combo);
        return combo ? combo.getChildren() : false;
    }
    function getSelect(obj) {
        if (obj && obj.domId && obj.getInputElement().$)				// Dialog element.
            return obj.getInputElement();
        else if (obj && obj.$)
            return obj;
        return false;
    }

    return {
        title: "多选框/下拉框",
        minWidth: CKEDITOR.env.ie ? 430 : 395,
        minHeight: CKEDITOR.env.ie ? 350 : 300,
        onShow: function() {
            delete this.selectBox;
            this.setupContent('clear');
            var element = this.getParentEditor().getSelection().getSelectedElement();
            if (element && element.getName() == "select") {
                this.selectBox = element;
                this.setupContent(element.getName(), element);

                // Load Options into dialog.
                var objOptions = getOptions(element);
                for (var i = 0; i < objOptions.count(); i++)
                    this.setupContent('option', objOptions.getItem(i));
            }
        },
        onOk: function() {
            var editor = this.getParentEditor(),
				element = this.selectBox,
				isInsertMode = !element;

            if (isInsertMode)
                element = editor.document.createElement('select');
            this.commitContent(element);

            if (isInsertMode) {
                editor.insertElement(element);
                if (CKEDITOR.env.ie) {
                    var sel = editor.getSelection(),
						bms = sel.createBookmarks();
                    setTimeout(function() {
                        sel.selectBookmarks(bms);
                    }, 0);
                }
            }
        },
        contents: [
			{
			    id: 'info',
			    label: editor.lang.select.selectInfo,
			    title: editor.lang.select.selectInfo,
			    accessKey: '',
			    elements: [
					{
					    id: 'FieldName',
					    type: 'text',
					    widths: ['25%', '75%'],
					    labelLayout: 'horizontal',
					    label: editor.lang.common.name,
					    'default': '',
					    accessKey: 'N',
					    validate: GPRP.validateName,   //自定义验证方法函数
					    style: 'width:350px',
					    setup: function(name, element) {
					        if (name == 'clear')
					            this.setValue(this['default'] || '');
					        else if (name == 'select') {
					            this.setValue(
										element.data('cke-saved-FieldName') || element.getAttribute('FieldName') || '');
					        }
					    },
					    commit: function(element) {
					        if (this.getValue())
					            element.data('cke-saved-FieldName', this.getValue());
					        else {
					            element.data('cke-saved-FieldName', false);
					            element.removeAttribute('FieldName');
					        }

					        //   element.data('cke-saved-multiple', true);
					        element.data('cke-saved-DataTypeID', '0');
					        element.data('cke-saved-FieldTypeID', '1');
					        element.data('cke-saved-sqldbtype', 'VarChar');
					        element.data('cke-saved-SqlDbLength', '600');
					        element.data('cke-saved-TextLength', '0');
					    }
					},  //名称
                    {
                    id: 'fieldDesc',
                    type: 'text',
                    widths: ['25%', '75%'],
                    labelLayout: 'horizontal',
                    label: "描述",
                    'default': '',
                    accessKey: 'V',
                    style: 'width:350px',
                    setup: function(name, element) {
                        if (name == 'clear')
                            this.setValue(this['default'] || '');
                        else if (name == 'select') {
                            this.setValue(element.data('cke-saved-fieldDesc') || element.getAttribute('fieldDesc') || '');
                        }
                    },
                    commit: function(element) {   //点击确认后增加相应的属性，名称为cke-saved- XXX
                        // var element = data.element;

                        if (this.getValue())
                            element.data('cke-saved-fieldDesc', this.getValue());
                        else {
                            element.data('cke-saved-fieldDesc', false);
                            element.removeAttribute('fieldDesc');
                        }
                    }
                }, //描述
					{
					id: 'txtValue',
					type: 'text',
					widths: ['25%', '75%'],
					labelLayout: 'horizontal',
					label: editor.lang.select.value,
					style: 'width:350px',
					'default': '',
					className: 'cke_disabled',
					onLoad: function() {
					    this.getInputElement().setAttribute('readOnly', true);
					},
					setup: function(name, element) {
					    if (name == 'clear')
					        this.setValue('');
					    else if (name == 'option' && element.getAttribute('selected'))
					        this.setValue(element.$.value);
					}
	},  //值
					{
					type: 'hbox',
					widths: ['175px', '170px'],
					children:
						[
							{
							    id: 'txtSize',
							    type: 'text',
							    labelLayout: 'horizontal',
							    label: editor.lang.select.size,
							    'default': '',
							    accessKey: 'S',
							    style: 'width:175px',
							    validate: function() {
							        var func = CKEDITOR.dialog.validate.integer(editor.lang.common.validateNumberFailed);
							        return ((this.getValue() === '') || func.apply(this));
							    },
							    setup: function(name, element) {
							        if (name == 'select')
							            this.setValue(element.getAttribute('size') || '');
							        if (CKEDITOR.env.webkit)
							            this.getInputElement().setStyle('width', '86px');
							    },
							    commit: function(element) {
							        if (this.getValue()) {
							            element.setAttribute('size', this.getValue());
							            element.data('cke-saved-type', 'list');
							            element.data('cke-saved-HTMLTypeID', '4');
							        }
							        else {
							            element.removeAttribute('size');
							            element.data('cke-saved-type', 'select');
							            element.data('cke-saved-HTMLTypeID', '5');
							        }
							    }
							},
							{
							    type: 'html',
							    html: '<span>' + CKEDITOR.tools.htmlEncode(editor.lang.select.lines) + '</span>'
							}
						]
	},   //高度
                    {
                    id: 'IsDynamic',
                    type: 'radio',
                    widths: ['25%', '75%'],
                    labelLayout: 'horizontal',
                    label: editor.lang.select.size,
                    'default': '0',
                    items: [["静态", "0"], ["动态", "1"]],
                    accessKey: 'S',

                    onLoad: function(e) {
                        this.setValue("0");
                    },
                    //    style: 'width:175px',
                    onClick: function(e) {
                        var state = this.getDialog().getContentElement("info", "IsDynamic");
                        var selectText = state.getValue();
                        st(this, selectText);
                    },
                    setup: function(name, element) {
                        if (name == 'select') {
                            var selectText = element.getAttribute('data-cke-saved-IsDynamic');
                            if (selectText == null)
                                selectText = element.getAttribute('IsDynamic');
                        }
                        else if (name == 'clear') {
                            st(this, "0");
                            return false;
                        }
                        if (selectText == 0) {
                            this.setValue("0");
                        }
                        else if (selectText == 1) {
                            this.setValue("1");
                        }
                        st(this, selectText);
                    },
                    commit: function(element) {
                        if (this.getValue())
                            element.data('cke-saved-IsDynamic', this.getValue());
                        else {
                            element.data('cke-saved-IsDynamic', false);
                            element.removeAttribute('IsDynamic');
                        }
                    }
                },  //静态动态选择框

					{
					id: "jintai",
					type: 'hbox',
					widths: ['50%', '50%'],
					children:
						[
							{
							    type: 'vbox',
							    children:
								[
									{
									    id: 'txtOptName',
									    type: 'text',
									    label: editor.lang.select.opText,
									    style: 'width:115px',
									    setup: function(name, element) {
									        if (name == 'clear')
									            this.setValue("");
									    }
									},
									{
									    type: 'select',
									    id: 'cmbName',
									    label: '',
									    title: '',
									    size: 5,
									    style: 'width:115px;height:75px',
									    items: [],
									    onChange: function() {
									        var dialog = this.getDialog(),
												values = dialog.getContentElement('info', 'cmbName'),
												optName = dialog.getContentElement('info', 'txtOptName'),
												optValue = dialog.getContentElement('info', 'txtOptName'),
												iIndex = getSelectedIndex(this);

									        setSelectedIndex(values, iIndex);
									        optName.setValue(this.getValue());
									        optValue.setValue(values.getValue());
									    },
									    setup: function(name, element) {
									        if (name == 'clear')
									            removeAllOptions(this);
									        else if (name == 'option')
									            addOption(this, element.getText(), element.getText(),
													this.getDialog().getParentEditor().document);
									    },
									    commit: function(element) {
									        var dialog = this.getDialog(),
												optionsNames = getOptions(this),
												optionsValues = getOptions(dialog.getContentElement('info', 'cmbName')),
												selectValue = dialog.getContentElement('info', 'txtOptName').getValue();

									        removeAllOptions(element);

									        for (var i = 0; i < optionsNames.count(); i++) {
									            var oOption = addOption(element, optionsNames.getItem(i).getValue(),
													optionsValues.getItem(i).getValue(), dialog.getParentEditor().document);
									            if (optionsValues.getItem(i).getValue() == selectValue) {
									                oOption.setAttribute('selected', 'selected');
									                oOption.selected = true;
									            }
									        }
									    }
									}
								]
							},
							{
							    type: 'vbox',
							    padding: 5,
							    children:
								[
									{
									    type: 'button',
									    id: 'btnAdd',
									    style: '',
									    label: editor.lang.select.btnAdd,
									    title: editor.lang.select.btnAdd,
									    style: 'width:100%;',
									    onClick: function() {
									        //Add new option.
									        var dialog = this.getDialog(),
												parentEditor = dialog.getParentEditor(),
												optName = dialog.getContentElement('info', 'txtOptName'),
												optValue = dialog.getContentElement('info', 'txtOptName'),
												names = dialog.getContentElement('info', 'cmbName'),
												values = dialog.getContentElement('info', 'cmbName');

									        //addOption(names, optName.getValue(), optName.getValue(), dialog.getParentEditor().document );
									        addOption(values, optValue.getValue(), optValue.getValue(), dialog.getParentEditor().document);

									        //	optName.setValue( "" );
									        optValue.setValue("");
									    }
									},
									{
									    type: 'button',
									    id: 'btnModify',
									    label: editor.lang.select.btnModify,
									    title: editor.lang.select.btnModify,
									    style: 'width:100%;',
									    onClick: function() {
									        //Modify selected option.
									        var dialog = this.getDialog(),
												optName = dialog.getContentElement('info', 'txtOptName'),
												optValue = dialog.getContentElement('info', 'txtOptName'),
												names = dialog.getContentElement('info', 'cmbName'),
												values = dialog.getContentElement('info', 'cmbName'),
												iIndex = getSelectedIndex(names);

									        if (iIndex >= 0) {
									            modifyOption(names, iIndex, optName.getValue(), optName.getValue());
									            modifyOption(values, iIndex, optValue.getValue(), optValue.getValue());
									        }
									    }
									},
									{
									    type: 'button',
									    id: 'btnUp',
									    style: 'width:100%;',
									    label: editor.lang.select.btnUp,
									    title: editor.lang.select.btnUp,
									    onClick: function() {
									        //Move up.
									        var dialog = this.getDialog(),
												names = dialog.getContentElement('info', 'cmbName'),
												values = dialog.getContentElement('info', 'cmbName');

									        changeOptionPosition(names, -1, dialog.getParentEditor().document);
									        changeOptionPosition(values, -1, dialog.getParentEditor().document);
									    }
									},
									{
									    type: 'button',
									    id: 'btnDown',
									    style: 'width:100%;',
									    label: editor.lang.select.btnDown,
									    title: editor.lang.select.btnDown,
									    onClick: function() {
									        //Move down.
									        var dialog = this.getDialog(),
												names = dialog.getContentElement('info', 'cmbName'),
												values = dialog.getContentElement('info', 'cmbName');

									        changeOptionPosition(names, 1, dialog.getParentEditor().document);
									        changeOptionPosition(values, 1, dialog.getParentEditor().document);
									    }
									},
                                    {
                                        type: 'button',
                                        id: 'btnDelete',
                                        label: editor.lang.select.btnDelete,
                                        title: editor.lang.select.btnDelete,
                                        onClick: function() {
                                            // Delete option.
                                            var dialog = this.getDialog(),
										names = dialog.getContentElement('info', 'cmbName'),
                                            //	values = dialog.getContentElement('info', 'cmbName'),
										optName = dialog.getContentElement('info', 'txtOptName');
                                            //	optValue = dialog.getContentElement('info', 'txtOptName');

                                            removeSelectedOptions(names);
                                            //  removeSelectedOptions(values);

                                            optName.setValue("");
                                            //optValue.setValue("");
                                        }
                                    }
								]
							}
						]
	},
                    {
                        id: "dym",
                        type: 'vbox',
                        widths: ['40%', '40%'],
                        onLoad: function() {
                            var thisDomId = this.domId;
                            $("#" + thisDomId + "").hide();

                        },
                        children: [
                           {
                               id: 'DataSetID',
                               type: 'text',
                               widths: ['25%', '75%'],
                               labelLayout: 'horizontal',
                               label: "数据集",
                               'default': '',
                               accessKey: 'V',
                               style: 'width:350px',
                               onClick: function() {
                                   $fieldDesc = this.getDialog().getContentElement("info", "DataSetID");
                                   $datasetID = this.getDialog().getContentElement("info", "DataSetIDValue");
                                   $TextColumn = this.getDialog().getContentElement("info", "TextColumn");
                                   $ValueColumn = this.getDialog().getContentElement("info", "ValueColumn");

                                   //$fieldDescID = fieldDesc.domId;
                                   //  $("#" + fieldDescID + "").parent().hide();   //隐藏父级td元素

                                   $.ligerDialog.open({ title: '选择数据集', name: 'winselector', width: 700, height: 400, isResize: true, url: '../demo/DataSet.htm', buttons: [
                        { text: '确定', onclick: f_selectContactOK },
                        { text: '取消', onclick: f_selectContactCancel }
                         ]
                                   });
                               },
                               setup: function(name, element) {
                                   if (name == 'clear')
                                       this.setValue(this['default'] || '');
                                   else if (name == 'select') {
                                       this.setValue(element.data('cke-saved-DataSetIDName') || element.getAttribute('DataSetIDName') || '');
                                   }
                               },
                               commit: function(element) {   //点击确认后增加相应的属性，名称为cke-saved- XXX
                                   // var element = data.element;

                                   if (this.getValue())
                                       element.data('cke-saved-DataSetIDName', this.getValue());
                                   else {
                                       element.data('cke-saved-DataSetIDName', false);
                                       element.removeAttribute('DataSetIDName');
                                   }
                               }
                           }, //描述
                           {
                           type: 'hbox',
                           widths: ['40%', '40%'],
                           children:
						[
                         {
                             id: 'ValueColumn',
                             type: 'select',
                             widths: ['25%', '75%'],
                             labelLayout: 'horizontal',
                             label: "值字段",
                             items: [""],
                             'default': '',
                             accessKey: 'V',
                             //    style: 'width:350px', 
                             setup: function(name, element) {
                                 if (name == 'clear')
                                     this.setValue(this['default'] || '');
                                 else if (name == 'select') {
                                     this.setValue(element.data('cke-saved-ValueColumn') || element.getAttribute('ValueColumn') || '');
                                 }
                             },
                             commit: function(element) {   //点击确认后增加相应的属性，名称为cke-saved- XXX
                                 // var element = data.element;

                                 if (this.getValue())
                                     element.data('cke-saved-ValueColumn', this.getValue());
                                 else {
                                     element.data('cke-saved-ValueColumn', false);
                                     element.removeAttribute('ValueColumn');
                                 }
                             }
                         },
                         {
                             id: 'TextColumn',
                             type: 'select',
                             widths: ['25%', '75%'],
                             labelLayout: 'horizontal',
                             label: "显示字段",
                             items: [""],
                             'default': '',
                             accessKey: 'V',
                             //    style: 'width:350px',
                             setup: function(name, element) {
                                 if (name == 'clear')
                                     this.setValue(this['default'] || '');
                                 else if (name == 'select') {
                                     this.setValue(element.data('cke-saved-TextColumn') || element.getAttribute('TextColumn') || '');
                                 }
                             },
                             commit: function(element) {   //点击确认后增加相应的属性，名称为cke-saved- XXX
                                 // var element = data.element;

                                 if (this.getValue())
                                     element.data('cke-saved-TextColumn', this.getValue());
                                 else {
                                     element.data('cke-saved-TextColumn', false);
                                     element.removeAttribute('TextColumn');
                                 }
                             }
                         },
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
                                     this.setValue(element.data('cke-saved-DataSetID') || element.getAttribute('DataSetID') || '');
                                 }
                             },
                             commit: function(element) {   //点击确认后增加相应的属性，名称为cke-saved- XXX
                                 // var element = data.element;

                                 if (this.getValue())
                                     element.data('cke-saved-DataSetID', this.getValue());
                                 else {
                                     element.data('cke-saved-DataSetID', false);
                                     element.removeAttribute('DataSetID');
                                 }
                             }
                         }
                        ]
                       }
                       ]
                    },  //动态内容
                    {
                    type: 'select',
                    id: 'cssStyleClass',
                    label: "样式",
                    items: cssStyleClass,
                    //    required: true,
                    setup: function(name, element) {   //双击已增加控件回传值
                        if (name == 'clear')
                            this.setValue(this['default'] || '');
                        else if (name == 'select') {
                            this.setValue(element.data('cke-saved-cssStyleClass') || element.getAttribute('cssStyleClass') || '');
                        }
                    },
                    commit: function(element) {   //点击确认后增加相应的属性，名称为cke-saved- XXX

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

function f_selectContact() {
    $.ligerDialog.success('提示内容');
    //        $.ligerDialog.open({ title: '选择联系人', name: 'winselector', width: 700, height: 300, url: 'SelectContact.htm', buttons: [
    //                { text: '确定', onclick: f_selectContactOK },
    //                { text: '取消', onclick: f_selectContactCancel }
    //            ]
    //        });
    return false;
}

function f_selectContactOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('请选择行!');
        return;
    }

    $fieldDesc.setValue(data.DataSetName);  //
    $datasetID.setValue(data.DataSetID);
    $TextColumn.clear();
    $ValueColumn.clear();
    var r = data.ReturnColumns.split(",");
    for (var i = 0; i < r.length; i++) {
        $TextColumn.add(r[i]);
        $ValueColumn.add(r[i]);
    }

    dialog.close();

}

function f_selectContactCancel(item, dialog) {
    dialog.close();
}

function st(obj,selectText) {
    var state = obj.getDialog().getContentElement("info", "IsDynamic");
    var dym = obj.getDialog().getContentElement("info", "dym");
    var jintai = obj.getDialog().getContentElement("info", "jintai");
    //var selectText = state.getValue();
    var jintaiHideID = jintai.domId;
    var dymHideID = dym.domId;

    switch (selectText) {
        case "0":
            $("#" + dymHideID + "").hide();
            $("#" + jintaiHideID + "").show();
            break;
        case "1":
            $("#" + jintaiHideID + "").hide();
            $("#" + dymHideID + "").show();
            break;
    }
}
