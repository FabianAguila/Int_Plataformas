using ApiLogistica.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogistica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntregasController : ControllerBase
    {
        private static List<Entrega> lista = new List<Entrega>
    {
        new Entrega
        {
            Id = 1,
            PedidoId = 1,
            Pedido = new Pedido
            {
                Id = 1,
                ClienteId = 1,
                Cliente = new Cliente { Id = 1, Nombre = "Cosme Fulanito", Direccion = "El salto 300", Telefono = "988997755", Email = "cosme@fulanito.com" },
                FechaPedido = DateTime.Now.AddDays(-5),
                Estado = "Completado"
            },
            FechaEntrega = DateTime.Now.AddDays(-2),
            DireccionEntrega = "El salto 300",
            Estado = "Entregado"
        },
        new Entrega
        {
            Id = 2,
            PedidoId = 2,
            Pedido = new Pedido
            {
                Id = 2,
                ClienteId = 2,
                Cliente = new Cliente { Id = 2, Nombre = "Fulano Aldo", Direccion = "Saltando 600", Telefono = "91122334455", Email = "aldo@fullano.com" },
                FechaPedido = DateTime.Now.AddDays(-3),
                Estado = "Pendiente"
            },
            FechaEntrega = DateTime.Now.AddDays(-1),
            DireccionEntrega = "Saltando 600",
            Estado = "En camino"
        }
    };

        // GET api/entregas
        [HttpGet]
        public IEnumerable<Entrega> Get()
        {
            return lista;
        }

        // GET api/entregas/5
        [HttpGet("{id}")]
        public ActionResult<Entrega> Get(int id)
        {
            var entrega = lista.FirstOrDefault(x => x.Id == id);
            if (entrega == null)
            {
                return NotFound();
            }
            return entrega;
        }

        // POST api/entregas
        [HttpPost]
        public IActionResult Post([FromBody] Entrega value)
        {
            value.Id = lista.Max(e => e.Id) + 1; // Generar nuevo ID incrementado
            lista.Add(value);
            return Ok(new
            {
                success = true,
                message = "Entrega registrada",
                result = value
            });
        }

        // PUT api/entregas/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Entrega value)
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
                message = "Entrega actualizada",
                result = value
            });
        }

        // DELETE api/entregas/5
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
                message = "Entrega eliminada",
                result = id
            });
        }
    }
}