import React, { Component } from 'react';
import axios from "axios";
import { Form, Label, Input, FormGroup, FormText, Button, Col, Row } from 'reactstrap'
export class Index extends Component {
  state = {
    fileType: "Image",
    file: [],
    message: "",
    keySize: "",
    key: "",
    result: ""
  }
  async handleFormSubmit(e) {
    e.preventDefault();
    // fetch("api/AES")
    //   .then((response) => response.json())
    //   .then((data) => console.log(data));

    var object = {};
    const formData = new FormData(e.target);
    formData.forEach(function (value, key) {
      object[key] = value;
    });
    for (var key of formData.entries()) {
      console.log(key[0] + ', ' + key[1])
    }
    axios.post("/api/AES", formData, {
      headers: { 'Content-Type': 'application/json' }
    }).then((response) =>{
      console.log("response : " + response);
      // this.setState({result : temp});
    }).catch(function(error){
      console.log(error);
    });

    // postWithFile("/api/Product", formData).then((response) => {
    //     if (response.status === 200) {
    //         alert("Success");
    //         window.location = "/";
    //     }
    // }).catch(function (error) {
    //     console.log("POST Product field");
    //     return Promise.reject(error);
    // });
  }
  isFileImage(file) {
    return file && file['type'].split('/')[0] === 'image';
  }
  renderPhotos(source, state) {
    return source.map((photo, i) => {
      return (<div
        id={i}
        key={photo}
        ref={this.setRef} style={{
          border: "1px solid rgb(222, 222, 222)",
          height: '250px',
          backgroundImage: `url(${photo})`,
          backgroundColor: "white",
          backgroundPosition: "center",
          backgroundRepeat: "no-repeat",
          backgroundSize: "contain",
          position: "relative"
        }}>
      </div>)
    })
  }
  handleImageChange(e, source) {
    if (e.target.files) {
      for (let i = 0; i < e.target.files.length; i++) {
        if (!this.isFileImage(e.target.files[i])) {
          this.setState({ message: "INVALID IMAGE FILE" });
          this.setState({ [source]: [] });
          return;
        }
      }
      const filesArray = Array.from(e.target.files).map((file) =>
        URL.createObjectURL(file)
      );

      this.setState({ [source]: filesArray });
      Array.from(e.target.files).map(
        (file) => {
          URL.revokeObjectURL(file);
        }
      );

      this.setState({ message: "" });
    }
  };
  render() {
    return (

      <Form className="body" onSubmit={(e) => this.handleFormSubmit(e)} encType="multipart/form-data">
        <h1>LSB WITH AES ENCRYPT/DECRYPT</h1>
        <FormGroup>
          <Label for="key">
            Key
          </Label>
          <Input
            id="key"
            name="key"
            placeholder="Enter key"
            type="text"
            required="true"
          />
        </FormGroup>

        <FormGroup>
          <Label for="message">
            Message
          </Label>
          <Input
            id="message"
            name="message"
            type="textarea"
            required="true"
          />
        </FormGroup>
        <FormGroup>
          <Label for="fileType">
            File type
          </Label>
          <Input
            id="fileType"
            name="fileType"
            type="select"
            required="true"
            onChange={(e)=>{
                this.setState({fileType: e.target.value});
            }}
          >
            <option value="Image">
              Image
            </option>
            <option value="Audio">
              Audio
            </option>
          </Input>
        </FormGroup>
        <Row xs="2">
          <Col >
            <Label for="file">
              File
            </Label>
            <div className="custom-file">
              <input type="file" required={true} className="custom-file-input" id="file" name="file" accept={this.state.fileType == "Image" ? 'image/*' : 'audio/*'} onChange={(e) => this.handleImageChange(e, "file")} required="true" />
              <label className="custom-file-label" htmlFor="file">Choose file</label>
            </div>
            <FormText>
              Audio file support only for .wav file.
            </FormText>
          </Col>
          <Col>
            <div>{this.renderPhotos(this.state.file, "file")}</div>
          </Col>
        </Row>
        {/* <FormGroup>


        </FormGroup> */}
        <FormGroup>
          <Label for="keySize">
            Key size (bit)
          </Label>
          <Input
            id="keySize"
            name="keySize"
            type="select"
            required="true"
          >
            <option value="128">
              128
            </option>
            <option value="192">
              192
            </option>
            <option value="256">
              256
            </option>
          </Input>
        </FormGroup>
        <FormGroup>
          <Label for="keySize">
            Action
          </Label>
          <Input
            id="action"
            name="action"
            type="select"
            required="true"
          >
            <option value="Encrypt">
              Encrypt
            </option>
            <option value="Decrypt">
              Decrypt
            </option>
          </Input>
        </FormGroup>
        <Button color="primary">
          Submit
        </Button>
      </Form>
    );
  }
}
