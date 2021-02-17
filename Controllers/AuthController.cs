using Microsoft.AspNetCore.Mvc;
using dotnet_core_api.Models;
using dotnet_core_api.Config;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace dotnet_core_api.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthManager jwtAuthManager;

        public AuthController(IJwtAuthManager authManager)
        {
            this.jwtAuthManager = authManager;
        }

        [HttpPost("authenticate")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthResponse>> Authenticate(UserCred userCred)
        {
            return await Task.Run<ActionResult<AuthResponse>>(() =>
            {
                var result = this.jwtAuthManager.Authenticate(userCred.username, userCred.password);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
            });

        }
    }

}
