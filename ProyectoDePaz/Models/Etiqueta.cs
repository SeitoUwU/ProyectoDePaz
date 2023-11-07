using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class Etiqueta
{
    public string EtqId { get; set; } = null!;

    public string EtqTipo { get; set; } = null!;

    public virtual ICollection<Doctieneetq> Doctieneetqs { get; set; } = new List<Doctieneetq>();
}
