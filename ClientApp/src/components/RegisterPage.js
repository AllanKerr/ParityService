import React, { Component } from 'react';
import RegisterForm from './RegisterForm';
import FormCard from './FormCard';

class RegisterPage extends Component {
  render() {
    return (
      <FormCard>
        <RegisterForm />
      </FormCard>
    );
  }
}

export default RegisterPage;
