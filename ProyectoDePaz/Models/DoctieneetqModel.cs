using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class DoctieneetqModel
{
    public string FkdocId { get; set; } = null!;

    public string FketqId { get; set; } = null!;

    public sbyte DoctienetqEstado { get; set; }

    public virtual DocumentoModel Fkdoc { get; set; } = null!;

    public virtual EtiquetaModel Fketq { get; set; } = null!;
}
