import axios from 'axios';

const ADD_ACCOUNT_LINK = 'ADD_ACCOUNT_LINK';
const initialState = { accountLinks: [] };

export const actionCreators = {
  addAccountLink: (xsrfToken, data) => async dispatch => {
    const config = {
      headers: {
        RequestVerificationToken: xsrfToken
      }
    };

    axios
      .post('accountlinks/add', data, config)
      .then(response => {
        console.log(response.data);
        dispatch({ type: ADD_ACCOUNT_LINK, accountLink: response.data });
      })
      .catch(error => {
        console.log(error);
      });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === ADD_ACCOUNT_LINK) {
    console.log(action.accountLink);

    return {
      ...state,
      accountLinks: [...state.accountLinks, action.accountLink]
    };
  }

  return state;
};
