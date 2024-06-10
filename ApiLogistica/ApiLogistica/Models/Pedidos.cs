namespace ApiLogistica.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }  // ID del cliente asociado al pedido
        public Cliente Cliente { get; set; }  // Cliente asociado al pedido
        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; }
        public List<PedidoDetalle> Detalles { get; set; }
    }
}