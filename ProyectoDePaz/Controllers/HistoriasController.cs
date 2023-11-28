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
            HistoriasData historias = new HistoriasData(connection);
            IngresoUsuarioData ingreso = new IngresoUsuarioData(connection);
            
            List<DepartamentoModel> dep = ingreso.getDepartamentos();
            List<EtiquetaModel> etiquetas = historias.getEtiquetas();

            ViewBag.departamentos = new SelectList(dep, "DepId", "DepNombre");
            return View(etiquetas);
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

        [HttpPost]
        public async Task<IActionResult> SubirHistoria(
            string titulo, string municipio, string descripcion, Boolean check, string[] etiquetas, IFormFile documento)
        {
            
            return View();
        }
    }
}
