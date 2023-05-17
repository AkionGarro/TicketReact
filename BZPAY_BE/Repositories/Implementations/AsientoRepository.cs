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
    /// Repository for Asiento
    /// </summary>
    public class AsientoRepository : GenericRepository<Asiento>, IAsientoRepository
    {
        /// <summary>
        /// Constructor of AsientoRepository
        /// </summary>
        /// <param name="project_ticketContext"></param>
        public AsientoRepository(project_ticketContext _context) : base(_context)
        {   
        }

        public async Task<IEnumerable<Asiento>> GetAllAsientosAsync()
        {
            return await _context.Asientos
                                 .Include(t => t.IdEscenarioNavigation)
                                 .Where(t => t.Active)
                                 .ToListAsync();
        }

        public async Task<Asiento> GetAsientosByIdAsync(int id)
        {
            return await _context.Asientos
                                 .Where(t => t.Active)
                                 .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}