using System.Threading.Tasks;

namespace NGH.API.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NGHDataContext _context;
        public UnitOfWork(NGHDataContext context)
        {
            this._context = context;
        }
        public async Task CommitAsync()
        {
            await this._context.SaveChangesAsync();
        }
        public void Commit() 
        {
            this._context.SaveChanges();
        }
    }
}