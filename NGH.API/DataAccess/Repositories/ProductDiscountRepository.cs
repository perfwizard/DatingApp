using NGH.API.Models;

namespace NGH.API.DataAccess.Repositories
{
    public class ProductDiscountRepository : BaseRepository<ProductDiscount>, IProductDiscountRepository
    {
        private readonly NGHDataContext _context;
        public ProductDiscountRepository(NGHDataContext context) : base(context)
        {
            _context = context;
        }

        
    }
}