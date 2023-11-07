using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class Generopersona
{
    public string GenId { get; set; } = null!;

    public string GenGeneroPersona { get; set; } = null!;

    public sbyte GenEstado { get; set; }

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
