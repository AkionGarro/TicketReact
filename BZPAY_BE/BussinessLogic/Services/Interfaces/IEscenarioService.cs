using BZPAY_BE.Repositories;
using BZPAY_BE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BZPAY_BE.BussinessLogic.Services.Interfaces
{
    /// <summary>
    /// Service Interface for Escenario. 
    /// </summary>
    
    public interface IEscenarioService
    {
        Task<IEnumerable<Escenario>> GetAllEscenariosAsync();

        Task<Escenario> GetEventoByIdAsync(int? id);

    }
}
