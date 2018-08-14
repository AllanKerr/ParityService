import React from 'react';
import NavMenu from './NavMenu';

export default props => (
  <div className="layout-container">
    <NavMenu />
    <div className="layout">{props.children}</div>
  </div>
);
