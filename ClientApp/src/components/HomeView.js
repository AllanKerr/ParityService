import React, { Component } from 'react';
import Authorization from './auth/Authorization';
import './HomeView.css';
import AssetPicker from './AssetPicker';
import SearchBar from './SearchBar';

class HomeView extends Component {
  state = {
    assets: {
      VCN: 22,
      XIC: 52,
      XEF: 26
    }
  };

  saveAssets = nextAssets => {
    console.log(nextAssets);
    this.setState({ assets: nextAssets });
  };

  render() {
    return (
      <div className="document-container">
        <div className="card">
          <AssetPicker onSave={this.saveAssets} assets={this.state.assets} />
        </div>
      </div>
    );
  }
}

export default Authorization(HomeView);
