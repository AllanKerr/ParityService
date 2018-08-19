import React, { Component } from 'react';
import './HomeView.css';
import AssetItem from './AssetItem';
import AssetItemTotal from './AssetItemTotal';

class AssetPicker extends Component {
  constructor(props) {
    super(props);
    this.state = {
      assets: props.assets
    };
  }

  changeTarget = (oldValue, newValue) => {
    const newTotal = this.state.total - oldValue + newValue;
    this.setState({ total: newTotal });
  };

  getAssets = () => {
    const assets = [];
    for (const symbol in this.state.assets) {
      if (this.state.assets.hasOwnProperty(symbol)) {
        const allocation = this.state.assets[symbol];
        assets.push({ symbol, allocation });
      }
    }
    return assets;
  };

  render() {
    console.log('render?');
    return (
      <div>
        <div className="asset-item-container">
          {this.getAssets().map(asset => (
            <AssetItem
              key={asset.symbol}
              {...asset}
              onChange={this.changeTarget}
            />
          ))}
          <AssetItemTotal total={this.state.total} />
        </div>
        <div className="asset-item-actions">
          <button className="button primary medium">Save</button>
          <button className="button secondary medium">Reset</button>
        </div>
      </div>
    );
  }
}

export default AssetPicker;
