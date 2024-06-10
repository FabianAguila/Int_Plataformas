using ApiLogistica.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiLogistica.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController
    {
        [HttpGet]
        [Route("listar")]
        public dynamic listarCliente()
        {

            List<Cliente> clientes = new List<Cliente>
            {
                new Cliente
                {
                    Id = 1,
                    Nombre = "Cosme Fulanito",
                    Direccion = "El salto 300",
                    Telefono = 988997755,
                    Email = "cosme@fulanito.com"
                },
                new Cliente
                {
                    Id = 2,
                    Nombre = "Fulano Aldo",
                    Direccion = "Saltando 600",
                    Telefono = 911223344,
                    Email = "aldo@fullano.com"
                }
            };
            return clientes;
        }

        [HttpPost]
        [Route("guardar")]
        public dynamic guardarCliente(Cliente cliente)
        {
            cliente.Id = 1;

            return new
            {
                succes = true,
                message = "cliente registrado",
                result = cliente
            };

        }
     }
}

