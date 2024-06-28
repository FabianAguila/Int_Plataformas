using ApiLogistica.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogistica.Controllers
{
    [Route("api/Productos")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private static List<Producto> lista = new List<Producto>
    {
        new Producto { Id = 1, Nombre = "Producto A", Precio = 100, Stock = 50 },
        new Producto { Id = 2, Nombre = "Producto B", Precio = 200, Stock = 30 }
    };

        // GET api/producto
        [HttpGet]
        public IEnumerable<Producto> Get()
        {
            return lista;
        }

        // GET api/producto/5
        [HttpGet("{id}")]
        public ActionResult<Producto> Get(int id)
        {
            var producto = lista.FirstOrDefault(x => x.Id == id);
            if (producto == null)
            {
                return NotFound();
            }
            return producto;
        }

        // POST api/producto
        [HttpPost]
        public IActionResult Post([FromBody] Producto value)
        {
            value.Id = lista.Max(p => p.Id) + 1; // Generar nuevo ID incrementado
            lista.Add(value);
            return Ok(new
            {
                success = true,
                message = "Producto registrado",
                result = value
            });
        }

        // PUT api/producto/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Producto value)
        {
            var selection = lista.FirstOrDefault(x => x.Id == id);
            if (selection == null)
            {
                return NotFound();
            }

            var index = lista.IndexOf(selection);
            lista[index] = value;

            return Ok(new
            {
                success = true,
                message = "Producto actualizado",
                result = value
            });
        }

        // DELETE api/producto/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var selection = lista.FirstOrDefault(x => x.Id == id);
            if (selection == null)
            {
                return NotFound();
            }

            lista.Remove(selection);

            return Ok(new
            {
                success = true,
                message = "Producto eliminado",
                result = id
            });
        }
    }
} 