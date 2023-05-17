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
    public class EventoController : Controller
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
        public async Task<IActionResult> Index()
        {
            var listaEventos= await _eventoService.GetAllEventosAsync();
            return View(listaEventos);
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            
            var evento = await _eventoService.GetEventoByIdAsync(id);
            
            if (evento == null) return NotFound();
 
            return View(evento);
        }

        // GET: Eventos/Create
        public async Task<IActionResult> Create()
        {
            var listaTipoEventos = await _tipoEventoService.GetAllTipoEventosAsync();
            var listaEscenarios = await _escenarioService.GetAllEscenariosAsync();
            ViewData["IdEscenario"] = new SelectList(listaEscenarios, "Id", "Nombre");
            ViewData["IdTipoEvento"] = new SelectList(listaTipoEventos, "Id", "Descripcion");
            return View();
        }

        // POST: Eventos/Create
        [HttpPost]
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
            ViewData["IdEscenario"] = new SelectList((System.Collections.IEnumerable)listaEscenarios, "Id", "Nombre");
            ViewData["IdTipoEvento"] = new SelectList((System.Collections.IEnumerable)listaTipoEventos, "Id", "Descripcion");
            return View(evento);
        }

        // GET: Evento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var evento = await _eventoService.GetEventoByIdAsync(id);
            
            if (evento == null) return NotFound();

            var listaTipoEventos = _tipoEventoService.GetAllTipoEventosAsync();
            var listaEscenarios = _escenarioService.GetAllEscenariosAsync();
            ViewData["IdEscenario"] = new SelectList((System.Collections.IEnumerable)listaEscenarios, "Id", "Nombre",evento.IdEscenario);
            ViewData["IdTipoEvento"] = new SelectList((System.Collections.IEnumerable)listaTipoEventos, "Id", "Descripcion",evento.IdTipoEvento);
            return View(evento);
        }

        // POST: Eventos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            ViewData["IdEscenario"] = new SelectList((System.Collections.IEnumerable)listaEscenarios, "Id", "Nombre", evento.IdEscenario);
            ViewData["IdTipoEvento"] = new SelectList((System.Collections.IEnumerable)listaTipoEventos, "Id", "Descripcion", evento.IdTipoEvento);
            return View(evento);
        }

        // GET: Eventoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var evento = await _eventoService.GetEventoByIdAsync(id);
            
            if (evento == null) return NotFound();

            return View(evento);
        }

        // POST: Eventoes/Delete/5
        [HttpPost, ActionName("Delete")]
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
        public async Task<IActionResult> DetalleEventos()
        {
            var listaEventos = await _eventoService.GetDetalleEventosAsync();
            return View(listaEventos); 
        }

        // GET: Eventos/Create/5
        public async Task<IActionResult> CreateAsientos(int? id)
        {
            if (id == null) { return NotFound(); }
            var eventoAsientos = await _eventoService.GetEventoAsientosAsync(id);
            if (eventoAsientos == null || eventoAsientos.Asientos == null) { return NotFound(); }
            return View(eventoAsientos);
        }

        // POST: Eventos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsientos(IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entradaEvento = await _entradaService.CreateEntradasAsync(collection);
                    if (entradaEvento == null)// las entradas fueron creadas
                        TempData["Success"] = "Las Entradas fueron creadas exitosamente...";
                    else //desplegar error --> Las entradas ya han sido creadas no se pueden volver a crear
                        TempData["Error"] = "Error, Las entradas ya fueron creadas...";
                }
                return RedirectToAction(nameof(DetalleEventos));
            }
            catch
            {
                return View();
            }
        }




        private bool EventoExists(int id)
        {
            return _eventoService.GetEventoByIdAsync(id) == null ? true : false;
        }
        
    }
}
