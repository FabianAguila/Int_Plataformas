using System;
using System.Collections.Generic;

namespace ApiLogistica.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; }
        public List<PedidoDetalle>? Detalles { get; set; }
    }
}
