using SistemaApiWeb.Entidades;
using Microsoft.Data.SqlClient;

namespace SistemaApiWeb.DAO
{
    public class ClienteDAO
    {
        private string cadena;

        public ClienteDAO() {
            cadena = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("cn");
        }

        public IEnumerable<Cliente> GetCliente() { 
            List<Cliente> clientesList = new List<Cliente>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand sqlCommand = new SqlCommand("exec sp_listar_clientes", 
                    cn);
                cn.Open();
                SqlDataReader dr = sqlCommand.ExecuteReader();
                while (dr.Read()) {
                    clientesList.Add(new Cliente()
                    {
                        idcliente = dr.GetInt32(0),
                        nombre = dr.GetString(1),
                        direccion = dr.GetString(2),
                        idpais = Int32.Parse(dr.GetString(3)),
                        telefono = dr.GetInt32(4),
                    });
                }
            }

            return clientesList;
        }

        public string Agregar(Cliente cliente) {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_insertar_cliente",
                        cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", cliente.nombre);
                    cmd.Parameters.AddWithValue("@direccion", cliente.direccion);
                    cmd.Parameters.AddWithValue("@idpais", cliente.idpais);
                    cmd.Parameters.AddWithValue("@telefono", cliente.telefono);
                    cn.Open();
                    int c = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha agregado {c} cliente";
                }
                catch (Exception ex) { mensaje = ex.Message;  } 
            }
            return mensaje;
        }

        public string Actualizar(Cliente cliente) {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena)) {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_editar_cliente", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcliente", cliente.idcliente);
                    cmd.Parameters.AddWithValue("@nombre", cliente.nombre);
                    cmd.Parameters.AddWithValue("@direccion", cliente.direccion);
                    cmd.Parameters.AddWithValue("@idpais", cliente.idpais);
                    cmd.Parameters.AddWithValue("@telefono", cliente.telefono);
                    cn.Open();
                    int c = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {c} cliente";
                }
                catch (Exception ex) { mensaje = ex.Message; }
            }
            return mensaje;
        }

        public Cliente GetCliente(int id)
        {
            Cliente cliente = new Cliente();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand sqlCommand = new SqlCommand("sp_buscar_cliente",
                    cn);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", id);
                cn.Open();
                SqlDataReader dr = sqlCommand.ExecuteReader();
                while (dr.Read())
                {
                    cliente.idcliente = dr.GetInt32(0);
                    cliente.nombre = dr.GetString(1);
                    cliente.direccion = dr.GetString(2);
                    cliente.idpais = Int32.Parse(dr.GetString(3));
                    cliente.telefono = dr.GetInt32(4);
                }
            }
            return cliente;
        }

        public string Eliminar(int id) {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(cadena)) {

                try {
                    SqlCommand cmd = new SqlCommand("sp_eliminar_cliente", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    int c = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha eliminado {c} cliente";
                } catch (Exception ex) { mensaje = ex.Message; }

            }

            return mensaje;
        }


    }
}
