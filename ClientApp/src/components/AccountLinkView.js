import React, { Component } from 'react';
import DocumentPage from './DocumentPage';
import InlineForm from './forms/InlineForm';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import XsrfProtection from './security/XsrfProtection';
import { actionCreators } from '../store/AccountLinks';

class AccountLinkView extends Component {
  state = {
    loading: false
  };

  onSubmit = event => {
    const target = event.target;
    const data = {
      refreshToken: target.Input.value,
      isPractice: target.IsPractice.checked
    };
    this.setState({ loading: true });
    this.props.addAccountLink(this.props.xsrfToken, data);
  };

  render() {
    return (
      <DocumentPage loading={this.state.loading}>
        <InlineForm
          disabled={this.state.loading}
          onSubmit={this.onSubmit}
          placeholder="Refresh Token"
          text="Link"
        >
          <label>
            Practice
            <input
              disabled={this.state.loading}
              name="IsPractice"
              type="checkbox"
            />
          </label>
        </InlineForm>
      </DocumentPage>
    );
  }
}

export default connect(
  _ => ({}),
  dispatch => bindActionCreators(actionCreators, dispatch)
)(XsrfProtection(AccountLinkView));
