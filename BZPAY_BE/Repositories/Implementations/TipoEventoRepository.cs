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
    /// Repository for TipoEvento
    /// </summary>
    public class TipoEventoRepository : GenericRepository<TipoEvento>, ITipoEventoRepository
    {
        /// <summary>
        /// Constructor of TipoEventoRepository
        /// </summary>
        /// <param name="project_ticketContext"></param>
        public TipoEventoRepository(project_ticketContext _context) : base(_context)
        {   
        }

        public async Task<IEnumerable<TipoEvento>> GetAllTipoEventosAsync()
        {
            return await _context.TipoEventos
                                 .Where(t => t.Active)
                                 .ToListAsync();
        }

        public async Task<TipoEvento> GetTipoEventoByIdAsync(int id)
        {
            return await _context.TipoEventos
                                 .Where(t => t.Active)
                                 .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}