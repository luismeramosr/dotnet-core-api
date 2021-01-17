using dotnet_core_api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace dotnet_core_api.Controllers {

    // Ejemplo de controlador
    [ApiController]
    // [Route("controller-route"] Ruta base del controlador
    // Los Routes definidos en los endpoints serian de la forma
    // /controller-route/endpoint-route
    public class TestController : ControllerBase {
        
        // Instancia de la base de datos
        private DB_PAMYSContext db = new DB_PAMYSContext();

        // Endpoint de prueba
        [HttpGet]
        // si el controlador tuviese ruta seria
        // /controller-route/ruta-definida-abajo
        [Route("/")]        
        public List<User> GetUsers() 
        {
            return this.db.Users.ToList();
        }
    }

}