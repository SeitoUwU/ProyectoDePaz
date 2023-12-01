using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class PublicacionModel
{
    public string PubliId { get; set; } = null!;

    public string PubliFechaPublicacion { get; set; } = null!;

    public sbyte PubliEstado { get; set; }

    public string FkdocId { get; set; } = null!;

    public virtual DocumentoModel Fkdoc { get; set; } = null!;
}
