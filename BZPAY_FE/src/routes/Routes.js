import React from "react";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from '../pages/Home';
import Login from '../pages/auth/Login';
import ForgotPassword from '../pages/auth/ForgotPassword';
import RecoverPassword from '../pages/auth/RecoverPassword';

function App() {
    return (
        <Router>
            <Routes>    
                <Route exact path="/" element={<Login />} />
                <Route exact path="/ForgotPassword" element={<ForgotPassword />} />
                <Route exact path="/RecoverPassword" element={<RecoverPassword />} />
                <Route exact path="/Home" element={<Home />} />
            </Routes>
        </Router>
    );
}

export default App;