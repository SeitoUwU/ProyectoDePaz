using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class Departamento
{
    public string DepId { get; set; } = null!;

    public string DepNombre { get; set; } = null!;

    public string DepEstado { get; set; } = null!;

    public virtual ICollection<Municipio> Municipios { get; set; } = new List<Municipio>();
}
