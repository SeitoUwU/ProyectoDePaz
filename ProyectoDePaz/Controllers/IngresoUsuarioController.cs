using Microsoft.AspNetCore.Mvc;

namespace ProyectoDePaz.Controllers
{
    public class IngresoUsuarioController : Controller
    {
        public IActionResult InicioSesion()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }
    }
}
