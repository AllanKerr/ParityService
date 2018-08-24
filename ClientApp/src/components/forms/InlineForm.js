import React, { Component } from 'react';
import './InlineForm.css';

class InlineForm extends Component {
  state = {
    isValid: false
  };

  constructor(props) {
    super(props);
    this.formRef = React.createRef();
  }

  textChanged = event => {
    const isValid =
      this.formRef.current && this.formRef.current.checkValidity();
    if (this.state.isValid !== isValid) {
      this.setState({ isValid });
    }
  };

  onSubmit = event => {
    event.preventDefault();
    this.props.onSubmit(event);
  };

  render() {
    return (
      <form
        ref={this.formRef}
        className="inline-form"
        type="submit"
        onSubmit={this.onSubmit}
      >
        <input
          disabled={this.props.disabled}
          required
          onChange={this.textChanged}
          name="Input"
          type="text"
          placeholder={this.props.placeholder}
        />
        {this.props.children}
        <button
          disabled={!this.state.isValid || this.props.disabled}
          type="submit"
          className="button primary medium"
        >
          {this.props.text}
        </button>
      </form>
    );
  }
}

InlineForm.defaultProps = {
  disabled: false,
  placeholder: '',
  text: 'Submit'
};

export default InlineForm;
