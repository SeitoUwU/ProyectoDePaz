using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class DepartamentoModel
{
    public string DepId { get; set; } = null!;

    public string DepNombre { get; set; } = null!;

    public string DepEstado { get; set; } = null!;

    public virtual ICollection<MunicipioModel> Municipios { get; set; } = new List<MunicipioModel>();
}
