using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace ProyectoFinal_CarritoCompras.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public float Precio { get; set; }
        public int Cantidad { get; set; }
        public string RutaImagen { get; set; }

        public int IdCategoria { get; set; }
        [ForeignKey("IdCategoria")]

        [JsonIgnore]
        public Categoria Categoria { get; set; }

        public int IdMarca { get; set; }
        [ForeignKey("IdMarca")]

        [JsonIgnore]
        public Marca Marca { get; set; }

    }
}
