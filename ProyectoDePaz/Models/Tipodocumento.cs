using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class Tipodocumento
{
    public string TipdocId { get; set; } = null!;

    public string TipdocTipo { get; set; } = null!;

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
}
