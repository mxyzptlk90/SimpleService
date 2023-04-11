using SimpleService.Core.Customer.Models;

namespace SimpleService.Core.Customer.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Models.CustomerModel>> GetCustomersAsync();
        Task<Models.CustomerModel> GetCustomerAsync(int id);
        Task<bool> DeleteCustomerAsync(int id);
        Task<Models.CustomerModel> CreateCustomerAsync(Models.CustomerModel customer);
        IEnumerable<Models.CustomerModel> GetCustomers();
        Models.CustomerModel GetCustomer(int id);
        bool DeleteCustomer(int id);
        Models.CustomerModel CreateCustomer(Models.CustomerModel customer);

    }
}
