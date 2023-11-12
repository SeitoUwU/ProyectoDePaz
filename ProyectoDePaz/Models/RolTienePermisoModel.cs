using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class RolTienePermisoModel
{
    public string PfkpermId { get; set; } = null!;

    public string PfkrolId { get; set; } = null!;

    public DateOnly RoltienpermFechaAgregacion { get; set; }

    public virtual PermisoModel Pfkperm { get; set; } = null!;

    public virtual RolModel Pfkrol { get; set; } = null!;
}
