import React from 'react';
import { withCookies } from 'react-cookie';

const XsrfProtection = WrappedComponent => {
  const WithXsrfProtection = props => {
    const { cookies, allCookies, ...filteredProps } = props;
    const token = props.cookies.get('XSRF-TOKEN');
    return <WrappedComponent xsrfToken={token} {...filteredProps} />;
  };

  return withCookies(WithXsrfProtection);
};

export default XsrfProtection;
