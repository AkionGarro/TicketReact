using OrderService.Infrastructure;
using OrderService.Models;
using OrderService.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BZPAY_BE.Repositories.Implementations
{
    /// <summary>
    /// Repository for AspnetMembership
    /// </summary>
    public class AspnetMembershipRepository : GenericRepository<AspnetMembership>, IAspnetMembershiprRepository
    {
        /// <summary>
        /// Constructor of AspnetMembershipRepository
        /// </summary>
        /// <param name="membershipContext"></param>
        public AspnetMembershipRepository(AspnetMembershipContext orderContext) : base(membershipContext)
        {
        }
    }
}