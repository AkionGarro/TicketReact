using BZPAY_BE.Repositories.Interfaces;
using BZPAY_BE.Models;
using BZPAY_UI.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace BZPAY_BE.Repositories.Implementations
{
    /// <summary>
    /// Repository for AspnetMembership
    /// </summary>
    public class AspnetUserRepository : GenericRepository<Aspnetuser>, IAspnetUserRepository
    {
        /// <summary>
        /// Constructor of AspnetUserRepository
        /// </summary>
        /// <param name="membershipContext"></param>
        public AspnetUserRepository(project_ticketContext projecticketContext) : base(projecticketContext)
        {
        }       
        
        public async Task<Aspnetuser?> GetUserByUserNameAsync(string username)
        {
           Aspnetuser user = await _context.Aspnetusers
                .Include(x=> x.Aspnetuserlogins)
                .SingleOrDefaultAsync(x=> x.UserName == username);
            return user;
        }
    }
}