using BZPAY_BE.Models;

namespace BZPAY_BE.DataAccess
{
    public class ForgotPasswordResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; } = null!;
        public DateTime Hour { get; set; }
    }
}
