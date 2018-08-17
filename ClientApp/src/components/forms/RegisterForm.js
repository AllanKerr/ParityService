import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { FormInput, FormSubmitButton } from './FormComponents';
import { actionCreators } from '../../store/Register';
import { bindActionCreators } from 'redux';

class RegisterForm extends Component {
  onSubmit = event => {
    event.preventDefault();
    this.props.onSubmit(event);
  };

  render() {
    return (
      <div>
        <h1>Create an account.</h1>
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
)(RegisterForm);
