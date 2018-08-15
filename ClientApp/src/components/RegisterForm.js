import React, { Component } from 'react';
import { connect } from 'react-redux';
import axios from 'axios';
import { Link } from 'react-router-dom';

class RegisterForm extends Component {
  logIn(event) {
    event.preventDefault();

    const data = {
      email: event.target.email.value,
      password: event.target.password.value
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
        <h1>Create an account.</h1>
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
          <div className="form-row">
            <input
              type="password"
              name="confirmPassword"
              placeholder="Confirm Password"
              required
            />
          </div>
          <button className="form-submit-button">Sign up</button>
        </form>
        <Link className="link" to="/">
          Already have an account?
        </Link>
      </div>
    );
  }
}

export default connect()(RegisterForm);
