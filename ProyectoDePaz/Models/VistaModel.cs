using Microsoft.AspNetCore.Mvc;

namespace ProyectoDePaz.Models;

public partial class VistaModel
{
    public string vis_id { get; set; } = null!;

    public string vis_nombre { get; set; } = null!;

    public string vis_cotrolador { get; set; } = null!;

    public string vis_accion { get; set; } = null!;

    public Boolean vis_estado { get; set; }

}
