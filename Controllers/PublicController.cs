using System.Net.Mime;
using System.Threading.Tasks;
using dotnet_core_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using dotnet_core_api.Config;

namespace dotnet_core_api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/public")]
    public class PublicController : ControllerBase
    {
        private DB_PAMYSContext db = new DB_PAMYSContext();
        private Encription bcrypt = new Encription();

        [HttpPost("client")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Client>> saveClient([FromBody] Client client)
        {
            return await Task.Run<ActionResult<Client>>(() =>
            {
                Client newClient = client;
                newClient.Password = bcrypt.hashPassword(newClient.Password);
                this.db.Clients.Add(newClient);
                this.db.SaveChanges();
                return Ok(client);
            });
        }


    }

}
