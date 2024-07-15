using System.Collections.Generic;

namespace ApiLogistica.Models
{
    public class Cliente
    {   
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string Direccion { get; set; } = default!;
        public string Telefono { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}