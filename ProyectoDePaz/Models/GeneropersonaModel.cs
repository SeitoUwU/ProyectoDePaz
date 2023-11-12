using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class GeneropersonaModel
{
    public string GenId { get; set; } = null!;

    public string GenGeneroPersona { get; set; } = null!;

    public sbyte GenEstado { get; set; }

    public virtual ICollection<PersonaModel> Personas { get; set; } = new List<PersonaModel>();
}
