import React, { Component } from 'react';
import './SearchBar.css';

const DEFAULT_BUTTON_TEXT = 'Search';
const DEFAULT_PLACEHOLDER_TEXT = 'Search for a symbol';

const ENTER_KEY_CODE = 13;

class SearchBar extends Component {
  searchText = '';

  state = {
    hasSearchText: false
  };

  onKeyPress = event => {
    if (event.keyCode !== ENTER_KEY_CODE) {
      return;
    }
    if (!this.state.hasSearchText) {
      return;
    }
    this.props.onSearch(this.searchText);
  };

  onSearch = event => {
    this.props.onSearch(this.searchText);
  };

  onSearchTextChange = event => {
    this.searchText = event.target.value;
    const hasSearchText = this.searchText.length !== 0;

    if (hasSearchText !== this.state.hasSearchText) {
      this.setState({ hasSearchText });
    }
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
        />
        <button
          disabled={!this.state.hasSearchText}
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
