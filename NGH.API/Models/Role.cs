using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NGH.API.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UIObjectPermission> ObjectPermissions { get; set; }

        public Role()
        {
            UserRoles = new Collection<UserRole>();
            ObjectPermissions = new Collection<UIObjectPermission>();
        }
    }
}