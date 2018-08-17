import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Logout';
import XsrfProtection from './security/XsrfProtection';
import { bindActionCreators } from 'redux';

import './NavMenu.css';

class NavMenu extends Component {
  logout = event => {
    this.props.logout(this.props.xsrfToken);
  };

  render() {
    var userAction;
    if (this.props.user == null) {
      userAction = (
        <div>
          <Link to="/login" className="button primary medium">
            Log In
          </Link>
          <Link to="/register" className="button secondary medium">
            Sign Up
          </Link>
        </div>
      );
    } else {
      userAction = (
        <button onClick={this.logout} className="button secondary medium">
          Log Out
        </button>
      );
    }

    return (
      <header className="nav">
        <Link className="logo" to="/">
          Parity
        </Link>
        {userAction}
      </header>
    );
  }
}

export default connect(
  state => ({ ...state.account, ...state.logout }),
  dispatch => bindActionCreators(actionCreators, dispatch)
)(XsrfProtection(NavMenu));
