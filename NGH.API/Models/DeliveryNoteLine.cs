using System.ComponentModel.DataAnnotations.Schema;

namespace NGH.API.Models
{
    public class DeliveryNoteLine : BaseEntity
    {
         public int Id { get; set; }
         public int LineNumber { get; set; }
         public int DeliveryNoteId { get; set; }
         public DeliveryNote DeliveryNote { get; set; }
         public string ProductId { get; set; }
         public string ProductCode { get; set; }
         public string ProductName { get; set; }
         public decimal Qty { get; set; }
         public decimal Weight { get; set; }
         public string UOM { get; set; }
         public decimal UnitPrice { get; set; }
         public decimal Discount { get; set; }
         public decimal LineTotal { get; set; }
    }
}