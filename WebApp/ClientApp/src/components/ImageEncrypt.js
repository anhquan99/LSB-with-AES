import React, { Component } from 'react';
import { Form, Label, Input, FormGroup, FormText, Button } from 'reactstrap'
export class ImageEncrypt extends Component {
  state = {
    file: [],
    message: "",
    keySize: "",
    key: ""
  }
  //   renderPhotos(source,state) {
  //     return source.map((photo,i) => {
  //         return (<div 
  //             id={i}
  //             key = {photo} 
  //             ref = {this.setRef} style={{
  //             border: "1px solid rgb(222, 222, 222)",
  //             height: '250px',
  //             backgroundImage: `url(${photo})`,
  //             backgroundColor: "white",
  //             backgroundPosition: "center",
  //             backgroundRepeat: "no-repeat",
  //             backgroundSize: "contain",
  //             position: "relative"
  //         }}>
  //             <button type="button" className="close" aria-label="Close" onClick={() => this.removeMultiPhotos({ photo}, source , state )}>
  //                 <span aria-hidden="true">&times;</span>
  //             </button>
  //         </div>)
  //     })
  // }
  render() {
    return (

      <Form>
        <h1>Image encrypt</h1>
        <FormGroup>
          <Label for="exampleEmail">
            Key
          </Label>
          <Input
            id="exampleEmail"
            name="email"
            placeholder="Enter key"
            type="text"
          />
        </FormGroup>

        <FormGroup>
          <Label for="exampleText">
            Message
          </Label>
          <Input
            id="exampleText"
            name="text"
            type="textarea"
          />
        </FormGroup>
        <FormGroup>
          <Label for="exampleFile">
            Image
          </Label>
          <div className="custom-file">
            <input type="file" required={true} className="custom-file-input" id="productImage" multiple="multiple" accept="image/*" onChange={(e) => this.handleMultiImageChange(e, "img", "imgFiles")} />
            <label className="custom-file-label" htmlFor="productImage">Choose file</label>
          </div>
          <FormText>
            This is some placeholder block-level help text for the above input. It's a bit lighter and easily wraps to a new line.
          </FormText>
          {/* <div className="grid">{this.renderPhotos(this.state.wallpaper, "wallpaper")}</div> */}
        </FormGroup>
        <FormGroup>
          <Label for="exampleSelect">
            Key size (bit)
          </Label>
          <Input
            id="exampleSelect"
            name="select"
            type="select"
          >
            <option>
              128
            </option>
            <option>
              192
            </option>
            <option>
              256
            </option>
          </Input>
        </FormGroup>
        <FormGroup>
          <Label for="exampleSelect">
            Action
          </Label>
          <Input
            id="exampleSelect"
            name="select"
            type="select"
          >
            <option>
              Encrypt
            </option>
            <option>
              Decrypt
            </option>
          </Input>
        </FormGroup>
        <Button>
          Submit
        </Button>
      </Form>
    );
  }
}
