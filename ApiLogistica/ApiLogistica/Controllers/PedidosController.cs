using ApiLogistica.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogistica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private static List<Pedido> lista = new List<Pedido>
    {
        new Pedido
        {
            Id = 1,
            ClienteId = 1,
            Cliente = new Cliente { Id = 1, Nombre = "Cosme Fulanito", Direccion = "El salto 300", Telefono = "988997755", Email = "cosme@fulanito.com" },
            FechaPedido = DateTime.Now.AddDays(-5),
            Estado = "Completado",
            Detalles = new List<PedidoDetalle>
            {
                new PedidoDetalle { Id = 1, PedidoId = 1, ProductoId = 1, Producto = new Producto { Id = 1, Nombre = "Producto A", Precio = 100, Stock = 50 }, Cantidad = 2, PrecioUnitario = 100 }
            }
        },
        new Pedido
        {
            Id = 2,
            ClienteId = 2,
            Cliente = new Cliente { Id = 2, Nombre = "Fulano Aldo", Direccion = "Saltando 600", Telefono = "91122334455", Email = "aldo@fullano.com" },
            FechaPedido = DateTime.Now.AddDays(-3),
            Estado = "Pendiente",
            Detalles = new List<PedidoDetalle>
            {
                new PedidoDetalle { Id = 2, PedidoId = 2, ProductoId = 2, Producto = new Producto { Id = 2, Nombre = "Producto B", Precio = 200, Stock = 30 }, Cantidad = 1, PrecioUnitario = 200 }
            }
        }
    };

        // GET api/pedido
        [HttpGet]
        public IEnumerable<Pedido> Get()
        {
            return lista;
        }

        // GET api/pedido/5
        [HttpGet("{id}")]
        public ActionResult<Pedido> Get(int id)
        {
            var pedido = lista.FirstOrDefault(x => x.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }
            return pedido;
        }

        // POST api/pedido
        [HttpPost]
        public IActionResult Post([FromBody] Pedido value)
        {
            value.Id = lista.Max(p => p.Id) + 1; // Generar nuevo ID incrementado
            lista.Add(value);
            return Ok(new
            {
                success = true,
                message = "Pedido registrado",
                result = value
            });
        }

        // PUT api/pedido/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Pedido value)
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
                message = "Pedido actualizado",
                result = value
            });
        }

        // DELETE api/pedido/5
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
                message = "Pedido eliminado",
                result = id
            });
        }
    }
}