using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProyectoDePaz.Data;
using ProyectoDePaz.Models;

namespace ProyectoDePaz.Controllers
{
    public class VerDocumentoController : Controller
    {
        private readonly MySqlConnection connection;
        public VerDocumentoController(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public IActionResult VerHistoria(string id)
        {
            HistoriasData historias = new HistoriasData(connection);
            ContenedorModel documento = historias.GetDocumento(id);
            return View("VerHistoria", documento);
        }

    }

}