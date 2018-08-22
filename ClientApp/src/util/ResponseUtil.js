const DEFAULT_ERROR = {
  '': ['An unknown error occurred.']
};

const isJson = headers => {
  if (!headers || !headers['content-type']) {
    return false;
  }
  return headers['content-type']
    .split(';')
    .map(component => component.trim())
    .includes('application/json');
};

const getResponseErrors = response => {
  if (!response) {
    return DEFAULT_ERROR;
  }
  if (isJson(response.headers) && response.data) {
    return response.data;
  }
  if (!response.statusText) {
    return DEFAULT_ERROR;
  }
  const errorMessage = `An unexpected error occurred: ${response.statusText.toLowerCase()}.`;
  return { '': [errorMessage] };
};

export { getResponseErrors };
