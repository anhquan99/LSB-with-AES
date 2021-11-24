import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Index } from './components/Index';
import { AES } from './components/AES';
import { LSB } from './components/LSB';
import { Analyze } from './components/Analyze';

import 'bootstrap/dist/css/bootstrap.min.css';
import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Index} />
        <Route exact path='/AES' component={AES} />
        <Route exact path='/LSB' component={LSB} />
        <Route exact path='/analyze' component={Analyze} />
      </Layout>
    );
  }
}
