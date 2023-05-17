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
    /// Repository for Entrada
    /// </summary>
    public class EntradaRepository : GenericRepository<Entrada>, IEntradaRepository
    {
        /// <summary>
        /// Constructor of EntradaRepository
        /// </summary>
        /// <param name="project_ticketContext"></param>
        public EntradaRepository(project_ticketContext _context) : base(_context)
        {   
        }

        public async Task<IEnumerable<Entrada>> GetAllEntradasAsync()
        {
            return await _context.Entradas
                                 .Where(x=>x.Active)
                                 .ToListAsync();
        }

        public async Task<Entrada> GetEntradaByIdAsync(int? id)
        {
            return await _context.Entradas
                                 .Where(t => t.Active)
                                 .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Entrada> GetEntradaByIdEventoAsync(int? id)
        {
            return await _context.Entradas
                                 .FirstOrDefaultAsync(m => m.IdEvento == id && m.Active);
        }
    }
}