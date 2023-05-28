using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BZPAY_BE.BussinessLogic.auth.ServiceInterface;
using System.Collections.Generic;
using System.Threading.Tasks;
using BZPAY_BE.DataAccess;
using BZPAY_BE.Models;
using System.Linq;

namespace BZPAY_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AspnetUserController : ControllerBase
    {
        private readonly IAspnetUserService _service;

        public AspnetUserController(IAspnetUserService service) => _service = service;

        /// <summary>
        /// StartSessionAsync
        /// </summary>
        /// <param>loginRequest</param>
        /// <returns>AspnetUserDo</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Aspnetuser),StatusCodes.Status200OK)]

        public async Task<ActionResult<Aspnetuser>> StartSessionAsync([FromBody] LoginRequest login)
        {
            Aspnetuser result  = await _service.StartSessionAsync(login);
            return (result is null) ? NotFound() : Ok(result);  
        }

        /// <summary>
        /// ForgotPasswordAsync
        /// </summary>
        /// <param>username</param>
        /// <returns>AspnetUserDo</returns>
 

    }
}