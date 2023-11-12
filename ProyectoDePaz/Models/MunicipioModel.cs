using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class MunicipioModel
{
    public string MunId { get; set; } = null!;

    public string MunNombre { get; set; } = null!;

    public sbyte MunEstado { get; set; }

    public string FkdepId { get; set; } = null!;

    public virtual ICollection<DocumentoModel> Documentos { get; set; } = new List<DocumentoModel>();

    public virtual DepartamentoModel Fkdep { get; set; } = null!;

    public virtual ICollection<InstitucionModel> Institucions { get; set; } = new List<InstitucionModel>();
}
