namespace NGH.API.Controllers.Resources
{
    public class ProductDiscountResource
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal DiscPrice { get; set; }
        public decimal Discount { get; set; }
        public bool IsPercent { get; set; }
    }
}