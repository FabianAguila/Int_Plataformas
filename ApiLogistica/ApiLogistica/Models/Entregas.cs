using System;

namespace ApiLogistica.Models
{
    public class Entrega
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string DireccionEntrega { get; set; }
        public string Estado { get; set; }
    }
}
