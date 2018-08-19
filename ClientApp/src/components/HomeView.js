import React from 'react';
import Authorization from './auth/Authorization';
import './HomeView.css';
import AssetPicker from './AssetPicker';

const HomeView = props => (
  <div className="document-container">
    <div className="card">
      <AssetPicker
        assets={{
          VCN: 22,
          XIC: 52,
          XEF: 26
        }}
      />
    </div>
  </div>
);

export default Authorization(HomeView);
