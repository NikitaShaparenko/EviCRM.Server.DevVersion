var options = {
        chart: {
            height: 330,
            type: "bar",
            toolbar: {
                show: !1
            }
        },
        plotOptions: {
            bar: {
                horizontal: !1,
                columnWidth: "0%",
                endingShape: "rounded"
            }
        },
        dataLabels: {
            enabled: !1
        },
        stroke: {
            show: !0,
            width: 2,
            colors: ["transparent"]
        },
        series: [{
            name: "Revenue",
            data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
        }],
        xaxis: {
            categories: ["Янв", "Фев", "Март", "Апр", "Май", "Июнь", "Июль", "Авг", "Сен", "Окт", "Нов", "Дек"]
        },
        yaxis: {
            title: {
                text: "(в тыс. рублей)"
            }
        },
        fill: {
            opacity: 1
        },
        colors: ["#556ee6"]
    },
    chart = new ApexCharts(document.querySelector("#revenue-chart"), options);
chart.render();