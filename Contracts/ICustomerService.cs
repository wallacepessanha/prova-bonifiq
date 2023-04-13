using ProvaPub.Models;

namespace ProvaPub.Contracts
{
    public interface ICustomerService
    {
        CustomerList ListCustomers(int page);
        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
    }
}
