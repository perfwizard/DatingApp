using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace NGH.API.Models
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [Required]
        public string Email { get; set; }
        public string TelNo { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
        public bool Deleted { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

        public User()
        {
            UserRoles = new Collection<UserRole>();
        }
    }
}