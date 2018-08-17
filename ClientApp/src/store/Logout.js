import axios from 'axios';
import { actionCreators as userActionCreators } from './User';

const receiveLogoutResponseType = 'RECEIVE_LOGOUT_RESPONSE';
const initialState = { errors: [] };

export const actionCreators = {
  logout: xsrfToken => async dispatch => {
    const config = {
      headers: {
        RequestVerificationToken: xsrfToken
      }
    };

    axios
      .post('account/logout', null, config)
      .then(response => {
        dispatch(userActionCreators.setUser(null));
      })
      .catch(error => {
        dispatch({
          type: receiveLogoutResponseType,
          errors: error.response.data
        });
      });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === receiveLogoutResponseType) {
    return {
      ...state,
      errors: action.errors
    };
  }

  return state;
};
