import React, { Component } from 'react';
import DocumentPage from './DocumentPage';
import InlineForm from './forms/InlineForm';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import XsrfProtection from './security/XsrfProtection';
import { actionCreators } from '../store/AccountLinks';
import AccountLinkItem from './AccountLinkItem';

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

        <ul>
          {this.props.accountLinks.map(accountLink => (
            <AccountLinkItem accountLink={accountLink} />
          ))}
        </ul>
      </DocumentPage>
    );
  }
}

export default connect(
  state => state.accountLinks,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(XsrfProtection(AccountLinkView));
