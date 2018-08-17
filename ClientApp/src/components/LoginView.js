import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import LoginForm from './LoginForm';
import FormCard from './FormCard';
import XsrfProtection from './security/XsrfProtection';
import { actionCreators } from '../store/Login';
import { Redirect } from 'react-router-dom';

class LoginView extends Component {
  onSubmit = event => {
    const target = event.target;
    const data = {
      email: target.Email.value,
      password: target.Password.value,
      rememberMe: target.RememberMe.checked
    };
    this.props.login(this.props.xsrfToken, data);
  };

  render() {
    if (this.props.user != null) {
      return <Redirect to="/home" />;
    }
    return (
      <FormCard>
        <LoginForm onSubmit={this.onSubmit} />
      </FormCard>
    );
  }
}

export default connect(
  state => state.account,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(XsrfProtection(LoginView));
