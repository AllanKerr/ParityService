import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Link } from 'react-router-dom';
import { FormInput, FormSubmitButton } from './forms/FormComponents';
import { actionCreators } from '../store/Login';
import { Redirect } from 'react-router-dom';

class LoginForm extends Component {
  logIn = event => {
    event.preventDefault();

    const target = event.target;
    const data = {
      email: target.Email.value,
      password: target.Password.value,
      rememberMe: target.RememberMe.checked
    };
    this.props.login(data);
  };

  render() {
    if (this.props.user != null) {
      return <Redirect to="/counter" />;
    }
    console.log(this.props);
    return (
      <div>
        <h1>Sign into Parity</h1>
        <form type="submit" onSubmit={this.logIn}>
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
          <div className="form-row form-row-multi">
            <label htmlFor="remember-me">
              <input type="checkbox" name="RememberMe" id="remember-me" />
              Remember me
            </label>
            <Link className="link" to="/counter">
              Forgot password?
            </Link>
          </div>
          <FormSubmitButton text="Login" errors={this.props.errors} />
        </form>
        <Link className="link" to="/register">
          Create an account
        </Link>
      </div>
    );
  }
}

export default connect(
  state => ({ ...state.user, ...state.login }),
  dispatch => bindActionCreators(actionCreators, dispatch)
)(LoginForm);
