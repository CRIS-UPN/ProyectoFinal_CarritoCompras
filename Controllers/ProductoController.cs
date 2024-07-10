using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_CarritoCompras.Datos;
using ProyectoFinal_CarritoCompras.Models;
using System.Threading.Tasks;

namespace ProyectoFinal_CarritoCompras.Controllers
{
    public class ProductoController : Controller
    
    {
        private readonly ApplicationDbContext _context;

        public ProductoController (ApplicationDbContext context)

        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .ToListAsync();

            return View(productos);
        }
    }
}
