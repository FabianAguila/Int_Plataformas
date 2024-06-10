using ApiLogistica.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogistica.Controllers
{
    public class EntregasController
    {
        [HttpGet]
        [Route("listar")]
        public dynamic ListarEntregas()
        {
            List<Entrega> entregas = new List<Entrega>
        {
            new Entrega
            {
                Id = 1,
                PedidoId = 1,
                Pedido = new Pedido { Id = "1", ClienteId = "1", Cliente = new Cliente { Id = 1, Nombre = "Cosme Fulanito" } },
                FechaEntrega = new DateTime(2023, 4, 7),
                DireccionEntrega = "El salto 300",
                Estado = "En camino"
            },
            new Entrega
            {
                Id = 2,
                PedidoId = 2,
                Pedido = new Pedido { Id = "2", ClienteId = "2", Cliente = new Cliente { Id = 2, Nombre = "Fulano Aldo" } },
                FechaEntrega = new DateTime(2023, 4, 7),
                DireccionEntrega = "Saltando 600",
                Estado = "Entregado"
            }
        };
            return entregas;
        }
        [HttpPost]
        [Route("guardar")]
        public dynamic GuardarEntrega(Entrega entrega)
        {
            entrega.Id = 3; // Generar un nuevo ID de entrega
            entrega.FechaEntrega = new DateTime(2023, 4, 7);
            entrega.Estado = "Pendiente";

            return new
            {
                success = true,
                message = "Entrega registrada",
                result = entrega
            };
        }
    }
}
