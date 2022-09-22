var mymap = L.map("leaflet-map").setView([59.9342802, 30.3350986], 13);
L.tileLayer("https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibmlraXRhNHNoYXAiLCJhIjoiY2t3ajh0NHkwMWczajJ2cXZvaDBxeDRwMSJ9.QglDuy87U_JPFa6Rd4bsfg", {
	maxZoom: 18,
	attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
	id: "mapbox/streets-v11",
	tileSize: 512,
	zoomOffset: -1
}).addTo(mymap);
var markermap = L.map("leaflet-map-marker").setView([59.9342802, 30.3350986], 8);
L.tileLayer("https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibmlraXRhNHNoYXAiLCJhIjoiY2t3ajh0NHkwMWczajJ2cXZvaDBxeDRwMSJ9.QglDuy87U_JPFa6Rd4bsfg", {
	maxZoom: 18,
	attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
	id: "mapbox/streets-v11",
	tileSize: 512,
	zoomOffset: -1
}).addTo(markermap), L.marker([59.9342802, 30.3350986]).addTo(markermap), L.circle([59.9342802, 30.3350986], {
	color: "#34c38f",
	fillColor: "#34c38f",
	fillOpacity: .5,
	radius: 500
}).addTo(markermap), L.polyline([
[60.095, 30.255],
[60.0423, 30.0215],
[60.0255, 29.8334],
[60.0172, 29.7263],
[59.9307, 29.6706],
[59.8718, 29.7372],
[59.8194, 29.8389],
[59.8211, 29.9652],
[59.7983, 30.1602],
[59.8328, 30.2804],
[59.809, 30.3353],
[59.8207,30.4218],
[59.8535, 30.5015],
[59.8941, 30.5262],
[59.9482, 30.5441],
[59.9733, 30.5427],
[60.0799, 30.3793],
[60.0926, 30.2488]
], {
	color: "#556ee6",
	fillColor: "#556ee6"
}).addTo(markermap);
var popupmap = L.map("leaflet-map-popup").setView([59.9342802, 30.3350986], 13);
L.tileLayer("https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw", {
	maxZoom: 18,
	attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
	id: "mapbox/streets-v11",
	tileSize: 512,
	zoomOffset: -1
}).addTo(popupmap), L.marker([51.5, -.09]).addTo(popupmap).bindPopup("<b>Hello world!</b><br />I am a popup.").openPopup(),

L.marker([59.9664191, 30.31041217]).addTo(popupmap).bindPopup("Большой пр-т П.С. 98 лит. А, кв 25").openPopup(),
L.marker([59.9466185, 30.355454417]).addTo(popupmap).bindPopup("Чайковского д.36, лит. А, кв. 4").openPopup(),
L.marker([59.9860281, 29.78174317]).addTo(popupmap).bindPopup("Петровская улица, дом 5, лит И").openPopup(),
L.marker([60.0088431, 29.729560317]).addTo(popupmap).bindPopup("Кронштадское шоссе, дом 28, лит Б.").openPopup(),
L.marker([60.0532688, 30.349298617]).addTo(popupmap).bindPopup("Санкт-Петербург, Сиреневый бульвар, дом 7, корпус 1, литера А, кв. 4").openPopup(),
L.marker([59.9875627, 30.313951217]).addTo(popupmap).bindPopup("Белоостровская улица, дом 25, литера А, кв. 3").openPopup(),
L.marker([59.9099365, 30.275973317]).addTo(popupmap).bindPopup("набережная Обводного канала, дом 211-213, литера А, кв. 3").openPopup(),
L.marker([60.0161779, 30.238050917]).addTo(popupmap).bindPopup("Санкт-Петербург, Авиаконструкторов, д. 20, к. 2, лит. А, кв. 1").openPopup(),
L.marker([59.9368308, 30.371511617]).addTo(popupmap).bindPopup("Старорусская д. 16\8-я Советская, д. 57, литера А, кв. 19").openPopup(),
L.marker([59.9958319, 30.374507617]).addTo(popupmap).bindPopup("Санкт-Петербург, Проспект Непокорённых, дом 9, корпус 1, литера А, кв. 21").openPopup(),
L.marker([59.9033348, 30.275315317]).addTo(popupmap).bindPopup("Старо-Петергофский проспект, дом 42, литера А, кв. 23").openPopup(),
L.marker([59.8072308, 30.09066691]).addTo(popupmap).bindPopup("Первомайская улица, дом 26, литера А, кв. 3").openPopup(),
L.marker([59.8673695, 30.37417417]).addTo(popupmap).bindPopup("ул. Турку д. 9, к.4, кв. 73").openPopup(),
L.marker([60.0094267, 30.251916317]).addTo(popupmap).bindPopup("Комендантский проспект, дом 16, корпус 2, кв. 1").openPopup(),
L.marker([59.8999617, 30.475310317]).addTo(popupmap).bindPopup("ул. Шотмана д.5, к.1, кв. 180").openPopup(),
L.marker([59.8258677, 30.156879317]).addTo(popupmap).bindPopup("Проспект Народного Ополчения, д. 231, кв. 23").openPopup(),
L.marker([59.9246578, 30.2795917]).addTo(popupmap).bindPopup("Набережная реки Пряжки, дом 18-20, литера А, кв. 9").openPopup(),
L.marker([60.0262847, 30.420889317]).addTo(popupmap).bindPopup("Черкасова д.11 кор.1").openPopup(),
L.marker([59.8352942, 30.178311117]).addTo(popupmap).bindPopup("Авангардная 23, кв. 19").openPopup(),
L.marker([59.9906439, 30.298872617]).addTo(popupmap).bindPopup("Набережная Черной речки 59, кв. 1").openPopup(),
L.marker([59.9139178, 30.33432117]).addTo(popupmap).bindPopup("Набережная Обводного канала д. 66, лит. А, кв.4").openPopup(),
L.marker([59.9136858, 30.347958117]).addTo(popupmap).bindPopup("Лиговский проспект д. 142 лит. А, кв. 8").openPopup(),
L.marker([59.9558782, 30.286171717]).addTo(popupmap).bindPopup("Офицерский переулок, д.8б, лит. В. , кв. 5").openPopup(),
L.marker([59.9587466, 30.285319517]).addTo(popupmap).bindPopup("ул. Пионерская, д. 37, литера А, кв. 2").openPopup(),
L.marker([59.9577177, 30.350730317]).addTo(popupmap).bindPopup("Академика Лебедева, д. 21, лит А, кв. 2").openPopup(),
L.marker([59.9365938, 30.375577217]).addTo(popupmap).bindPopup("9-я Советская улица, дом 22, кв. 32").openPopup(),
L.marker([59.9271694, 30.311406417]).addTo(popupmap).bindPopup("Казначейская ул. д.5, кв.9").openPopup(),
L.marker([59.9213079, 30.335838317]).addTo(popupmap).bindPopup("Звенигородская улица, 12/17, кв.9").openPopup(),
L.marker([59.9960627, 30.377020317]).addTo(popupmap).bindPopup("пр-т Непокорённых, д. 13, к.1, кв. 60").openPopup();

var popup = L.popup(),
	customiconsmap = L.map("leaflet-map-custom-icons").setView([59.9342802, 30.3350986], 8);
L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", {
	attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
}).addTo(customiconsmap);
var LeafIcon = L.Icon.extend({
		options: {
			iconSize: [50, 50],
			iconAnchor: [0, 0],
			popupAnchor: [0, 0]
		}
	}),
	rassprofIcon = new LeafIcon({
		iconUrl: "assets/images/rassprof.png"
	});
	rdIcon = new LeafIcon({
		iconUrl: "assets/images/rd.png"
	});
L.marker([59.99439, 30.33062], {
	icon: rassprofIcon
}).addTo(customiconsmap).bindPopup("Офис на Большом Сампсоньевском").openPopup();
L.marker([59.93551, 30.26847], {
	icon: rdIcon
}).addTo(customiconsmap).bindPopup("Офис на В.О.").openPopup();


var interactivemap = L.map("leaflet-map-interactive-map").setView([37.8, -96], 4);

function getColor(e) {
	return 1e3 < e ? "#435fe3" : 500 < e ? "#556ee6" : 200 < e ? "#677de9" : 100 < e ? "#798ceb" : 50 < e ? "#8a9cee" : 20 < e ? "#9cabf0" : 10 < e ? "#aebaf3" : "#c0c9f6"
}

function style(e) {
	return {
		weight: 2,
		opacity: 1,
		color: "white",
		dashArray: "3",
		fillOpacity: .7,
		fillColor: getColor(e.properties.density)
	}
}
L.tileLayer("https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw", {
	maxZoom: 18,
	attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
	id: "mapbox/light-v9",
	tileSize: 512,
	zoomOffset: -1
}).addTo(interactivemap);


var geojson = L.geoJson(statesData, {
		style: style
	}).addTo(interactivemap),
	cities = L.layerGroup();
L.marker([59.9664191, 30.31041217]).addTo(cities).bindPopup("Большой пр-т П.С. 98 лит. А, кв 25").openPopup(),
L.marker([59.9466185, 30.355454417]).addTo(cities).bindPopup("Чайковского д.36, лит. А, кв. 4").openPopup(),
L.marker([59.9860281, 29.78174317]).addTo(cities).bindPopup("Петровская улица, дом 5, лит И").openPopup(),
L.marker([60.0088431, 29.729560317]).addTo(cities).bindPopup("Кронштадское шоссе, дом 28, лит Б.").openPopup(),
L.marker([60.0532688, 30.349298617]).addTo(cities).bindPopup("Санкт-Петербург, Сиреневый бульвар, дом 7, корпус 1, литера А, кв. 4").openPopup(),
L.marker([59.9875627, 30.313951217]).addTo(cities).bindPopup("Белоостровская улица, дом 25, литера А, кв. 3").openPopup(),
L.marker([59.9099365, 30.275973317]).addTo(cities).bindPopup("набережная Обводного канала, дом 211-213, литера А, кв. 3").openPopup(),
L.marker([60.0161779, 30.238050917]).addTo(cities).bindPopup("Санкт-Петербург, Авиаконструкторов, д. 20, к. 2, лит. А, кв. 1").openPopup(),
L.marker([59.9368308, 30.371511617]).addTo(cities).bindPopup("Старорусская д. 16\8-я Советская, д. 57, литера А, кв. 19").openPopup(),
L.marker([59.9958319, 30.374507617]).addTo(cities).bindPopup("Санкт-Петербург, Проспект Непокорённых, дом 9, корпус 1, литера А, кв. 21").openPopup(),
L.marker([59.9033348, 30.275315317]).addTo(cities).bindPopup("Старо-Петергофский проспект, дом 42, литера А, кв. 23").openPopup(),
L.marker([59.8072308, 30.09066691]).addTo(cities).bindPopup("Первомайская улица, дом 26, литера А, кв. 3").openPopup(),
L.marker([59.8673695, 30.37417417]).addTo(cities).bindPopup("ул. Турку д. 9, к.4, кв. 73").openPopup(),
L.marker([60.0094267, 30.251916317]).addTo(cities).bindPopup("Комендантский проспект, дом 16, корпус 2, кв. 1").openPopup(),
L.marker([59.8999617, 30.475310317]).addTo(cities).bindPopup("ул. Шотмана д.5, к.1, кв. 180").openPopup(),
L.marker([59.8258677, 30.156879317]).addTo(cities).bindPopup("Проспект Народного Ополчения, д. 231, кв. 23").openPopup(),
L.marker([59.9246578, 30.2795917]).addTo(cities).bindPopup("Набережная реки Пряжки, дом 18-20, литера А, кв. 9").openPopup(),
L.marker([60.0262847, 30.420889317]).addTo(cities).bindPopup("Черкасова д.11 кор.1").openPopup(),
L.marker([59.8352942, 30.178311117]).addTo(cities).bindPopup("Авангардная 23, кв. 19").openPopup(),
L.marker([59.9906439, 30.298872617]).addTo(cities).bindPopup("Набережная Черной речки 59, кв. 1").openPopup(),
L.marker([59.9139178, 30.33432117]).addTo(cities).bindPopup("Набережная Обводного канала д. 66, лит. А, кв.4").openPopup(),
L.marker([59.9136858, 30.347958117]).addTo(cities).bindPopup("Лиговский проспект д. 142 лит. А, кв. 8").openPopup(),
L.marker([59.9558782, 30.286171717]).addTo(cities).bindPopup("Офицерский переулок, д.8б, лит. В. , кв. 5").openPopup(),
L.marker([59.9587466, 30.285319517]).addTo(cities).bindPopup("ул. Пионерская, д. 37, литера А, кв. 2").openPopup(),
L.marker([59.9577177, 30.350730317]).addTo(cities).bindPopup("Академика Лебедева, д. 21, лит А, кв. 2").openPopup(),
L.marker([59.9365938, 30.375577217]).addTo(cities).bindPopup("9-я Советская улица, дом 22, кв. 32").openPopup(),
L.marker([59.9271694, 30.311406417]).addTo(cities).bindPopup("Казначейская ул. д.5, кв.9").openPopup(),
L.marker([59.9213079, 30.335838317]).addTo(cities).bindPopup("Звенигородская улица, 12/17, кв.9").openPopup(),
L.marker([59.9960627, 30.377020317]).addTo(cities).bindPopup("пр-т Непокорённых, д. 13, к.1, кв. 60").openPopup();
var mbAttr = 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
	mbUrl = "https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw",
	
	grayscale = L.tileLayer(mbUrl, {
		id: "mapbox/light-v9",
		tileSize: 512,
		zoomOffset: -1,
		attribution: mbAttr
	}),
	streets = L.tileLayer(mbUrl, {
		id: "mapbox/streets-v11",
		tileSize: 512,
		zoomOffset: -1,
		attribution: mbAttr
	}),
	
	
	layergroupcontrolmap = L.map("leaflet-map-group-control", {
		center: [59.9342802, 30.3350986],
		zoom: 8,
		layers: [streets, cities]
	}),
	baseLayers = {
		"Оттенки серого": grayscale,
		"Улицы": streets
	},
	overlays = {
		"Недвижка": cities
	};
L.control.layers(baseLayers, overlays).addTo(layergroupcontrolmap);