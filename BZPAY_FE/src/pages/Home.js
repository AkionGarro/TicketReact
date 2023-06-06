import { useNavigate } from "react-router-dom";
import React, { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "../css/Home.css";
import Cookies from "universal-cookie";
import { useTranslation } from "react-i18next";
import Navigation from "./Navigation";
import Footer from "./Footer";
function Home() {
  const cookies = new Cookies();
  const navigate = useNavigate();
  const { t, i18n } = useTranslation();

  useEffect(() => {
    if (!cookies.get("username")) {
      navigate("/");
    }
  }, []);

  return (
    <div className="home">
      <Navigation />
      <div className="card text-center">
        <div className="card-body">
          <h5 className="card-title">{t("started_session")}</h5>
          <p className="card-text">
            {t("welcome", { username: cookies.get("username") })}
          </p>
        </div>
      </div>

      <div id="page-container">
        <div id="content-wrap">
          <div
            id="carouselExampleSlidesOnly"
            className="carousel slide carrousel-container"
            data-bs-ride="carousel"
          >
            <div className="carousel-inner">
              <div className="carousel-item active" data-bs-interval="3000">
                <img
                  src="./images/img1.jp"
                  className="d-block w-100"
                  alt="img1"
                />
              </div>
              <div className="carousel-item" data-bs-interval="3000">
                <img
                  src="./images/img2.jpg"
                  className="d-block w-100"
                  alt="img2"
                />
              </div>
              <div className="carousel-item" data-bs-interval="3000">
                <img
                  src="./images/img3.jpg"
                  className="d-block w-100"
                  alt="img3"
                />
              </div>
              <div className="carousel-item" data-bs-interval="3000">
                <img
                  src="./images/img4.jpg"
                  className="d-block w-100"
                  alt="img4"
                />
              </div>
              <div className="carousel-item" data-bs-interval="3000">
                <img
                  src="./images/img5.jpg"
                  className="d-block w-100"
                  alt="img5"
                />
              </div>
              <div className="carousel-item" data-bs-interval="3000">
                <img
                  src="./images/img6.jpg"
                  className="d-block w-100"
                  alt="img6"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
      <Footer />
    </div>
  );
}

export default Home;
