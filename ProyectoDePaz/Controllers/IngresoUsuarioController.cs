using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using ProyectoDePaz.Models;
using ProyectoDePaz.Procedimientos;

namespace ProyectoDePaz.Controllers
{
    public class IngresoUsuarioController : Controller
    {
        private readonly MySqlConnection connection;
        public IngresoUsuarioController(MySqlConnection connection) { 
            this.connection = connection;
        }
        public IActionResult InicioSesion()
        {
            return View();
        }

        public IActionResult Registro()
        {
            IngresoUsuarioProced ingUsu = new IngresoUsuarioProced(connection);

            List<DepartamentoModel> dep = ingUsu.getDepartamentos();

            ViewBag.departamento = new SelectList(dep, "DepId", "DepNombre");
            return View();
        }

        [HttpGet]
        public ActionResult mostrarMunicipio(String depId)
        {
            IngresoUsuarioProced ingUsu = new IngresoUsuarioProced(connection);
            List<MunicipioModel> mun = ingUsu.getMunicipios(depId);
            ViewBag.Municipios = new SelectList(mun, "MunId", "MunNombre");
            return PartialView("_SelectsDinamicos");
        }

        [HttpGet]
        public ActionResult mostrarInstitucion(String munId)
        {
            IngresoUsuarioProced ingUsu = new IngresoUsuarioProced(connection);
            List<InstitucionModel> ins = ingUsu.getInstituciones(munId);
            ViewBag.Instituciones = new SelectList(ins, "InsId", "InsInstitucion");
            return PartialView("_SelectsDinamicos");
        }
        
    }
}
