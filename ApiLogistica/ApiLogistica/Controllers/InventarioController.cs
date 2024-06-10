using ApiLogistica.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ApiLogistica.Controllers
{
    [ApiController]
    [Route("inventario")]
    public class InventarioController : ControllerBase
    {
        private static List<Producto> productos = new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Producto A", Precio = 100, Stock = 50 },
            new Producto { Id = 2, Nombre = "Producto B", Precio = 200, Stock = 30 }
        };

        [HttpGet]
        [Route("listar")]
        public dynamic ListarInventario()
        {
            return productos;
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarInventario(int id, [FromBody] int cantidad)
        {
            var producto = productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            producto.Stock = cantidad;
            return NoContent();
        }

        [HttpPut]
        [Route("reducir/{id}")]
        public IActionResult ReducirStock(int id, [FromBody] int cantidad)
        {
            var producto = productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            if (producto.Stock < cantidad)
            {
                return BadRequest(new { message = "Stock insuficiente" });
            }

            producto.Stock -= cantidad;
            return Ok(producto);
        }
    }
}
