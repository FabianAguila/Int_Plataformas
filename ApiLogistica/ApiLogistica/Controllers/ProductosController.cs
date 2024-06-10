using ApiLogistica.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiLogistica.Controllers
{
    [ApiController]
    [Route("producto")]
    public class ProductosController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ProductosController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("listar")]
        public dynamic ListarProductos()
        {
            List<Producto> productos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Producto A", Precio = 100, Stock = 50 },
                new Producto { Id = 2, Nombre = "Producto B", Precio = 200, Stock = 30 }
            };
            return productos;
        }

        [HttpPost]
        [Route("guardar")]
        public async Task<IActionResult> GuardarProducto([FromBody] Producto producto)
        {
            int cantidad = producto.Cantidad;

            var content = new StringContent(JsonSerializer.Serialize(cantidad), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"http://localhost:5000/inventario/reducir/{producto.Id}", content);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(new { message = "No se pudo reducir el stock" });
            }

            return Ok(new
            {
                success = true,
                message = "Producto registrado y stock actualizado",
                result = producto
            });
        }
    }
}
