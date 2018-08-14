import React, { Component } from 'react';
import { connect } from 'react-redux';
import axios from 'axios';
import register from '../registerServiceWorker';

class Home extends Component {
  register(event) {
    event.preventDefault();

    const data = {
      email: event.target.email.value,
      password: event.target.password.value,
      confirmPassword: event.target.confirmPassword.value
    };
    axios
      .post('account/register', data)
      .then(response => {
        console.log(response);
      })
      .catch(error => {
        console.log(error);
      });
  }

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

  logOut() {
    axios
      .post('account/logout', null)
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
        <h1>Hello, world!</h1>
        <form type="submit" onSubmit={this.register}>
          <div>
            <input type="text" name="email" placeholder="Email" required />
          </div>
          <div>
            <input
              type="password"
              name="password"
              placeholder="Password"
              required
            />
          </div>
          <div>
            <input
              type="password"
              name="confirmPassword"
              placeholder="Confirm Password"
              required
            />
          </div>
          <button>Register</button>
        </form>

        <form type="submit" onSubmit={this.logIn}>
          <div>
            <input type="text" name="email" placeholder="Email" required />
          </div>
          <div>
            <input
              type="password"
              name="password"
              placeholder="Password"
              required
            />
          </div>
          <div>
            <input type="checkbox" name="rememberMe" />
          </div>
          <button>Log In</button>
        </form>

        <button onClick={this.logOut}>Log Out</button>
      </div>
    );
  }
}

export default connect()(Home);
