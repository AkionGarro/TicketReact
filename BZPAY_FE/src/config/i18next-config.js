import i18n from "i18next";
import { initReactI18next } from 'react-i18next';
import LanguageDetector from 'i18next-browser-languagedetector';
import Backend from 'i18next-http-backend';

i18n      //.use(Backend)
    .use(LanguageDetector)
    .use(initReactI18next)
    .init({
        //lng:'en',
        fallbacklng: 'es',
        resources: {
            es: {
                translation: {
                    welcome: 'Hola. Bienvenido al NuevoUI {{ username }} usted ha sido exitosamente logueado y se encuentra en la página de home',
                    user: 'Usuario',
                    password:'Contraseña',
                    login: 'Iniciar Sesión',
                    actual_language: 'Lenguaje Actual',
                    close_session: 'Cerrar Sesión',
                    spanish: 'Español',
                    english: 'Inglés',
                    started_session: 'Sesión Iniciada',
                    user_error: 'Por favor digite un usuario válido',
                    error_password: 'Por favor digite un password válido',
                    error_captcha: 'Por favor seleccione el captcha',
                    invalid_credentials: 'Credenciales Inválidas',
                    forgot_password: 'Olvidó su contraseña?',
                    check_email: 'Por favor, revise su correo registrado',
                    accept: 'Aceptar',
                    bzpayclient: 'bzpaycliente',
                    newpassword: 'Nueva Contraseña',
                    confirmpassword: 'Confirmar Contraseña',
                    passwords_not_match: 'Las contraseñas digitadas NO coinciden',
                    password_not_valid: 'La contraseña no tiene el formato correcto',
                    password_changed: 'La contraseña ha sido modificada exitosamente',
                    expired_link: 'El enlace está expirado',
                    invalid_token: 'Token inválido',
                    password_format: 'Formato de la contraseña',
                    password_extension: 'Extensión de 8 a 16 caracteres',
                    minimal_letters: 'Al menos una mayúscula y una minúscula',
                    minimal_chars_digits: 'Al menos un dígito y un caracter especial'
                }
            },
            en: {
                translation: {
                    welcome: 'Hi.Welcome to the NewUI {{ username }} you have been successfully logged in and you are on the home page',
                    user: 'User',
                    password:'Password',
                    login: 'Login',
                    actual_language: 'Actual Language',
                    close_session: 'Close Session',
                    spanish: 'Spanish',
                    english: 'English',
                    started_session: 'Started Session',
                    user_error: 'Please enter a valid username',
                    error_password: 'Please enter a valid password',
                    error_captcha: 'Please check the captcha',
                    invalid_credentials: 'Invalid credentials',
                    forgot_password: 'Forgot Password?',
                    check_email: 'Please, check your registered email',
                    accept: 'Accept',
                    bzpayclient: 'bzpayclient',
                    newpassword: 'New Password',
                    confirmpassword: 'Confirm Password',
                    passwords_not_match: 'The passwords entered don´t match',
                    password_not_valid: 'The password doesn´t have the correct format',
                    password_changed: 'The password was successfully changed',
                    expired_link: 'The link has expired',
                    invalid_token: 'Invalid Token',
                    password_format: 'Password format',
                    password_extension: 'Extension 8 to 16 characters ',
                    minimal_letters: 'At least one uppercase and one lowercase',
                    minimal_chars_digits: 'At least one digit and one special character'
                }
            }

        }
    })

