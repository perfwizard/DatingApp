using System.Threading.Tasks;

namespace NGH.API.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
         Task CommitAsync();
         void Commit();
    }
}