import React from 'react';
import Authorization from './auth/Authorization';

const HomeView = () => <h1>Home</h1>;

export default Authorization(HomeView);
