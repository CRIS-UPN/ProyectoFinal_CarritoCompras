using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
namespace ProyectoFinal_CarritoCompras.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }

        [JsonIgnore]
        public ICollection<Producto> Productos { get; set; }
    }
}
