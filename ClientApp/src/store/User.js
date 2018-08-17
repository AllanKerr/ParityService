const SET_USER = 'SET_USER';
const initialState = { user: null };

export const actionCreators = {
  setUser: user => ({ type: SET_USER, user })
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === SET_USER) {
    return {
      ...state,
      user: action.user
    };
  }

  return state;
};

export const middleware = store => next => action => {
  const result = next(action);
  if (action.type === SET_USER) {
    if (action.user == null) {
      localStorage.clear();
    } else {
      localStorage.setItem('user', JSON.stringify(result.user));
    }
  }
  return result;
};
