using BZPAY_BE.BussinessLogic.Services.Interfaces;
using BZPAY_BE.DataAccess;
using BZPAY_BE.Models;
using BZPAY_BE.Repositories.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_web.Models.DbModels;
using System.Security.Claims;

namespace BZPAY_BE.BussinessLogic.Services.Implementations
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _compraRepository;
        private readonly IEntradaRepository _entradaRepository;
        private readonly IAspnetUserRepository _aspnetUserRepository; 

        public CompraService(ICompraRepository compraRepository, IEntradaRepository entradaRepository, IAspnetUserRepository aspnetUserRepository)
        {
            _compraRepository = compraRepository;
            _entradaRepository = entradaRepository;
            _aspnetUserRepository = aspnetUserRepository;
        }
        public async Task<bool> CreateCompraAsync([FromBody] EnterAmountTicket compra)
        {
            var estado = true;
            var userId = await _aspnetUserRepository.GetUserByUserIdAsync(compra.User);
            bool todosMayoresACero = compra.Asientos.All(objeto => objeto.Cantidad > 0);
            if (todosMayoresACero)
            {
                for (var i = 0; i < compra.Asientos.Count(); i++)
                {
                    var entrada = await _entradaRepository.GetEntradaByIdAsync(compra.Asientos[i].Id);
                    entrada.Disponibles = entrada.Disponibles - compra.Asientos[i].Cantidad;
                    if (compra.Asientos[i].Cantidad > 0)
                    {
                        var comprasEntrada = new Compra
                        {
                            IdEntrada = compra.Asientos[i].Id,
                            Cantidad = compra.Asientos[i].Cantidad,
                            FechaReserva = DateTime.Now,
                            FechaPago = DateTime.Now,
                            CreatedBy = compra.User,
                            UpdatedBy = compra.User,
                            UpdatedAt = DateTime.Now,
                            CreatedAt = DateTime.Now,
                            Active = true,
                            UserId = userId
                        };

                        await _compraRepository.AddAsync(comprasEntrada);
                    }



                }
            }
            else
            {
                estado = false;
            }
            
            return estado;
          
        }

        public async Task<IEnumerable<Compra>> GetAllComprasAsync(string? userId)
        {
            var listaCompra = await _compraRepository.GetAllComprasAsync(userId);
            return listaCompra;
        }

        public async Task<IEnumerable<CarritoCompras>> GetCarritoComprasAsync(string? userId)
        {
            var listaCompra = await _compraRepository.GetCarritoComprasAsync(userId);
            return listaCompra;
        }

        public async Task<Compra> GetCompraByIdAsync(int? id)
        {
            var lista = await _compraRepository.GetCompraByIdAsync(id);
            return lista;
        }


        public Task<String> ImprimirConfirmedCompraPdfAsync(int? id)
        {
            var pdfBytes = _compraRepository.ImprimirConfirmedCompraPdfAsync(id);
            return pdfBytes;
        }
    }
}
