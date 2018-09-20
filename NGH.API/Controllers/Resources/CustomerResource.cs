namespace NGH.API.Controllers.Resources
{
    public class CustomerResource
    {
        public int Id { get; set; }
        public string CustomerGroupName { get; set; }
        public string CompanyName { get; set; }
        public string TaxId { get; set; }
        public string BranchCode { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string TelNo { get; set; }
        public string MobileNo { get; set; }
        public string FaxNo { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public decimal Balance { get; set; }
    }
}