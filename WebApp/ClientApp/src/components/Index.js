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
export class Index extends Component {
  state = {
    fileType: "Image",
    file: [],
    message: "",
    keySize: "",
    key: "",
    result: "",
    fileName: ""
  };
  audioRef = React.createRef();
  isFileImage(file) {
    return file && file["type"].split("/")[0] === "image";
  }
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
  async handleImageChange(e, source) {
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
      // Array.from(e.target.files).map(file => {
      //   URL.revokeObjectURL(file);
      // });
      await this.setState({ fileName: origin[0].name });

    }
  }
  async handleAudioChange(e, source) {
    var origin = e.target.files;
    const filesArray = Array.from(e.target.files).map(file =>
      URL.createObjectURL(file)
    );
    await this.setState({ [source]: filesArray });
    this.audioRef.current.pause();
    this.audioRef.current.load();
    await this.setState({ fileName: origin[0].name });
  }
  render() {
    return (
      <Form
        action="/api/AES"
        className="body"
        method="POST"
        encType="multipart/form-data"
      >
        <h1>LSB WITH AES ENCRYPT/DECRYPT</h1>
        <FormGroup>
          <Label for="key">Key</Label>
          <Input
            id="key"
            name="key"
            placeholder="Enter key"
            type="text"
            required={true}
          />
        </FormGroup>

        <FormGroup>
          <Label for="message">Message</Label>
          <Input id="message" name="message" type="textarea" />
        </FormGroup>
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
        <FormGroup>
          <Label for="keySize">Key size (bit)</Label>
          <Input id="keySize" name="keySize" type="select" required={true}>
            <option value="128">128</option>
            <option value="192">192</option>
            <option value="256">256</option>
          </Input>
        </FormGroup>
        <FormGroup>
          <Label for="keySize">Action</Label>
          <Input id="action" name="action" type="select" required={true}>
            <option value="Encrypt">Encrypt</option>
            <option value="Decrypt">Decrypt</option>
          </Input>
        </FormGroup>
        <Button color="primary">Submit</Button>
      </Form>
    );
  }
}
