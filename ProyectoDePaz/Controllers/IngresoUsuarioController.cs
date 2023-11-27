using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using ProyectoDePaz.Models;
using System;
using System.Security.Claims;
using ProyectoDePaz.Data;

namespace ProyectoDePaz.Controllers
{
    public class IngresoUsuarioController : Controller
    {
        private readonly MySqlConnection connection;
        public IngresoUsuarioController(MySqlConnection connection)
        {
            this.connection = connection;
        }
        public IActionResult InicioSesion()
        {
            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null)
            {
                if (c.Identity.IsAuthenticated)
                {
                    //return RedirectToAction("inicioAsesor", "AsesorController");
                }
            }
            return View();
        }

        public IActionResult Registro()
        {
            IngresoUsuarioData ingUsu = new IngresoUsuarioData(connection);
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
            IngresoUsuarioData ingUsu = new IngresoUsuarioData(connection);
            List<MunicipioModel> mun = ingUsu.getMunicipios(depId);
            return Json(mun);
        }

        [HttpGet]
        public ActionResult mostrarInstitucion(String munId)
        {
            IngresoUsuarioData ingUsu = new IngresoUsuarioData(connection);
            List<InstitucionModel> ins = ingUsu.getInstituciones(munId);
            return Json(ins);
        }

        [HttpPost]
        public async Task<ActionResult> IniciarSesion(UsuarioModel usuario)
        {
            IngresoUsuarioData ingreso = new IngresoUsuarioData(connection);
            UsuarioModel usu = ingreso.inicioSesion(usuario);
            if (usu.FkrolId != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.UsuCorreo),
                    new Claim(ClaimTypes.Role, usu.FkrolId),
                    new Claim("correo", usuario.UsuCorreo)
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = false,
                    ExpiresUtc = DateTimeOffset.MaxValue
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);
                if (usu.FkrolId == "a50e964f-8848-11ee-8027-cecd02c24f20" || usu.FkrolId == "a984b435-8848-11ee-8027-cecd02c24f20")
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("InicioSesion", "IngresoUsuario");
        }

        [HttpPost]
        public IActionResult RegistroPersona(
            string nombreUno, string apellUno, long telefono,
            string nombreDos, string apellDos, string fechaNacimiento, string rol, string genero,
            string institucion, string correo, string contrasenia)
        {
            IngresoUsuarioData ingUsu = new IngresoUsuarioData(connection);
            ContenedorModel contenedor = new ContenedorModel();

            int anio = DateTime.Now.Year;
            DateTime fechaNac = Convert.ToDateTime(fechaNacimiento);
            int edad = anio - fechaNac.Year;

            contenedor.persona.PerNombreUno = nombreUno;
            contenedor.persona.PerApellidoUno = apellUno;
            contenedor.persona.PerTelefono = telefono;
            contenedor.persona.PerNombreDos = nombreDos;
            contenedor.persona.PerApellidoDos = apellDos;
            contenedor.persona.PerEdad = edad + "";
            contenedor.usuario.FkrolId = rol;
            contenedor.persona.FkgenId = genero;
            contenedor.persona.FkinsId = institucion;
            contenedor.usuario.UsuCorreo = correo;
            contenedor.usuario.UsuContrasenia = contrasenia;

            ingUsu.registrarUsuario(contenedor);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
