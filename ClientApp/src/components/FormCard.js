import React from 'react';
import './Style.css';

const FormCard = props => (
  <div className="card-container">
    <div className="card form">
      <div className="card-content">
        <div>{props.children}</div>
      </div>
    </div>
  </div>
);

export default FormCard;
