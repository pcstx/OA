
CKEDITOR.dialog.add('detailGroup', function(editor) {
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
        title: "明细组",
        minWidth: CKEDITOR.env.ie ? 460 : 395,
        minHeight: CKEDITOR.env.ie ? 320 : 300,
        contents: [
			{
			    id: 'info',
			    label: editor.lang.select.selectInfo,
			    title: editor.lang.select.selectInfo,
			    accessKey: '',
			    elements: [
					{
					    type: 'hbox',
					    widths: ['50%', '50%'],
					    children: [{
					        type: 'html',
					        html: '<span>' + CKEDITOR.tools.htmlEncode("明细组输入") + '</span>'
					    },
                        {
                            type: 'html',
                            html: '<span>' + CKEDITOR.tools.htmlEncode("明细字段输入") + '</span>'
                        }
                        ]
					},
					{
					    type: 'hbox',
					    widths: ['25%', '25%', '25%', '25%'],
					    children:
						[
							{
							    type: 'vbox',
							    children:
								[
									{
									    id: 'groupName',
									    type: 'text',
									    label: '名称',
									    style: 'width:115px',
									    setup: function(name, element) {
									        if (name == 'clear')
									            this.setValue("");
									    }
									},  //名称
                                    {
                                    id: 'groupDesc',
                                    type: 'text',
                                    label: '描述',
                                    style: 'width:115px',
                                    setup: function(name, element) {
                                        if (name == 'clear')
                                            this.setValue("");
                                    }
                                },  //描述
									{
									type: 'select',
									id: 'cmbName',
									label: '',
									title: '',
									size: 5,
									style: 'width:115px',
									items: [],
									onLoad: function() {
									    var dialog = this.getDialog(),
											parentEditor = dialog.getParentEditor(),
											groupName = dialog.getContentElement('info', 'groupName'),
											groupDesc = dialog.getContentElement('info', 'groupDesc'),
									    	names = dialog.getContentElement('info', 'cmbName');

									    for (var i = 0; i < GPRP.json.items.length; i++) {
									        addOption(names, groupName.getValue(), groupDesc.getValue(), dialog.getParentEditor().document);
									    }
									},
									onClick: function() {
									    var dialog = this.getDialog(),
											 	values = dialog.getContentElement('info', 'cmbName'),   //当前控件
												optName = dialog.getContentElement('info', 'groupName'),    //名称
												optValue = dialog.getContentElement('info', 'groupDesc'),       //描述
                                                groupValue = dialog.getContentElement('info', 'cmbValue'),  //要增加内容 
									    iIndex = getSelectedIndex(this);

									    if (this.getInputElement().$.selectedIndex >= 0) {
									        setSelectedIndex(values, iIndex);
									        optName.setValue(this.getInputElement().$[this.getInputElement().$.selectedIndex].text);
									        optValue.setValue(this.getValue());

									        groupValue.clear();

									        for (var i = 0; i < GPRP.json.items[iIndex].groups.length; i++) {
									            addOption(groupValue, GPRP.json.items[iIndex].groups[i].fieldDesc, GPRP.json.items[iIndex].groups[i].fieldID, dialog.getParentEditor().document);
									        }
									    }
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
												optionsValues = getOptions(dialog.getContentElement('info', 'cmbValue')),
												selectValue = dialog.getContentElement('info', 'txtValue').getValue();

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
}  //明细组

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
												groupName = dialog.getContentElement('info', 'groupName'),
												groupDesc = dialog.getContentElement('info', 'groupDesc'),
										    	names = dialog.getContentElement('info', 'cmbName');

									        if (GPRP.validateGroup(groupName.getValue())) {  //判断名称
									            addOption(names, groupName.getValue(), groupDesc.getValue(), dialog.getParentEditor().document);
									            var newNode = { "groupName": groupName.getValue(), "groupDesc": groupDesc.getValue(), "groups": [] };
									            GPRP.json.items.push(newNode);

									            groupName.setValue("");
									            groupDesc.setValue("");
									        }
									    }
									},  //新增
									{
									type: 'button',
									id: 'btnModify',
									label: editor.lang.select.btnModify,
									title: editor.lang.select.btnModify,
									style: 'width:100%;',
									onClick: function() {
									    //Modify selected option.
									    var dialog = this.getDialog(),
												optName = dialog.getContentElement('info', 'groupName'),
												optValue = dialog.getContentElement('info', 'groupDesc'),
												names = dialog.getContentElement('info', 'cmbName'),
												iIndex = getSelectedIndex(names);

									    if (GPRP.validateGroup(optName.getValue(), iIndex)) {  //判断名称
									        if (iIndex >= 0) {
									            modifyOption(names, iIndex, optName.getValue(), optValue.getValue());
									            GPRP.json.items[iIndex].groupName = optName.getValue();
									            GPRP.json.items[iIndex].groupDesc = optValue.getValue();
									        }
									    }
									}
					},  //修改
                                    {
                                    type: 'button',
                                    id: 'btnDelete',
                                    label: editor.lang.select.btnDelete,
                                    title: editor.lang.select.btnDelete,
                                    onClick: function() {
                                        // Delete option.
                                        var dialog = this.getDialog(),
										names = dialog.getContentElement('info', 'cmbName'),
                                        values = dialog.getContentElement('info', 'cmbValue'),
										optName = dialog.getContentElement('info', 'groupName'),
										optValue = dialog.getContentElement('info', 'groupDesc'),
                                        iIndex = getSelectedIndex(names);

                                        removeSelectedOptions(names);

                                        GPRP.json.items.splice(iIndex, 1);
                                        optName.setValue("");
                                        optValue.setValue("");
                                        values.clear();
                                    }
                                },  //删除
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
												values = dialog.getContentElement('info', 'groupDesc'),
												iIndex = getSelectedIndex(names);

									    changeOptionPosition(names, -1, dialog.getParentEditor().document);
									    if (iIndex > 0) {
									        var tmp = GPRP.json.items[iIndex - 1];
									        GPRP.json.items[iIndex - 1] = GPRP.json.items[iIndex];
									        GPRP.json.items[iIndex] = tmp;
									    }


									}
					},  //上升
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
												values = dialog.getContentElement('info', 'groupDesc'),
												iIndex = getSelectedIndex(names);

									    changeOptionPosition(names, 1, dialog.getParentEditor().document);
									    if (iIndex < GPRP.json.items.length - 1) {
									        var tmp = GPRP.json.items[iIndex + 1];
									        GPRP.json.items[iIndex + 1] = GPRP.json.items[iIndex];
									        GPRP.json.items[iIndex] = tmp;
									    }
									}
}   //下降
								]
                            },  //添加修改
							{
							type: 'vbox',
							children:
								[
									{
									    type: 'select',
									    id: 'cmbValue',
									    label: '',
									    size: 5,
									    style: 'width:115px;height:150px',
									    items: [],
									    setup: function(name, element) {
									        if (name == 'clear')
									            this.setValue("");
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
									    id: 'btnGroupAdd',
									    style: '',
									    label: editor.lang.select.btnAdd,
									    title: editor.lang.select.btnAdd,
									    style: 'width:100%;',
									    onClick: function() {
									        //////////判断是否有选中项
									        var dialog = this.getDialog(),
											 	values = dialog.getContentElement('info', 'cmbName'),
												optName = dialog.getContentElement('info', 'groupName'),
												optValue = dialog.getContentElement('info', 'groupDesc'),
												iIndex = getSelectedIndex(values);

									        $fieldDesc = this.getDialog().getContentElement("info", "cmbValue");
									        // $fieldDesc = this.getDialog().getContentElement("info", "cmbValue");
									        $diaglog = this.getDialog().getParentEditor().document;
									        $iIndex = iIndex;

									        if (iIndex >= 0) {
									            $.ligerDialog.open({ title: '选择明细字段', name: 'winselector', width: 700, height: 450, isResize: true, url: '../demo/FormField.htm', buttons: [
                        { text: '确定', onclick: f_selectContactOK },
                        { text: '取消', onclick: f_selectContactCancel }
                         ]
									            });
									        }
									        else {
									            GPRP.warn("请先选中组");
									        }
									    }
									},
                                    {
                                        type: 'button',
                                        id: 'btnGroupDelete',
                                        label: editor.lang.select.btnDelete,
                                        title: editor.lang.select.btnDelete,
                                        onClick: function() {
                                            // Delete option.
                                            var dialog = this.getDialog(),
                                           	names = dialog.getContentElement('info', 'cmbName'),
										    values = dialog.getContentElement('info', 'cmbValue'),
                                            iIndex = getSelectedIndex(values),
                                            groupIndex = getSelectedIndex(names);

                                            if (groupIndex >= 0) {
                                                removeSelectedOptions(values);

                                                GPRP.json.items[groupIndex].groups.splice(iIndex, 1);
                                            }
                                        }
                                    },  //删除
									{
									type: 'button',
									id: 'btnGroupUp',
									style: 'width:100%;',
									label: editor.lang.select.btnUp,
									title: editor.lang.select.btnUp,
									onClick: function() {
									    //Move up.
									    var dialog = this.getDialog(),
												names = dialog.getContentElement('info', 'cmbName'),
												values = dialog.getContentElement('info', 'cmbValue'),
                                                iIndex = getSelectedIndex(values),
                                                groupIndex = getSelectedIndex(names);

									    if (groupIndex >= 0) {
									        changeOptionPosition(values, -1, dialog.getParentEditor().document);
									        if (iIndex > 0) {
									            var tmp = GPRP.json.items[groupIndex].groups[iIndex - 1];
									            GPRP.json.items[groupIndex].groups[iIndex - 1] = GPRP.json.items[groupIndex].groups[iIndex];
									            GPRP.json.items[groupIndex].groups[iIndex] = tmp;
									        }
									    }
									}
					},  //上升
									{
									type: 'button',
									id: 'btnGroupDown',
									style: 'width:100%;',
									label: editor.lang.select.btnDown,
									title: editor.lang.select.btnDown,
									onClick: function() {
									    //Move down.
									    var dialog = this.getDialog(),
												names = dialog.getContentElement('info', 'cmbName'),
												values = dialog.getContentElement('info', 'cmbValue'),
                                                iIndex = getSelectedIndex(values),
                                                groupIndex = getSelectedIndex(names);

									    if (groupIndex >= 0) {
									        //   changeOptionPosition(names, 1, dialog.getParentEditor().document);
									        changeOptionPosition(values, 1, dialog.getParentEditor().document);
									        if (iIndex < GPRP.json.items[groupIndex].groups.length - 1) {
									            var tmp = GPRP.json.items[groupIndex].groups[iIndex + 1];
									            GPRP.json.items[groupIndex].groups[iIndex + 1] = GPRP.json.items[groupIndex].groups[iIndex];
									            GPRP.json.items[groupIndex].groups[iIndex] = tmp;
									        }
									    }
									}
}  //下降
								]
}  //添加修改
						]
					}
				]
			}
		]
    };
});

function f_selectContactOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        GPRP.warn('请选择行!');
        return;
    }

    for (var i = 0; i < data.length; i++) {
        var iflag = 0;
        for(var j=0;j<GPRP.json.items[$iIndex].groups.length;j++) {
            if (GPRP.json.items[$iIndex].groups[j].fieldID == data[i].FieldID) {
                iflag = 1;
                break;
            } 
        }

        if (iflag == 0) {
            GPRP.addOption($fieldDesc, data[i].FieldDesc, data[i].FieldID, $diaglog);
            var newNode = { "fieldDesc": data[i].FieldDesc, "fieldID": data[i].FieldID };
            GPRP.json.items[$iIndex].groups.push(newNode);       
        }     
    }

    dialog.close(); 
}

function f_selectContactCancel(item, dialog) {
    dialog.close();
}