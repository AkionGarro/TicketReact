import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "../pages/Home";
import Login from "../pages/auth/Login";
import Entradas from "../pages/Entradas";
import DetallesEventos from "../pages/DetallesEventos";

function App() {
  return (
    <Router>
      <Routes>
        <Route exact path="/" element={<Login />} />
        <Route exact path="/Home" element={<Home />} />
        <Route exact path="/Entradas" element={<Entradas />} />
        <Route exact path="/DetallesEventos" element={<DetallesEventos />} />
      </Routes>
    </Router>
  );
}

export default App;
