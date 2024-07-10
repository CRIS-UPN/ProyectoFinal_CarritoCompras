namespace ProyectoFinal_CarritoCompras.Models
{
    public class ConfirmacionCompra
    {
        public DetalleVenta DetalleVenta { get; set; }

        public CarroCompra CarroCompra { get; set; }
        public List<ItemCarrito> Productos { get; set; }
    }
}
