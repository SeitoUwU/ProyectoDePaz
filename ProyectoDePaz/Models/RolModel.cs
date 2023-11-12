using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class RolModel
{
    public string RolId { get; set; } = null!;

    public string RolRol { get; set; } = null!;

    public virtual ICollection<RolTienePermisoModel> RolTienePermisos { get; set; } = new List<RolTienePermisoModel>();

    public virtual ICollection<UsuarioModel> Usuarios { get; set; } = new List<UsuarioModel>();
}
