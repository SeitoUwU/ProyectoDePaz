using MySql.Data.MySqlClient;
using ProyectoDePaz.Models;

namespace ProyectoDePaz.Data
{
    public class HistoriasData
    {
        private readonly MySqlConnection con;
        public HistoriasData(MySqlConnection con)
        {
            this.con = con;
        }

        public List<EtiquetaModel> getEtiquetas()
        {
            List<EtiquetaModel> etiquetas = new List<EtiquetaModel>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(con.ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("mostrarEtiquetas", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EtiquetaModel etiqueta = new EtiquetaModel();
                                etiqueta.EtqId = reader.GetString(0);
                                etiqueta.EtqTipo = reader.GetString(1);
                                etiquetas.Add(etiqueta);
                            }
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return etiquetas;
        }
    }
}
