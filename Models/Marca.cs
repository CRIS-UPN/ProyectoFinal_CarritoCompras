using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace ProyectoFinal_CarritoCompras.Models
{
    public class Marca
    {
        [Key]
        public int IdMarca { get; set; }
        public string NombreMarca { get; set; }


        [JsonIgnore]
        public ICollection<Producto> Productos { get; set; }
    }
}
