import { useNavigate } from "react-router-dom";
import React, { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "../css/Home.css";
import "../css/Entradas.css";
import Home from "./Home";
import Navigation from "./Navigation";

function Entradas() {
  const [allEvents, setAllEvents] = useState([]);

  const getEvents = async () => {
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
    getEvents();
  }, []);

  return (
    <div>
      <Navigation />
      <div>
        <div className="headTable">
          <h1>Eventos activos</h1>
        </div>

        <div className="eventsContainer">
          <table className="table table-dark table-striped table-hover">
            <thead>
              <tr>
                <th scope="col">Id Evento</th>
                <th scope="col">Descripción</th>
                <th scope="col">Tipo Evento</th>
                <th scope="col">Fecha</th>
                <th scope="col">Tipo escenario</th>
                <th scope="col">Escenario</th>
                <th scope="col">Localización</th>
                <th scope="col">Actions</th>
              </tr>
            </thead>
            <tbody>
              {allEvents &&
                allEvents.map((item) => (
                  <tr key={item.id}>
                    <th scope="row">{item.id}</th>
                    <td>{item.descripcion}</td>
                    <td>{item.tipoEvento}</td>
                    <td>{item.fecha}</td>
                    <td>{item.tipoEscenario}</td>
                    <td>{item.escenario}</td>
                    <td>{item.localizacion}</td>
                    <td>
                      <div className="d-flex flex-column justify-content-center align-items-center ">
                        <a>
                          <svg
                            xmlns="http://www.w3.org/2000/svg"
                            className="icon icon-tabler icon-tabler-eye"
                            width="32"
                            height="32"
                            viewBox="0 0 24 24"
                            strokeWidth="1.5"
                            stroke="#ffffff"
                            fill="none"
                            strokeLinecap="round"
                            strokeLinejoin="round"
                          >
                            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                            <path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" />
                            <path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" />
                          </svg>
                        </a>
                        <p>See details</p>
                      </div>
                    </td>
                  </tr>
                ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default Entradas;