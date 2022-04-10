/**
 * @license Copyright (c) 2003-2022, CKSource Holding sp. z o.o. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

//CKEDITOR.editorConfig = function( config ) {
//	// Define changes to default configuration here. For example:
//	// config.language = 'fr';
//	// config.uiColor = '#AADC6E';
//};

CKEDITOR.editorConfig = function (config) {
    config.toolbarGroups = [
        { name: 'document', groups: ['mode', 'document', 'doctools'] },
        { name: 'clipboard', groups: ['clipboard', 'undo'] },
        { name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
        { name: 'forms', groups: ['forms'] },
        { name: 'links', groups: ['links'] },
        { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
        { name: 'insert', groups: ['insert'] },
        { name: 'tools', groups: ['tools'] },
        '/',
        { name: 'styles', groups: ['styles'] },
        { name: 'colors', groups: ['colors'] },
        { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
        { name: 'others', groups: ['others'] },
        { name: 'about', groups: ['about'] }
    ];

    config.removeButtons = 'Anchor,About';

    config.filebrowserBrowseUrl = '/Assets/admin/libs/ckfinder/ckfinder.html',
    config.filebrowserUploadUrl = '/Assets/admin/libs/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files',
    config.filebrowserWindowWidth = '1000',
        config.filebrowserWindowHeight = '1000'

    config.height = 600;        // 500 pixels high.
    //config.height = '25em';     // CSS unit (em).
};