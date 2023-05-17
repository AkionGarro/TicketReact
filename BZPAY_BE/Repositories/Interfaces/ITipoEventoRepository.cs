using BZPAY_BE.DataAccess;
using BZPAY_BE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BZPAY_BE.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for TipoEvento
    /// </summary>
    public interface ITipoEventoRepository : IGenericRepository<TipoEvento>
    {
        Task<IEnumerable<TipoEvento>> GetAllTipoEventosAsync();

        Task<TipoEvento> GetTipoEventoByIdAsync(int id);
        
    }

    
}

