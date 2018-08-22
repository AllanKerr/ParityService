import React from 'react';
import './DocumentPage.css';

const DocumentPage = props => (
  <div className="document-container">
    <div className="card">{props.children}</div>
  </div>
);

export default DocumentPage;
