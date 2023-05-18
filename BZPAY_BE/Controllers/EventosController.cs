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
        public async Task<IActionResult> Index()
        {
            var listaEventos= await _eventoService.GetAllEventosAsync();
            return (listaEventos is null) ? NotFound() : Ok(listaEventos);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Evento), StatusCodes.Status200OK)]
        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var evento = await _eventoService.GetEventoByIdAsync(id);

            if (evento == null) return NotFound();

            return (evento is null) ? NotFound() : Ok(evento);
        }

        // GET: Eventos/Create
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Evento), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create()
        {
            var listaTipoEventos = await _tipoEventoService.GetAllTipoEventosAsync();
            var listaEscenarios = await _escenarioService.GetAllEscenariosAsync();
            //ViewData["IdEscenario"] = new SelectList(listaEscenarios, "Id", "Nombre");
            //ViewData["IdTipoEvento"] = new SelectList(listaTipoEventos, "Id", "Descripcion");
            List<Object> Lista = new();
            Lista.Add(listaEscenarios);
            Lista.Add(listaTipoEventos);
            return (Lista is null) ? NotFound() : Ok(Lista);
        }

        // POST: Eventos/Create
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Evento), StatusCodes.Status200OK)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Fecha,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdTipoEvento,IdEscenario")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                var result = await _eventoService.CreateEventoAsync(evento);
                return RedirectToAction(nameof(Index));
            }
            var listaTipoEventos = _tipoEventoService.GetAllTipoEventosAsync();
            var listaEscenarios = _escenarioService.GetAllEscenariosAsync();
            //ViewData["IdEscenario"] = new SelectList((System.Collections.IEnumerable)listaEscenarios, "Id", "Nombre");
            //ViewData["IdTipoEvento"] = new SelectList((System.Collections.IEnumerable)listaTipoEventos, "Id", "Descripcion");
            return (evento is null) ? NotFound() : Ok(evento);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Evento), StatusCodes.Status200OK)]
        // GET: Evento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var evento = await _eventoService.GetEventoByIdAsync(id);

            if (evento == null) return NotFound();

            var listaTipoEventos = _tipoEventoService.GetAllTipoEventosAsync();
            var listaEscenarios = _escenarioService.GetAllEscenariosAsync();
            //ViewData["IdEscenario"] = new SelectList((System.Collections.IEnumerable)listaEscenarios, "Id", "Nombre",evento.IdEscenario);
            //ViewData["IdTipoEvento"] = new SelectList((System.Collections.IEnumerable)listaTipoEventos, "Id", "Descripcion",evento.IdTipoEvento);
            return (evento is null) ? NotFound() : Ok(evento);
        }

        // POST: Eventos/Edit/5
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Evento), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Fecha,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdTipoEvento,IdEscenario")] Evento evento)
        {
            if (id != evento.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _eventoService.UpdateEventoAsync(evento);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            var listaTipoEventos = _tipoEventoService.GetAllTipoEventosAsync();
            var listaEscenarios = _escenarioService.GetAllEscenariosAsync();
            //ViewData["IdEscenario"] = new SelectList((System.Collections.IEnumerable)listaEscenarios, "Id", "Nombre", evento.IdEscenario);
            //ViewData["IdTipoEvento"] = new SelectList((System.Collections.IEnumerable)listaTipoEventos, "Id", "Descripcion", evento.IdTipoEvento);
            return (evento is null) ? NotFound() : Ok(evento);
        }

        // GET: Eventoes/Delete/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Evento), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var evento = await _eventoService.GetEventoByIdAsync(id);

            if (evento == null) return NotFound();


            return (evento is null) ? NotFound() : Ok(evento);
        }

        // POST: Eventoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Evento), StatusCodes.Status200OK)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _eventoService.GetEventoByIdAsync(id);
            evento.Active = false;
            try
            {
                await _eventoService.UpdateEventoAsync(evento);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(evento.Id))
                    return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: DetalleEventos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DetallesEvento), StatusCodes.Status200OK)]
        public async Task<IActionResult> DetalleEventos()
        {
            var listaEventos = await _eventoService.GetDetalleEventosAsync();
            return (listaEventos is null) ? NotFound() : Ok(listaEventos);
        }

        // GET: Eventos/Create/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Evento), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsientos(int? id)
        {
            if (id == null) { return NotFound(); }
            var eventoAsientos = await _eventoService.GetEventoAsientosAsync(id);
            if (eventoAsientos == null || eventoAsientos.Asientos == null) { return NotFound(); }

            return (eventoAsientos is null) ? NotFound() : Ok(eventoAsientos);
        }

        // POST: Eventos/Create
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Evento), StatusCodes.Status200OK)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsientos(IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entradaEvento = await _entradaService.CreateEntradasAsync(collection);
                    //if (entradaEvento == null)// las entradas fueron creadas
                    //    TempData["Success"] = "Las Entradas fueron creadas exitosamente...";
                    //else //desplegar error --> Las entradas ya han sido creadas no se pueden volver a crear
                    //    TempData["Error"] = "Error, Las entradas ya fueron creadas...";
                }
                return RedirectToAction(nameof(DetalleEventos));
            }
            catch
            {
                return BadRequest();
            }
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Evento), StatusCodes.Status200OK)]
        private bool EventoExists(int id)
        {
            return _eventoService.GetEventoByIdAsync(id) == null ? true : false;
        }

    }
}
