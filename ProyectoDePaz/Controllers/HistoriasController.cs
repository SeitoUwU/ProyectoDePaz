using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using ProyectoDePaz.Data;
using ProyectoDePaz.Models;

namespace ProyectoDePaz.Controllers
{
    public class HistoriasController : Controller
    {
        private readonly MySqlConnection connection;
        public HistoriasController(MySqlConnection connection)
        {
            this.connection = connection;
        }
        public IActionResult Historias()
        {
            return View();
        }
        public IActionResult SubirHistoria()
        {
            IngresoUsuarioData ing = new IngresoUsuarioData(connection);
            List<DepartamentoModel> dep = ing.getDepartamentos();
            ViewBag.departamentos = new SelectList(dep, "DepId", "DepNombre");
            return View();
        }
        public IActionResult MapaInteractivo()
        {
            return View();
        }

        [HttpGet]
        public ActionResult mostrarMunicipios(string depId)
        {
            IngresoUsuarioData ingreso = new IngresoUsuarioData(connection);
            List<MunicipioModel> mun = ingreso.getMunicipios(depId);
            return Json(mun);
        }
    }
}
