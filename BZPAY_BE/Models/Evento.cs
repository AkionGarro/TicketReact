using System;
using System.Collections.Generic;

namespace BZPAY_BE.Models
{
    public partial class Evento
    {
        public Evento()
        {
            Entrada = new HashSet<Entrada>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = null!;
        public bool Active { get; set; }
        public int IdTipoEvento { get; set; }
        public int IdEscenario { get; set; }

        public virtual Escenario IdEscenarioNavigation { get; set; } = null!;
        public virtual TipoEvento IdTipoEventoNavigation { get; set; } = null!;
        public virtual ICollection<Entrada> Entrada { get; set; }
    }
}
