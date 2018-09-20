using System.ComponentModel.DataAnnotations.Schema;

namespace NGH.API.Models
{
    public class Status
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        //public string StatusCode { get; set; }
        public string StatusName { get; set; }
    }
}