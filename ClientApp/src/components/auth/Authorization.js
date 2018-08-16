import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../store/User';
import { Redirect } from 'react-router-dom';

const Authorization = allowedRoles => WrappedComponent => {
  const WithAuthorization = props => {
    const loginRedirect = <Redirect to="/" />;
    if (props.user == null) {
      return loginRedirect;
    }
    return <WrappedComponent {...props} />;
  };

  return connect(
    state => state.account,
    dispatch => bindActionCreators(actionCreators, dispatch)
  )(WithAuthorization);
};

export default Authorization;
