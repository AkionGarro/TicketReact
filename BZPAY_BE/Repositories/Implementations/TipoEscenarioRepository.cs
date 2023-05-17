using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using BZPAY_BE.Models;
using BZPAY_BE.Repositories.Interfaces;

namespace BZPAY_UI.Repositories.Implementations
{
    /// <summary>
    /// Repository for Tipoescenario
    /// </summary>
    public class TipoEscenarioRepository : GenericRepository<TipoEscenario>, ITipoEscenarioRepository
    {
        /// <summary>
        /// Constructor of TipoEscenarioRepository
        /// </summary>
        /// <param name="project_ticketContext"></param>
        public TipoEscenarioRepository(project_ticketContext _context) : base(_context)
        {   
        }

        public async Task<IEnumerable<TipoEscenario>> GetAllTipoEscenariosAsync()
        {
            return await _context.TipoEscenarios
                                 .Include(t => t.IdEscenarioNavigation)
                                 .Where(t => t.Active)
                                 .ToListAsync();
        }

        public async Task<TipoEscenario> GetTipoEscenarioByIdAsync(int id)
        {
            return await _context.TipoEscenarios
                                 .Where(t => t.Active)
                                 .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}