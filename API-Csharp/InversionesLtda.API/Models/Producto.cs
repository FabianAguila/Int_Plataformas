using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InversionesLtda.API.Models
{
    public class Producto
    {
        public int id { get; set; }
        public string nombre_produc { get; set; }
        public string precio { get; set; }
        public string total { get; set; }
    }
}
