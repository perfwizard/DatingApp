using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NGH.API.Models
{
    public class ProductDiscount : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         public int Id { get; set; }
         [ForeignKey("ProductId")]
         public int ProductId { get; set; }
         public Product Product { get; set; }
         public int CustomerId { get; set; }
         public decimal DiscPrice { get; set; }
         public decimal Discount { get; set; }
         public bool IsPercent { get; set; }
    }
}