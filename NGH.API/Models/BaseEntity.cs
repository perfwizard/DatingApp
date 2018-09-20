using System;

namespace NGH.API.Models
{
    public class BaseEntity
    {
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}