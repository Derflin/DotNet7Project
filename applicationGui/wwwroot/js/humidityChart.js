function addData2(data) {

    const dataPoints = [];

    const chart = new CanvasJS.Chart("chartHumidity", {
        animationEnabled: true,
        theme: "dark1",
        title: {
            text: "Humidity",
            fontWeight: "lighter",
        },
        axisY: {
            title: "percent",
            crosshair: {
                enabled: true,
                valueFormatString: "#,##0.##",
                snapToDataPoint: true
            }
        },
        axisX: {
            crosshair: {
                enabled: true,
                snapToDataPoint: true
            }
        },
        data: [{
            type: "line",
            yValueFormatString: "#,##0.##",
            xValueFormatString: "MMM DD hh mm ss",
            dataPoints: dataPoints
        }]
    });

    for (let i = 0; i < data.length; i++) {
        dataPoints.push({
            x: new Date(data[i].dateTime),
            y: data[i].humidity
        });
    }
    chart.render();

}
