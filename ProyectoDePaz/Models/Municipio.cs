using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class Municipio
{
    public string MunId { get; set; } = null!;

    public string MunNombre { get; set; } = null!;

    public sbyte MunEstado { get; set; }

    public string FkdepId { get; set; } = null!;

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();

    public virtual Departamento Fkdep { get; set; } = null!;

    public virtual ICollection<Institucion> Institucions { get; set; } = new List<Institucion>();
}
