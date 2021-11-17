import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Index } from './components/Index';

import 'bootstrap/dist/css/bootstrap.min.css';
import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Index} />
      </Layout>
    );
  }
}
