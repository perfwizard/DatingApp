using System;
using System.Collections.Generic;
using NGH.API.Models;

namespace NGH.API.Controllers.Resources
{
    public class DeliveryNoteResource
    {
        public int Id { get; set; }
        public string DnNo { get; set; }
        public DateTime DnDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string TaxId { get; set; }  
        public string BranchCode { get; set; }      
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime DueDate { get; set; }
        public string SalesPIC { get; set; }
        public decimal Discount { get; set; }
        public decimal ActualWeight { get; set; }
        public decimal StdPrice { get; set; }
        public decimal TradePrice { get; set; }
        public decimal GoldPercent { get; set; }
        public string Remark { get; set; }
        public string InternalMemo { get; set; }
        public decimal Subtotal { get; set; }
        public decimal VatRate { get; set; }
        public decimal VatAmount { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Balance { get; set; }
        public bool IssueInvoice { get; set; }
        public string StatusId { get; set; }
        public string StatusName {get; set; }
        public ICollection<DeliveryNoteLineResource> DeliveryNoteLines { get; set; }
    }
}