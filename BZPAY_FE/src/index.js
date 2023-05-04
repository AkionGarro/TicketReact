import React from 'react';
import ReactDOM from 'react-dom';
import Routes from './routes/Routes';
import './config/i18next-config';
import "@fontsource/poppins";
import './css/index.css';

ReactDOM.render(
  <React.StrictMode>
        <Routes />
  </React.StrictMode>,
  document.getElementById('root')
);
