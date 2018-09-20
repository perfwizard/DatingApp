using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NGH.API.Models
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public decimal StdWeight { get; set; } = 0;
        [Required]
        public decimal EstWeight { get; set; } = 0;
        public string UOM { get; set; }
        public decimal Wage { get; set; }
        public string CustProductCode { get; set; }
        public decimal StdPrice { get; set; }
        public string ImagePath { get; set; }
        public decimal OnHandQty { get; set; }
        public bool Deleted { get; set; }
        public virtual List<ProductDiscount> ProductDiscounts { get; set; }

        public Product()
        {
            this.ProductDiscounts = new List<ProductDiscount>();
        }
    }
}