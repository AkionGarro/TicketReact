using AutoMapper;
using BZPAY_BE.BussinessLogic.auth.ServiceInterface;
using BZPAY_BE.DataAccess;
using BZPAY_BE.Repositories.Interfaces;
using BZPAY_BE.Helpers;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using BZPAY_BE.Models;
using Microsoft.AspNetCore.Identity;

namespace BZPAY_BE.BussinessLogic.auth.ServiceImplementation
{
    /// <summary>
    /// Service for Aspnet Users
    /// </summary>
    public class AspnetUserService : IAspnetUserService
    {
        private readonly IAspnetUserRepository _aspnetUserRepository;

        /// <summary>
        /// Constructor of AspnetUserService
        /// </summary>
        /// <param name="aspnetUserRepository"></param>
        /// <param name="mapper"></param>
        public AspnetUserService(IAspnetUserRepository aspnetUserRepository)
        {
            _aspnetUserRepository = aspnetUserRepository;

        }

        public async Task<Aspnetuser?> StartSessionAsync(LoginRequest login)
        {
            var user = await _aspnetUserRepository.GetUserByUserNameAsync(login.Username);
            if (user == null) return null;
         

            var passwordHasher = new PasswordHasher<Aspnetuser>();
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);
            if (result == PasswordVerificationResult.Success)
            {
                Console.WriteLine("Successfull");
                return user;
            }
            else
            {
                Console.WriteLine("Mamar verga");
                return null;
            }
            
      
        }

    }
}