using Microsoft.AspNetCore.Mvc;
using ApiLogistica.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLogistica.Controllers
{
    [Route("api/Compras")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompraController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var compras = await _context.Compras.Include(c => c.Cliente).Include(c => c.Producto).ToListAsync();
            return Ok(compras);
        }

        [HttpGet("{productoId}/{clienteId}")]
        public async Task<IActionResult> GetById(int productoId, int clienteId)
        {
            var compra = await _context.Compras
                .Include(c => c.Cliente)
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(c => c.ProductoId == productoId && c.ClienteId == clienteId);
            if (compra == null)
            {
                return NotFound();
            }
            return Ok(compra);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Compra compra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var clienteExiste = await _context.Clientes.FindAsync(compra.Cliente.Id);
            if (clienteExiste == null)
            {
                _context.Clientes.Add(compra.Cliente);
                await _context.SaveChangesAsync();
            }
            else
            {
                compra.ClienteId = clienteExiste.Id;
                compra.NombreCliente = clienteExiste.Nombre;
                compra.DireccionCliente = clienteExiste.Direccion;
                compra.TelefonoCliente = clienteExiste.Telefono;
                compra.EmailCliente = clienteExiste.Email;
            }
            var productoExiste = await _context.Productos.FindAsync(compra.Producto.Id);
            if (productoExiste == null)
            {
                _context.Productos.Add(compra.Producto);
                await _context.SaveChangesAsync();
            }
            else
            {
                compra.ProductoId = productoExiste.Id;
                compra.NombreProducto = productoExiste.Nombre;
                compra.PrecioProducto = productoExiste.Precio;
                compra.StockProducto = productoExiste.Stock;
                compra.CantidadProducto = productoExiste.Cantidad;
            }
            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { productoId = compra.ProductoId, clienteId = compra.ClienteId }, compra);
        
    }

        [HttpPut("{productoId}/{clienteId}")]
        public async Task<IActionResult> Put(int productoId, int clienteId, [FromBody] Compra compra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (productoId != compra.ProductoId || clienteId != compra.ClienteId)
            {
                return BadRequest();
            }

            var compraExiste = await _context.Compras
                .Include(c => c.Cliente)
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(c => c.ProductoId == productoId && c.ClienteId == clienteId);
            if (compraExiste == null)
            {
                return NotFound();
            }

            compraExiste.Cliente.Nombre = compra.Cliente.Nombre;
            compraExiste.Cliente.Direccion = compra.Cliente.Direccion;
            compraExiste.Cliente.Telefono = compra.Cliente.Telefono;
            compraExiste.Cliente.Email = compra.Cliente.Email;
            compraExiste.NombreCliente = compra.Cliente.Nombre;
            compraExiste.DireccionCliente = compra.Cliente.Direccion;
            compraExiste.TelefonoCliente = compra.Cliente.Telefono;
            compraExiste.EmailCliente = compra.Cliente.Email;
            compraExiste.Producto.Nombre = compra.Producto.Nombre;
            compraExiste.Producto.Precio = compra.Producto.Precio;
            compraExiste.Producto.Stock = compra.Producto.Stock;
            compraExiste.Producto.Cantidad = compra.Producto.Cantidad;
            compraExiste.NombreProducto = compra.Producto.Nombre;
            compraExiste.PrecioProducto = compra.Producto.Precio;
            compraExiste.StockProducto = compra.Producto.Stock;
            compraExiste.CantidadProducto = compra.Producto.Cantidad;
            compraExiste.Cantidad = compra.Cantidad;
            compraExiste.TotalCosto = compra.TotalCosto;

            await _context.SaveChangesAsync();

            return Ok(compraExiste);

        }

        [HttpDelete("{productoId}/{clienteId}")]
        public async Task<IActionResult> Delete(int productoId, int clienteId)
        {
            var compra = await _context.Compras
                .FirstOrDefaultAsync(c => c.ProductoId == productoId && c.ClienteId == clienteId);
            if (compra == null)
            {
                return NotFound();
            }
            _context.Compras.Remove(compra);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}