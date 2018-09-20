using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NGH.API.Models
{
    public class UIObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UIObjectPermission> ObjectPermissions { get; set; }
        
        public UIObject()
        {
            ObjectPermissions = new Collection<UIObjectPermission>();
        }
    }
}