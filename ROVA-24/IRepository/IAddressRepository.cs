using ROVA_24.Models;
using ROVA_24.ServiceResponse;

namespace ROVA_24.IRepository
{
    public interface IAddressRepository
    {
        Task<Customers> getCustomerByNameAndPhoneNumberAsync(string phoneNumber);
        Task<ServiceResponse<Address>> addAddressesAsync(Address address);
        Task<bool> checkCustomerExistsOrNotAsync(string name,string phoneNumber);
        Task<List<Address>> getAllAddressesFromDbAsync();
        Task<Address> getAddressById(int addressId);
        Task<Address> getAddressByCustomerId(int customerId);
        Task<Address> getAddressByIdAndCustomerId(int addressId, int customerId);
        Task<ServiceResponse<Address>> updateCustomerAddressAsync(Address address);
        Task<bool> deleteAddressByAddressIdAsync(int addressId);


    }
}
