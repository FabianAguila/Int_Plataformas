using ApiLogistica.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogistica.Controllers
{
    [Route("api/Inventario")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private static List<Inventario> lista = new List<Inventario>
    {
        new Inventario
        {
            Id = 1,
            ProductoId = 1,
            Producto = new Producto { Id = 1, Nombre = "Producto A", Precio = 100, Stock = 50 },
            Cantidad = 50,
            FechaActualizacion = DateTime.Now.AddDays(-1)
        },
        new Inventario
        {
            Id = 2,
            ProductoId = 2,
            Producto = new Producto { Id = 2, Nombre = "Producto B", Precio = 200, Stock = 30 },
            Cantidad = 30,
            FechaActualizacion = DateTime.Now.AddDays(-2)
        }
    };

        // GET api/inventario
        [HttpGet]
        public IEnumerable<Inventario> Get()
        {
            return lista;
        }

        // GET api/inventario/5
        [HttpGet("{id}")]
        public ActionResult<Inventario> Get(int id)
        {
            var inventario = lista.FirstOrDefault(x => x.Id == id);
            if (inventario == null)
            {
                return NotFound();
            }
            return inventario;
        }

        // POST api/inventario
        [HttpPost]
        public IActionResult Post([FromBody] Inventario value)
        {
            value.Id = lista.Max(i => i.Id) + 1; // Generar nuevo ID incrementado
            lista.Add(value);
            return Ok(new
            {
                success = true,
                message = "Inventario registrado",
                result = value
            });
        }

        // PUT api/inventario/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Inventario value)
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
                message = "Inventario actualizado",
                result = value
            });
        }

        // DELETE api/inventario/5
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
                message = "Inventario eliminado",
                result = id
            });
        }
    }
}