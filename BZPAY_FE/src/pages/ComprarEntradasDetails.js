import React, { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "../css/DetallesEventos.css";
import Navigation from "./Navigation";
import Footer from "./Footer";
import { useLocation, useNavigate } from "react-router-dom";
import { event } from "jquery";
import Swal from "sweetalert2";

function ComprarEntradasDetails() {
  const [eventTickets, setAllEventTickets] = useState([]);
  const [cantidad, setCantidad] = useState(0);
  const location = useLocation();
  const currentEvent = location.state?.data;
  var idEvent = currentEvent.id;
  var currentUser = localStorage.getItem("user");
  const navigate = useNavigate();

  const handleChangeCantidad = (e, itemId) => {
    var { value } = e.target;

    value = parseInt(value);
    var updatedEventTickets = eventTickets.map((item) => {
      if (item.id === itemId) {
        setCantidad(value);
        return {
          ...item,
          cantidad: value,
        };
      }
      return item;
    });
    setAllEventTickets(updatedEventTickets);
  };

  const getInfo = async () => {
    const url =
      "https://localhost:7052/api/Evento/GetEventoEntradas?id=" + idEvent;
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
      console.log("-----------------------");
      console.log(data.entradas);
      console.log("-----------------------");
      setAllEventTickets(data.entradas);

      if (!response.status == 200 || !response.status == 404) {
        const message = `Un error ha ocurrido: ${response.status}`;
        throw new Error(message);
      }
    } catch (error) {
      throw Error(error);
    }
  };

  const buyTickets = async () => {
    const url = "https://localhost:7052/api/Compra/CreateCompra";
    const origin = "https://localhost:3000";

    const myHeaders = {
      "Content-Type": "application/json",
      "Access-Control-Allow-Origin": origin,
    };

    var requestSeats = [];
    eventTickets.forEach((element) => {
      requestSeats.push({
        id: element.id,
        cantidad: element.cantidad,
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
      if (data == false) {
        Swal.fire({
          icon: "error",
          title: "Oops...",
          text: "No se han podido comprar las entradas",
        });
      } else {
        Swal.fire({
          icon: "success",
          title: "¡Entradas compradas!",
          text: "Se han comprado las entradas correctamente",
        });
        navigate("/ComprarEntradas");
      }
      if (!response.status == 200 || !response.status == 404) {
        const message = `Un error ha ocurrido: ${response.status}`;
        throw new Error(message);
      }
    } catch (error) {
      throw Error(error);
    }
  };

  useEffect(() => {
    getInfo();
  }, []);

  return (
    <div>
      <Navigation />
      <div className="containerEvent">
        <h3 className="display-5">Comprar entradas</h3>
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
            {eventTickets &&
              eventTickets.map((item) => {
                return (
                  <tr key={item.id}>
                    <td>{item.id}</td>
                    <td>{item.tipoAsiento}</td>
                    <td>{item.disponibles}</td>
                    <td>{item.precio}</td>
                    <div>
                      <input
                        id="inputCantidad"
                        type="number"
                        min={0}
                        className="fixB"
                        onChange={(e) => handleChangeCantidad(e, item.id)}
                      ></input>
                    </div>
                  </tr>
                );
              })}
          </tbody>
        </table>
        <div>
          <button className="btn-buy" onClick={buyTickets}>
            Comprar
          </button>
        </div>
      </div>

      <Footer />
    </div>
  );
}

export default ComprarEntradasDetails;
