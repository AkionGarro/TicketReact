using Microsoft.AspNetCore.Mvc;
using BZPAY_BE.Models;
using project_web.Models;
using BZPAY_BE.DataAccess;
using project_web.Models.DbModels;
using BZPAY_BE.BussinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BZPAY_BE.BussinessLogic.Services.Implementations;
using BZPAY_BE.BussinessLogic.auth.ServiceInterface;

namespace BZPAY_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CompraController:ControllerBase
    {
        private readonly ICompraService _compraService;
        private readonly IEventoService _eventoService;
        private readonly ITipoEventoService _tipoEventoService;
        private readonly IEscenarioService _escenarioService;
        private readonly IEntradaService _entradaService;
        private readonly IAspnetUserService _aspnetUserService;

        public CompraController(ICompraService compraService, IEventoService eventoService, ITipoEventoService tipoEventoService, IEscenarioService escenarioService, IEntradaService entradaService,
            IAspnetUserService aspnetUserService)
        {
            _aspnetUserService = aspnetUserService;
            _compraService = compraService;
            _eventoService = eventoService;
            _tipoEventoService = tipoEventoService;
            _escenarioService = escenarioService;
            _entradaService = entradaService;
        }
        // GET: Eventos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Compra), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllComprasAsync(string? userId)
        {
            var listaCompra = await _compraService.GetAllComprasAsync(userId);
            return (listaCompra is null) ? NotFound() : Ok(listaCompra);
        }


        // GET: Compra
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Compra), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompraByIdAsync(int? id)
        {
            var listaCompra = await _compraService.GetCompraByIdAsync(id);
            return (listaCompra is null) ? NotFound() : Ok(listaCompra);
        }
        //Carito de compras
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CarritoCompras), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCarritoComprasAsync(string? username)
        {

            var userId = await _aspnetUserService.GetUserByUserIdAsync(username);
            var listaCompra = await _compraService.GetCarritoComprasAsync(userId);
            return (listaCompra is null) ? NotFound() : Ok(listaCompra);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<Compra>> CreateCompraAsync([FromBody] EnterAmountTicket compra)
        {

            var comprasEntrada = await _compraService.CreateCompraAsync(compra);


            return (comprasEntrada is true) ? Ok(comprasEntrada) : Ok(comprasEntrada);

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        public async Task<ActionResult<Compra>> ImprimirConfirmedCompraPdfAsync(int? id)
        {

            var pdfBytes = await _compraService.ImprimirConfirmedCompraPdfAsync(id);
            return File(pdfBytes, "application/pdf", "archivo.pdf");

        }
    }
   
}
