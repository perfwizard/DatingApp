using System;

namespace NGH.API.Models
{
    public class RedemptionPrice
    {
        public int Id { get; set; }
        public decimal GoldType { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public int PriceType { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}