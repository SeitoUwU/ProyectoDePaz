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

        public Boolean RegistrarHistorias(DocumentoModel doc, string[] etiquetas)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(con.ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("insertarDocumento", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", doc.DocId);
                        cmd.Parameters.AddWithValue("@titulo", doc.DocTitulo);
                        cmd.Parameters.AddWithValue("@descripcion", doc.DocDescripcion);
                        cmd.Parameters.AddWithValue("@link", doc.DocLink);
                        cmd.Parameters.AddWithValue("@doc", doc.DocDocumento);
                        cmd.Parameters.AddWithValue("@correo", doc.FkperId);
                        cmd.Parameters.AddWithValue("@municipio", doc.FkmunId);
                        cmd.Parameters.AddWithValue("@tipdoc", doc.FktipdocId);
                        cmd.ExecuteNonQuery();
                    }
                    using (MySqlCommand cmd = new MySqlCommand("registrarEtiquetasHistoria", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        foreach (string eti in etiquetas)
                        {
                            cmd.Parameters.AddWithValue("@documento", doc.DocId);
                            cmd.Parameters.AddWithValue("@etiqueta", eti);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
