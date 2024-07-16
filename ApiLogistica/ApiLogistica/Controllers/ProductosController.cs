using ApiLogistica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiLogistica.Controllers
{
    [Route("api/Productos")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/productos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var productos = await _context.Productos.ToListAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno en el servidor: {ex.Message}");
            }
        }

        // GET api/productos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        // POST api/productos
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return Ok(producto);
        }

        // PUT api/productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(producto);
        }

        // DELETE api/productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return Ok(producto);
        }
    }
}