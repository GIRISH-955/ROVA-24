using ROVA_24.Models;
using ROVA_24.ServiceResponse;

namespace ROVA_24.IRepository
{
    public interface IAddressRepository
    {
        Task<Customers> GetCustomerByNameAndPhoneNumberAsync(string phoneNumber);
        Task<ServiceResponse<Address>> AddaddressesAsync(Address address);
        Task<bool> checkCustomerExistsOrNotAsync(string name,string phoneNumber);
        Task<List<Address>> GetAllAddressesFromDbAsync();
        Task<Address> getAddressById(int addressId);
        Task<Address> getAddressByCustomerId(int customerId);
        Task<Address> getAddressByIdAndCustomerId(int addressId, int customerId);
        Task<ServiceResponse<Address>> updateCustomerAddressAsync(Address address);
        Task<bool> DeleteUserByAddressIdAsync(int addressId);


    }
}
