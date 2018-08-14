import React, { Component } from 'react';
import { connect } from 'react-redux';
import axios from 'axios';
import LoginFrom from './LoginForm';
import FormCard from './FormCard';

class Home extends Component {
  logIn(event) {
    event.preventDefault();

    const data = {
      email: event.target.email.value,
      password: event.target.password.value,
      rememberMe: event.target.rememberMe.checked
    };
    axios
      .post('account/login', data)
      .then(response => {
        console.log(response);
      })
      .catch(error => {
        console.log(error);
      });
  }

  render() {
    return (
      <FormCard>
        <LoginFrom />
      </FormCard>
    );
  }
}

export default connect()(Home);
