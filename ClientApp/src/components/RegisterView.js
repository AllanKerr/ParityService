import React, { Component } from 'react';
import RegisterForm from './RegisterForm';
import FormCard from './FormCard';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import XsrfProtection from './security/XsrfProtection';
import { actionCreators } from '../store/Register';
import { Redirect } from 'react-router-dom';

class RegisterView extends Component {
  onSubmit = event => {
    const target = event.target;
    const data = {
      email: target.Email.value,
      password: target.Password.value,
      confirmPassword: target.ConfirmPassword.value
    };
    this.props.register(this.props.xsrfToken, data);
  };

  render() {
    if (this.props.user != null) {
      return <Redirect to="/home" />;
    }
    return (
      <FormCard>
        <RegisterForm onSubmit={this.onSubmit} />
      </FormCard>
    );
  }
}

export default connect(
  state => state.account,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(XsrfProtection(RegisterView));
