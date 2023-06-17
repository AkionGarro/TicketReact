using BZPAY_BE.Models;
using Microsoft.AspNetCore.Mvc;
using project_web.Models;
using project_web.Models.DbModels;
using System.Linq.Expressions;

namespace BZPAY_BE.Repositories.Interfaces
{
    public interface ICompraRepository : IGenericRepository<Compra>
    {
        Task<IEnumerable<Compra>> GetAllComprasAsync(string? userId);
        Task<IEnumerable<CarritoCompras>> GetCarritoComprasAsync(string? userId);
        Task<Compra> GetCompraByIdAsync(int? id);
        Task<bool> DeleteCompraByIdAsync(int? id);
        Task<String> ImprimirConfirmedCompraPdfAsync(int? id);
    }
}
