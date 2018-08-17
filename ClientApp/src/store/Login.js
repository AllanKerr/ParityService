import axios from 'axios';
import { actionCreators as userActionCreators } from './User';

const receiveLoginResponseType = 'RECEIVE_LOGIN_RESPONSE';
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
        console.log(error);
        dispatch({
          type: receiveLoginResponseType,
          errors: error.response.data
        });
      });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === receiveLoginResponseType) {
    return {
      ...state,
      errors: action.errors
    };
  }

  return state;
};
