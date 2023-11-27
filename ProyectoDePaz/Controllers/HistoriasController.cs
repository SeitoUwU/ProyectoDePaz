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
            List<EtiquetaModel> etiquetas = historias.getEtiquetas();
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
    }
}
