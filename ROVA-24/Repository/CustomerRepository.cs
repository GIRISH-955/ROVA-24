using Microsoft.EntityFrameworkCore;
using ROVA_24.Data;
using ROVA_24.DTO;
using ROVA_24.IRepository;
using ROVA_24.Model;

namespace ROVA_24.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ROVADBContext _context;

        public CustomerRepository(ROVADBContext context)
        {
            _context = context;
        }
        public async Task<Customer> AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            return await _context.Customers.Include(c => c.Addresses).FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }
        public async Task UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }
            public async Task DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
    

