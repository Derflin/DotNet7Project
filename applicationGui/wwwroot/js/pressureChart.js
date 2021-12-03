function addData(data) {

    const dataPoints = [];

    const chart = new CanvasJS.Chart("chartPressure", {
        animationEnabled: true,
        theme: "dark1",
        title: {
            text: "Pressure"
        },
        axisY: {
            title: "hPa",
            titleFontSize: 18,
            suffix: "",
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
            lineColor:"green",
            color:"green",
            yValueFormatString: "#,##0.## ",
            xValueFormatString: "MMM DD hh mm ss",
            dataPoints: dataPoints
        }]
    });

    for (let i = 0; i < data.length; i++) {

        dataPoints.push({
            x: new Date(data[i].dateTime),
            y: data[i].pressure
        });
    }
    chart.render();

}
