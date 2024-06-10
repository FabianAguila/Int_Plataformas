using ApiLogistica.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogistica.Controllers
{
    [ApiController]
    [Route("pedidos")]
    public class PedidosController
    {
        [HttpGet]
        [Route("listar")]
        public dynamic ListarPedidos()
        {
            List<Pedido> pedidos = new List<Pedido>
        {
            new Pedido
            {
                Id = 1,
                ClienteId = 1,
                Cliente = new Cliente { Id = 1, Nombre = "Cosme Fulanito" },
                FechaPedido = new DateTime(2023, 4, 7),
                Estado = "Pendiente",
                Detalles = new List<PedidoDetalle>
                {
                    new PedidoDetalle
                    {
                        Id = 1,
                        PedidoId = 1,
                        ProductoId = 1,
                        Producto = new Producto { Id = 1, Nombre = "Producto A" },
                        Cantidad = 2,
                        PrecioUnitario = 100.0M
                    }
                }
            },
            new Pedido
            {
                Id = 2,
                ClienteId = 2,
                Cliente = new Cliente { Id = 2, Nombre = "Fulano Aldo" },
                FechaPedido = new DateTime(2023, 4, 7),
                Estado = "Enviado",
                Detalles = new List<PedidoDetalle>
                {
                    new PedidoDetalle
                    {
                        Id = 2,
                        PedidoId = 2,
                        ProductoId = 2,
                        Producto = new Producto { Id = 2, Nombre = "Producto B" },
                        Cantidad = 1,
                        PrecioUnitario = 200.0M
                    }
                }
            }
        };
            return pedidos;
        }
    }
}
