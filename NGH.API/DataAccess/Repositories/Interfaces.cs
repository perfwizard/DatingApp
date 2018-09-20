using System.Collections.Generic;
using System.Threading.Tasks;
using NGH.API.Helpers;
using NGH.API.Models;

namespace NGH.API.DataAccess.Repositories
{
    public interface ICustomerRepository : IRepository<Customer> 
    {
        Task<PagedList<Customer>> GetCustomers(CustomerParams param);
        bool IsCustomerCodeUnique(string code);
    }
    public interface IProductRepository : IRepository<Product> {
        Task<PagedList<Product>> GetProductList(ProductParams param);
        bool IsProductCodeUnique(string code);
    }

    public interface  IDeliveryNoteRepository : IRepository<DeliveryNote> {
        Task<PagedList<DeliveryNote>> GetDeliveryNotes(DeliveryNoteParams param);
        Task<DeliveryNote> GetLastDnNo(string dnPrefix);
        Task<DeliveryNote> GetDeliveryNoteWithLines(int id);
    }
    public interface  IDeliverNoteLineRepository : IRepository<DeliveryNoteLine> {
        Task<IEnumerable<DeliveryNoteLine>> GetDnLineByDnId(int id);
    }
    public interface  IProductDiscountRepository : IRepository<ProductDiscount> {}
    public interface  IStockTransactionRepository : IRepository<StockTransaction> {}
    public interface IUserRepository : IRepository<User>
    {
        Task<User> Register(User user);
        Task ResetPassword(string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username, string email);
         Task<PagedList<User>> GetUserList(UserParams param);
    }

    public interface  IStatusRepository : IRepository<Status> 
    {
        Task<Status> GetStatusName(string code);
    }
}