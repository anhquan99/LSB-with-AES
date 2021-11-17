import React, { Component } from 'react';
import { NavMenu } from './NavMenu';
import { Form, Label, Input, FormGroup, FormText, Button } from 'reactstrap'

export class AudioEncrypt extends Component {

  render () {
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
          Audio
        </Label>
        <div className="custom-file">
          <input type="file" required={true} className="custom-file-input" id="productImage" multiple="multiple" accept="audio/.wav" onChange={(e) => this.handleMultiImageChange(e, "img", "imgFiles")} />
          <label className="custom-file-label" htmlFor="productImage">Choose file</label>
        </div>
        <FormText>
          This is some placeholder block-level help text for the above input. It's a bit lighter and easily wraps to a new line.
        </FormText>
        {/* <div className="grid">{this.renderPhotos(this.state.wallpaper, "wallpaper")}</div> */}
      </FormGroup>
      <FormGroup tag="fieldset">
        <legend>
          Key size
        </legend>
        <FormGroup check>
          <Input
            name="radio1"
            type="radio"
          />
          {' '}
          <Label check>
            128
          </Label>
        </FormGroup>
        <FormGroup check>
          <Input
            name="radio1"
            type="radio"
          />
          {' '}
          <Label check>
            192
          </Label>
        </FormGroup>
        <FormGroup
          check
          disabled
        >
          <Input
            name="radio1"
            type="radio"
          />
          {' '}
          <Label check>
            256
          </Label>
        </FormGroup>
      </FormGroup>
      <Button>
        Submit
      </Button>
    </Form>
    );
  }
}
