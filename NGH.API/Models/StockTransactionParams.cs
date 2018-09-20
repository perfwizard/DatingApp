using System;

namespace NGH.API.Models
{
    public class StockTransactionParams
    {
        public int ProductId { get; set; }
        public DateTime StockDateFrom { get; set; }
        public DateTime StockDateTo { get; set; }
    }
}