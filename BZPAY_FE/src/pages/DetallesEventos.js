import React, { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "../css/DetallesEventos.css";
import Navigation from "./Navigation";
import Footer from "./Footer";
import { useLocation } from "react-router-dom";

function DetallesEventos() {
  const [eventSeats, setAllEventSeats] = useState([]);
  const location = useLocation();
  const currentEvent = location.state?.data;
  var idEvent = currentEvent.id;

  const getSeats = async () => {
    const url =
      "https://localhost:7052/api/Evento/GetEventoAsientos?id=" + idEvent;
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
      setAllEventSeats(data.asientos);
      if (!response.status == 200 || !response.status == 404) {
        const message = `Un error ha ocurrido: ${response.status}`;
        throw new Error(message);
      }
    } catch (error) {
      throw Error(error);
    }
  };

  useEffect(() => {
    getSeats();
  }, []);

  return (
    <div>
      <Navigation />
      <div>
        <h2>Detalles del evento</h2>
        <div className="">
          <div>
            <label>Descripción</label>
            <input value={currentEvent.descripcion} disabled></input>
          </div>
          <div>
            <label>Tipo Evento</label>
            <input value={currentEvent.tipoEvento} disabled></input>
          </div>
          <div>
            <label>Fecha</label>
            <input value={currentEvent.fecha} disabled></input>
          </div>
        </div>

        <div>
          <div>
            <label>Tipo escenario</label>
            <input value={currentEvent.tipoEscenario} disabled></input>
          </div>

          <div>
            <label>Escenario</label>
            <input value={currentEvent.escenario} disabled></input>
          </div>

          <div>
            <label>Localización</label>
            <input value={currentEvent.localizacion} disabled></input>
          </div>
        </div>
      </div>

      <Footer />
    </div>
  );
}

export default DetallesEventos;
