using System;

namespace NGH.API.Models
{
    public class StockTransaction : BaseEntity
    {
        public int Id { get; set; }
        public string DocNo { get; set; }
        public string PIC { get; set; }
        public int ProductId { get; set; }
        public DateTime StockDate { get; set; }
        public int? FromLocationId { get; set; }
        public int? ToLocationId { get; set; }
        public decimal Qty { get; set; }
        public decimal Weight { get; set; }
        public string UOM { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public bool ReceiveFlag { get; set; }

    }
}