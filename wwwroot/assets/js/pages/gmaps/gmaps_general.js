var latlng_1 = new google.maps.LatLng(30.31041217, 59.9664191);
 var latlng_2 = new google.maps.LatLng(30.355454417, 59.9466185);
 var latlng_3 = new google.maps.LatLng(29.78174317, 59.9860281);
 var latlng_4 = new google.maps.LatLng(29.729560317, 60.0088431);
 var latlng_5 = new google.maps.LatLng(30.349298617, 60.0532688);
 var latlng_6 = new google.maps.LatLng(30.313951217, 59.9875627);
 var latlng_7 = new google.maps.LatLng(30.275973317, 59.9099365);
 var latlng_8 = new google.maps.LatLng(30.238050917, 60.0161779);
 var latlng_9 = new google.maps.LatLng(30.371511617, 59.9368308);
 var latlng_10 = new google.maps.LatLng(30.374507617, 59.9958319);
 var latlng_11 = new google.maps.LatLng(30.275315317, 59.9033348);
 var latlng_12 = new google.maps.LatLng(30.09066691, 59.8072308);
 var latlng_13 = new google.maps.LatLng(30.37417417, 59.8673695);
 var latlng_14 = new google.maps.LatLng(30.251916317, 60.0094267);
 var latlng_15 = new google.maps.LatLng(30.475310317, 59.8999617);
 var latlng_16 = new google.maps.LatLng(30.156879317, 59.8258677);
 var latlng_17 = new google.maps.LatLng(30.2795917, 59.9246578);
 var latlng_18 = new google.maps.LatLng(30.420889317, 60.0262847);
 var latlng_19 = new google.maps.LatLng(30.178311117, 59.8352942);
 var latlng_20 = new google.maps.LatLng(30.298872617, 59.9906439);
 var latlng_21 = new google.maps.LatLng(30.33432117, 59.9139178);
 var latlng_22 = new google.maps.LatLng(30.347958117, 59.9136858);
 var latlng_23 = new google.maps.LatLng(30.286171717, 59.9558782);
 var latlng_24 = new google.maps.LatLng(30.285319517, 59.9587466);
 var latlng_25 = new google.maps.LatLng(30.350730317, 59.9577177);
 var latlng_26 = new google.maps.LatLng(30.375577217, 59.9365938);
 var latlng_27 = new google.maps.LatLng(30.311406417, 59.9271694);
 var latlng_28 = new google.maps.LatLng(30.335838317, 59.9213079);
 var latlng_29 = new google.maps.LatLng(30.377020317, 59.9960627);

var place_1="Большой пр-т П.С. 98 лит. А, кв 25";
var place_2="Чайковского д.36, лит. А, кв. 4";
var place_3="Петровская улица, дом 5, лит И";
var place_4="Кронштадское шоссе, дом 28, лит Б.";
var place_5="Санкт-Петербург, Сиреневый бульвар, дом 7, корпус 1, литера А, кв. 4";
var place_6="Белоостровская улица, дом 25, литера А, кв. 3";
var place_7="набережная Обводного канала, дом 211-213, литера А, кв. 3";
var place_8="Санкт-Петербург, Авиаконструкторов, д. 20, к. 2, лит. А, кв. 1";
var place_9="Старорусская д. 16\8-я Советская, д. 57, литера А, кв. 19";
var place_10="Санкт-Петербург, Проспект Непокорённых, дом 9, корпус 1, литера А, кв. 21";
var place_11="Старо-Петергофский проспект, дом 42, литера А, кв. 23";
var place_12="Первомайская улица, дом 26, литера А, кв. 3";
var place_13="ул. Турку д. 9, к.4, кв. 73";
var place_14="Комендантский проспект, дом 16, корпус 2, кв. 1";
var place_15="ул. Шотмана д.5, к.1, кв. 180";
var place_16="Проспект Народного Ополчения, д. 231, кв. 23";
var place_17="Набережная реки Пряжки, дом 18-20, литера А, кв. 9";
var place_18="Черкасова д.11 кор.1";
var place_19="Авангардная 23, кв. 19";
var place_20="Набережная Черной речки 59, кв. 1";
var place_21="Набережная Обводного канала д. 66, лит. А, кв.4";
var place_22="Лиговский проспект д. 142 лит. А, кв. 8";
var place_23="Офицерский переулок, д.8б, лит. В. , кв. 5";
var place_24="ул. Пионерская, д. 37, литера А, кв. 2";
var place_25="Академика Лебедева, д. 21, лит А, кв. 2";
var place_26="9-я Советская улица, дом 22, кв. 32";
var place_27="Казначейская ул. д.5, кв.9";
var place_28="Звенигородская улица, 12/17, кв.9";
var place_29="пр-т Непокорённых, д. 13, к.1, кв. 60";


        var myOptions =
            {
            zoom:4,
            center:latlng_1,
            mapTypeId: google.maps.MapTypeId.ROADMAP
            };

        var map= new google.maps.Map(document.getElementById('maps'),myOptions);

        var marker1 = new google.maps.Marker({
            position: latlng_1,
            title: place_1,
            clickable: true, 
            map: map
        });

       var marker2 = new google.maps.Marker({
            position: latlng_2,
            title: place_2,
            clickable: true, 
            map: map
        });
		
		var marker3 = new google.maps.Marker({
            position: latlng_3,
            title: place_3,
            clickable: true, 
            map: map
        });
		
		var marker4 = new google.maps.Marker({
            position: latlng_4,
            title: place_4,
            clickable: true, 
            map: map
        });
		
		var marker5 = new google.maps.Marker({
            position: latlng_5,
            title: place_5,
            clickable: true, 
            map: map
        });
		
		var marker6 = new google.maps.Marker({
            position: latlng_6,
            title: place_6,
            clickable: true, 
            map: map
        });
		
		var marker7 = new google.maps.Marker({
            position: latlng_7,
            title: place_7,
            clickable: true, 
            map: map
        });
		
		var marker8 = new google.maps.Marker({
            position: latlng_8,
            title: place_8,
            clickable: true, 
            map: map
        });
		
		var marker9 = new google.maps.Marker({
            position: latlng_9,
            title: place_9,
            clickable: true, 
            map: map
        });
		
		var marker10 = new google.maps.Marker({
            position: latlng_10,
            title: place_10,
            clickable: true, 
            map: map
        });
		
		var marker11 = new google.maps.Marker({
            position: latlng_11,
            title: place_11,
            clickable: true, 
            map: map
        });
		
		
	var marker12 = new google.maps.Marker({
            position: latlng_12,
            title: place_12,
            clickable: true, 
            map: map
        });
		
		var marker13 = new google.maps.Marker({
            position: latlng_13,
            title: place_13,
            clickable: true, 
            map: map
        });
		
		var marker14 = new google.maps.Marker({
            position: latlng_14,
            title: place_14,
            clickable: true, 
            map: map
        });
		
		var marker15 = new google.maps.Marker({
            position: latlng_15,
            title: place_15,
            clickable: true, 
            map: map
        });
		
		var marker16 = new google.maps.Marker({
            position: latlng_16,
            title: place_16,
            clickable: true, 
            map: map
        });
		
		
		var marker17 = new google.maps.Marker({
            position: latlng_17,
            title: place_17,
            clickable: true, 
            map: map
        });
		
		var marker18 = new google.maps.Marker({
            position: latlng_18,
            title: place_18,
            clickable: true, 
            map: map
        });
		
		var marker19 = new google.maps.Marker({
            position: latlng_19,
            title: place_19,
            clickable: true, 
            map: map
        });
		
		var marker20 = new google.maps.Marker({
            position: latlng_20,
            title: place_20,
            clickable: true, 
            map: map
        });
		
		var marker21 = new google.maps.Marker({
            position: latlng_21,
            title: place_21,
            clickable: true, 
            map: map
        });
		
		var marker22 = new google.maps.Marker({
            position: latlng_22,
            title: place_22,
            clickable: true, 
            map: map
        });
		
		var marker23 = new google.maps.Marker({
            position: latlng_23,
            title: place_23,
            clickable: true, 
            map: map
        });
		
		var marker24 = new google.maps.Marker({
            position: latlng_24,
            title: place_24,
            clickable: true, 
            map: map
        });
		
		var marker25 = new google.maps.Marker({
            position: latlng_25,
            title: place_25,
            clickable: true, 
            map: map
        });
		
		var marker26 = new google.maps.Marker({
            position: latlng_26,
            title: place_26,
            clickable: true, 
            map: map
        });
		
		var marker27 = new google.maps.Marker({
            position: latlng_27,
            title: place_27,
            clickable: true, 
            map: map
        });
		
		var marker28 = new google.maps.Marker({
            position: latlng_28,
            title: place_28,
            clickable: true, 
            map: map
        });
		
		var marker29 = new google.maps.Marker({
            position: latlng_29,
            title: place_29,
            clickable: true, 
            map: map
        });
		
