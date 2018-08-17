import React from 'react';
import FormError from './FormError';
import '../Style.css';

const FormInput = props => (
  <div className="form-row">
    <input
      type={props.type}
      name={props.name}
      placeholder={props.placeholder}
      required
    />
    <FormError name={props.name} errors={props.errors} />
  </div>
);

const FormSubmitButton = props => (
  <div className="form-row">
    <button className="button primary large form-submit-button">
      {props.text}
    </button>
    <FormError errors={props.errors} />
  </div>
);

export { FormInput, FormSubmitButton };
