using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using BZPAY_BE.DataAccess;
using BZPAY_BE.Repositories.Interfaces;
using System;
using BZPAY_BE.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BZPAY_BE.BussinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace  BZPAY_BE.BussinessLogic.Services.Implementations
{
    /// <summary>
    /// Service for Entrada
    /// </summary>
    public class EntradaService : IEntradaService
    {
        private readonly IEntradaRepository _entradaRepository;

        /// <summary>
        /// Constructor of EntradaService
        /// </summary>
        /// <param name="entradaRepository"></param>
        public EntradaService(IEntradaRepository entradaRepository, IConfiguration config)
        {
            _entradaRepository = entradaRepository;   
        }

        public async Task<IEnumerable<Entrada>> GetAllEntradasAsync()
        {
            var lista = await _entradaRepository.GetAllEntradasAsync();
            return lista;
        }

        public async Task<Entrada> GetEntradaByIdAsync(int? id)
        {
             var lista = await _entradaRepository.GetEntradaByIdAsync(id);
             return lista;
        }

        public async Task<bool> CreateEntradasAsync([FromBody] EnterPrice price )
        {
            var idEvento = price.Id;
            //verificar primero si ya existen las entradas porque solo se pueden crear una vez
            var entradasEvento = await _entradaRepository.GetEntradaByIdEventoAsync(idEvento);
            if (entradasEvento == null)//si entradas no han sido creadas --> crearlas
            {
                //var descripciones = price.Descripcion;
                //var cantidades = form[2].Value.ToList();
                //var precios = form[3].Value.ToList();
                for (var i = 0; i < price.Asientos.Count(); i++)
                {
                    var entrada = new Entrada();
                    entrada.IdEvento = idEvento;
                    entrada.TipoAsiento = price.Asientos[i].Descripcion;
                    entrada.Disponibles = price.Asientos[i].Cantidad;
                    entrada.Precio = price.Asientos[i].Precio;
                    entrada.CreatedAt = DateTime.Now;
                    entrada.CreatedBy = "luz";
                    entrada.UpdatedAt = DateTime.Now;
                    entrada.UpdatedBy = "luz";
                    entrada.Active = true;
                    await _entradaRepository.AddAsync(entrada);
                }
                return true;
            }
            else
            {
                return false;
            }
            
        }

    }
}