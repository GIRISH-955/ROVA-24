using ROVA_24.DTO;
using ROVA_24.IRepository;
using ROVA_24.IService;
using ROVA_24.Model;
using System.Xml.Linq;

namespace ROVA_24.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CustomerDTO> AddCustomer(CustomerDTO customerDto)
        {
            var customer = new Customer
            {
                Name = customerDto.Name,
                email = customerDto.Email,
                Password = customerDto.Password,
                PhoneNumber = customerDto.PhoneNumber,
                Addresses = customerDto.Addresses.Select(a => new Address
                {
                    Street = a.Street,
                    City = a.City,
                    State = a.State,
                    Country = a.Country,
                    ZipCode = a.ZipCode
                }).ToList()
            };

            var addedCustomer = await _customerRepository.AddCustomer(customer);
            if (addedCustomer != null)
            {
                var addedCustomerDto = new CustomerDTO
                {
                    Name = addedCustomer.Name,
                    Email = addedCustomer.email,
                    Password = addedCustomer.Password,
                    PhoneNumber = addedCustomer.PhoneNumber,
                    Addresses = addedCustomer.Addresses.Select(a => new ROVA_24.DTO.AddressDTO // Use ROVA_24.DTO.AddressDTO here
                    {
                        Street = a.Street,
                        City = a.City,
                        State = a.State,
                        Country = a.Country,
                        ZipCode = a.ZipCode
                    }).ToList()
                };
                return addedCustomerDto;
            }
            else
            {
                throw new Exception("Failed to add customer");
            }
        }        
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _customerRepository.GetAllCustomers();
        }

        public async Task<Customer> GetCustomerById(int Customerid)
        {
            return await _customerRepository.GetCustomerById(Customerid);
        }
        public async Task UpdateCustomer(int customerId, Customer updatedCustomer)
        {
            var existingCustomer = await _customerRepository.GetCustomerById(customerId);
            if (existingCustomer != null)
            {
                existingCustomer.Name = updatedCustomer.Name;
                existingCustomer.email = updatedCustomer.email;
                existingCustomer.PhoneNumber = updatedCustomer.PhoneNumber;
                // Update addresses
                foreach (var updatedAddress in updatedCustomer.Addresses)
                {
                    var existingAddress = existingCustomer.Addresses.FirstOrDefault(a => a.AddressId == updatedAddress.AddressId);
                    if (existingAddress != null)
                    {
                        existingAddress.Street = updatedAddress.Street;
                        existingAddress.City = updatedAddress.City;
                        existingAddress.State = updatedAddress.State;
                        existingAddress.Country = updatedAddress.Country;
                        existingAddress.ZipCode = updatedAddress.ZipCode;
                    }
                }
                await _customerRepository.UpdateCustomer(existingCustomer);
            }
            else
            {
                throw new Exception("Customer not found");
            }
        }
    

    public async Task DeleteCustomer(int id)
        {
            await _customerRepository.DeleteCustomer(id);
        }
    }
}
