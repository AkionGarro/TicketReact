using BZPAY_BE.DataAccess;
using BZPAY_BE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BZPAY_BE.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for Evento
    /// </summary>
    public interface IAsientoRepository : IGenericRepository<Asiento>
    {
        Task<IEnumerable<Asiento>> GetAllAsientosAsync();

        Task<Asiento> GetAsientosByIdAsync(int id);
    }

    
}

