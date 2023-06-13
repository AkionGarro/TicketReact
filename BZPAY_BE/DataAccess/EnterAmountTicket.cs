using project_web.Models.DbModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BZPAY_BE.DataAccess
{
    public class EnterAmountTicket
    {
        [Display(Name = "User")]
        public string? User { get; set; }
        [Display(Name = "Asientos")]
        public List<InfoEntrada> Asientos { get; set; }

    }
    public class InfoEntrada
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }

    }
}
