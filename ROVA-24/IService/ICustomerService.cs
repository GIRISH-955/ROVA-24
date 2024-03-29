using ROVA_24.DTO;
using ROVA_24.Model;

namespace ROVA_24.IService
{
    public interface ICustomerService
    {
        Task<CustomerDTO> AddCustomer(CustomerDTO customerDto);
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int Customerid);
        Task UpdateCustomer(int customerId, Customer updatedCustomer);
        Task DeleteCustomer(int id);
    }
}
