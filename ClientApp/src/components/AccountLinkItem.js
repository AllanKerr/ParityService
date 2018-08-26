import React from 'react';
import './AccountLinkItem.css';

const AccountLinkItem = props => (
  <li className="account-link-item">
    <h3>{props.accountLink.creationTime}</h3>
    <label>
      Is Practice{' '}
      <input disabled type="checkbox" checked={props.accountLink.isPractice} />
    </label>
    <button className="button medium primary">Accounts</button>
    <button className="button medium destructive">Unlink</button>
  </li>
);

export default AccountLinkItem;
