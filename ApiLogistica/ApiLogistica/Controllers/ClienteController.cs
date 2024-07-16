using ApiLogistica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiLogistica.Controllers
{
    [Route("api/Cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/cliente
        [HttpGet("ObtenerCli")]
        public async Task<IActionResult> Get()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return Ok(clientes);
        }

        // GET api/cliente/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        // POST api/cliente
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return Ok(cliente);
        }

        // PUT api/cliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(cliente);
        }

        // DELETE api/cliente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return Ok(cliente);
        }
    }
}
