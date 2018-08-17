import React, { Component } from 'react';
import RegisterForm from './RegisterForm';
import FormCard from './FormCard';

class RegisterView extends Component {
  render() {
    return (
      <FormCard>
        <RegisterForm />
      </FormCard>
    );
  }
}

export default RegisterView;
