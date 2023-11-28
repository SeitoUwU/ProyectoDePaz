using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using ProyectoDePaz.Data;
using ProyectoDePaz.Models;
using System.Security.Claims;
using System.Text.Json;

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
        public IActionResult RegistrarComic([FromForm] Dictionary<string, string> formData, IFormFile documento)
        {
            DocumentoModel doc = new DocumentoModel();
            HistoriasData historia = new HistoriasData(connection);
            string[] etiquetas = null;
            byte[] archivo = null;
            if (formData != null && formData.Count > 0)
            {
                Guid id = Guid.NewGuid();
                doc.DocId = id.ToString();
                doc.DocTitulo = formData["titulo"];
                doc.DocDescripcion = formData["descripcion"];
                doc.FkmunId = formData["municipio"];
                doc.FktipdocId = "cde76276-8b8c-11ee-ac4e-cecd02c24f20";
                if (formData.TryGetValue("etiquetas", out string etiquetasJSON))
                {
                    etiquetas = JsonSerializer.Deserialize<string[]>(etiquetasJSON);
                }
                bool check = formData.ContainsKey("check") && bool.TryParse(formData["check"], out bool checkValue) ? checkValue : false;
                if (!check)
                {
                    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                    doc.FkperId = claim.Value;
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    documento.CopyTo(ms);
                    archivo = ms.ToArray();
                }
                doc.DocDocumento = archivo;
                historia.RegistrarHistorias(doc, etiquetas);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarVideo()
        {
            DocumentoModel doc = new DocumentoModel();
            HistoriasData historia = new HistoriasData(connection);
            string datos;
            using (var reader = new System.IO.StreamReader(Request.Body))
            {
                datos = await reader.ReadToEndAsync();
            }

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(datos);

            if (data != null)
            {
                if (
                    data.TryGetValue("titulo", out var tituloObj) &&
                    data.TryGetValue("municipio", out var municipioObj) &&
                    data.TryGetValue("descripcion", out var descripcionObj) &&
                    data.TryGetValue("check", out var checkObj) &&
                    data.TryGetValue("etiquetas", out var etiquetasObj) &&
                    data.TryGetValue("url", out var urlObj))
                {
                    Guid id = Guid.NewGuid();
                    doc.DocId = id.ToString();
                    doc.FktipdocId = "cdcb9d72-8b8c-11ee-ac4e-cecd02c24f20";
                    doc.DocTitulo = (string)tituloObj;
                    doc.FkmunId = (string)municipioObj;
                    doc.DocDescripcion = (string)descripcionObj;
                    doc.DocLink = (string)urlObj;
                    if (!(bool)checkObj)
                    {
                        Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                        doc.FkperId = claim.Value;
                    }
                    string[] etiquetas = ((JArray)etiquetasObj).ToObject<string[]>();
                    historia.RegistrarHistorias(doc, etiquetas);

                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
