$(function () {
    var myChart = Highcharts.chart('container', {
        chart: {
            type: 'bar'
        },
        title: {
            text: 'Event name'
        },
        xAxis: {
            categories: ['a', 'b', 'c']
        },
        yAxis: {
            title: {
                text: 'Number of votes'
            }
        },
        series: [{
            name: 'Votes',
            data: [1, 2, 4]
        }]
    });
});