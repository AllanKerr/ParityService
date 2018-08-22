import React, { Component } from 'react';
import './SearchBar.css';

const DEFAULT_BUTTON_TEXT = 'Search';
const DEFAULT_PLACEHOLDER_TEXT = 'Search for a symbol';

const ENTER_KEY_CODE = 13;

class SearchBar extends Component {
  state = {
    searchText: ''
  };

  onKeyPress = event => {
    if (event.keyCode !== ENTER_KEY_CODE) {
      return;
    }
    if (this.state.searchText === '') {
      return;
    }
    this.props.onSearch(this.state.searchText);
    this.setState({ searchText: '' });
  };

  onSearch = event => {
    this.props.onSearch(this.state.searchText);
    this.setState({ searchText: '' });
  };

  onSearchTextChange = event => {
    const searchText = event.target.value;
    this.setState({ searchText });
  };

  render() {
    const buttonText =
      this.props.buttonText != null
        ? this.props.buttonText
        : DEFAULT_BUTTON_TEXT;
    const placeHolderText =
      this.props.placeHolderText != null
        ? this.props.placeHolderText
        : DEFAULT_PLACEHOLDER_TEXT;

    return (
      <div className="container search-bar">
        <input
          onChange={this.onSearchTextChange}
          onKeyUp={this.onKeyPress}
          placeholder={placeHolderText}
          className="search-input"
          type="text"
          value={this.state.searchText}
        />
        <button
          disabled={this.props.disabled || this.state.searchText === ''}
          onClick={this.onSearch}
          className="button primary medium search-button"
        >
          {buttonText}
        </button>
      </div>
    );
  }
}

export default SearchBar;
