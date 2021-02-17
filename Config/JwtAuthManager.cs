using dotnet_core_api.Models;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;

namespace dotnet_core_api.Config
{

    public class JwtAuthManager : IJwtAuthManager
    {
        private DB_PAMYSContext db = new DB_PAMYSContext();
        private Encription bcrypt = new Encription();
        private readonly string key;

        public JwtAuthManager(string key)
        {
            this.key = key;
        }


        public object Authenticate(string username, string password)
        {
            Client user = this.db.Clients
                .Where(e => (e.Username == username)).FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            if (bcrypt.verifyPassword(password, user.Password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, username)
                }),
                    Expires = DateTime.UtcNow.AddHours(10),
                    SigningCredentials =
                        new SigningCredentials(
                                new SymmetricSecurityKey(tokenKey),
                                SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.role = this.db.Roles.Find(user.IdRol);
                return new AuthResponse { jwt = tokenHandler.WriteToken(token), user = user };
            }
            else
            {
                return null;
            }
        }
    }

}
