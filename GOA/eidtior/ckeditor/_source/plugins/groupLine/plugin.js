CKEDITOR.plugins.add('groupLine',   //编辑器追加控件方法
{
init: function(editor) {    //初始化方法
    var pluginName = 'groupLine';   //控件名称
    CKEDITOR.dialog.add(pluginName, this.path + 'dialogs/groupLine.js');  //打开对话框路径
    editor.addCommand(pluginName, new CKEDITOR.dialogCommand(pluginName));       //增加命令
    editor.ui.addButton('groupLine',     //在编辑器上面增加按钮
            {
            label: '明细行选择',
            command: pluginName,      //命令
            //icon: CKEDITOR.plugins.getPath('label') + 'hello.png'  //图标
            icon: this.path + '../../skins/pluginsImages/groupLine.png'
        });

    editor.on('doubleclick', function(evt) {
        var element = evt.data.element;

        if (element.is('img') && element.data('cke-real-element-type') == 'groupLine')
            evt.data.dialog = 'groupLine';
    });

    // If the "contextmenu" plugin is loaded, register the listeners.
    if (editor.contextMenu) {
        editor.contextMenu.addListener(function(element, selection) {
            if (element && element.is('img') && !element.isReadOnly()
								&& element.data('cke-real-element-type') == 'groupLine')
                return { flash: CKEDITOR.TRISTATE_OFF };
        });
    }

}

});