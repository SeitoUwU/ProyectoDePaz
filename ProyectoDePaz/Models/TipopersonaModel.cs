using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class TipopersonaModel
{
    public string TiperId { get; set; } = null!;

    public string TiperTipoPersona { get; set; } = null!;

    public virtual ICollection<PersonaModel> Personas { get; set; } = new List<PersonaModel>();
}
