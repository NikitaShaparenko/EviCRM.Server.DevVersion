var date = new Date();
    date.setDate(date.getDate() - 1);// За день до текущей даты
    var picker1 = $('#datetimepicker1').datetimepicker({
        format: 'yyyy-mm',
        weekStart: 1,// Начало в понедельник
        minView : 2,// Отображение месяца и дня
        autoclose:true,// Автоматически закрывается после выбора
        locale:'ru',// Язык китайский, представьте bootstrap-datetimepicker.zh-CN.js
        todayHighlight:true,// Выделите текущую дату
        startDate: date,// Устанавливаем минимальное время
        / * bootstrap datetimepicker имеет несколько версий, и названия некоторых свойств разные. Например, я использовал предыдущую версию и установил для атрибута времени начала значение minDate, а время окончания - maxDate. Также существует версия времени начала: startDate, время окончания endDate. Большинство онлайн-загрузок должно быть последним. * /
    });
    var picker2 = $('#datetimepicker2').datetimepicker({
        format: 'yyyy-mm-dd',
        weekStart: 1,
        minView : 2,
        autoclose:true,
       locale:'ru',
        todayHighlight:true,
        startDate: date,
    });
    picker1.on('changeDate', function (e) {
        var date = new Date(e.date);
        date.setDate(date.getDate() + 1);
        $('#datetimepicker2').datetimepicker('setStartDate',date);// Динамическая установка минимального значения datetimepicker2, которое не может быть меньше или равно datetimepicker1
    });
    picker2.on('changeDate', function (e) {
        var date = new Date(e.date);
        date.setDate(date.getDate() - 1);
        $('#datetimepicker1').datetimepicker('setEndDate',date);// Динамическая установка максимального значения datetimepicker1, которое не может быть больше или равно datetimepicker2
    });