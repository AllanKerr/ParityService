import React, { Component } from 'react';
import './Modal.css';

class Modal extends Component {
  constructor(props) {
    super(props);

    this.state = {
      open: true
    };
  }
  onExit = () => {
    this.setState({ open: false });
  };

  ignoreClick = event => {
    event.stopPropagation();
  };

  render() {
    return (
      <div onClick={this.onExit} open={this.state.open} className="overlay">
        <div onClick={this.ignoreClick} className="modal-container">
          <header>
            <h1>{this.props.title}</h1>
            <button onClick={this.onExit} className="button x">
              ✕
            </button>
          </header>
          <div className="modal-card">{this.props.children}</div>
        </div>
      </div>
    );
  }
}

Modal.defaultProps = {
  title: 'My Modal Title'
};

export default Modal;
