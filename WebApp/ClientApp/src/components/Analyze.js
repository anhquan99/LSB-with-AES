import React, { Component } from "react";
import {
    Form,
    Label,
    Input,
    FormGroup,
    FormText,
    Button,
    Col,
    Row
} from "reactstrap";
import {
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
} from 'chart.js';
import Chart from 'chart.js/auto';
Chart.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend);
const state = {
    labels: ['January', 'February', 'March',
        'April', 'May'],
    datasets: [
        {
            label: 'Rainfall',
            fill: false,
            lineTension: 0.5,
            borderColor: 'rgb(255, 99, 132)',
            backgroundColor: 'rgba(255, 99, 132, 0.5)',
            borderWidth: 2,
            data: [65, 59, 80, 81, 56]
        },
        {
            label: 'Test',
            fill: false,
            lineTension: 0.5,
            borderColor: 'rgb(53, 162, 235)',
            backgroundColor: 'rgba(53, 162, 235, 0.5)',
            borderWidth: 2,
            data: [35, 40, 10, 51, 0]
        }
    ]
}
const options = {
    responsive: true,
    plugins: {
        legend: {
            position: 'top',
        },
        title: {
            display: true,
            text: 'Chart.js Line Chart',
        },
    },
};
export class Analyze extends Component {
    state = {
        fileType: "Image",
        file: [],
        message: "",
        keySize: "",
        key: "",
        result: "",
        fileName: ""
    };
    renderPhotos(source, state) {
        return source.map((photo, i) => {
            return (
                <div
                    id={i}
                    key={photo}
                    ref={this.setRef}
                    style={{
                        border: "1px solid rgb(222, 222, 222)",
                        height: "250px",
                        backgroundImage: `url(${photo})`,
                        backgroundColor: "white",
                        backgroundPosition: "center",
                        backgroundRepeat: "no-repeat",
                        backgroundSize: "contain",
                        position: "relative"
                    }}
                ></div>
            );
        });
    }
    renderAudio(source, state) {
        return source.map((audio) => {
            return (
                <audio ref={this.audioRef} controls className="full_sizeAudio">
                    <source src={audio} type="audio/ogg" />
                    <source src={audio} type="audio/mpeg" />
                </audio>
            );
        });
    }
    render() {
        return (
            <Form
                action="/api/AES"
                className="body"
                method="POST"
                // onSubmit={e => this.handleFormSubmit(e)}
                encType="multipart/form-data"
            >
                <h1>ANALYZE FILE</h1>
                <FormGroup>
                    <Label for="fileType">File type</Label>
                    <Input
                        id="fileType"
                        name="fileType"
                        type="select"
                        required={true}
                        onChange={e => {
                            this.setState({ fileType: e.target.value });
                        }}
                    >
                        <option value="Image">Image</option>
                        <option value="Audio">Audio</option>
                    </Input>
                </FormGroup>
                <Row xs="2">
                    <Col>
                        <Label for="file">File</Label>
                        <div className="custom-file">
                            <input
                                type="file"
                                required={true}
                                className="custom-file-input"
                                id="file"
                                name="file"
                                accept={this.state.fileType == "Image" ? "image/*" : ".wav"}
                                onChange={(e) => {
                                    if (this.state.fileType === "Image") {
                                        this.handleImageChange(e, "file")
                                    }
                                    else {
                                        this.handleAudioChange(e, "file")
                                    }
                                }}
                                required={true}
                            />
                            <label className="custom-file-label" htmlFor="file">
                                {this.state.fileName === '' ? "Choose file" : this.state.fileName}
                            </label>
                        </div>
                        <FormText>Audio file support only for .wav file.</FormText>
                    </Col>
                    <Col>
                        {this.state.fileType === "Image" ? <div className="full_size">{this.renderPhotos(this.state.file, "file")}</div> :
                            <div className="full_size">{this.renderAudio(this.state.file, "file")}</div>}
                    </Col>
                </Row>
                <Row xs="2">
                    <Col>
                        <Label for="file">File</Label>
                        <div className="custom-file">
                            <input
                                type="file"
                                required={true}
                                className="custom-file-input"
                                id="file"
                                name="file"
                                accept={this.state.fileType == "Image" ? "image/*" : ".wav"}
                                onChange={(e) => {
                                    if (this.state.fileType === "Image") {
                                        this.handleImageChange(e, "file")
                                    }
                                    else {
                                        this.handleAudioChange(e, "file")
                                    }
                                }}
                                required={true}
                            />
                            <label className="custom-file-label" htmlFor="file">
                                {this.state.fileName === '' ? "Choose file" : this.state.fileName}
                            </label>
                        </div>
                        <FormText>Audio file support only for .wav file.</FormText>
                    </Col>
                    <Col>
                        {this.state.fileType === "Image" ? <div className="full_size">{this.renderPhotos(this.state.file, "file")}</div> :
                            <div className="full_size">{this.renderAudio(this.state.file, "file")}</div>}
                    </Col>
                </Row>
                <br></br>
                <Button color="primary">Submit</Button>
            </Form>
            // <Line
            //     data={state}
            //     options={{
            //         responsive: true,
            //         plugins: {
            //             legend: {
            //                 position: 'top',
            //             },
            //             title: {
            //                 display: true,
            //                 text: 'Chart.js Line Chart',
            //             },
            //         },
            //     }}
            // />

        );
    }
}