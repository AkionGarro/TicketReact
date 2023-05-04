import React, { useState, useEffect,useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import md5 from 'md5';
import 'bootstrap/dist/css/bootstrap.min.css';
import Cookies from 'universal-cookie';
import '../../css/auth/Login.css';
import { useTranslation } from 'react-i18next';
import ReCAPTCHA from "react-google-recaptcha";
import Spinner from "react-spinkit";

function ForgotPassword() {

    const captcha = useRef(null);
    const cookies = new Cookies();
    const navigate = useNavigate();
    const { t, i18n } = useTranslation();
    const [captchaValido, cambiarCaptchaValido] = useState(null);
    const [usuarioValido, cambiarUsuarioValido] = useState(null);
    const [userValido, cambiarUserValido] = useState(null);
    const [credencialesValido, cambiarCredenciales] = useState(null);
    const [disable, setDisable] = useState(false);
    const [isLoading, setIsLoading] = useState(null);

    const [form, setForm] = useState({
        username: ''
    });

    const onChange = (e) => {
        const { name, value } = e.target;
        setForm({
            ...form,
            [name]: value
        },[]);
    }

    const onChangeCaptcha = () => {

        if (captcha.current.getValue())
            cambiarCaptchaValido(true);
    }

    const onClickUser = () => {
        cambiarUserValido(true);
        cambiarCredenciales(true);
    }

    const handleClick = (event) => {
        event.preventDefault();
        var valid = true;
        
        (form.username === '')? cambiarUserValido(valid = false) : cambiarUserValido(valid = true);
        if (captcha.current.getValue()) {
            cambiarUsuarioValido(valid=true);
            cambiarCaptchaValido(valid=true);
        }
        else
        {
            cambiarUsuarioValido(valid=false);
            cambiarCaptchaValido(valid=false);
        }
        if (valid) {
            setDisable(true);
            verifyUser();
        }
    };

    const verifyUser = async (event) => {

        const url = 'https://localhost:7052/api/AspnetUser/ForgotPassword';
        const origin = 'https://localhost:3000';

        const myHeaders = {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': origin
        };

        const settings = {
            method: 'post',
            headers: myHeaders,
            body: JSON.stringify(form.username)
        };

        setIsLoading(true);

        try {
    
            const response = await fetch(url, settings);
            const data = await response.json();

            if (!response.status == 200 || !response.status == 404) {
                const message = `Un error ha ocurrido: ${response.status}`;
                throw new Error(message);
            }

            if (response.status === 200 || response.status === 404) {
                cambiarCredenciales(false);
                setDisable(false);
                setIsLoading(false);
            }
                
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
                    <form className="form-signin">
                        <span id="reauth-email" className="reauth-email"></span>
                        <p className="input_title">{ t('user') }</p>
                        <input
                            type="text"
                            name="username"
                            id="username"
                            className="login_box"
                            onChange = {onChange}
                            onClick = {onClickUser}
                            placeholder = {t('bzpayclient')}
                            required
                            autoFocus />
                        {userValido === false && < div className="user_error">{t('user_error')}</div>}
                        <div className="ReCAPTCHA">
                            <ReCAPTCHA
                                ref={captcha}
                                sitekey="6LdN-NoeAAAAAErGaW79_lgvfkZ4TumWk2swCykg"
                                onChange={onChangeCaptcha}
                                hl={i18n.language}
                            />
                        </div>
                        <br />
                        {captchaValido === false && < div className="error_captcha">{t('error_captcha')}</div>}
                        <button className="btn btn-lg btn-primary" type="submit" onClick={handleClick} disabled={disable}> {t('accept')} </button>
                        <br />
                        <div className='div-spinner'>
                            {isLoading === true && <Spinner className="ball-pulse-sync" color="#332D61" />}
                        </div>
                        {credencialesValido === false && < div className="check_email">{t('check_email')}</div>}
                    </form>
                </div>
            </div>
        </div>
        
        )
}

export default ForgotPassword;