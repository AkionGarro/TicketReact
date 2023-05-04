using System.ComponentModel.DataAnnotations;

namespace BZPAY_BE.DataAccess
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }    
    }
}
