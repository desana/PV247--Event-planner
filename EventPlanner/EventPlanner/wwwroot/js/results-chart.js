$(function () {
    if (dataJson.categories.length === 0) {
        $("#container").text("There are no votes for this event.");
    } else {
        var myChart = Highcharts.chart("container", {
            chart: {
                type: "bar"
            },
            title: {
                text: dataJson.eventName
            },
            xAxis: {
                categories: dataJson.categories
            },
            yAxis: {
                title: {
                    text: "Number of votes"
                }
            },
            series: [{
                name: "Votes",
                data: dataJson.data
            }]
        });
    }
    
});

$(".toggle-diagram")
    .click(function() {
        $("#toggle-div").toggle();
        $("#arrow").toggleClass("glyphicon-menu-up");
        $("#arrow").toggleClass("glyphicon-menu-down");
    });
