CKEDITOR.plugins.add('browsebutton',   //编辑器追加控件方法
{
init: function(editor) {    //初始化方法
    var pluginName = 'browsebutton';   //控件名称
    CKEDITOR.dialog.add(pluginName, this.path + 'dialogs/browsebutton.js');  //打开对话框路径
    editor.addCommand(pluginName, new CKEDITOR.dialogCommand(pluginName));       //增加命令
    editor.ui.addButton('browsebutton',     //在编辑器上面增加按钮
            {
            label: '上传附件',
            command: pluginName,      //命令
            //icon: CKEDITOR.plugins.getPath('label') + 'hello.png'  //图标
            icon: this.path + '../../skins/pluginsImages/bb.png'
        });

    editor.on('doubleclick', function(evt) {
        var element = evt.data.element;

        if (element.is('input') && element.data('cke-real-element-type') == 'file')
            evt.data.dialog = 'file';
    });


}

});