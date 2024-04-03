using ROVA_24.DTO.AddressDTO;
using ROVA_24.DTO.DeleteAddressDTO;
using ROVA_24.DTO.UpdateAddressDTO;
using ROVA_24.ServiceResponse;

namespace ROVA_24.IServices
{
    public interface IAddressServices
    {
        Task<ServiceResponse<AddressResponseDTO>> addAddressAsync(AddressRequestDTO addressRequestDTO);
        Task<ServiceResponse<List<AddressDTO>>> getAllAddressAsync();
        Task<ServiceResponse<AddressDTO>> getAddressesByIdAsync(int addressId);
        Task<ServiceResponse<UpdateAddressDTO>> updateAddressesByIdAsync(int addressId,UpdateAddressDTO updateAddressDTO);
        Task<ServiceResponse<string>> deleteAddressByIdAsync(int addressId);
    }
}
