using NuGet.Versioning;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
namespace ProyectoFinal_CarritoCompras.Models
{
    public class CarroCompra
    {
        [Key]
        public int IdCarro { get; set; }
        public float Subtotal { get; set; }

        public float Cantidad { get; set; }

        public int IdProducto { get; set; }
        [ForeignKey("IdProducto")]
        public Producto Producto { get; set; }

        public ICollection<ItemCarrito> ItemsCarrito { get; set; }
    }
}
