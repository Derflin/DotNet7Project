function addData4(data) {

    const dataPoints = [];

    const chart = new CanvasJS.Chart("chartWindSpeed", {
        animationEnabled: true,
        theme: "dark1",
        title: {
            text: "Speed of Wind",
        },
        axisY: {
            title: "m/s",
            suffix: "   ",
            crosshair: {
                enabled: true,
                valueFormatString: "#,##0.##°",
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
            lineColor:"red",
            color:"red",
            yValueFormatString: "#,##0.## °",
            xValueFormatString: "MMM DD hh mm ss",
            dataPoints: dataPoints
        }]
    });

    for (let i = 0; i < data.length; i++) {
        dataPoints.push({
            x: new Date(data[i].dateTime),
            y: data[i].speed
        });
    }
    chart.render();

}
