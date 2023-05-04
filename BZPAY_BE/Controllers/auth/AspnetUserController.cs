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
        [ProducesResponseType(typeof(AspnetUserDo),StatusCodes.Status200OK)]
        public async Task<ActionResult<AspnetUserDo>> StartSessionAsync([FromBody] LoginRequest login)
        {
            AspnetUserDo result  = await _service.StartSessionAsync(login);
            return (result is null) ? NotFound() : Ok(result);  
        }

        /// <summary>
        /// ForgotPasswordAsync
        /// </summary>
        /// <param>username</param>
        /// <returns>AspnetUserDo</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AspnetUserDo),StatusCodes.Status200OK)]
        public async Task<ActionResult<AspnetUserDo>> ForgotPasswordAsync([FromBody] string username)
        {
            AspnetUserDo result = await _service.ForgotPasswordAsync(username);
            return (result is null) ? NotFound() : Ok(result);
        }

        /// <summary>
        /// UpdatePasswordAsync
        /// </summary>
        /// <param>UpdatePasswordRequest</param>
        /// <returns>AspnetUserDo</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AspnetUserDo), StatusCodes.Status200OK)]
        public async Task<ActionResult<AspnetUserDo>> UpdatePasswordAsync([FromBody] UpdatePasswordRequest data)
        {
            AspnetUserDo result = await _service.UpdatePasswordAsync(data);
            return (result is null) ? NotFound() : Ok(result);
        }

    }
}