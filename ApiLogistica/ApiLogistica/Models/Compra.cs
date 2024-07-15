namespace ApiLogistica.Models
{
    public class Compra
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = default!;
        public string NombreCliente { get; set; } = default!;
        public string DireccionCliente { get; set; } = default!;
        public string TelefonoCliente { get; set; } = default!;
        public string EmailCliente { get; set; } = default!;
        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = default!;
        public string NombreProducto { get; set; } = default!;
        public int PrecioProducto { get; set; }
        public int StockProducto { get; set; }  
        public int CantidadProducto { get; set; }
        public int Cantidad { get; set; }
        public int TotalCosto { get; set; }
    }

}
