using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NGH.API.Helpers;
using NGH.API.Models;

namespace NGH.API.DataAccess.Repositories
{
    public class DeliveryNoteRepository : BaseRepository<DeliveryNote>, IDeliveryNoteRepository
    {
        private readonly NGHDataContext _context;

        public DeliveryNoteRepository(NGHDataContext context) : base(context) 
        {
            _context = context;
        }

        public Task<PagedList<DeliveryNote>> GetDeliveryNotes(DeliveryNoteParams param)
        {
            var dns = _context.DeliveryNotes
                .Include(l => l.DeliveryNoteLines)
                .Include(c => c.Customer)
                .Include(s => s.Status)
                .Where(dn => 
                    param.SearchString != null && (
                        param.SearchString == string.Empty ||
                        dn.DnNo.Contains(param.SearchString) ||
                        dn.Remark.Contains(param.SearchString) ||
                        dn.InternalMemo.Contains(param.SearchString) ||
                        dn.SalesPIC.Contains(param.SearchString)
                    ) ||
                    (
                        param.SearchString == null && (
                            (param.CustomerId == 0 || dn.CustomerId == param.CustomerId) &&
                            (string.IsNullOrEmpty(param.SalesPIC) || dn.SalesPIC == param.SalesPIC) &&
                            //(string.IsNullOrEmpty(param.Status) || param.Status.Contains(dn.Status.ToString())) &&
                            (param.DnDateFrom == null || dn.DnDate >= param.DnDateFrom.Value) &&
                            (param.DnDateTo == null || dn.DnDate <= param.DnDateTo.Value) &&
                            (param.dueDateFrom == null || dn.DueDate >= param.dueDateFrom.Value) &&
                            (param.dueDateTo == null || dn.DueDate <= param.dueDateTo.Value) &&
                            (param.BalanceFrom == null || dn.Balance >= param.BalanceFrom.Value) &&
                            (param.BalanceTo == null || dn.Balance <= param.BalanceTo.Value)
                        )
                    ) 
                );

            return PagedList<DeliveryNote>.CreateListAsync(dns, param.PageNumber, param.PageSize);
        }
        public async Task<DeliveryNote> GetLastDnNo(string dnPrefix)
        {
            return await _context.DeliveryNotes
                .Where(dn => dn.DnNo.StartsWith(dnPrefix))
                .OrderByDescending(dn => dn.DnNo).FirstOrDefaultAsync();
        }

        public async Task<DeliveryNote> GetDeliveryNoteWithLines(int id)
        {
            return await _context.DeliveryNotes
                    .Include(d => d.Status)
                    .Include(d => d.DeliveryNoteLines)
                    .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}