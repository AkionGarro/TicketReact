using BZPAY_BE.BussinessLogic.Services.Interfaces;
using BZPAY_BE.DataAccess;
using BZPAY_BE.Models;
using BZPAY_BE.Repositories.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BZPAY_BE.BussinessLogic.Services.Implementations
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _compraRepository;
        private readonly IEntradaRepository _entradaRepository;

        public CompraService(ICompraRepository compraRepository, IEntradaRepository entradaRepository) {
            _compraRepository = compraRepository;
            _entradaRepository = entradaRepository;
        }
        public async Task<bool> CreateCompraAsync([FromBody] EnterAmountTicket compra)
        {
            var estado = true;
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
                            CreatedBy = "luz",
                            UpdatedBy = "luz",
                            UpdatedAt = DateTime.Now,
                            CreatedAt = DateTime.Now,
                            Active = true,
                            UserId = "12df3ea5-72bb-4277-96f1-b53775869344"
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

        public async Task<Compra> GetCompraByIdAsync(int? id)
        {
            var lista = await _compraRepository.GetCompraByIdAsync(id);
            return lista;
        }


        public Task<byte[]> ImprimirConfirmedCompraPdfAsync(int? id)
        {
            var pdfBytes = _compraRepository.ImprimirConfirmedCompraPdfAsync(id);
            return pdfBytes;
        }
    }
}
