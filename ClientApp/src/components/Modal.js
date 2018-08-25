import React, { Component } from 'react';
import './Modal.css';

const MULTI_ACTION_MODAL = 'MULTI_ACTION_MODAL';
const ACTION_MODAL = 'ACTION_MODAL';

class MultiActionModal extends Component {
  render() {
    return (
      <Modal {...this.props} type={MULTI_ACTION_MODAL}>
        {this.props.children}
      </Modal>
    );
  }
}

MultiActionModal.defaultProps = {
  primaryActionText: 'Primary',
  secondaryActionText: 'Secondary'
};

class ActionModal extends Component {
  render() {
    return (
      <Modal {...this.props} type={ACTION_MODAL}>
        {this.props.children}
      </Modal>
    );
  }
}

ActionModal.defaultProps = {
  actionText: 'Action'
};

class Modal extends Component {
  constructor(props) {
    super(props);

    this.state = {
      open: true
    };
  }

  close = () => {
    this.setState({ open: false });
  };

  onExit = () => {
    this.close();
  };

  ignoreClick = event => {
    event.stopPropagation();
  };

  render() {
    var footer;
    if (this.props.type === MULTI_ACTION_MODAL) {
      footer = (
        <footer>
          <button
            onClick={this.props.onPrimaryAction}
            className="button primary medium"
          >
            {this.props.primaryActionText}
          </button>
          <button
            onClick={this.props.onSecondaryAction}
            className="button secondary medium"
          >
            {this.props.secondaryActionText}
          </button>
        </footer>
      );
    } else if (this.props.type === ACTION_MODAL) {
      footer = (
        <footer>
          <button
            onClick={this.props.onAction}
            className="button secondary medium"
          >
            {this.props.actionText}
          </button>
        </footer>
      );
    }
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
          {footer}
        </div>
      </div>
    );
  }
}

Modal.defaultProps = {
  title: 'My Modal Title'
};

export { Modal, ActionModal, MultiActionModal };
