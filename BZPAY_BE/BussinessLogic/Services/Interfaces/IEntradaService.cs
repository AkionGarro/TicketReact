using Microsoft.AspNetCore.Http;
using BZPAY_BE.Repositories;
using BZPAY_BE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BZPAY_BE.BussinessLogic.Services.Interfaces
{
    /// <summary>
    /// Service Interface for Entrada. 
    /// </summary>
    
    public interface IEntradaService
    {
        Task<IEnumerable<Entrada>> GetAllEntradasAsync();

        Task<Entrada> GetEntradaByIdAsync(int? id);

        Task<Entrada> CreateEntradasAsync(IFormCollection form);

    }
}
