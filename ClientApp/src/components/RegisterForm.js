import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { FormInput, FormSubmitButton } from './forms/FormComponents';
import { actionCreators } from '../store/Register';
import XsrfProtection from './security/XsrfProtection';
import { bindActionCreators } from 'redux';

class RegisterForm extends Component {
  register = event => {
    event.preventDefault();

    const target = event.target;
    const data = {
      email: target.Email.value,
      password: target.Password.value,
      confirmPassword: target.ConfirmPassword.value
    };
    this.props.register(this.props.xsrfToken, data);
  };

  render() {
    return (
      <div>
        <h1>Create an account.</h1>
        <form type="submit" onSubmit={this.register}>
          <FormInput
            type="email"
            name="Email"
            placeholder="Email address"
            errors={this.props.errors}
          />
          <FormInput
            type="password"
            name="Password"
            placeholder="Password"
            errors={this.props.errors}
          />
          <FormInput
            type="password"
            name="ConfirmPassword"
            placeholder="Confirm Password"
            errors={this.props.errors}
          />

          <FormSubmitButton text="Sign Up" errors={this.props.errors} />
        </form>
        <Link className="link" to="/login">
          Already have an account?
        </Link>
      </div>
    );
  }
}

export default connect(
  state => state.register,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(XsrfProtection(RegisterForm));
