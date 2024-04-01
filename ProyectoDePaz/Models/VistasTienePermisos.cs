using Microsoft.AspNetCore.Mvc;

namespace ProyectoDePaz.Models;

public partial class VistasTienePermisos
{
    public string pfkvis_id { get; set; } = null!;
    public string pfkperm_id { get; set; } = null!;
    public Boolean vistienperm_estado { get; set; }
}
