using MySql.Data.MySqlClient;
using NuGet.Protocol.Plugins;
using ProyectoDePaz.Models;
using System.Data.SqlTypes;

namespace ProyectoDePaz.Data
{
    public class HistoriasData
    {
        private readonly MySqlConnection con;
        private readonly MySqlConnection connection;
        public HistoriasData(MySqlConnection con)
        {
            this.con = con;
            connection = new MySqlConnection(con.ConnectionString);
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

        public List<ContenedorModel> mostrarHistorias()
        {
            List<ContenedorModel> historias = new List<ContenedorModel>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(con.ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("mostrarDocumentos", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ContenedorModel historia = new ContenedorModel();
                                historia.documento.DocId = reader.GetString(0);
                                historia.documento.DocTitulo = reader.GetString(1);
                                historia.documento.DocDescripcion = reader.GetString(2);
                                historia.persona.PerNombreUno = reader.GetString(3);
                                historia.persona.PerApellidoUno = reader.GetString(4);
                                historia.tipodocumento.TipdocTipo = reader.GetString(5);
                                historia.publicacion.PubliFechaPublicacion = reader.GetString(6);
                                historias.Add(historia);
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            return historias;
        }

        public List<ContenedorModel> obtenerFiltros(List<ContenedorModel> list)
        {
            try
            {


                connection.Open();
                using (MySqlCommand command = new MySqlCommand("mostrarDepartamentos", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ContenedorModel contenedor = new ContenedorModel();
                            contenedor.departamento.DepId = reader.GetString(0);
                            contenedor.departamento.DepNombre = reader.GetString(1);
                            list.Add(contenedor);
                        }
                    }
                }
                connection.Close();

                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("mostrarTipoDocumentos", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ContenedorModel historia = new ContenedorModel();
                            historia.tipodocumento.TipdocId = reader.GetString(0);
                            historia.tipodocumento.TipdocTipo = reader.GetString(1);
                            list.Add(historia);
                        }
                    }
                }
                connection.Close();
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("mostrarEtiquetas", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ContenedorModel historia = new ContenedorModel();
                            historia.etiqueta.EtqId = reader.GetString(0);
                            historia.etiqueta.EtqTipo = reader.GetString(1);
                            list.Add(historia);
                        }
                    }
                }
                connection.Close();

            }
            catch (System.Exception) { throw; }
            return list;
        }

        public List<ContenedorModel> filtrarHistorias(string idEtq, string idDep, string idTipo)
        {
            List<ContenedorModel> historias = new List<ContenedorModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.ConnectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("filtrarDocumentos", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@etiqueta", idEtq);
                        cmd.Parameters.AddWithValue("@tipodoc", idTipo);
                        cmd.Parameters.AddWithValue("@dep", idDep);
                        cmd.ExecuteNonQuery();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ContenedorModel historia = new ContenedorModel();
                                historia.documento.DocId = reader.GetString(0);
                                historia.documento.DocTitulo = reader.GetString(1);
                                historia.documento.DocDescripcion = reader.GetString(2);
                                historia.persona.PerNombreUno = reader.GetString(3);
                                historia.persona.PerApellidoUno = reader.GetString(4);
                                historia.tipodocumento.TipdocTipo = reader.GetString(5);
                                historia.publicacion.PubliFechaPublicacion = reader.GetString(6);
                                historias.Add(historia);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (System.Exception) { throw; }
            return historias;
        }

        public List<ContenedorModel> getDocumentosMunicipio()
        {
            List<ContenedorModel> documentos = new List<ContenedorModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.ConnectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("mostrarDocumentosMuni", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.ExecuteNonQuery();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ContenedorModel documento = new ContenedorModel();
                                documento.documento.DocId = reader.GetString(0);
                                documento.municipio.MunNombre = reader.GetString(1);
                                documentos.Add(documento);
                            }
                        }
                    }
                }
            }
            catch (System.Exception) { throw; }
            return documentos;
        }

        public ContenedorModel GetDocumento(string id)
        {
            ContenedorModel documento = new ContenedorModel();
            try
            {

                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("mostrarDocumento", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            documento.tipodocumento.TipdocId = reader.GetString(0);
                            if (!reader.IsDBNull(1))
                            {
                                documento.documento.DocLink = reader.GetString(1);
                            }
                            if (!reader.IsDBNull(2))
                            {
                                long fileSize = reader.GetBytes(2, 0, null, 0, 0); // Obtener el tamaño del archivo
                                byte[] fileData = new byte[fileSize]; // Crear un arreglo de bytes del tamaño del archivo

                                long bytesRead = 0;
                                int bufferSize = 1024;
                                int currentIndex = 0;

                                while (bytesRead < fileSize)
                                {
                                    int curBytes = (int)reader.GetBytes(2, bytesRead, fileData, currentIndex, bufferSize);

                                    // Verificar si hay más datos para leer y ajustar el tamaño del búfer si es necesario
                                    if (curBytes == 0)
                                    {
                                        bufferSize *= 2; // Duplicar el tamaño del búfer si no se leyó ningún byte
                                    }

                                    bytesRead += curBytes;
                                    currentIndex += curBytes;

                                    if (currentIndex + bufferSize > fileSize)
                                    {
                                        bufferSize = (int)(fileSize - currentIndex); // Ajustar el tamaño del búfer si se alcanza el final del archivo
                                    }
                                }

                                // Redimensionar el arreglo de bytes al tamaño real
                                Array.Resize(ref fileData, (int)fileSize);

                                documento.documento.DocDocumento = fileData;
                            }
                        }
                    }
                }
                connection.Close();

            }
            catch (Exception)
            {

                throw;
            }
            return documento;
        }
    }
}
