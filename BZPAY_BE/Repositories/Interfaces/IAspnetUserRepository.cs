using BZPAY_BE.DataAccess;
using BZPAY_BE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BZPAY_BE.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for AspnetUser
    /// </summary>
    public interface IAspnetUserRepository : IGenericRepository<AspnetUser>
    {
        Task<AspnetUser?> GetUserByUserNameAsync(string username);
    }

}

