import React from 'react';
import './Loadable.css';
import LoadingIndicator from './LoadingIndicator';

export default props => {
  var className;
  if (props.loading) {
    className = 'overlay';
  } else {
    className = 'overlay hidden';
  }

  return (
    <div className="overlay-container">
      <div className={className}>
        <LoadingIndicator />
      </div>
      <div className="layout">{props.children}</div>
    </div>
  );
};
