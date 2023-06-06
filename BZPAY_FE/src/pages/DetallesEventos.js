import React, { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "../css/Home.css";
import "../css/Entradas.css";
import Navigation from "./Navigation";
import Footer from "./Footer";

function DetallesEventos() {
  const [allEvents, setAllEvents] = useState([]);

  const getSeats = async () => {
    const url = "https://localhost:7052/api/Evento/GetDetalleEventos";
    const origin = "https://localhost:3000";

    const myHeaders = {
      "Content-Type": "application/json",
      "Access-Control-Allow-Origin": origin,
    };

    const settings = {
      method: "get",
      headers: myHeaders,
    };

    try {
      const response = await fetch(url, settings);
      const data = await response.json();
      setAllEvents(data);
      console.log(data);
      if (!response.status == 200 || !response.status == 404) {
        const message = `Un error ha ocurrido: ${response.status}`;
        throw new Error(message);
      }
    } catch (error) {
      throw Error(error);
    }
  };

  useEffect(() => {
    
  }, []);


  return (
    <div>
      <Navigation />

      <Footer />
    </div>
  );
}

export default DetallesEventos;
