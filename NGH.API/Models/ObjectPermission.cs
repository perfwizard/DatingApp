namespace NGH.API.Models
{
    public class UIObjectPermission
    {
        public int ObjectId { get; set; }
        public int PermissionId { get; set; }
        public UIObject Object { get; set; }
        public Permission Permission { get; set; }

    }
}