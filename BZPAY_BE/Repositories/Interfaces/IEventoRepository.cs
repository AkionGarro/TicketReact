using BZPAY_BE.DataAccess;
using BZPAY_BE.Models;
using project_web.Models;
using project_web.Models.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BZPAY_BE.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for Evento
    /// </summary>
    public interface IEventoRepository : IGenericRepository<Evento>
    {
        Task<IEnumerable<Evento>> GetAllEventosAsync();
        Task<Evento> GetEventoByIdAsync(int? id);
        Task<IEnumerable<DetallesEvento>> GetDetalleEventosAsync();
        Task<EventoAsiento> GetEventoAsientosAsync(int? id);
        Task<EventoEntrada> GetEventoEntradasAsync(int? id);
    }

    
}

