using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using dotnet_core_api.env;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using dotnet_core_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
// necesario para el webroot
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using dotnet_core_api.utilities;
namespace dotnet_core_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/client/")]
    public class ClientController : ControllerBase
    {
        public PhotoUtilities photoUtilities = new PhotoUtilities();
        private readonly IConfiguration _configuration;
        public static IWebHostEnvironment _webHostEnvironment;
        private EnviromentApp env;
        public ClientController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _configuration = configuration;//instancio la configuracion
            _webHostEnvironment = webHostEnvironment;
            this.env = new EnviromentApp(_webHostEnvironment, _configuration); // se la paso a mi modelo con las constantes
        }
        private DB_PAMYSContext db = new DB_PAMYSContext();

        // Todos los endpoints son funciones asincronas, para mejorar
        // el performance, aun falta ver una manera de retornar los estados http
        // y validar errores.
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<Client>> getAll()
        {
            return await Task.Run<IEnumerable<Client>>(() =>
            {
                return this.db.Clients.ToList();
            });
        }

        // Recibe el parametro id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Client>> getById(int id)
        {
            return await Task.Run<ActionResult<Client>>(() =>
            {
                var client = this.db.Clients.Find(id);
                if (client != null)
                    return Ok(client);
                else
                    return NotFound();
            });
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Client>> save(Client client)
        {
            return await Task.Run<ActionResult<Client>>(() =>
            {
                if (client == null)
                    return BadRequest();
                this.db.Clients.Add(client);
                this.db.SaveChanges();
                var newClient = client;
                newClient.role = this.db.Roles.Find(newClient.IdRol);
                return Ok(newClient);
            });
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Client>> update(Client client)
        {
            return await Task.Run<ActionResult<Client>>(() =>
            {
                try
                {
                    var updateTask = this.db.Clients.Update(client);
                    if (updateTask.State == EntityState.Modified)
                        this.db.SaveChanges();
                    var updatedClient = client;
                    updatedClient.role = this.db.Roles.Find(updatedClient.IdRol);
                    return Ok(client);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            });
        }

        [HttpDelete("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> delete(int id)
        {
            var client = this.db.Clients.Find(id);
            return await Task.Run<IActionResult>(() =>
            {
                try
                {
                    var deleteTask = this.db.Clients.Remove(client);
                    if (deleteTask.State == EntityState.Deleted)
                        this.db.SaveChanges();
                    return Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            });
        }

        // upload image client
        [Route("photos/upload")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Client>> uploadPhotoClient([FromForm] IFormFile imgFile, [FromForm] int idClient)
        {
            return await Task.Run<ActionResult<Client>>(async () =>
            {
                var clientCurrent = this.db.Clients.Find(idClient);
                // existe un cliente y la imgfile almenos algo
                if (clientCurrent != null || imgFile.Length != 0)
                {
                    string path = this.env.pathClientPhotos;
                    // nombre copio el archivo y retorna el namefile
                    string nameFileEncript = await this.photoUtilities.copyPhoto(imgFile, path);
                    //elimina el archivo si existe
                    await this.photoUtilities.removePhoto(clientCurrent.ProfilePictureUrl, path);
                    // guardo en la bd
                    clientCurrent.ProfilePictureUrl = nameFileEncript;
                    clientCurrent.role = this.db.Roles.Find(clientCurrent.IdRol);
                    this.db.SaveChanges();
                    return Ok(clientCurrent);
                }
                return BadRequest();

            });
        }


    }
}

