import React, { Component } from "react";
import MyChart from './MyChart';
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
import axios from "axios";
import Line from 'chart.js/auto';
Line.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend);


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
        original: [],
        watermarked: [],
        originFileName: "",
        watermarkedFileName: "",

        redChartOrigin: [],
        redChartWater: [],
        redChartLabel: [],

        greenChartOrigin: [],
        greenChartWater: [],
        greenChartLabel: [],

        blueChartOrigin: [],
        blueChartWater: [],
        blueChartLabel: [],

        audioChartOrigin: [],
        audioChartWater: [],
        audioChartLabel: [],

    };
    audioOriginalRef = React.createRef();
    audioWatermarkRef = React.createRef();
    isFileImage(file) {
        return file && file["type"].split("/")[0] === "image";
    }
    async handleImageChange(e, source, name) {
        var origin = e.target.files;
        if (e.target.files) {
            for (let i = 0; i < e.target.files.length; i++) {
                if (!this.isFileImage(e.target.files[i])) {
                    this.setState({ message: "INVALID IMAGE FILE" });
                    this.setState({ [source]: [] });
                    return;
                }
            }
            const filesArray = Array.from(e.target.files).map(file =>
                URL.createObjectURL(file)
            );

            await this.setState({ [source]: filesArray });
            if (origin[0]?.name) {
                await this.setState({ [name]: origin[0].name });
            }
            else {
                await this.setState({ [name]: "" });
            }

        }
    }
    renderPhotos(source, state) {
        return source.map((photo, i) => {
            return (
                <div
                    id={i}
                    key={photo}
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
    async handleImageChange(e, source, name) {
        var origin = e.target.files;
        if (e.target.files) {
            for (let i = 0; i < e.target.files.length; i++) {
                if (!this.isFileImage(e.target.files[i])) {
                    this.setState({ message: "INVALID IMAGE FILE" });
                    this.setState({ [source]: [] });
                    return;
                }
            }
            const filesArray = Array.from(e.target.files).map(file =>
                URL.createObjectURL(file)
            );

            await this.setState({ [source]: filesArray });
            if (origin[0]?.name) {
                await this.setState({ [name]: origin[0].name });
            }
            else {
                await this.setState({ [name]: "" });
            }
        }
    }
    renderAudio(source, name) {

        if (name.toLowerCase() === "original") {
            return source.map((audio) => {
                return (
                    <audio ref={this.audioOriginalRef} controls className="full_sizeAudio">
                        <source src={audio} type="audio/ogg" />
                        <source src={audio} type="audio/mpeg" />
                    </audio>
                );
            });
        }
        else {
            return source.map((audio) => {
                return (
                    <audio ref={this.audioWatermarkRef} controls className="full_sizeAudio">
                        <source src={audio} type="audio/ogg" />
                        <source src={audio} type="audio/mpeg" />
                    </audio>
                );
            });
        }
    }
    makeChart(original, watermarked, name) {
        const dataState = {
            datasets: [
                {
                    label: 'Original' + name,
                    fill: false,
                    lineTension: 0.5,
                    borderColor: 'rgb(255, 99, 132)',
                    backgroundColor: 'rgba(255, 99, 132, 0.5)',
                    borderWidth: 2,
                    data: original
                },
                {
                    label: 'Watermarked' + name,
                    fill: false,
                    lineTension: 0.5,
                    borderColor: 'rgb(53, 162, 235)',
                    backgroundColor: 'rgba(53, 162, 235, 0.5)',
                    borderWidth: 2,
                    data: watermarked
                }
            ]
        }
        return (
            <Line
                data={dataState}
                options={{
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: { name } + 'Chart',
                        },
                    },
                }}
            />
        );
    }
    async handleAudioChange(e, source, name) {
        var origin = e.target.files;
        const filesArray = Array.from(e.target.files).map(file =>
            URL.createObjectURL(file)
        );
        await this.setState({ [source]: filesArray });
        if (origin[0]?.name) {
            await this.setState({ [name]: origin[0].name });
            if (source.toLowerCase() === "original") {
                this.audioOriginalRef.current.pause();
                this.audioOriginalRef.current.load();
            }
            else {
                this.audioWatermarkRef.current.pause();
                this.audioWatermarkRef.current.load();
            }

        }
        else {
            await this.setState({ [name]: "" });
        }

    }
    async handleFormSubmit(e) {
        e.preventDefault();
        const formData = new FormData(e.target);
        await axios.post("/api/analyze", formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        }).then((response) => {
            if (this.state.fileType.toLowerCase() === "image") {
                this.setState({ redChartOrigin: response.data[0].original });
                this.setState({ redChartWater: response.data[0].watermarked });
                this.setState({ redChartLabel: response.data[0].label });

                this.setState({ greenChartOrigin: response.data[1].original });
                this.setState({ greenChartWater: response.data[1].watermarked });
                this.setState({ greenChartLabel: response.data[1].label });

                this.setState({ blueChartOrigin: response.data[2].original });
                this.setState({ blueChartWater: response.data[2].watermarked });
                this.setState({ blueChartLabel: response.data[2].label });

            }
            else {
                this.setState({ audioChartOrigin: response.data.original });
                this.setState({ audioChartWater: response.data.watermarked });
                this.setState({ audioChartLabel: response.data.label });
            }
        }).catch((response) => {
            console.log(response)
        });
    }
    clearChart() {
        this.setState({ redChartOrigin: [] });
        this.setState({ redChartWater: [] });
        this.setState({ redChartLabel: [] });

        this.setState({ greenChartOrigin: [] });
        this.setState({ greenChartWater: [] });
        this.setState({ greenChartLabel: [] });

        this.setState({ blueChartOrigin: [] });
        this.setState({ blueChartWater: [] });
        this.setState({ blueChartLabel: [] });

        this.setState({ audioChartOrigin: [] });
        this.setState({ audioChartWater: [] });
        this.setState({ audioChartLabel: [] });
    }
    render() {
        return (
            <>
                <Form
                    className="body"
                    method="POST"
                    encType="multipart/form-data"
                    onSubmit={e => this.handleFormSubmit(e)}
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
                            <Label for="file">Original</Label>
                            <div className="custom-file">
                                <input
                                    type="file"
                                    required={true}
                                    className="custom-file-input"
                                    id="file"
                                    name="original"
                                    accept={this.state.fileType == "Image" ? "image/*" : ".wav"}
                                    onChange={(e) => {
                                        if (this.state.fileType === "Image") {
                                            this.handleImageChange(e, "original", "originFileName")
                                        }
                                        else {
                                            this.handleAudioChange(e, "original", "originFileName")
                                        }
                                    }}
                                    required={true}
                                />
                                <label className="custom-file-label" htmlFor="file">
                                    {this.state.originFileName === '' ? "Choose file" : this.state.originFileName}
                                </label>
                            </div>
                            <FormText>Audio file support only for .wav file.</FormText>
                        </Col>
                        <Col>
                            {this.state.fileType === "Image" ? <div className="full_size">{this.renderPhotos(this.state.original, "original")}</div> :
                                <div className="full_size">{this.renderAudio(this.state.original, "original")}</div>}
                        </Col>
                    </Row>
                    <br />
                    <br />
                    <Row xs="2">
                        <Col>
                            <Label for="file">Watermarked</Label>
                            <div className="custom-file">
                                <input
                                    type="file"
                                    required={true}
                                    className="custom-file-input"
                                    id="file"
                                    name="watermarked"
                                    accept={this.state.fileType == "Image" ? "image/*" : ".wav"}
                                    onChange={(e) => {
                                        if (this.state.fileType === "Image") {
                                            this.handleImageChange(e, "watermarked", "watermarkedFileName")
                                        }
                                        else {
                                            this.handleAudioChange(e, "watermarked", "watermarkedFileName")
                                        }
                                    }}
                                    required={true}
                                />
                                <label className="custom-file-label" htmlFor="file">
                                    {this.state.watermarkedFileName === '' ? "Choose file" : this.state.watermarkedFileName}
                                </label>
                            </div>
                            <FormText>Audio file support only for .wav file.</FormText>
                        </Col>
                        <Col>
                            {this.state.fileType === "Image" ? <div className="full_size">{this.renderPhotos(this.state.watermarked, "watermarked")}</div> :
                                <div className="full_size">{this.renderAudio(this.state.watermarked, "watermarked")}</div>}
                        </Col>
                    </Row>
                    <br></br>
                    <Row xs="4">
                        <Button color="primary">Submit</Button>
                        <Col></Col>
                        <Col></Col>
                        <Button onClick={() => this.clearChart()} color="danger">Clear</Button>
                    </Row>

                </Form>
                {this.state.redChartOrigin.length != 0 && this.state.redChartWater.length != 0 ?
                    <div>
                        <h1>Red pixel</h1>
                        <MyChart original={this.state.redChartOrigin} watermarked={this.state.redChartWater} label={this.state.redChartLabel} name="Red" chartName="Red "></MyChart>
                    </div>
                    : ""}

                {this.state.greenChartOrigin.length != 0 && this.state.greenChartWater.length != 0 ?
                    <div>
                        <h1>Green pixel</h1>
                        <MyChart original={this.state.greenChartOrigin} watermarked={this.state.greenChartWater} label={this.state.greenChartLabel} name="Green " chartName="Green "></MyChart>
                    </div>
                    : ""}

                {this.state.blueChartOrigin.length != 0 && this.state.blueChartWater.length != 0 ?
                    <div>
                        <h1>Blue pixel</h1>
                        <MyChart original={this.state.blueChartOrigin} watermarked={this.state.blueChartWater} label={this.state.blueChartLabel} name="Blue" chartName="Blue"></MyChart>
                    </div>
                    : ""}

                {this.state.audioChartOrigin.length != 0 && this.state.audioChartWater.length != 0 ?
                    <div>
                        <h1>Audio data bytes</h1>
                        <MyChart original={this.state.audioChartOrigin} watermarked={this.state.audioChartWater} label={this.state.audioChartLabel} name="audio" chartName="Audio"></MyChart>
                    </div>
                    : ""}

            </>


        );
    }
}