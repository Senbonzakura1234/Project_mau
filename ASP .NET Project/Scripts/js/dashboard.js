
$(function () {
    $.fn.digits = function () {
        return this.each(function() {
            $(this).text($(this).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
        });
    }
    $.ajax({
        url: "/Dashboard/GetBannerData",
        type: "GET",
        success: function (res) {
            $("#totalSales").html(res.lastMonth);
            $("#totalSales").digits();
            $("#changePercentSales").html(res.detailPercent);
            $("#percentSales").html(res.percentageInt);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });

    // Remove pro banner on close
    if ($("#bannerClose").length) {
        document.querySelector("#bannerClose").addEventListener("click", function () {
                document.querySelector("#proBanner").classList.add("d-none");
            });
    }
    

    window.Chart.defaults.global.legend.labels.usePointStyle = true;


    var ctx;
    var gradientStrokeViolet;
    var myChart;
    

    if ($("#points-chart").length) {
        ctx = document.getElementById("points-chart").getContext("2d");

        gradientStrokeViolet = ctx.createLinearGradient(0, 0, 0, 181);
        gradientStrokeViolet.addColorStop(0, "rgba(218, 140, 255, 1)");
        gradientStrokeViolet.addColorStop(1, "rgba(154, 85, 255, 1)");

        myChart = new window.Chart(ctx, {
            type: "bar",
            data: {
                labels: [1, 2, 3, 4, 5, 6, 7, 8],
                datasets: [
                    {
                        label: "North Zone",
                        borderColor: gradientStrokeViolet,
                        backgroundColor: gradientStrokeViolet,
                        hoverBackgroundColor: gradientStrokeViolet,
                        pointRadius: 0,
                        //fill: false,
                        borderWidth: 1,
                        fill: "origin",
                        data: [20, 40, 15, 35, 25, 50, 30, 20]
                    },
                    {
                        label: "South Zone",
                        borderColor: "#e9eaee",
                        backgroundColor: "#e9eaee",
                        hoverBackgroundColor: "#e9eaee",
                        pointRadius: 0,
                        //fill: false,
                        borderWidth: 1,
                        fill: "origin",
                        data: [40, 30, 20, 10, 50, 15, 35, 20]
                    }
                ]
            },
            options: {
                legend: {
                    display: false
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            display: false,
                            min: 0,
                            stepSize: 10
                        },
                        gridLines: {
                            drawBorder: false,
                            display: false
                        }
                    }],
                    xAxes: [{
                        gridLines: {
                            display: false,
                            drawBorder: false,
                            color: "rgba(0,0,0,1)",
                            zeroLineColor: "#eeeeee"
                        },
                        ticks: {
                            padding: 20,
                            fontColor: "#9c9fa6",
                            autoSkip: true
                        },
                        barPercentage: 0.7
                    }]
                }
            },
            elements: {
                point: {
                    radius: 0
                }
            }
        });
    }
    var gradientStrokeBlue;
    if ($("#events-chart").length) {
        ctx = document.getElementById("events-chart").getContext("2d");

        gradientStrokeBlue = ctx.createLinearGradient(0, 0, 0, 181);
        gradientStrokeBlue.addColorStop(0, "rgba(54, 215, 232, 1)");
        gradientStrokeBlue.addColorStop(1, "rgba(177, 148, 250, 1)");

        myChart = new window.Chart(ctx, {
            type: "bar",
            data: {
                labels: [1, 2, 3, 4, 5, 6, 7, 8],
                datasets: [
                    {
                        label: "Domestic",
                        borderColor: gradientStrokeBlue,
                        backgroundColor: gradientStrokeBlue,
                        hoverBackgroundColor: gradientStrokeBlue,
                        pointRadius: 0,
                        //fill: false,
                        borderWidth: 1,
                        fill: "origin",
                        data: [20, 40, 15, 35, 25, 50, 30, 20]
                    },
                    {
                        label: "International",
                        borderColor: "#e9eaee",
                        backgroundColor: "#e9eaee",
                        hoverBackgroundColor: "#e9eaee",
                        pointRadius: 0,
                        //fill: false,
                        borderWidth: 1,
                        fill: "origin",
                        data: [40, 30, 20, 10, 50, 15, 35, 20]
                    }
                ]
            },
            options: {
                legend: {
                    display: false
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            display: false,
                            min: 0,
                            stepSize: 10
                        },
                        gridLines: {
                            drawBorder: false,
                            display: false
                        }
                    }],
                    xAxes: [{
                        gridLines: {
                            display: false,
                            drawBorder: false,
                            color: "rgba(0,0,0,1)",
                            zeroLineColor: "#eeeeee"
                        },
                        ticks: {
                            padding: 20,
                            fontColor: "#9c9fa6",
                            autoSkip: true
                        },
                        barPercentage: 0.7
                    }]
                }
            },
            elements: {
                point: {
                    radius: 0
                }
            }
        });
    }
    var gradientLegendViolet;
    var gradientLegendBlue;
    var gradientStrokeRed;
    var gradientLegendRed;
    if ($("#visit-sale-chart").length) {
        window.Chart.defaults.global.legend.labels.usePointStyle = true;
        ctx = document.getElementById("visit-sale-chart").getContext("2d");

        gradientStrokeViolet = ctx.createLinearGradient(0, 0, 0, 181);
        gradientStrokeViolet.addColorStop(0, "rgba(218, 140, 255, 1)");
        gradientStrokeViolet.addColorStop(1, "rgba(154, 85, 255, 1)");
        gradientLegendViolet = "linear-gradient(to right, rgba(218, 140, 255, 1), rgba(154, 85, 255, 1))";

        gradientStrokeBlue = ctx.createLinearGradient(0, 0, 0, 360);
        gradientStrokeBlue.addColorStop(0, "rgba(54, 215, 232, 1)");
        gradientStrokeBlue.addColorStop(1, "rgba(177, 148, 250, 1)");
        gradientLegendBlue = "linear-gradient(to right, rgba(54, 215, 232, 1), rgba(177, 148, 250, 1))";

        gradientStrokeRed = ctx.createLinearGradient(0, 0, 0, 300);
        gradientStrokeRed.addColorStop(0, "rgba(255, 191, 150, 1)");
        gradientStrokeRed.addColorStop(1, "rgba(254, 112, 150, 1)");
        gradientLegendRed = "linear-gradient(to right, rgba(255, 191, 150, 1), rgba(254, 112, 150, 1))";


        $.ajax({
            url: "/Dashboard/GetColumnChartData",
            type: "GET",
            success: function (res) {
                const data1 = res.data1;
                const data2 = res.data2;
                const data3 = res.data3;
                const countData1 = [], countData2 = [], countData3 = [], monthDataDisplay = [];
                const monthDataString = [
                    "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"
                ];
                for (let i = 0; i < data1.length; i++) {
                    countData1[i] = data1[i].Count;
                    const j = data1[i].MonthData - 1;
                    monthDataDisplay[i] = monthDataString[j];
                }
                for (let i = 0; i < data2.length; i++) {
                    countData2[i] = data2[i].Count;
                }
                for (let i = 0; i < data3.length; i++) {
                    countData3[i] = data3[i].Count;
                }
                console.log(countData1);
                console.log(countData2);
                console.log(countData3);
                myChart = new window.Chart(ctx, {
                    type: "bar",
                    data: {
                        labels: monthDataDisplay,
                        datasets: [
                            {
                                label: "less than $50",
                                borderColor: gradientStrokeViolet,
                                backgroundColor: gradientStrokeViolet,
                                hoverBackgroundColor: gradientStrokeViolet,
                                legendColor: gradientLegendViolet,
                                pointRadius: 0,
                                //fill: false,
                                borderWidth: 1,
                                fill: "origin",
                                data: countData3
                            },
                            {
                                label: "from $50 to $200",
                                borderColor: gradientStrokeRed,
                                backgroundColor: gradientStrokeRed,
                                hoverBackgroundColor: gradientStrokeRed,
                                legendColor: gradientLegendRed,
                                pointRadius: 0,
                                //fill: false,
                                borderWidth: 1,
                                fill: "origin",
                                data: countData2
                            },
                            {
                                label: "over than $200",
                                borderColor: gradientStrokeBlue,
                                backgroundColor: gradientStrokeBlue,
                                hoverBackgroundColor: gradientStrokeBlue,
                                legendColor: gradientLegendBlue,
                                pointRadius: 0,
                                //fill: false,
                                borderWidth: 1,
                                fill: "origin",
                                data: countData1
                            }
                        ]
                    },
                    options: {
                        responsive: true,
                        legend: false,
                        legendCallback: function (chart) {
                            const text = [];
                            text.push("<ul>");
                            for (let i = 0; i < chart.data.datasets.length; i++) {
                                text.push(`<li><span class="legend-dots" style="background:${chart.data.datasets[i].legendColor}"></span>`);
                                if (chart.data.datasets[i].label) {
                                    text.push(chart.data.datasets[i].label);
                                }
                                text.push("</li>");
                            }
                            text.push("</ul>");
                            return text.join("");
                        },
                        scales: {
                            yAxes: [{
                                ticks: {
                                    display: false,
                                    min: 0,
                                    stepSize: 20,
                                    max: 80
                                },
                                gridLines: {
                                    drawBorder: false,
                                    color: "rgba(235,237,242,1)",
                                    zeroLineColor: "rgba(235,237,242,1)"
                                }
                            }],
                            xAxes: [{
                                gridLines: {
                                    display: false,
                                    drawBorder: false,
                                    color: "rgba(0,0,0,1)",
                                    zeroLineColor: "rgba(235,237,242,1)"
                                },
                                ticks: {
                                    padding: 20,
                                    fontColor: "#9c9fa6",
                                    autoSkip: true
                                },
                                categoryPercentage: 0.5,
                                barPercentage: 0.5
                            }]
                        }
                    },
                    elements: {
                        point: {
                            radius: 0
                        }
                    }
                });
                $("#visit-sale-chart-legend").html(myChart.generateLegend());
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });

        
    }
    if ($("#visit-sale-chart-dark").length) {
        window.Chart.defaults.global.legend.labels.usePointStyle = true;
        ctx = document.getElementById("visit-sale-chart-dark").getContext("2d");

        gradientStrokeViolet = ctx.createLinearGradient(0, 0, 0, 181);
        gradientStrokeViolet.addColorStop(0, "rgba(218, 140, 255, 1)");
        gradientStrokeViolet.addColorStop(1, "rgba(154, 85, 255, 1)");
        gradientLegendViolet = "linear-gradient(to right, rgba(218, 140, 255, 1), rgba(154, 85, 255, 1))";

        gradientStrokeBlue = ctx.createLinearGradient(0, 0, 0, 360);
        gradientStrokeBlue.addColorStop(0, "rgba(54, 215, 232, 1)");
        gradientStrokeBlue.addColorStop(1, "rgba(177, 148, 250, 1)");
        gradientLegendBlue = "linear-gradient(to right, rgba(54, 215, 232, 1), rgba(177, 148, 250, 1))";

        gradientStrokeRed = ctx.createLinearGradient(0, 0, 0, 300);
        gradientStrokeRed.addColorStop(0, "rgba(255, 191, 150, 1)");
        gradientStrokeRed.addColorStop(1, "rgba(254, 112, 150, 1)");
        gradientLegendRed = "linear-gradient(to right, rgba(255, 191, 150, 1), rgba(254, 112, 150, 1))";

        myChart = new window.Chart(ctx, {
            type: "bar",
            data: {
                labels: ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG"],
                datasets: [
                    {
                        label: "CHN",
                        borderColor: gradientStrokeViolet,
                        backgroundColor: gradientStrokeViolet,
                        hoverBackgroundColor: gradientStrokeViolet,
                        legendColor: gradientLegendViolet,
                        pointRadius: 0,
                        //fill: false,
                        borderWidth: 1,
                        fill: "origin",
                        data: [20, 40, 15, 35, 25, 50, 30, 20]
                    },
                    {
                        label: "USA",
                        borderColor: gradientStrokeRed,
                        backgroundColor: gradientStrokeRed,
                        hoverBackgroundColor: gradientStrokeRed,
                        legendColor: gradientLegendRed,
                        pointRadius: 0,
                        //fill: false,
                        borderWidth: 1,
                        fill: "origin",
                        data: [40, 30, 20, 10, 50, 15, 35, 40]
                    },
                    {
                        label: "UK",
                        borderColor: gradientStrokeBlue,
                        backgroundColor: gradientStrokeBlue,
                        hoverBackgroundColor: gradientStrokeBlue,
                        legendColor: gradientLegendBlue,
                        pointRadius: 0,
                        //fill: false,
                        borderWidth: 1,
                        fill: "origin",
                        data: [70, 10, 30, 40, 25, 50, 15, 30]
                    }
                ]
            },
            options: {
                responsive: true,
                legend: false,
                legendCallback: function (chart) {
                    const text = [];
                    text.push("<ul>");
                    for (let i = 0; i < chart.data.datasets.length; i++) {
                        text.push(`<li><span class="legend-dots" style="background:${chart.data.datasets[i].legendColor}"></span>`);
                        if (chart.data.datasets[i].label) {
                            text.push(chart.data.datasets[i].label);
                        }
                        text.push("</li>");
                    }
                    text.push("</ul>");
                    return text.join("");
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            display: false,
                            min: 0,
                            stepSize: 20,
                            max: 80
                        },
                        gridLines: {
                            drawBorder: false,
                            color: "#322f2f",
                            zeroLineColor: "#322f2f"
                        }
                    }],
                    xAxes: [{
                        gridLines: {
                            display: false,
                            drawBorder: false,
                            color: "rgba(0,0,0,1)",
                            zeroLineColor: "rgba(235,237,242,1)"
                        },
                        ticks: {
                            padding: 20,
                            fontColor: "#9c9fa6",
                            autoSkip: true
                        },
                        categoryPercentage: 0.5,
                        barPercentage: 0.5
                    }]
                }
            },
            elements: {
                point: {
                    radius: 0
                }
            }
        });
        $("#visit-sale-chart-legend-dark").html(myChart.generateLegend());
    }
    if ($("#traffic-chart").length) {
        gradientStrokeBlue = ctx.createLinearGradient(0, 0, 0, 181);
        gradientStrokeBlue.addColorStop(0, "rgba(54, 215, 232, 1)");
        gradientStrokeBlue.addColorStop(1, "rgba(177, 148, 250, 1)");
        gradientLegendBlue = "linear-gradient(to right, rgba(54, 215, 232, 1), rgba(177, 148, 250, 1))";

        gradientStrokeRed = ctx.createLinearGradient(0, 0, 0, 50);
        gradientStrokeRed.addColorStop(0, "rgba(255, 191, 150, 1)");
        gradientStrokeRed.addColorStop(1, "rgba(254, 112, 150, 1)");
        gradientLegendRed = "linear-gradient(to right, rgba(255, 191, 150, 1), rgba(254, 112, 150, 1))";

        var gradientStrokeGreen = ctx.createLinearGradient(0, 0, 0, 300);
        gradientStrokeGreen.addColorStop(0, "rgba(6, 185, 157, 1)");
        gradientStrokeGreen.addColorStop(1, "rgba(132, 217, 210, 1)");
        var gradientLegendGreen = "linear-gradient(to right, rgba(6, 185, 157, 1), rgba(132, 217, 210, 1))";

        $.ajax({
            url: "/Dashboard/GetPieChartData",
            type: "GET",
            success: function (res) {
                const data = res.data;
                console.log(data);
                const paymentTypeData = [];
                const percentData = [];
                for (let i = 0; i < data.length; i++) {
                    paymentTypeData[i] = data[i].PaymentTypeData;
                    percentData[i] = data[i].Percent;
                }
                console.log(paymentTypeData);
                console.log(percentData);
                var trafficChartData = {
                    datasets: [{
                        data: percentData,
                        backgroundColor: [
                            gradientStrokeBlue,
                            gradientStrokeGreen,
                            gradientStrokeRed
                        ],
                        hoverBackgroundColor: [
                            gradientStrokeBlue,
                            gradientStrokeGreen,
                            gradientStrokeRed
                        ],
                        borderColor: [
                            gradientStrokeBlue,
                            gradientStrokeGreen,
                            gradientStrokeRed
                        ],
                        legendColor: [
                            gradientLegendBlue,
                            gradientLegendGreen,
                            gradientLegendRed
                        ]
                    }],

                    // These labels appear in the legend and in the tooltips when hovering different arcs
                    labels: paymentTypeData
                };
                const trafficChartOptions = {
                    responsive: true,
                    animation: {
                        animateScale: true,
                        animateRotate: true
                    },
                    legend: false,
// ReSharper disable once UnusedParameter
                    legendCallback: function (chart) {
                        const text = [];
                        text.push("<ul>");
                        for (let i = 0; i < trafficChartData.datasets[0].data.length; i++) {
                            text.push(`<li><span class="legend-dots" style="background:${trafficChartData.datasets[0].legendColor[i]}"></span>`);
                            if (trafficChartData.labels[i]) {
                                text.push(trafficChartData.labels[i]);
                            }
                            text.push(`<span class="float-right">${trafficChartData.datasets[0].data[i]}%</span>`);
                            text.push("</li>");
                        }
                        text.push("</ul>");
                        return text.join("");
                    }
                };
                const trafficChartCanvas = $("#traffic-chart").get(0).getContext("2d");
                const trafficChart = new window.Chart(trafficChartCanvas, {
                    type: "doughnut",
                    data: trafficChartData,
                    options: trafficChartOptions
                });
                $("#traffic-chart-legend").html(trafficChart.generateLegend());
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });


       
    }
});