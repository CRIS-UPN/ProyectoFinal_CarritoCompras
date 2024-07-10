namespace ProyectoFinal_CarritoCompras.Models
{
    public class VistaDetalleVentacs
    {
        public List<Categoria> Categorias { get; set; }
        public List<CarroCompra> CarroCompra { get; set; }

        public List<Marca> Marcas { get; set; }
        public List<Producto> Producto { get; set; }
        public List<ItemCarrito> Carro { get; set; }
        public float Subtotal { get; set; }
        public string TipoBusqueda { get; set; }

    }
}
