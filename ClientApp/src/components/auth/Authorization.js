import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../store/User';
import { Redirect } from 'react-router-dom';

const Authorization = WrappedComponent => {
  const WithAuthorization = props => {
    if (props.user == null) {
      return <Redirect to="/login" />;
    }
    return <WrappedComponent {...props} />;
  };

  return connect(
    state => state.account,
    dispatch => bindActionCreators(actionCreators, dispatch)
  )(WithAuthorization);
};

export default Authorization;
