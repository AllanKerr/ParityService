import axios from 'axios';
import { actionCreators as userActionCreators } from './User';

const setRegisterErrorsType = 'SET_REGISTER_ERRORS';
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
          type: setRegisterErrorsType,
          errors: error.response.data
        });
      });
  },
  clearErrors: () => ({ type: setRegisterErrorsType, errors: [] })
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === setRegisterErrorsType) {
    return {
      ...state,
      errors: action.errors
    };
  }

  return state;
};
