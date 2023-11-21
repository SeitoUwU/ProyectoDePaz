using ProyectoDePaz.Models;
using MySql.Data.MySqlClient;

namespace ProyectoDePaz.Procedimientos
{
    public class IngresoUsuarioProced
    {
        private readonly MySqlConnection con;

        public IngresoUsuarioProced(MySqlConnection con)
        {
            this.con = con;
        }
        public List<DepartamentoModel> getDepartamentos()
        {
            List<DepartamentoModel> dep = new List<DepartamentoModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.ConnectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new("mostrarDepartamentos", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DepartamentoModel departamento = new DepartamentoModel();
                                departamento.DepId = reader.GetString(0);
                                departamento.DepNombre = reader.GetString(1);
                                dep.Add(departamento);
                            }
                        }
                    }
                    connection.Close();

                }
            }
            catch (Exception ex)
            {

            }

            return dep;
        }

        public List<MunicipioModel> getMunicipios(String depId)
        {
            List<MunicipioModel> mun = new List<MunicipioModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.ConnectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new("mostrarMunicipiosSegunDep", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@dep", depId);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MunicipioModel municipio = new MunicipioModel();
                                municipio.MunId = reader.GetString(0);
                                municipio.MunNombre = reader.GetString(1);
                                mun.Add(municipio);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }

            return mun;
        }

        public List<InstitucionModel> getInstituciones(string munId)
        {
            List<InstitucionModel> ins = new List<InstitucionModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.ConnectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new("mostrarInstitucionSegunMun", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@mun", munId);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                InstitucionModel institucion = new InstitucionModel();
                                institucion.InsId = reader.GetString(0);
                                institucion.InsInstitucion = reader.GetString(1);
                                ins.Add(institucion);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return ins;
        }
    }
}
