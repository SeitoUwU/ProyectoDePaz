using Microsoft.AspNetCore.Mvc;

namespace ProyectoDePaz.Controllers
{
    public class HistoriasController : Controller
    {
        public IActionResult Historias()
        {
            return View();
        }
        public IActionResult SubirHistoria()
        {
            return View();
        }
        public IActionResult MapaInteractivo()
        {
            return View();
        }
    }
}
