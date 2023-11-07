using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class Permiso
{
    public string PermId { get; set; } = null!;

    public string PermPermiso { get; set; } = null!;

    public virtual ICollection<RolTienePermiso> RolTienePermisos { get; set; } = new List<RolTienePermiso>();
}
