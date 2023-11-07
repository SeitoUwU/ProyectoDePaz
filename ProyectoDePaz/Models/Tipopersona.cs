using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class Tipopersona
{
    public string TiperId { get; set; } = null!;

    public string TiperTipoPersona { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
