using BZPAY_BE.DataAccess;
using BZPAY_BE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BZPAY_BE.BussinessLogic.auth.ServiceInterface
{
    /// <summary>
    /// Service Interface for AspnetUser. 
    /// </summary>
    
    public interface IAspnetUserService
    {
        Task<AspnetUserDo?> StartSessionAsync(LoginRequest login);
        Task<AspnetUserDo?> ForgotPasswordAsync(string username);
        Task<AspnetUserDo?> UpdatePasswordAsync(UpdatePasswordRequest data);
    }
}
