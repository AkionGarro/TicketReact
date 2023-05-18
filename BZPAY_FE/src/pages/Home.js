import { useNavigate } from "react-router-dom";
import React, { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "../css/Home.css";
import Cookies from "universal-cookie";
import { useTranslation } from "react-i18next";

function Home() {
  const utf8 = require("utf8");
  const cookies = new Cookies();
  const navigate = useNavigate();
  const { t, i18n } = useTranslation();
  const lang = document.getElementById("language");

  function changeToEnglish() {
    i18n.changeLanguage("en");
  }

  function changeToSpanish() {
    i18n.changeLanguage("es");
  }

  lang.addEventListener("change", function () {
    const selectedValue = lang.value;

    if (selectedValue === "1") {
      changeToSpanish();
    }
    if (selectedValue === "2") {
      changeToEnglish();
    } else {
      console.log("No se ha seleccionado idioma");
    }
  });

  const cerrarSesion = () => {
    cookies.remove("username", { path: "/" });
    navigate("/");
  };

  useEffect(() => {
    if (!cookies.get("username")) {
      navigate("/");
    }
  }, []);

  return (
    <div className="home">
      <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <div class="container-fluid">
          <a class="navbar-brand" href="#">
            TectTickets
          </a>
          <button
            class="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarNav"
            aria-controls="navbarNav"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span class="navbar-toggler-icon"></span>
          </button>
          <div class="collapse navbar-collapse" id="navbarNav">
            <ul class="navbar-nav">
              <li class="nav-item">
                <a class="nav-link" href="#">
                  Crear Entradas
                </a>
              </li>
              <li class="nav-item"></li>
            </ul>
          </div>
          <div className="d-flex gap-1">
            <div className="d-flex">
              <button className="btn-logout" onClick={() => cerrarSesion()}>
                {t("close_session")}
              </button>
            </div>
            <div className="d-flex">
              <select id="language" className="languages">
                <option selected value="0">
                  Language
                </option>
                <option value="1">Spanish</option>
                <option value="2">English</option>
              </select>
            </div>
          </div>
        </div>
      </nav>
      <div className="card text-center">
        <div className="card-body">
          <h5 className="card-title">{t("started_session")}</h5>
          <p className="card-text">
            {t("welcome", { username: cookies.get("username") })}
          </p>
        </div>
      </div>
    </div>
  );
}

export default Home;
