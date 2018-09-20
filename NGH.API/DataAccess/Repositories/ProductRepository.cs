using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NGH.API.Helpers;
using NGH.API.Models;

namespace NGH.API.DataAccess.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly NGHDataContext _context;
        public ProductRepository(NGHDataContext context) : base(context) { 
            _context = context;
        }
        public async Task<PagedList<Product>> GetProductList(ProductParams prod)
        {
            var products = _context.Products.Where(p => 
                                prod.SearchString != null && 
                                (
                                    p.ProductCode.Contains(prod.SearchString) ||
                                    p.ProductName.Contains(prod.SearchString) ||
                                    prod.SearchString == ""
                                ) || prod.SearchString == null
                            );
            
            return await PagedList<Product>.CreateListAsync(products, prod.PageNumber, prod.PageSize);
        }
        public bool IsProductCodeUnique(string code)
        {
            return _context.Products.Any(p => p.ProductCode == code && !p.Deleted);
        }
    }
}