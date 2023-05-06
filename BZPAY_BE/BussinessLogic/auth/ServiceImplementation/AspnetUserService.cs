using AutoMapper;
using BZPAY_BE.BussinessLogic.auth.ServiceInterface;
using BZPAY_BE.DataAccess;
using BZPAY_BE.Repositories.Interfaces;
using BZPAY_BE.Helpers;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BZPAY_BE.BussinessLogic.auth.ServiceImplementation
{
    /// <summary>
    /// Service for Aspnet Users
    /// </summary>
    public class AspnetUserService : IAspnetUserService
    {
        private readonly IAspnetUserRepository _aspnetUserRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IConfiguration _config;
        
        /// <summary>
        /// Constructor of AspnetUserService
        /// </summary>
        /// <param name="aspnetUserRepository"></param>
        /// <param name="mapper"></param>
        public AspnetUserService(IAspnetUserRepository aspnetUserRepository, IMapper mapper, IStringLocalizer<SharedResource> localizer,IConfiguration config)
        {
            _aspnetUserRepository = aspnetUserRepository;
            _mapper = mapper;
            _localizer = localizer;
            _config = config;   
        }

        public async Task<AspnetUserDo?> StartSessionAsync(LoginRequest login)
        {/*
            var user = await _aspnetUserRepository.GetUserByUserNameAsync(login.Username);
            if (user == null) return null;
            var encrypt = SecurityHelper.EncodePassword(login.Password, 1, user.AspnetMembership.PasswordSalt);
            if (encrypt != user.AspnetMembership.Password)
                return null;
            var userDo = _mapper.Map<AspnetUserDo>(user); 
            userDo.Membership = _mapper.Map<AspnetMembershipDo>(user.AspnetMembership);
            return userDo;*/
            return null;
        }

        public async Task<AspnetUserDo?> ForgotPasswordAsync(string username)
        {
            //set culture
            var culture = Thread.CurrentThread.CurrentCulture.Name;
            var lang = culture.Substring(0, 2);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            //Process
            var user = await _aspnetUserRepository.GetUserByUserNameAsync(username);
            if (user == null) return null;
            var userDo = _mapper.Map<AspnetUserDo>(user);
            //userDo.Membership = _mapper.Map<AspnetMembershipDo>(user.AspnetMembership);
            var response = new ForgotPasswordResponse { UserId = userDo.UserId.ToString(), UserName = userDo.UserName, Hour = DateTime.Now };
            var link = _config["Hosts:FrontEndURL"] + "/RecoverPassword?token=" + SecurityHelper.Encript(JsonSerializer.Serialize(response));
            var subject = _localizer["Subject"];
            var body = _localizer["Body1"] + "\r\n\n" + link + "\r\n\n" + _localizer["Body2"];
            MailHelper.RecoverPasswordSendMail(userDo.Membership.Email, subject, body);
            return userDo;
        }

        public async Task<AspnetUserDo?> UpdatePasswordAsync(UpdatePasswordRequest data)
        {   
            //set culture
            var culture = Thread.CurrentThread.CurrentCulture.Name;
            var lang = culture.Substring(0, 2);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            //Process
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$");
            var user = await _aspnetUserRepository.GetUserByUserNameAsync(data.Username);
            if (user == null) return null;
            var s = data.Password.Trim();
            var password = ((s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None)) ? SecurityHelper.Decript(data.Password):data.Password;
            Match match = regex.Match(password);
            if (!match.Success) throw new Exception(_localizer["InvalidPassword"]);
            var minutes = (Clock.Now - data.Hour).TotalMinutes;
            if (minutes > 30) throw new Exception(_localizer["ExpiredLink"]);
            //user.AspnetMembership.Password = SecurityHelper.EncodePassword(password, 1, user.AspnetMembership.PasswordSalt);
            //user.AspnetMembership.LastPasswordChangedDate = Clock.Now;   
            var result = await _aspnetUserRepository.UpdateAsync(user);
            var userDo = _mapper.Map<AspnetUserDo>(result);
            //userDo.Membership = _mapper.Map<AspnetMembershipDo>(result.AspnetMembership);
            return userDo;
        }
    }
}