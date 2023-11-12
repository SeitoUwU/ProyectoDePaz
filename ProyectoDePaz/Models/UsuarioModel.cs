using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class UsuarioModel
{
    public string UsuCorreo { get; set; } = null!;

    public string UsuContrasenia { get; set; } = null!;

    public string FkrolId { get; set; } = null!;

    public virtual RolModel Fkrol { get; set; } = null!;

    public virtual ICollection<PersonaModel> Personas { get; set; } = new List<PersonaModel>();
}
