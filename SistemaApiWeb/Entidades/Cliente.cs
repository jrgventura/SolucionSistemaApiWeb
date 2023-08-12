using Microsoft.AspNetCore.Mvc;

namespace SistemaApiWeb.Entidades
{
    public class Cliente
    {
        public int idcliente { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public int idpais { get; set; }
        public int telefono { get; set; }
    }
}
