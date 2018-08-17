import React, { Component } from 'react';
import LoginForm from './LoginForm';
import FormCard from './FormCard';

class LoginView extends Component {
  render() {
    return (
      <FormCard>
        <LoginForm />
      </FormCard>
    );
  }
}

export default LoginView;
