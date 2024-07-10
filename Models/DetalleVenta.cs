using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace ProyectoFinal_CarritoCompras.Models
{
    public class DetalleVenta
    {
        [Key]
        public int IdVenta { get; set; }
        public float Subtotal { get; set; }
        public float Total { get; set; }

        public int IdCarro { get; set; }
        [ForeignKey("IdCarro")]
        public CarroCompra CarroCompra { get; set; }

        public virtual ICollection<CarroCompra> CarroCompraDetalles { get; set; } = new List<CarroCompra>();
    }
}
