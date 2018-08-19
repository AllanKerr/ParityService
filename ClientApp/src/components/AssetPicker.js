import React, { Component } from 'react';
import './HomeView.css';
import AssetItem from './AssetItem';
import AssetItemTotal from './AssetItemTotal';

class AssetPicker extends Component {
  constructor(props) {
    super(props);
    this.state = {
      assets: this.copyAssets(props.assets),
      isModified: false
    };
  }

  reset = event => {
    const nextAssets = this.copyAssets(this.props.assets);
    this.setState({
      assets: nextAssets,
      isModified: false
    });
  };

  changeAllocation = (symbol, nextAllocation) => {
    const nextState = this.state;
    nextState.assets[symbol] = nextAllocation;
    nextState.isModified = true;
    this.setState(nextState);
  };

  removeAsset = symbol => {
    const nextState = this.state;
    delete nextState.assets[symbol];
    nextState.isModified = true;
    this.setState(nextState);
  };

  copyAssets = assets => {
    const copy = {};
    for (const symbol in assets) {
      if (assets.hasOwnProperty(symbol)) {
        copy[symbol] = assets[symbol];
      }
    }
    return copy;
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

  getTotal = () => {
    let total = 0;
    for (const symbol in this.state.assets) {
      if (this.state.assets.hasOwnProperty(symbol)) {
        total += this.state.assets[symbol];
      }
    }
    return total;
  };

  render() {
    const total = this.getTotal();
    return (
      <div>
        <div className="asset-item-container">
          {this.getAssets().map(asset => (
            <AssetItem
              key={asset.symbol}
              {...asset}
              onChange={this.changeAllocation}
              onRemove={this.removeAsset}
            />
          ))}
          <AssetItemTotal total={this.getTotal()} />
        </div>
        <div className="asset-item-actions">
          <button
            disabled={!this.state.isModified || total !== 100}
            className="button primary medium"
          >
            Save
          </button>
          <button
            disabled={!this.state.isModified}
            onClick={this.reset}
            className="button secondary medium"
          >
            Reset
          </button>
        </div>
      </div>
    );
  }
}

export default AssetPicker;
