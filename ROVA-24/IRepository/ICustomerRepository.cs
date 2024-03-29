using ROVA_24.DTO;
using ROVA_24.Model;

namespace ROVA_24.IRepository
{
    public interface ICustomerRepository
    {
        Task<Customer> AddCustomer(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int customerId);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(int id);
    }
}
