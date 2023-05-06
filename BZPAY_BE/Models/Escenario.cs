using System;
using System.Collections.Generic;

namespace BZPAY_BE.Models
{
    public partial class Escenario
    {
        public Escenario()
        {
            Asientos = new HashSet<Asiento>();
            Eventos = new HashSet<Evento>();
            TipoEscenarios = new HashSet<TipoEscenario>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Localizacion { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = null!;
        public bool Active { get; set; }

        public virtual ICollection<Asiento> Asientos { get; set; }
        public virtual ICollection<Evento> Eventos { get; set; }
        public virtual ICollection<TipoEscenario> TipoEscenarios { get; set; }
    }
}
