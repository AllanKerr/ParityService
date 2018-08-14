import React from 'react';
import './Style.css';

const FormCard = props => (
  <div className="card-container">
    <div className="card">
      <div>{props.children}</div>
    </div>
  </div>
);

export default FormCard;
