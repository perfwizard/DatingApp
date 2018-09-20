using System.Collections.Generic;
using NGH.API.Models;

namespace NGH.API.Controllers.Resources
{
    public class UserResource
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelNo { get; set; }
        public string ImagePath { get; set; }

        public List<Role> Roles { get; set; }
    }
}