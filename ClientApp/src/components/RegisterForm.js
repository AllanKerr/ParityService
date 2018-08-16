import React, { Component } from 'react';
import { connect } from 'react-redux';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { FormInput, FormSubmitButton } from './forms/FormComponents';
import { withCookies, Cookies } from 'react-cookie';

class RegisterForm extends Component {
  state = {
    errors: {}
  };

  register = event => {
    event.preventDefault();

    const target = event.target;
    const data = {
      email: target.Email.value,
      password: target.Password.value,
      confirmPassword: target.ConfirmPassword.value
    };

    const token = this.props.cookies.get('XSRF-TOKEN');

    const config = {
      headers: {
        RequestVerificationToken: token
      }
    };

    console.log(config);

    axios
      .post('account/register', data, config)
      .then(response => {
        console.log(response);
      })
      .catch(error => {
        this.setState({ errors: error.response.data });
      });
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
            errors={this.state.errors}
          />
          <FormInput
            type="password"
            name="Password"
            placeholder="Password"
            errors={this.state.errors}
          />
          <FormInput
            type="password"
            name="ConfirmPassword"
            placeholder="Confirm Password"
            errors={this.state.errors}
          />

          <FormSubmitButton text="Sign Up" errors={this.state.errors} />
        </form>
        <Link className="link" to="/">
          Already have an account?
        </Link>
      </div>
    );
  }
}

export default connect()(withCookies(RegisterForm));
