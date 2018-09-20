using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NGH.API.Models;

namespace NGH.API.Controllers.Resources
{
    public class RegisterUserResource
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelNo { get; set; }
        public string ImagePath { get; set; }

        public List<Role> Roles { get; set; }
    }
}