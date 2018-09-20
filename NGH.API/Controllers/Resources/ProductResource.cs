using System.Collections.Generic;
using NGH.API.Models;

namespace NGH.API.Controllers.Resources
{
    public class ProductResource
    {
        public int Id { get; set; }
        public decimal StdWeight { get; set; }
        public decimal EstWeight { get; set; }
        public string UOM { get; set; }
        public decimal Wage { get; set; }
        public string CustProductCode { get; set; }
        public decimal StdPrice { get; set; }
        public string ImagePath { get; set; }
        public decimal OnHandQty { get; set; }
        public bool Deleted { get; set; }
        public List<ProductDiscountResource> Discounts { get; set; }

        public ProductResource()
        {
            this.Discounts = new List<ProductDiscountResource>();
        }
    }
}