import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "../pages/Home";
import Login from "../pages/auth/Login";
import Entradas from "../pages/Entradas";
import DetallesEventos from "../pages/DetallesEventos";
import Boleteria from "../pages/Boleteria";
import ComprarEntradas from "../pages/ComprarEntradas";
import ComprarEntradasDetails from "../pages/ComprarEntradasDetails";
function App() {
  return (
    <Router>
      <Routes>
        <Route exact path="/" element={<Login />} />
        <Route exact path="/Home" element={<Home />} />
        <Route exact path="/Entradas" element={<Entradas />} />
        <Route exact path="/DetallesEventos" element={<DetallesEventos />} />
        <Route exact path="/Boleteria" element={<Boleteria />} />
        <Route exact path="/ComprarEntradas" element={<ComprarEntradas />} />
        <Route exact path="/ComprarEntradasDetails" element={<ComprarEntradasDetails />} />
      </Routes>
    </Router>
  );
}

export default App;
