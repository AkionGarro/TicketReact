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
                  src="https://images.pexels.com/photos/598686/pexels-photo-598686.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                  className="d-block w-100"
                  alt="img1"
                ></img>
              </div>
              <div className="carousel-item" data-bs-interval="3000">
                <img
                  src="https://images.pexels.com/photos/269948/pexels-photo-269948.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                  className="d-block w-100"
                  alt="img2"
                />
              </div>
              <div className="carousel-item" data-bs-interval="3000">
                <img
                  src="https://images.pexels.com/photos/45258/ballet-production-performance-don-quixote-45258.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                  className="d-block w-100"
                  alt="img3"
                />
              </div>
              <div className="carousel-item" data-bs-interval="3000">
                <img
                  src="https://images.pexels.com/photos/1884574/pexels-photo-1884574.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                  className="d-block w-100"
                  alt="img4"
                />
              </div>
              <div className="carousel-item" interval="3000">
                <img
                  src="https://images.pexels.com/photos/167491/pexels-photo-167491.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                  className="d-block w-100"
                  alt="img5"
                />
              </div>
              <div className="carousel-item" data-bs-interval="3000">
                <img
                  src="https://images.pexels.com/photos/1448385/pexels-photo-1448385.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
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
