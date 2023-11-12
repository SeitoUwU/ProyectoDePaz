using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class InstitucionModel
{
    public string InsId { get; set; } = null!;

    public string InsInstitucion { get; set; } = null!;

    public string FkmunId { get; set; } = null!;

    public virtual MunicipioModel Fkmun { get; set; } = null!;

    public virtual ICollection<PersonaModel> Personas { get; set; } = new List<PersonaModel>();
}
