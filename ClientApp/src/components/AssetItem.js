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
    allocation: 0,
    barColor: COLORS[Math.floor(Math.random() * COLORS.length)]
  };

  constructor(props) {
    super(props);
    this.state.allocation = props.allocation;
  }

  changeTarget = event => {
    var targetValue = event.target.value;
    if (targetValue > 100) {
      targetValue = 100;
    } else if (targetValue < 0 || targetValue === '') {
      targetValue = 0;
    } else {
      targetValue = Number(targetValue);
    }
    this.props.onChange(this.state.allocation, targetValue);
    this.setState({ allocation: targetValue });
  };

  render() {
    const style = {
      width: `${this.state.allocation}%`,
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
            value={this.state.value}
            id={`${this.props.symbol}-input`}
            placeholder={0}
            onChange={this.changeTarget}
            min={0}
            max={100}
            type="number"
          />
        </span>
      </div>,
      <button
        key={`${this.props.symbol}-button`}
        className="button destructive medium"
      >
        Remove
      </button>
    ];
  }
}

export default AssetItem;
