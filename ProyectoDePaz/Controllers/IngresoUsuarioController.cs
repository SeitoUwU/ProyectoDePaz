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
            List<RolModel> roles = ingUsu.getRol();
            List<GeneropersonaModel> generos = ingUsu.getGenero();

            ViewBag.departamento = new SelectList(dep, "DepId", "DepNombre");
            ViewBag.roles = new SelectList(roles, "RolId", "RolRol");
            ViewBag.generos = new SelectList(generos, "GenId", "GenGeneroPersona");
            return View();
        }

        [HttpGet]
        public ActionResult mostrarMunicipio(String depId)
        {
            IngresoUsuarioProced ingUsu = new IngresoUsuarioProced(connection);
            List<MunicipioModel> mun = ingUsu.getMunicipios(depId);
            return Json(mun);
        }

        [HttpGet]
        public ActionResult mostrarInstitucion(String munId)
        {
            IngresoUsuarioProced ingUsu = new IngresoUsuarioProced(connection);
            List<InstitucionModel> ins = ingUsu.getInstituciones(munId);
            return Json(ins);
        }

        [HttpPost]
        public IActionResult RegistroPersona(ContenedorModel contenedor)
        {
            IngresoUsuarioProced ingUsu = new IngresoUsuarioProced(connection);
            ingUsu.registrarUsuario(contenedor);
            return RedirectToAction("Index", "Home");
        }

    }
}
