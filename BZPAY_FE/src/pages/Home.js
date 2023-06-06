import { useNavigate } from "react-router-dom";
import React, { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "../css/Home.css";
import Cookies from "universal-cookie";
import { useTranslation } from "react-i18next";
import Navigation from "./Navigation";
import Footer from "./Footer";
import ImagesSlider from "./Carrousel";

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

      <ImagesSlider />
      <Footer />
    </div>
  );
}

export default Home;
