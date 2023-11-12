using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class PermisoModel
{
    public string PermId { get; set; } = null!;

    public string PermPermiso { get; set; } = null!;

    public virtual ICollection<RolTienePermisoModel> RolTienePermisos { get; set; } = new List<RolTienePermisoModel>();
}
