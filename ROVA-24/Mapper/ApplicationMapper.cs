using AutoMapper;
using ROVA_24.DTO.AddressDTO;
using ROVA_24.Models;

namespace ROVA_24.Mapper
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper() 
        {
            CreateMap<AddressRequestDTO,Address>();
            CreateMap<Address , AddressResponseDTO>();
            CreateMap<AddressDTO , Address>();
        }
    }
}
