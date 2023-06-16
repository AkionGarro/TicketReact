import { useNavigate } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "../css/Home.css";
import Cookies from "universal-cookie";
import { useTranslation } from "react-i18next";
function Navigation() {
  const cookies = new Cookies();
  const navigate = useNavigate();
  const { t, i18n } = useTranslation();
  const lang = document.getElementById("language");
  const userRol = localStorage.getItem("userRole");
  function changeToEnglish() {
    i18n.changeLanguage("en");
  }

  function changeToSpanish() {
    i18n.changeLanguage("es");
  }
  if (lang) {
    lang.addEventListener("click", () => {
      // if default value is changed
      lang.addEventListener("change", () => {
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
    });
  }

  const cerrarSesion = () => {
    cookies.remove("username", { path: "/" });
    navigate("/");
  };
  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-light">
      <div className="container-fluid">
        <a className="navbar-brand" href="/Home">
          TectTickets
        </a>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav">
            {userRol === "Admin" && (
              <li className="nav-item">
                <a className="nav-link" href="/Entradas">
                  Crear Entradas
                </a>
              </li>
            )}

            {userRol === "User" && (
              <li className="nav-item">
                <a className="nav-link" href="/ComprarEntradas">
                  Comprar Entradas
                </a>
              </li>
            )}

            {userRol === "Worker" && (
              <li className="nav-item">
                <a className="nav-link" href="/Boleteria">
                  Boleteria
                </a>
              </li>
            )}
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
              <option defaultValue value="0">
                Language
              </option>
              <option value="1">Spanish</option>
              <option value="2">English</option>
            </select>
          </div>
        </div>
      </div>
    </nav>
  );
}

export default Navigation;
