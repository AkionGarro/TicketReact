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
    /// Repository for Escenario
    /// </summary>
    public class EscenarioRepository : GenericRepository<Escenario>, IEscenarioRepository
    {
        /// <summary>
        /// Constructor of EscenarioRepository
        /// </summary>
        /// <param name="project_ticketContext"></param>
        public EscenarioRepository(project_ticketContext _context) : base(_context)
        {   
        }

        public async Task<IEnumerable<Escenario>> GetAllEscenariosAsync()
        {
            return await _context.Escenarios.ToListAsync();
        }

        public async Task<Escenario> GetEscenarioByIdAsync(int? id)
        {
            return await _context.Escenarios
                                 .Where(t => t.Active)
                                 .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}