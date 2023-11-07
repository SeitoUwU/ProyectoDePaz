using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class Institucion
{
    public string InsId { get; set; } = null!;

    public string InsInstitucion { get; set; } = null!;

    public string FkmunId { get; set; } = null!;

    public virtual Municipio Fkmun { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
