import axios from 'axios';
import { actionCreators as userActionCreators } from './User';

const receiveRegisterResponseType = 'RECEIVE_REGISTER_RESPONSE';
const initialState = { errors: [] };

export const actionCreators = {
  register: (xsrfToken, data) => async dispatch => {
    const config = {
      headers: {
        RequestVerificationToken: xsrfToken
      }
    };

    axios
      .post('account/register', data, config)
      .then(response => {
        dispatch(userActionCreators.setUser(response.data));
      })
      .catch(error => {
        console.log(error);
        dispatch({
          type: receiveRegisterResponseType,
          errors: error.response.data
        });
      });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === receiveRegisterResponseType) {
    return {
      ...state,
      errors: action.errors
    };
  }

  return state;
};
