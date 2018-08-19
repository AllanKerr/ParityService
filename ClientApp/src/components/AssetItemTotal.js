import React, { Component } from 'react';
import './HomeView.css';

class AssetItemTotal extends Component {
  render() {
    const totalText = `${this.props.total}%`;
    if (this.props.total === 100) {
      return (
        <p className="label primary large asset-item-total">{totalText}</p>
      );
    }
    return (
      <p className="label primary error large asset-item-total">{totalText}</p>
    );
  }
}

export default AssetItemTotal;
