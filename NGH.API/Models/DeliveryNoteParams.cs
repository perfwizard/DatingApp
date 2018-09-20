using System;

namespace NGH.API.Models
{
    public class DeliveryNoteParams : CommonParams
    {
        public string DnNo { get; set; }
        public DateTime? DnDateFrom { get; set; }
        public DateTime? DnDateTo{ get; set; }
        public DateTime? dueDateFrom { get; set; }
        public DateTime? dueDateTo{ get; set; }
        public int CustomerId { get; set; }
        public string SalesPIC { get; set; }
        public string Status { get; set; } // Multiple status ids e.g. '100,200,300'
        public decimal? BalanceFrom { get; set; }
        public decimal? BalanceTo { get; set; }

    }
}