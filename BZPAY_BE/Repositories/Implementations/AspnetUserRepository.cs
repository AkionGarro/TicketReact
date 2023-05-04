using BZPAY_BE.Repositories.Interfaces;
using BZPAY_BE.Models;
using BZPAY_UI.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace BZPAY_BE.Repositories.Implementations
{
    /// <summary>
    /// Repository for AspnetMembership
    /// </summary>
    public class AspnetUserRepository : GenericRepository<AspnetUser>, IAspnetUserRepository
    {
        /// <summary>
        /// Constructor of AspnetUserRepository
        /// </summary>
        /// <param name="membershipContext"></param>
        public AspnetUserRepository(MembershipContext membershipContext) : base(membershipContext)
        {
        }       

        public async Task<AspnetUser?> GetUserByUserNameAsync(string username)
        {
            AspnetUser user = await _context.AspnetUsers
                .Include(x => x.AspnetMembership)
                .SingleOrDefaultAsync(x => x.UserName == username);
            return user;
        }
    }
}