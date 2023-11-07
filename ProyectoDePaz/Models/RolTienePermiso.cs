using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class RolTienePermiso
{
    public string PfkpermId { get; set; } = null!;

    public string PfkrolId { get; set; } = null!;

    public DateOnly RoltienpermFechaAgregacion { get; set; }

    public virtual Permiso Pfkperm { get; set; } = null!;

    public virtual Rol Pfkrol { get; set; } = null!;
}
