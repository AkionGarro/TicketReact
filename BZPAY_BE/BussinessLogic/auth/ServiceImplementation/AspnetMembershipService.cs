using AutoMapper;
using BZPAY_BE.BussinessLogic.auth.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BZPAY_BE.BussinessLogic.auth.ServiceImplementation
{
    /// <summary>
    /// Service for AspnetMembership
    /// </summary>
    public class AspnetMembershipService : IAspnetMembershipService
    {
        private readonly IAspnetMembershipRepository _aspnetMembershipRepository;
        private readonly IMapper _mapper;
        
        /// <summary>
        /// Constructor of AspnetMembershipService
        /// </summary>
        /// <param name="aspnetMembershipRepository"></param>
        /// <param name="mapper"></param>
        public AspnetMembershipService(IAspnetMembershipRepository aspnetMembershipRepository, IMapper mapper)
        {
            _aspnetMembershipRepository = aspnetMembershipRepository;
            _mapper = mapper;
        }
    }
}