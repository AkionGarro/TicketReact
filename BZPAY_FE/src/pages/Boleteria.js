import React, { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import Navigation from "./Navigation";
import Footer from "./Footer";
import Swal from "sweetalert2";
import { useNavigate } from "react-router-dom";
import "../css/Boleteria.css";

function Boleteria() {
  const [allTickets, setAllTickets] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [clicked, setClicked] = useState(false);
  const [eventsPerPage] = useState(5);
  const navigate = useNavigate();
  const getCarrito = async () => {
    const userName = document.getElementById("userName").value;
    if (userName == "") {
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Debe ingresar un ID de usuario",
      });
    } else {
      const url =
        "https://localhost:7052/api/Compra/GetAllCompras?username=" + userName;
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
        setAllTickets(data);
        console.log(data);
        if (data.length != 0) {
          setClicked(true);
        }
        if (!response.status == 200 || !response.status == 404) {
          const message = `Un error ha ocurrido: ${response.status}`;
          throw new Error(message);
        }
      } catch (error) {
        throw Error(error);
      }
    }
  };

  useEffect(() => {}, []);

  const indexOfLastEvent = currentPage * eventsPerPage;
  const indexOfFirstEvent = indexOfLastEvent - eventsPerPage;
  const currentEvents = allTickets.slice(indexOfFirstEvent, indexOfLastEvent);

  const navigateToPage = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  const pageNumbers = [];
  for (let i = 1; i <= Math.ceil(allTickets.length / eventsPerPage); i++) {
    pageNumbers.push(i);
  }

  return (
    <div>
      <Navigation />
      <div>
        <div className="headTable">
          <h2>Procesar compras</h2>
        </div>

        <div className="d-flex flex-column align-items-center justify-content-center">
          <label className="label-UserId">Digite el nombre del usuario</label>
          <input
            type="text"
            id="userName"
            name="userName"
            className="input-userId"
          />
          <button type="button" className="btn-cart" onClick={getCarrito}>
            Buscar
          </button>
        </div>
      </div>
      {clicked && (
        <div className="containerView">
          <table className="table table-dark table-striped table-hover">
            <thead>
              <tr>
                <th scope="col">Id Compra</th>
                <th scope="col">Cantidad</th>
                <th scope="col">Fecha Reserva</th>
                <th scope="col">Codigo Entrada</th>
                <th scope="col">Actions</th>
              </tr>
            </thead>
            <tbody>
              {currentEvents &&
                currentEvents.map((item) => (
                  <tr key={item.id}>
                    <th scope="row">{item.id}</th>
                    <td>{item.cantidad}</td>
                    <td>{item.fechaReserva}</td>
                    <td>{item.idEntrada}</td>
                    <td>
                      <div className="d-flex flex-column justify-content-center align-items-center ">
                        <a>
                          <svg
                            xmlns="http://www.w3.org/2000/svg"
                            class="icon icon-tabler icon-tabler-trash"
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
                            <path d="M4 7l16 0" />
                            <path d="M10 11l0 6" />
                            <path d="M14 11l0 6" />
                            <path d="M5 7l1 12a2 2 0 0 0 2 2h8a2 2 0 0 0 2 -2l1 -12" />
                            <path d="M9 7v-3a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v3" />
                          </svg>
                        </a>
                        <p className="text-center">Eliminar</p>
                      </div>
                    </td>
                  </tr>
                ))}
            </tbody>
          </table>

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
      )}

      <Footer />
    </div>
  );
}

export default Boleteria;
