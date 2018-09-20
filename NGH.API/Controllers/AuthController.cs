using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NGH.API.Controllers.Resources;
using NGH.API.DataAccess.Repositories;

namespace NGH.API.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepo;
        public AuthController(IConfiguration config, IMapper mapper, IUserRepository userRepo)
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
            this.config = config;

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginUserResource user)
        {
            // 1. Get user to login from database.
            var loggedInUser = await this.userRepo.Login(user.Username, user.Password);

            // 2. If user not exists notify error.
            if (loggedInUser == null)
                return Unauthorized();

            // 3. Specify cliams which stores in token. Claims should be as minimum as possible.
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, loggedInUser.Id.ToString()),
                new Claim(ClaimTypes.Name, loggedInUser.Username)
            };

            // 4. Generate token using Json Web Token (JWT)
            var tokenHandler = new JwtSecurityTokenHandler();
            // 5. Get secret key from setting file.
            var key = Encoding.ASCII.GetBytes(this.config.GetSection("AppSettings:Token").Value);
            // 6. Create token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            // 7. Create token.
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var userResource = this.mapper.Map<UserResource>(loggedInUser);

            // 8. Return token string and user resource object containing necessary user info.
            return Ok(new
            {
                token = tokenString,
                user = userResource
            });
        }
    }
}