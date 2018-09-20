using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NGH.API.Helpers;
using NGH.API.Models;

namespace NGH.API.DataAccess.Repositories
{
   public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly NGHDataContext _context;
        public CustomerRepository(NGHDataContext context) : base(context) {
            _context = context;
        }
        public async Task<PagedList<Customer>> GetCustomers(CustomerParams cus)
        {
            var customers = _context.Customers.Include(cg => cg.CustomerGroup).Where(c => 
                                (cus.SearchString != null && 
                                (
                                    c.CompanyName.Contains(cus.SearchString) || 
                                    c.ContactName.Contains(cus.SearchString) ||
                                    cus.SearchString == string.Empty
                                )) || cus.SearchString == null
                            );
            return await PagedList<Customer>.CreateListAsync(customers, cus.PageNumber, cus.PageSize);
        }

        public bool IsCustomerCodeUnique(string code)
        {
            return _context.Customers.Any(c => c.CustomerCode == code && !c.Deleted);
        }
    }
}