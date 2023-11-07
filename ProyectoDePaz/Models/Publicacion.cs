using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class Publicacion
{
    public string PubliId { get; set; } = null!;

    public DateOnly PubliFechaPublicacion { get; set; }

    public sbyte PubliEstado { get; set; }

    public string FkdocId { get; set; } = null!;

    public virtual Documento Fkdoc { get; set; } = null!;
}
