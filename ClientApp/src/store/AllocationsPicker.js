const START_LOADING = 'START_LOADING';
const FINISH_LOADING = 'FINISH_LOADING';

const initialState = {
  loading: false,
  errors: {}
};

export const actionCreators = {
  startLoading: () => ({ type: START_LOADING }),
  finishLoading: errors => ({ type: FINISH_LOADING, errors }),
  clearErrors: () => ({ type: FINISH_LOADING, errors: {} })
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === START_LOADING) {
    return {
      ...state,
      loading: true,
      errors: {}
    };
  }

  if (action.type === FINISH_LOADING) {
    console.log(action.errors);
    return {
      ...state,
      loading: false,
      errors: action.errors
    };
  }

  return state;
};
