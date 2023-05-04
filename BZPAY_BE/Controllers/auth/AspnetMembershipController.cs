using BZPAY_BE.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace BZPAY_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AspnetMembershipController : ControllerBase
    {
        private readonly ILogger<AspnetMembershipController> _logger;

        public AspnetMembershipController(ILogger<AspnetMembershipController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public LoginResponse IniciarSesion([FromBody] LoginRequest login)
        {
            var response = new LoginResponse();

            if (login.Username == "Lesmes" && login.Password == "daf30309483f82484d66608e7d8492c0")
            {
                response.Result = true;
                response.User = login;
            }
            else
            {
                response.Result = false;
                response.User = null;
            }

            return response;
        }

    }
}