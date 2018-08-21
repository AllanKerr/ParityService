import axios from 'axios';

const updateAllocationsType = 'UPDATE_ALLOCATIONS';
const initialState = { allocations: {} };

export const actionCreators = {
  update: (xsrfToken, data) => async dispatch => {
    const config = {
      headers: {
        RequestVerificationToken: xsrfToken
      }
    };

    axios
      .put('allocations/update', { allocations: data }, config)
      .then(response => {
        dispatch({
          type: updateAllocationsType,
          allocations: data
        });
      })
      .catch(error => {
        console.log(error);
      });
  }
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === updateAllocationsType) {
    return {
      ...state,
      allocations: action.allocations
    };
  }

  return state;
};
