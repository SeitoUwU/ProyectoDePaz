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

        public List<TipodocumentoModel> getTipoDocumento()
        {
            List<TipodocumentoModel> tipodocumento = new List<TipodocumentoModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.ConnectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new("mostrarTipoDocumento", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TipodocumentoModel tipodocumentoModel = new TipodocumentoModel();
                                tipodocumentoModel.TipdocId = reader.GetString(0);
                                tipodocumentoModel.TipdocTipo = reader.GetString(1);
                                tipodocumento.Add(tipodocumentoModel);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return tipodocumento;
        }
    }
}
