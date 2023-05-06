using System;
using System.Collections.Generic;

namespace BZPAY_BE.Models
{
    public partial class TipoEscenario
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = null!;
        public bool Active { get; set; }
        public int IdEscenario { get; set; }

        public virtual Escenario IdEscenarioNavigation { get; set; } = null!;
    }
}
