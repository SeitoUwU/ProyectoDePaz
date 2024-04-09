using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using ProyectoDePaz.Models;
using System;
using System.Security.Claims;
using ProyectoDePaz.Data;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace ProyectoDePaz.Controllers
{
    public class IngresoUsuarioController : Controller
    {
        private readonly MySqlConnection connection;
        private readonly INotyfService _notyService;
        public IngresoUsuarioController(MySqlConnection connection, INotyfService notyService)
        {
            this.connection = connection;
            _notyService = notyService;
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
                _notyService.Success("Bienvenido " + usu.UsuCorreo);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                TempData["Error"] = "Correo o contraseña incorrectos";
                return RedirectToAction("InicioSesion", "IngresoUsuario");
            }
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
