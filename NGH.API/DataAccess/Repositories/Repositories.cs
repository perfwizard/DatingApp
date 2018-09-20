using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NGH.API.Helpers;
using NGH.API.Models;

namespace NGH.API.DataAccess.Repositories
{
    public class StockTransactionRepository : BaseRepository<StockTransaction>, IStockTransactionRepository
    {
        public StockTransactionRepository(NGHDataContext context) : base(context) { }
    }
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        private readonly NGHDataContext _context;

        public StatusRepository(NGHDataContext context) : base(context) {
            _context = context;
        }

        public async Task<Status> GetStatusName(string code)
        {
            return await _context.Status.FirstOrDefaultAsync(s => s.StatusName == code);
        }
    }

}