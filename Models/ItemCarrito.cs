using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_CarritoCompras.Models
{
    public class ItemCarrito
    {

        [Key]
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }

        public float Subtotal { get; set; }

    }
}
