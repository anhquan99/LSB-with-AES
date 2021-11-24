import React from 'react';
import { Line } from 'react-chartjs-2';
import { createImportSpecifier } from 'typescript';

const state = {
    labels: ['January', 'February', 'March',
        'April', 'May'],
    datasets: [
        {
            label: 'Rainfall',
            fill: false,
            lineTension: 0.5,
            backgroundColor: 'rgba(75,192,192,1)',
            borderColor: 'rgba(0,0,0,1)',
            borderWidth: 2,
            data: [65, 59, 80, 81, 56]
        }
    ]
}

export default class MyChart extends React.Component {
    state = {
        emptyLabels: [],
        length: 0,
        data: {},
    }
    constructor(props) {
        super(props);
        console.log(this.props.original);
        console.log(this.props.watermarked);
        // if (props.original < props.watermarked) {
        //     this.setState({length : props.original.length});
        // }
        // else {
        //     this.setState({length : props.watermarked.length});
        // }
        // var tempLabels = [];
        // for (var i = 0; i < this.length; i++) {
        //     tempLabels.push("");
        // }
        // this.setState({emptyLabels : tempLabels });

        // this.setState({data : dataSet});
    }
    // dataSet = {
    //     labels: ["","","",""],
    //     datasets: [
    //         {
    //             label: 'Original ' + this.props.name,
    //             fill: false,
    //             lineTension: 0.5,
    //             borderColor: 'rgb(255, 99, 132)',
    //             backgroundColor: 'rgba(255, 99, 132, 0.5)',
    //             borderWidth: 2,
    //             data: [6,7,8,9,10]
    //         },
    //         {
    //             label: 'Watermarked ' + this.props.name,
    //             fill: false,
    //             lineTension: 0.5,
    //             borderColor: 'rgb(53, 162, 235)',
    //             backgroundColor: 'rgba(53, 162, 235, 0.5)',
    //             borderWidth: 2,
    //             data: [1,2,3,4,5]
    //         }
    //     ]
    // }
    render() {
        return (
            <div>
                <Line
                    data={{
                        labels: ["", "", "", ""],
                        datasets: [
                            {
                                label: 'Original ' + this.props.name,
                                fill: false,
                                lineTension: 0.5,
                                borderColor: 'rgb(255, 99, 132)',
                                backgroundColor: 'rgba(255, 99, 132, 0.5)',
                                borderWidth: 2,
                                data: this.props.watermarked
                            },
                            {
                                label: 'Watermarked ' + this.props.name,
                                fill: false,
                                lineTension: 0.5,
                                borderColor: 'rgb(53, 162, 235)',
                                backgroundColor: 'rgba(53, 162, 235, 0.5)',
                                borderWidth: 2,
                                data: this.props.original
                            }]
                    }}
                    options={{
                        title: {
                            display: true,
                            text: this.props.chartName,
                            fontSize: 20
                        },
                        legend: {
                            display: true,
                            position: 'right'
                        }
                    }}
                />
            </div>
        );
    }
}
