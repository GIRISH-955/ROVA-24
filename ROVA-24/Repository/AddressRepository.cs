using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ROVA_24.Data;
using ROVA_24.IRepository;
using ROVA_24.Models;
using ROVA_24.ServiceResponse;

namespace ROVA_24.Repository
{
    public class AddressRepository:IAddressRepository
    {
        private readonly Rova_23DBContext _dbContext; 
        public AddressRepository(Rova_23DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customers> GetCustomerByNameAndPhoneNumberAsync(string phoneNumber)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }

        public async Task<ServiceResponse<Address>> AddaddressesAsync(Address address)
        {
            _dbContext.Add(address);
            await _dbContext.SaveChangesAsync();
            return new ServiceResponse<Address>
            {
                Success = true,
                Data = address
            };
        }
        public async Task<bool> checkCustomerExistsOrNotAsync(string name,string phoneNumber)
        {
            var isExist = await _dbContext.Customers.AnyAsync(c => c.Name == name && c.PhoneNumber == phoneNumber);
            return isExist;
        }
        public async Task<List<Address>> GetAllAddressesFromDbAsync()
        {
            //return await _dbContext.Address.ToListAsync();
            return await _dbContext.Address.Include(a => a.Customer).ToListAsync();
        }
        public async Task<Address> getAddressById(int addressId)
        {
            Address address = await _dbContext.Address.Where(i => i.AddressId == addressId).FirstOrDefaultAsync();
            return address;
        }
        public async Task<Address> getAddressByCustomerId(int customerId)
        {
            Address address = await _dbContext.Address.Where(i => i.CustomerId == customerId).FirstOrDefaultAsync();
            return address;
        }
        public async Task<Address> getAddressByIdAndCustomerId(int addressId, int customerId)
        {
            Address address = await _dbContext.Address
                                        .Where(a => a.AddressId == addressId && a.CustomerId == customerId)
                                        .FirstOrDefaultAsync();
            return address;
        }
        public async Task<ServiceResponse<Address>> updateCustomerAddressAsync(Address address)
        {
            try
            {
                // Update the user in the database
                 _dbContext.Address.Update(address);
                await _dbContext.SaveChangesAsync();

                return new ServiceResponse<Address>
                {
                    Success = true,
                    Data = address,
                };
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a failure response
                return new ServiceResponse<Address>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<bool> DeleteUserByAddressIdAsync(int addressId)
        {
            var address = await _dbContext.Address.FirstOrDefaultAsync(u => u.AddressId == addressId);
            if (address != null)
            {
               _dbContext.Address.Remove(address);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false; // User not found
        }


    }
}
