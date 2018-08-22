import axios from 'axios';
import { actionCreators as userActionCreators } from './User';

const setLoginErrorsType = 'SET_LOGIN_ERRORS';
const initialState = { errors: [] };

export const actionCreators = {
  login: (xsrfToken, data) => async dispatch => {
    const config = {
      headers: {
        RequestVerificationToken: xsrfToken
      }
    };

    axios
      .post('account/login', data, config)
      .then(response => {
        dispatch(userActionCreators.setUser(response.data));
      })
      .catch(error => {
        console.log(error.response);
        dispatch({
          type: setLoginErrorsType,
          errors: error.response.data
        });
      });
  },

  clearErrors: () => {
    return {
      type: setLoginErrorsType,
      errors: []
    };
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === setLoginErrorsType) {
    return {
      ...state,
      errors: action.errors
    };
  }

  return state;
};
