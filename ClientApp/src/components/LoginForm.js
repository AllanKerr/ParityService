import React, { Component } from 'react';
import { connect } from 'react-redux';
import axios from 'axios';
import { Link } from 'react-router-dom';

class LoginForm extends Component {
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
      <div>
        <h1>Sign into Parity</h1>
        <form type="submit" onSubmit={this.logIn}>
          <div className="form-row">
            <input
              type="text"
              name="email"
              placeholder="Email address"
              required
            />
          </div>
          <div className="form-row">
            <input
              type="password"
              name="password"
              placeholder="Password"
              required
            />
          </div>
          <div className="form-row form-row-multi">
            <label htmlFor="remember-me">
              <input type="checkbox" name="rememberMe" id="remember-me" />
              Remember Me
            </label>
            <Link className="link" to="/counter">
              Forgot password?
            </Link>
          </div>
          <button className="form-submit-button">Login</button>
        </form>
        <Link className="link" to="/counter">
          Create an account
        </Link>
      </div>
    );
  }
}

export default connect()(LoginForm);
