import axios from 'axios';
import { actionCreators as userActionCreators } from './User';

const receiveLoginResponseType = 'RECEIVE_LOGIN_RESPONSE';
const initialState = { errors: [], isLoading: false };

export const actionCreators = {
  login: data => async dispatch => {
    axios
      .post('account/login', data)
      .then(response => {
        dispatch(
          userActionCreators.setUser({ name: 'John Doe', role: 'admin' })
        );
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
