import React from 'react';
import '../Style.css';

const FormError = props => {
  const name = props.name !== undefined ? props.name : '';

  var errors;
  var hasErrors = false;
  if (props.errors != null) {
    errors = props.errors[name];
    hasErrors = errors !== undefined && errors.length > 0;
  }
  if (!hasErrors) {
    errors = [];
  }

  return (
    <ul className="form-error-container" has-errors={hasErrors.toString()}>
      {errors.map((error, i) => (
        <li className="form-error" key={i}>
          {error}
        </li>
      ))}
    </ul>
  );
};

export default FormError;
