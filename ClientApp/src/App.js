import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import RegisterView from './components/RegisterView';
import LoginView from './components/LoginView';
import LandingView from './components/LandingView';
import HomeView from './components/HomeView';

export default () => (
  <Layout>
    <Route exact path="/" component={LandingView} />
    <Route path="/login" component={LoginView} />
    <Route path="/register" component={RegisterView} />
    <Route path="/home" component={HomeView} />
    <Route path="/counter" component={Counter} />
    <Route path="/fetchdata/:startDateIndex?" component={FetchData} />
  </Layout>
);
