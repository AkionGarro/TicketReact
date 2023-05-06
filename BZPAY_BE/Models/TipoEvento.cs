using System;
using System.Collections.Generic;

namespace BZPAY_BE.Models
{
    public partial class TipoEvento
    {
        public TipoEvento()
        {
            Eventos = new HashSet<Evento>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = null!;
        public bool Active { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; }
    }
}
