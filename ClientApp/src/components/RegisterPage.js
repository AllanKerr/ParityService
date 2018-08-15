import React, { Component } from 'react';
import { connect } from 'react-redux';
import axios from 'axios';
import RegisterForm from './RegisterForm';
import FormCard from './FormCard';

class RegisterPage extends Component {
  register(event) {
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
        <RegisterForm />
      </FormCard>
    );
  }
}

export default connect()(RegisterPage);
