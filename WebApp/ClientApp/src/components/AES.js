import React, { Component } from "react";
import {
    Form,
    Label,
    Input,
    FormGroup,
    Button,
    Card,
    Alert,
    CardText,
    CardTitle,
    Row,
    Col,
    Spinner
} from "reactstrap";
import axios from "axios";
export class AES extends Component {
    state = {
        result: "",
        action: "Encrypt",
        isSubmit: false
    }
    copyToClipBoard() {
        navigator.clipboard.writeText(this.state.result);
        alert("Copied text to clipboard");
    }
    async handleFormSubmit(e) {
        this.setState({ isSubmit: true })
        e.preventDefault();
        const formData = new FormData(e.target);
        if (this.state.action.toLowerCase() === "encrypt") {
            await axios.post("/api/AES/encrypt_text", formData).then((response) => {
                this.setState({ result: response.data });
            }).catch((response) => {
                alert(response);
            });
        }
        else {
            await axios.post("/api/AES/decrypt_text", formData).then((response) => {
                this.setState({ result: response.data });
            }).catch((response) => {
                alert(response);
            });
        }
        this.setState({ isSubmit: false })
    }
    render() {
        return (
            <div>
                <Form
                    action="/api/AES/"
                    className="body"
                    onSubmit={e => this.handleFormSubmit(e)}
                >
                    <h1>AES ENCRYPT/DECRYPT</h1>
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
                    <FormGroup
                        check
                        inline
                    >
                        <Input id="isByte" name="isByte" type="checkbox" />
                        <Label check>
                            Is byte?
                        </Label>
                    </FormGroup>
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
                        <Input id="action" name="action" type="select" required={true} onChange={e => {
                            this.setState({ action: e.target.value });
                        }}>
                            <option value="Encrypt">Encrypt</option>
                            <option value="Decrypt">Decrypt</option>
                        </Input>
                    </FormGroup>
                    {this.state.isSubmit === true ? <Col><Spinner animation="border" /></Col> : <Button color="primary">Submit</Button>}
                </Form>
                {/* {this.state.result !== "" ? <div>RESULT: {this.state.result}</div> : ""} */}
                {this.state.result !== "" ?

                    <Card body>
                        <Row>
                            <Col xs="11">
                                <CardTitle tag="h5">
                                    Result {this.state.action}
                                </CardTitle>
                            </Col>
                            <Col xs="1">
                                <Button id="copy" outline color="primary" onClick={() => this.copyToClipBoard()} >
                                    Copy
                                </Button>
                            </Col>

                        </Row>
                        <br />
                        <Alert
                            color="success"
                        >
                            {this.state.result}
                        </Alert>
                    </Card> : ""}
            </div>
        );
    }
}