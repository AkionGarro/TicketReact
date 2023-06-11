using BZPAY_BE.Models;
using Microsoft.EntityFrameworkCore;
using BZPAY_BE.Repositories.Interfaces;
using BZPAY_UI.Repositories.Implementations;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using project_web.Models.DbModels;

namespace BZPAY_BE.Repositories.Implementations
{
    public class CompraRepository : GenericRepository<Compra>, ICompraRepository
    {
        public CompraRepository(project_ticketContext context) : base(context)
        {
            
        }
         public async Task<IEnumerable<Compra>> GetAllComprasAsync(string? userId)
        {
            return await _context.Compras.Where(x => x.Active && x.UserId
            == userId).ToListAsync();
        }

        public async Task<Compra> GetCompraByIdAsync(int? id)
        {
            return await _context.Compras
                                .Where(t => t.Active)
                                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<byte[]> ImprimirConfirmedCompraPdfAsync(int? id)
        {
                var compra = await _context.Compras.FindAsync(id);
                if (compra != null)
                {



                    var pdf = (from C in _context.Compras
                               join ENT in _context.Entradas on C.IdEntrada equals ENT.Id
                               join EVNT in _context.Eventos on ENT.IdEvento equals EVNT.Id
                               join TEVNT in _context.TipoEventos on EVNT.IdTipoEvento equals TEVNT.Id
                               join ESC in _context.Escenarios on EVNT.IdEscenario equals ESC.Id
                               join USR in _context.Aspnetusers on C.UserId equals USR.Id
                               where C.Id == id

                               select new Pdf
                               {
                                   IdCompra = C.Id,
                                   Cantidad = C.Cantidad,
                                   fechaPago = C.FechaPago,
                                   UserId = USR.Id,
                                   Nombre = USR.UserName,
                                   Telefono = USR.PhoneNumber,
                                   IdEntrada = ENT.Id,
                                   PrecioEntrada = (int)ENT.Precio,
                                   IdEvento = EVNT.Id,
                                   DescripcionEvento = EVNT.Descripcion,
                                   tipoEvento = TEVNT.Id,
                                   DescripcionTipoEvento = TEVNT.Descripcion,
                                   idEscenario = ESC.Id,
                                   nombreEscenario = ESC.Nombre

                               }).ToList();



                    Document doc = new Document(PageSize.A4, 20f, 20f, 30f, 30f);
                    MemoryStream ms = new MemoryStream();
                    PdfWriter.GetInstance(doc, ms);
                    doc.Open();

                    //string imagePath = "wwwroot\\img\\logoTec.png";
                    //Image logo = Image.GetInstance(imagePath);
                    //logo.SetAbsolutePosition(500f, 745f);
                    //logo.ScaleAbsolute(100f, 100f);

                    //// Agregar la imagen al documento
                    //doc.Add(logo);
                    // Agregar contenido al documento

                    foreach (var item in pdf)
                    {
                        string html = "<h3>Información de la compra:</h3>"
                    + "<strong>Id de Compra:</strong> " + item.IdCompra + "<br/>"
                    + "<strong>Cantidad:</strong> " + item.Cantidad + "<br/>"
                    + "<strong>Fecha de Pago:</strong> " + item.fechaPago + "<br/><br/>"

                    + "<h3>Información del Usuario:</h3>"
                    + "<strong>Id de Usuario:</strong> " + item.UserId + "<br/>"
                    + "<strong>Nombre de Usuario:</strong> " + item.Nombre + "<br/>"
                    + "<strong>Teléfono de Usuario:</strong> " + item.Telefono + "<br/><br/>"

                    + "<h3>Información de la Entrada:</h3>"
                    + "<strong>Id de Entrada:</strong> " + item.IdEntrada + "<br/>"
                    + "<strong>Precio de Entrada:</strong> " + item.PrecioEntrada + "<br/><br/>"

                    + "<h3>Información del Evento:</h3>"
                    + "<strong>Id de Evento:</strong> " + item.IdEvento + "<br/>"
                    + "<strong>Descripción de Evento:</strong> " + item.DescripcionEvento + "<br/>"
                    + "<strong>Id de Tipo de Evento:</strong> " + item.tipoEvento + "<br/>"
                    + "<strong>Descripción de Tipo de Evento:</strong> " + item.DescripcionTipoEvento + "<br/><br/>"

                    + "<h3>Información del Escenario:</h3>"
                    + "<strong>Id de Escenario:</strong> " + item.idEscenario + "<br/>"
                    + "<strong>Nombre de Escenario:</strong> " + item.nombreEscenario + "<br/><br/>";

                        // Crea un nuevo elemento HTML

                        var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(html), null);

                        // Agrega cada elemento HTML al documento
                        foreach (var htmlElement in parsedHtmlElements)
                        {
                            doc.Add(htmlElement);
                        }

                    }

                    // Cerrar el documento
                    doc.Close();
                    compra.Active = false;
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                    // Guardar el archivo PDF en disco
                    byte[] pdfBytes = ms.ToArray();
                    return pdfBytes;

            }
            else { return null; }

            
        }
    }
}
