using System.ComponentModel.DataAnnotations;

namespace BZPAY_BE.DataAccess
{
    public class UpdatePasswordRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public DateTime Hour { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
