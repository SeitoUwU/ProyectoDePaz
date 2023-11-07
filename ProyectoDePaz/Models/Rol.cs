using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class Rol
{
    public string RolId { get; set; } = null!;

    public string RolRol { get; set; } = null!;

    public virtual ICollection<RolTienePermiso> RolTienePermisos { get; set; } = new List<RolTienePermiso>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
