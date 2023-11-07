using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class Usuario
{
    public string UsuCorreo { get; set; } = null!;

    public string UsuContrasenia { get; set; } = null!;

    public string FkrolId { get; set; } = null!;

    public virtual Rol Fkrol { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
