import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import RegisterView from './components/RegisterView';
import LoginView from './components/LoginView';

export default () => (
  <Layout>
    <Route exact path="/" component={Home} />
    <Route path="/login" component={LoginView} />
    <Route path="/register" component={RegisterView} />
    <Route path="/counter" component={Counter} />
    <Route path="/fetchdata/:startDateIndex?" component={FetchData} />
  </Layout>
);
