import axios from 'axios';
import { actionCreators as allocationsPickerActionCreators } from './AllocationsPicker';
import { getResponseErrors } from '../util/ResponseUtil';

const updateAllocationsType = 'UPDATE_ALLOCATIONS';
const initialState = { allocations: {} };

export const actionCreators = {
  update: (xsrfToken, data) => async dispatch => {
    dispatch(allocationsPickerActionCreators.startLoading());

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
        dispatch(allocationsPickerActionCreators.finishLoading({}));
      })
      .catch(error => {
        var errors = getResponseErrors(error.response);
        console.log(errors);
        dispatch(allocationsPickerActionCreators.finishLoading(errors));
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
