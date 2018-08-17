import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Link } from 'react-router-dom';
import { FormInput, FormSubmitButton } from './FormComponents';
import { actionCreators } from '../../store/Login';

class LoginForm extends Component {
  onSubmit = event => {
    event.preventDefault();
    this.props.onSubmit(event);
  };

  componentWillUnmount() {
    this.props.clearErrors();
  }

  render() {
    return (
      <div>
        <h1>Log in to Parity.</h1>
        <form type="submit" onSubmit={this.onSubmit}>
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
            <Link className="link" to="/">
              Forgot password?
            </Link>
          </div>
          <FormSubmitButton text="Log In" errors={this.props.errors} />
        </form>
        <Link className="link" to="/register">
          Create an account
        </Link>
      </div>
    );
  }
}

export default connect(
  state => state.login,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(LoginForm);
