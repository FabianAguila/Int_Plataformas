using ApiLogistica.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogistica.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private static List<Cliente> lista = new List<Cliente>
    {
        new Cliente { Id = 1, Nombre = "Cosme Fulanito", Direccion = "El salto 300", Telefono = "988997755", Email = "cosme@fulanito.com" },
        new Cliente { Id = 2, Nombre = "Fulano Aldo", Direccion = "Saltando 600", Telefono = "91122334455", Email = "aldo@fullano.com" }
    };

        // GET api/clientes
        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            return lista;
        }

        // GET api/clientes/5
        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(string id)
        {
            // Convertir el parámetro id a int
            if (!int.TryParse(id, out int clienteId))
            {
                // Manejar el caso en el que el id no sea un número válido
                return BadRequest("ID de cliente no válido");
            }

            // Buscar el cliente por su ID convertido a int
            var cliente = lista.FirstOrDefault(x => x.Id == clienteId);
            if (cliente == null)
            {
                return NotFound();
            }
            return cliente;
        }

        // POST api/clientes
        [HttpPost]
        public IActionResult Post([FromBody] Cliente value)
        {
            lista.Add(value);
            return Ok(new
            {
                success = true,
                message = "Cliente registrado",
                result = value
            });
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Cliente value)
        {
            if (!int.TryParse(id, out int clienteId))
            {
                return BadRequest("ID de cliente no válido");
            }
            var selection = lista.FirstOrDefault(x => x.Id == clienteId);
            if (selection == null)
            {
                return NotFound();
            }

            var index = lista.IndexOf(selection);
            lista[index] = value;

            return Ok(new
            {
                success = true,
                message = "Cliente actualizado",
                result = value
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!int.TryParse(id, out int clienteId))
            {
                return BadRequest("ID de cliente no válido");
            }
            var selection = lista.FirstOrDefault(x => x.Id == clienteId);
            if (selection == null)
            {
                return NotFound();
            }
            lista.Remove(selection);

            return Ok(new
            {
                success = true,
                message = "Cliente eliminado",
                result = clienteId
            });
        }
    }
    }




