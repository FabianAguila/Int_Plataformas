using ApiLogistica.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogistica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosDetalleController : ControllerBase
    {
        private static List<PedidoDetalle> lista = new List<PedidoDetalle>
    {
        new PedidoDetalle
        {
            Id = 1,
            PedidoId = 1,
            Pedido = new Pedido { Id = 1, ClienteId = 1, FechaPedido = DateTime.Now.AddDays(-5), Estado = "Completado" },
            ProductoId = 1,
            Producto = new Producto { Id = 1, Nombre = "Producto A", Precio = 100, Stock = 50 },
            Cantidad = 2,
            PrecioUnitario = 100
        },
        new PedidoDetalle
        {
            Id = 2,
            PedidoId = 2,
            Pedido = new Pedido { Id = 2, ClienteId = 2, FechaPedido = DateTime.Now.AddDays(-3), Estado = "Pendiente" },
            ProductoId = 2,
            Producto = new Producto { Id = 2, Nombre = "Producto B", Precio = 200, Stock = 30 },
            Cantidad = 1,
            PrecioUnitario = 200
        }
    };

        // GET api/pedidosdetalle
        [HttpGet]
        public IEnumerable<PedidoDetalle> Get()
        {
            return lista;
        }

        // GET api/pedidosdetalle/5
        [HttpGet("{id}")]
        public ActionResult<PedidoDetalle> Get(int id)
        {
            var detalle = lista.FirstOrDefault(x => x.Id == id);
            if (detalle == null)
            {
                return NotFound();
            }
            return detalle;
        }

        // POST api/pedidosdetalle
        [HttpPost]
        public IActionResult Post([FromBody] PedidoDetalle value)
        {
            value.Id = lista.Max(d => d.Id) + 1; // Generar nuevo ID incrementado
            lista.Add(value);
            return Ok(new
            {
                success = true,
                message = "Detalle del pedido registrado",
                result = value
            });
        }

        // PUT api/pedidosdetalle/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PedidoDetalle value)
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
                message = "Detalle del pedido actualizado",
                result = value
            });
        }

        // DELETE api/pedidosdetalle/5
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
                message = "Detalle del pedido eliminado",
                result = id
            });
        }
    }
}