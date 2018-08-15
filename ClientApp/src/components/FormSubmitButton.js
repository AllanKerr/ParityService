import React from 'react';
import FormError from './FormError';
import './Style.css';

const FormSubmitButton = props => (
  <div className="form-row">
    <button className="form-submit-button">{props.text}</button>
    <FormError errors={props.errors} />
  </div>
);

export default FormSubmitButton;
