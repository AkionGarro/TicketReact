using BZPAY_BE.Models;

namespace BZPAY_BE.DataAccess
{
    public class AspnetUserDo
    {
        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string LoweredUserName { get; set; } = null!;
        public string? MobileAlias { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime LastActivityDate { get; set; }
        public AspnetMembershipDo Membership { get; set; }
    }
}
