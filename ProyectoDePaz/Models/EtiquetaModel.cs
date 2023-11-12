using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class EtiquetaModel
{
    public string EtqId { get; set; } = null!;

    public string EtqTipo { get; set; } = null!;

    public virtual ICollection<DoctieneetqModel> Doctieneetqs { get; set; } = new List<DoctieneetqModel>();
}
