import React, { Component } from 'react';
import './HomeView.css';

const COLORS = [
  '#1f2d46',
  '#588aa4',
  '#1f2d46',
  '#e44822',
  '#fcb52b',
  '#f3adac',
  '#86bab8',
  '#588aa4',
  '#1f2d46'
];

class AssetItem extends Component {
  state = {
    barColor: COLORS[Math.floor(Math.random() * COLORS.length)]
  };

  changeAllocation = event => {
    var targetValue = event.target.value;
    if (targetValue > 100) {
      targetValue = 100;
    } else if (targetValue < 0 || targetValue === '') {
      targetValue = 0;
    } else {
      targetValue = Number(targetValue);
    }
    this.props.onChange(this.props.symbol, targetValue);
  };

  remove = event => {
    this.props.onRemove(this.props.symbol);
  };

  render() {
    const style = {
      width: `${this.props.allocation}%`,
      backgroundColor: this.state.barColor
    };

    return [
      <label
        key={`${this.props.symbol}-label`}
        className="label medium"
        htmlFor={`${this.props.symbol}-input`}
      >
        {this.props.symbol}
      </label>,
      <div key={`${this.props.symbol}-bar`} className="bar-container">
        <div style={style} className="bar" />
      </div>,
      <div
        key={`${this.props.symbol}-input`}
        className="container accessory-input"
      >
        <span className="accessory percent">
          <input
            value={this.props.allocation}
            id={`${this.props.symbol}-input`}
            placeholder={0}
            onChange={this.changeAllocation}
            min={0}
            max={100}
            type="number"
          />
        </span>
      </div>,
      <button
        key={`${this.props.symbol}-button`}
        className="button destructive medium"
        onClick={this.remove}
      >
        Remove
      </button>
    ];
  }
}

export default AssetItem;
