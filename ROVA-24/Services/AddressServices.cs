using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ROVA_24.DTO.AddressDTO;
using ROVA_24.DTO.DeleteAddressDTO;
using ROVA_24.DTO.UpdateAddressDTO;
using ROVA_24.IRepository;
using ROVA_24.IServices;
using ROVA_24.Models;
using ROVA_24.ServiceResponse;

namespace ROVA_24.Services
{
    public class AddressServices : IAddressServices
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public AddressServices(IAddressRepository addressRepository,IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<AddressResponseDTO>> AddAddressAsync(AddressRequestDTO addressRequestDTO)
        {
            var existingCustomer =  await _addressRepository.GetCustomerByNameAndPhoneNumberAsync(addressRequestDTO.PhoneNumber);
            if(existingCustomer!=null) 
            {
                var newAddress = new Address
                {
                    CustomerId = existingCustomer.CustomerId,
                    Street = addressRequestDTO.Street,
                    City = addressRequestDTO.City,
                    State = addressRequestDTO.State,
                    Country = addressRequestDTO.Country,
                    Zipcode = addressRequestDTO.Zipcode,
                    Name = addressRequestDTO.Name,
                    PhoneNumber = addressRequestDTO.PhoneNumber,
                    AlternatePhoneNumber = addressRequestDTO.AlternatePhoneNumber

                };
                await _addressRepository.AddaddressesAsync(newAddress);
                var addressResponseDTO = new AddressResponseDTO
                {
                    AddressId = newAddress.AddressId,
                    Name = newAddress.Name, // Accessing properties after awaiting the task
                    PhoneNumber = existingCustomer.PhoneNumber, // Accessing properties after awaiting the task
                    Street = newAddress.Street,
                    City = newAddress.City,
                    State = newAddress.State,
                    Country = newAddress.Country,
                    Zipcode = newAddress.Zipcode,
                    AlternatePhoneNumber = newAddress.AlternatePhoneNumber
                };
                return new ServiceResponse<AddressResponseDTO>
                {
                    Success = true,
                    Data = addressResponseDTO,
                    Status= System.Net.HttpStatusCode.OK,
                    Message = "Address added successfully"
                };
            }
            return new ServiceResponse<AddressResponseDTO>
            {
                Success = false,
                Status= System.Net.HttpStatusCode.BadRequest,
                Message = "Customer does not exist"
            };

        }

        public async Task<ServiceResponse<List<AddressDTO>>> GetAllAddressAsync()
        {
            var response = new ServiceResponse<List<AddressDTO>>();

            try
            {
                var addresses = await _addressRepository.GetAllAddressesFromDbAsync();

                if (addresses != null)
                {
                    List<AddressDTO> addressDto = new List<AddressDTO>();
                    foreach (var address in addresses)
                    {
                        addressDto.Add(new AddressDTO
                        {
                            AddressId = address.AddressId,
                            CustomerId = address.CustomerId,
                            Name= address.Name,
                            PhoneNumber = address.PhoneNumber,
                            AlternatePhoneNumber = address.AlternatePhoneNumber,
                            Street = address.Street,
                            City = address.City,
                            State = address.State,
                            Country = address.Country,
                            Zipcode = address.Zipcode,    
                        });
                    }

                    response.Data = addressDto;
                    response.Success = true;
                    response.Status= System.Net.HttpStatusCode.OK;
                    response.Message = "Addresses fetched successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Status = System.Net.HttpStatusCode.BadRequest;
                    response.Message = "No addresses found.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error fetching addresses: " + ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<AddressDTO>> GetAddressesByIdAsync(int addressId)
        {
            try
            {
                var response = await _addressRepository.getAddressById(addressId);
                if (response != null)
                {
                    var addressdto = new AddressDTO
                    {
                        AddressId = response.AddressId,
                        CustomerId = response.CustomerId,
                        Name = response.Name,
                        PhoneNumber = response.PhoneNumber,
                        AlternatePhoneNumber = response.AlternatePhoneNumber,
                        Street = response.Street,
                        City = response.City,
                        State = response.State,
                        Country = response.Country,
                        Zipcode = response.Zipcode
                    };
                    return new ServiceResponse<AddressDTO>
                    {
                        Success = true,
                        Data = addressdto,
                        Status= System.Net.HttpStatusCode.OK,
                        Message = "Address fetched successfully"
                    };

                }
                return new ServiceResponse<AddressDTO>
                {
                    Success = false,
                    Status= System.Net.HttpStatusCode.BadRequest,
                    Message = "Address not found"
                };

            }
            catch (Exception ex)
            {
                return new ServiceResponse<AddressDTO>
                {
                    Message = ex.Message,
                };
            }
        }
        public async Task<ServiceResponse<UpdateAddressDTO>> UpdateAddressesByIdAsync(int addressId, UpdateAddressDTO updateAddressDTO)
        {
            var existingCustomer = await _addressRepository.getAddressByCustomerId(updateAddressDTO.CustomerId);
            if (existingCustomer == null)
            {
                return new ServiceResponse<UpdateAddressDTO>
                {
                    Status= System.Net.HttpStatusCode.BadRequest,
                    Message = "Customer does not exist"
                };
            }
            var existingAddress = await _addressRepository.getAddressByIdAndCustomerId(addressId, updateAddressDTO.CustomerId);
            if (existingAddress == null)
            {
                return new ServiceResponse<UpdateAddressDTO>
                {
                    Status= System.Net.HttpStatusCode.BadRequest,
                    Message = "Address does not exist for this customer"
                };
            }
            existingAddress.Name = updateAddressDTO.Name;
            existingAddress.AlternatePhoneNumber = updateAddressDTO.AlternatePhoneNumber;
            existingCustomer.Street = updateAddressDTO.Street;
            existingCustomer.City = updateAddressDTO.City;
            existingCustomer.State = updateAddressDTO.State;
            existingCustomer.Country = updateAddressDTO.Country;
            existingCustomer.Zipcode = updateAddressDTO.Zipcode;

            var result = await _addressRepository.updateCustomerAddressAsync(existingCustomer);
            if (result.Success)
            {
                return new ServiceResponse<UpdateAddressDTO>
                {
                    Success = true,
                    Data = updateAddressDTO,
                    Status= System.Net.HttpStatusCode.OK,
                    Message = "address Updated successfully"
                };
            }
            return new ServiceResponse<UpdateAddressDTO>
            {
                Success = true,
                Status= System.Net.HttpStatusCode.BadRequest,
                Message = "Error occured while updating"
            };

        }
        public async Task<ServiceResponse<string>> DeleteAddressByIdAsync(int addressId)
        {
            var existingAddress = await _addressRepository.getAddressById(addressId);
            if (existingAddress != null)
            {
                var deletionResult = await _addressRepository.DeleteUserByAddressIdAsync(addressId);
                if (deletionResult)
                {
                    return new ServiceResponse<string>
                    {
                        Success = true,
                        Status = System.Net.HttpStatusCode.OK,
                        Message = "Address is deleted."
                    };
                }
            }
            return new ServiceResponse<string>
            {
                Success = false,
                Status = System.Net.HttpStatusCode.BadRequest,
                Message = "Address Does not exist"
            };
        }
    }
}
           