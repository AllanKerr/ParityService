import React, { Component } from 'react';
import './HomeView.css';
import AssetPicker from './AssetPicker';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import XsrfProtection from './security/XsrfProtection';
import { actionCreators } from '../store/Allocations';
import Authorization from './auth/Authorization';

class HomeView extends Component {
  saveAssets = nextAssets => {
    console.log(nextAssets);
    this.props.update(this.props.xsrfToken, nextAssets);
  };

  render() {
    return (
      <div className="document-container">
        <div className="card">
          <AssetPicker
            onSave={this.saveAssets}
            assets={this.props.allocations}
          />
        </div>
      </div>
    );
  }
}

export default connect(
  state => state.allocations,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(XsrfProtection(Authorization(HomeView)));
