import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { routerReducer, routerMiddleware } from 'react-router-redux';
import * as Login from './Login';
import * as Logout from './Logout';
import * as Register from './Register';
import * as User from './User';
import * as Allocations from './Allocations';
import * as AllocationsPicker from './AllocationsPicker';
import * as AccountLinks from './AccountLinks';

export default function configureStore(history, initialState) {
  const reducers = {
    login: Login.reducer,
    logout: Logout.reducer,
    register: Register.reducer,
    account: User.reducer,
    allocations: Allocations.reducer,
    allocationsPicker: AllocationsPicker.reducer,
    accountLinks: AccountLinks.reducer
  };

  const middleware = [thunk, routerMiddleware(history), User.middleware];

  // In development, use the browser's Redux dev tools extension if installed
  const enhancers = [];
  const isDevelopment = process.env.NODE_ENV === 'development';
  if (
    isDevelopment &&
    typeof window !== 'undefined' &&
    window.devToolsExtension
  ) {
    enhancers.push(window.devToolsExtension());
  }

  const rootReducer = combineReducers({
    ...reducers,
    routing: routerReducer
  });

  return createStore(
    rootReducer,
    initialState,
    compose(
      applyMiddleware(...middleware),
      ...enhancers
    )
  );
}
