using ROVA_24.DTO.AddressDTO;
using ROVA_24.DTO.DeleteAddressDTO;
using ROVA_24.DTO.UpdateAddressDTO;
using ROVA_24.ServiceResponse;

namespace ROVA_24.IServices
{
    public interface IAddressServices
    {
        Task<ServiceResponse<AddressResponseDTO>> AddAddressAsync(AddressRequestDTO addressRequestDTO);
        Task<ServiceResponse<List<AddressDTO>>> GetAllAddressAsync();
        Task<ServiceResponse<AddressDTO>> GetAddressesByIdAsync(int addressId);
        Task<ServiceResponse<UpdateAddressDTO>> UpdateAddressesByIdAsync(int addressId,UpdateAddressDTO updateAddressDTO);
        Task<ServiceResponse<string>> DeleteAddressByIdAsync(int addressId);
    }
}
