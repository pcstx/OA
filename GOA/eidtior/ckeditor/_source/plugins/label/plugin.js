CKEDITOR.plugins.add('label',   //编辑器追加控件方法
{
    init: function(editor) {    //初始化方法
    var pluginName = 'label';   //控件名称
    CKEDITOR.dialog.add(pluginName, this.path + 'dialogs/label.js');  //打开对话框路径
        editor.addCommand(pluginName, new CKEDITOR.dialogCommand(pluginName));       //增加命令
        editor.ui.addButton('label',     //在编辑器上面增加按钮
            {
                label: '标签',
                command: pluginName,      //命令
                //icon: CKEDITOR.plugins.getPath('label') + 'hello.png'  //图标
                icon: this.path + 'images/hello.png'
            });

            editor.on('doubleclick', function(evt) {
                var element = evt.data.element;

                if (element.is('img') && element.data('cke-real-element-type') == 'label')
                    evt.data.dialog = 'label';
            });
 
 
    }
    
});