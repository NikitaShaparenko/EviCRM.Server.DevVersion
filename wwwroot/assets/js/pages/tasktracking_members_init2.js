$(document).ready(function () {
    "use strict";
    tinymce.init({
        language: "ru",
        selector: ".taskdesc-editor-class",
        height: 350,
        plugins: ["advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker", "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking", "save table contextmenu directionality emoticons template paste textcolor"],
        toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | l      ink image | print preview media fullpage | forecolor backcolor emoticons",
        style_formats: [{
            title: "Жирный текст",
            inline: "b"
        }, {
            title: "Красный текст",
            inline: "span",
            styles: {
                color: "#ff0000"
            }
        }, {
            title: "Красный заголовок",
            block: "h1",
            styles: {
                color: "#ff0000"
            }
        }, {
            title: "Шаблон 1",
            inline: "span",
            classes: "example1"
        }, {
            title: "Шаблон 2",
            inline: "span",
            classes: "example2"
        }, {
            title: "Стили таблицы"
        }, {
            title: "Столбец таблицы 1",
            selector: "tr",
            classes: "tablerow1"
        }]
    })});
	$(document).ready(function () {
    "use strict";
    tinymce.init({
        language: "ru",
        selector: ".taskdesc-editor-class-small",
        height: 100,
        plugins: ["advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker", "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking", "save table contextmenu directionality emoticons template paste textcolor"],
        toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | l      ink image | print preview media fullpage | forecolor backcolor emoticons",
        style_formats: [{
            title: "Жирный текст",
            inline: "b"
        }, {
            title: "Красный текст",
            inline: "span",
            styles: {
                color: "#ff0000"
            }
        }, {
            title: "Красный заголовок",
            block: "h1",
            styles: {
                color: "#ff0000"
            }
        }, {
            title: "Шаблон 1",
            inline: "span",
            classes: "example1"
        }, {
            title: "Шаблон 2",
            inline: "span",
            classes: "example2"
        }, {
            title: "Стили таблицы"
        }, {
            title: "Столбец таблицы 1",
            selector: "tr",
            classes: "tablerow1"
        }]
    })});