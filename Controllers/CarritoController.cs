using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_CarritoCompras.Models;
using ProyectoFinal_CarritoCompras.Datos;
using ProyectoFinal_CarritoCompras.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ProyectoFinal_CarritoCompras.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarritoController( ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categorias = _context.Categorias.ToList();
            var marcas = _context.Marcas.ToList();
            var carrito = HttpContext.Session.GetObjectFromJson<List<CarroCompra>>("Carro") ?? new List<CarroCompra>();
            var viewModel = new VistaDetalleVentacs { Categorias = categorias, Marcas = marcas, CarroCompra = carrito };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult BuscarProductos(string tipoBusqueda, int id)
        {
            IQueryable<Producto> productos = _context.Productos.Include(p => p.Categoria).Include(p => p.Marca);

            if (tipoBusqueda == "Categoria")
            {
                productos = productos.Where(p => p.IdCategoria == id);
            }
            else if (tipoBusqueda == "Marca")
            {
                productos = productos.Where(p => p.IdMarca == id);
            }

            var productosList = productos.ToList();
            var categorias = _context.Categorias.ToList();
            var marcas = _context.Marcas.ToList();
            var viewModel = new VistaDetalleVentacs
            {
                Categorias = categorias,
                Marcas = marcas,
                Producto = productosList,
                TipoBusqueda = tipoBusqueda
            };

            return View("Index", viewModel);
        }

        [Authorize]

        [HttpPost]
        public IActionResult AgregarAlCarro(int idProducto, int cantidad)
        {
            
                var producto = _context.Productos.Include(p => p.Categoria).Include(p => p.Marca).FirstOrDefault(p => p.IdProducto == idProducto);

                if (producto == null)
                {
                    return NotFound();
                }

                List<CarroCompra> carro = HttpContext.Session.GetObjectFromJson<List<CarroCompra>>("Carro") ?? new List<CarroCompra>();

                var itemEnCarro = carro.FirstOrDefault(c => c.Producto.IdProducto == idProducto);

                if (itemEnCarro == null)
                {
                    carro.Add(new CarroCompra { Producto = producto, Cantidad = cantidad });
                }
                else
                {
                    itemEnCarro.Cantidad += cantidad;
                }

                HttpContext.Session.SetObjectAsJson("Carro", carro);

                return RedirectToAction("Index");
           
        }

        [HttpPost]
        public IActionResult ActualizarCantidad(int idProducto, int cantidad)
        {
            List<CarroCompra> carro = HttpContext.Session.GetObjectFromJson<List<CarroCompra>>("Carro");

            if (carro != null)
            {
                var itemEnCarro = carro.FirstOrDefault(c => c.Producto.IdProducto == idProducto);
                if (itemEnCarro != null)
                {
                    itemEnCarro.Cantidad = cantidad;
                    HttpContext.Session.SetObjectAsJson("Carro", carro);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EliminarDelCarro(int idProducto)
        {
            List<CarroCompra> carro = HttpContext.Session.GetObjectFromJson<List<CarroCompra>>("Carro");

            if (carro != null)
            {
                var itemEnCarro = carro.FirstOrDefault(c => c.Producto.IdProducto == idProducto);
                if (itemEnCarro != null)
                {
                    carro.Remove(itemEnCarro);
                    HttpContext.Session.SetObjectAsJson("Carro", carro);
                }
            }

            return RedirectToAction("Index");
        }

        [Authorize]

        [HttpPost]
        public IActionResult RealizarPago()
        {
            var carroItems = HttpContext.Session.GetObjectFromJson<List<CarroCompra>>("Carro") ?? new List<CarroCompra>();
            HttpContext.Session.Remove("Carro");
            return View("ConfirmacionCompra", carroItems);
        }

        [Authorize]

        [HttpPost]
        public IActionResult ConfirmarCompra()
        {
            var carroItems = HttpContext.Session.GetObjectFromJson<List<CarroCompra>>("Carro") ?? new List<CarroCompra>();

            if (carroItems.Count == 0)
            {
                return RedirectToAction("Index");
            }

            var nuevaVenta = new DetalleVenta
            {
                Subtotal = carroItems.Sum(item => item.Producto.Precio * item.Cantidad),
                Total = carroItems.Sum(item => item.Producto.Precio * item.Cantidad) 
            };


            foreach (var item in carroItems)
            {
                var detalle = new CarroCompra
                {
                    IdProducto = item.Producto.IdProducto,
                    Cantidad = item.Cantidad,
                    Subtotal = item.Producto.Precio * item.Cantidad
                };
            }

            _context.SaveChanges();

            HttpContext.Session.Remove("Carro");

            return RedirectToAction("CompraConfirmada");
        }

        public IActionResult ObtenerOpciones(string tipoBusqueda)
        {
            List<SelectListItem> opciones = new List<SelectListItem>();

            if (tipoBusqueda == "Categoria")
            {
                var categorias = _context.Categorias.ToList();
                opciones = categorias.Select(c => new SelectListItem
                {
                    Value = c.IdCategoria.ToString(),
                    Text = c.NombreCategoria
                }).ToList();
            }
            else if (tipoBusqueda == "Marca")
            {
                var marcas = _context.Marcas.ToList();
                opciones = marcas.Select(m => new SelectListItem
                {
                    Value = m.IdMarca.ToString(),
                    Text = m.NombreMarca
                }).ToList();
            }

            return Json(opciones);
        }

        public IActionResult CompraConfirmada()
        {
            ViewBag.MensajeConfirmacion = "Compra Realizada con éxito";
            return View();
        }


    }
}
