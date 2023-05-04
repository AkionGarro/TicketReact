import React, { useState, useEffect,useRef } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import Cookies from 'universal-cookie';
import '../../css/auth/Login.css';
import { useTranslation } from 'react-i18next';
import Spinner from "react-spinkit";

function RecoverPassword() {

    const cookies = new Cookies();
    const navigate = useNavigate();
    const { t, i18n } = useTranslation();
    const [passwordNoValido, cambiarPasswordNoValido] = useState(null);
    const [passwordsNotMatch, cambiarPasswordsNotMatch] = useState(null);
    const [passwordChanged, cambiarPasswordChanged] = useState(null);
    const [isLoading, setIsLoading] = useState(null);
    const [expiredLink, setExpired] = useState(false);
    const [invalidToken, setInvalidToken] = useState(false);

    const [form, setForm] = useState({
        newpassword: '',
        confirmpassword: ''
    });

    const onChange = (e) => {
        const { name, value } = e.target;
        setForm({
            ...form,
            [name]: value
        },[]);
    }

    const onClickNewPassword = () => {
        cambiarPasswordNoValido(true);
        cambiarPasswordsNotMatch(true);
        cambiarPasswordChanged(true);
        setExpired(false);
        setInvalidToken(false);
    }

    const onClickConfirmPassword = () => {
        cambiarPasswordNoValido(true);
        cambiarPasswordsNotMatch(true);
        cambiarPasswordChanged(true);
        setExpired(false);
        setInvalidToken(false);
    }

    const handleClick = (event) => {
        event.preventDefault();
        var passMatch = true;
        var passFormat = true;
        var regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$/;

        //validar que newpassword y confirmpassword sean iguales
        (form.confirmpassword === form.newpassword) ? cambiarPasswordsNotMatch(passMatch = true) : cambiarPasswordsNotMatch(passMatch = false);

        //validar que formato del password sea valido
        form.confirmpassword.match(regex) ? cambiarPasswordNoValido(passFormat = true) : cambiarPasswordNoValido(passFormat = false);

        if (passMatch && passFormat) {
            changePassword();
        }
    };

    const query = new URLSearchParams(useLocation().search);
    const token = query.get('token');

    const changePassword = async (event) => {

        const url = 'https://localhost:7052/api/AspnetUser/UpdatePassword';
        const origin = 'https://localhost:3000';

        var userData = null;

        try
        {
            userData = JSON.parse(decodeURIComponent(atob(token)));
        } catch (e) {
            setInvalidToken(true);
            return;
        }

        const recoverPassword = {
            username: userData.UserName,
            hour: userData.Hour,
            password: btoa(form.newpassword)
        }

        const myHeaders = {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': origin
        };

        const settings = {
            method: 'put',
            headers: myHeaders,
            body: JSON.stringify(recoverPassword)
        };

        setIsLoading(true);
                  
        try {
              var startDate = new Date(Date.parse(recoverPassword.hour));
              var elapsedMinutes = Math.round(((new Date() - startDate) / 1000) / 60);
              if (elapsedMinutes > 30) {
                  setExpired(true);
              } else {
                       setExpired(false);
                       const response = await fetch(url, settings);
                       const data = await response.json();
                       if (!response.status == 200 || !response.status == 404) {
                            const message = `Un error ha ocurrido: ${response.status}`;
                            throw new Error(message);
                       }
                   
                       if (response.status === 200) {
                           cambiarPasswordChanged(false);
                       }

                       if (response.status === 404) {
                           cambiarPasswordsNotMatch(false);
                       }
              } //else
              setIsLoading(false);

        } catch (error) {
            throw Error(error);
        }
    }
    
    useEffect(() => {
        if (cookies.get('username')) {
            navigate('/Home');
        }
    }, []);

    return (
        <div className="m-0 vh-100 row justify-content-center align-items-center login">
            <div className="container">
                <div className="card card-container ">
                    <br />
                    <img src="./images/logo.png" alt="" />
                    <hr />
                    <p className='password-format'>{t('password_format')}</p>
                    <ul>
                        <li>{t('password_extension')}</li>
                        <li>{t('minimal_letters')}</li>
                        <li>{t('minimal_chars_digits')}</li>
                    </ul>
                    <hr />
                    <form className="form-signin">
                        <span id="reauth-email" className="reauth-email"></span>
                        <p className="input_title">{ t('newpassword') }</p>
                        <input
                            type="password"
                            name="newpassword"
                            id="newpassword"
                            className="login_box"
                            onChange = {onChange}
                            onClick = {onClickNewPassword}
                            placeholder= '********'
                            required
                            autoFocus />
                        <p className="input_title">{ t('confirmpassword') }</p>
                        <input
                            type="password"
                            name="confirmpassword"
                            id="confirmpassword"
                            className="login_box"
                            onChange={onChange}
                            onClick={onClickConfirmPassword}
                            placeholder="********"
                            required />
                        <hr />
                        <br />
                        <button className="btn btn-lg btn-primary" type="submit" onClick={handleClick}> {t('accept')} </button>
                        <br />
                        <div className='div-spinner'>
                            {isLoading === true && <Spinner className="ball-pulse-sync" color="#332D61" />}
                        </div>
                        {passwordsNotMatch === false && < div className="passwords_not_match">{t('passwords_not_match')}</div>}
                        {passwordNoValido === false && < div className="password_not_valid">{t('password_not_valid')}</div>}
                        {passwordChanged === false && < div className="password_changed">{t('password_changed')}</div>}
                        {expiredLink === true && < div className="expired_link">{t('expired_link')}</div>}
                        {invalidToken === true && < div className="invalid_token">{t('invalid_token')}</div>}
                    </form>
                </div>
            </div>
        </div>
        
        )
}

export default RecoverPassword;