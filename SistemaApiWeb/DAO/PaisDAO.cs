using SistemaApiWeb.Entidades;
using Microsoft.Data.SqlClient;

namespace SistemaApiWeb.DAO
{
    public class PaisDAO
    {
        private string cadena;

        public PaisDAO() { 
            cadena = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("cn");
        }

        public IEnumerable<Pais> GetPaises() { 
            List<Pais> paisList = new List<Pais>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("exec sp_listar_paises", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) {
                    paisList.Add(new Pais()
                    {
                        idpais = dr.GetInt32(0),
                        nombrepais = dr.GetString(1)
                    });
                }
            }
            return paisList;
        }

    }
}
