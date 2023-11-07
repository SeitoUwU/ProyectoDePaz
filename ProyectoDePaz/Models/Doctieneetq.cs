using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class Doctieneetq
{
    public string FkdocId { get; set; } = null!;

    public string FketqId { get; set; } = null!;

    public sbyte DoctienetqEstado { get; set; }

    public virtual Documento Fkdoc { get; set; } = null!;

    public virtual Etiqueta Fketq { get; set; } = null!;
}
