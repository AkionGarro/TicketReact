using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BZPAY_BE.BussinessLogic.Services.Interfaces;
using BZPAY_BE.Models;
using project_web.Models;
using BZPAY_BE.DataAccess;
using project_web.Models.DbModels;

namespace BZPAY_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        private readonly ITipoEventoService _tipoEventoService ;
        private readonly IEscenarioService _escenarioService;
        private readonly IEntradaService _entradaService;

        public EventoController(IEventoService eventoService, ITipoEventoService tipoEventoService, IEscenarioService escenarioService, IEntradaService entradaService)
        {    
            _eventoService = eventoService;
            _tipoEventoService = tipoEventoService;
            _escenarioService = escenarioService;
            _entradaService = entradaService;
        }
        // GET: Eventos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Evento), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEventosAsync()
        {
            var listaEventos= await _eventoService.GetAllEventosAsync();
            return (listaEventos is null) ? NotFound() : Ok(listaEventos);
        }

        // GET: DetalleEventos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DetallesEvento), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDetalleEventosAsync()
        {
            var listaEventos = await _eventoService.GetDetalleEventosAsync();
            return (listaEventos is null) ? NotFound() : Ok(listaEventos);
        }

        // GET: Eventos/Create/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EventoAsiento), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEventoAsientosAsync(int? id)
        {
            if (id == null) { return NotFound(); }
            var eventoAsientos = await _eventoService.GetEventoAsientosAsync(id);
            if (eventoAsientos == null || eventoAsientos.Asientos == null) { return NotFound(); }

            return (eventoAsientos is null) ? NotFound() : Ok(eventoAsientos);
        }

        // GET: Eventos/Create/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(EventoEntrada), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEventoEntradasAsync(int? id)
        {
            if (id == null) { return NotFound(); }
            var eventoEntrada = await _eventoService.GetEventoEntradasAsync(id);
            if (eventoEntrada == null || eventoEntrada.Entradas == null) { return NotFound(); }

            return (eventoEntrada is null) ? NotFound() : Ok(eventoEntrada);
        }

        // POST: Eventos/Create
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<Entrada>> CreateEntradasAsync([FromBody] EnterPrice price, string? user)
        {
           
                    var entradaEvento = await _entradaService.CreateEntradasAsync(price, user);
          

            return (entradaEvento is true) ? Ok(entradaEvento) : Ok(entradaEvento) ;

        }



        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(Evento), StatusCodes.Status200OK)]
        //private bool GetEventoByIdAsync(int id)
        //{
        //    return _eventoService.GetEventoByIdAsync(id) == null ? true : false;
        //}

    }
}
