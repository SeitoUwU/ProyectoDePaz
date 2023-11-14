using System;
using System.Collections.Generic;

namespace ProyectoDePaz.Models;

public partial class PersonaModel
{
    public string PerId { get; set; } = null!;

    public string PerNombreUno { get; set; } = null!;

    public string? PerNombreDos { get; set; }

    public string PerApellidoUno { get; set; } = null!;

    public string? PerApellidoDos { get; set; }

    public long? PerTelefono { get; set; }

    public string? PerEdad { get; set; }

    public string FkusuCorreo { get; set; } = null!;

    public string FkgenId { get; set; } = null!;

    public string FkinsId { get; set; } = null!;

    public virtual ICollection<DocumentoModel> Documentos { get; set; } = new List<DocumentoModel>();

    public virtual GeneropersonaModel Fkgen { get; set; } = null!;

    public virtual InstitucionModel Fkins { get; set; } = null!;

    public virtual UsuarioModel FkusuCorreoNavigation { get; set; } = null!;
}
