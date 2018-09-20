using System;
using System.Collections.Generic;
using System.Linq;
using NGH.API.DataAccess;
using NGH.API.Helpers;
using NGH.API.Models;

namespace NGH.API.Services
{
    public class UserService : IUserService
    {
        private NGHDataContext _context;

        public UserService(NGHDataContext context)
        {
            _context = context;
        }
        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);
            if (user == null)
                return null;

            //Check if password is correct. 
            if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;

            //Authentication is success.
            return user;
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new AppException("Password is requried.");
            
            if (_context.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username \"" + user.Username + "\" is already taken.");

            byte[] passwordHash, passwordSalt;
            createPasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
        public void Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);
            if (user == null)
                throw new AppException("User not found");

            if (user.Username != userParam.Username)
            {
                //Username has changed so check if the new username has been taken.
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new AppException("Username " + userParam.Username + " is already taken");
            }

            //Update user property
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;

            //Update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                createPasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            _context.Users.Update(user);
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        private static void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.ASCII.GetBytes(password));
            }
        }
        private static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.ASCII.GetBytes(password));
                for (int i=0; i<computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}