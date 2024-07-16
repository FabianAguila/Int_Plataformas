using Microsoft.AspNetCore.Mvc;
using ApiLogistica.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApiLogistica.Controllers
{
    [Route("api/Compras")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CompraController> _logger;

        public CompraController(ApplicationDbContext context, ILogger<CompraController> logger)
        {
            _context = context;
            _logger = logger;
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
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _logger.LogInformation("Iniciando la creación de una nueva compra...");

                // Verificar si el cliente ya está siendo rastreado por el contexto
                var clienteExistente = await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == compra.ClienteId);
                if (clienteExistente == null)
                {
                    _context.Clientes.Add(compra.Cliente);
                    await _context.SaveChangesAsync();
                    compra.ClienteId = compra.Cliente.Id;
                }
                else
                {
                    compra.ClienteId = clienteExistente.Id;
                    _context.Entry(clienteExistente).State = EntityState.Detached;
                    _context.Attach(compra.Cliente);
                }
                var productoExistente = await _context.Productos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == compra.ProductoId);
                if (productoExistente == null)
                {
                    _context.Productos.Add(compra.Producto);
                    await _context.SaveChangesAsync();
                    compra.ProductoId = compra.Producto.Id;
                }
                else
                {
                    compra.ProductoId = productoExistente.Id;
                    _context.Entry(productoExistente).State = EntityState.Detached;
                    _context.Attach(compra.Producto);
                }
                _context.Compras.Add(compra);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("Compra creada con éxito.");
                return Ok(compra);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError($"Error al crear la compra: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
