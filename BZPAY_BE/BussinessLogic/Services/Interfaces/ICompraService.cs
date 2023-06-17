using BZPAY_BE.DataAccess;
using BZPAY_BE.Models;
using Microsoft.AspNetCore.Mvc;
using project_web.Models.DbModels;

namespace BZPAY_BE.BussinessLogic.Services.Interfaces
{
    public interface ICompraService
    {
        Task<IEnumerable<Compra>> GetAllComprasAsync(string? userId);
        Task<bool> CreateCompraAsync([FromBody] EnterAmountTicket compra);
        Task<String> ImprimirConfirmedCompraPdfAsync(int? id);
        Task<Compra> GetCompraByIdAsync(int? id);
        Task<IEnumerable<CarritoCompras>> GetCarritoComprasAsync(string? userId);

    }
}
