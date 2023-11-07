using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class Persona
{
    public string PerId { get; set; } = null!;

    public string PerNombreUno { get; set; } = null!;

    public string? PerNombreDos { get; set; }

    public string PerApellidoUno { get; set; } = null!;

    public string? PerApellidoDos { get; set; }

    public long? PerTelefono { get; set; }

    public string? PerEdad { get; set; }

    public string FktiperId { get; set; } = null!;

    public string FkusuCorreo { get; set; } = null!;

    public string FkgenId { get; set; } = null!;

    public string FkinsId { get; set; } = null!;

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();

    public virtual Generopersona Fkgen { get; set; } = null!;

    public virtual Institucion Fkins { get; set; } = null!;

    public virtual Tipopersona Fktiper { get; set; } = null!;

    public virtual Usuario FkusuCorreoNavigation { get; set; } = null!;
}
