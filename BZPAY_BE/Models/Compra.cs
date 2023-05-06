using System;
using System.Collections.Generic;

namespace BZPAY_BE.Models
{
    public partial class Compra
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = null!;
        public int Active { get; set; }
        public int IdEntrada { get; set; }
        public string UserId { get; set; } = null!;

        public virtual Entrada IdEntradaNavigation { get; set; } = null!;
    }
}
