using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class TipodocumentoModel
{
    public string TipdocId { get; set; } = null!;

    public string TipdocTipo { get; set; } = null!;

    public virtual ICollection<DocumentoModel> Documentos { get; set; } = new List<DocumentoModel>();
}
