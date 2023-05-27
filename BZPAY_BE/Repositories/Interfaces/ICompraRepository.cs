using BZPAY_BE.Models;
using project_web.Models.DbModels;
using System.Linq.Expressions;

namespace BZPAY_BE.Repositories.Interfaces
{
    public interface ICompraRepository : IGenericRepository<Compra>
    {
        Task<IEnumerable<Compra>> GetAllComprasAsync();
    }
}
