import { useNavigate } from 'react-router-dom';
import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../css/Home.css';
import Cookies from 'universal-cookie';
import { useTranslation } from 'react-i18next';

function Home() {

    const utf8 = require('utf8');
    const cookies = new Cookies();
    const navigate = useNavigate();
    const { t, i18n } = useTranslation();

    function changeToEnglish() {
        i18n.changeLanguage("en");
    }

    function changeToSpanish() {
        i18n.changeLanguage("es");
    }

    const cerrarSesion = () => {
        cookies.remove('username', { path: '/' });
        navigate('/');
    }

    useEffect(() => {
        if (!cookies.get('username')) {
            navigate('/');
        }
    }, []);

    return (
        <div className="home">
            <div className="card text-center">
                    <div className="card-body">
                    <h5 className="card-title">{ t('started_session') }</h5>
                    <p className="card-text">
                        {t('welcome', { username: cookies.get('username') })}
                    </p>
                    <button className="btn btn-danger" onClick={() => cerrarSesion()}>{t('close_session')}</button>
                    <br />
                    <button className="btn btn-success" onClick={() => changeToSpanish()}>{t('spanish')}</button>
                    <br />
                    <button className="btn btn-success" onClick={() => changeToEnglish()}>{t('english')}</button>
                    </div>
                </div>
            </div>
           );
}                             

export default Home;