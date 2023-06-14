import React, { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "../css/DetallesEventos.css";
import Navigation from "./Navigation";
import Footer from "./Footer";
import { useLocation } from "react-router-dom";
import { event } from "jquery";

function DetallesEventos() {
  const [eventSeats, setAllEventSeats] = useState([]);
  const [precio, setPrecio] = useState(0);
  const location = useLocation();
  const currentEvent = location.state?.data;
  var idEvent = currentEvent.id;
  var currentUser = localStorage.getItem("user");

  const handleChangePrecio = (e, itemId) => {
    var { value } = e.target;

    value = parseInt(value);

    var updatedEventSeats = eventSeats.map((item) => {
      if (item.id === itemId) {
        setPrecio(value);
        console.log(item);
        return {
          ...item,
          precio: value,
        };
      }
      return item;
    });
    setAllEventSeats(updatedEventSeats);
  };

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
      console.log(currentUser);

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

    var requestSeats = [];
    eventSeats.forEach((element) => {
      requestSeats.push({
        id: element.id,
        cantidad: element.precio,
      });
    });
    const tickets = {
      user: currentUser,
      asientos: requestSeats,
    };

    const settings = {
      method: "post",
      headers: myHeaders,
      body: JSON.stringify(tickets),
    };

    try {
      console.log(tickets);
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
              <th className="fixB">Precio</th>
            </tr>
          </thead>
          <tbody>
            {eventSeats &&
              eventSeats.map((item) => {
                return (
                  <tr key={item.id}>
                    <td>{item.id}</td>
                    <td>{item.descripcion}</td>
                    <td>{item.cantidad}</td>
                    <div>
                      <input
                        id="inputPrecio"
                        type="number"
                        min={0}
                        className="fixB"
                        onChange={(e) => handleChangePrecio(e, item.id)}
                      ></input>
                    </div>
                  </tr>
                );
              })}
          </tbody>
        </table>
        <div>
          <button className="btn-buy" onClick={createTickets}>
            Crear
          </button>
        </div>
      </div>

      <Footer />
    </div>
  );
}

export default DetallesEventos;
