using SimpleService.Core.Customer.Interfaces;
using SimpleService.Core.Customer.Models;
using SimpleService.Data.InMemoryStorage;

namespace SimpleService.Data.Repositories
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private readonly InMemoryDataStorageWrapper<CustomerModel> _customerStorage;

        public InMemoryCustomerRepository(InMemoryDataStorageWrapper<CustomerModel> customerStorage) {
            _customerStorage = customerStorage;
        }

        public CustomerModel CreateCustomer(CustomerModel customer) {
            var id = _customerStorage.Add(customer);
            customer.Id = id;
            return customer;
        }

        public Task<CustomerModel> CreateCustomerAsync(CustomerModel customer) => Task.FromResult(CreateCustomer(customer));
        public bool DeleteCustomer(int id) => _customerStorage.RemoveItem(id);
        public Task<bool> DeleteCustomerAsync(int id) => Task.FromResult(DeleteCustomer(id));
        public CustomerModel GetCustomer(int id) => _customerStorage.ContainsKey(id) ? _customerStorage[id] : null;
        public Task<CustomerModel> GetCustomerAsync(int id) => Task.FromResult(GetCustomer(id));
        public IEnumerable<CustomerModel> GetCustomers() => _customerStorage.ListAll();
        public Task<IEnumerable<CustomerModel>> GetCustomersAsync() => Task.FromResult(GetCustomers());
    }
}
