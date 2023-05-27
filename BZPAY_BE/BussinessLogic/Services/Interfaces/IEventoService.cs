using BZPAY_BE.Repositories;
using BZPAY_BE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using project_web.Models;
using project_web.Models.DbModels;

namespace BZPAY_BE.BussinessLogic.Services.Interfaces
{
    /// <summary>
    /// Service Interface for Evento. 
    /// </summary>
    
    public interface IEventoService
    {
        Task<IEnumerable<Evento>> GetAllEventosAsync();

        Task<Evento> GetEventoByIdAsync(int? id);

        Task<Evento> CreateEventoAsync(Evento evento);

        Task<Evento> UpdateEventoAsync(Evento evento);

        Task<IEnumerable<DetallesEvento>> GetDetalleEventosAsync();

        Task<EventoAsiento> GetEventoAsientosAsync(int? id);
        Task<EventoEntrada> GetEventoEntradasAsync(int? id);

        //Task<Evento> GetDetallesEventosAsync();
        //Task<AspnetUserDo?> StartSessionAsync(LoginRequest login);
        //Task<AspnetUserDo?> ForgotPasswordAsync(string username);
        //Task<AspnetUserDo?> UpdatePasswordAsync(UpdatePasswordRequest data);
    }
}
