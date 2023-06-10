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
  var user = localStorage.getItem("user");

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
      console.log(user);

      if (!response.status == 200 || !response.status == 404) {
        const message = `Un error ha ocurrido: ${response.status}`;
        throw new Error(message);
      }
    } catch (error) {
      throw Error(error);
    }
  };

  const createTickets = async () => {
    const url = "https://localhost:7052/api/Evento/CreateEntradas";
    const origin = "https://localhost:3000";

    const myHeaders = {
      "Content-Type": "application/json",
      "Access-Control-Allow-Origin": origin,
    };

    const settings = {
      method: "get",
      headers: myHeaders,
    };

    const tickets ={
      idEvento: idEvent,
      idUsuario: user,
    }
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
      <div className="containerEvent">
        <h2>Detalles del evento</h2>
        <div className="infoEventContainer">
          <div className="infoValue">
            <label>Descripción</label>
            <input value={currentEvent.descripcion} disabled></input>
          </div>
          <div className="infoValue">
            <label>Tipo Evento</label>
            <input value={currentEvent.tipoEvento} disabled></input>
          </div>
          <div className="infoValue">
            <label>Fecha</label>
            <input value={currentEvent.fecha} disabled></input>
          </div>
        </div>

        <div className="infoEventContainer">
          <div className="infoValue">
            <label>Tipo escenario</label>
            <input value={currentEvent.tipoEscenario} disabled></input>
          </div>

          <div className="infoValue">
            <label>Escenario</label>
            <input value={currentEvent.escenario} disabled></input>
          </div>

          <div className="infoValue">
            <label>Localización</label>
            <input value={currentEvent.localizacion} disabled></input>
          </div>
        </div>
      </div>

      <div className="asientosContainer">
        <table className="table table-dark table-striped table-hover">
          <thead>
            <tr>
              <th>Id Asiento</th>
              <th>Tipo de Asiento</th>
              <th>Disponibles</th>
              <th>Precio</th>
              <th className="fixB">Cantidad</th>
            </tr>
          </thead>
          <tbody>
            {eventSeats &&
              eventSeats.map((item) => (
                <tr key={item.id}>
                  <td>{item.id}</td>
                  <td>{item.descripcion}</td>
                  <td>{item.cantidad}</td>
                  <td>{item.precio}</td>
                  <div>
                    <input
                      type="number"
                      min={0}
                      max={item.cantidad}
                      className="fixB"
                    ></input>
                  </div>
                </tr>
              ))}
          </tbody>
        </table>
        <div>
          <button className="btn-buy">Comprar</button>
        </div>
      </div>

      <Footer />
    </div>
  );
}

export default DetallesEventos;
