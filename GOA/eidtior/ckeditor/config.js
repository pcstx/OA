/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function(config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    // config.extraPlugins = 'HelloWorld';

    config.toolbar_Mine =
                [
                 { name: 'edit', items: ['Form', 'label', 'TextField', 'Textarea', 'Checkbox', 'Select', 'update', 'browsebutton', 'groupLine', 'detailGroup'/*, '-', 'CreateDiv', 'Radio', 'HiddenField', 'Table', 'Button'*/] },
                //  '/', 
                    { name: 'clipboard', items: ['Cut', 'Copy', 'Paste',  'SelectAll', '-', 'Undo', 'Redo'] },
                   { name: 'tools', items: ['Maximize', 'ShowBlocks'] }, 
              //      {name: 'extent', items: ['Source', '-', 'HelloWorld', 'label'] }
                ];
    config.toolbar = 'Mine';
  // config.extraPlugins += (config.extraPlugins ? ',HelloWorld' : 'HelloWorld');
    config.extraPlugins += (config.extraPlugins ? ',label' : 'label');
    config.extraPlugins += (config.extraPlugins ? ',browsebutton' : 'browsebutton');
    config.extraPlugins += (config.extraPlugins ? ',groupLine' : 'groupLine');
    config.extraPlugins += (config.extraPlugins ? ',detailGroup' : 'detailGroup'); 
    config.extraPlugins+=(config.extraPlugins?',update':'update');
};
