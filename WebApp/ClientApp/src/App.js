import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { ImageEncrypt } from './components/ImageEncrypt';
import { AudioEncrypt } from './components/AudioEncrypt';
import { Counter } from './components/Counter';

import 'bootstrap/dist/css/bootstrap.min.css';
import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={ImageEncrypt} />
        <Route path='/counter' component={AudioEncrypt} />
        {/* <Route path='/fetch-data' component={FetchData} /> */}
      </Layout>
    );
  }
}
