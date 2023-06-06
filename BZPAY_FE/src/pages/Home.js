import { useNavigate } from "react-router-dom";
import React, { useState, useEffect } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "../css/Home.css";
import Cookies from "universal-cookie";
import { useTranslation } from "react-i18next";
import Navigation from "./Navigation";
import Footer from "./Footer";
import ImagesSlider from "./Carrousel";
import Swal from "sweetalert2";
function Home() {
  const cookies = new Cookies();
  const navigate = useNavigate();
  const { t, i18n } = useTranslation();

  useEffect(() => {
    if (!cookies.get("username")) {
      navigate("/");
    }else{
      Swal.fire({
        position: "center",
        icon: "success",
        title: "Bienvenido a TecTickets",
        text: cookies.get("username"),
        showConfirmButton: true,
        timer: 10000,
      });
    }
  }, []);

  return (
    <div className="home">
      <Navigation />
      <ImagesSlider />
      <Footer />
    </div>
  );
}

export default Home;
