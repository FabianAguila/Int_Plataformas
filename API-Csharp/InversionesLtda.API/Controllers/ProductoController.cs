using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InversionesLtda.API.Models;
using System.Collections.Generic;
using System.Linq;


namespace InversionesLtda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private static IList<Producto> lista = new List<Producto>();

        // GET: ProductoController
        [HttpGet]
        public IEnumerable<Producto> Get()
        {
            return lista;
        }

        // GET: ProductoController/5
        [HttpGet("{id}")]
        public Producto Get(int id)
        {
            return lista.FirstOrDefault(x => x.id == id);
        }

        // POST api/<ProductoController>
        [HttpPost]
        public void Post([FromBody] Producto value)
        {
            lista.Add(value);
        }

        // PUT api/<ProductoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Producto value)
            {
                Producto selection = lista.FirstOrDefault(x => x.id == id);
                lista[lista.IndexOf(selection)] = value;
            }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
            {
                lista.Remove(lista.FirstOrDefault(x => x.id == id));
            }
    }
}

