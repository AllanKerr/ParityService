import axios from 'axios';
import { actionCreators as allocationsPickerActionCreators } from './AllocationsPicker';

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
        console.log(response);
      })
      .catch(error => {
        console.log(error.response.data);
      });
  }
};
