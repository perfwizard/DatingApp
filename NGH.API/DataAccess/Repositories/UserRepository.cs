using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NGH.API.DataAccess.UnitOfWork;
using NGH.API.Helpers;
using NGH.API.Models;

namespace NGH.API.DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly NGHDataContext _context;
        public UserRepository(NGHDataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedList<User>> GetUserList(UserParams param)
        {
            var user = _context.Users.Where(u => 
                                param.SearchString == "" || param.SearchString == null ||
                                u.Username.Contains(param.SearchString) ||
                                u.Email.Contains(param.SearchString));


            return await PagedList<User>.CreateListAsync(user, param.PageNumber, param.PageSize);
        }

        public async Task<User> Login(string username, string password)
        {
            // 1. Get user with the specified username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
 
            // 2. If no user found then return null
            if (user == null)
                return null;

            // 3. Check if password is valid.
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<User> Register(User user)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);

            return user;
        }

        public Task ResetPassword(string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UserExists(string username, string email)
        {
            return await _context.Users.AnyAsync(u => u.Username == username || u.Email == email);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            // 1. Check if password is not null.
            if (password == null) throw new ArgumentNullException("password");

            // 2. Check if password is not empty.
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace.", "password");

            // 3. Check if hashed password length is valid
            if (passwordHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");

            // 4. Check if password salt length is valid
            if (passwordSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordSalt");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                // 5. Calculate hash password based on password parameter received.
                var computedHash = hmac.ComputeHash(System.Text.Encoding.ASCII.GetBytes(password));

                // 6. Loop through has calculated in 5. and compare each byte to password hash
                // retrieved from database.
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}