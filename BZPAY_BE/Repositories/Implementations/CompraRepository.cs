using BZPAY_BE.Models;
using Microsoft.EntityFrameworkCore;
using BZPAY_BE.Repositories.Interfaces;
using BZPAY_UI.Repositories.Implementations;
using System.Security.Cryptography.X509Certificates;

namespace BZPAY_BE.Repositories.Implementations
{
    public class CompraRepository : GenericRepository<Compra>, ICompraRepository
    {
        public CompraRepository(project_ticketContext context) : base(context)
        {
            
        }
         public async Task<IEnumerable<Compra>> GetAllComprasAsync(string? userId)
        {
            return await _context.Compras.Where(x => x.Active && x.UserId
            == userId).ToListAsync();
        }
    }
}
