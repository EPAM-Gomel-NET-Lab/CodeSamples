import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import { BrowserRouter } from "react-router-dom"
import history from "./history";
import 'bootstrap/dist/css/bootstrap.css';

ReactDOM.render(
  <BrowserRouter history={history}>
    <App/>
  </BrowserRouter>,
  document.getElementById('root')
);
