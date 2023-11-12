using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class PublicacionModel
{
    public string PubliId { get; set; } = null!;

    public DateOnly PubliFechaPublicacion { get; set; }

    public sbyte PubliEstado { get; set; }

    public string FkdocId { get; set; } = null!;

    public virtual DocumentoModel Fkdoc { get; set; } = null!;
}
