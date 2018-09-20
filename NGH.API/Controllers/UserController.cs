using System;
using System.Collections.Generic;
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
using NGH.API.DataAccess.UnitOfWork;
using NGH.API.Helpers;
using NGH.API.Models;

namespace NGH.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _uom;

        public UserController(IUserRepository userRepo, 
                IMapper mapper, IConfiguration config, IUnitOfWork uom)
        {
            _config = config;
            _uom = uom;
            _mapper = mapper;
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<List<UserResource>> Get(UserParams param)
        {
            var user = await _userRepo.GetUserList(param);

            var userList = _mapper.Map<List<UserResource>>(user);

            Response.AddPagination(user.CurrentPage, user.PageSize, user.TotalCount, user.TotalPages);

            return userList;
        }
        [HttpGet("{id}", Name="GetUser")]
        public async Task<IActionResult> GetById(int id) {
            var user = await _userRepo.GetByIdAsync(id);

            if (user == null)
                return NotFound("User not found");

            return Ok(_mapper.Map<UserResource>(user));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginUserResource user)
        {
            // 1. Get user to login from database.
            var loggedInUser = await _userRepo.Login(user.Username, user.Password);

            // 2. If user not exists notify error.
            if (loggedInUser == null)
                return Unauthorized();

            // 3. Specify cliams which stores in token. Claims should be as minimum as possible.
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, loggedInUser.Id.ToString()),
                new Claim(ClaimTypes.Name, 
                    string.IsNullOrEmpty(loggedInUser.Username) ? loggedInUser.Email : loggedInUser.Username)
            };

            // 4. Generate token using Json Web Token (JWT)
            var tokenHandler = new JwtSecurityTokenHandler();
            // 5. Get secret key from setting file.
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Secret").Value);
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

            var returnUser = _mapper.Map<User>(loggedInUser);

            // 8. Return token string and user resource object containing necessary user info.
            return Ok(new
            {
                token = tokenString,
                user = returnUser
            });
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserResource user)
        {
            if (!string.IsNullOrEmpty(user.Username))
                user.Username = user.Username.ToLower();
                
            if (await _userRepo.UserExists(user.Username, user.Email))
                ModelState.AddModelError("Duplicate", "Username or email already exist.");

            var userToCreate = _mapper.Map<User>(user);

            //Validate request goes here.
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            userToCreate.CreateDate = DateTime.Now;
            userToCreate.CreateBy = ClaimTypes.NameIdentifier;

            var createdUser = await _userRepo.Register(userToCreate);
            await _uom.CommitAsync();

            var userToReturn = _mapper.Map<UserResource>(createdUser);

            return CreatedAtRoute("GetUser", 
                new { controller="User", id = createdUser.Id }, userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]UserResource user)
        {
            if (user.Id != id)
                return Unauthorized();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null || id != Int32.Parse(userId.Value))
                return Unauthorized();


            var storedUser = await _userRepo.GetByIdAsync(id);
            if (storedUser == null)
                return NotFound("User not found.");
            
            _mapper.Map(user, storedUser);
            await _uom.CommitAsync();

            return NoContent();
        }

    }
}