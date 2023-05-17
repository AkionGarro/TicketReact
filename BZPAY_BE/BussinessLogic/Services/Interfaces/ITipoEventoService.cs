using BZPAY_BE.Repositories;
using BZPAY_BE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BZPAY_BE.BussinessLogic.Services.Interfaces
{
    /// <summary>
    /// Service Interface for TipoEvento. 
    /// </summary>
    
    public interface ITipoEventoService
    {

        Task<IEnumerable<TipoEvento>> GetAllTipoEventosAsync();
    }
}
