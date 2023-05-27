using project_web.Models.DbModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BZPAY_BE.DataAccess
{
    public class EnterPrice
    {
        [Display(Name = "Id Evento")]
        public int Id { get; set; }

        [Display(Name = "Asientos")]
        public List<InfoAsiento> Asientos { get; set; }

    }
    public class InfoAsiento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public int Cantidad { get; set; }
        public Decimal Precio { get; set; }

    }
}
