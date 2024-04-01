using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProyectoDePaz.Models;

namespace ProyectoDePaz.Componente
{
    public class MenuDinamico : ViewComponent
    {
        private readonly MySqlConnection connection;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MenuDinamico(MySqlConnection connection, IHttpContextAccessor httpContextAccessor)
        {
            this.connection = connection;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            List<VistaModel> menu = verVistas(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            return View(menu);
        }

        private List<VistaModel> verVistas(string correo)
        {
            List<VistaModel> vistas = new List<VistaModel>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connection.ConnectionString))
                {
                    conn.Open(); 
                    using (MySqlCommand cmd = new MySqlCommand("obtenerVistasRol", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.ExecuteNonQuery();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VistaModel vista = new VistaModel();
                                vista.vis_nombre = reader.GetString("VIS_NombreVista");
                                vista.vis_cotrolador = reader.GetString("VIS_Controlador");
                                vista.vis_accion = reader.GetString("VIS_Accion");
                                vistas.Add(vista);

                            }
                        }
                    }
                    conn.Close();
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return vistas;
        }
    }
}
