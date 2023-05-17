using BZPAY_BE.DataAccess;
using BZPAY_BE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BZPAY_BE.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for Escenario
    /// </summary>
    public interface IEscenarioRepository : IGenericRepository<Escenario>
    {
        Task<IEnumerable<Escenario>> GetAllEscenariosAsync();

        Task<Escenario> GetEscenarioByIdAsync(int? id);

    }

    
}

