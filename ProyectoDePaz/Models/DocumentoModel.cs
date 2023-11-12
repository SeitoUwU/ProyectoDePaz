using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class DocumentoModel
{
    public string DocId { get; set; } = null!;

    public string DocTitulo { get; set; } = null!;

    public string DocDescripcion { get; set; } = null!;

    public string? DocLink { get; set; }

    public byte[]? DocDocumento { get; set; }

    public string FkperId { get; set; } = null!;

    public string FkmunId { get; set; } = null!;

    public string FktipdocId { get; set; } = null!;

    public virtual ICollection<DoctieneetqModel> Doctieneetqs { get; set; } = new List<DoctieneetqModel>();

    public virtual MunicipioModel Fkmun { get; set; } = null!;

    public virtual PersonaModel Fkper { get; set; } = null!;

    public virtual TipodocumentoModel Fktipdoc { get; set; } = null!;

    public virtual ICollection<PublicacionModel> Publicacions { get; set; } = new List<PublicacionModel>();
}
