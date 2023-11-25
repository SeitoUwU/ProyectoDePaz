using ProyectoDePaz.Models;
using MySql.Data.MySqlClient;

namespace ProyectoDePaz.Data
{
    public class IngresoUsuarioData
    {
        private readonly MySqlConnection con;

        public IngresoUsuarioData(MySqlConnection con)
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

        public List<RolModel> getRol()
        {
            List<RolModel> rol = new List<RolModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.ConnectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new("mostrarRoles", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RolModel rolModel = new RolModel();
                                rolModel.RolId = reader.GetString(0);
                                rolModel.RolRol = reader.GetString(1);
                                rol.Add(rolModel);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return rol;
        }

        public List<GeneropersonaModel> getGenero()
        {
            List<GeneropersonaModel> gen = new List<GeneropersonaModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.ConnectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new("mostrarGenero", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GeneropersonaModel genero = new GeneropersonaModel();
                                genero.GenId = reader.GetString(0);
                                genero.GenGeneroPersona = reader.GetString(1);
                                gen.Add(genero);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return gen;
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

        public void registrarUsuario(ContenedorModel contenedor)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(con.ConnectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new("registrarUsuario", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@nombreUno", contenedor.persona.PerNombreUno);
                        command.Parameters.AddWithValue("@nombreDos", contenedor.persona.PerNombreDos);
                        command.Parameters.AddWithValue("@ApellUno", contenedor.persona.PerApellidoUno);
                        command.Parameters.AddWithValue("@ApellDos", contenedor.persona.PerApellidoDos);
                        command.Parameters.AddWithValue("@telefono", contenedor.persona.PerTelefono);
                        command.Parameters.AddWithValue("@edad", contenedor.persona.PerEdad);
                        command.Parameters.AddWithValue("@tipopersona", contenedor.usuario.FkrolId);
                        command.Parameters.AddWithValue("@correo", contenedor.usuario.UsuCorreo);
                        command.Parameters.AddWithValue("@contrasenia", contenedor.usuario.UsuContrasenia);
                        command.Parameters.AddWithValue("@genero", contenedor.persona.FkgenId);
                        command.Parameters.AddWithValue("@instituto", contenedor.persona.FkinsId);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public UsuarioModel inicioSesion(UsuarioModel usu)
        {
            UsuarioModel usuario = new UsuarioModel();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(con.ConnectionString))
                {
                    conn.Open();
                    using(MySqlCommand cmd = new MySqlCommand("inicioSesion", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@correo", usu.UsuCorreo);
                        cmd.Parameters.AddWithValue("@contrasenia", usu.UsuContrasenia);

                        cmd.ExecuteNonQuery();
                        using(MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usuario.FkrolId = reader.GetString(0);
                            }
                        }

                    }
                    conn.Close();
                }
            }catch (Exception ex)
            {

            }
            return usuario;
        }
    }
}
