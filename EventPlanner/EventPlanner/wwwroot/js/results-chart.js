$(function () {
    var myChart = Highcharts.chart('container', {
        chart: {
            type: 'bar'
        },
        title: {
            text: dataJson.eventName
        },
        xAxis: {
            categories: dataJson.categories
        },
        yAxis: {
            title: {
                text: 'Number of votes'
            }
        },
        series: [{
            name: 'Votes',
            data: dataJson.data
        }]
    });
});

$("#click-to-toggle").click(function () {
    $("#toggle-div").toggle();
});