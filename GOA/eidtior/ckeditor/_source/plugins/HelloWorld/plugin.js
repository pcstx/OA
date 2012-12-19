CKEDITOR.plugins.add('HelloWorld', {
    init: function (editor) {
        var pluginName = 'HelloWorld';
        CKEDITOR.dialog.add(pluginName, this.path + 'dialogs/HelloWorld.js');
        editor.addCommand(pluginName, new CKEDITOR.dialogCommand(pluginName));
        editor.ui.addButton(pluginName,
        {
            label: '附件上传',
            command: pluginName,
            icon: this.path + 'images/hello.png'
        });

        editor.addCss(
				'img.hello' +
				'{' +
					'background-image: url(' + CKEDITOR.getUrl( this.path + 'images/placeholder.png' ) + ');' +
					'background-position: center center;' +
					'background-repeat: no-repeat;' +
					'border: 1px solid #a9a9a9;' +
					'width: 80px;' +
					'height: 80px;' +
				'}'
				);

        editor.on( 'doubleclick', function( evt )
			{
				var element = evt.data.element;

				if (element.is('img') && element.data('cke-real-element-type') == 'file')
				    evt.data.dialog = 'HelloWorld';   
			});

      },
    afterInit: function (editor) {
    var dataProcessor = editor.dataProcessor,
			htmlFilter = dataProcessor && dataProcessor.htmlFilter,
			dataFilter = dataProcessor && dataProcessor.dataFilter;

    // Cleanup certain IE form elements default values.
    if (CKEDITOR.env.ie) {
        htmlFilter && htmlFilter.addRules(
			{
			    elements:
				{
				    input: function (input) {
				        var attrs = input.attributes,
							type = attrs.type;
				        // Old IEs don't provide type for Text inputs #5522
				        if (!type)
				            attrs.type = 'file';
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
				    input: function (element) {
				        if (element.attributes.type == 'file') 
				        return editor.createFakeParserElement(element, 'hello', 'file');
				    }
				}
			});
    }
}

   

});