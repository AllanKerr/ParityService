import React, { Component } from 'react';
import './DocumentPage.css';
import Loadable from './loading/Loadable';

class DocumentPage extends Component {
  render() {
    return (
      <div className="document-container">
        <div className="card">
          <Loadable loading={this.props.loading}>
            <div className="card-content">{this.props.children}</div>
          </Loadable>
        </div>
      </div>
    );
  }
}

DocumentPage.defaultProps = {
  loading: false
};

export default DocumentPage;
