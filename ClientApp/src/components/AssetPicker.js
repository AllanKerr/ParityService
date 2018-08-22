import React, { Component } from 'react';
import './HomeView.css';
import AssetItem from './AssetItem';
import AssetItemTotal from './AssetItemTotal';
import SearchBar from './SearchBar';
import Loadable from './loading/Loadable';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../store/AllocationsPicker';
import FormError from './forms/FormError';

class AssetPicker extends Component {
  constructor(props) {
    super(props);
    this.state = {
      assets: this.copyAssets(props.assets),
      isModified: false
    };
  }

  onSearch = searchText => {
    const assets = this.state.assets;
    const symbol = searchText.toUpperCase();
    if (assets.hasOwnProperty(symbol)) {
      return;
    }
    if (Object.keys(assets).length === 0) {
      assets[symbol] = 100;
    } else {
      assets[symbol] = 0;
    }
    this.setState({
      assets: assets,
      isModified: true
    });
  };

  hasErrors(props) {
    return Object.keys(props.errors).length !== 0;
  }

  componentWillReceiveProps(nextProps) {
    const hasSaved =
      this.props.loading && !nextProps.loading && !this.hasErrors(nextProps);
    if (hasSaved) {
      this.setState({
        ...this.state,
        isModified: false
      });
    }
  }

  componentWillMount() {
    if (this.hasErrors(this.props)) {
      this.props.clearErrors();
    }
  }

  save = event => {
    const nextAssets = this.copyAssets(this.state.assets);
    this.props.onSave(nextAssets);
  };

  reset = event => {
    const nextAssets = this.copyAssets(this.props.assets);
    this.props.clearErrors();
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
        <SearchBar disabled={this.props.loading} onSearch={this.onSearch} />
        <div className="loadable-allocation-item-container">
          <Loadable loading={this.props.loading}>
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
          </Loadable>
        </div>
        <FormError errors={this.props.errors} />
        <div className="asset-item-actions">
          <button
            onClick={this.save}
            disabled={
              this.props.loading || !this.state.isModified || total !== 100
            }
            className="button primary medium"
          >
            Save
          </button>
          <button
            disabled={this.props.loading || !this.state.isModified}
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

export default connect(
  state => state.allocationsPicker,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(AssetPicker);
