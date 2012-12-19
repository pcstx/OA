CKEDITOR.plugins.add('detailGroup',   //编辑器追加控件方法
{
init: function(editor) {    //初始化方法
    var pluginName = 'detailGroup';   //控件名称
    CKEDITOR.dialog.add(pluginName, this.path + 'dialogs/detailGroup.js');  //打开对话框路径
  editor.addCommand(pluginName, new CKEDITOR.dialogCommand(pluginName));       //增加命令
    //  editor.addCommand(pluginName,alert(1));
    editor.ui.addButton('detailGroup',     //在编辑器上面增加按钮
            {
            label: '明细组',
            command: pluginName,      //命令
            //icon: CKEDITOR.plugins.getPath('label') + 'hello.png'  //图标
            icon: this.path + '../../skins/pluginsImages/detailGroup.png'
        });

//    editor.on('click', function(evt) {
//        alert(1);
//    });

    editor.on('doubleclick', function(evt) {
   //     alert(20);
                var element = evt.data.element;

               if (element.is('input') && element.data('cke-real-element-type') == 'file')
                   evt.data.dialog = 'file';
    });


}

});