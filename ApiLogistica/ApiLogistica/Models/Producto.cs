namespace ApiLogistica.Models;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public int Precio { get; set; }
    public int Stock { get; set; }
    public int Cantidad { get; set; }
}