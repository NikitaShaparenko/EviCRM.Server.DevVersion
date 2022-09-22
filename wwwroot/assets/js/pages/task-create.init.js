$(document).ready(function () {
    "use strict";
    0 < $("#taskdesc-editor").length && tinymce.init({
       
        language: "ru",
        selector: "textarea#taskdesc-editor",
        height: 200,
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