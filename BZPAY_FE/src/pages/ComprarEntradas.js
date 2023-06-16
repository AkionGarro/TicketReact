import React, { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import Navigation from "./Navigation";
import Footer from "./Footer";
import Swal from "sweetalert2";
import { useNavigate } from "react-router-dom";
import "../css/ComprarEntradas.css";

function ComprarEntradas() {
  const [allEvents, setAllEvents] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [eventsPerPage] = useState(5);
  const navigate = useNavigate();

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
  const sendEvent = (item) => {
    if (item != null) {
      navigate("/ComprarEntradasDetails", { state: { data: item } });
    } else {
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "No se ha podido enviar el evento",
      });
    }
  };

  useEffect(() => {
    getEvents();

    const sendEvent = (item) => {
      if (item != null) {
        Swal.fire({
          icon: "success",
          title: "Evento enviado",
          text: "El evento se ha enviado correctamente",
        });
        navigate("/ComprarEntradasDetails", { state: { data: item } });
      } else {
        Swal.fire({
          icon: "error",
          title: "Oops...",
          text: "No se ha podido enviar el evento",
        });
      }
    };
  }, []);

  const indexOfLastEvent = currentPage * eventsPerPage;
  const indexOfFirstEvent = indexOfLastEvent - eventsPerPage;
  const currentEvents = allEvents.slice(indexOfFirstEvent, indexOfLastEvent);

  const navigateToPage = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  const pageNumbers = [];
  for (let i = 1; i <= Math.ceil(allEvents.length / eventsPerPage); i++) {
    pageNumbers.push(i);
  }

  return (
    <div>
      <Navigation />
      <div className="containerEvent">
        <div className="headTable">
          <h2>Eventos disponibles</h2>
        </div>

        <div>
          <div>
            <button className="btn-cart">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                class="icon icon-tabler icon-tabler-shopping-cart"
                width="32"
                height="32"
                viewBox="0 0 24 24"
                stroke-width="1.5"
                stroke="#ffffff"
                fill="none"
                stroke-linecap="round"
                stroke-linejoin="round"
              >
                <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                <path d="M6 19m-2 0a2 2 0 1 0 4 0a2 2 0 1 0 -4 0" />
                <path d="M17 19m-2 0a2 2 0 1 0 4 0a2 2 0 1 0 -4 0" />
                <path d="M17 17h-11v-14h-2" />
                <path d="M6 5l14 1l-1 7h-13" />
              </svg>
              Carrito Compras
            </button>
          </div>

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
              {currentEvents &&
                currentEvents.map((item) => (
                  <tr key={item.id}>
                    <th scope="row">{item.id}</th>
                    <td>{item.descripcion}</td>
                    <td>{item.tipoEvento}</td>
                    <td>{item.fecha}</td>
                    <td>{item.tipoEscenario}</td>
                    <td>{item.escenario}</td>
                    <td>{item.localizacion}</td>
                    <td>
                      <div
                        className="d-flex flex-column justify-content-center align-items-center "
                        onClick={() => sendEvent(item)}
                      >
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
                        <p className="text-center">See details</p>
                      </div>
                    </td>
                  </tr>
                ))}
            </tbody>
          </table>
        </div>

        <ul className="pagination justify-content-center ">
          {pageNumbers.map((pageNumber) => (
            <li
              key={pageNumber}
              className={`page-item ${
                pageNumber === currentPage ? "active" : ""
              }`}
            >
              <button
                className="page-link"
                onClick={() => navigateToPage(pageNumber)}
              >
                {pageNumber}
              </button>
            </li>
          ))}
        </ul>
      </div>
      <Footer />
    </div>
  );
}

export default ComprarEntradas;
