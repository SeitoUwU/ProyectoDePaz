using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class Documento
{
    public string DocId { get; set; } = null!;

    public string DocTitulo { get; set; } = null!;

    public string DocDescripcion { get; set; } = null!;

    public string? DocLink { get; set; }

    public byte[]? DocDocumento { get; set; }

    public string FkperId { get; set; } = null!;

    public string FkmunId { get; set; } = null!;

    public string FktipdocId { get; set; } = null!;

    public virtual ICollection<Doctieneetq> Doctieneetqs { get; set; } = new List<Doctieneetq>();

    public virtual Municipio Fkmun { get; set; } = null!;

    public virtual Persona Fkper { get; set; } = null!;

    public virtual Tipodocumento Fktipdoc { get; set; } = null!;

    public virtual ICollection<Publicacion> Publicacions { get; set; } = new List<Publicacion>();
}
