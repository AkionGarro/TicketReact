using BZPAY_BE.DataAccess;
using BZPAY_BE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BZPAY_BE.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for AspnetUser
    /// </summary>
    public interface IAspnetUserRepository : IGenericRepository<Aspnetuser>
    {
        Task<Aspnetuser?> GetUserByUserNameAsync(string username);
        Task<String?> GetUserByUserIdAsync(string username);
    }

}

