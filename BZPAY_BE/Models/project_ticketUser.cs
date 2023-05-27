using Microsoft.AspNetCore.Identity;

namespace BZPAY_BE.Models
{
    public class project_ticketUser : IdentityUser

    {
        public virtual ICollection<Compra> Compras { get; } = new List<Compra>();
    }
}
