import axios from 'axios';

const receiveLoginResponseType = 'RECEIVE_LOGIN_RESPONSE';
const initialState = { errors: [], isLoading: false };

export const actionCreators = {
  login: data => async dispatch => {
    axios
      .post('account/login', data)
      .then(response => {
        console.log('ACTION CREATOR');
        console.log(response);
      })
      .catch(error => {
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
