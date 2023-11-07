using Microsoft.AspNetCore.Mvc;

namespace ProyectoDePaz.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
