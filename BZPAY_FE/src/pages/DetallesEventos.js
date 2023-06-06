import React, { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "../css/Home.css";
import "../css/Entradas.css";
import Navigation from "./Navigation";
import Footer from "./Footer";
import { useLocation } from "react-router-dom";

function DetallesEventos() {
  const [allEvents, setAllEvents] = useState([]);
  const location = useLocation();
  const itemData = location.state?.data;
  var id = itemData.id;
  const getSeats = async () => {
    const url = "https://localhost:7052/api/Evento/GetEventoAsientos";
    const origin = "https://localhost:3000";

    const myHeaders = {
      "Content-Type": "application/json",
      "Access-Control-Allow-Origin": origin,
    };

    const settings = {
      method: "post",
      headers: myHeaders,
      body: id,
    };

    try {
      const response = await fetch(url, settings);
      const data = await response.json();
      console.log(data);
      if (!response.status == 200 || !response.status == 404) {
        const message = `Un error ha ocurrido: ${response.status}`;
        throw new Error(message);
      }
    } catch (error) {
      throw Error(error);
    }
  };

  const showInfo = () => {
    console.log(itemData);
  };

  useEffect(() => {
    showInfo();
  }, []);

  return (
    <div>
      <Navigation />
      <div>
        <h1>Hello World</h1>
      </div>

      <Footer />
    </div>
  );
}

export default DetallesEventos;
