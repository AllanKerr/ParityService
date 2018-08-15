import React, { Component } from 'react';
import { connect } from 'react-redux';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { FormInput, FormSubmitButton } from './FormInput';

class LoginForm extends Component {
  state = {
    errors: {}
  };

  logIn = event => {
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
        console.log(error.response.data);
        this.setState({ errors: error.response.data });
      });
  };

  render() {
    return (
      <div>
        <h1>Sign into Parity</h1>
        <form type="submit" onSubmit={this.logIn}>
          <FormInput
            type="email"
            name="Email"
            placeholder="Email address"
            errors={this.state.errors}
          />
          <FormInput
            type="password"
            name="Password"
            placeholder="Password"
            errors={this.state.errors}
          />
          <div className="form-row form-row-multi">
            <label htmlFor="remember-me">
              <input type="checkbox" name="rememberMe" id="remember-me" />
              Remember me
            </label>
            <Link className="link" to="/counter">
              Forgot password?
            </Link>
          </div>
          <FormSubmitButton text="Login" errors={this.state.errors} />
        </form>
        <Link className="link" to="/register">
          Create an account
        </Link>
      </div>
    );
  }
}

export default connect()(LoginForm);
